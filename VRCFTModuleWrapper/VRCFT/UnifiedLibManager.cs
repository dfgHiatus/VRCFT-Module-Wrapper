﻿using BaseX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace VRCFTModuleWrapper.VRCFT
{
    public enum ModuleState
    {
        Uninitialized = -1, // If the module is not initialized, we can assume it's not being used
        Idle = 0,   // Idle and above we can assume the module in question is or has been in use
        Active = 1  // We're actively getting tracking data from the module
    }

    public static class UnifiedLibManager
    {
        #region Delegates
        public static Action<ModuleState, ModuleState> OnTrackingStateUpdate = (b, b1) => { };
        #endregion

        #region Statuses
        public static ModuleState EyeStatus
        {
            get => _eyeModule?.Status.EyeState ?? ModuleState.Uninitialized;
            set
            {
                if (_eyeModule != null)
                    _eyeModule.Status.EyeState = value;
                OnTrackingStateUpdate.Invoke(value, LipStatus);
            }
        }

        public static ModuleState LipStatus
        {
            get => _lipModule?.Status.LipState ?? ModuleState.Uninitialized;
            set
            {
                if (_lipModule != null)
                    _lipModule.Status.LipState = value;
                OnTrackingStateUpdate.Invoke(EyeStatus, value);
            }
        }
        #endregion

        #region Modules
        private static ExtTrackingModule _eyeModule, _lipModule;
        private static readonly Dictionary<ExtTrackingModule, Thread> UsefulThreads =
            new Dictionary<ExtTrackingModule, Thread>();
        #endregion

        private static Thread _initializeWorker;

        public static void Initialize(bool eye = true, bool lip = true)
        {
            if (_initializeWorker != null && _initializeWorker.IsAlive) _initializeWorker.Abort();

            // Start Initialization
            _initializeWorker = new Thread(() =>
            {
                // Kill lingering threads
                TeardownAllAndReset();

                // Init
                FindAndInitRuntimes(eye, lip);
            });

            UniLog.Log("Starting initialization for " + (eye ? "eye" : "") + (eye && lip ? " and " : "") + (lip ? "lip" : "") + " tracking");
            _initializeWorker.Start();
        }

        private static List<Type> LoadExternalModules()
        {
            var returnList = new List<Type>();
            var customLibsPath = Path.Combine(Utils.PersistentDataDirectory, "CustomLibs");

            if (!Directory.Exists(customLibsPath))
                Directory.CreateDirectory(customLibsPath);

            UniLog.Log("Loading External Modules...");

            // Load dotnet dlls from the VRCFTLibs folder
            foreach (var dll in Directory.GetFiles(customLibsPath, "*.dll"))
            {
                if (RemoveZoneIdentifier(dll))
                    // Skip the module which will most likely crash the app
                    continue;

                UniLog.Log("Loading " + dll);

                Type module;
                try
                {
                    var loadedModule = Assembly.LoadFrom(dll);
                    // Get the first class that implements ExtTrackingModule
                    module = loadedModule.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(ExtTrackingModule)));
                }
                catch (ReflectionTypeLoadException e)
                {
                    foreach (var loaderException in e.LoaderExceptions)
                    {
                        UniLog.Error("LoaderException: " + loaderException.Message);
                    }
                    UniLog.Error("Exception loading " + dll + ". Skipping.");
                    continue;
                }
                catch (BadImageFormatException e)
                {
                    UniLog.Error("Encountered a .dll with an invalid format: " + e.Message + ". Skipping...");
                    continue;
                }

                if (module != null)
                {
                    returnList.Add(module);
                    UniLog.Log("Loaded external tracking module: " + module.Name);
                    continue;
                }

                UniLog.Warning("Module " + dll + " does not implement ExtTrackingModule");
            }

            return returnList;
        }

        private static void EnsureModuleThreadStarted(ExtTrackingModule module)
        {
            if (UsefulThreads.ContainsKey(module))
                return;

            var thread = new Thread(module.GetUpdateThreadFunc().Invoke);
            UsefulThreads.Add(module, thread);
            thread.Start();
        }

        private static void FindAndInitRuntimes(bool eye = true, bool lip = true)
        {
            UniLog.Log("Finding and initializing runtimes...");

            // Get a list of our own built-in modules
            var trackingModules = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsSubclassOf(typeof(ExtTrackingModule)));

            // Concat both our own modules and the external ones
            trackingModules = trackingModules.Union(LoadExternalModules());

            foreach (var module in trackingModules)
            {
                UniLog.Log("Initializing module: " + module.Name);
                // Create module
                var moduleObj = (ExtTrackingModule)Activator.CreateInstance(module);

                // If there is still a need for a module with eye or lip tracking and this module supports the current need, try initialize it
                if (EyeStatus == ModuleState.Uninitialized && moduleObj.Supported.SupportsEye ||
                    LipStatus == ModuleState.Uninitialized && moduleObj.Supported.SupportsLip)
                {
                    bool eyeSuccess, lipSuccess;
                    try
                    {
                        (eyeSuccess, lipSuccess) = moduleObj.Initialize(eye, lip);
                    }
                    catch (MissingMethodException)
                    {
                        UniLog.Error(moduleObj.GetType().Name + " does not properly implement ExtTrackingModule. Skipping.");
                        continue;
                    }
                    catch (Exception e)
                    {
                        UniLog.Error("Exception initializing " + moduleObj.GetType().Name + ". Skipping.");
                        UniLog.Error(e.Message);
                        continue;
                    }

                    // If eyeSuccess or lipSuccess was true, set the status to active
                    if (eyeSuccess && _eyeModule == null)
                    {
                        _eyeModule = moduleObj;
                        EyeStatus = ModuleState.Active;
                        EnsureModuleThreadStarted(moduleObj);
                    }

                    if (lipSuccess && _lipModule == null)
                    {
                        _lipModule = moduleObj;
                        LipStatus = ModuleState.Active;
                        EnsureModuleThreadStarted(moduleObj);
                    }
                }

                if (EyeStatus > ModuleState.Uninitialized && LipStatus > ModuleState.Uninitialized)
                    break;    // Keep enumerating over all modules until we find ones we can use
            }

            if (eye)
            {
                if (EyeStatus != ModuleState.Uninitialized) UniLog.Log("Eye Tracking Initialized via " + _eyeModule);
                else UniLog.Warning("Eye Tracking will be unavailable for this session.");
            }

            if (lip)
            {
                if (LipStatus != ModuleState.Uninitialized) UniLog.Log("Lip Tracking Initialized via " + _lipModule);
                else UniLog.Warning("Lip Tracking will be unavailable for this session.");
            }
        }

        // Signal all active modules to gracefully shut down their respective runtimes
        public static void TeardownAllAndReset()
        {
            foreach (var module in UsefulThreads)
            {
                UniLog.Log("Teardown: " + module.Key.GetType().Name);
                module.Key.Teardown();
                module.Value.Abort();
                UniLog.Log("Teardown complete: " + module.Key.GetType().Name);
            }
            UsefulThreads.Clear();

            EyeStatus = ModuleState.Uninitialized;
            LipStatus = ModuleState.Uninitialized;

            _eyeModule = null;
            _lipModule = null;
        }

        /* Removes the 'downloaded from the internet' attribute from a module
        * @param DLL file path
        * @return error; if true then the module should be skipped
        */
        private static bool RemoveZoneIdentifier(string path)
        {
            string zoneFile = path + ":Zone.Identifier";

            if (Utils.GetFileAttributes(zoneFile) == 0xffffffff) // INVALID_FILE_ATTRIBUTES
                //zone file doesn't exist, everything's good
                return false;

            if (Utils.DeleteFile(zoneFile))
                UniLog.Log("Removing the downloaded file identifier from " + path);
            else
            {
                UniLog.Error("Couldn't removed the 'file downloaded' mark from the " + path + " module! Please unblock the file manually");
                return true;
            }

            return false;
        }
    }
}

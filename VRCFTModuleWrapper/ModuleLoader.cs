using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System.Threading;
using VRCFTModuleWrapper;
using VRCFTModuleWrapper.VRCFT;

namespace ModuleLoader
{
    public class ModuleLoader : NeosMod
    {
        public override string Name => " VRCFTModuleWrapper";
        public override string Author => "dfgHiatus";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/dfgHiatus/VRCFT-Module-Wrapper";

        internal static ModConfiguration config;
        internal static readonly CancellationTokenSource MasterCancellationTokenSource = new CancellationTokenSource();

        # region Config Keys
        [AutoRegisterConfigKey]
        public static ModConfigurationKey<int> inPort = new ModConfigurationKey<int>("inPort", "In port", () => 9000);

        [AutoRegisterConfigKey]
        public static ModConfigurationKey<int> outPort = new ModConfigurationKey<int>("outPort", "Out port", () => 9001);

        [AutoRegisterConfigKey]
        public static ModConfigurationKey<string> ip = new ModConfigurationKey<string>("ip", "VRCFT IP", () => "127.0.0.1");

        [AutoRegisterConfigKey]
        public static ModConfigurationKey<bool> enableEye = new ModConfigurationKey<bool>("enableEye", "Blink Speed", () => true);

        [AutoRegisterConfigKey]
        public static ModConfigurationKey<bool> enableLip = new ModConfigurationKey<bool>("enableLip", "Blink Speed", () => true);
        #endregion

        public override void OnEngineInit()
        {
            config = GetConfiguration();

            // Load dependencies
            DependencyManager.Load();

            // Initialize Tracking Runtimes
            UnifiedLibManager.Initialize(
                config.GetValue(enableEye),
                config.GetValue(enableLip));

            if (UnifiedLibManager.EyeStatus == ModuleState.Uninitialized ||
                UnifiedLibManager.LipStatus == ModuleState.Uninitialized)
            {
                Msg("Failed to initialize VRCFT modules. Please check your configuration.");
                return;
            }

            if (UnifiedLibManager.EyeStatus != ModuleState.Uninitialized)
            {
                Engine.Current.InputInterface.RegisterInputDriver(new EyeDevice());
            }
            
            if (UnifiedLibManager.LipStatus != ModuleState.Uninitialized)
            {
                Engine.Current.InputInterface.RegisterInputDriver(new MouthDevice());
            }

            new Thread(new ThreadStart(PollModules));

            Engine.Current.OnShutdown += Teardown;

            new Harmony("net.dfgHiatus.VRCFTModuleWrapper").PatchAll();
        }

        private static void PollModules()
        {
            while (!MasterCancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(10);

                UnifiedTrackingData.OnUnifiedDataUpdated.Invoke(
                    UnifiedTrackingData.LatestEyeData,
                    UnifiedTrackingData.LatestLipData);
            }
        }

        private static void Teardown()
        {
            // Kill our threads
            MasterCancellationTokenSource.Cancel();

            // Utils.TimeEndPeriod(1);
            UnifiedLibManager.TeardownAllAndReset();
        }
    }
}
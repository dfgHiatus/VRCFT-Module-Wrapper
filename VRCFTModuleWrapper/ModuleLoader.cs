using FrooxEngine;
using NeosModLoader;
using System.Threading;
using VRCFTModuleWrapper;

namespace ModuleLoader
{
    public class ModuleLoader : NeosMod
    {
        public override string Name => " VRCFTModuleWrapper";
        public override string Author => "dfgHiatus";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/dfgHiatus/VRCFT-Module-Wrapper";

        internal static ModConfiguration config;

        # region Config Keys
        [AutoRegisterConfigKey]
        public static ModConfigurationKey<int> inPort = new ModConfigurationKey<int>("inPort", "VRCFT Port (Change requires restart)", () => 9000);
        #endregion

        public override void OnEngineInit()
        {
            config = GetConfiguration();
            new VRCFTOSC(config.GetValue(inPort));

            Engine.Current.OnReady += OnReady;
        }

        private void OnReady()
        {
            Engine.Current.InputInterface.RegisterInputDriver(new EyeDevice());
            Engine.Current.InputInterface.RegisterInputDriver(new MouthDevice());
        }
    }
}
using BepInEx;
using HarmonyLib;
using LaunchPadBooster;

namespace Space_Duct_Tape
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        public const string GUID = "com.monotoast.Space_Duct_Tape";
        public const string NAME = "Space_Duct_Tape";
        public const string VERSION = "1.0.0";

        public static Main Instance;
        private Mod _mod;

        private void Awake()
        {
            Instance = this;
            Logger.LogInfo($"{NAME} v{VERSION} is loading...");

            _mod = new Mod(NAME, VERSION);

            _mod.RegisterNetworkMessage<ConfigSync>();
            ConfigurablePatchAttribute.Configure(Config);
            
            try
            {
                // TODO move this into each patch
                Logger.LogInfo($"{NAME} patches applied successfully.");
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Error patching: {ex}");
            }

            Logger.LogInfo($"{NAME} v{VERSION} has loaded.");
        }

        private void OnDestroy()
        {
            try
            {
                // TODO move this into each patch
                Logger.LogInfo($"{NAME} patches have been removed.");
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Error unpatching: {ex}");
            }
        }
    }
}

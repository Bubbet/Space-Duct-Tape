using System;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;

namespace Space_Duct_Tape
{
    [ConfigurablePatch, HarmonyPatch]
    public static class PatchTemplateExample
    {
        private static Harmony _harmony;
        private static PatchClassProcessor _patchClassProcessor;

        [UsedImplicitly]
        public static void Configure(ConfigFile configFile)
        {
            _harmony = new Harmony(Main.GUID + "." + nameof(PatchTemplateExample));
            _patchClassProcessor = new PatchClassProcessor(_harmony, typeof(PatchTemplateExample));
            var enabled = configFile.Bind("Patches", nameof(PatchTemplateExample), true, "Example patch");
            enabled.SettingChanged += OnEnabledOnSettingChanged;
            OnEnabledOnSettingChanged(null, new SettingChangedEventArgs(enabled));
        }
        
        private static void OnEnabledOnSettingChanged(object _, EventArgs args)
        {
            if (args is not SettingChangedEventArgs { ChangedSetting: ConfigEntry<bool> entry }) return;
            if (entry.Value)
            {
                // Can't use harmony.PatchAll() because it would patch everything in the dll.
                _patchClassProcessor.Patch();
            }
            else
            {
                _harmony.UnpatchSelf();
            }
        }
    }
}
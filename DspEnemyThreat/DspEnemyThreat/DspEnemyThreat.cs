using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace DspEnemyThreat
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class DspEnemyThreat : BaseUnityPlugin
    {
        private const string PluginGuid = "DspEnemyThreat_configuration";
        private const string PluginName = "DspEnemyThreat";
        private const string PluginVersion = "1.0";


        public static bool IsActive;

        private void Awake()
        {
            Logger.LogWarning($"{PluginName} is loaded!");
            Logger.LogWarning($"{PluginName} Enemy state: {IsActive}");

            var harmony = new Harmony(PluginName);
            harmony.PatchAll();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F7))
            {
                IsActive = !IsActive;
                Logger.LogWarning($"{PluginName} Enemy state changed: {IsActive}");
            }
        }
    }


    [HarmonyPatch(typeof(DFGBaseComponent), nameof(DFGBaseComponent.UpdateFactoryThreat))]
    public static class ThreatOverride1
    {
        public static bool Prefix()
        {
            return DspEnemyThreat.IsActive;
        }
    }
}
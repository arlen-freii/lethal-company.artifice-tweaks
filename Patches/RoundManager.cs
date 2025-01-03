using HarmonyLib;
using BepInEx.Logging;

namespace ArtificeTweaks.Patches;

[HarmonyPatch(typeof(RoundManager))]
internal class RoundManager_Patches {

    [HarmonyPatch("LoadNewLevel")]
    [HarmonyPriority(800)]
    [HarmonyPrefix]
    private static void LoadNewLevel_Prefix(ref SelectableLevel newLevel) {

        if (newLevel.name == ArtificeTweaks.LevelName) {

            ArtificeTweaks.TimedLog(LogLevel.Info, "Level loading is Artifice, applying scrap count range and factory size multiplier tweaks.");

            newLevel.minScrap = ArtificeTweaks.UpdatedMinScrap;
            newLevel.maxScrap = ArtificeTweaks.UpdatedMaxScrap;

            newLevel.factorySizeMultiplier = ArtificeTweaks.UpdatedFactorySizeMultiplier;

            ArtificeTweaks.TimedLog(LogLevel.Info, "Successfully applied.");
        }

    }

    [HarmonyPatch("GenerateNewFloor")]
    [HarmonyPriority(200)]
    [HarmonyPrefix]
    private static void GenerateNewFloor_Prefix(RoundManager __instance) {

        if (__instance.currentLevel.name == ArtificeTweaks.LevelName) {

            ArtificeTweaks.TimedLog(LogLevel.Info, "Level loading is Artifice, applying dungeon flow and factory size tweaks.");

            SelectableLevel level = __instance.currentLevel;
            level.dungeonFlowTypes = ArtificeTweaks.UpdatedDungeonFlows;
            level.factorySizeMultiplier = ArtificeTweaks.UpdatedFactorySizeMultiplier;

            ArtificeTweaks.TimedLog(LogLevel.Info, "Successfully applied.");
        }

    }

}
using BepInEx.Logging;
using DunGen;
using HarmonyLib;

namespace ArtificeTweaks.Patches;

[HarmonyPatch(typeof(RuntimeDungeon))]
internal class RuntimeDungeon_Patches {

    [HarmonyPatch("Generate")]
    [HarmonyPriority(300)]
    [HarmonyPrefix]
    private static void Generate_Prefix(RuntimeDungeon __instance) {

        if (RoundManager.Instance.currentLevel.name == ArtificeTweaks.LevelName) {

            ArtificeTweaks.TimedLog(LogLevel.Info, "Generating Artifice interior, applying updated factory size multiplier value.");

            __instance.Generator.LengthMultiplier = RoundManager.Instance.mapSizeMultiplier * RoundManager.Instance.currentLevel.factorySizeMultiplier;
            
            ArtificeTweaks.TimedLog(LogLevel.Info, "Successfully applied.");

        }

    }
}
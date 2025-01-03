using DunGen;
using HarmonyLib;

namespace ArtificeTweaks.Patches;

// TODO: Is priority essential or just a way to avoid incompatability?
//       Check blame probably on why this was added.

[HarmonyPatch(typeof(RuntimeDungeon))]
internal class RuntimeDungeon_Patches {

    [HarmonyPatch("Generate")]
    [HarmonyPriority(300)]
    [HarmonyPrefix]
    private static void Generate_Prefix(RuntimeDungeon __instance) {

        if (RoundManager.Instance.currentLevel.name == ArtificeTweaks.LevelName) {

            __instance.Generator.LengthMultiplier = RoundManager.Instance.mapSizeMultiplier * RoundManager.Instance.currentLevel.factorySizeMultiplier;

        }

    }
}
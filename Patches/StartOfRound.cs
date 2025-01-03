using System;
using System.Linq;
using BepInEx.Logging;
using HarmonyLib;

namespace ArtificeTweaks.Patches;

[HarmonyPatch(typeof(StartOfRound))]
internal class StartOfRound_Patches {

    [HarmonyPatch("OnShipLandedMiscEvents")]
    [HarmonyPostfix]
    private static void OnShipLandedMiscEvents_Postfix() {

        if (RoundManager.Instance.currentLevel.name == ArtificeTweaks.LevelName) {

            SelectableLevel level = RoundManager.Instance.currentLevel;

            int scrapSpawned = UnityEngine.Object.FindObjectsOfType<GrabbableObject>().Where(
                (ob) => { return ob.itemProperties.isScrap && !ob.isInShipRoom && !ob.isInElevator; }
            ).Count();

            string interiorSpawned = Enum.GetName(typeof(InteriorIDs), RoundManager.Instance.currentDungeonType);

            ArtificeTweaks.TimedLog(LogLevel.Debug, "Ship landed, tweaked values are:");
            ArtificeTweaks.TimedLog(LogLevel.Debug, $"factorySizeMultiplier: {level.factorySizeMultiplier};");
            ArtificeTweaks.TimedLog(LogLevel.Debug, $"minScrap, maxScrap, scrapSpawned: {level.minScrap}, {level.maxScrap}, {scrapSpawned};");
            ArtificeTweaks.TimedLog(LogLevel.Debug, $"dungeonFlowTypes: {ArtificeTweaks.PPFlowTypes(level.dungeonFlowTypes)};");
            ArtificeTweaks.TimedLog(LogLevel.Debug, $"interiorSpawned: {interiorSpawned};");
            
        }

    }

}
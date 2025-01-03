#pragma warning disable IDE0051

using System;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ArtificeTweaks;

enum InteriorIDs {
    Factory = 0,
    Mansion = 1,
    Mineshaft = 4,
}

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class ArtificeTweaks : BaseUnityPlugin {
    public static ArtificeTweaks Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    public static readonly string LevelName = "ArtificeLevel";
    public static readonly IntWithRarity[] UpdatedDungeonFlows = [
        new() { id = (int)InteriorIDs.Factory,   rarity = 25 },
        new() { id = (int)InteriorIDs.Mansion,   rarity = 50 },
        new() { id = (int)InteriorIDs.Mineshaft, rarity = 25 },
    ];
    public static readonly float UpdatedFactorySizeMultiplier = 2.0f;
    public static readonly int UpdatedMinScrap = 30;
    public static readonly int UpdatedMaxScrap = 38;

    private void Awake() {
        Logger = base.Logger;
        Instance = this;

        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch() {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch() {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }

    public static void TimedLog(LogLevel level, object data) {

        Logger.Log(level, $"{DateTime.UtcNow} | {data}");
        
    }

    public static string PPFlowTypes(IntWithRarity[] flows) {
        return string.Join(", ", flows.Select(
            (flow) => { return $"{Enum.GetName(typeof(InteriorIDs), flow.id)} - {flow.rarity}"; }
        ));
    } 
}

using HarmonyLib;
using UnityEngine;
using Verse;

namespace VisibleErrorLogs
{
    public class VisibleErrorLogsMod : Mod
    {
        public VisibleErrorLogsMod(ModContentPack content) : base(content)
        {
            // since we are dealing with very low-level stuff, we cannot use HugsLib
            // we therefore need to handle the Harmony init here.
            LogInfo("Visible Error Logs, starting up. Hopefully the patches work.");
            Harmony harmony = new Harmony("rimworld." + content.PackageId);
            harmony.PatchAll();
        }

        public static readonly string MODPREFIX = "V1024-VEL";

        public static void LogError(string message)
        {
            Log.Error($"[{MODPREFIX}] {message}");
        }

        public static void LogWarning(string message)
        {
            Log.Warning($"[{MODPREFIX}] {message}");
        }

        public static void LogInfo(string message)
        {
            Log.Message($"[{MODPREFIX}] {message}");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            // use this to create an assertion button in mod settings to test this mod
            base.DoSettingsWindowContents(inRect);
        }
    }
}

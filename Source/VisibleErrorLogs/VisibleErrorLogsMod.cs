using HarmonyLib;
using System;
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
            // also print RimWorld (assembly) version
            // supposedly the log will also print RimWorld version (more accurate), but sometimes I just miss it.
            Version rimworldVersion = typeof(Mod).Assembly.GetName().Version;
            LogInfo($"Detecting your RimWorld assembly version as {rimworldVersion}");
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
            Listing_Standard listing = new Listing_Standard(GameFont.Small);
            listing.Begin(inRect);
            listing.ColumnWidth = 400;

            // button
            if (listing.ButtonText("Trigger a harmless error, to see the debug log"))
            {
                LogError("BOOOOO!!!!! ;D Heh heh~~~ Did I scare you? Wanna try again? ;)");
            }

            // finish
            listing.End();

            // base call
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Visible Error Logs";
        }
    }
}

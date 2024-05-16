using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace VisibleErrorLogs
{
    [HarmonyPatch(typeof(Log))]
    [HarmonyPatch(nameof(Log.Error))]
    internal class Transpiler_Log_Error
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> TranspileMethod(IEnumerable<CodeInstruction> instructions)
        {
            /*
             * Patch the method, so that the conditional that wraps TryOpenLogWindow() is removed
             * 
             * There is only 1 occurence of TryOpenLogWindow(), and also only 1 occurence of the related checking
             */
            return new CodeMatcher(instructions)
                .MatchStartForward(
                    new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(PlayDataLoader), nameof(PlayDataLoader.Loaded)))
                ) // in a cursed approach, find the conditional, and NOP it, thus unwrapping the inner side
                // then set NOP 4 times
                .SetAndAdvance(OpCodes.Nop, null)
                .SetAndAdvance(OpCodes.Nop, null)
                .SetAndAdvance(OpCodes.Nop, null)
                .SetAndAdvance(OpCodes.Nop, null)
                // finish
                .InstructionEnumeration();
        }
    }
}

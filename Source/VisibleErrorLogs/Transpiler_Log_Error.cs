using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace VisibleErrorLogs
{
    [HarmonyPatch(typeof(Log))]
    // target the specific method to handle 1.3 and 1.4 old code having overloaded but deprecated methods
    [HarmonyPatch(nameof(Log.Error), typeof(string))]
    internal class Transpiler_Log_Error
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> TranspileMethod(IEnumerable<CodeInstruction> instructions)
        {
            /*
             * Patch the method, so that the conditional that wraps TryOpenLogWindow() is removed
             * 
             * There is only 1 occurence of TryOpenLogWindow(), and also only 1 occurence of the related checking
             * 
             * This related checking occurs right after a function call, which is also occuring only 1 time in the entire function
             */
            return new CodeMatcher(instructions)
                .MatchStartForward(
                    new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(Log), "PostMessage"))
                ) // in a cursed approach, find the conditional, and NOP it, thus unwrapping the inner side
                .Advance(1)
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

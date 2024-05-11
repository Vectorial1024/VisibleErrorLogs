using Verse;

namespace VisibleErrorLogs
{
    public class VisibleErrorLogsMod : Mod
    {
        public VisibleErrorLogsMod(ModContentPack content) : base(content)
        {
            // since we are dealing with very low-level stuff, we cannot use HugsLib
            // we therefore need to handle the Harmony init here.
        }
    }
}

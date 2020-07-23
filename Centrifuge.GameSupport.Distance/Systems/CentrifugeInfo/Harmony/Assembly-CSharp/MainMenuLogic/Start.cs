using Centrifuge.Distance.Internal;
using Harmony;

namespace Centrifuge.Distance.Systems.CentrifugeInfo.Harmony
{
    [HarmonyPatch(typeof(MainMenuLogic), "Start")]
    internal static class MainMenuLogic__Start
    {
        [HarmonyPostfix]
        internal static void Postfix()
        {
            VersionNumber.Create();
        }
    }
}


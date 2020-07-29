using Centrifuge.Distance.Internal;
using Harmony;

namespace Centrifuge.Distance.Systems.CentrifugeInfo.Harmony
{
    [HarmonyPatch(typeof(SpeedrunTimerLogic), "OnEnable")]
    internal static class SpeedrunTimerLogic__OnEnable
    {
        [HarmonyPostfix]
        internal static void Postfix(SpeedrunTimerLogic __instance)
        {
            VersionNumber.Create(__instance.gameObject);
        }
    }
}
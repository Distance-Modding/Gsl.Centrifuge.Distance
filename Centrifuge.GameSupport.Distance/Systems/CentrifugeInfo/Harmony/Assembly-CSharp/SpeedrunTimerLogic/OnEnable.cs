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
            GameAPI.Instance.logger_.Warning("SpeedrunTimerLogic__OnEnable");
            
            VersionNumber.Create(__instance.gameObject);
        }
    }
}
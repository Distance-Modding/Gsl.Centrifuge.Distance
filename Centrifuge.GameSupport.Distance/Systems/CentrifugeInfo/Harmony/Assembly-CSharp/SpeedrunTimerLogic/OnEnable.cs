using Centrifuge.Distance.Internal;
using HarmonyLib;
using System;

namespace Centrifuge.Distance.Systems.CentrifugeInfo.Harmony
{
    [HarmonyPatch(typeof(SpeedrunTimerLogic), "OnEnable")]
    internal static class SpeedrunTimerLogic__OnEnable
    {
        [HarmonyPostfix]
        internal static void Postfix(SpeedrunTimerLogic __instance)
        {
            try
            {
                VersionNumber.Create(__instance.gameObject);
            }
            catch (Exception e)
            {
                GameAPI.Instance.Logger.Exception(e);
            }
        }
    }
}
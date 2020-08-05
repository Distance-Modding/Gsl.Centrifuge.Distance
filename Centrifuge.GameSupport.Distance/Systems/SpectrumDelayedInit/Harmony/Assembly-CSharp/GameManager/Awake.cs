using HarmonyLib;

namespace Centrifuge.Distance.Systems.SpectrumDelayedInit.Harmony
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal static class GameManager__Awake
    {
        [HarmonyPrefix]
        internal static void Postfix()
        {
            ReactorProxy.InvokeLateInitialize();
        }
    }
}

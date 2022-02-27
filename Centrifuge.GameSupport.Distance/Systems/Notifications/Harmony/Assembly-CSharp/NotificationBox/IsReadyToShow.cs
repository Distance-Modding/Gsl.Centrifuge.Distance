using HarmonyLib;

namespace Centrifuge.Distance.Notifications.Harmony
{
	//[HarmonyPatch(typeof(NotificationBox), "IsReadyToShow")]
	internal static class NotificationBox__IsReadyToShow
	{
		[HarmonyPrefix]
		internal static bool Prefix(out bool __result)
		{
			__result = false;
			return false;
		}
	}
}

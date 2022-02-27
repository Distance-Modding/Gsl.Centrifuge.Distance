using HarmonyLib;

namespace Centrifuge.Distance.Notifications.Harmony
{
	[HarmonyPatch(typeof(NotificationBox), "Display")]
	internal static class NotificationBox__Display
	{
		[HarmonyPrefix]
		internal static bool Prefix()
		{
			return false;
		}
	}
}

using HarmonyLib;

namespace Centrifuge.Distance.Notifications.Harmony
{
	//[HarmonyPatch(typeof(NotificationBox), "Show")]
	internal static class NotificationBox__ShowNextOrHide
	{
		[HarmonyPrefix]
		internal static bool Prefix()
		{
			return false;
		}
	}
}

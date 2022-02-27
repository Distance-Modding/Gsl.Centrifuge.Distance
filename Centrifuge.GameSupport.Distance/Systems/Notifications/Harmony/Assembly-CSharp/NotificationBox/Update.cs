using HarmonyLib;

namespace Centrifuge.Distance.Notifications.Harmony
{
	//[HarmonyPatch(typeof(NotificationBox), "Update")]
	internal static class NotificationBox__Update
	{
		[HarmonyPrefix]
		internal static bool Prefix()
		{
			return false;
		}
	}
}

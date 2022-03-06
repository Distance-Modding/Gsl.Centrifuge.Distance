using Centrifuge.Distance.Notifications.Scripts;
using HarmonyLib;

namespace Centrifuge.Distance.Notifications.Harmony
{
	[HarmonyPatch(typeof(NotificationBox), "Show")]
	internal static class NotificationBox__Show
	{
		[HarmonyPrefix]
		internal static bool Prefix(NotificationBox.Notification n, bool menuOnly = true)
		{
			NotificationPanel.Instance.Show(new VanillaNotification(n), menuOnly);
			return false;
		}
	}
}

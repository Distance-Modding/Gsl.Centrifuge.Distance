using HarmonyLib;
using Centrifuge.Distance.Notifications.Scripts;
using Centrifuge.Distance.Data.Notifications;

namespace Centrifuge.Distance.Notifications.Harmony
{
	//[HarmonyPatch(typeof(NotificationBox), "Show")]
	internal static class NotificationBox__Show
	{
		[HarmonyPrefix]
		internal static bool Prefix(NotificationBox.Notification n, bool menuOnly = true)
		{
			NotificationPanel.Instance.Show(new Notification(n), menuOnly);
			return false;
		}
	}
}

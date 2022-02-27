using Centrifuge.Distance.Data.Notifications;
using Centrifuge.Distance.Notifications.Scripts;
using System.Linq;

namespace Centrifuge.Distance.Game
{
	public static class NotificationsBox
	{
		public const float DEFAULT_DURATION = 6f;
		public const bool DEFAULT_MENU_ONLY = true;

		public static void Show(NotificationBase notification, bool menuOnly = DEFAULT_MENU_ONLY)
		{
			NotificationPanel.Instance.Show(notification, menuOnly);
		}

		public static void Show(string title, string description, NotificationType type, string spriteName, float duration = DEFAULT_DURATION, bool menuOnly = DEFAULT_MENU_ONLY)
		{
			Show(new Notification(title, description, spriteName, type, duration), menuOnly);
		}

		public static void Show(string title, string description, NotificationType type, float duration = DEFAULT_DURATION, bool menuOnly = DEFAULT_MENU_ONLY)
		{
			string spriteName = global::NotificationBox.Notification.unlockSpriteNames_.ElementAtOrDefault((int)type);
			Show(title, description, type, spriteName, duration, menuOnly);
		}

		public static void Show(string title, string description, float duration = DEFAULT_DURATION, bool menuOnly = DEFAULT_MENU_ONLY)
		{
			Show(title, description, NotificationType.Achievement, duration, menuOnly);
		}
	}
}

namespace Centrifuge.Distance.Notifications
{
	internal class VanillaNotification : NotificationBase
	{
		public string Sprite { get; internal set; }
		public NotificationType Type { get; internal set; }

		public VanillaNotification(string title, string description, string sprite, NotificationType type, float duration)
		: base(title, description, duration)
		{
			Sprite = sprite;
			Type = type;
		}

		public VanillaNotification(NotificationBox.Notification notification)
		: this(notification.title, notification.description, notification.spriteName, notification.type.ToGSLEnum(), notification.time)
		{ }

		public override void Prepare(NotificationPrefabData prefab)
		{
			base.Prepare(prefab);
			prefab.Icon.spriteName = Sprite;
		}

		public override void Display(NotificationPrefabData prefab)
		{
			base.Display(prefab);
			AudioManager.PostEvent(Type.GetAudioEventName());
			foreach (var sprite in prefab.Icon.atlas.GetListOfSprites())
			{
				GameAPI.Instance.Logger.Warning($"Sprite: {sprite}");
			}
		}

		public static implicit operator NotificationBox.Notification(VanillaNotification n)
		{
			return new NotificationBox.Notification
			(
				n.Title,
				n.Description,
				n.Type.ToDistanceEnum(),
				n.Sprite,
				n.Duration
			);
		}
	}
}

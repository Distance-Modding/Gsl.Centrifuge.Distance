namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationSound : NotificationModifier
	{
		public string EventName { get; protected set; }

		public NotificationSound(string eventName)
		{
			EventName = eventName;
		}

		public NotificationSound(NotificationType type)
		: this(type.GetAudioEventName()) { }

		public NotificationSound(NotificationBox.NotificationType type)
		: this(type.ToGSLEnum()) { }

		public override void Display(NotificationPrefabData data)
		{
			base.Display(data);
			AudioManager.PostEvent(EventName);
		}
	}
}

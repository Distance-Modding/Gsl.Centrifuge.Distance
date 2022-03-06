namespace Centrifuge.Distance.Notifications.Modifiers
{
	public abstract class NotificationModifier
	{
		public virtual void Prepare(NotificationPrefabData data) { }

		public virtual void Display(NotificationPrefabData data) { }

		public virtual void Reset(NotificationPrefabData data) { }
	}
}

using Centrifuge.Distance.Notifications.Scripts;

namespace Centrifuge.Distance.Data.Notifications
{
    public class Notification : NotificationBase
    {
        public string Sprite { get; internal set; }
        public NotificationType Type { get; internal set; }

        public Notification(string title, string description, string sprite, NotificationType type, float duration)
        : base(title, description, duration)
        {
            Sprite = sprite;
            Type = type;
        }

        public Notification(NotificationBox.Notification notification)
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
        }

        public static implicit operator NotificationBox.Notification(Notification n)
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

using Centrifuge.Distance.Notifications.Scripts;

namespace Centrifuge.Distance.Data.Notifications
{
    public abstract class NotificationBase
    {
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public float Duration { get; internal set; }

        protected NotificationBase(string title, string description, float duration)
        {
            Title = title;
            Description = description;
            Duration = duration;
        }

        public virtual void Prepare(NotificationPrefabData prefab)
        {
            prefab.Title.text = Title.ToUpper();
            prefab.Description.text = Description;
        }

        public virtual void Display(NotificationPrefabData prefab) { }

        public virtual void Reset(NotificationPrefabData prefab) { }
    }
}

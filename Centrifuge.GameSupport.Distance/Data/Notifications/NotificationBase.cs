﻿#pragma warning disable IDE0060, RCS1163
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

        public void Prepare(NotificationPrefabData prefab)
        {
            prefab.Title.text = $"#{Title.ToUpper()}";
            prefab.Description.text = Description;
        }

        public void Display(NotificationPrefabData prefab) { }

        public void Reset(NotificationPrefabData prefab) { }
    }
}

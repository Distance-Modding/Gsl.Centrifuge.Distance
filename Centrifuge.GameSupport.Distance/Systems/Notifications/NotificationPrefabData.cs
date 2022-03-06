using UnityEngine;

namespace Centrifuge.Distance.Notifications
{
    public class NotificationPrefabData
    {
        public GameObject PrefabObject { get; set; }
        public UIPanel Panel { get; set; }
        public UILabel Title { get; set; }
        public UILabel Description { get; set; }
        public UISprite Icon { get; set; }
    }
}
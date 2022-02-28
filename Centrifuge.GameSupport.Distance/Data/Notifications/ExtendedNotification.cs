using Centrifuge.Distance.Notifications.Scripts;
using UnityEngine;

namespace Centrifuge.Distance.Data.Notifications
{
	public class ExtendedNotification : NotificationBase
	{
		#region Notification Properties
		public Texture2D Icon { get; protected set; } = null;
		public string AudioEvent { get; protected set; }
		#endregion

		#region Cached Restored Fields
		// Fields used to store the state of the notification box before modifications in Prepare
		// These values are used in Reset to leave the gameObject clean for later notifications
		private UIAtlas defaultAtlas, currentAtlas;
		#endregion

		public ExtendedNotification(string title, string description, float duration)
		: base(title, description, duration)
		{ }

		public ExtendedNotification WithIcon(Texture2D icon)
		{
			Icon = icon;
			return this;
		}

		public ExtendedNotification WithAudio(string eventName)
		{
			AudioEvent = eventName;
			return this;
		}

		public override void Prepare(NotificationPrefabData prefab)
		{
			base.Prepare(prefab);
			defaultAtlas = prefab.Icon.mAtlas;

			if (Icon)
			{
				currentAtlas = prefab.PrefabObject.AddComponent<UIAtlas>();
				currentAtlas.spriteMaterial = Object.Instantiate(defaultAtlas.spriteMaterial);
				currentAtlas.spriteMaterial.mainTexture = Icon;

				UISpriteData spriteData = new UISpriteData() { name = "icon" };
				spriteData.SetRect(0, 0, Icon.width, Icon.height);
				spriteData.SetPadding(0, 0, 0, 0);
				spriteData.SetBorder(0, 0, 0, 0);

				currentAtlas.spriteList.Add(spriteData);

				prefab.Icon.mAtlas = currentAtlas;
				prefab.Icon.spriteName = "icon";
				//prefab.Icon.SetAtlasSprite(spriteData);
			} else
			{
				prefab.Icon.spriteName = "sprite not set!";
			}
		}

		public override void Display(NotificationPrefabData prefab)
		{
			base.Display(prefab);
			if (!string.IsNullOrEmpty(AudioEvent))
			{
				AudioManager.PostEvent(AudioEvent);
			}
		}

		public override void Reset(NotificationPrefabData prefab)
		{
			base.Reset(prefab);
			prefab.Icon.mAtlas = defaultAtlas;

			if (currentAtlas)
			{
				Object.DestroyImmediate(currentAtlas);
			}
		}
	}
}

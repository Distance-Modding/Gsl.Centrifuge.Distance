using UnityEngine;

namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationTexture : NotificationModifier
	{
		public Texture Texture { get; protected set; }

		// Fields used to store the state of the notification box before modifications in Prepare
		// These values are used in Reset to leave the gameObject clean for later notifications
		private UIAtlas defaultAtlas, currentAtlas;
		private string oldSpriteName;

		public NotificationTexture(Texture texture)
			=> Texture = texture;

		public NotificationTexture(Texture2D texture)
			=> Texture = texture;

		public override void Prepare(NotificationPrefabData data)
		{
			base.Prepare(data);
			defaultAtlas = data.Icon.mAtlas;
			oldSpriteName = data.Icon.spriteName;

			if (Texture)
			{
				currentAtlas = data.PrefabObject.AddComponent<UIAtlas>();
				currentAtlas.spriteMaterial = Object.Instantiate(defaultAtlas.spriteMaterial);
				currentAtlas.spriteMaterial.mainTexture = Texture;

				UISpriteData spriteData = new UISpriteData() { name = "icon" };
				spriteData.SetRect(0, 0, Texture.width, Texture.height);
				spriteData.SetPadding(0, 0, 0, 0);
				spriteData.SetBorder(0, 0, 0, 0);

				currentAtlas.spriteList.Add(spriteData);

				data.Icon.mAtlas = currentAtlas;
				data.Icon.spriteName = "icon";
			}
			else
			{
				data.Icon.spriteName = "sprite not set!";
			}
		}

		public override void Reset(NotificationPrefabData data)
		{
			base.Reset(data);
			data.Icon.mAtlas = defaultAtlas;
			data.Icon.spriteName = oldSpriteName;

			if (currentAtlas)
			{
				Object.DestroyImmediate(currentAtlas);
			}
		}
	}
}

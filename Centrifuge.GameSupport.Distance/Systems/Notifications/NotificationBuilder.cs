using System.Collections.Generic;
using System.Linq;
using Centrifuge.Distance.Game;
using Centrifuge.Distance.Notifications.Modifiers;
using UnityEngine;

namespace Centrifuge.Distance.Notifications
{
	public class NotificationBuilder : NotificationBase
	{
		protected Stack<NotificationModifier> modifiers;

		public NotificationBuilder(string title, string description, float duration)
		: base(title, description, duration)
		{
			modifiers = new Stack<NotificationModifier>();
		}

		public void Show(bool menuOnly = true) => NotificationsBox.Show(this, menuOnly);

		#region Overriden Methods
		public override void Prepare(NotificationPrefabData prefab)
		{
			base.Prepare(prefab);
			foreach (NotificationModifier modifier in modifiers)
			{
				modifier.Prepare(prefab);
			}
		}

		public override void Display(NotificationPrefabData prefab)
		{
			base.Display(prefab);
			foreach (NotificationModifier modifier in modifiers)
			{
				modifier.Display(prefab);
			}
		}

		public override void Reset(NotificationPrefabData prefab)
		{
			foreach (NotificationModifier modifier in modifiers.Reverse())
			{
				modifier.Reset(prefab);
			}
			base.Reset(prefab);
		}
		#endregion

		#region Modifiers
		public NotificationBuilder AddModifier(NotificationModifier modifier)
		{
			modifiers.Push(modifier);
			return this;
		}

		#region NotificationSprite
		public NotificationBuilder WithIcon(string spriteName)
			=> AddModifier(new NotificationSprite(spriteName));

		public NotificationBuilder WithIcon(SpriteIcon sprite)
			=> AddModifier(new NotificationSprite(sprite));
		#endregion

		#region NotificationTexture
		public NotificationBuilder WithTexture(Texture texture)
			=> AddModifier(new NotificationTexture(texture));

		public NotificationBuilder WithTexture(Texture2D texture)
			=> AddModifier(new NotificationTexture(texture));
		#endregion

		#region NotificationSound
		public NotificationBuilder WithSound(string eventName)
			=> AddModifier(new NotificationSound(eventName));

		public NotificationBuilder WithSound(NotificationType notificationType)
			=> AddModifier(new NotificationSound(notificationType));

		public NotificationBuilder WithSound(NotificationBox.NotificationType notificationType)
			=> AddModifier(new NotificationSound(notificationType));
		#endregion

		#region Color Customization
		#region NotificationTitleColor
		public NotificationBuilder WithTitleColor(Color color)
			=> AddModifier(new NotificationTitleColor(color));
		#endregion

		#region NotificationDotColor
		public NotificationBuilder WithDotColor(Color color)
			=> AddModifier(new NotificationDotColor(color));
		#endregion

		public NotificationBuilder WithForegroundColor(Color color)
			=> WithTitleColor(color).WithDotColor(color);

		#region NotificationBackgroundColor
		public NotificationBuilder WithBackgroundColor(Color color)
			=> AddModifier(new NotificationBackgroundColor(color));
		#endregion

		#region NotificationBackgroundColor
		public NotificationBuilder WithDescriptionColor(Color color)
			=> AddModifier(new NotificationDescriptionColor(color));
		#endregion
		#endregion
		#endregion
	}
}

using UnityEngine;

namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationTitleColor : NotificationModifier
	{
		public const float DEFAULT_OPACITY = 1.0f;

		public Color Color { get; protected set; }

		private Color defaultColor;

		public NotificationTitleColor(Color color)
		{
			Color = color;
		}

		public override void Prepare(NotificationPrefabData data)
		{
			base.Prepare(data);
			defaultColor = data.Title.mColor;
			data.Title.color = Color;
		}

		public override void Reset(NotificationPrefabData data)
		{
			base.Reset(data);
			data.Title.color = defaultColor.WithOpacity(DEFAULT_OPACITY);
		}
	}
}

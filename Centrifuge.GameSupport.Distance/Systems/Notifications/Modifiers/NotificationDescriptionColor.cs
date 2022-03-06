using UnityEngine;

namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationDescriptionColor : NotificationModifier
	{
		public const float DEFAULT_OPACITY = 1.0f;

		public Color Color { get; protected set; }

		private Color defaultColor;

		public NotificationDescriptionColor(Color color)
		{
			Color = color;
		}

		public override void Prepare(NotificationPrefabData data)
		{
			base.Prepare(data);
			defaultColor = data.Description.mColor;
			data.Description.color = Color;
		}

		public override void Reset(NotificationPrefabData data)
		{
			base.Reset(data);
			data.Description.color = defaultColor.WithOpacity(DEFAULT_OPACITY);
		}
	}
}

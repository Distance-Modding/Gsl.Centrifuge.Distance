using UnityEngine;

namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationDotColor : NotificationModifier
	{
		public const float DEFAULT_OPACITY = 1.0f;

		public Color Color { get; protected set; }
		protected UISprite UnplayedCircle { get; set; }

		private Color defaultColor;

		public NotificationDotColor(Color color)
		{
			Color = color;
		}

		public override void Prepare(NotificationPrefabData data)
		{
			base.Prepare(data);
			UnplayedCircle = data.Title.transform.parent.Find("Unplayed Circle").GetComponent<UISprite>();
			defaultColor = UnplayedCircle.mColor;
			UnplayedCircle.color = Color;
		}

		public override void Reset(NotificationPrefabData data)
		{
			base.Reset(data);
			UnplayedCircle.color = defaultColor.WithOpacity(DEFAULT_OPACITY);
		}
	}
}

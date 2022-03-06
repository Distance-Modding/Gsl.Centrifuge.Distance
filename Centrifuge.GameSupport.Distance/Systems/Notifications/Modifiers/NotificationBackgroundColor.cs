using UnityEngine;

namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationBackgroundColor : NotificationModifier
	{
		public const float DEFAULT_BACKGROUND_OPACITY = 1.0f;
		public const float DEFAULT_BACKGROUNDFAR_OPACITY = 0.501f;

		public Color Color { get; protected set; }
		protected UISprite Background { get; set; }
		protected UISprite BackgroundFar { get; set; }

		private Color defaultBackgroundColor, defaultBackgroundFarColor;

		public NotificationBackgroundColor(Color color)
		{
			Color = color;
		}

		public override void Prepare(NotificationPrefabData data)
		{
			base.Prepare(data);
			Background = data.Title.transform.parent.Find("Background").GetComponent<UISprite>();
			BackgroundFar = Background.transform.Find("BackgroundFar").GetComponent<UISprite>();

			defaultBackgroundColor = Background.mColor;
			defaultBackgroundFarColor = BackgroundFar.mColor;

			Background.color = Color.WithOpacity(defaultBackgroundColor.a);
			BackgroundFar.color = Color.WithOpacity(defaultBackgroundFarColor.a);
		}

		public override void Reset(NotificationPrefabData data)
		{
			base.Reset(data);
			Background.color = defaultBackgroundColor.WithOpacity(DEFAULT_BACKGROUND_OPACITY);
			BackgroundFar.color = defaultBackgroundFarColor.WithOpacity(DEFAULT_BACKGROUNDFAR_OPACITY);
		}
	}
}

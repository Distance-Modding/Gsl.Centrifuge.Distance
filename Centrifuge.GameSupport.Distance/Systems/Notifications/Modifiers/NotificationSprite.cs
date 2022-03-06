namespace Centrifuge.Distance.Notifications.Modifiers
{
	public class NotificationSprite : NotificationModifier
	{
		public string SpriteName { get; protected set; }

		public NotificationSprite(string spriteName)
		{
			SpriteName = spriteName;
		}

		public NotificationSprite(SpriteIcon icon)
		: this(icon.ToString()) { }

		public override void Prepare(NotificationPrefabData data)
		{
			base.Prepare(data);
			data.Icon.spriteName = SpriteName;
		}
	}
}

using BaseEnum = global::NotificationBox.NotificationType;

namespace Centrifuge.Distance.Data.Notifications
{
    public enum NotificationType
    {
        Campaign = BaseEnum.Campaign,
        Levels = BaseEnum.Levels,
        Cheats = BaseEnum.Cheats,
        Car = BaseEnum.Car,
        Achievement = BaseEnum.Achievement
    }

    public static class NotificationTypeMethods
    {
        public static BaseEnum ToDistanceEnum(this NotificationType value)
        {
            return (BaseEnum)value;
        }

        public static NotificationType ToGSLEnum(this BaseEnum value)
        {
            return (NotificationType)value;
        }

        public static string GetAudioEventName(this NotificationType value)
        {
            switch (value)
            {
                case NotificationType.Campaign:
                    return "Play_DiscoverableArea";
                case NotificationType.Car:
                    return "Play_UnlockedCar";
                default:
                    return "Play_MedalScreenUnlocked";
            }
        }
    }
}

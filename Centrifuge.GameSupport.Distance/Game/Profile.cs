namespace Centrifuge.Distance.Game
{
    public static class Profile
    {
        public static ProfileManager Manager => G.Sys.ProfileManager_;

        public static global::Profile Current => Manager?.CurrentProfile_;

        public static ProfileProgress Progress => Current?.Progress_;

        public static ProfileStats Stats  => Progress?.Stats_;

        public static string Name => Current ? Current.DisplayName_ : "";

        public static int TotalMedals => Progress ? (int)Progress.TotalMedalCount_ : 0;

        public static class Campaigns
        {
            public static bool Adventure => Progress && Progress.HasCompletedAdventureMode_;
            public static bool LostToEchoes => Progress && Progress.HasCompletedLostToEchoes_;
            public static bool Nexus => Progress && Progress.HasCompletedNexus_;
        }
    }
}

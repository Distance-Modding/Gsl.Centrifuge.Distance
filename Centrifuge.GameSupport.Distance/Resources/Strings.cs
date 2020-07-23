namespace Centrifuge.Distance
{
    public static class InternalResources
    {
        public static class Strings
        {
            public static class MenuSystem
            {
                public static string MenuActionPrevious => "PREVIOUS";

                public static string MenuActionNext => "NEXT";

                public static string MenuPageDescription => "Page {0} of {1}";

                public static string RootMenuName => "Centrifuge";

                public static string RootMenuFullName => "Configure Centrifuge Mods";

                public static string UnavailableMenuError => "This menu is currently unavailable.\nNo menu entries found.";

                public static string UnavailableMenuErrorTitle => "CENTRIFUGE SETTINGS MANAGER";
            }

            public static class VersionInfo
            {
                public static string CentrifugeVersion => "CENTRIFUGE {0}";

                public static string CentrifugeMods => "{0} MOD(S) LOADED";

                public static string CentrifugeGsls => "{0} GSL(S) LOADED";

                public static string Info => "{0} - {1} - {2}";
            }

            public static class Settings
            {
                public static class Gsl
                {
                    public static string MenuTitle => "Game Support Library Settings";

                    public static string MenuDescription => "Settings related to the common modding library(GSL).";

                    public static string ShowVersionInfo => "SHOW VERSION INFO";

                    public static string ShowVersionInfoDescription => "Display the centrifuge version in the main menu.";
                }
            }
        }
    }
}

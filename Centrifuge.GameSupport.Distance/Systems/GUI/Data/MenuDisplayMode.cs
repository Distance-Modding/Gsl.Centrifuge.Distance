using System;
using DisplayMode = global::Spectrum.API.GUI.Data.MenuDisplayMode;

namespace Centrifuge.Distance.GUI.Data
{
    [Flags]
    public enum MenuDisplayMode
    {
        None = DisplayMode.None,
        MainMenu = DisplayMode.MainMenu,
        PauseMenu = DisplayMode.PauseMenu,
        Both = DisplayMode.Both
    }

    public static class MenuDisplayModeEx
    {
        public static MenuDisplayMode Mode(this DisplayMode mode)
        {
            return (MenuDisplayMode)mode;
        }

        public static DisplayMode Mode(this MenuDisplayMode mode)
        {
            return (DisplayMode)mode;
        }
    }
}

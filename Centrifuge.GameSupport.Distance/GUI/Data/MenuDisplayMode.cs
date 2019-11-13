using System;

namespace Centrifuge.Distance.GUI.Data
{
    [Flags]
    public enum MenuDisplayMode
    {
        None,
        MainMenu,
        PauseMenu,
        Both = MainMenu | PauseMenu
    }
}

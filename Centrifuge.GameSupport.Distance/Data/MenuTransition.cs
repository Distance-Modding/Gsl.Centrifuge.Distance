using System;

namespace Centrifuge.Distance.Data
{
    [Flags]
    public enum MenuTransition : uint
    {
        OpenMenu,
        CloseMenu,
        DoNothing
    }
}

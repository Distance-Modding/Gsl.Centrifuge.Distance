using System;

namespace Centrifuge.Distance.Data
{
    [Flags]
    public enum Transition : uint
    {
        In,
        Out,
        None
    }
}

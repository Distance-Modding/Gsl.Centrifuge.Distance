using System;

namespace Centrifuge.Distance.Data
{
    [Flags]
    public enum MessageResult
    {
        None,
        Ok,
        Cancel,
        Yes,
        No
    }
}

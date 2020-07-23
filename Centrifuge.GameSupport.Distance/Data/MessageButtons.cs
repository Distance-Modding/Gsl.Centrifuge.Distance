using System;

namespace Centrifuge.Distance.Data
{
    [Flags]
    public enum MessageButtons
    {
        Ok = MessagePanelLogic.ButtonType.Ok,
        OkCancel = MessagePanelLogic.ButtonType.OkCancel,
        YesNo = MessagePanelLogic.ButtonType.YesNo
    }
}

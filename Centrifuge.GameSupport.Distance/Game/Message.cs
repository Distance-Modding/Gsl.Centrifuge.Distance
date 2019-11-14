namespace Centrifuge.Distance.Game
{
    public static class Message
    {
        public static bool Show(MessagePanelLogic.ButtonType type, string message, string title = "", float time = 0.0f, bool selectConfirm = false)
        {
            bool result = false;
            bool returned = false;

            G.Sys.MenuPanelManager_.ShowMessage(message, title, () =>
            {
                result = true;
                returned = true;
            }, () =>
            {
                result = false;
                returned = true;
            }, type, selectConfirm, UIWidget.Pivot.Center, time);

            while (!returned);

            return result;
        }
        public static bool ShowOk(string message, string title = "", float time = 0.0f) => Show(MessagePanelLogic.ButtonType.Ok, message, title, time);
        public static bool ShowOkCancel(string message, string title = "", float time = 0.0f) => Show(MessagePanelLogic.ButtonType.OkCancel, message, title, time);
        public static bool ShowYesNo(string message, string title = "", float time = 0.0f) => Show(MessagePanelLogic.ButtonType.YesNo, message, title, time);
    }
}

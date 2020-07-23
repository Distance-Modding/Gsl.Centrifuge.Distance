using Centrifuge.Distance.Data;
using System;

namespace Centrifuge.Distance.Game
{
    public class MessageBox
    {
        private readonly string Message = "";
        private readonly string Title = "";
        private float Time = 0.0f;
        private MessagePanelLogic.ButtonType Buttons = MessagePanelLogic.ButtonType.Ok;

        private Action Confirm;
        private Action Cancel;

        private MessageBox(string message, string title)
        {
            Message = message;
            Title = title;
            Confirm = EmptyAction;
            Cancel = EmptyAction;
        }

        public static MessageBox Create(string content, string title = "")
        {
            return new MessageBox(content, title);
        }

        public MessageBox SetButtons(MessageButtons buttons)
        {
            Buttons = (MessagePanelLogic.ButtonType)buttons;
            return this;
        }

        public MessageBox SetTimeout(float delay)
        {
            Time = delay;
            return this;
        }

        public MessageBox OnConfirm(Action action)
        {
            Confirm = action;
            return this;
        }
        public MessageBox OnCancel(Action action)
        {
            Cancel = action;
            return this;
        }

        public void Show()
        {
            G.Sys.MenuPanelManager_.ShowMessage(Message, Title, () => { Confirm(); }, () => { Cancel(); }, Buttons, false, UIWidget.Pivot.Center, Time); ;
        }

        private void EmptyAction() { }
    }
}

using Centrifuge.Distance.GUI.Data;

namespace Centrifuge.Distance.GUI.Controls
{
    public class PasswordPrompt : InputPrompt
    {
        public PasswordPrompt(MenuDisplayMode mode, string id, string name)
            : base(mode, id, name) { }

        protected override void OnTweak()
        {
            InputPromptPanel.CreatePassword(
                new InputPromptPanel.OnSubmit(OnSubmit),
                new InputPromptPanel.OnPop(OnCancel),
                Title,
                DefaultValue()
            );
        }
    }
}
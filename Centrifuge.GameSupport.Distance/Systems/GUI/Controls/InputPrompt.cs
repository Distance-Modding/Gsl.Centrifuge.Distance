﻿using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System;

namespace Centrifuge.Distance.GUI.Controls
{
    public class InputPrompt : MenuItemBase
    {
        public string Title { get; set; }
        public Func<string> DefaultValue { get; set; }
        public Func<string, string> Validator { get; set; }
        public Action<string> SubmitAction { get; set; }
        public Action<InputPrompt> CloseAction { get; set; }

        public InputPrompt(MenuDisplayMode mode, string id, string name)
            : base(mode, id, name) { }

        public InputPrompt WithTitle(string title)
        {
            Title = title ?? string.Empty;
            return this;
        }

        public InputPrompt WithDefaultValue(string defaultValue)
        {
            DefaultValue = delegate () { return defaultValue; };
            return this;
        }
        
        public InputPrompt WithDefaultValue(Func<string> defaultValue)
        {
            DefaultValue = defaultValue ?? delegate() { return string.Empty; };
            return this;
        }

        public InputPrompt WithSubmitAction(Action<string> submitAction)
        {
            SubmitAction = submitAction ?? throw new ArgumentNullException("Submit action cannot be null.");
            return this;
        }

        public InputPrompt WithCloseAction(Action<InputPrompt> closeAction)
        {
            CloseAction = closeAction;
            return this;
        }

        public InputPrompt ValidatedBy(Func<string, string> validator)
        {
            Validator = validator;
            return this;
        }

        protected virtual bool OnSubmit(out string error, string input)
        {
            error = Validator?.Invoke(input);

            if (error == null)
            {
                SubmitAction?.Invoke(input);
                return true;
            }
            return false;
        }

        protected virtual void OnCancel()
        {
            CloseAction?.Invoke(this);
        }

        protected virtual void OnTweak()
        {
            InputPromptPanel.Create(
                new InputPromptPanel.OnSubmit(OnSubmit),
                new InputPromptPanel.OnPop(OnCancel),
                Title,
                DefaultValue()
            );
        }

        public override void Tweak(CentrifugeMenu menu)
        {
            menu.TweakAction(Name, () =>
            {
                OnTweak();
                base.Tweak(menu);
            }, Description);
        }
    }
}
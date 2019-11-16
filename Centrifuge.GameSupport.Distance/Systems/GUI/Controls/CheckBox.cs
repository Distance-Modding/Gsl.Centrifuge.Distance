﻿using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System;

namespace Centrifuge.Distance.GUI.Controls
{
    public class CheckBox : MenuItemBase
    {
        public Func<bool> Get { get; set; }
        public Action<bool> Set { get; set; }
        public bool DefaultValue { get; private set; }

        public CheckBox(MenuDisplayMode mode, string id, string name)
            : base(mode, id, name)
        {
            Get = () => DefaultValue;
            Set = new Action<bool>((value) => { });
        }

        public CheckBox WithDefaultValue(bool defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

        public CheckBox WithGetter(Func<bool> getter)
        {
            Get = getter ?? throw new ArgumentNullException("Getter cannot be null.");
            return this;
        }

        public CheckBox WithSetter(Action<bool> setter)
        {
            Set = setter ?? throw new ArgumentNullException("Setter cannot be null.");
            return this;
        }

        public override void Tweak(CentrifugeMenu menu)
        {
            if (Get == null || Set == null)
                throw new InvalidOperationException("Cannot call Tweak with Get or Set being null.");

            menu.TweakBool(Name, Get(), Set, Description);
            base.Tweak(menu);
        }
    }
}
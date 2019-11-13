using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System;

namespace Centrifuge.Distance.GUI.Controls
{
    public class ActionButton : MenuItemBase
    {
        public Action OnClick { get; set; }

        public ActionButton(MenuDisplayMode mode, string id, string name)
            : base(mode, id, name) { }

        public ActionButton WhenClicked(Action onClick)
        {
            OnClick = onClick;
            return this;
        }

        public override void Tweak(CentrifugeMenu menu)
        {
            if (OnClick == null)
                throw new InvalidOperationException("OnClick action not initialized. Use WhenClicked() to configure the action.");

            menu.TweakAction(Name, OnClick, Description);
            base.Tweak(menu);
        }
    }
}
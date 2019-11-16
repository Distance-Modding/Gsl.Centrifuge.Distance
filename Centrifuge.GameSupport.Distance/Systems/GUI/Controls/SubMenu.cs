using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;

namespace Centrifuge.Distance.GUI.Controls
{
    public class SubMenu : MenuItemBase
    {
        public MenuTree MenuTree { get; private set; }

        public SubMenu(MenuDisplayMode mode, string id, string name)
            : base(mode, id, name) { }

        public SubMenu NavigatesTo(MenuTree menuTree)
        {
            MenuTree = menuTree;
            return this;
        }

        public override void Tweak(CentrifugeMenu menu)
        {
            menu.TweakAction(Name, () =>
            {
                MenuSystem.ShowMenu(MenuTree, menu, 0);
                base.Tweak(menu);
            }, Description);
        }
    }
}
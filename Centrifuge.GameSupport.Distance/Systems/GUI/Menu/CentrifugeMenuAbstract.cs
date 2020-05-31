using Reactor.API.Interfaces.Systems;

namespace Centrifuge.Distance.GUI.Menu
{
    public abstract class CentrifugeMenuAbstract : SuperMenu
    {
        public IManager Manager { get; set; }

        public override string MenuTitleName_ => Resources.Strings.Menu.RootMenuFullName;
        public override string Name_ => Resources.Strings.Menu.RootMenuName;
        public override bool DisplayInMenu(bool isPauseMenu) => true;

        public CentrifugeMenuAbstract()
        {
            menuBlueprint_ = MenuSystem.MenuBlueprint;
        }

        public override void InitializeVirtual() { }
        public override void OnPanelPop() { }
    }
}
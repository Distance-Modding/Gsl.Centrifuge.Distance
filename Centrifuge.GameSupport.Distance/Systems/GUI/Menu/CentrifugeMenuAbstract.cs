using Reactor.API.Interfaces.Systems;

namespace Centrifuge.Distance.GUI.Menu
{
    public abstract class CentrifugeMenuAbstract : SuperMenu
    {
        public MenuPanelManager PanelManager => G.Sys.MenuPanelManager_;

        public IManager Manager { get; set; }

        public abstract string Title { get; }

        public override string MenuTitleName_ => Title;

        public override string Name_ => InternalResources.Strings.MenuSystem.RootMenuName;

        public override bool DisplayInMenu(bool isPauseMenu) => true;

        public CentrifugeMenuAbstract()
        {
            menuBlueprint_ = MenuSystem.MenuBlueprint;
        }

        public override void InitializeVirtual() { }

        public virtual void UpdateVirtual()
        {
            return;
        }

        public virtual void Update()
        {
            return;
        }

        public override void OnPanelPop() { }
    }
}
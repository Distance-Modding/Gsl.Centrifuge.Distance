using Centrifuge.Distance.GUI.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Centrifuge.Distance.GUI.Menu
{
    public static class MenuSystem
    {
        internal static GameObject MenuBlueprint { get; set; }
        internal static MenuTree MenuTree { get; set; }
        static MenuSystem() => MenuTree = new MenuTree("menu.centrifuge.main", Resources.Strings.Menu.RootMenuName);

        internal static void ShowMenu(MenuTree menuTree, SuperMenu parentMenu, int pageIndex)
        {
            foreach (var component in parentMenu.PanelObject_.GetComponents<CentrifugeMenu>())
                component.Destroy();

            var menu = parentMenu.PanelObject_.AddComponent<CentrifugeMenu>();

            menu.CurrentPageIndex = pageIndex;
            menu.MenuPanel = MenuPanel.Create(menu.PanelObject_, true, true, false, true, false, false);

            menu.MenuPanel.onPanelPop_ += () =>
            {
                if (!G.Sys.MenuPanelManager_.panelStack_.Contains(menu.MenuPanel))
                {
                    parentMenu.PanelObject_.SetActive(true);
                    if (menu.SwitchPageOnClose)
                        ShowMenu(menuTree, parentMenu, pageIndex);
                }
            };

            menu.Title = menuTree.Title;
            menu.MenuTree = menuTree;

            parentMenu.PanelObject_.SetActive(false);

            G.Sys.MenuPanelManager_.Push(menu.MenuPanel);
        }

        internal static void ShowRootMenu(MenuTree menuTree, GameObject parent, int pageIndex)
        {
            foreach (var component in parent.GetComponents<CentrifugeMenu>())
                component.Destroy();

            var menu = parent.AddComponent<CentrifugeMenu>();

            menu.CurrentPageIndex = pageIndex;
            menu.MenuPanel = MenuPanel.Create(menu.PanelObject_, true, true, false, true, false, false);

            menu.MenuPanel.onPanelPop_ += () =>
            {
                if (!G.Sys.MenuPanelManager_.panelStack_.Contains(menu.MenuPanel))
                {
                    parent.SetActive(true);
                    if (menu.SwitchPageOnClose)
                        ShowRootMenu(menuTree, parent, pageIndex);
                }
            };

            menu.Title = menuTree.Title;
            menu.MenuTree = menuTree;

            parent.SetActive(false);

            G.Sys.MenuPanelManager_.Push(menu.MenuPanel);
        }

        public static MenuDisplayMode GetCurrentDisplayMode()
        {
            if (SceneManager.GetActiveScene().name.ToLower() == "mainmenu")
                return MenuDisplayMode.MainMenu;

            return MenuDisplayMode.PauseMenu;
        }
    }
}
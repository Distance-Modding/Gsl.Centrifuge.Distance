using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Centrifuge.Distance.GUI.Menu
{
    public static class MenuSystem
    {
        internal static GameObject MenuBlueprint { get; set; }

        internal static MenuTree MenuTree { get; set; }

        static MenuSystem()
        {
            MenuTree = new MenuTree("menu.centrifuge.main", InternalResources.Strings.MenuSystem.RootMenuFullName);
        }
        
        internal static void ShowMenu(MenuTree menuTree, CentrifugeMenu parentMenu, int pageIndex)
        {
            if (menuTree.GetItems().Count is 0)
            {
                ShowUnavailableMessage();
                return;
            }

            foreach (var component in parentMenu.PanelObject_.GetComponents<CentrifugeMenu>())
            {
                component.Destroy();
            }

            var menu = GameAPI.Instance.gameObject.AddComponent<CentrifugeMenu>();
            menu.MenuTree = menuTree;

            menu.CurrentPageIndex = pageIndex;
            menu.MenuPanel = MenuPanel.Create(menu.PanelObject_, true, true, false, true, true, true);
            menu.MenuPanel.backgroundOpacity_ = 0.75f;

            menu.MenuPanel.onIsTopChanged_ += (isTop) =>
            {
                if (isTop)
                {
                    menu.ResetAnimations();
                }
                else
                {
                    if (G.Sys.MenuPanelManager_.panelStack_.Contains(menu.MenuPanel))
                    {
                        menu.SwitchPage(menu.CurrentPageIndex, false, true);
                    }
                    else
                    {
                        menu.SwitchPage(0, false, true);
                    }
                }
            };

            menu.MenuPanel.onPanelPop_ += () =>
            {
                if (!G.Sys.MenuPanelManager_.panelStack_.Contains(menu.MenuPanel))
                {
                    menu.SwitchPage(0, false, true);
                    parentMenu.PanelObject_.SetActive(true);

                    if (menu.MenuTree != MenuTree)
                    {
                        menu.PanelObject_.Destroy();
                    }

                    menu.Destroy();
                }
            };

            parentMenu.PanelObject_.SetActive(false);

            menu.MenuPanel.Push();
        }

        public static void ShowUnavailableMessage()
        {
            MessageBox.Create(InternalResources.Strings.MenuSystem.UnavailableMenuError, InternalResources.Strings.MenuSystem.UnavailableMenuErrorTitle)
            .SetButtons(Distance.Data.MessageButtons.Ok)
            .Show();
        }

        public static MenuDisplayMode GetCurrentDisplayMode()
        {
            if (SceneManager.GetActiveScene().name.ToLower() == "mainmenu")
            {
                return MenuDisplayMode.MainMenu;
            }

            return MenuDisplayMode.PauseMenu;
        }
    }
}
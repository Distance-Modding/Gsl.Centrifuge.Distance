using Centrifuge.Distance.Data;
using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Events.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Centrifuge.Distance.GUI.Menu
{
    public static class MenuSystem
    {
        internal static GameObject MenuBlueprint { get; set; }

        internal static Transition Transition { get; set; } = Transition.In;

        internal static MenuTree TransitionTree { get; set; }

        internal static MenuTree MenuTree { get; set; }

        static MenuSystem()
        {
            MenuTree = new MenuTree("menu.centrifuge.main", InternalResources.Strings.MenuSystem.RootMenuFullName)
            .SetType(MenuType.Root);

            TransitionTree = new MenuTree("menu.centrifuge.void", string.Empty)
            {
                new EmptyElement(),
                new SubMenu(MenuDisplayMode.Both, "nav:main_menu", "OPEN SETTINGS")
                .NavigatesTo(MenuTree)
            }
            .SetType(MenuType.None);

            MenuOpened.Subscribe(OnMenuOpened);
        }

        internal static void OnMenuOpened(MenuOpened.Data data)
        {
            MenuPanelManager manager = G.Sys.MenuPanelManager_;

            if (data.menu.MenuTree == TransitionTree)
            {
                // TODO: patch push and pop
                // Pop twice when required
                // 

                switch (Transition)
                {
                    case Transition.In:
                        Transition = Transition.Out;

                        //manager.panelStack_.Remove(data.menu.MenuPanel);

                        //ShowMenu(MenuTree, data.menu, 0);


                        break;
                    case Transition.Out:
                        // Reset transition and close menu
                        Transition = Transition.In;

                        //MenuPanel panel = manager.panelStack_.Last();

                        //panel.dontDisableOnPop_ = false;
                        //panel.Pop();

                        break;
                }
            }
        }

        internal static void ShowMenu(MenuTree menuTree, SuperMenu parentMenu, int pageIndex)
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

            menu.MenuPanel.onPanelPop_ += () =>
            {
                if (!G.Sys.MenuPanelManager_.panelStack_.Contains(menu.MenuPanel))
                {
                    parentMenu.PanelObject_.SetActive(true);

                    if (menu.SwitchPageOnClose)
                    {
                        menu.SwitchPageOnClose = false;
                        ShowMenu(menuTree, parentMenu, menu.CurrentPageIndex);
                    }
                    else
                    {
                        menu.Destroy();
                    }
                }
            };

            parentMenu.PanelObject_.SetActive(false);

            //G.Sys.MenuPanelManager_.Push(menu.MenuPanel);
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
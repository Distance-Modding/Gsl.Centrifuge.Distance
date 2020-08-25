using Events.GUI;
using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Data;
using Reactor.API.Interfaces.Systems;
using System;
using System.Linq;
using UnityEngine;
using Centrifuge.Distance.Data;

namespace Centrifuge.Distance.GUI.Menu
{
    public class CentrifugeMenu : CentrifugeMenuAbstract
    {
        private const int MaxEntriesPerPage = 9;

        private InputManager InputManager { get; set; }

        private int PageCount => (int)Math.Max(Math.Ceiling(MenuTree.Count / (float)MaxEntriesPerPage), 1);

        public override string Title => MenuTree.Title;

        public GameObject TitleLabel => PanelObject_.transform.Find("MenuTitleTemplate/UILabel - Title").gameObject;
        
        public GameObject DescriptionLabel => PanelObject_.transform.Find("MenuTitleTemplate/UILabel - Description").gameObject;
        
        public GameObject OptionsTable => PanelObject_.transform.Find("Options/OptionsTable").gameObject;

        internal MenuType Type => MenuTree.Type;
        
        public MenuPanel MenuPanel { get; internal set; }
        
        public MenuTree MenuTree { get; internal set; } = new MenuTree("menu.centrifuge.error", "[FF0000]Error[-]");
                
        public string Description { get; set; }
        
        public bool SwitchPageOnClose { get; internal set; }
        
        public int CurrentPageIndex { get; internal set; } = 0;
        
        public string Id => MenuTree.Id;

        public override void InitializeVirtual()
        {
            InputManager = G.Sys.InputManager_;

            if (!MenuTree.GetItems().Any())
            {
                MessageBox.Create(InternalResources.Strings.MenuSystem.UnavailableMenuError, InternalResources.Strings.MenuSystem.UnavailableMenuErrorTitle)
                .SetButtons(MessageButtons.Ok)
                .OnConfirm(() =>
                {
                    PanelManager.TopPanel_.onPanelPop_ += () =>
                    {
                        MenuPanel.Pop();
                    };
                })
                .Show();
            }

            DisplayMenu();

            MenuOpened.Broadcast(new MenuOpened.Data(this));
        }

        public CentrifugeMenu WithManager(IManager manager)
        {
            Manager = manager;
            return this;
        }

        private void DisplayMenu()
        {
            MenuTree currentTree = MenuTree.GetItems(MenuSystem.GetCurrentDisplayMode());

            if (currentTree.Any())
            {
                for (int i = CurrentPageIndex * MaxEntriesPerPage; i < (CurrentPageIndex * MaxEntriesPerPage) + MaxEntriesPerPage; i++)
                {
                    if (i < currentTree.Count)
                    {
                        currentTree[i]?.Tweak(this);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public override void Update()
        {
            bool flag = true;

            flag &= PanelObject_ != null;
            flag &= PanelObject_?.activeInHierarchy is true;

            if (flag)
            {
                UpdateVirtual();
            }
        }

        public override void UpdateVirtual()
        {
            bool multiplePages = MenuTree.Count > MaxEntriesPerPage;

            G.Sys.MenuPanelManager_.SetBottomLeftActionButtonEnabled(InputAction.MenuPageUp, multiplePages);
            G.Sys.MenuPanelManager_.SetBottomLeftActionButtonEnabled(InputAction.MenuPageDown, multiplePages);

            G.Sys.MenuPanelManager_.SetBottomLeftActionButton(InputAction.MenuPageUp, InternalResources.Strings.MenuSystem.MenuActionPrevious);
            G.Sys.MenuPanelManager_.SetBottomLeftActionButton(InputAction.MenuPageDown, InternalResources.Strings.MenuSystem.MenuActionNext);
            
            if (multiplePages)
            {
                if (InputManager.GetKeyUp(InputAction.MenuPageUp))
                {
                    CurrentPageIndex -= 1;
                    CurrentPageIndex = CurrentPageIndex < 0 ? PageCount - 1 : CurrentPageIndex > PageCount - 1 ? 0 : CurrentPageIndex;
                    SwitchPageOnClose = true;
                }

                if (InputManager.GetKeyUp(InputAction.MenuPageDown))
                {
                    CurrentPageIndex += 1;
                    CurrentPageIndex = CurrentPageIndex < 0 ? PageCount - 1 : CurrentPageIndex > PageCount - 1 ? 0 : CurrentPageIndex;
                    SwitchPageOnClose = true;
                }
            }

            if (Type == MenuType.Root)
            {
                MenuSystem.Transition = SwitchPageOnClose ? Transition.None : Transition.Out;
            }

            if (SwitchPageOnClose)
            {
                MenuPanel.Pop();
            }

            Description = string.Format(InternalResources.Strings.MenuSystem.MenuPageDescription, CurrentPageIndex + 1, PageCount);

            TitleLabel?.SetActive(true);
            UILabel TitleLabelObject = TitleLabel.GetComponent<UILabel>();

            (menu_.menuTitleLabel_ ?? TitleLabelObject).text = Title;

            DescriptionLabel?.SetActive(true);
            UILabel DescriptionLabelObject = DescriptionLabel.GetComponent<UILabel>();

            menu_.menuDescriptionLabel_.text_ = DescriptionLabelObject.text = Description;
        }
    }
}
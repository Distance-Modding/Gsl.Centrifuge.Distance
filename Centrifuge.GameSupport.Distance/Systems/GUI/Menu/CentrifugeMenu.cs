using Centrifuge.Distance.Data;
using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Data;
using Events.GUI;
using Events.Menu;
using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;

namespace Centrifuge.Distance.GUI.Menu
{
    public class CentrifugeMenu : CentrifugeMenuAbstract
    {
        private const int MaxEntriesPerPage = 9;

        #region Properties / Fields / ...
        private InputManager InputManager { get; set; }

        private int PageCount => (int)Math.Max(Math.Ceiling(MenuTree.Count / (float)MaxEntriesPerPage), 1);

        private SuperDuperMenu MenuController => PanelObject_?.GetComponent<SuperDuperMenu>();

        public override string Title => MenuTree.Title;

        public GameObject TitleLabel => PanelObject_.transform.Find("MenuTitleTemplate/UILabel - Title").gameObject;
        
        public GameObject DescriptionLabel => PanelObject_.transform.Find("MenuTitleTemplate/UILabel - Description").gameObject;
        
        public GameObject OptionsTable => PanelObject_.transform.Find("Options/OptionsTable").gameObject;
        
        public MenuPanel MenuPanel { get; internal set; }
        
        public MenuTree MenuTree { get; internal set; } = new MenuTree("menu.centrifuge.error", "[FF0000]Error[-]");
                
        public string Description { get; set; }
                
        public int CurrentPageIndex { get; internal set; } = 0;

        public bool ShouldAnimate { get; internal set; } = true;

        public string Id => MenuTree.Id;
        #endregion

        #region Display Entries
        public override void InitializeVirtual()
        {
            InputManager = G.Sys.InputManager_;

            DisplayMenu();

            MenuOpened.Broadcast(new MenuOpened.Data(this));
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
            else
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
        }
        #endregion

        public void OnEnable()
        {
            if (ShouldAnimate)
            {
                // Reset animation duration/delay on optiontable UIExFadeIn components
                ShouldAnimate = false;
            }
        }

        public void SwitchPage(int value = 0, bool relative = true, bool resetObjects = true)
        {
            if (relative)
            {
                CurrentPageIndex += value;
            }
            else
            {
                CurrentPageIndex = value;
            }

            CurrentPageIndex = CurrentPageIndex < 0 ? PageCount - 1 : CurrentPageIndex > PageCount - 1 ? 0 : CurrentPageIndex;

            if (resetObjects)
            {
                foreach (var item in MenuController.items_)
                {
                    if (SuperDuperMenu.defaultFloatValues_.ContainsKey(item.name))
                    {
                        SuperDuperMenu.defaultFloatValues_.Remove(item.name);
                    }

                    item.gameObject.DeactivateAndDestroy();
                }

                MenuController.items_.Clear();

                MenuController.actions_.Values.Do(x => x.gameObject.DeactivateAndDestroy());
                MenuController.toggles_.Values.Do(x => x.gameObject.DeactivateAndDestroy());
                MenuController.sliders_.Values.Do(x => x.gameObject.DeactivateAndDestroy());
                MenuController.popups_.Values.Do(x => x.gameObject.DeactivateAndDestroy());

                MenuController.actions_.Clear();
                MenuController.toggles_.Clear();
                MenuController.sliders_.Clear();
                MenuController.popups_.Clear();

                MenuController.buttonsTable_.Reposition();

                MenuController.SetDescription(null, "");

                SetDescription.Broadcast(new SetDescription.Data(string.Empty, true, string.Empty));

                MenuController.Init(this);
                MenuController.Initialize();
            }
        }

        #region Update
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
                    SwitchPage(-1);
                }

                if (InputManager.GetKeyUp(InputAction.MenuPageDown))
                {
                    SwitchPage(+1);
                }
            }

            Description = string.Format(InternalResources.Strings.MenuSystem.MenuPageDescription, CurrentPageIndex + 1, PageCount);

            TitleLabel?.SetActive(true);
            UILabel TitleLabelObject = TitleLabel.GetComponent<UILabel>();

            (menu_.menuTitleLabel_ ?? TitleLabelObject).text = Title;

            DescriptionLabel?.SetActive(true);
            UILabel DescriptionLabelObject = DescriptionLabel.GetComponent<UILabel>();

            menu_.menuDescriptionLabel_.text_ = DescriptionLabelObject.text = Description;
        }
        #endregion
    }
}
using Centrifuge.Distance.Data;
using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Data;
using Events.GUI;
using Events.Menu;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngineInternal;

namespace Centrifuge.Distance.GUI.Menu
{
    public class CentrifugeMenu : CentrifugeMenuAbstract
    {
        private const int MaxEntriesPerPage = 9;

        #region Properties / Fields / ...
        private InputManager InputManager { get; set; }

        private GameObject[] Blueprints => new GameObject[]
        {
            MenuController.actionBlueprint_,
            MenuController.sliderBlueprint_,
            MenuController.popupBlueprint_,
            MenuController.toggleBlueprint_
        };

        private int PageCount => (int)Math.Max(Math.Ceiling(MenuTree.Count / (float)MaxEntriesPerPage), 1);

        private SuperDuperMenu MenuController => PanelObject_?.GetComponent<SuperDuperMenu>();
        
        private UIExFancyFadeInMenu MenuFade => PanelObject_?.GetComponent<UIExFancyFadeInMenu>();

        public override string Title => MenuTree.Title;

        public GameObject TitleLabel => PanelObject_.transform.Find("MenuTitleTemplate/UILabel - Title").gameObject;
        
        public GameObject DescriptionLabel => PanelObject_.transform.Find("MenuTitleTemplate/UILabel - Description").gameObject;
        
        public GameObject OptionsTable => PanelObject_.transform.Find("Options/OptionsTable").gameObject;
        
        public MenuPanel MenuPanel { get; internal set; }
        
        public MenuTree MenuTree { get; internal set; } = new MenuTree("menu.centrifuge.error", "[FF0000]Error[-]");
                
        public string Description { get; set; }
                
        public int CurrentPageIndex { get; internal set; } = 0;

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
            ResetAnimations();
        }

        public void ResetAnimations()
        {
            foreach (UIWidget widget in MenuFade.widgets_.ToArray() ?? new UIWidget[0])
            {
                GameAPI.Instance.Logger.Info(widget.name);
            }

            List<UIExFancyFadeIn> fades = new List<UIExFancyFadeIn>();

            foreach (GameObject item in OptionsTable.GetChildren())
            {
                if (!Blueprints.Contains(item))
                {
                    GameAPI.Instance.Logger.Warning($"{item.name} - {item.HasComponent<UIExFancyFadeIn>()}");

                    UIExFancyFadeIn fade = item.GetOrAddComponent<UIExFancyFadeIn>();

                    fades.Add(fade);
                }
            }

            Vector2 min = float.MinValue.ToVector2();
            Vector2 max = float.MaxValue.ToVector2();

            foreach (UIExFancyFadeIn fade in fades)
            {
                UIWidget widget = fade.GetComponent<UIWidget>();
                Vector2 worldCenter = widget.worldCenter;

                min = Vector2.Max(min, worldCenter);
                max = Vector2.Min(max, worldCenter);
            }

            foreach (UIExFancyFadeIn fade in fades)
            {
                UIWidget widget = fade.GetComponent<UIWidget>();

                Vector2 worldCenter = widget.worldCenter;
                float num = 1 - Mathf.InverseLerp(max.y, min.y, worldCenter.y);

                fade.offset_ = new Vector3(MenuFade.offset_, 0);
                fade.duration_ = MenuFade.duration_;
                fade.delay_ = num * MenuFade.delay_;

                fade.offset_.x *= -1;

                fade.initialAlpha_ = 1;
                widget.alpha = 1;

                fade.Init();
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
                void destroyObject(GameObject obj)
                {
                    obj.transform.parent = null;
                    obj.DeactivateAndDestroy();
                    DestroyImmediate(obj);
                }

                foreach (var item in MenuController.items_)
                {
                    if (SuperDuperMenu.defaultFloatValues_.ContainsKey(item.name))
                    {
                        SuperDuperMenu.defaultFloatValues_.Remove(item.name);
                    }

                    UIWidget widget = item.GetComponent<UIWidget>();

                    if (widget && MenuFade.widgets_.Contains(widget))
                    {
                        MenuFade.widgets_.Remove(widget);
                    }

                    item.gameObject.DeactivateAndDestroy();
                }

                MenuController.items_.Clear();

                MenuController.actions_.Values.Do(x => destroyObject(x.gameObject));
                MenuController.toggles_.Values.Do(x => destroyObject(x.gameObject));
                MenuController.sliders_.Values.Do(x => destroyObject(x.gameObject));
                MenuController.popups_.Values.Do(x => destroyObject(x.gameObject));

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
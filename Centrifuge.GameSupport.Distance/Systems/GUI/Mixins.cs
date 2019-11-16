using Centrifuge.Distance.GUI.Menu;
using Harmony;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Centrifuge.Distance.GUI
{
    internal class Mixins
    {
        //[HarmonyPatch(typeof(MainMenuGameModeButtons), "Init", new Type[] { typeof(IEnumerable<string>), typeof(Action<string>) })]
        public class InitModeButtons
        {
            static bool Prefix(MainMenuGameModeButtons __instance, ref IEnumerable<string> modeNames, ref Action<string> onModeClick)
            {
                GameAPI.Logger.Warning(__instance.name);

                if (__instance.gameObject.name != "Options") return true;

                List<MenuButtonList.ButtonInfo> buttonInfoList = new List<MenuButtonList.ButtonInfo>();
                foreach (string modeName in modeNames)
                {
                    MenuButtonList.ButtonInfo buttonInfo = new MenuButtonList.ButtonInfo
                    {
                        name = modeName
                    };
                    if (onModeClick != null)
                        buttonInfo.onClick = __instance.CreateAction(modeName, onModeClick);
                    buttonInfo.description = G.Sys.GameManager_.GetModeDescription(modeName);
                    buttonInfoList.Add(buttonInfo);
                }
                buttonInfoList.Add(CreateEntry(Resources.Strings.Menu.RootMenuName, Resources.Strings.Menu.RootMenuFullName, () => MenuSystem.ShowRootMenu(MenuSystem.MenuTree, __instance.gameObject, 0)));
                __instance.Init(buttonInfoList);

                GameObject buttons = __instance.transform.Find("OptionsButtons").gameObject;
                foreach (GameObject button in buttons.GetChildren())
                {
                    SetMenuDescriptionOnHover menudescription = button.GetComponent<SetMenuDescriptionOnHover>();
                    if (menudescription.Text_ == Resources.Strings.Menu.RootMenuFullName)
                    {
                        ModeCompleteStatusMenuLogic status = button.transform.Find("Finish Status").GetComponent<ModeCompleteStatusMenuLogic>();

                        status.gameObject.SetActive(true);
                        status.gameObject.AddComponent<TargetObject>();
                        status.StartAnimation();
                    }
                }
                return false;
            }

            public class TargetObject : MonoBehaviour
            { }

            static MenuButtonList.ButtonInfo CreateEntry(string name, string description, Action action)
            {
                MenuButtonList.ButtonInfo buttonInfo = new MenuButtonList.ButtonInfo
                {
                    name = name,
                    description = description,
                    onClick = action
                };
                return buttonInfo;
            }
        }

        //[HarmonyPatch(typeof(OptionsButtons), "GetButtonInfos")]
        public class GetOptionsButtonInfos
        {
            static void Postfix(OptionsButtons __instance, List<MenuButtonList.ButtonInfo> __result, ref OptionsMenuLogic optionsMenu, ref bool isPauseMenu)
            {
                __result.Add(new MenuButtonList.ButtonInfo()
                {
                    name = Resources.Strings.Menu.RootMenuFullName,
                    onClick = () =>
                    {
                        MenuSystem.ShowRootMenu(MenuSystem.MenuTree, __instance.gameObject, 0);
                    }
                });
            }
        }

        [HarmonyPatch(typeof(OptionsMenuLogic), "GetSubmenus")]
        public class GetOptionsSubmenus
        {
            static void Prefix(OptionsMenuLogic __instance, IEnumerable<OptionsSubmenu> __result, ref bool isPauseMenu)
            {
                foreach (var menu in __instance.subMenus_)
                    if (menu.GetType().IsSubclassOf(typeof(SuperMenu)))
                        MenuSystem.MenuBlueprint = ((SuperMenu)menu).menuBlueprint_;


                var CentrifugeMenu = __instance.gameObject.AddComponent<CentrifugeMenu>();
                CentrifugeMenu.IsRootMenu = true;
                CentrifugeMenu.MenuTree = MenuSystem.MenuTree;

                List<OptionsSubmenu> menus = new List<OptionsSubmenu>(__instance.subMenus_);

                foreach (var menu in __instance.subMenus_)
                    if (menu.Name_ == CentrifugeMenu.Name_)
                        menus.Remove(menu);

                menus.Add(CentrifugeMenu);

                __instance.subMenus_ = menus.ToArray();
            }
        }

        [HarmonyPatch(typeof(ModeCompleteStatusMenuLogic), "SetValues")]
        public class ModeCompleteStatusMenuLogicSetValues
        {
            static bool Prefix(ModeCompleteStatusMenuLogic __instance, float t)
            {
                return false;
                GameObject container = __instance.gameObject;
                InitModeButtons.TargetObject target = container.GetComponent<InitModeButtons.TargetObject>();
                if (target)
                {
                    __instance.progressBar_.enabled = false;
                    __instance.completeLabel_.text = string.Format(Resources.Strings.Menu.RootMenuIndicator, GameAPI.Manager.GetLoadedMods().Count);
                    

                    return false;
                }
                return true;
            }
        }
    }
}

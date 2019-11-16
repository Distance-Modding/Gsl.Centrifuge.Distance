using Centrifuge.Distance.GUI.Menu;
using Harmony;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Centrifuge.Distance.GUI
{
    internal class Mixins
    {
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
    }
}

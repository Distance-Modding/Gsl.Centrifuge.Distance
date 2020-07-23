using Centrifuge.Distance.GUI.Menu;
using Harmony;
using System.Collections.Generic;
using System.Linq;

namespace Centrifuge.Distance.Systems.GUI.Harmony
{
    [HarmonyPatch(typeof(OptionsMenuLogic), "DisplaySubmenu")]
    internal static class OptionsMenuLogic__DisplaySubmenu
    {
        [HarmonyPrefix]
        internal static bool Prefix(OptionsMenuLogic __instance, string submenuName)
        {
            List<OptionsSubmenu> menus = __instance.subMenus_.ToList();

            OptionsSubmenu subMenu = menus.Find(x => x.Name_ == submenuName);

            if (subMenu && subMenu is CentrifugeMenu)
            {
                CentrifugeMenu centrifugeMenu = subMenu as CentrifugeMenu;

                if (!centrifugeMenu.MenuTree.Any())
                {
                    MenuSystem.ShowUnavailableMessage();
                    return false;
                }
            }

            return true;
        }
    }
}
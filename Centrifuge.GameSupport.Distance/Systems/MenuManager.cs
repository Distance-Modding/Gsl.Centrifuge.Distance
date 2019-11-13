using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System;

namespace Centrifuge.Distance.Systems
{
    public static class MenuManager
    {
        public static bool AddMenu(MenuTree menu) => AddMenu(menu, string.Empty);

        public static bool AddMenu(MenuTree menu, string description) => AddMenu((SubMenu)menu, description);

        public static bool AddMenu(SubMenu menu) => AddMenu(menu, string.Empty);

        public static bool AddMenu(SubMenu menu, string description)
        {
            try
            {
                MenuSystem.MenuTree.Add(menu);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System;

namespace Centrifuge.Distance.Game
{
    public static class SettingsMenu
    {
        public static bool AddNew(MenuTree menu) => AddNew(menu, string.Empty);

        public static bool AddNew(MenuTree menu, string description)
        {
            SubMenu submenu = menu;
            submenu.Description = description;
            return AddNew(submenu);
        }
        
        public static bool AddNew(SubMenu menu)
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

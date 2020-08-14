using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System;

namespace Centrifuge.Distance.Game
{
    public static class Menus
    {
        public static void AddNew(MenuDisplayMode displayMode, MenuTree menuTree, string description = null)
        {
            AddNew(displayMode, menuTree, menuTree.Title, description);
        }

        public static void AddNew(MenuDisplayMode displayMode, MenuTree menuTree, string title, string description = null)
        {
            try
            {
                MenuSystem.MenuTree.Add(new SubMenu(displayMode, menuTree.Id, title)
                    .NavigatesTo(menuTree)
                    .WithDescription(description)
                );

                GameAPI.Instance.Logger.Info($"Added new menu tree: '{menuTree.Id}', '{menuTree.Title}'...");
            }
            catch (Exception ex)
            {
                GameAPI.Instance.Logger.Error($"Failed to add the menu tree: '{menuTree.Id}', '{menuTree.Title}'.");
                GameAPI.Instance.Logger.Exception(ex);
            }
        }
    }
}

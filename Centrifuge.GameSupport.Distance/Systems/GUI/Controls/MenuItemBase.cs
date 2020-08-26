using Centrifuge.Distance.GUI.Data;
using Centrifuge.Distance.GUI.Menu;
using System.Linq;
using UnityEngine;

namespace Centrifuge.Distance.GUI.Controls
{
    public abstract class MenuItemBase
    {
        public MenuDisplayMode Mode { get; }

        public string Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        protected MenuItemBase(MenuDisplayMode mode, string id, string name)
        {
            Mode = mode;
            Id = id;
            Name = name;
        }

        public MenuItemBase WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public virtual void Tweak(CentrifugeMenu menu)
        {
            //GameObject item = menu.OptionsTable.transform.Find(Name).gameObject;
            GameObject[] items = (from x in menu.OptionsTable.GetChildren() where string.Equals(x.name, Name) select x).ToArray();

            GameObject item = null;

            if (items.Any())
            {
                item = items.First();
            }

            if (item != null)
            {
                MenuItemInfo info = item.AddComponent<MenuItemInfo>();
                info.Item = this;
            }
        }
    }
}
using Centrifuge.Distance.GUI.Controls;
using UnityEngine;

namespace Centrifuge.Distance.GUI.Data
{
    public class MenuItemInfo : MonoBehaviour
    {
        public string Id => Item?.Id;
        public MenuItemBase Item { get; set; }
    }
}

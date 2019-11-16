#pragma warning disable IDE0051

using UnityEngine;

namespace Centrifuge.Distance.GUI.Menu
{
    internal class CentrifugeMenuController : MonoBehaviour
    {
        internal CentrifugeMenu Menu { get; set; }

        void Update()
        {
            if (Menu && Menu.PanelObject_ && Menu.PanelObject_.activeInHierarchy && Menu.MenuPanel.IsTop_)
                Menu?.UpdateVirtual();
        }
    }
}
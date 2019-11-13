using UnityEngine;

namespace Centrifuge.Distance.GUI.Menu
{
    internal class SpectrumMenuController : MonoBehaviour
    {
        internal CentrifugeMenu Menu { get; set; }

        void Update()
        {
            if (Menu != null && Menu.PanelObject_.activeInHierarchy && Menu.MenuPanel.IsTop_)
                Menu.UpdateVirtual();
        }
    }
}
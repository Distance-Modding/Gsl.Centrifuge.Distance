using Centrifuge.Distance.GUI.Menu;

namespace Events.GUI
{
    public class MenuOpened : StaticEvent<MenuOpened.Data>
    {
        public struct Data
        {
            public CentrifugeMenu menu;

            public Data(CentrifugeMenu m)
            {
                menu = m;
            }
        }
    }
}

using Centrifuge.Distance.GUI.Menu;

namespace Events.GUI
{
    public class MenuClosed : StaticEvent<MenuClosed.Data>
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

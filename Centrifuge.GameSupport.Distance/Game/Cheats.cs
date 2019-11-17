using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Centrifuge.Distance.Game
{
    public static class Cheats
    {
        public static CheatsManager Manager => G.Sys.CheatsManager_;

        public static bool GameplayCheatsUsed => Manager.anyGameplayCheatsUsedThisLevel_;
        public static bool GameplayCheatsUsing => Manager.AnyGameplayCheatsCurrentlyUsed_;
        public static bool IsCheatEnabled(ECheat cheat) => Manager.IsEnabled(cheat);
        public static bool IsCheatUnlocked(ECheat cheat) => Manager.IsUnlocked(cheat);
        public static bool EnableCheat(ECheat cheat, bool state)
        {
            if (IsCheatUnlocked(cheat))
            {
                Manager.SetEnabled(cheat, state);
                return IsCheatEnabled(cheat) == state;
            }
            return false;
        }
    }
}

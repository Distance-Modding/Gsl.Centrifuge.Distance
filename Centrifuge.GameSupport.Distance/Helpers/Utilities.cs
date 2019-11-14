// Original code by Ciastex (http://github.com/ciastex)
// File abailable at https://github.com/Ciastex/Spectrum/blob/5d507db3266f2331eb29feb34754252c5edb6e01/Spectrum.Interop/Helpers/Utilities.cs

using UnityEngine;

namespace Centrifuge.Distance.Helpers
{
    internal static class Utilities
    {
        internal static GameObject FindLocalCar()
        {
            return G.Sys.PlayerManager_?.Current_?.playerData_?.Car_;
        }

        internal static CarLogic FindLocalCarLogic()
        {
            return G.Sys.PlayerManager_?.Current_?.playerData_?.CarLogic_;
        }

        internal static CarScreenLogic FindLocalVehicleScreen()
        {
            var carScreenLogic = G.Sys.PlayerManager_?.Current_?.playerData_?.CarScreenLogic_;
            if (carScreenLogic?.CarLogic_.IsLocalCar_ ?? false)
            {
                return carScreenLogic;
            }
            return null;
        }
    }
}

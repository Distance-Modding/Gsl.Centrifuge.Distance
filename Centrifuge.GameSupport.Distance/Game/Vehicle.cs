using Centrifuge.Distance.Helpers;
using UnityEngine;

namespace Centrifuge.Distance.Game
{
    public static class Vehicle
    {
        public static class Hud
        {
            private static HoverScreenEmitter HoverScreenEmitter { get; set; }
            private static bool CanOperateOnHoverScreen => HoverScreenEmitter != null;

            public static void SetHUDText(string text, float displayTime, int priority = -1)
            {
                UpdateParentObject();

                if (CanOperateOnHoverScreen)
                {
                    HoverScreenEmitter.SetTrickText(new TrickyTextLogic.TrickText(displayTime, priority, TrickyTextLogic.TrickText.TextType.standard, text));
                }
            }

            public static void SetHUDText(string text, float time = 3.0f)
            {
                SetHUDText(text, time);
            }

            public static void Clear()
            {
                HoverScreenEmitter hse = GameObject.Find("LocalCar")?.GetComponent<HoverScreenEmitter>();
                HoverScreenParent hsp = hse.hoverScreenParent_;
                TrickyTextLogic ttl = hsp.rightTrickyTextObj_.GetComponent<TrickyTextLogic>();
                ttl.textList_.Clear();
            }

            private static void UpdateParentObject()
            {
                var localCar = Utilities.FindLocalCar();
                HoverScreenEmitter = localCar?.GetComponent<HoverScreenEmitter>();
            }
        }

        public static CarStats CarStats => Utilities.FindLocalVehicleScreen().CarLogic_.CarStats_;

        private static CarLogic CarLogic { get; set; }

        public static float HeatLevel
        {
            get
            {
                UpdateObjectReferences();
                if (CarLogic)
                {
                    return CarLogic.Heat_;
                }
                return 0f;
            }
        }

        public static float VelocityKPH
        {
            get
            {
                UpdateObjectReferences();
                if (CarLogic)
                {
                    return CarLogic.CarStats_.GetKilometersPerHour();
                }
                return 0f;
            }
        }

        public static float VelocityMPH
        {
            get
            {
                UpdateObjectReferences();
                if (CarLogic)
                {
                    return CarLogic.CarStats_.GetMilesPerHour();
                }
                return 0f;
            }
        }

        public static void SetJetFlamesColor(string hexColor)
        {
            UpdateObjectReferences();
            if (CarLogic && CarLogic.CarLogicLocal_)
            {
                var jets = CarLogic.CarLogicLocal_.jetsGadget_;
                if (jets)
                {
                    var flames = jets.flames_;

                    foreach (var flame in flames)
                    {
                        flame.SetCustomColor(hexColor.ToColor());
                    }
                }
            }
        }

        public static void SetBoostFlameColor(string hexColor)
        {
            UpdateObjectReferences();
            if (CarLogic && CarLogic.CarLogicLocal_)
            {
                var booster = Reflection.GetPrivate<BoostGadget>(CarLogic.CarLogicLocal_, "boostGadget_");
                if (booster != null)
                {
                    foreach (var flame in booster.flames_)
                    {
                        flame.SetCustomColor(hexColor.ToColor());
                    }
                }
            }
        }

        public static void SetWingTrailsColor(string hexColor)
        {
            UpdateObjectReferences();
            if (CarLogic && CarLogic.CarLogicLocal_ != null)
            {
                var wingsGadget = Reflection.GetPrivate<WingsGadget>(CarLogic.CarLogicLocal_, "wingsGadget_");
                if (wingsGadget != null)
                {
                    var wingTrailHelpers = Reflection.GetPrivate<WingTrailHelper[]>(wingsGadget, "wingTrails_");
                    if (wingTrailHelpers != null)
                    {
                        foreach (var wingTrailHelper in wingTrailHelpers)
                        {
                            var wingTrail = Reflection.GetPrivate<WingTrail>(wingTrailHelper, "wingTrail_");
                            if (wingTrail != null)
                            {
                                wingTrail.GetComponent<Renderer>().material.color = hexColor.ToColor();
                            }
                        }
                    }
                }
            }
        }

        public static void SetInfiniteCooldown(bool value)
        {
            CarLogic.SetInfiniteCooldown(value);
        }

        private static void UpdateObjectReferences()
        {
            CarLogic = (Utilities.FindLocalCar()?.GetComponent<CarLogic>()) ?? Utilities.FindLocalCarLogic();
        }

        public static class Screen
        {
            private static CarScreenLogic CarScreenLogic { get; set; }

            public static int LineLength { get; set; } = 20;

            internal static void FindScreen()
            {
                CarScreenLogic = Utilities.FindLocalVehicleScreen();
            }

            public static void Clear()
            {
                FindScreen();

                if (CarScreenLogic)
                {
                    CarScreenLogic.ClearDecodeText();
                }
            }

            public static void SetTimeBarText(string text, string hexColor, float time)
            {
                FindScreen();

                if (CarScreenLogic)
                {
                    CarScreenLogic.timeWidget_?.SetTimeTextToString(text, hexColor.ToColor(), time);
                }
            }

            public static void WriteText(string text, float perCharacterInterval, int clearDelayUnits, float displayDelay, bool clearOnEnd, string timeBarText)
            {
                FindScreen();

                if (CarScreenLogic)
                {
                    var formattedForScreen = text.WordWrap(LineLength);

                    for (var i = 0; i < clearDelayUnits; i++)
                    {
                        formattedForScreen += " ";
                    }
                    CarScreenLogic.DecodeText(formattedForScreen, perCharacterInterval, displayDelay, clearOnEnd, timeBarText);
                }
            }

            public static void WriteText(string text, float perCharacterInterval, int clearDelayUnits)
            {
                WriteText(text, perCharacterInterval, clearDelayUnits, 0.0f, true, string.Empty);
            }

            public static void WriteText(string text, string timeBarText)
            {
                WriteText(text, 0.0753f, 10, 0.0f, true, timeBarText);
            }

            public static void WriteText(string text)
            {
                WriteText(text, 0.0753f, 10, 0.0f, true, string.Empty);
            }
        }
    }
}

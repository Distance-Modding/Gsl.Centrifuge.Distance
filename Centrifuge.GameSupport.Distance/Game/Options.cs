using System;
using UnityEngine;
using static GraphicsSettings;
using static ReplaySettings;

namespace Centrifuge.Distance.Game
{
    public static class Options
    {
        public static GameManager Game => G.Sys.GameManager_;

        public static OptionsManager OptionsManager => G.Sys.OptionsManager_;

        public static class Audio
        {
            public static AudioSettings Manager => OptionsManager.Audio_;

            public static class Subtitles
            {
                public static bool Enabled
                {
                    get => Manager.SubtitlesVisible_;
                    set => Manager.SubtitlesVisible_ = value;
                }

                public static SubtitlesFontSize Size
                {
                    get => Manager.SubtitlesFontSize_;
                    set => Manager.SubtitlesFontSize_ = value;
                }
            }

            public static class Announcer
            {
                public static AnnouncerOptions State
                {
                    get => Manager.AnnouncerOptions_;
                    set => Manager.AnnouncerOptions_ = value;
                }

                public static bool Checkpoints
                {
                    get => Manager.AnnouncerCheckpoints_;
                    set => Manager.AnnouncerCheckpoints_ = value;
                }

                public static bool Death
                {
                    get => Manager.AnnouncerDeath_;
                    set => Manager.AnnouncerDeath_ = value;
                }

                public static bool Mode
                {
                    get => Manager.AnnouncerMode_;
                    set => Manager.AnnouncerMode_ = value;
                }
            }

            public static class Volume
            {
                public static float Master
                {
                    get => Manager.MasterVolume_;
                    set => Manager.MasterVolume_ = value;
                }

                public static float Announcer
                {
                    get => Manager.AnnouncerVolume_;
                    set => Manager.AnnouncerVolume_ = value;
                }

                public static float Car
                {
                    get => Manager.CarVolume_;
                    set => Manager.CarVolume_ = value;
                }

                public static float Environment
                {
                    get => Manager.AmbientVolume_;
                    set => Manager.AmbientVolume_ = value;
                }

                public static float Menu
                {
                    get => Manager.MenuVolume_;
                    set => Manager.MenuVolume_ = value;
                }

                public static float Music
                {
                    get => Manager.MusicVolume_;
                    set => Manager.MusicVolume_ = value;
                }

                public static float Obstacle
                {
                    get => Manager.ObstacleVolume_;
                    set => Manager.ObstacleVolume_ = value;
                }
            }
            public static class CustomMusic
            {
                public static bool Enabled
                {
                    get => Manager.EnableCustomMusic_;
                    set => Manager.EnableCustomMusic_ = value;
                }

                public static string Folder
                {
                    get => Manager.CustomMusicPath_;
                    set => Manager.CustomMusicPath_ = value;
                }

                public static bool SearchSubdirectories
                {
                    get => Manager.SearchSubdirectories_;
                    set => Manager.SearchSubdirectories_ = value;
                }

                public static bool Shuffle
                {
                    get => Manager.RandomizeCustomMusic_;
                    set => Manager.RandomizeCustomMusic_ = value;
                }

                public static bool Loop
                {
                    get => Manager.LoopTrackCustomMusic_;
                    set => Manager.LoopTrackCustomMusic_ = value;
                }
            }
        }
        public static class General
        {
            public static GeneralSettings Manager => OptionsManager.General_;

            public static float BoomBoxBloomIntensity
            {
                get => Manager.BoomBoxIntensity_;
                set => Manager.BoomBoxIntensity_ = value;
            }

            public static bool BoomBoxEnabled
            {
                get => Manager.BoomBoxMode_;
                set => Manager.BoomBoxMode_ = value;
            }

            public static float BoomBoxShakeIntensity
            {
                get => Manager.BoomBoxShakeIntensity_;
                set => Manager.BoomBoxShakeIntensity_ = value;
            }

            public static float CabinetLightsBrightness
            {
                get => Manager.CabinetLightsBrightness_;
                set => Manager.CabinetLightsBrightness_ = value;
            }

            public static CarScreenVisualizer CarScreenVisualizer
            {
                get => (CarScreenVisualizer)Manager.CarScreenVisualizer_;
                set => Manager.CarScreenVisualizer_ = (int)value;
            }

            public static bool HolidayFeatures
            {
                get => Manager.HolidayFeatures_;
                set => Manager.HolidayFeatures_ = value;
            }

            public static bool MenuAnimations
            {
                get => Manager.menuAnimations_;
                set => Manager.menuAnimations_ = value;
            }

            public static bool ShowCarTrickText
            {
                get => Manager.ShowCarTrickText_;
                set => Manager.ShowCarTrickText_ = value;
            }

            public static bool ShufflePlaylists
            {
                get => Manager.ShufflePlaylists_;
                set => Manager.ShufflePlaylists_ = value;
            }

            public static bool SpeedrunOutputLog
            {
                get => Manager.SpeedrunOutputLog_;
                set => Manager.SpeedrunOutputLog_ = value;
            }

            public static bool SpeedrunTimer
            {
                get => Manager.SpeedrunTimer_;
                set => Manager.SpeedrunTimer_ = value;
            }

            public static SplitScreenCameraSplit SplitScreenCameraSplit
            {
                get => Manager.SplitScreenCameraSplit_;
                set => Manager.SplitScreenCameraSplit_ = value;
            }

            public static Units Units
            {
                get => Manager.Units_;
                set => Manager.Units_ = value;
            }

            public static bool WorkshopLevelAutoDownloading
            {
                get => Game.IsSteamBuild_ && Manager.WorkshopLevelAutoDownloading_;
                set => Manager.WorkshopLevelAutoDownloading_ = Game.IsSteamBuild_ && value;
            }

            public static bool WorkshopRatingPrivacyMode
            {
                get => Game.IsSteamBuild_ && Manager.WorkshopRatingPrivacyMode_;
                set => Manager.WorkshopRatingPrivacyMode_ = Game.IsSteamBuild_ && value;
            }
        }
        public static class Graphics
        {
            public static GraphicsSettings Manager => OptionsManager.Graphics_;

            public static int AnisotropicFiltering
            {
                get => Manager.AnisotropicFiltering_;
                set => Manager.AnisotropicFiltering_ = value;
            }

            public static AASetting AntiAliasing
            {
                get => Manager.AntiAliasing_;
                set => Manager.AntiAliasing_ = value;
            }

            public static bool Bloom
            {
                get => Manager.Bloom_;
                set => Manager.Bloom_ = value;
            }

            public static float Brightness
            {
                get => Manager.BrightnessValue_;
                set => Manager.BrightnessValue_ = value;
            }

            public static bool MotionBlur
            {
                get => Manager.Version_ < 8 ? Manager.CameraMotionBlur_ : Manager.MotionBlur_;
                set
                {
                    if (Manager.Version_ < 8)
                    {
                        Manager.CameraMotionBlur_ = value;
                    }
                    else
                    {
                        Manager.MotionBlur_ = value;
                    }
                }
            }

            public static bool CarDamage
            {
                get => Manager.CarDamage_;
                set => Manager.CarDamage_ = value;
            }

            public static bool DepthOfField
            {
                get => Manager.DepthOfField_;
                set => Manager.DepthOfField_ = value;
            }

            public static bool DetailedLighting
            {
                get => Manager.DetailedLighting_;
                set => Manager.DetailedLighting_ = value;
            }

            public static int DrawDistance
            {
                get => Manager.DrawDistance_;
                set => Manager.DrawDistance_ = value;
            }

            public static bool FilmGrain
            {
                get => Manager.FilmGrain_;
                set => Manager.FilmGrain_ = value;
            }

            public static bool Fullscreen
            {
                get => Manager.Fullscreen_;
                set => Manager.Fullscreen_ = value;
            }

            public static LetterboxMode LetterBox
            {
                get => (LetterboxMode)Manager.LetterBoxMode_;
                set => Manager.LetterBoxMode_ = (int)value;
            }

            public static bool Particles
            {
                get => Manager.Particles_;
                set => Manager.Particles_ = value;
            }

            public static bool RadialBlur
            {
                get => Manager.RadialBlur_;
                set => Manager.RadialBlur_ = value;
            }

            public static RealtimeReflectionsSetting RealtimeReflections
            {
                get => Manager.RealtimeReflections_;
                set => Manager.RealtimeReflections_ = value;
            }

            public static Vector2 Resolution
            {
                get => new Vector2(Manager.ResolutionWidth_, Manager.ResolutionHeight_);
                set
                {
                    Manager.ResolutionWidth_ = (int)Math.Round(value.x);
                    Manager.ResolutionHeight_ = (int)Math.Round(value.y);
                }
            }

            public static ShadowQuality ShadowQuality
            {
                get => (ShadowQuality)Manager.ShadowQuality_;
                set => Manager.ShadowQuality_ = (int)value;
            }

            public static bool SunShafts
            {
                get => Manager.SunShafts_;
                set => Manager.SunShafts_ = value;
            }

            public static int TextureQuality
            {
                get => Manager.TextureQuality_;
                set => Manager.TextureQuality_ = value;
            }

            public static bool Vignetting
            {
                get => Manager.Vignetting_;
                set => Manager.Vignetting_ = value;
            }

            public static bool VSync
            {
                get => Manager.VSyncCount_ > 0;
                set => Manager.VSyncCount_ = value == true ? 1 : 0;
            }
        }
        public static class Replay
        {
            public static ReplaySettings Manager => OptionsManager.Replay_;

            public static float GhostsBrightness
            {
                get => Manager.GhostBrightness_;
                set => Manager.GhostBrightness_ = value;
            }

            public static int GhostsCount
            {
                get => Manager.GhostsInArcadeCount_;
                set => Manager.GhostsInArcadeCount_ = value;
            }

            public static GhostsInArcade GhostsType
            {
                get => Manager.GhostsInArcadeType_;
                set => Manager.GhostsInArcadeType_ = value;
            }

            public static bool GhostsNamesVisible
            {
                get => Manager.GhostsNamesVisible_;
                set => Manager.GhostsNamesVisible_ = value;
            }

            public static bool PlaybackSpeedAffectsMusic
            {
                get => Manager.PlaybackSpeedAffectsMusic_;
                set => Manager.PlaybackSpeedAffectsMusic_ = value;
            }
        }
    }
}
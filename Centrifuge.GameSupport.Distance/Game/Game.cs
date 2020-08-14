// Original code by Ciastex (http://github.com/ciastex)
// File abailable at https://github.com/Ciastex/Spectrum/blob/5d507db3266f2331eb29feb34754252c5edb6e01/Spectrum.Interop/Game/Game.cs

using System;
using UnityEngine;

namespace Centrifuge.Distance.Game
{
    public class Game
    {
        public static GameModeID CurrentMode => G.Sys.GameManager_.Mode_.GameModeID_;

        public static string LevelName => G.Sys.GameManager_.LevelName_;
        public static string LevelPath => G.Sys.GameManager_.LevelPath_;
        public static string SceneName => GameManager.SceneName_;

        public static string WatermarkText
        {
            get
            {
                var gameObject = GameObject.Find("AlphaVersion");

                if (gameObject == null)
                {
                    return string.Empty;
                }

                var labelComponent = gameObject.GetComponent<UILabel>();
                return labelComponent?.text;
            }

            set
            {
                var gameObject = GameObject.Find("AlphaVersion");

                if (gameObject == null)
                {
                    GameAPI.Instance.Logger.Error("API: Couldn't find AlphaVersion game object.");
                    return;
                }

                var labelComponent = gameObject.GetComponent<UILabel>();

                if (labelComponent != null)
                {
                    labelComponent.text = value;
                }
                else
                {
                    GameAPI.Instance.Logger.Error("API: AlphaVersion game object found, but no UILabel component exists.");
                }
            }
        }

        public static bool ShowWatermark
        {
            get
            {
                var gameObject = GameObject.Find("AlphaVersion");
                if (gameObject == null)
                {
                    return false;
                }

                return gameObject.activeSelf;
            }
            set
            {
                var gameObject = GameObject.Find("AlphaVersion");
                gameObject?.SetActive(value);
            }
        }

        public static void RestartLevel()
        {
            if (G.Sys.GameManager_.IsModeCreated_ && !G.Sys.GameManager_.IsLevelEditorMode_)
            {
                G.Sys.GameManager_.RestartLevel();
            }
        }
    }
}

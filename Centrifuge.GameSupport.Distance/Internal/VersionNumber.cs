using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Centrifuge.Distance.Internal
{
    internal class VersionNumber : MonoBehaviour
    {
        internal const float MaximumOpacity = 0.7f;

        internal static VersionNumber Instance { get; set; } = null;
        internal static bool creatingInstance = false;

        internal UILabel label;
        internal UIWidget widget;
        internal UIPanel panel;

        internal static void Create(GameObject speedrunTimerLogic = null)
        {
            if (!Instance && !creatingInstance)
            {
                creatingInstance = true;
                GameObject alphaVersionAnchorBlueprint = null;

                if (speedrunTimerLogic)
                {
                    alphaVersionAnchorBlueprint = speedrunTimerLogic;
                }
                else
                {
                    alphaVersionAnchorBlueprint = GameObject.Find("Anchor : Speedrun Timer");
                }

                if (alphaVersionAnchorBlueprint)
                {
                    GameObject centrifugeInfoAnchor = Instantiate(alphaVersionAnchorBlueprint, alphaVersionAnchorBlueprint.transform.parent);
                    
                    centrifugeInfoAnchor.SetActive(true);
                    centrifugeInfoAnchor.name = "Anchor : Centrifuge Info";
                    
                    centrifugeInfoAnchor.ForEachChildObjectDepthFirstRecursive((obj) =>
                    {
                        obj.SetActive(true);
                        obj.RemoveComponents<SpeedrunTimerLogic>();
                    });

                    UILabel label = centrifugeInfoAnchor.GetComponentInChildren<UILabel>();

                    Instance = label.gameObject.AddComponent<VersionNumber>();
                }

                creatingInstance = false;
            }
        }

        internal void Start()
        {
            GameObject anchorObject = gameObject.Parent();
            GameObject panelObject = anchorObject.Parent();

            label = GetComponent<UILabel>();
            widget = anchorObject.GetComponent<UIWidget>();
            panel = panelObject.GetComponent<UIPanel>();

            widget.alpha = 0;

            AdjustPosition();
        }

        internal void Update()
        {
            label.text = string.Format(InternalResources.Strings.VersionInfo.Info, Centrifuge, Mods, Gsls);

            Visible = CanDisplay;
        }

        private Coroutine _transitionCoroutine = null;

        private bool _visible = true;
        internal bool Visible
        {
            get => _visible;
            set
            {
                if (value != _visible)
                {
                    if (_transitionCoroutine != null)
                    {
                        StopCoroutine(_transitionCoroutine);
                    }

                    _transitionCoroutine = StartCoroutine(Transition(value));
                }

                _visible = value;
            }
        }

        internal bool CanDisplay
        {
            get
            {
                bool flag = true;
                flag &= string.Equals(SceneManager.GetActiveScene().name, "mainmenu", StringComparison.InvariantCultureIgnoreCase);
                flag &= G.Sys.MenuPanelManager_.panelStack_.Count == 2;
                flag &= GameAPI.Instance.config_.ShowVersionInfo;
                flag &= G.Sys.GameManager_.IsLevelLoaded_;
                flag &= G.Sys.GameManager_.BlackFade_.currentState_ == BlackFadeLogic.FadeState.Idle;
                return flag;
            }
        }

        internal void AdjustPosition()
        {
            label.SetAnchor(panel.gameObject, 21, 19, -19, -17);
            label.pivot = UIWidget.Pivot.TopLeft;
        }

        internal IEnumerator Transition(bool visible, float duration = 0.2f)
        {
            float target = visible ? MaximumOpacity : 0.0f;
            float current = widget.alpha;

            for (float time = 0.0f; time < duration; time += Timex.deltaTime_)
            {
                if (!Game.Options.General.MenuAnimations)
                {
                    break;
                }

                float value = MathUtil.Map(time, 0, duration, current, target);
                widget.alpha = value;

                yield return null;
            }

            widget.alpha = target;

            yield break;
        }

        internal Version ReactorVersion => typeof(Reactor.API.Defaults).Assembly.GetProductVersion();
        
        internal string Centrifuge => string.Format(InternalResources.Strings.VersionInfo.CentrifugeVersion, ReactorVersion.ToString(3));
        
        internal string Mods => string.Format(InternalResources.Strings.VersionInfo.CentrifugeMods, GameAPI.Instance.manager_.GetLoadedMods().Count);
        
        internal string Gsls => string.Format(InternalResources.Strings.VersionInfo.CentrifugeGsls, GameAPI.Instance.manager_.GetLoadedGslIds().Count);
    }
}
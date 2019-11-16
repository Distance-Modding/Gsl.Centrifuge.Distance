using Reactor.API.Attributes;
using Reactor.API.Configuration;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Runtime.Patching;
using Reactor.API.Logging;
using System;
using UnityEngine;

namespace Centrifuge.Distance
{
    [GameSupportLibraryEntryPoint(DistanceGameNamespace)]
    internal sealed class GameAPI : MonoBehaviour
    {
        internal const string DistanceGameNamespace = "com.github.reherc/Centrifuge.Distance";

        internal static IManager Manager;
        internal static Settings Settings { get; set; }
        internal static Log Logger { get; set; }

        public void Initialize(IManager manager)
        {
            DontDestroyOnLoad(gameObject);
            Logger = LogManager.GetForCurrentAssembly();
            Logger.LogLevel = LogLevel.Everything;

            Manager = manager;

            try
            {
                RuntimePatcher.AutoPatch();
                RuntimePatcher.RunTranspilers();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to initialize harmony. Mods will still be loaded, but may not function correctly.");
                Logger.Exception(e);
            }
        }
    }
}

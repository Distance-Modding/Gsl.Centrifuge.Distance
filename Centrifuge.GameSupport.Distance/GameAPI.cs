using Harmony;
using Reactor.API.Attributes;
using Reactor.API.Configuration;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using System;
using System.Reflection;
using UnityEngine;

namespace Centrifuge.Distance
{
    [GameSupportLibraryEntryPoint(DistanceGameNamespace)]
    internal sealed class GameAPI : MonoBehaviour
    {
        internal const string DistanceGameNamespace = "com.github.reherc/Centrifuge.Distance";

        internal static IManager Manager;
        internal static Settings Settings { get; set; }
        internal static Log Log { get; set; }
        private HarmonyInstance HarmonyInstance { get; set; }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Log = new Log("distance_gsl");

            try
            {
                HarmonyInstance = HarmonyInstance.Create(DistanceGameNamespace);
                HarmonyInstance.PatchAll(Assembly.GetAssembly(typeof(GameAPI)));
            }
            catch (Exception e)
            {
                Log.Error("Failed to initialize harmony. Mods will still be loaded, but may not function correctly.");
                Log.Exception(e, true);
            }
        }
    }
}

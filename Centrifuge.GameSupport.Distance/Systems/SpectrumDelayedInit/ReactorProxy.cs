using Reactor.API.Interfaces.Systems;
using Reactor.Extensibility;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Centrifuge.Distance.Systems.SpectrumDelayedInit
{
    internal static class ReactorProxy
    {
        internal static Reactor.Manager Manager => GameAPI.Instance.Manager as Reactor.Manager;

        internal static List<ModHost> Mods => Manager.ModRegistry.Mods;

        internal const string LateInitializeMethod = "LateInitialize";

        public static HashSet<string> CachedLateInitialize = new HashSet<string>();

        internal static void InvokeLateInitialize()
        {
            foreach (ModHost host in Mods)
            {
                if (CachedLateInitialize.Contains(host.ModID))
                {
                    continue;
                }
                else
                {
                    CachedLateInitialize.Add(host.ModID);
                }

                object instance = host.Instance;

                Type type = instance.GetType();

                MethodInfo method = type.GetMethod(LateInitializeMethod, new Type[1] { typeof(IManager) });

                if (method != null)
                {
                    GameAPI.Instance.Logger.Info($"Invoking delayed initialize hook 'LateInitialize' for {host.ModID}...");
                    method.Invoke(instance, new object[1] { Manager });
                }
            }
        }
    }
}

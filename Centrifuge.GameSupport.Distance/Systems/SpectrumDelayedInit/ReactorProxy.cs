using Reactor.API.Interfaces.Systems;
using Reactor.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Centrifuge.Distance.Systems.SpectrumDelayedInit
{
    internal static class ReactorProxy
    {
        internal static Reactor.Manager Manager => GameAPI.Instance.manager_ as Reactor.Manager;

        internal static List<ModHost> Mods => Manager.ModRegistry.Mods;

        internal const string LateInitializeMethod = "LateInitialize";

        internal static void InvokeLateInitialize()
        {
            foreach (ModHost host in Mods)
            {
                object instance = host.Instance;

                Type type = instance.GetType();

                MethodInfo method = type.GetMethod(LateInitializeMethod, new Type[1] { typeof(IManager) });

                if (method != null) 
                {
                    method.Invoke(instance, new object[1] { Manager });
                }
            }
        }
    }
}

using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Centrifuge.Distance.Systems.ExportedTypes.Harmony
{
    [HarmonyPatch(typeof(GUtils), "GetExportedTypesOfType")]
    internal static class GetExportedTypesOfType
    {
        [HarmonyPostfix]
        internal static void Postfix(Type baseType, ref Type[] __result)
        {
            List<Type> types = __result.ToList();

            if (TypeExportManager.Types.Contains(baseType))
            {
                types.AddRange(GetTypesOfType(baseType));
            }

            __result = types.ToArray();
        }

        internal static List<Type> GetTypesOfType(Type baseType)
        {
            List<Type> result = new List<Type>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(baseType) && ValidateType(type))
                    {
                        result.Add(type);
                    }
                }
            }

            return result;
        }

        internal static bool ValidateType(Type type)
        {
            bool flag = true;

            flag &= !type.IsAbstract;
            flag &= !type.IsGenericTypeDefinition;
            flag &= !type.IsInterface;

            return flag;
        }
    }
}

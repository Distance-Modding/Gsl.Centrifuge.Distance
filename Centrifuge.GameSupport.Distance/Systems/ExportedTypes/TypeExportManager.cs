using System;
using System.Collections.Generic;
using System.Reflection;

namespace Centrifuge.Distance.Systems.ExportedTypes
{
    internal static class TypeExportManager
    {
        private static readonly Dictionary<Type, Func<Type, bool>> types_ = new Dictionary<Type, Func<Type, bool>>();

        private static bool enabled_ = true;

        internal static IEnumerable<Type> Types => GetTypes();

        internal static void Register<T>()
        {
            Type type = typeof(T);

            if (!types_.ContainsKey(type))
            {
                types_.Add(type, (_) => true);
            }
        }

        internal static void Register<T>(Func<Type, bool> validator)
        {
            Type type = typeof(T);

            if (!types_.ContainsKey(type))
            {
                types_.Add(type, validator);
            }
        }

        internal static void Unregister<T>()
        {
            Type type = typeof(T);

            types_.Remove(type);
        }

        internal static void UnregisterAll()
        {
            types_.Clear();
        }

        internal static IEnumerable<Type> GetTypes()
        {
            if (enabled_)
            {
                return types_.Keys.CopyToArray();
            }
            else
            {
                return new Type[0];
            }
        }

        internal static bool PerformTypeCheck(Type baseType, Type type)
        {
            Func<Type, bool> validator = (_) => true;

            if (types_.ContainsKey(baseType))
            {
                validator = types_[baseType];
            }

            return validator(type);
        }

        internal static List<Type> GetTypesOfType(Type baseType)
        {
            List<Type> result = new List<Type>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.IsSubclassOf(baseType) && ValidateType(type) && PerformTypeCheck(baseType, type))
                        {
                            result.Add(type);
                        }
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    continue;
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

        internal static void SetState(bool value)
        {
            enabled_ = value;
        }

        internal static bool GetState()
        {
            return enabled_;
        }

        internal static void Enable()
        {
            SetState(true);
        }

        internal static void Disable()
        {
            SetState(false);
        }
    }
}

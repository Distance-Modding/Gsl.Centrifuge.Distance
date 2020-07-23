using System;
using System.Collections.Generic;

namespace Centrifuge.Distance.Systems.ExportedTypes
{
    internal static class TypeExportManager
    {
        private static readonly List<Type> types_ = new List<Type>();

        private static bool enabled_ = true;

        internal static IEnumerable<Type> Types => GetTypes();

        internal static void Register<T>()
        {
            Type type = typeof(T);

            if (!types_.Contains(type))
            {
                types_.Add(type);
            }
        }

        internal static void Unregister<T>()
        {
            Type type = typeof(T);

            types_.RemoveAll((t) => string.Equals(t.FullName, type.FullName, StringComparison.InvariantCultureIgnoreCase));
        }

        internal static void UnregisterAll()
        {
            types_.Clear();
        }

        internal static IEnumerable<Type> GetTypes()
        {
            if (enabled_)
            {
                return types_.CopyToArray();
            }
            else
            {
                return new Type[0];
            }
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

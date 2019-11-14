// Original code by Ciastex (http://github.com/ciastex)
// File abailable at https://github.com/Ciastex/Spectrum/blob/5d507db3266f2331eb29feb34754252c5edb6e01/Spectrum.Interop/Helpers/Reflection.cs

#pragma warning disable IDE0034

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Centrifuge.Distance.Helpers
{
    public class Reflection
    {
        public static MethodInfo GetMethod(object o, string name, bool isStatic)
        {
            var allMethods = new List<MethodInfo>();

            if (isStatic)
            {
                allMethods.AddRange(o.GetType().GetMethods(BindingFlags.Static | BindingFlags.Public));
                allMethods.AddRange(o.GetType().GetMethods(BindingFlags.Static | BindingFlags.NonPublic));
            }
            else
            {
                allMethods.AddRange(o.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public));
                allMethods.AddRange(o.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic));
            }

            foreach (var method in allMethods)
            {
                if (method.Name == name)
                {
                    return method;
                }
            }
            return null;
        }

        public static FieldInfo GetField(object o, string name, bool isStatic)
        {
            var allFields = new List<FieldInfo>();

            if (isStatic)
            {
                allFields.AddRange(o.GetType().GetFields(BindingFlags.Static | BindingFlags.Public));
                allFields.AddRange(o.GetType().GetFields(BindingFlags.Static | BindingFlags.NonPublic));
            }
            else
            {
                allFields.AddRange(o.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public));
                allFields.AddRange(o.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
            }

            foreach (var field in allFields)
            {
                if (field.Name == name)
                {
                    return field;
                }
            }
            return null;
        }

        public static PropertyInfo GetProperty(object o, string name, bool isStatic)
        {
            var allProperties = new List<PropertyInfo>();

            if (isStatic)
            {
                allProperties.AddRange(o.GetType().GetProperties(BindingFlags.Static | BindingFlags.Public));
                allProperties.AddRange(o.GetType().GetProperties(BindingFlags.Static | BindingFlags.NonPublic));
            }
            else
            {
                allProperties.AddRange(o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public));
                allProperties.AddRange(o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic));
            }

            foreach (var property in allProperties)
            {
                if (property.Name == name)
                {
                    return property;
                }
            }
            return null;
        }

        public static T CallMethod<T>(object o, string name, params object[] parameters)
        {
            var method = GetMethod(o, name, true); // try with static first
            if (method != null)
            {
                try
                {

                    return (T)Convert.ChangeType(method.Invoke(o, parameters), typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API: Failed to call static method {name}:\n{ex}");
                    return default(T);
                }
            }

            method = GetMethod(o, name, false); // then with instance method
            if (method != null)
            {
                try
                {
                    return (T)Convert.ChangeType(method.Invoke(o, parameters), typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API: Failed to call instance method {name}:\n{ex}");
                    return default(T);
                }
            }
            Console.WriteLine($"API: Method {name} does not exist in the specified object.");
            return default(T);
        }

        public static T GetFieldValue<T>(object o, string name)
        {
            var field = GetField(o, name, true);
            if (field != null)
            {
                try
                {
                    return (T)Convert.ChangeType(field.GetValue(o), typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API: Failed to get value of static field {name}:\n{ex}");
                    return default(T);
                }
            }

            field = GetField(o, name, false);
            if (field != null)
            {
                try
                {
                    return (T)Convert.ChangeType(field.GetValue(o), typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API: Failed to get value of instance field {name}:\n{ex}");
                    return default(T);
                }
            }
            Console.WriteLine($"API: Field {name} does not exist in the specified object.");
            return default(T);
        }

        public static T GetPropertyValue<T>(object o, string name)
        {
            var property = GetProperty(o, name, true);
            if (property != null)
            {
                try
                {
                    return (T)Convert.ChangeType(property.GetValue(o, null), typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API: Failed to get value of static property {name}:\n{ex}");
                    return default(T);
                }
            }

            property = GetProperty(o, name, false);
            if (property != null)
            {
                try
                {
                    return (T)Convert.ChangeType(property.GetValue(o, null), typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API: Failed to get value of instance property {name}:\n{ex}");
                    return default(T);
                }
            }

            Console.WriteLine($"API: Property {name} does not exist in the specified object.");
            return default(T);
        }

        internal static T GetPrivate<T>(object o, string fieldname)
        {
            var field = o.GetType().GetField(fieldname, BindingFlags.Instance | BindingFlags.NonPublic);
            return (T)field?.GetValue(o);
        }

        internal static void SetPrivate<T>(object o, string fieldname, T value)
        {
            var field = o.GetType().GetField(fieldname, BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(o, value);
        }
    }
}

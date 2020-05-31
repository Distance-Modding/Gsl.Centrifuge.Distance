using System;
using System.Reflection;

// Code from here
// https://www.codeproject.com/Articles/80343/Accessing-private-members

public static class FieldAccess
{
    public static T GetPrivateField<T>(this object obj, string name)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = obj.GetType();
        FieldInfo field = type.GetField(name, flags);
        return (T)field.GetValue(obj);
    }

    public static T GetPrivateProperty<T>(this object obj, string name)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = obj.GetType();
        PropertyInfo field = type.GetProperty(name, flags);
        return (T)field.GetValue(obj, null);
    }

    public static void SetPrivateField(this object obj, string name, object value)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = obj.GetType();
        FieldInfo field = type.GetField(name, flags);
        field.SetValue(obj, value);
    }

    public static void SetPrivateProperty(this object obj, string name, object value)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = obj.GetType();
        PropertyInfo field = type.GetProperty(name, flags);
        field.SetValue(obj, value, null);
    }

    public static T CallPrivateFunction<T>(this object obj, string name, params object[] param)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(name, flags);
        return (T)method.Invoke(obj, param);
    }

    public static void CallPrivateMethod(this object obj, string name, params object[] param)
    {
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(name, flags);
        method.Invoke(obj, param);
    }
}
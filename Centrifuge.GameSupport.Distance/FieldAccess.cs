#pragma warning disable RCS1110
using System;
using System.Reflection;

// Code from here
// https://www.codeproject.com/Articles/80343/Accessing-private-members

public static class FieldAccess
{
    private const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.NonPublic;

    public static T GetPrivateField<T>(this object obj, string name)
    {
        Type type = obj.GetType();
        FieldInfo field = type.GetField(name, FLAGS);
        return (T)field.GetValue(obj);
    }

    public static T GetPrivateProperty<T>(this object obj, string name)
    {
        Type type = obj.GetType();
        PropertyInfo field = type.GetProperty(name, FLAGS);
        return (T)field.GetValue(obj, null);
    }

    public static void SetPrivateField(this object obj, string name, object value)
    {
        Type type = obj.GetType();
        FieldInfo field = type.GetField(name, FLAGS);
        field.SetValue(obj, value);
    }

    public static void SetPrivateProperty(this object obj, string name, object value)
    {
        Type type = obj.GetType();
        PropertyInfo field = type.GetProperty(name, FLAGS);
        field.SetValue(obj, value, null);
    }

    public static T CallPrivateFunction<T>(this object obj, string name, params object[] param)
    {
        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(name, FLAGS);
        return (T)method.Invoke(obj, param);
    }

    public static void CallPrivateMethod(this object obj, string name, params object[] param)
    {
        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(name, FLAGS);
        method.Invoke(obj, param);
    }
}
using System;
public static class EnumExtensions
{
    public static bool HasFlag<T>(this T value, T flag) where T : struct
    {
        var numberA = Convert.ToUInt64(value);
        var numberB = Convert.ToUInt64(flag);

        return (numberA & numberB) != 0;
    }
}

#pragma warning disable RCS1110
using UnityEngine;

public static class UnityEngine__ColorExtensions
{
    public static Color WithOpacity(this Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }

    public static Color WithOpacity(this Color c, int a)
    {
        return c.WithOpacity(a / 255.0f);
    }
}

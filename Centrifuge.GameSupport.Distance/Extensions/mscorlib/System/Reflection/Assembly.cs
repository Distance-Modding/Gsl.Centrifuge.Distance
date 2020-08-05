using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public static class System___Reflection__AssemblyExtensions
{
    public static Version GetFileVersion(this Assembly assembly)
    {
        return new Version(assembly.GetVersionInfo().FileVersion);
    }

    public static string GetProductVersion(this Assembly assembly)
    {
        return assembly.GetVersionInfo().ProductVersion;
    }

    public static FileVersionInfo GetVersionInfo(this Assembly assembly)
    {
        FileInfo assemblyFile = new FileInfo(assembly.Location);
        return FileVersionInfo.GetVersionInfo(assemblyFile.FullName);
    }
}

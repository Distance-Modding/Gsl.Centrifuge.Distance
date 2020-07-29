using System;

namespace Centrifuge.Distance.EditorScripts.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EditorScriptAttribute : Attribute
    {
        public EditorScriptAttribute()
        {
        }
    }
}

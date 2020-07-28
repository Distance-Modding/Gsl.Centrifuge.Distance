using System;

namespace Centrifuge.Distance.Systems.EditorTools.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EditorToolAttribute : Attribute
    {
        public EditorToolAttribute()
        {
        }
    }
}

using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Enums;

namespace Nenn.InspectorEnhancements.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    public class InlinePropertyAttribute : CustomPropertyAttribute
    {
        public string CustomName { get; }
        public bool IsDefault { get; private set; }
        public InlinePropertyNameMode DisplayMode { get; }

        public InlinePropertyAttribute()
        {
            IsDefault = true;
            DisplayMode = InlinePropertyNameMode.None;
        }

        public InlinePropertyAttribute(InlinePropertyNameMode displayMode)
        {
            DisplayMode = displayMode;
        }

        public InlinePropertyAttribute(string customName)
        {
            DisplayMode = InlinePropertyNameMode.HeaderName;
            CustomName = customName;
        }

        public InlinePropertyAttribute(InlinePropertyNameMode displayMode, string customName)
        {
            DisplayMode = displayMode;
            CustomName = customName;
        }

        public InlinePropertyAttribute(string customName, InlinePropertyNameMode displayMode)
        {
            DisplayMode = displayMode;
            CustomName = customName;
        }
    }
}

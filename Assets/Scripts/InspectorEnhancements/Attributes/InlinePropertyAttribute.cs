using System;

namespace InspectorEnhancements
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    public class InlinePropertyAttribute : CustomPropertyAttribute
    {
        public string CustomName { get; }
        public bool IsDefault { get {return isDefault;} private set {isDefault = value;} }
        public InlinePropertyNameMode DisplayMode { get; }

        private bool isDefault = false;

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

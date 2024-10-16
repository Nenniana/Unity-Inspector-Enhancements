using System;

namespace InspectorEnhancements
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class HideLabelAttribute : CustomPropertyAttribute
    {
        
    }
}
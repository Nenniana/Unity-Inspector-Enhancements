using System;
using InspectorEnhancements.Attributes.Base;

namespace InspectorEnhancements.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class HideLabelAttribute : CustomPropertyAttribute
    {
        
    }
}
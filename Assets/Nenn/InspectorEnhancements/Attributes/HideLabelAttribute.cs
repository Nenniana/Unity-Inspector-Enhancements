using System;
using Nenn.InspectorEnhancements.Attributes.Base;

namespace Nenn.InspectorEnhancements.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class HideLabelAttribute : CustomPropertyAttribute
    {
        
    }
}
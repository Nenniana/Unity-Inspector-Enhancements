using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Base;

namespace Nenn.InspectorEnhancements.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class HideLabelAttribute : CustomPropertyAttribute
    {
        
    }
}
using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional.Base;

namespace Nenn.InspectorEnhancements.Runtime.Attributes.Conditional
{
    public class HideIfAttribute : ConditionalAttribute
    {
        public HideIfAttribute() {}
        public HideIfAttribute(string condition) : base(condition)
        {
            MemberName = condition;
            Parameters = Array.Empty<object>();
        }
    
        public HideIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            MemberName = condition;
            Parameters = parameters;
        }
    }
}
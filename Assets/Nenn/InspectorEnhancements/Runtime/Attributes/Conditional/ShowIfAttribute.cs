using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional.Base;

namespace Nenn.InspectorEnhancements.Runtime.Attributes.Conditional
{
    public class ShowIfAttribute : ConditionalAttribute
    {
        public ShowIfAttribute () {}
        public ShowIfAttribute(string condition) : base(condition)
        {
            MemberName = condition;
            Parameters = Array.Empty<object>();
        }
    
        public ShowIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            MemberName = condition;
            Parameters = parameters;
        }
    }
}
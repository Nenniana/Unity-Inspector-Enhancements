using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional.Base;

namespace Nenn.InspectorEnhancements.Runtime.Attributes.Conditional
{
    public class HideIfAttribute : ConditionalAttribute
    {
        public HideIfAttribute() {}
        public HideIfAttribute(string condition) : base(condition)
        {
            MemberName = condition;
            Parameters = new object[0];
        }
    
        public HideIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            MemberName = condition;
            Parameters = parameters;
        }
    }
}
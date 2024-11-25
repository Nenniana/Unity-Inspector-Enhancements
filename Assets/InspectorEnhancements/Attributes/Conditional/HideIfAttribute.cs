using InspectorEnhancements.Attributes.Conditional.Base;

namespace InspectorEnhancements.Attributes.Conditional
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
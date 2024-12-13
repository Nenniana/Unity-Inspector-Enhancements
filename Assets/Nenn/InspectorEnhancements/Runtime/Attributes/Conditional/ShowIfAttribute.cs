using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional.Base;

namespace Nenn.InspectorEnhancements.Runtime.Attributes.Conditional
{
    public class ShowIfAttribute : ConditionalAttribute
    {
        public ShowIfAttribute () {}
        public ShowIfAttribute(string condition) : base(condition)
        {
            MemberName = condition;
            Parameters = new object[0];
        }
    
        public ShowIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            MemberName = condition;
            Parameters = parameters;
        }
    }
}
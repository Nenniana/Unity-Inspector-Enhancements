using Nenn.InspectorEnhancements.Runtime.Attributes.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberOwner;

namespace Nenn.InspectorEnhancements.Runtime.Attributes.Conditional.Base
{
    public abstract class ConditionalAttribute : CustomPropertyAttribute, IMemberOwner
    {
        public string MemberName { get; protected set; }
        public object[] Parameters { get; protected set; }

        public ConditionalAttribute() {}
    
        public ConditionalAttribute(string condition)
        {
            MemberName = condition;
            Parameters = new object[0];
        }
    
        public ConditionalAttribute(string condition, params object[] parameters)
        {
            MemberName = condition;
            Parameters = parameters;
        }
    }
}
using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberOwner;

namespace Nenn.InspectorEnhancements.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CollectionDropdownAttribute : CustomPropertyAttribute, IMemberOwner
    {
        public string MemberName { get; private set; }

        public object[] Parameters { get; private set; }

        public CollectionDropdownAttribute (string condition) {
            MemberName = condition;
        }

        public CollectionDropdownAttribute (string condition, params object[] parameters) {
            MemberName = condition;
            Parameters = parameters;
        }
    }
}
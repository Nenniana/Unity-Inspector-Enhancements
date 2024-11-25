using System;
using InspectorEnhancements.Attributes.Base;
using InspectorEnhancements.Helpers.Interfaces.IMemberOwner;

namespace InspectorEnhancements.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CollectionDropdownAttribute : CustomPropertyAttribute, IMemberOwner
    {
        public string MemberName { get; private set; }

        public object[] Parameters { get; private set; }

        public CollectionDropdownAttribute (string _condition) {
            MemberName = _condition;
        }

        public CollectionDropdownAttribute (string _condition, params object[] _parameters) {
            MemberName = _condition;
            Parameters = _parameters;
        }
    }
}
using System;

namespace InspectorEnhancements
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CollectionDropdownAttribute : CustomPropertyAttribute, IMethodOwner
    {
        public string MethodName { get; private set; }

        public object[] Parameters { get; private set; }

        public CollectionDropdownAttribute (string _condition) {
            MethodName = _condition;
        }

        public CollectionDropdownAttribute (string _condition, params object[] _parameters) {
            MethodName = _condition;
            Parameters = _parameters;
        }
    }
}
using System;

namespace InspectorEnhancements
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MethodButtonAttribute : CustomPropertyAttribute, IMethodOwner
    {
        public string Condition { get; private set; }
        public object[] Parameters { get; private set; }

        public MethodButtonAttribute() {}

        public MethodButtonAttribute (string _condition)
        {
            Condition = _condition;
        }

        public MethodButtonAttribute (string _condition, params object[] _parameters)
        {
            Condition = _condition;
            Parameters = _parameters;
        }
    }
}
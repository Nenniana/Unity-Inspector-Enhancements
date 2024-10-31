using System;

namespace InspectorEnhancements
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodButtonAttribute : CustomPropertyAttribute
    {
        public object[] Parameters { get; private set; }

        public MethodButtonAttribute() {}

        public MethodButtonAttribute (params object[] _parameters)
        {
            Parameters = _parameters;
        }
    }
}
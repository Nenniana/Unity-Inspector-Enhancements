using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IParameterOwner;

namespace Nenn.InspectorEnhancements.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodButtonAttribute : CustomPropertyAttribute, IParameterOwner
    {
        public object[] Parameters { get; private set; }
        public bool ExpandParameters { get; private set; }

        public MethodButtonAttribute(bool _expandParameters = true) 
        {
            ExpandParameters = _expandParameters;
        }

        public MethodButtonAttribute (bool _expandParameters = true, params object[] _parameters)
        {
            ExpandParameters = _expandParameters;
            Parameters = _parameters;
        }

        public MethodButtonAttribute (params object[] _parameters)
        {
            ExpandParameters = true;
            Parameters = _parameters;
        }
    }
}
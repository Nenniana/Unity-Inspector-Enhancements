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

        public MethodButtonAttribute(bool expandParameters = true) 
        {
            ExpandParameters = expandParameters;
        }

        public MethodButtonAttribute (bool expandParameters = true, params object[] parameters)
        {
            ExpandParameters = expandParameters;
            Parameters = parameters;
        }

        public MethodButtonAttribute (params object[] parameters)
        {
            ExpandParameters = true;
            Parameters = parameters;
        }
    }
}
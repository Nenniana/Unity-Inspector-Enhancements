using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.General;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers.IParameterProvider
{
    public class OverwriteableParameterProvider : Base.IParameterProvider
    {
        public object GetOrAdd(string methodName, ParameterInfo parameter, Func<object> valueFactory)
        {
            string key = GetUniqueParameterName(methodName, parameter);
            return OverwriteableStringCache<object>.GetOrAdd(typeof(object), key, valueFactory);
        }

        public object OverwriteOrAdd(string methodName, ParameterInfo parameter, object value)
        {
            string key = GetUniqueParameterName(methodName, parameter);
            return OverwriteableStringCache<object>.OverwriteOrAdd(typeof(object), key, value);
        }

        private static string GetUniqueParameterName(string methodName, ParameterInfo parameter)
        {
            return methodName + "." + parameter.Name;
        }
    }
}
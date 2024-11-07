using System;
using System.Reflection;

namespace InspectorEnhancements
{
    public interface IParameterProvider
    {
        object GetOrAdd(string methodName, ParameterInfo parameter, Func<object> valueFactory);
        object OverwriteOrAdd(string methodName, ParameterInfo parameter, object value);
    }
}
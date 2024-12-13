using System;
using System.Reflection;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers.IParameterProvider.Base
{
    public interface IParameterProvider
    {
        object GetOrAdd(string methodName, ParameterInfo parameter, Func<object> valueFactory);
        object OverwriteOrAdd(string methodName, ParameterInfo parameter, object value);
    }
}
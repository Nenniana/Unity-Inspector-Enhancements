using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IParameterOwner;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers
{
    public class ParameterMethodManager
    {
        private readonly IParameterValueDelegateProvider.Base.IParameterValueDelegateProvider _valueProvider;
        private readonly IParameterProvider.Base.IParameterProvider _parameterProvider;

        public ParameterMethodManager(IParameterValueDelegateProvider.Base.IParameterValueDelegateProvider valueProvider, IParameterProvider.Base.IParameterProvider parameterProvider)
        {
            this._valueProvider = valueProvider;
            this._parameterProvider = parameterProvider;
        }

        public object[] GetParameterValues(MethodInfo method, IParameterOwner attribute, object targetObject)
        {
            // Retrieves parameter values and caches them
            var parameters = method.GetParameters();
            Func<object>[] parameterValues = _valueProvider.GetValueDelegates(method, attribute, targetObject);
            object[] cachedParameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                cachedParameterValues[i] = _parameterProvider.GetOrAdd(method.Name, parameters[i], parameterValues[i]);
            }

            return cachedParameterValues;
        }

        public void CacheParameterValues(string methodName, ParameterInfo[] parameters, object[] parameterValues)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                _parameterProvider.OverwriteOrAdd(methodName, parameters[i], parameterValues[i]);
            }
        }
    }
}
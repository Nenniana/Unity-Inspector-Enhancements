using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IParameterOwner;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers
{
    public class ParameterMethodManager
    {
        private readonly IParameterValueDelegateProvider.Base.IParameterValueDelegateProvider valueProvider;
        private readonly IParameterProvider.Base.IParameterProvider parameterProvider;

        public ParameterMethodManager(IParameterValueDelegateProvider.Base.IParameterValueDelegateProvider valueProvider, IParameterProvider.Base.IParameterProvider parameterProvider)
        {
            this.valueProvider = valueProvider;
            this.parameterProvider = parameterProvider;
        }

        public object[] GetParameterValues(MethodInfo method, IParameterOwner attribute, object targetObject)
        {
            // Retrieves parameter values and caches them
            var parameters = method.GetParameters();
            Func<object>[] parameterValues = valueProvider.GetValueDelegates(method, attribute, targetObject);
            object[] cachedParameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                cachedParameterValues[i] = parameterProvider.GetOrAdd(method.Name, parameters[i], parameterValues[i]);
            }

            return cachedParameterValues;
        }

        public void CacheParameterValues(string methodName, ParameterInfo[] parameters, object[] parameterValues)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterProvider.OverwriteOrAdd(methodName, parameters[i], parameterValues[i]);
            }
        }
    }
}
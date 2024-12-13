using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.General;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IDefaultValueProvider.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IParameterOwner;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers.IParameterValueDelegateProvider 
{
    public class ParameterValueDelegateProvider : Base.IParameterValueDelegateProvider
    {
        private readonly IDefaultValueProvider defaultValueProvider;

        public ParameterValueDelegateProvider(IDefaultValueProvider defaultValueProvider)
        {
            this.defaultValueProvider = defaultValueProvider;
        } 

        public Func<object>[] GetValueDelegates(MethodInfo method, IParameterOwner attribute, object targetObject)
        {
            var parameters = method.GetParameters();
            var parameterValues = new Func<object>[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = ResolveParameterValueDelegate(parameters[i], attribute, targetObject, i);
            }

            return parameterValues;
        }

        private Func<object> ResolveParameterValueDelegate(ParameterInfo parameter, IParameterOwner attribute, object targetObject, int index)
        {
            Func<object> valueDelegate = () => defaultValueProvider.GetDefaultValue(parameter.ParameterType);

            if (parameter.HasDefaultValue)
            {
                valueDelegate = () => parameter.DefaultValue;
            }

            if (attribute.Parameters != null && attribute.Parameters.Length > index && attribute.Parameters[index] != null)
            {
                var providedParameter = attribute.Parameters[index];

                if (parameter.ParameterType == providedParameter.GetType()) 
                {
                    valueDelegate = () => providedParameter;
                }
                
                else if (providedParameter is string parameterName)
                {
                    var fieldInfo = ReflectionHelper.FindMemberInfo<FieldInfo>(targetObject.GetType(), parameterName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

                    if (fieldInfo != null)
                    {
                        valueDelegate = () => fieldInfo.GetValue(targetObject);
                    }
                }
            }

            return valueDelegate;
        }
    }
}
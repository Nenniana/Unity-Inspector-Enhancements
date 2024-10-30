using System;
using System.Reflection;
using UnityEngine;

namespace InspectorEnhancements
{
    public class DefaultMethodInvoker : IMethodInvoker
    {
        private readonly IMemberInfoProvider _memberInfoProvider = new DefaultMemberInfoProvider();

        public object InvokeMethod(object target, object[] passedParameters, MethodInfo methodInfo)
        {
            try
            {
                var parameterValues = BuildParameterValues(target, passedParameters, methodInfo);
                if (parameterValues == null) return null;

                return methodInfo.Invoke(target, parameterValues);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error invoking method '{methodInfo.Name}' on {target.GetType()}: {ex.Message}");
                return null;
            }
        }

        private object[] BuildParameterValues(object target, object[] passedParameters, MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = GetParameterValue(target, passedParameters, parameters[i], i);
                if (parameterValues[i] == null && !parameters[i].HasDefaultValue)
                {
                    Debug.LogWarning($"Missing required parameter '{parameters[i].Name}' for method '{methodInfo.Name}'.");
                    return null;
                }
            }

            return parameterValues;
        }

        private object GetParameterValue(object target, object[] passedParameters, ParameterInfo parameter, int index)
        {
            if (index < passedParameters.Length)
            {
                return GetPassedOrFieldValue(target, passedParameters[index]);
            }
            return parameter.HasDefaultValue ? parameter.DefaultValue : null;
        }

        private object GetPassedOrFieldValue(object target, object passedParam)
        {
            if (passedParam is string fieldName)
            {
                FieldInfo fieldInfo = _memberInfoProvider.TryGetMemberInfo<FieldInfo>(target, fieldName);
                if (fieldInfo == null)
                {
                    Debug.LogWarning($"Field '{fieldName}' not found in {target.GetType()}");
                    return null;
                }
                return fieldInfo.GetValue(target);
            }
            return passedParam;
        }
    }
}
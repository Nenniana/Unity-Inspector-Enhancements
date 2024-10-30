using System;
using System.Reflection;
using UnityEngine;

namespace InspectorEnhancements
{
    public class DefaultMethodInvoker : IMethodInvoker
    {
        private readonly IMemberInfoProvider _memberInfoProvider;

        public DefaultMethodInvoker(IMemberInfoProvider memberInfoProvider)
        {
            _memberInfoProvider = memberInfoProvider ?? throw new ArgumentNullException(nameof(memberInfoProvider));
        }

        public object InvokeMethod(IMethodOwner target, MethodInfo methodInfo)
        {
            try
            {
                var parameterValues = BuildParameterValues(target, methodInfo);
                if (parameterValues == null) return null;

                return methodInfo.Invoke(target, parameterValues);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error invoking method '{methodInfo.Name}' on {target.GetType()}: {ex.Message}");
                return null;
            }
        }

        private object[] BuildParameterValues(IMethodOwner target, MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = GetParameterValue(target, parameters[i], i);
                if (parameterValues[i] == null && !parameters[i].HasDefaultValue)
                {
                    Debug.LogWarning($"Missing required parameter '{parameters[i].Name}' for method '{methodInfo.Name}'.");
                    return null;
                }
            }

            return parameterValues;
        }

        private object GetParameterValue(IMethodOwner target, ParameterInfo parameter, int index)
        {
            if (index < target.Parameters.Length)
            {
                return GetPassedOrFieldValue(target, target.Parameters[index]);
            }
            return parameter.HasDefaultValue ? parameter.DefaultValue : null;
        }

        private object GetPassedOrFieldValue(IMethodOwner target, object passedParam)
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
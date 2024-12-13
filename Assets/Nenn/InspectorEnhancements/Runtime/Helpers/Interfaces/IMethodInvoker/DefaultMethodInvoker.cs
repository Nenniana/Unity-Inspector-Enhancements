using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.General;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker.Base;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker
{
    public class DefaultMethodResolver : IMethodResolver
    {
        private readonly IMemberInfoProvider.Base.IMemberInfoProvider _memberInfoProvider = new CacheMemberInfoProvider();

        public object[] InvokeMethod(object target, object[] passedParameters, MethodInfo methodInfo)
        {
            try
            {
                if (target == null) throw new ArgumentNullException(nameof(target));
                if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
                
                var parameterValues = BuildParameterValues(target, passedParameters, methodInfo);
                if (parameterValues == null)
                {
                    throw new ArgumentNullException(nameof(parameterValues), "Parameter cannot be null.");
                }

                return parameterValues;
            }
            catch (ArgumentNullException ex)
            {
                EditorOnlyLogger.LogWarning($"Warning: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                EditorOnlyLogger.LogError($"Unexpected error in BuildParameterValues: {ex.Message}");
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
                    EditorOnlyLogger.LogWarning($"Missing required parameter '{parameters[i].Name}' for method '{methodInfo.Name}'.");
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
                FieldInfo fieldInfo = _memberInfoProvider.TryGetMemberInfo<FieldInfo>(target.GetType(), fieldName);
                if (fieldInfo == null)
                {
                    EditorOnlyLogger.LogWarning($"Field '{fieldName}' not found in {target.GetType()}");
                    return null;
                }
                return fieldInfo.GetValue(target);
            }
            return passedParam;
        }
    }
}
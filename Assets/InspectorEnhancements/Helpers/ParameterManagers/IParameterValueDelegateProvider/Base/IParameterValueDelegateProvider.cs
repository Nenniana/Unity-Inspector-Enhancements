using System;
using System.Reflection;
using InspectorEnhancements.Helpers.Interfaces.IParameterOwner;

namespace InspectorEnhancements.Helpers.ParameterManagers.IParameterValueDelegateProvider.Base
{
    public interface IParameterValueDelegateProvider
    {
        Func<object>[] GetValueDelegates(MethodInfo method, IParameterOwner attribute, object targetObject);
    }
}
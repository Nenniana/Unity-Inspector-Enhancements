using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IParameterOwner;

namespace Nenn.InspectorEnhancements.Helpers.ParameterManagers.IParameterValueDelegateProvider.Base
{
    public interface IParameterValueDelegateProvider
    {
        Func<object>[] GetValueDelegates(MethodInfo method, IParameterOwner attribute, object targetObject);
    }
}
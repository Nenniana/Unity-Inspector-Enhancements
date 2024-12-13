using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IParameterOwner;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers.IParameterValueDelegateProvider.Base
{
    public interface IParameterValueDelegateProvider
    {
        Func<object>[] GetValueDelegates(MethodInfo method, IParameterOwner attribute, object targetObject);
    }
}
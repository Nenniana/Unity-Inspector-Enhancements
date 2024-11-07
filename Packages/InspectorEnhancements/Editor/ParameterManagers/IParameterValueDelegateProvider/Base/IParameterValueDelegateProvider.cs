using System;
using System.Reflection;

namespace InspectorEnhancements
{
    public interface IParameterValueDelegateProvider
    {
        Func<object>[] GetValueDelegates(MethodInfo method, IParameterOwner attribute, object targetObject);
    }
}
using System.Reflection;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker.Base
{
    public interface IMethodResolver
    {
        object[] InvokeMethod(object target, object[] parameters, MethodInfo methodInfo);
    }
}
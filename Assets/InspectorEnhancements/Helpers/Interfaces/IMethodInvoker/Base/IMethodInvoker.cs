using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMethodResolver
    {
        object[] InvokeMethod(object target, object[] parameters, MethodInfo methodInfo);
    }
}
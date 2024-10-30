using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMethodInvoker
    {
        object InvokeMethod(object target, object[] parameters, MethodInfo methodInfo);
    }
}
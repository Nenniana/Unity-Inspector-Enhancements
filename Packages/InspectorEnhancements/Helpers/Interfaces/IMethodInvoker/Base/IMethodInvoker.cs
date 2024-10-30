using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMethodInvoker
    {
        object InvokeMethod(IMethodOwner target, MethodInfo methodInfo);
    }
}
using System.Reflection;

namespace InspectorEnhancements
{
    public interface ICustomInspectorElement
    {
        bool IsApplicable(MemberInfo member);
        void DrawElement(MemberInfo member, object targetObject);
    }
}
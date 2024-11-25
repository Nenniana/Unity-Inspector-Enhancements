using System.Reflection;

namespace InspectorEnhancements.Helpers.CustomInspectorElements.Base
{
    public interface ICustomInspectorElement
    {
        bool IsApplicable(MemberInfo member);
        void DrawElement(MemberInfo member, object targetObject);
    }
}
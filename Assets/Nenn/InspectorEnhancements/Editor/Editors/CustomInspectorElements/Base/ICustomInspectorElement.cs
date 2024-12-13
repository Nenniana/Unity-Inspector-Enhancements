using System.Reflection;

namespace Nenn.InspectorEnhancements.Editor.Editors.CustomInspectorElements.Base
{
    public interface ICustomInspectorElement
    {
        bool IsApplicable(MemberInfo member);
        void DrawElement(MemberInfo member, object targetObject);
    }
}
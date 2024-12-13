using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers.IMethodRenderer
{
    public class MethodRenderer : Base.IMethodRenderer
    {
        public bool DrawMethodButton(string methodName, bool hasParameters)
        {
            return GUILayout.Button(methodName, GUILayout.ExpandWidth(true));
        }
    }
}
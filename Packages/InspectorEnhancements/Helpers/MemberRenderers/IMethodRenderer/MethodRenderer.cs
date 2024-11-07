using UnityEngine;

namespace InspectorEnhancements
{
    public class MethodRenderer : IMethodRenderer
    {
        public bool DrawMethodButton(string methodName, bool hasParameters)
        {
            return GUILayout.Button(methodName, GUILayout.ExpandWidth(true));
        }
    }
}
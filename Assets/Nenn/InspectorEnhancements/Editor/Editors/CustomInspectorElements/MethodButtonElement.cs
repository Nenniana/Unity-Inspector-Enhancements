using System.Linq;
using System.Reflection;
using Nenn.InspectorEnhancements.Editor.Editors.CustomInspectorElements.Base;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Editors.CustomInspectorElements
{
    public class MethodButtonElement : ICustomInspectorElement
    {
        private readonly ParameterMethodRenderer renderer;
        private readonly ParameterMethodManager parameterManager;

        public MethodButtonElement(ParameterMethodRenderer renderer, ParameterMethodManager parameterManager) 
        {
            this.renderer = renderer;
            this.parameterManager = parameterManager;
        }

        public bool IsApplicable(MemberInfo member)
        {
            return member is MethodInfo method && method.GetCustomAttribute(typeof(MethodButtonAttribute), true) != null;
        }

        public void DrawElement(MemberInfo member, object targetObject)
        {
            var method = (MethodInfo)member;
            var attribute = method.GetCustomAttribute<MethodButtonAttribute>(true);
            var parameterValues = parameterManager.GetParameterValues(method, attribute, targetObject);
            string foldoutKey = $"{targetObject.GetType().FullName}.{method.Name}.Foldout";

            bool hasParameters = method.GetParameters().Any();
            bool parametersFoldedOut = renderer.GetParameterFoldoutState(foldoutKey, attribute.ExpandParameters);

            if (hasParameters) GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            if (renderer.DrawMethodButton(method.Name, hasParameters))
            {
                method.Invoke(targetObject, parameterValues); 
            }

            if (hasParameters)
            {
                renderer.ToggleFoldout(foldoutKey, parametersFoldedOut, "Show");
            }

            GUILayout.EndHorizontal();

            if (parametersFoldedOut && hasParameters)
            {
                
                renderer.DrawParameterFields(method.GetParameters(), ref parameterValues);
                parameterManager.CacheParameterValues(method.Name, method.GetParameters(), parameterValues);
            }

            if (hasParameters) GUILayout.EndVertical();
        }
    }
}

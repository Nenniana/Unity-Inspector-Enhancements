using System.Reflection;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers
{
    public class ParameterMethodRenderer
    {
        private readonly IMethodRenderer.Base.IMethodRenderer methodRenderer;
        private readonly IParameterRenderer.Base.IParameterRenderer parameterRenderer;
        private readonly IFoldoutProvider.Base.IFoldoutProvider foldoutProvider;

        public ParameterMethodRenderer(IMethodRenderer.Base.IMethodRenderer methodRenderer, IParameterRenderer.Base.IParameterRenderer parameterRenderer, IFoldoutProvider.Base.IFoldoutProvider foldoutProvider) 
        {
            this.methodRenderer = methodRenderer;
            this.parameterRenderer = parameterRenderer;
            this.foldoutProvider = foldoutProvider;
        }

        public bool DrawMethodButton(string methodName, bool hasParameters)
        {
            return methodRenderer.DrawMethodButton(methodName, hasParameters);
        }

        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues)
        {
            parameterRenderer.DrawParameterFields(parameters, ref parameterValues);
        }

        public bool ToggleFoldout(string foldoutKey, bool defaultState, string foldoutText)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal(GUILayout.Width(60));
            bool foldoutState = foldoutProvider.ToggleFoldout(foldoutKey, defaultState, foldoutText);;
            GUILayout.EndHorizontal();

            return foldoutState;
        }

        public bool GetParameterFoldoutState(string foldoutKey, bool defaultState)
        {
            return foldoutProvider.GetFoldoutState(foldoutKey, defaultState);
        }
    }
}
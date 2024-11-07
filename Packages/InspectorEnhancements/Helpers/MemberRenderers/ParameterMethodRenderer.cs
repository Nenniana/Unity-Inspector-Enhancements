using System.Reflection;
using UnityEngine;

namespace InspectorEnhancements
{
    public class ParameterMethodRenderer
    {
        private readonly IMethodRenderer methodRenderer;
        private readonly IParameterRenderer parameterRenderer;
        private readonly IFoldoutProvider foldoutProvider;

        public ParameterMethodRenderer(IMethodRenderer methodRenderer, IParameterRenderer parameterRenderer, IFoldoutProvider foldoutProvider) 
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
using System.Reflection;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers
{
    public class ParameterMethodRenderer
    {
        private readonly IMethodRenderer.Base.IMethodRenderer _methodRenderer;
        private readonly IParameterRenderer.Base.IParameterRenderer _parameterRenderer;
        private readonly IFoldoutProvider.Base.IFoldoutProvider _foldoutProvider;

        public ParameterMethodRenderer(IMethodRenderer.Base.IMethodRenderer methodRenderer, IParameterRenderer.Base.IParameterRenderer parameterRenderer, IFoldoutProvider.Base.IFoldoutProvider foldoutProvider) 
        {
            this._methodRenderer = methodRenderer;
            this._parameterRenderer = parameterRenderer;
            this._foldoutProvider = foldoutProvider;
        }

        public bool DrawMethodButton(string methodName, bool hasParameters)
        {
            return _methodRenderer.DrawMethodButton(methodName, hasParameters);
        }

        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues)
        {
            _parameterRenderer.DrawParameterFields(parameters, ref parameterValues);
        }

        public bool ToggleFoldout(string foldoutKey, bool defaultState, string foldoutText)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal(GUILayout.Width(60));
            bool foldoutState = _foldoutProvider.ToggleFoldout(foldoutKey, defaultState, foldoutText);
            GUILayout.EndHorizontal();

            return foldoutState;
        }

        public bool GetParameterFoldoutState(string foldoutKey, bool defaultState)
        {
            return _foldoutProvider.GetFoldoutState(foldoutKey, defaultState);
        }
    }
}
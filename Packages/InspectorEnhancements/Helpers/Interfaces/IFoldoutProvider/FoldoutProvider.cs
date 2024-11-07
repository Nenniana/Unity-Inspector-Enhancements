using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    public class FoldoutProvider : IFoldoutProvider
    {
        public bool GetFoldoutState(string foldoutKey, bool defaultState = false)
        {
            return EditorPrefs.GetBool(foldoutKey, defaultState);
        }

        public bool ToggleFoldout(string foldoutKey, bool defaultState, string foldoutText)
        {
            bool isExpanded = EditorPrefs.GetBool(foldoutKey, defaultState);
            bool newExpanded = GUILayout.Toggle(isExpanded, foldoutText, EditorStyles.foldout);

            if (newExpanded != isExpanded)
            {
                EditorPrefs.SetBool(foldoutKey, newExpanded);
            }

            return newExpanded;
        }
    }
}
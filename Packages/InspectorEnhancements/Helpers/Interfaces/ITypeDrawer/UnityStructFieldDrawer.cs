using System;
using System.Reflection;
using UnityEditor;

namespace InspectorEnhancements
{
    public class UnityStructFieldDrawer : ITypeDrawer
    {
        private readonly EditorGUILayoutMethodProvider editorGUILayoutMethodProvider;

        public UnityStructFieldDrawer (EditorGUILayoutMethodProvider editorGUILayoutMethodProvider)
        {
            this.editorGUILayoutMethodProvider = editorGUILayoutMethodProvider;
        }

        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            MethodInfo method = editorGUILayoutMethodProvider.GetEditorGUILayoutMethod(type);

            if (method != null)
            {
                EditorGUI.BeginDisabledGroup(!isEditable);
                value = method.Invoke(null, new object[] { label, value, null });
                EditorGUI.EndDisabledGroup();
                return true;
            }

            return false;
        }
    }
}
using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Editor.Helpers.EditorGUILayoutMethodResolver;
using UnityEditor;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer
{
    public class UnityStructFieldDrawer : Base.ITypeDrawer
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
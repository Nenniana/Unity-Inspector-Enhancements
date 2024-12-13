using System;
using UnityEditor;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer
{
    public class UnityNativeFieldDrawer : Base.ITypeDrawer
    {
        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            if (!typeof(UnityEngine.Object).IsAssignableFrom(type))
                return false;

            value = EditorGUILayout.ObjectField(label, (UnityEngine.Object)value, type, true);
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.IFieldDrawer
{
    public class BaseFieldDrawer : Base.IFieldDrawer
    {
        private readonly List<ITypeDrawer.Base.ITypeDrawer> _typeDrawers;

        public BaseFieldDrawer(List<ITypeDrawer.Base.ITypeDrawer> typeDrawers)
        {
            this._typeDrawers = typeDrawers ?? throw new ArgumentNullException(nameof(typeDrawers));
        }

        public void DrawField(string fieldName, ref object fieldValue, Type type, bool isEditable)
        {
            foreach (var drawer in _typeDrawers)
            {
                if (drawer.Draw(fieldName, ref fieldValue, type, isEditable))
                    return;
            }

            EditorGUILayout.LabelField(fieldName, $"Unsupported field type: {type.Name}");
        }
    }
}

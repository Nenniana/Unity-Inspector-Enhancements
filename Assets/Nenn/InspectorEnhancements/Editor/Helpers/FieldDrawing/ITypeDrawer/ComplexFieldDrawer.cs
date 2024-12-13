using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider.Base;
using UnityEditor;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer
{
    public class ComplexFieldDrawer : Base.ITypeDrawer
    {
        private readonly Func<IFieldDrawer.Base.IFieldDrawer> _fieldDrawerFactory;
        private readonly IMemberInfoProvider _memberInfoProvider;

        public ComplexFieldDrawer(Func<IFieldDrawer.Base.IFieldDrawer> fieldDrawerFactory, IMemberInfoProvider memberInfoProvider)
        {
            // Use factory delegate to pass IFieldDrawer instance
            this._fieldDrawerFactory = fieldDrawerFactory ?? throw new ArgumentNullException(nameof(fieldDrawerFactory));
            this._memberInfoProvider = memberInfoProvider ?? throw new ArgumentNullException(nameof(memberInfoProvider));
        }

        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            if (!type.IsClass && !type.IsValueType)
                return false;

            if (value == null)
            {
                value = Activator.CreateInstance(type); 
            }

            EditorGUILayout.LabelField(label, type.Name);
            EditorGUI.indentLevel++;

            var fields = _memberInfoProvider.TryGetAllMemberInfo<FieldInfo>(type);
            foreach (var field in fields)
            {
                if (!field.IsPublic || field.IsNotSerialized)
                {
                    continue;
                }

                object fieldValue = field.GetValue(value);
                _fieldDrawerFactory().DrawField(field.Name, ref fieldValue, field.FieldType, !field.IsInitOnly);
                field.SetValue(value, fieldValue);
            }

            EditorGUI.indentLevel--;
            return true;
        }
    }
}
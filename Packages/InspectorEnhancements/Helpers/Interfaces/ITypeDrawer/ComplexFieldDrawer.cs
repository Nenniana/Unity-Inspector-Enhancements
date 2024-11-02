using System;
using System.Reflection;
using UnityEditor;

namespace InspectorEnhancements
{
    public class ComplexFieldDrawer : ITypeDrawer
    {
        private readonly Func<IFieldDrawer> fieldDrawerFactory;
        private readonly IMemberInfoProvider memberInfoProvider;

        public ComplexFieldDrawer(Func<IFieldDrawer> fieldDrawerFactory, IMemberInfoProvider memberInfoProvider)
        {
            // Use factory delegate to pass IFieldDrawer instance
            this.fieldDrawerFactory = fieldDrawerFactory ?? throw new ArgumentNullException(nameof(fieldDrawerFactory));
            this.memberInfoProvider = memberInfoProvider ?? throw new ArgumentNullException(nameof(memberInfoProvider));
        }

        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            if (!type.IsClass && !type.IsValueType)
                return false;

            EditorGUILayout.LabelField(label, type.Name);
            EditorGUI.indentLevel++;

            var fields = memberInfoProvider.TryGetAllMemberInfo<FieldInfo>(type);
            foreach (var field in fields)
            {
                if (!field.IsPublic || field.IsNotSerialized)
                    continue;

                object fieldValue = field.GetValue(value);

                fieldDrawerFactory().DrawField(field.Name, ref fieldValue, field.FieldType, !field.IsInitOnly);
                field.SetValue(value, fieldValue);
            }

            EditorGUI.indentLevel--;
            return true;
        }
    }
}
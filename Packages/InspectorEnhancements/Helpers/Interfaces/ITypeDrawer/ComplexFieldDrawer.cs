using System;
using System.Reflection;
using UnityEditor;

namespace InspectorEnhancements
{
    public class ComplexFieldDrawer : ITypeDrawer
    {
        private readonly IFieldDrawer fieldDrawer;
        private readonly IMemberInfoProvider memberInfoProvider;

        public ComplexFieldDrawer(IFieldDrawer fieldDrawer, IMemberInfoProvider memberInfoProvider)
        {
            this.fieldDrawer = fieldDrawer ?? throw new ArgumentNullException(nameof(fieldDrawer));
            this.memberInfoProvider = memberInfoProvider ?? throw new ArgumentNullException(nameof(memberInfoProvider));
        }

        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            // Ensure the type is complex
            if (!type.IsClass && !type.IsValueType)
                return false;

            EditorGUILayout.LabelField(label, type.Name);
            EditorGUI.indentLevel++;

            // Use member info provider to get fields
            var fields = memberInfoProvider.TryGetAllMemberInfo<FieldInfo>(type);
            foreach (var field in fields)
            {
                if (!field.IsPublic || field.IsNotSerialized)
                    continue;

                object fieldValue = field.GetValue(value);
                fieldDrawer.DrawField(field.Name, ref fieldValue, field.FieldType, !field.IsInitOnly);
                field.SetValue(value, fieldValue);
            }

            EditorGUI.indentLevel--;
            return true;
        }
    }
}
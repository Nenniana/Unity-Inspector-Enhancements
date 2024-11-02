using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace InspectorEnhancements
{
    [CustomEditor(typeof(FieldDrawingTest))]
    public class FieldDrawingTestEditor : Editor
    {
        private IFieldDrawer fieldDrawer;

        public FieldDrawingTestEditor () 
        {
            fieldDrawer = FieldDrawerFactory.CreateDefaultFieldDrawer(new CacheMemberInfoProvider(), new EditorGUILayoutMethodProvider());
        }

        public override void OnInspectorGUI()
        {   
            FieldDrawingTest testBehaviour = (FieldDrawingTest)target;
            EditorGUILayout.LabelField("Custom Field Drawer Test", EditorStyles.boldLabel);

            // Draw each field in the TestBehaviour script
            foreach (var field in testBehaviour.GetType().GetFields())
            {
                object fieldValue = field.GetValue(testBehaviour);
                fieldDrawer.DrawField(field.Name, ref fieldValue, field.FieldType, true);
                field.SetValue(testBehaviour, fieldValue);
            }
        }
    }
}

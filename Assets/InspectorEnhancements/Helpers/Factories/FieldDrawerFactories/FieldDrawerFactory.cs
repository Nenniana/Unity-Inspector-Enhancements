using System.Collections.Generic;

namespace InspectorEnhancements
{
    public static class FieldDrawerFactory
    {
        public static IFieldDrawer CreateDefaultFieldDrawer(
            IMemberInfoProvider memberInfoProvider,
            EditorGUILayoutMethodProvider editorGUILayoutProvider)
        {
            BaseFieldDrawer fieldDrawer = null;

            fieldDrawer = new BaseFieldDrawer(
                new List<ITypeDrawer>
                {
                    new PrimitiveFieldDrawer(),
                    new UnityNativeFieldDrawer(),
                    new UnityStructFieldDrawer(editorGUILayoutProvider),
                    new ComplexFieldDrawer(() => fieldDrawer, memberInfoProvider)
                }
            );

            return fieldDrawer;
        }
    }
}
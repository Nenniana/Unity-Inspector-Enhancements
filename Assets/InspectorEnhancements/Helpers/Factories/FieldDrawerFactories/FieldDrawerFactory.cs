using System.Collections.Generic;
using InspectorEnhancements.Helpers.Interfaces.EditorGUILayoutMethodResolver;
using InspectorEnhancements.Helpers.Interfaces.IFieldDrawer;
using InspectorEnhancements.Helpers.Interfaces.IFieldDrawer.Base;
using InspectorEnhancements.Helpers.Interfaces.IMemberInfoProvider.Base;
using InspectorEnhancements.Helpers.Interfaces.ITypeDrawer;
using InspectorEnhancements.Helpers.Interfaces.ITypeDrawer.Base;

namespace InspectorEnhancements.Helpers.Factories.FieldDrawerFactories
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
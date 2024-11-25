using System.Collections.Generic;
using Nenn.InspectorEnhancements.Helpers.Interfaces.EditorGUILayoutMethodResolver;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IFieldDrawer;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IFieldDrawer.Base;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IMemberInfoProvider.Base;
using Nenn.InspectorEnhancements.Helpers.Interfaces.ITypeDrawer;
using Nenn.InspectorEnhancements.Helpers.Interfaces.ITypeDrawer.Base;

namespace Nenn.InspectorEnhancements.Helpers.Factories.FieldDrawerFactories
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
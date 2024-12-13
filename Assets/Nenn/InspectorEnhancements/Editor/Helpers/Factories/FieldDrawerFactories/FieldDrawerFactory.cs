using System.Collections.Generic;
using Nenn.InspectorEnhancements.Editor.Helpers.EditorGUILayoutMethodResolver;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.IFieldDrawer;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.IFieldDrawer.Base;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider.Base;

namespace Nenn.InspectorEnhancements.Editor.Helpers.Factories.FieldDrawerFactories
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
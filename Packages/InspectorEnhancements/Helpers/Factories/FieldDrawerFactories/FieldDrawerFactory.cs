using System.Collections.Generic;

namespace InspectorEnhancements
{
    public static class FieldDrawerFactory
{
    // Create a default IFieldDrawer with dependencies passed in as parameters
    public static IFieldDrawer CreateDefaultFieldDrawer(
        IMemberInfoProvider memberInfoProvider,
        EditorGUILayoutMethodProvider editorGUILayoutProvider)
    {
        // Declare fieldDrawer variable so we can reference it in the delegate
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

        // Inject the fieldDrawer reference into ComplexFieldDrawer after construction
        return fieldDrawer;
    }
}
}
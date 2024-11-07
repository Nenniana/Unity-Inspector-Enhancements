
namespace InspectorEnhancements
{
    public static class MethodButtonElementFactory
    {
        public static MethodButtonElement CreateDefaultMethodButtonElement()
        {
            var cacheMemberInfoProvider = new CacheMemberInfoProvider();
            var editorGUILayoutMethodProvider = new EditorGUILayoutMethodProvider();
            var fieldDrawer = FieldDrawerFactory.CreateDefaultFieldDrawer(cacheMemberInfoProvider, editorGUILayoutMethodProvider);
            var parameterRenderer = new ParameterRenderer(fieldDrawer);
            var methodRenderer = new MethodRenderer();
            var parameterMethodRenderer = new ParameterMethodRenderer(methodRenderer, parameterRenderer, new FoldoutProvider());
            var defaultValueProvider = new DefaultValueProvider();
            var overwriteableParameterProvider = new OverwriteableParameterProvider();
            var parameterValueDelegateProvider = new ParameterValueDelegateProvider(defaultValueProvider);
            var parameterMethodManager = new ParameterMethodManager(parameterValueDelegateProvider, overwriteableParameterProvider);
            
            return new MethodButtonElement(parameterMethodRenderer, parameterMethodManager);
        }
    }
}
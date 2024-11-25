
using InspectorEnhancements.Helpers.CustomInspectorElements;
using InspectorEnhancements.Helpers.Factories.FieldDrawerFactories;
using InspectorEnhancements.Helpers.Interfaces.EditorGUILayoutMethodResolver;
using InspectorEnhancements.Helpers.Interfaces.IDefaultValueProvider;
using InspectorEnhancements.Helpers.Interfaces.IFoldoutProvider;
using InspectorEnhancements.Helpers.Interfaces.IMemberInfoProvider;
using InspectorEnhancements.Helpers.MemberRenderers;
using InspectorEnhancements.Helpers.MemberRenderers.IMethodRenderer;
using InspectorEnhancements.Helpers.MemberRenderers.IParameterRenderer;
using InspectorEnhancements.Helpers.ParameterManagers;
using InspectorEnhancements.Helpers.ParameterManagers.IParameterProvider;
using InspectorEnhancements.Helpers.ParameterManagers.IParameterValueDelegateProvider;

namespace InspectorEnhancements.Helpers.Factories.MethodButtonElementFactory
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
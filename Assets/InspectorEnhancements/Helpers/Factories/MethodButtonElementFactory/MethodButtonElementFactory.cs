using Nenn.InspectorEnhancements.Helpers.CustomInspectorElements;
using Nenn.InspectorEnhancements.Helpers.Factories.FieldDrawerFactories;
using Nenn.InspectorEnhancements.Helpers.Interfaces.EditorGUILayoutMethodResolver;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IDefaultValueProvider;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IFoldoutProvider;
using Nenn.InspectorEnhancements.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Helpers.MemberRenderers;
using Nenn.InspectorEnhancements.Helpers.MemberRenderers.IMethodRenderer;
using Nenn.InspectorEnhancements.Helpers.MemberRenderers.IParameterRenderer;
using Nenn.InspectorEnhancements.Helpers.ParameterManagers;
using Nenn.InspectorEnhancements.Helpers.ParameterManagers.IParameterProvider;
using Nenn.InspectorEnhancements.Helpers.ParameterManagers.IParameterValueDelegateProvider;

namespace Nenn.InspectorEnhancements.Helpers.Factories.MethodButtonElementFactory
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
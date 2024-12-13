using Nenn.InspectorEnhancements.Editor.Editors.CustomInspectorElements;
using Nenn.InspectorEnhancements.Editor.Helpers.EditorGUILayoutMethodResolver;
using Nenn.InspectorEnhancements.Editor.Helpers.Factories.FieldDrawerFactories;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.IFoldoutProvider;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers.IMethodRenderer;
using Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers.IParameterRenderer;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IDefaultValueProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers;
using Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers.IParameterProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.ParameterManagers.IParameterValueDelegateProvider;

namespace Nenn.InspectorEnhancements.Editor.Helpers.Factories.MethodButtonElementFactory
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
using System.Collections.Generic;
using System.Reflection;
using Nenn.InspectorEnhancements.Editor.Editors.CustomInspectorElements.Base;
using Nenn.InspectorEnhancements.Editor.Helpers.Factories.MethodButtonElementFactory;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider.Base;
using UnityEditor;

namespace Nenn.InspectorEnhancements.Editor.Editors
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class CompositeInspectorEditor : UnityEditor.Editor
    {
        private readonly List<ICustomInspectorElement> customInspectorElements = new List<ICustomInspectorElement>();
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();

        private void OnEnable()
        {
            // Register available custom elements here, injecting dependencies as needed
            customInspectorElements.Add(
                MethodButtonElementFactory.CreateDefaultMethodButtonElement()
            );
        } 

        public override void OnInspectorGUI()
        {
            // Draw the default inspector first
            DrawDefaultInspector();

            DrawCustomElements();
        }

        private void DrawCustomElements()
        {
            var members = memberInfoProvider.TryGetAllMemberInfo<MemberInfo>(target.GetType());
            foreach (var member in members)
            {
                foreach (var element in customInspectorElements)
                {
                    if (element.IsApplicable(member))
                    {
                        element.DrawElement(member, target);
                    }
                }
            }
        }
    }
}

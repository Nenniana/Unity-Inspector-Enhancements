using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace InspectorEnhancements
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class CompositeInspectorEditor : Editor
    {
        private readonly List<ICustomInspectorElement> customInspectorElements = new List<ICustomInspectorElement>();
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();

        private void OnEnable()
        {
            // Register available custom elements here, injecting dependencies as needed
            customInspectorElements.Add(new MethodButtonElement(
                new DefaultValueProvider(), 
                new OverwriteableParameterProvider(), 
                FieldDrawerFactory.CreateDefaultFieldDrawer(new CacheMemberInfoProvider(), new EditorGUILayoutMethodProvider())
            ));
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

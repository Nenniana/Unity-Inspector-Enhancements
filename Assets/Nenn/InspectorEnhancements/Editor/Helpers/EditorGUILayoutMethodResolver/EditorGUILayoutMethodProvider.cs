using System;
using System.Collections.Generic;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider.Base;
using UnityEditor;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Helpers.EditorGUILayoutMethodResolver
{
    public class EditorGUILayoutMethodProvider
    {
        private readonly IMemberInfoProvider _memberInfoProvider;
        private static readonly Dictionary<string, MethodInfo> CachedMethods = new Dictionary<string, MethodInfo>();

        public EditorGUILayoutMethodProvider() {
            _memberInfoProvider = new CacheMemberInfoProvider();
            
            if (CachedMethods.Count == 0)
            {
                InitializeCache();
            }
        }

        // Inject IMemberInfoProvider to retrieve general member information when needed
        public EditorGUILayoutMethodProvider(IMemberInfoProvider memberInfoProvider)
        {
            _memberInfoProvider = memberInfoProvider ?? throw new ArgumentNullException(nameof(memberInfoProvider));

            // Initialize cache only if the dictionary is empty
            if (CachedMethods.Count == 0)
            {
                InitializeCache();
            }
        }

        // Public method for fetching a specific EditorGUILayout method based on field type
        public MethodInfo GetEditorGUILayoutMethod(Type fieldType)
        {
            // Construct the method name based on the field type
            string methodName = $"{fieldType.Name}Field";

            // Attempt to retrieve from the cache directly
            CachedMethods.TryGetValue(methodName, out var method);
            return method;
        }

        // This method retrieves and caches all relevant EditorGUILayout methods in one go
        private void InitializeCache()
        {
            var methods = _memberInfoProvider.TryGetAllMemberInfo<MethodInfo>(
                typeof(EditorGUILayout), BindingFlags.Public | BindingFlags.Static);

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                if (parameters.Length == 3 &&
                    parameters[0].ParameterType == typeof(string) &&
                    parameters[2].ParameterType == typeof(GUILayoutOption[]))
                {
                    var fieldType = parameters[1].ParameterType;
                    var methodName = $"{fieldType.Name}Field";

                    // Cache each method with its name if not already cached
                    CachedMethods.TryAdd(methodName, method);
                }
            }
        }
    }
}

using System;
using System.Reflection;

namespace InspectorEnhancements
{
    public static class ReflectionHelper
    {
        // Find a method by name in the target object with appropriate binding flags
        public static MethodInfo FindMethod(object target, string methodName)
        {
            if (target == null) 
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(methodName)) 
                throw new ArgumentException("Method name cannot be null or empty", nameof(methodName));

            var type = target.GetType();
            var method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            return method;
        }

        // Find a field by name in the target object
        public static FieldInfo FindField(object target, string fieldName)
        {
            if (target == null) 
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(fieldName)) 
                throw new ArgumentException("Field name cannot be null or empty", nameof(fieldName));

            var type = target.GetType();
            var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            return field;
        }

        // Find a property by name in the target object
        public static PropertyInfo FindProperty(object target, string propertyName)
        {
            if (target == null) 
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(propertyName)) 
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            var type = target.GetType();
            var property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            return property;
        }
    }
}

using System;

namespace InspectorEnhancements
{
    public class DefaultValueProvider : IDefaultValueProvider
    {
        public object GetDefaultValue(Type type)
        {
            // Use reflection to invoke a generic method that returns default(T)
            return typeof(DefaultValueHelper)
                .GetMethod(nameof(DefaultValueHelper.GetDefault))
                .MakeGenericMethod(type)
                .Invoke(null, null);
        }

        private static class DefaultValueHelper
        {
            public static T GetDefault<T>() => default;
        }
    }
}
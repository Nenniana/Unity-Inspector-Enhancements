using System;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IDefaultValueProvider
{
    public class DefaultValueProvider : Base.IDefaultValueProvider
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
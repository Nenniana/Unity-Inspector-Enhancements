using System;

namespace InspectorEnhancements
{
    public interface IDefaultValueProvider
    {
        object GetDefaultValue(Type type);
    }
}
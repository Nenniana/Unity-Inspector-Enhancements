using System;

namespace InspectorEnhancements.Helpers.Interfaces.IDefaultValueProvider.Base
{
    public interface IDefaultValueProvider
    {
        object GetDefaultValue(Type type);
    }
}
using System;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IDefaultValueProvider.Base
{
    public interface IDefaultValueProvider
    {
        object GetDefaultValue(Type type);
    }
}
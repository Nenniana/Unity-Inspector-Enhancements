using System;

namespace InspectorEnhancements.Helpers.Interfaces.ITypeDrawer.Base
{
    public interface ITypeDrawer
    {
        bool Draw(string label, ref object value, Type type, bool isEditable);
    }
}
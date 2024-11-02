using System;

namespace InspectorEnhancements
{
    public interface ITypeDrawer
    {
        bool Draw(string label, ref object value, Type type, bool isEditable);
    }
}
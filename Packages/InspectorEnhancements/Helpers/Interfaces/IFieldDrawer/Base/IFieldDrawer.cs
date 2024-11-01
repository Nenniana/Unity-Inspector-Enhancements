using System;

namespace InspectorEnhancements
{
    public interface IFieldDrawer
    {
        void DrawField(string fieldName, ref object fieldValue, Type type, bool isEditable);
    }
}
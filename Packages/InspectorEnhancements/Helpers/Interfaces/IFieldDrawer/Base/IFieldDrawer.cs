using System;

namespace InspectorEnhancements
{
    public interface IFieldDrawer
    {
        void DrawField(string fieldName, object fieldValue, Type type, bool isEditable);
    }
}
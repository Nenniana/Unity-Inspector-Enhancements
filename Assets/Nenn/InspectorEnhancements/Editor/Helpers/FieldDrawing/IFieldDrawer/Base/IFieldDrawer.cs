using System;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.IFieldDrawer.Base
{
    public interface IFieldDrawer
    {
        void DrawField(string fieldName, ref object fieldValue, Type type, bool isEditable);
    }
}
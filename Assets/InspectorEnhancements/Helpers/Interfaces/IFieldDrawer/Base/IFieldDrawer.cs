using System;

namespace Nenn.InspectorEnhancements.Helpers.Interfaces.IFieldDrawer.Base
{
    public interface IFieldDrawer
    {
        void DrawField(string fieldName, ref object fieldValue, Type type, bool isEditable);
    }
}
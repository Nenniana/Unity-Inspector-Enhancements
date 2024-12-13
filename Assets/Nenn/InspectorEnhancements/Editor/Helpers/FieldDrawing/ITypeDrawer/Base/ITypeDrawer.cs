using System;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer.Base
{
    public interface ITypeDrawer
    {
        bool Draw(string label, ref object value, Type type, bool isEditable);
    }
}
using System;

namespace InspectorEnhancements
{
    public abstract class BaseFieldDrawer : IFieldDrawer
    {
        public virtual void DrawField(string fieldName, object fieldValue, Type type, bool isEditable)
        {
            throw new NotImplementedException();
        }
    }
}
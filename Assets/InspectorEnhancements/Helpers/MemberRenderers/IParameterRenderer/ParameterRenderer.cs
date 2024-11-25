using System.Reflection;
using InspectorEnhancements.Helpers.Interfaces.IFieldDrawer.Base;

namespace InspectorEnhancements.Helpers.MemberRenderers.IParameterRenderer
{
    public class ParameterRenderer : Base.IParameterRenderer
    {
        private readonly IFieldDrawer fieldDrawer;

        public ParameterRenderer(IFieldDrawer fieldDrawer) 
        {
            this.fieldDrawer = fieldDrawer;
        }

        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                fieldDrawer.DrawField(parameter.Name, ref parameterValues[i], parameter.ParameterType, true);
            }
        }
    }
}
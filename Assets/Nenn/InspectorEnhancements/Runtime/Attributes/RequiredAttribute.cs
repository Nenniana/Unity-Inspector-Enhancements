using System;
using Nenn.InspectorEnhancements.Runtime.Attributes.Base;

namespace Nenn.InspectorEnhancements.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredAttribute : CustomPropertyAttribute 
    {
        public string ErrorMessage;
    
        public RequiredAttribute(string errorMessage = "Below field is required.") 
        {
            ErrorMessage = errorMessage;
        }
    }
}
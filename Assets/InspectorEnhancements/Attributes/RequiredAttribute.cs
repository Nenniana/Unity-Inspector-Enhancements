using System;
using InspectorEnhancements.Attributes.Base;

namespace InspectorEnhancements.Attributes
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
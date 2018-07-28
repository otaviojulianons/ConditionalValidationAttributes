using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Attributes
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyCondition { get; set; }

        private object _valueExpected;
        public object ValueExpected {
            get {
                return _valueExpected;
            }
            set {
                _valueExpected = value;
                _needValidadeValueExpected = true;
            }
        }

        private bool _needValidadeValueExpected;

        public RequiredIfAttribute(string nameOfPropertyCondition)
        {
            PropertyCondition = nameOfPropertyCondition;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.PropertyCondition);
            if (property == null)
                return new ValidationResult($"Property {PropertyCondition} not found.");

            var valueCondition = property.GetValue(validationContext.ObjectInstance);
            var needValidateValue = false;
            if (_needValidadeValueExpected)
            {
                needValidateValue = valueCondition != null ? 
                    valueCondition.Equals(ValueExpected) : valueCondition == ValueExpected;
            }
            else
                needValidateValue = new RequiredAttribute().IsValid(valueCondition);                   

            if(needValidateValue && !new RequiredAttribute().IsValid(value))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
            
        }
    }
}

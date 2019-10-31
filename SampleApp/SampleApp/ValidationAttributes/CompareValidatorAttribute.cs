using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace SampleApp.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CompareValidatorAttribute : ValidationAttribute
    {
        public CompareValidatorAttribute(string otherProperty)
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException(nameof(otherProperty));
            }
            OtherProperty = otherProperty;
        }
        public string OtherProperty { get; private set; }
        public string OtherPropertyDisplayName { get; internal set; }

        //public override bool IsValid(object value)
        //{
        //    return false;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, ErrorMessage, OtherProperty));
            }
            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            string[] membernames = { validationContext.MemberName };
            if (!Equals(value, otherPropertyValue))
            {
                //if (OtherPropertyDisplayName == null)
                //{
                //    OtherPropertyDisplayName = GetDisplayNameForProperty(validationContext.ObjectType, OtherProperty);
                //}

                return new ValidationResult(
                    FormatErrorMessage(validationContext.DisplayName), membernames);
            }
            return ValidationResult.Success;

        }



        public override bool RequiresValidationContext
        {
            get
            {
                return true;
            }
        }
    }
}

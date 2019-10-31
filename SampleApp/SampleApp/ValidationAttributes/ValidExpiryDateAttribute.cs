using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SampleApp.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ValidExpiryDateAttribute : ValidationAttribute
    {
        public string Format { get; set; }
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (string.IsNullOrWhiteSpace(value?.ToString())) return false;

            DateTime result;
            var isvalid = DateTime.TryParseExact(value.ToString(), Format, new CultureInfo("en-US"), DateTimeStyles.None, out result);
            return isvalid;
        }

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    if (value == null) return new ValidationResult(ErrorMessage);
        //    if (string.IsNullOrWhiteSpace(value?.ToString())) new ValidationResult(ErrorMessage);

        //    DateTime result;
        //    var isvalid = DateTime.TryParseExact(value.ToString(), Format, new CultureInfo("en-US"), DateTimeStyles.None, out result);
        //    return isvalid ? ValidationResult.Success : new ValidationResult(errorMessage: ErrorMessage);
        //}
    }
}

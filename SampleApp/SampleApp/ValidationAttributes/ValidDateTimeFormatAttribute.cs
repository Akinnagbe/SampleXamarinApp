using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace SampleApp.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ValidDateTimeFormatAttribute : ValidationAttribute
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
    }
}

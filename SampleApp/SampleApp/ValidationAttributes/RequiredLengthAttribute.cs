using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleApp.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]

    public class RequiredLengthAttribute : ValidationAttribute
    {
        public RequiredLengthAttribute(int length)
        {
            Length = length;
        }
        public int Length { get; private set; }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var isvalid = value?.ToString().Length == Length;
            return isvalid;
        }
    }
}

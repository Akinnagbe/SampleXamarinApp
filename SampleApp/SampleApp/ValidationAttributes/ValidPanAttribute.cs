using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SampleApp.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ValidPanAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (Regex.IsMatch(value.ToString(), @"(\b34|37)\d{13}$")
                || Regex.IsMatch(value.ToString(), @"\b4\d{15}$")
                || Regex.IsMatch(value.ToString(), @"\b5[1-5]\d{14}$")
                || Regex.IsMatch(value.ToString(), @"((\b506(099|100))|(\b5061[0-9][0-8])|(\b6500[0-2][2-7]))\d{10,13}$"))
            {
                return true;
            }

            //if (Regex.IsMatch(value.ToString(), @"\b4\d{15}$"))
            //{
            //    return true;
            //}
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SampleApp.Controls
{
    public class SecurityQuestionLayout : StackLayout
    {
        public static readonly BindableProperty IsQuestionCompletedProperty = BindableProperty.Create("IsQuestionCompleted", typeof(bool), typeof(SecurityQuestionLayout), null);

        public bool IsQuestionCompleted
        {
            get { return (bool)GetValue(IsQuestionCompletedProperty); }
            set { SetValue(IsQuestionCompletedProperty, value); }
        }
    }
}

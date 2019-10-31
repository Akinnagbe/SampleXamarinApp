using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Models.SecurityQuestion
{
    public class GetSecurityQuestion : MvvmHelpers.ObservableObject
    {
        public int Id { get; set; }
        public string Question { get; set; }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private string _answer = "";
        public string Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }
    }
}

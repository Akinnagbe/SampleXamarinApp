using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SampleApp.ViewModels.Base
{
    public class ValidatableObject : INotifyDataErrorInfo, INotifyPropertyChanged
    {

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value, nameof(Errors));
        }

        public bool HasErrors => Errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);


        private bool _isModelValid = default(bool);

        public bool IsModelValid
        {
            get => _isModelValid;
            set => SetProperty(ref _isModelValid, value);
        }




        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(Errors));


        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (Errors.ContainsKey(propertyName) && (Errors[propertyName] != null) && Errors[propertyName].Count > 0)
                    return Errors[propertyName].ToList();
                else
                    return null;
            }
            else
                return Errors.SelectMany(err => err.Value.ToList());
        }

        public void ValidateProperty(object value, [CallerMemberName]string propertyName = null)
        {
            var validationContext = new ValidationContext(this, null, null);
            validationContext.MemberName = propertyName;
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);
            //clear previous errors from property
            if (Errors.ContainsKey(propertyName))
            {
                Errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }

            HandleValidationResults(validationResults);
            ValidateModel();

        }

        private void HandleValidationResults(List<ValidationResult> validationResults)
        {
            //Group validation results by property names
            var resultsByPropNames = from res in validationResults
                                     from mname in res.MemberNames
                                     group res by mname into g
                                     select g;

            //add errors to dictionary and inform  binding engine about errors
            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                Errors.Add(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }
        }

        public void ValidateModel()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);

            //clear all previous errors
            var propNames = Errors.Keys.ToList();
            Errors.Clear();
            propNames.ForEach(pn => OnErrorsChanged(pn));

            HandleValidationResults(validationResults);
            IsModelValid = !HasErrors;
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
                   [CallerMemberName]string propertyName = "",
                   Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}

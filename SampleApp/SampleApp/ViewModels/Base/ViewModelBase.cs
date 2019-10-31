
using Microsoft.AppCenter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleApp.Enums;
using SampleApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleApp.ViewModels.Base
{
    public class ViewModelBase : MvvmHelpers.BaseViewModel, INotifyDataErrorInfo
    {
        public ViewModelBase()
        {
            Countries = new List<Country>();

        }
        #region Validation
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value, nameof(Errors)); }
        }

        public bool HasErrors => Errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);

        private bool _isModelValid = default(bool);
        public bool IsModelValid
        {
            get => _isModelValid;
            set => SetProperty(ref _isModelValid, value);
        }

        //public bool IsNotLocalCurrency { get; set; } = Settings.SelectedWallet == null ? false : !string.Equals(Settings.SelectedWallet.Currency, CurrencyCode.NGN.ToString());

        //private BillingInformation _billingInformation = new BillingInformation();
        //public BillingInformation BillingInformation
        //{
        //    get { return _billingInformation; }
        //    set { SetProperty(ref _billingInformation, value); }
        //}

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        //public void SaveLoginDetails(JObject jObject, string username)
        //{
        //    Settings.AccessToken = jObject.Value<string>("access_token");
        //    Settings.AccessTokenIssuedAt = jObject.Value<DateTime>(".issued");
        //    Settings.AccessTokenExpiresAt = jObject.Value<DateTime>(".expires");
        //    Settings.CustomerId = jObject.Value<string>("customerId");
        //    Settings.UserId = int.Parse(jObject.Value<string>("userId"));
        //    Settings.Username = username;

        //    Settings.Firstname = jObject.Value<string>("firstname");
        //    Settings.Lastname = jObject.Value<string>("lastname");
        //    Settings.Phonenumber = jObject.Value<string>("phonenumber");
        //    Settings.Residence = jObject.Value<string>("country");
        //    Settings.Bvn = jObject.Value<string>("bvn");

        //    App.Current.MainPage = new DashboardView();

        //    //Send Notification for Subscriptions
        //    MessagingCenter.Send((App)Application.Current, Subscriptions.loadcurrency);
        //    MessagingCenter.Send((App)Application.Current, Subscriptions.loaddebitcards, Settings.UserId);
        //    MessagingCenter.Send((App)Application.Current, Subscriptions.loadwalletbalance, Settings.CustomerId);

        //    if (!string.IsNullOrWhiteSpace(username))
        //        AppCenter.SetUserId(username);
        //}

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(Errors));
            IsModelValid = Errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);
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
            Validator.TryValidateObject(this, validationContext, validationResults);

            //clear all previous errors
            var propNames = Errors.Keys.ToList();
            Errors.Clear();
            propNames.ForEach(pn => OnErrorsChanged(pn));

            HandleValidationResults(validationResults);
        }
        #endregion

        public List<Country> Countries { get; set; }
        private string _primaryCurrency = CurrencyCode.NGN.ToString();
        public string PrimaryCurrency
        {
            get { return _primaryCurrency; }
            set { SetProperty(ref _primaryCurrency, value); }
        }


        public virtual Task InitializeAsync()
        {
            #region Countries
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ViewModelBase)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("SampleApp.countries.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
                Countries = JsonConvert.DeserializeObject<List<Country>>(text);
            }
            #endregion

            return Task.FromResult(false);
        }


        //private CurrencyCode _currencyCode = default(CurrencyCode);
        //public CurrencyCode CurrencyCode
        //{
        //    get { return _currencyCode; }
        //    set { SetProperty(ref _currencyCode, value); }
        //}

        private decimal _currentBalance = default(decimal);
        public decimal CurrentBalance
        {
            get { return _currentBalance; }
            set { SetProperty(ref _currentBalance, value); }
        }


        public bool HasInternet()
        {
            return (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet);
        }


        private bool showErrorMessage = false;
        public bool ShowErrorMessage
        {
            get { return showErrorMessage; }
            set { SetProperty(ref showErrorMessage, value); }
        }


        private string notificationMessage = default(string);
        public string NotificationMessage
        {
            get { return notificationMessage; }
            set { SetProperty(ref notificationMessage, value); }
        }

        private string _buttonText = "Next";
        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        private string _pin = string.Empty;
        public string Pin
        {
            get { return _pin; }
            set { SetProperty(ref _pin, value); }
        }

        private bool _isPinSet = false;
        public bool IsPinSet
        {
            get { return _isPinSet; }
            set { SetProperty(ref _isPinSet, value); }
        }

        private Color _notificationColor = Color.FromHex("#FFE77E74");
        public Color NotificationColor
        {
            get { return _notificationColor; }
            set { SetProperty(ref _notificationColor, value); }
        }

        private string _notificationGlyph = "\uE888";
        public string NotificationGlyph
        {
            get { return _notificationGlyph; }
            set { SetProperty(ref _notificationGlyph, value); }
        }

        private FormattedString formattedString = default(FormattedString);
        public FormattedString FormattedString
        {
            get { return formattedString; }
            set { SetProperty(ref formattedString, value); }
        }

        public string ReferenceId { get; set; }
    }
}

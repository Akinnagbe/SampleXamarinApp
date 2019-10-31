
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using SampleApp.Models.SecurityQuestion;
using SampleApp.ValidationAttributes;
using SampleApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
namespace SampleApp.ViewModels
{
    public class SampleViewModel : Base.ViewModelBase
    {
        // public string Name { get; set; } = "Olamide";

        public SampleViewModel()
        {

            InitializeAsync();
            Step1 = new OnboardingStep1();
            Step2 = new OnboardingStep2();
            Step3 = new OnboardingStep3();
            Step4 = new OnboardingStep4();
            Step5 = new OnboardingStep5();
            SecurityQuestions = new ObservableCollection<GetSecurityQuestion>();
            SecurityQuestionAnswers = new List<SecurityQuestionAnswer>();


            SubmitCommand = new Command(async () =>
            {
                IsBusy = true;
                ButtonText = "Creating Account...";
                var result = await SubmitAsync();
                if (!result.Item1)
                {
                    // DependencyService.Get<IToastMessage>().ShowToast(result.Item2);
                    NotificationMessage = result.Item2;
                    ShowErrorMessage = true;
                }
                else
                {
                    //SignIn


                  //  App.Current.MainPage = new MessageView();
                }
                IsBusy = false;
                ButtonText = "Complete Setup";



            });
        }

        #region Commands
        public ICommand SubmitCommand { get; set; }
        #endregion


        public ObservableCollection<GetSecurityQuestion> SecurityQuestions { get; set; }

        public List<SecurityQuestionAnswer> SecurityQuestionAnswers { get; set; }
        public OnboardingStep1 Step1 { get; set; }
        public OnboardingStep2 Step2 { get; set; }
        public OnboardingStep3 Step3 { get; set; }
        public OnboardingStep4 Step4 { get; set; }
        public OnboardingStep5 Step5 { get; set; }

        private int _currentIndex = 0;
        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        private bool _isQuestionComplete = false;
        public bool IsQuestionComplete
        {
            get => _isQuestionComplete;
            set => SetProperty(ref _isQuestionComplete, value);
        }



        #region Events
        public async Task<Tuple<bool, string>> SubmitAsync()
        {
            //try
            //{
            //    if (!HasInternet())
            //    {
            //        return new Tuple<bool, string>(false, "No Internet Connection");
            //    }
            //    bool status = false;
            //    string errorMessage = string.Empty;
            //    SecurityQuestionAnswers.Clear();
            //    SecurityQuestionAnswers = this.SecurityQuestions.Where(s => s.IsSelected).Select(q => new SecurityQuestionAnswer { Answer = q.Answer, QuestionId = q.Id }).ToList();
            //    if (SecurityQuestionAnswers.Any(s => string.IsNullOrWhiteSpace(s.Answer)))
            //    {
            //        return new Tuple<bool, string>(false, "Please answer selected security questions");
            //    }

            //    var mapper = App.MapperConfiguration.CreateMapper();
            //    var signUp = mapper.Map<SignUpModel>(this);
            //    //  var kk = JsonConvert.SerializeObject(signUp);
            //    var signUpService = RestService.For<ISignUp>(Constants.FxMallamApi);
            //    var register = await signUpService.Register(signUp);
            //    //var content = await register.Content.ReadAsStringAsync();
            //    //var jObject = JObject.Parse(content);
            //    status = register.Status;// jObject.Value<bool>("Status");
            //    if (!register.Status)
            //    {
            //        errorMessage = register.Message;
            //    }
            //    else
            //    {
            //        AppCenter.SetUserId(signUp.EmailAddress);
            //    }

            //    return new Tuple<bool, string>(status, errorMessage);

            //}
            //catch (ApiException ee)
            //{
            //    Crashes.TrackError(ee);
            //    if (ee.StatusCode == System.Net.HttpStatusCode.BadRequest)
            //    {
            //        string error = await ee.GetExceptionAsync();
            //        return new Tuple<bool, string>(false, error);
            //    }
            //    else
            //    {
            //        return new Tuple<bool, string>(false, ee.Content);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Crashes.TrackError(ex);
                return new Tuple<bool, string>(false, "An error occurred");
            //}
        }

        public async Task<Tuple<bool, string>> GetOtp()
        {
            //try
            //{
            //    if (!HasInternet())
            //    {
            //        return new Tuple<bool, string>(false, "No Internet Connection");
            //    }

            //    IsBusy = true;
            //    ShowErrorMessage = false;
            //    ButtonText = "Sending OTP...";
            //    var signUpService = RestService.For<ISignUp>(new HttpClient(new HttpClientHandlerException()) { BaseAddress = new Uri(Constants.FxMallamApi) });

            //    // string telephone = $"{Step4.CountryCode}{Step4.Phonenumber}";
            //    SendOtpModel otpModel = new SendOtpModel
            //    {
            //        Email = Step3.Email,
            //        Phonenumber = Step3.Phonenumber,
            //        Name = Step2.Firstname,
            //        Purpose = "Verify phonenumber"
            //    };
            //    var register = await signUpService.SendOtp(otpModel);
            //    return new Tuple<bool, string>(register.IsSuccessStatusCode, "");
            //}
            //catch (Exception ex)
            //{
            //    Crashes.TrackError(ex);
              return new Tuple<bool, string>(false, "");
            //}
            //finally
            //{
            //    IsBusy = false;
            //    ButtonText = "Next";
            //}
        }

        public async Task<Tuple<bool, string>> ValidateBVN()
        {
            //try
            //{
            //    if (!HasInternet())
            //    {
            //        return new Tuple<bool, string>(false, "No Internet Connection");
            //    }

            //    IsBusy = true;
            //    ShowErrorMessage = false;
            //    ButtonText = "Validating Your BVN...";
            //    var signUpService = RestService.For<ISignUp>(new HttpClient(new HttpClientHandlerException()) { BaseAddress = new Uri(Constants.FxMallamApi) });


            //    var isBvnValid = await signUpService.ValidateBVN(Step3.BVN);

            //    if (!isBvnValid.Status)
            //    {
            //        NotificationMessage = isBvnValid.Data.Bvn;
            //        ShowErrorMessage = true;
            //        return new Tuple<bool, string>(false, "Invalid BVN");

            //    }
            //    var bvnDetails = isBvnValid.Data;
            //    //valid Firstname
            //    if (!string.Equals(Step2.Firstname.Trim(), bvnDetails.FirstName.Trim(), StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        string msg = "Your Firstname does not match the Firstname in the BVN";
            //        NotificationMessage = msg;
            //        ShowErrorMessage = true;
            //        return new Tuple<bool, string>(false, msg);
            //    }

            //    //if (!string.Equals(Step2.Middlename.Trim(), bvnDetails.MiddleName.Trim(), StringComparison.InvariantCultureIgnoreCase))
            //    //{
            //    //    string msg = "Your Middlename does not match the Middlename in the BVN";
            //    //    NotificationMessage = msg;
            //    //    ShowErrorMessage = true;
            //    //    return new Tuple<bool, string>(false, msg);
            //    //}
            //    if (!string.Equals(Step2.Surname.Trim(), bvnDetails.LastName.Trim(), StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        string msg = "Your Surname does not match the Surname in the BVN";
            //        NotificationMessage = msg;
            //        ShowErrorMessage = true;
            //        return new Tuple<bool, string>(false, msg);
            //    }
            //    DateTime date = DateTime.ParseExact(Step2.DateOfBirth, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            //    string dt = date.ToString("dd-MMM-yy");
            //    if (!string.Equals(bvnDetails.DateOfBirth, dt, StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        string msg = "Your Date of Birth does not match the Date of Birth in the BVN";
            //        NotificationMessage = msg;
            //        ShowErrorMessage = true;
            //        return new Tuple<bool, string>(false, msg);
            //    }
            //    return new Tuple<bool, string>(true, "");
            //}
            //catch (ApiException ee)
            //{
            //    Crashes.TrackError(ee);
            //    NotificationMessage = "An error occurred";
            //    ShowErrorMessage = true;
            //    return new Tuple<bool, string>(false, "An error occurred");
            //}
            //catch (Exception ex)
            //{
            //    NotificationMessage = "Could not process the request";
            //    ShowErrorMessage = true;
            //    Crashes.TrackError(ex);
                return new Tuple<bool, string>(false, "Could not process the request");
            //}
            //finally
            //{
            //    IsBusy = false;
            //    ButtonText = "Next";
            //}

        }
        //public async Task SignIn()
        //{
        //    try
        //    {
        //        NotificationMessage = "";
        //        ShowErrorMessage = false;

        //        if (!HasInternet())
        //        {
        //            NotificationMessage = "No internet connection";
        //            ShowErrorMessage = true;
        //            return;
        //        }


        //        IsBusy = true;
        //        ButtonText = "Authenticating...";
        //        string errorMessage = string.Empty;
        //        var signIn = new SignInModel
        //        {
        //            Password = Step2.Password,
        //            Username = Step1.Email
        //        };
        //        var signInService = RestService.For<ISignIn>(Constants.FxMallamApi);
        //        var request = await signInService.SignIn(signIn);
        //        var content = await request.Content.ReadAsStringAsync();
        //        JObject jObject = JObject.Parse(content);
        //        if (!request.IsSuccessStatusCode)
        //        {
        //            string errormessage = jObject.Value<string>("error_description");
        //            NotificationMessage = errormessage;
        //            ShowErrorMessage = true;
        //            return;
        //        }

        //        Settings.AccessToken = jObject.Value<string>("access_token");
        //        Settings.AccessTokenIssuedAt = jObject.Value<DateTime>(".issued");
        //        Settings.AccessTokenExpiresAt = jObject.Value<DateTime>(".expires");
        //        App.Current.MainPage = new DashboardView();
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //        NotificationMessage = "An error occurred, please try again";
        //        ShowErrorMessage = true;
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //        ButtonText = "Complete Setup";
        //    }
        //}

        public async Task GetSecurityQuestions()
        {
            try
            {
                if (!HasInternet())
                {
                    return;
                }

                //IsBusy = true;
                //ShowErrorMessage = false;
                //var sqService = RestService.For<ISecurityQuestion>(new HttpClient(new HttpClientHandlerException()) { BaseAddress = new Uri(Constants.FxMallamApi) });
                //var securityQuestions = await sqService.GetSecurityQuestions();
                //if (securityQuestions.Status)
                //{
                //    SecurityQuestions.Clear();
                //    securityQuestions.Data.ForEach(s => SecurityQuestions.Add(s));
                //}
                // 

            }
            catch (ApiException ee)
            {
                NotificationMessage = "Unable to load Security Questions";
                ShowErrorMessage = true;
                Crashes.TrackError(ee);
            }
            catch (Exception ex)
            {
                NotificationMessage = "Unknown Error";
                ShowErrorMessage = true;
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }

    public class OnboardingStep1 : ValidatableObject
    {

        private string _nationality = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string Nationality
        {
            get => _nationality;
            set => SetProperty(ref _nationality, value, nameof(Nationality), () => ValidateProperty(value));
        }


        private string _residentCountry = string.Empty;

        /// <summary>
        /// Country of Origin
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public string ResidentCountry
        {
            get => _residentCountry;
            set => SetProperty(ref _residentCountry, value, nameof(ResidentCountry), () => ValidateProperty(value));
        }
    }

    public class OnboardingStep2 : ValidatableObject
    {
        private string _firstname = default(string);
        [Required]
        public string Firstname
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value, nameof(Firstname), () => ValidateProperty(value));
        }

        private string _middlename = default(string);

        public string Middlename
        {
            get => _middlename;
            set => SetProperty(ref _middlename, value, nameof(Middlename), () => ValidateProperty(value));
        }

        private string _surname = default(string);
        [Required]
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value, nameof(Surname), () => ValidateProperty(value));
        }
        private string _selectedTitle = string.Empty;
        [Required(ErrorMessage = "Title is required")]
        public string SelectedTitle
        {
            get => _selectedTitle;
            set => SetProperty(ref _selectedTitle, value, nameof(SelectedTitle), () => ValidateProperty(value));
        }

        private string _dateOfBirth = string.Empty;
        [Required]
        [ValidDateTimeFormat(Format = "dd/MM/yyyy", ErrorMessage = "Invalid Date, format is dd/MM/yyyy")]
        public string DateOfBirth
        {
            get => _dateOfBirth;
            set => SetProperty(ref _dateOfBirth, value, nameof(DateOfBirth), () => ValidateProperty(value));
        }


    }

    public class OnboardingStep3 : ValidatableObject
    {
        private string _email = string.Empty;
        [Required(ErrorMessage = "your email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, nameof(Email), () => ValidateProperty(value));
        }

        private string _phonenumber = string.Empty;
        [Required]
        public string Phonenumber
        {
            get => _phonenumber;
            set => SetProperty(ref _phonenumber, value, nameof(Phonenumber), () => ValidateProperty(value));
        }

        private string _address = default(string);
        [Required]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value, nameof(Address), () => ValidateProperty(value));
        }

        private string _selectedGender = string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        public string SelectedGender
        {
            get => _selectedGender;
            set => SetProperty(ref _selectedGender, value, nameof(SelectedGender), () => ValidateProperty(value));
        }

        private string _bvn = string.Empty;
        [Required]
        [RequiredLength(11, ErrorMessage = "Required length is 11")]
        public string BVN
        {
            get => _bvn;
            set => SetProperty(ref _bvn, value, nameof(BVN), () => ValidateProperty(value));
        }
    }
    public class OnboardingStep4 : ValidatableObject
    {
        private string _verificationCode = default(string);
        [Required]
        [RequiredLength(6, ErrorMessage = "Required length is 6")]
        public string VerificationCode
        {
            get => _verificationCode;
            set => SetProperty(ref _verificationCode, value, nameof(VerificationCode), () => ValidateProperty(value));
        }


        private string timerStep = "30";
        public string TimerStep
        {
            get => timerStep;
            set => SetProperty(ref timerStep, value);
        }

    }
    public class OnboardingStep5 : ValidatableObject
    {

        private string _password = default(string);
        [Required]
        [MinLength(8, ErrorMessage = "Minimum of 8 characters")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password), () => ValidateProperty(value));
        }


        private string _confirmpassword = default(string);
        [Required]
        [MinLength(8, ErrorMessage = "Minimum of 8 characters")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword
        {
            get => _confirmpassword;
            set => SetProperty(ref _confirmpassword, value, nameof(ConfirmPassword), () => ValidateProperty(value));
        }

        private string _transactionPin = default;
        [Required]
        [MinLength(4, ErrorMessage = "Minimum of 4 characters")]
        [MaxLength(4, ErrorMessage = "Maximum of 4 characters")]
        public string TransactionPin
        {
            get => _transactionPin;
            set => SetProperty(ref _transactionPin, value, nameof(TransactionPin), () => ValidateProperty(value));
        }

        private string _confirmTransactionPin = default;
        [Required]
        [MinLength(4, ErrorMessage = "Minimum of 4 characters")]
        [MaxLength(4, ErrorMessage = "Maximum of 4 characters")]
        [Compare("TransactionPin", ErrorMessage = "TransactionPins does not match")]
        public string ConfirmTransactionPin
        {
            get => _confirmTransactionPin;
            set => SetProperty(ref _confirmTransactionPin, value, nameof(ConfirmTransactionPin), () => ValidateProperty(value));
        }
    }

}

using SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleView : ContentPage
    {
        SampleViewModel vm;

        int currentIndex = 0;
        public SampleView()
        {
            InitializeComponent();

            //CountryCodes = new Dictionary<string, string>();
            //CountryCodes.Add("Nigeria", "+234");
            //CountryCodes.Add("United Kingdom", "400");
            //CountryCodes.Add("U.S.A", "500");
        }

        protected override async void OnAppearing()
        {

            vm = this.BindingContext as SampleViewModel;
            base.OnAppearing();
            currentIndex = vm.CurrentIndex;
            //vm.SecurityQuestionAnswers.Clear();
            //await vm.GetSecurityQuestions();
        }
        protected void SignUp(object sender, EventArgs e)
        {
         //   SignUpView nextPage = new SignUpView();
            // ViewModels.Base.ViewModelLocator.SetAutoWireViewModel(nextPage, true);
           // Navigation.PushAsync(nextPage);
        }

        private void SignIn(object sender, EventArgs e)
        {
           // App.Current.MainPage = new DashboardView();
        }
        private void Signuptabview_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            signuptabview.SelectedIndex = e.Index;
            currentIndex = e.Index;
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            bool hasErrors = ModelHasErrors(currentIndex);
            if (hasErrors) return;


            if (currentIndex == 2)
            {
                var isBvnValid = await vm.ValidateBVN();
                if (isBvnValid.Item1)
                {
                    await vm.GetOtp();
                }
                else { return; }
            }
            if (currentIndex == 4)
            {
                vm.ButtonText = "Sign Up";
            }
            if (currentIndex == 5)
            {
                vm.SubmitCommand.Execute(null);
            }

            if (currentIndex == signuptabview.Items.Count - 1) return;
            vm.CurrentIndex += 1;

        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            if (vm.CurrentIndex == 5)
            {
                vm.ButtonText = "Next";
            }
            if (vm.CurrentIndex == 0) return;
            vm.CurrentIndex -= 1;

        }

        private void StartTimerEvent()
        {
            var timer = TimeSpan.FromSeconds(1);
            var maxTime = TimeSpan.FromSeconds(30);
            Device.StartTimer(timer, () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    vm.Step4.TimerStep = maxTime.ToString("mm\\:ss");
                });
                maxTime = maxTime.Subtract(TimeSpan.FromSeconds(1));
                bool canStop = maxTime.Seconds > 0;
                return canStop;
            });
        }
        private bool ModelHasErrors(int index)
        {

            switch (index)
            {
                case 0:
                    vm.Step1.ValidateModel();
                    return vm.Step1.HasErrors;
                case 1:
                    vm.Step2.ValidateModel();
                    return vm.Step2.HasErrors;
                case 2:
                    vm.Step3.ValidateModel();
                    return vm.Step3.HasErrors;
                case 3:
                    vm.Step4.ValidateModel();
                    return vm.Step4.HasErrors;
                case 4:
                    vm.Step5.ValidateModel();
                    return vm.Step5.HasErrors;
                case 5:
                    return !vm.IsQuestionComplete;
                default:
                    return false;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            vm.NotificationMessage = string.Empty;
            vm.ShowErrorMessage = false;
        }


        private void Nationality_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            //  var bindingContext = this.BindingContext as SampleViewModel;
            var obj = sender as Syncfusion.XForms.ComboBox.SfComboBox;
            vm.Step1.Nationality = obj.SelectedValue?.ToString();
        }
        private void ResidentCountry_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            // var bindingContext = this.BindingContext as SampleViewModel;
            var obj = sender as Syncfusion.XForms.ComboBox.SfComboBox;
            vm.Step1.ResidentCountry = obj.SelectedValue?.ToString();
        }
        private void Title_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            // var bindingContext = this.BindingContext as SampleViewModel;
            var obj = sender as Syncfusion.XForms.ComboBox.SfComboBox;
            vm.Step2.SelectedTitle = obj.SelectedValue?.ToString();
        }
        private void Gender_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            // var bindingContext = this.BindingContext as SampleViewModel;
            var obj = sender as Syncfusion.XForms.ComboBox.SfComboBox;
            vm.Step3.SelectedGender = obj.SelectedValue?.ToString();
        }
        protected override bool OnBackButtonPressed()
        {


            if (signuptabview.SelectedIndex == 0)
                return base.OnBackButtonPressed();
            else
            {
                currentIndex--;
                vm.CurrentIndex = currentIndex;
                if (currentIndex == 5)
                {
                    vm.ButtonText = "Sign Up";
                }
                else
                {
                    vm.ButtonText = "Next";
                }
                return true;
            }

        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // if (vm.SecurityQuestionAnswers.Count == 3) return;//Max question to be answered is 3

            //CheckBox checkBox = sender as CheckBox;
            //var ctx = checkBox.BindingContext as Models.SecurityQuestion.GetSecurityQuestion;
            //ctx.IsSelected = e.Value;

            //var selectedQuestAns = vm.SecurityQuestionAnswers.FirstOrDefault(s => s.QuestionId == ctx.Id);
            //if (selectedQuestAns == null)
            //{
            //    vm.SecurityQuestionAnswers.Add(new Models.SecurityQuestion.SecurityQuestionAnswer { QuestionId = ctx.Id, Answer = ctx.Answer });
            //    vm.IsQuestionComplete = vm.SecurityQuestionAnswers.Count == 3;
            //}
            //else
            //{
            //    vm.SecurityQuestionAnswers.Remove(selectedQuestAns);
            //    ctx.Answer = string.Empty;
            //    vm.IsQuestionComplete = vm.SecurityQuestionAnswers.Count == 3;
            //}

        }
    }
}
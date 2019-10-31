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
    public partial class WelcomeScreenView : ContentPage
    {
        public WelcomeScreenView()
        {
            InitializeComponent();
        }

        protected void SignUp(object sender, EventArgs e)
        {
            //SignUpView nextPage = new SignUpView();
            //ViewModels.Base.ViewModelLocator.SetAutoWireViewModel(nextPage, true);

            Navigation.PushAsync(new SampleView());
            // Navigation.PushAsync( new OnBoardingView());
        }

        private void SignIn(object sender, EventArgs e)
        {
            //SignInView nextPage = new SignInView();
            //ViewModels.Base.ViewModelLocator.SetAutoWireViewModel(nextPage, true);
            //Navigation.PushAsync(nextPage);

        }
    }
}
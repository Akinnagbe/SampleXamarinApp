using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SampleApp.Services;
using SampleApp.Views;
using AutoMapper;

namespace SampleApp
{
    public partial class App : Application
    {
        public static MapperConfiguration MapperConfiguration;
        public App()
        {
            InitializeComponent();

            //  DependencyService.Register<MockDataStore>();
            // MainPage = new AppShell();
            MainPage = new NavigationPage(new WelcomeScreenView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MapperConfiguration = Bootstrapper.Configure();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

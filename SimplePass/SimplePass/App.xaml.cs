using Plugin.FirebaseAuth;
using SimplePass.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass
{
    public partial class App : Application
    {
        public static Plugin.FirebaseAuth.IUser userLogged;
        public App()
        {
            InitializeComponent();

            userLogged = CrossFirebaseAuth.Current.Instance.CurrentUser;

            if (userLogged is null)
                MainPage = new NavigationPage(new Carousel());
            else
                MainPage = new AppShell();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using SimplePass.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Carousel());
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

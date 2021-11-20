using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Plugin.FirebaseAuth;
using SimplePass.Models;
using SimplePass.Views.MainFlyout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimplePass.Views
{
    public partial class Login : ContentPage
    {
        public User oUser { get; set; }
        public Login()
        {
            InitializeComponent();
            oUser = new User();

            if(App.userLogged != null)
                oUser.email = App.userLogged.Email;

            this.BindingContext = oUser;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                bool hasFingerprint = Preferences.Get("hasFingerprintActivated", false);
                if (hasFingerprint)
                {
                    var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Iniciar sesión", "Iniciaremos sesión usando tu huella dactilar."));
                    if (result.Authenticated)
                    {
                        var stack = Navigation.NavigationStack[0];

                        Navigation.InsertPageBefore(new Main(), stack);

                        await Navigation.PopToRootAsync(true);
                    }
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", $"Error: {ex.Message}\nSource: {ex.Source}", "Cerrar");
            }
        }

        private void CreateAccount_Clicked(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack[0] == this)
            {
                CreateAccount.isFirstPage = false;
                Navigation.PushAsync(new CreateAccount());
            }
            else
                Navigation.PopToRootAsync();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                // TODO: Loading login

                var userInfo = CrossFirebaseAuth.Current.Instance.CurrentUser;

                var user = (User)BindingContext;

                var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(user.email, user.password);

                if (result != null)
                {
                    var stack = Navigation.NavigationStack[0];

                    Navigation.InsertPageBefore(new Main(), stack);

                    await Navigation.PopToRootAsync(true);
                }

            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", $"Error: {ex.Message}\nSource: {ex.Source}", "De acuerdo");
            }
            finally
            {
                // Stop loading
            }
        }
    }
}

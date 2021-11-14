using Plugin.FirebaseAuth;
using SimplePass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
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
            this.BindingContext = oUser;
        }

        private void CreateAccount_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                // TODO: Loading login

                var user = (User)BindingContext;

                var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(user.email, user.password);

                _ = this.DisplayToastAsync( "Hola " + result.User.DisplayName);

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

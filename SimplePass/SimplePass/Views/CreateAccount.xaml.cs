using Plugin.CloudFirestore;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimplePass.Models;
using Plugin.FirebaseAuth;

namespace SimplePass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        public User oUser { get; set; }
        public CreateAccount()
        {
            InitializeComponent();
            oUser = new User();
            this.BindingContext = oUser;
        }

        private async void Close_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void CreateSession_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Loading
                User user = (User)BindingContext;

                //using (var sha = new System.Security.Cryptography.SHA256Managed())
                //{
                //    byte[] textData = System.Text.Encoding.UTF8.GetBytes(user.password);
                //    byte[] hash = sha.ComputeHash(textData);
                //    user.password = BitConverter.ToString(hash).Replace("-", String.Empty);
                //}


                var result = await CrossFirebaseAuth.Current.Instance.CreateUserWithEmailAndPasswordAsync(user.email, user.password);

                //await CrossCloudFirestore.Current
                //                         .Instance
                //                         .Collection("users")
                //                         .AddAsync(user);

                _ = this.DisplayToastAsync("Usuario registrado");
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", $"Error: {ex.Message}\nSource: {ex.Source}", "De acuerdo");
            }
            finally
            {
                //Stop loading
            }

        }
    }
}
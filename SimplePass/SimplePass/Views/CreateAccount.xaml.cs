using Plugin.CloudFirestore;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimplePass.Models;
using Plugin.FirebaseAuth;
using SimplePass.Views.MainFlyout;

namespace SimplePass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        public User oUser { get; set; }
        public static bool isFirstPage { get; set; } = true;
        public CreateAccount()
        {
            InitializeComponent();
            oUser = new User();
            this.BindingContext = oUser;
        }

        private async void Close_Clicked(object sender, EventArgs e)
        {
            var firstPage = Navigation.NavigationStack[0];
            if (isFirstPage == true)
                Navigation.InsertPageBefore(new Login(), firstPage);


            await Navigation.PopToRootAsync();
        }

        private async void CreateSession_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Loading
                User user = (User)BindingContext;

                var result = await CrossFirebaseAuth.Current.Instance.CreateUserWithEmailAndPasswordAsync(user.email, user.password);

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
                //Stop loading
            }

        }
    }
}
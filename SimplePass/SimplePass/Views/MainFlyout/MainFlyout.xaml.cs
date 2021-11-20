using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass.Views.MainFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainFlyout : ContentPage
    {
        public ListView ListView;

        public MainFlyout()
        {
            InitializeComponent();

            BindingContext = new MainFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class MainFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainFlyoutMenuItem> MenuItems { get; set; }

            public String email { get; set; }

            public MainFlyoutViewModel()
            {
                email = App.userLogged.Email;
                MenuItems = new ObservableCollection<MainFlyoutMenuItem>(new[]
                {
                    new MainFlyoutMenuItem { Id = 0, Title = "Huella Digital" },
                    new MainFlyoutMenuItem { Id = 1, Title = "Califícanos" }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private async void Logout_Tapped(object sender, EventArgs e)
        {
            try
            {
                CrossFirebaseAuth.Current.Instance.SignOut();
                Preferences.Clear();

                var stack = Navigation.NavigationStack[0];

                Navigation.InsertPageBefore(new Login(), stack);

                await Navigation.PopToRootAsync(true);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
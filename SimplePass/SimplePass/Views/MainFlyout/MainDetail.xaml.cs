using Plugin.CloudFirestore;
using SimplePass.Models;
using SimplePass.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass.Views.MainFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDetail : ContentPage
    {
        private MainViewModel mainVM { get; set; }
        public MainDetail()
        {
            InitializeComponent();
            mainVM = new MainViewModel();
            this.BindingContext = mainVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            mainVM.GetAccountsAsync();
        }

        private async void AddNew_Clicked(object sender, EventArgs e)
        {
            try
            {

                var result = await Navigation.ShowPopupAsync(new PopupAddAccount { BindingContext = new Account() });

                if (result != null)
                    this.mainVM.observableAccounts.Add((Account)result);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Account account = (Account)e.Item;

                var result = await Navigation.ShowPopupAsync(new PopupAddAccount(true) { BindingContext = account });

                if (result != null)
                {
                    if (this.mainVM.observableAccounts.Contains(result))
                        this.mainVM.observableAccounts.Remove((Account)result);
                    this.mainVM.observableAccounts.Add((Account)result);
                }
                else
                    this.mainVM.RemoveAccount(account);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
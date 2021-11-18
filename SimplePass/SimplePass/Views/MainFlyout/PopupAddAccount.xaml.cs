using Plugin.CloudFirestore;
using SimplePass.Models;
using SimplePass.Services;
using System;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass.Views.MainFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddAccount : Popup
    {
        private Account oAccount { get; set; }
        public PopupAddAccount(bool wantEdit = false)
        {
            InitializeComponent();
            this.btnSave.Command = wantEdit ? new Command(Edit_Clicked) : new Command(Save_Clicked);
        }

        private async void Save_Clicked(object sender)
        {
            try
            {
                oAccount = (Account)BindingContext;

                oAccount.password = Encryption.SecurityEncryptMD5(oAccount.password);               


                var response = await CrossCloudFirestore.Current
                                                        .Instance
                                                        .Collection("Users")
                                                        .Document(App.userLogged.Uid)
                                                        .Collection("Accounts")
                                                        .AddAsync(oAccount);

                oAccount.id = response.Id;

                this.Dismiss(oAccount);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error al guardar");
            }
        }

        private async void Edit_Clicked(object sender)
        {
            try
            {
                oAccount = (Account)BindingContext;

                oAccount.password = Encryption.SecurityEncryptMD5(oAccount.password);


                var response = CrossCloudFirestore.Current
                                                  .Instance
                                                  .Collection("Users")
                                                  .Document(App.userLogged.Uid)
                                                  .Collection("Accounts")
                                                  .Document(oAccount.id)
                                                  .UpdateAsync(oAccount);

                this.Dismiss(oAccount);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error al guardar");
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.Dismiss(null);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error al guardar");
            }
        }
    }
}
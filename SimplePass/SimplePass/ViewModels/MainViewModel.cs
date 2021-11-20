using Plugin.CloudFirestore;
using SimplePass.Models;
using SimplePass.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace SimplePass.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> observableAccounts { get { return _accounts; } set { _accounts = value; OnPropertyChanged("observableAccounts"); } }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MainViewModel()
        {
            observableAccounts = new ObservableCollection<Account>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void GetAccountsAsync()
        {
            try
            {
                var documents = await CrossCloudFirestore.Current
                                                        .Instance
                                                        .Collection("Users")
                                                        .Document(App.userLogged.Uid)
                                                        .Collection("Accounts")
                                                        .GetAsync();
                               
                foreach (var document in documents.Documents)
                {
                    Account account = new Account();
                    account.site = (String)document.Data["site"];
                    account.email = (String)document.Data["email"];
                    account.password = Encryption.SecurityDecryptMD5((String)document.Data["password"]);
                    account.id = document.Id;
                    
                    observableAccounts.Add(account);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al obtener datos");
            }
        }

        public async void RemoveAccount(Account account)
        {
            var document = CrossCloudFirestore.Current
                                              .Instance
                                              .Collection("Users")
                                              .Document(App.userLogged.Uid)
                                              .Collection("Accounts")
                                              .Document(account.id)
                                              .DeleteAsync();

            this.observableAccounts.Remove(account);
        }
    }
}

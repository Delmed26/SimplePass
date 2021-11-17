using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

            public MainFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainFlyoutMenuItem>(new[]
                {
                    new MainFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new MainFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new MainFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new MainFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new MainFlyoutMenuItem { Id = 4, Title = "Page 5" },
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
    }
}
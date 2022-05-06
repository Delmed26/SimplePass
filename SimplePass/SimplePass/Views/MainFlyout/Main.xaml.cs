
using Plugin.Fingerprint;
using SimplePass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass.Views.MainFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : FlyoutPage
    {
        public Main()
        {
            InitializeComponent();
            FlyoutPage.ListView.SelectionMode = ListViewSelectionMode.None;
            FlyoutPage.ListView.ItemTapped += ListView_ItemTapped;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MainFlyoutMenuItem;
            if (item == null)
                return;

            if(item.Title == "Califícanos")
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        var url = "https://apps.apple.com/mx/app/netflix/id363590051";
                        await Launcher.OpenAsync(new Uri(url));
                        break;
                    case Device.Android:
                        bool isInstalled = await googlePlayInstalled("market://details?id=com.netflix.mediaclient&hl=es");
                        if (!isInstalled)
                            await Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.netflix.mediaclient&hl=es_MX&gl=US"));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
                if (isFingerprintAvailable)
                    Navigation.ShowPopup(new Fingerprint());
                else
                    _ = this.DisplayToastAsync("Detector de huellas no disponible");
            }
        }

        private async Task<bool> googlePlayInstalled(String uri)
        {
            try
            {
                await Launcher.OpenAsync(new Uri(uri));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
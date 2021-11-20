using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace SimplePass.Views.MainFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Fingerprint : Popup
    {
        public Fingerprint()
        {
            InitializeComponent();
            useFingerprint();
        }

        public async void useFingerprint()
        {
            try
            {
                var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Guardar", "Utilizaremos la huella guardada en tu dispositivo para iniciar sesión."));
                if (result.Authenticated)
                {
                    this.BindingContext = Xamarin.Forms.Color.Green;
                    Preferences.Set("hasFingerprintActivated",true);
                }
                else
                {
                    this.BindingContext = Xamarin.Forms.Color.Red;
                }
            }
            catch (System.Exception)
            {

            }
        }

        private void Delete_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                Preferences.Remove("hasFingerprintActivated");
                this.Dismiss(null);
            }
            catch (System.Exception)
            {

            }
        }
    }
}
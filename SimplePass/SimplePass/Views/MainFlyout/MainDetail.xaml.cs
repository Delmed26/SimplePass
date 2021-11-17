using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplePass.Views.MainFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDetail : ContentPage
    {
        public MainDetail()
        {
            InitializeComponent();
        }

        private void AddNew_Clicked(object sender, EventArgs e)
        {
            try
            {
                //TODO: Loading



            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
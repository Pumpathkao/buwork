using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupName : Popup
    {
        public PopupName()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            Dismiss(null);
        }
    }
}
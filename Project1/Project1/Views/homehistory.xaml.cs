using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class homehistory : ContentPage
    {
        public homehistory()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            searchimg.Icon = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fsearch.png?alt=media&token=ab6fc5ad-4c66-41ed-ba74-3be3c58bb827";
            reporthimg.Icon = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Ffile.png?alt=media&token=919328f6-5eb7-43ed-9f18-14f79481cdad";
            homeimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fhome.png?alt=media&token=8970ba7e-274d-470b-bf19-70e05d2e3577";
            addressimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Faddress.png?alt=media&token=7966ae2f-682f-4e19-b5c7-e94af44eaa52";
            backbunton.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";

        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uploadimage());
        }

        private async void TabViewItem_TabTapped_1(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
          


            await Navigation.PushAsync(new inspechistory());
        }

        async private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new History());
        }

        private void TabViewItem_TabTapped(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
            Navigation.PushAsync(new sendemail());
        }
    }
}
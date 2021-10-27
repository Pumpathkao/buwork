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
    public partial class home : ContentPage
    {
        public home()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            image1.Source = Global.imageurl1;
            name.Text = Global.name;
            name1.Text = Global.name;
            phone.Text = Global.phone;
            email.Text = Global.email;
            address.Text = Global.address;
            round.Text = Global.round.ToString();
            dateound.Text = Global.dateround;
            searchimg.Icon = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fsearch%20(1).png?alt=media&token=3ad06835-0e17-41fe-8a39-a3561ffd71ba";
            reporthimg.Icon = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fanalytics.png?alt=media&token=0f5db83c-e523-4a5f-a060-2e8f6d6807d4";
            homeimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fhome%20(1).png?alt=media&token=95374f1f-3ac8-4be0-b532-f4362a5b81da";
            addressimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Faddress%20(1).png?alt=media&token=5fe004da-95a5-4dae-804e-42dee839dcde";
            backbunton.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";


        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uploadimage());
        }

        private async void TabViewItem_TabTapped_1(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
            var check = await GoogleFirebase.Getjob(Global.username, Global.jobid);
            check.round = check.round + 1;
            check.dateround = DateTime.Now.ToString();

            await GoogleFirebase.UpdateUser(check);


            await Navigation.PushAsync(new Inspect());
            Global.dateround = Global.dateround + 1;
        }

        async private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new main());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var check = await GoogleFirebase.Getjob(Global.username, Global.jobid);
            check.closejob = 1;

            bool answer = await DisplayAlert("Warning", "Do you want to close job?", "Yes", "No");
            if (answer)
                await GoogleFirebase.updateclosejob(check);
            await Navigation.PushAsync(new main());
        }

        private void TabViewItem_TabTapped(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
            Navigation.PushAsync(new sendemail());
        }
    }
}
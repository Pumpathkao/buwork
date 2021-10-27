using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Splashscreen : ContentPage
    {
        public Splashscreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Delay for a few seconds on the splash screen
            await Task.Delay(3000);
            await image.ScaleTo(1, 2000);
            await image.ScaleTo(0.9, 1500, Easing.Linear);
            await image.ScaleTo(50, 1200, Easing.Linear);
            // Instantiate a NavigationPage with the MainPage
          Navigation.PushAsync(new login());
        
        }
    }
}
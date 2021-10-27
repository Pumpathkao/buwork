using Project1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Project1.Models;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
        public login()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = await GoogleFirebase.GetUser(username.Text);
            if (user != null)
            {
                if (username.Text == user.Username && password.Text == user.Password)
                {
                    await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");

                    
                    Navigation.PushAsync(new main());
                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    _navigation.RemovePage(_lastPage);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Username or password incorrect", "Ok");
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Username and Password", "OK");
        }
    
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new regis());
        }
    }
}
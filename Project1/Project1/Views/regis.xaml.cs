using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Project1.Services;
using System.Text.RegularExpressions;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class regis : ContentPage
    {
        public regis()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(name1.Text) && string.IsNullOrEmpty(user1.Text) && string.IsNullOrEmpty(pass1.Text))
            {
                DisplayAlert("Plese!!!!", "Plese fill every field", "Ok");
            }
            else { 
            int checkid = 0;
            Userdata data1;
            var check = await GoogleFirebase.GetAllUser();
            for(int i = 0; i < check.Count; i++)
            {
                if(user1.Text == check[i].Username)
                {
                    checkid = 1;
                }
            }
            if (checkid == 0)
            {
               
                data1 = new Userdata
                {
                    Username = user1.Text,
                    Password = pass1.Text,
                    Name = name1.Text,
                    ImageUrl = "https://static-00.iconduck.com/assets.00/person-icon-486x512-eeiy7owm.png"

                };

                //call AddUser function which we define in Firebase helper class
                //
                if (user1.Text.ToCharArray().Length < 4)
                {
                    DisplayAlert("Error", "กรุณาใส่ Username มากกว่า 4 ตัว", "Ok");
                }
                else
                {
                    if (pass1.Text.Length < 6)
                    {
                        DisplayAlert("Error", "กรุณาใส่ password มากกว่า 6 ตัว", "Ok");
                    }
                    else
                    {
                        var user = await GoogleFirebase.AddUser(data1);
                        if (user)
                        {
                            await App.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");
                            Navigation.PushAsync(new login());
                        }
                    }
                }
            }
            else
            {
                DisplayAlert("Error", "Username already", "ok");
            }



            }
        }

        private  void Button_Clicked_1(object sender, EventArgs e)
        {
           Navigation.PopAsync();
        }
    }
}
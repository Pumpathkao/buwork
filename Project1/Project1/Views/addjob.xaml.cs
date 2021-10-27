using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addjob : ContentPage
    {
        Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public addjob()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        public class RandomGenerator
        {
            // Instantiate random number generator.  
            // It is better to keep a single Random instance 
            // and keep using Next on the same instance.  
            private readonly Random _random = new Random();

            // Generates a random number within a range.      
            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }

            // Generates a random string with a given size.    
            public string RandomString(int size, bool lowerCase = false)
            {
                var builder = new StringBuilder(size);

                // Unicode/ASCII Letters are divided into two blocks
                // (Letters 65–90 / 97–122):   
                // The first group containing the uppercase letters and
                // the second group containing the lowercase.  

                // char is a single Unicode character  
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; // A...Z or a..z: length = 26  

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }

            // Generates a random password.  
            // 4-LowerCase + 4-Digits + 2-UpperCase  
            public string RandomPassword()
            {
                var passwordBuilder = new StringBuilder();

                // 4-Letters lower case   
                passwordBuilder.Append(RandomString(4, true));

                // 4-Digits between 1000 and 9999  
                passwordBuilder.Append(RandomNumber(1000, 9999));

                // 2-Letters upper case  
                passwordBuilder.Append(RandomString(2));
                return passwordBuilder.ToString();
            }
        }
        private async void date_DateSelected(object sender, DateChangedEventArgs e)
        {
           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var generator = new RandomGenerator();
            var randomid = generator.RandomPassword();
            Job data1;

            if (!myMultiValidation.IsValid)
            {
                var errorBuilder = new StringBuilder();

                foreach (var error in myMultiValidation.Errors)
                {
                    if (error is string)
                    {
                        errorBuilder.AppendLine(((string)error).ToString());
                    }

                    await DisplayAlert("Error", errorBuilder.ToString(), "OK");
                }
            }
            else
            {
                if (phone.Text.Length == 10)
                {
                    data1 = new Job
                    {
                        address = addrerss.Text,
                        email = email.Text,
                        username = Global.UsernameGlobal,
                        jobid = randomid,
                        phone = phone.Text,
                        name = name.Text,
                        date = date.Date,
                        img = "home.png",
                        title = title.Text,
                        round = 0,
                        ImageUrl = "https://home.frasersproperty.co.th/usersupload/project/cover/20210713180114_Cover_Mobile.jpg",
                        closejob = 0
                    };
                    var user = await GoogleFirebase.AddJob(data1);
                    //call AddUser function which we define in Firebase helper class
                    await DisplayAlert("Add Success", "", "Ok");
                    Navigation.PushAsync(new main());
                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    _navigation.RemovePage(_lastPage);
                }
                else
                {
                    DisplayAlert("Error", "Phone number = 10", "ok");
                }
            }



        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}
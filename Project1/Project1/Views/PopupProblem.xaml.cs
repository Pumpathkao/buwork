using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupProblem : Popup
    {
        int level = -1;
        public PopupProblem()
        {
            InitializeComponent();
            lv1.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fwarning%20(5).png?alt=media&token=c40c1fd4-50aa-403a-a29f-8ae477f4307e";
            lv2.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fwarning%20(5).png?alt=media&token=c40c1fd4-50aa-403a-a29f-8ae477f4307e";
            lv3.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fwarning%20(5).png?alt=media&token=c40c1fd4-50aa-403a-a29f-8ae477f4307e";
            lv4.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fwarning%20(5).png?alt=media&token=c40c1fd4-50aa-403a-a29f-8ae477f4307e";
            lv5.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fwarning%20(5).png?alt=media&token=c40c1fd4-50aa-403a-a29f-8ae477f4307e";
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

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Dismiss(null);
           
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            lv1.BorderColor = Color.Black;
            lv2.BorderColor = Color.Transparent;
            lv3.BorderColor = Color.Transparent;
            lv4.BorderColor = Color.Transparent;
            lv5.BorderColor = Color.Transparent;
            level = 1;
            
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            lv1.BorderColor = Color.Transparent;
            lv2.BorderColor = Color.Black;
            lv3.BorderColor = Color.Transparent;
            lv4.BorderColor = Color.Transparent;
            lv5.BorderColor = Color.Transparent;
            level = 2;
        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            lv1.BorderColor = Color.Transparent;
            lv2.BorderColor = Color.Transparent;
            lv3.BorderColor = Color.Black;
            lv4.BorderColor = Color.Transparent;
            lv5.BorderColor = Color.Transparent;
            level = 3;
        } 

        private void ImageButton_Clicked_3(object sender, EventArgs e)
        {
            lv1.BorderColor = Color.Transparent;
            lv2.BorderColor = Color.Transparent;
            lv3.BorderColor = Color.Transparent;
            lv4.BorderColor = Color.Black;
            lv5.BorderColor = Color.Transparent;
            level = 4;
        }

        private void ImageButton_Clicked_4(object sender, EventArgs e)
        {
            lv1.BorderColor = Color.Transparent;
            lv2.BorderColor = Color.Transparent;
            lv3.BorderColor = Color.Transparent;
            lv4.BorderColor = Color.Transparent;
            lv5.BorderColor = Color.Black;
            level = 5;
        }

        private async void  Button_Clicked(object sender, EventArgs e)
        {
            if (level == -1)
            {
                Application.Current.MainPage.DisplayAlert("Error", "choose level", "ok");
            }
            else
            {
                var generator = new RandomGenerator();
                var randomid = generator.RandomPassword();
                problem data1;
                data1 = new problem
                {
                   abountproblem = problem.Text,
                   checklistid = Global.ChecklistId,
                   exteriaId = Global.ExteriaId,
                   jobid = Global.jobid,
                   level=level,
                   problemid = randomid,
                   ImageUrl= "https://i.pinimg.com/originals/3c/fc/52/3cfc52c14a58f4de54d5234e2a2f4b8b.png"
                };
              
                var user = await GoogleFirebase.addproblem(data1);
                //call AddUser function which we define in Firebase helper class    
                Dismiss(null);
                Application.Current.MainPage.Navigation.PushAsync(new EditPage());
            }
        }
    }
}
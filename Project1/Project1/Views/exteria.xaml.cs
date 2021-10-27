using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class exteria : ContentPage
    {
        public exteria()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent(); 
            backbunton.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";
            plusimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fplus.png?alt=media&token=3ad1b6b3-31b8-4f02-a013-b14e4c16ba53";
            adddata();
        }
        
        public async void adddata()
            {
                var data = await GoogleFirebase.GetAllexterianame();
                var data1 = new ObservableCollection<Models.exteria>();
                int i = 0;
                for (i = 0; i < data.Count; i++)
                {
                    if (Global.jobid == data[i].jobid&&Global.ChecklistId==data[i].checklistid)
                    {
                        data1.Add(data[i]);
                    }
                }
                listView.ItemsSource = data1;
        }
        

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Detail", " ");
            if (result == null||result=="")
            {
                DisplayAlert("Error", "Put detail", "ok");
            }
            else
            {
                var generator = new RandomGenerator();
                var randomid = generator.RandomPassword();

                Models.exteria data;



                data = new Models.exteria
                {

                    exteriaid = randomid,
                    checklistid = Global.ChecklistId,
                    jobid = Global.jobid,
                    name = result,
                    ImageUrl = "https://image.flaticon.com/icons/png/512/685/685655.png"

                };

                var check = await GoogleFirebase.addexterianame(data);


                await Navigation.PushAsync(new exteria());
            }
          
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Models.exteria)e.SelectedItem;
            Global.ExteriaId = item.exteriaid;
            Global.nametop = item.name;
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditPage
                {
                    BindingContext = e.SelectedItem as Models.exteria,
                    Title = "Edit User"

                });
               


            }
            
        }
        private async void listView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                //  var item = (jobchecklist)e.CurrentSelection;
                var item = (Models.exteria)e.CurrentSelection.FirstOrDefault();
                Global.ExteriaId = item.exteriaid;
                Global.nametop = item.name;
                Global.exteria = item.name;
                await Navigation.PushAsync(new EditPage
                {
                    BindingContext = e.CurrentSelection as exteria,


                });
            }
        }
        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;

            var SelectedParent = item.BindingContext;
            var x = listView.Parent.BindingContext = SelectedParent;
            var item1 = x as Models.exteria;

            bool answer =
                  await Application.Current
                  .MainPage.DisplayAlert("ยืนยัน", item1.name,
                  "ลบ", "ยกเลิก");
            if (answer)
            {
                await GoogleFirebase.Deleteexteria(item1.checklistid, item1.name, item1.jobid,item1.exteriaid);
                await GoogleFirebase.deleteproblem1(item1.exteriaid);
                Navigation.PushAsync(new exteria());

            }

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

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Inspect());
        }
    }
}
using Newtonsoft.Json;
using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspecCheck : ContentPage
    {
        List<String> datalist = new List<String>();
        private ObservableCollection<Checklist> _rootobj;

        int check = 0;
        public InspecCheck()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            buttonback.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";



            adddata();
        }
        public async void adddata()
        {
            var data2 = await GoogleFirebase.GetAllJobCheckList();
            var data3 = new ObservableCollection<jobchecklist>();
            int i = 0;
            for (i = 0; i < data2.Count; i++)
            {
                if (Global.jobid == data2[i].jobid)
                {
                    data3.Add(data2[i]);
                }
            }


            var data = await GoogleFirebase.Getlistall();
            var data1 = new ObservableCollection<listall>();
            int j = 0;
            if (data3.Count == 0)
            {
                for (j = 0; j < data.Count; j++)
                {

                    data1.Add(data[j]);

                }
            }
            else
            {
                for (j = 0; j < data.Count; j++)
                {

                    data1.Add(data[j]);

                }
                for (j = 0; j < data.Count; j++)
                {
                    for (int k = 0; k < data3.Count; k++)
                    {
                        if (data3[k].checklist == data[j].checkllistname)
                        {
                            data1.Remove(data[j]);
                        }
                    }
                }
            }

            MyList.ItemsSource = data1;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            var cb = (CheckBox)sender;
            var item = (listall)cb.BindingContext;
            var id = item.checkllistname;
            check = 0;
            for (int i = 0; i < datalist.Count; i++)
            {
                if (datalist.Count > 0)
                {
                    if (datalist[i] == item.checkllistname)
                    {
                        check = 1;

                    }
                    else
                    {
                        check = 0;
                    }
                }

            }
            if (check == 0)
            {
                datalist.Add(item.checkllistname);

            }
            else
            {
                datalist.Remove(item.checkllistname);
            }


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            jobchecklist data;
            string a = Global.jobid;
            for (int i = 0; i < datalist.Count; i++)
            {
                var randomchecklistid = random();
                data = new jobchecklist
                {
                    jobid = Global.jobid,
                    checklist = datalist[i],
                    checklistid = randomchecklistid


                };
                var user = await GoogleFirebase.AddJobCheckList(data);

            }

            await Navigation.PushAsync(new Inspect());
        }
        public string random()
        {
            var generator = new RandomGenerator();
            var randomid = generator.RandomPassword();
            return randomid;
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
            Navigation.PopAsync();
        }
    }
}
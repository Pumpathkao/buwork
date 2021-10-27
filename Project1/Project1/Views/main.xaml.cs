using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class main : ContentPage
    {
        public IList<Job> data { get; private set; }
        public main()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            adddata();
            name.Text = Global.NameGlobal;
            addimage();
            username.Text = Global.UsernameGlobal;
        }
        public async void adddata()
        {
            var data = (await GoogleFirebase.GetAllJob()).OrderBy(x => x.date).ToList();
            var data1 = new ObservableCollection<Job>();
            int i = 0;
            for (i = 0; i < data.Count; i++)
            {
                if (Global.UsernameGlobal == data[i].username)
                {
                    if (data[i].closejob == 0)
                    {
                        data1.Add(data[i]);
                    }

                }
            }
            listView.ItemsSource = data1;
        }
        public async void addimage()
        {
            var data = await GoogleFirebase.GetAllUser();
            var data1 = "";
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Username == Global.username)
                {
                    data1 = data[i].ImageUrl;
                }
            }



            img.Source = data1;
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (Job)e.SelectedItem;



                Global.title = item.title;
                Global.name = item.name;
                Global.date = item.date;
                Global.img = item.img;
                Global.phone = item.phone;
                Global.email = item.email;
                Global.jobid = item.jobid;
                Global.address = item.address;
                Global.round = item.round;
                Global.dateround = item.dateround;
                Global.ImageName = item.ImageName;
                Global.imageurl1 = item.ImageUrl;
                await Navigation.PushAsync(new home
                {
                    BindingContext = e.SelectedItem as Job,
                    Title = "Edit User"

                });





            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new addjob());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new login());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new History());
        }

        private void img_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uploadprofile());
        }
    }
}
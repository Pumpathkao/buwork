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
    public partial class History : ContentPage
    {
        public History()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            adddata();
            backbunton.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";
        }
        public async void adddata()
        {
            var data = await GoogleFirebase.GetAllJob();
            var data1 = new ObservableCollection<Job>();
            int i = 0;
            for (i = 0; i < data.Count; i++)
            {
                if (Global.UsernameGlobal == data[i].username)
                {
                    if (data[i].closejob == 1)
                    {
                        data1.Add(data[i]);
                    }

                }
            }
            listView.ItemsSource = data1;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new main());
        }
        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new homehistory
                {
                    BindingContext = e.SelectedItem as Job,
                    Title = "Edit User"

                });
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





            }
        }
    }
}
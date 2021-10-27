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
    public partial class exteriahistory : ContentPage
    {
        public exteriahistory()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            backbunton.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";

            adddata();
        }

        public async void adddata()
        {
            var data = await GoogleFirebase.GetAllexterianame();
            var data1 = new ObservableCollection<Models.exteria>();
            int i = 0;
            for (i = 0; i < data.Count; i++)
            {
                if (Global.jobid == data[i].jobid && Global.ChecklistId == data[i].checklistid)
                {
                    data1.Add(data[i]);
                }
            }
            listView.ItemsSource = data1;
        }


      
        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Models.exteria)e.SelectedItem;
            Global.ExteriaId = item.exteriaid;
            Global.nametop = item.name;
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new editpagehistory
                {
                    BindingContext = e.SelectedItem as Models.exteria,
                    Title = "Edit User"

                });



            }
        }
      
        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
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
    public partial class inspechistory : ContentPage
    {
        public inspechistory()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            backbunton.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";

            adddata();
        }
        public async void adddata()
        {
            var data = await GoogleFirebase.GetAllJobCheckList();
            var data1 = new ObservableCollection<jobchecklist>();
            int i = 0;
            for (i = 0; i < data.Count; i++)
            {
                if (Global.jobid == data[i].jobid)
                {
                    data1.Add(data[i]);
                }
            }
            listView.ItemsSource = data1;
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

       

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;

            var SelectedParent = item.BindingContext;
            var x = listView.Parent.BindingContext = SelectedParent;
            var item1 = x as jobchecklist;

            bool answer =
                  await Application.Current
                  .MainPage.DisplayAlert("ยืนยัน > ลบเพลง?", item1.checklist,
                  "ลบ", "ยกเลิก");
            if (answer)
            {
                await GoogleFirebase.DeleteJobChecklist(item1.checklistid, item1.checklist, item1.jobid);
                Navigation.PushAsync(new Inspect());

            }

        }



        private async void listView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                //  var item = (jobchecklist)e.CurrentSelection;
                var item = (jobchecklist)e.CurrentSelection.FirstOrDefault();
                Global.ChecklistId = item.checklistid;
                await Navigation.PushAsync(new exteriahistory
                {
                    BindingContext = e.CurrentSelection as jobchecklist,


                });
            }
        }
    }
}
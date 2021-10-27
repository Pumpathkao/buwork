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
    public partial class Inspect : ContentPage
    {
        private ObservableCollection<inspeclist> _rootobj;
        public Inspect()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            editimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fdocument-editor.png?alt=media&token=f8d40762-2061-4518-882b-04f27379d3c0";
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
            Navigation.PushAsync(new home());
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InspecCheck());
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
                  .MainPage.DisplayAlert("ยืนยัน", item1.checklist,
                  "ลบ", "ยกเลิก");
            if (answer)
            {
                var check = await GoogleFirebase.Getallproblem();
                {
                    for (int i = 0; i < check.Count; i++)
                    {
                        if (item1.checklistid == check[i].checklistid)
                        {
                            await GoogleFirebase.deleteproblem(item1.checklistid);
                        }
                    }

                    await GoogleFirebase.DeleteJobChecklist(item1.checklistid, item1.checklist, item1.jobid);
                    Navigation.PushAsync(new Inspect());

                }

            }
        }

        

        private async void listView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                //  var item = (jobchecklist)e.CurrentSelection;
                var item = (jobchecklist)e.CurrentSelection.FirstOrDefault();
                Global.ChecklistId = item.checklistid;
                await Navigation.PushAsync(new exteria
                {
                    BindingContext = e.CurrentSelection as jobchecklist,


                });
                Global.location = item.checklist;
            }
        }
    }
}
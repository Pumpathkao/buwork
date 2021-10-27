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
    public partial class editpagehistory : ContentPage
    {

        string locationcheck;
        public editpagehistory()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            nTop.Text = Global.nametop;
            buttonback.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";


            showproblem();

            showimage();

        }
        public async void showimage()
        {
            var data = await GoogleFirebase.Getex(Global.ExteriaId);
            imgsorce.Source = data.ImageUrl;

        }

 

      
        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (problem)e.SelectedItem;
                Global.problemid = item.problemid;
                Global.problemname = item.abountproblem;
                await Navigation.PushAsync(new detailproblemhistory
                {
                    BindingContext = e.SelectedItem as Job,
                    Title = "Edit User"

                });


            }
        }
     
        public async void showproblem()
        {
            var data = await GoogleFirebase.GetAllProblem();
            var data1 = new ObservableCollection<Models.problem>();
            int i = 0;
            for (i = 0; i < data.Count; i++)
            {
                if (Global.jobid == data[i].jobid && Global.ChecklistId == data[i].checklistid && Global.ExteriaId == data[i].exteriaId)
                {
                    data1.Add(data[i]);
                }
            }
            listView.ItemsSource = data1;
        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uploadimageeditpage());
        }

        private void buttonback_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
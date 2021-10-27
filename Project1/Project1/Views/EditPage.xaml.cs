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
    public partial class EditPage : ContentPage
    {
        string locationcheck;
        public EditPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            nTop.Text = Global.nametop;
            showproblem();
            plusimg.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fplus.png?alt=media&token=3ad1b6b3-31b8-4f02-a013-b14e4c16ba53";
            buttonback.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";


            showimage();

        }
        public async void showimage()
        {
            var data = await GoogleFirebase.Getex(Global.ExteriaId);
            imgsorce.Source = data.ImageUrl;

        }
      
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new PopupProblem()
            {
                IsLightDismissEnabled = false
            });
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {


            Navigation.ShowPopup(new PopupLocation()
            {
                IsLightDismissEnabled = false
            });


        }
        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (problem)e.SelectedItem;
                Global.problemid = item.problemid;
                Global.problemname = item.abountproblem;
                await Navigation.PushAsync(new detailproblem
                {
                    BindingContext = e.SelectedItem as Job,
                    Title = "Edit User"

                });
              

            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new popupname()
            {
                IsLightDismissEnabled = false
            });
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
            Navigation.PushAsync(new exteria());
        }
    }
}
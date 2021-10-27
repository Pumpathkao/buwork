using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detailproblem : ContentPage
    {
        public detailproblem()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            location.Text = Global.location+" , "+Global.exteria;
            problem.Text = Global.problemname;
            buttonback.Source = "https://firebasestorage.googleapis.com/v0/b/projectfinal-1e184.appspot.com/o/resource%2Fback.png?alt=media&token=f5a24afc-6404-4fa1-b9ff-1cbcafc0e52e";
            addimage();
          
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new uploadimageproblem());
        }
        public async void addimage()
        {
            var data = await GoogleFirebase.GetAllProblem();
            var data1 = "";
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].problemid == Global.problemid)
                {
                    data1 = data[i].ImageUrl;
                    if (data[i].deciption == null)
                    {
                        Global.deciptionproblem = " ";
                    }
                    else
                    {
                        Global.deciptionproblem = data[i].deciption;
                    }
                    
                }
            }
            editor.Text = Global.deciptionproblem;


            img.Source = data1;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var check1 = await GoogleFirebase.Getproblem(Global.problemid);
            check1.location = Global.location;
            check1.exteria = Global.exteria;
            check1.deciption = editor.Text;
            await GoogleFirebase.updateproblem(check1);

            Navigation.PushAsync(new EditPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            bool answer =
                await Application.Current
                .MainPage.DisplayAlert("ยืนยัน > ลบ?", Global.problemname,
                "ลบ", "ยกเลิก");
            if (answer)
            {
                await GoogleFirebase.DeleteJob(Global.problemid);
                Navigation.PushAsync(new EditPage());

            }
        }
    }
}
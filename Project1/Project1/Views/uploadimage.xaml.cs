using Plugin.Media;
using Plugin.Media.Abstractions;
using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class uploadimage : ContentPage
    {
        MediaFile file;
        byte[] imageAsBytes = null;
        string fileName = "";
        string path1 = "";
        public uploadimage()
        {
            InitializeComponent();
        }
        public byte[] StreamToByteArray(Stream pSource)
        {
            using (var memoryStream = new MemoryStream())
            {
                pSource.CopyTo(memoryStream);
                imageAsBytes = memoryStream.ToArray();
            }
            return imageAsBytes;
        }
        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            if (imageAsBytes != null)
            {
                if (Global.fileName == null)
                {

                    using (var streamF = new MemoryStream(imageAsBytes))
                    {
                        await FSHelper.UploadFile(streamF, fileName);
                    }
                }
                else
                {
                    if (Global.fileName == "Default.JPEG")
                    {
                        using (var streamF = new MemoryStream(imageAsBytes))
                        {
                            await FSHelper.UploadFile(streamF, fileName);
                        }
                    }
                    else
                    {
                        await FSHelper.DeleteFile(Global.fileName);
                        using (var streamF = new MemoryStream(imageAsBytes))
                        {
                            await FSHelper.UploadFile(streamF, fileName);
                        }
                    }

                }
               
            }
            Global.fileName = fileName;
            var check = await GoogleFirebase.Getjob(Global.username,Global.jobid);
            check.ImageUrl = Global.imageUrl;

            await GoogleFirebase.UpdateUser(check);
           
            Navigation.PushAsync(new main());
        }
        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;
                path1 = Path.GetExtension(file.Path);
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    fileName = Path.GetFileName(file.Path);
                    var imageAsBytes = StreamToByteArray(file.GetStream());
                    file.Dispose();
                    var stream = new MemoryStream(imageAsBytes);
                    return stream;
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            }); ;

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            imgChoosed.Source = ImageSource.FromStream(() =>
            {
                fileName = Path.GetFileName(file.Path);
                var imageAsBytes = StreamToByteArray(file.GetStream());
                file.Dispose();
                var stream = new MemoryStream(imageAsBytes);
                return stream;
            });
        }
    }
}
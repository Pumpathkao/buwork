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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class uploadprofile : ContentPage
    {
        MediaFile file;
        byte[] imageAsBytes = null;
        string fileName = "";
        string path1 = "";
        string pat1 = "";
        public uploadprofile()
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
                if (Global.fileName1 == null)
                {

                    using (var streamF = new MemoryStream(imageAsBytes))
                    {
                        await FSHelper.UploadFile1(streamF, fileName);
                    }
                }
                else
                {
                    if (Global.fileName1 == "Default.JPEG")
                    {
                        using (var streamF = new MemoryStream(imageAsBytes))
                        {
                            await FSHelper.UploadFile1(streamF, fileName);
                        }
                    }
                    else
                    {
                        await FSHelper.DeleteFile1(Global.fileName1);
                        using (var streamF = new MemoryStream(imageAsBytes))
                        {
                            await FSHelper.UploadFile1(streamF, fileName);
                        }
                    }

                }

            }
            Global.fileName1 = fileName;

            var check1 = await GoogleFirebase.GetUser(Global.username);
            check1.ImageUrl = Global.imageUrl1;
            Global.imageurl1 = check1.ImageUrl;
            await GoogleFirebase.UpdateProfile(check1);

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
                pat1 = Path.GetFullPath(file.Path);
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
                Name = fileName,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            }); ;

            if (file == null)
                return;
            pat1 = Path.GetFullPath(file.Path);
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
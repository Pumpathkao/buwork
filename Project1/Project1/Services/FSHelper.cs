using Firebase.Database;
using Firebase.Storage;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Services
{
   public static  class FSHelper
    { 
      public static FirebaseStorage firebaseStorage =
             new FirebaseStorage("projectfinal-1e184.appspot.com");



    public static string storageID { get; set; }

    public static string RootName { get; set; }

    

    public static async Task<string> UploadFile(Stream fileStream, string fileName)
    {
        Global.imageUrl = await firebaseStorage
             .Child("jobphoto")
             .Child(fileName)
             .PutAsync(fileStream);

        return Global.imageUrl;
    }

    public static async Task<string> GetFile(string fileName)
    {
        return await firebaseStorage
            .Child("jobphoto")
            .Child(fileName)
            .GetDownloadUrlAsync();
    }

    public static async Task DeleteFile(string fileName)
    {
        await firebaseStorage
             .Child("jobphoto")
             .Child(fileName)
             .DeleteAsync();

    }
        public static async Task<string> UploadFile1(Stream fileStream, string fileName)
        {
            Global.imageUrl1 = await firebaseStorage
                 .Child("photoedit")
                 .Child(fileName)
                 .PutAsync(fileStream);

            return Global.imageUrl1;
        }

        public static async Task<string> GetFile1(string fileName)
        {
            return await firebaseStorage
                .Child("photoedit")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public static async Task DeleteFile1(string fileName)
        {
            await firebaseStorage
                 .Child("photoedit")
                 .Child(fileName)
                 .DeleteAsync();

        }
    }
}
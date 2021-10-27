using Plugin.Media;
using Plugin.Media.Abstractions;
using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sendemail : ContentPage
    {
        int num = 0;
        int num11 = 0;
        MediaFile file;
        byte[] imageAsBytes = null;
        string fileName = "";
        string path1 = "";
        string[] pat;

        string name = "";
        public sendemail()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            add();
        }
        public async void add()
        {
            int num1 = 0;
            int a = 0;
             StringBuilder test = new StringBuilder();
            var check = await GoogleFirebase.GetAllProblem();
            pat = new string[check.Count];
   
            for (int y = 0; y < check.Count; y++)
            {
                if (check[y].jobid == Global.jobid)
                {  
                    
                        test.AppendLine("location " + check[y].location + "(" + check[y].exteria + ")");
                        test.AppendLine("Problem " + check[y].abountproblem);
                        test.AppendLine("Level " + check[y].level);
                        test.AppendLine("Picture Name " + check[y].ImageName);
                        test.AppendLine();
                        pat[a] = check[y].partfile;
                    a++;
                    num1++;
                }
            }
            num = num1;
            txtBody.Text = test.ToString();
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

        void btnSend_Clicked(object sender, System.EventArgs e)
        {
            if (!myMultiValidation.IsValid)
            {
                var errorBuilder = new StringBuilder();

                foreach (var error in myMultiValidation.Errors)
                {
                    if (error is string)
                    {
                        errorBuilder.AppendLine(((string)error).ToString());
                    }

                    DisplayAlert("Error", errorBuilder.ToString(), "OK");
                }
            }
            else
            {
                Attachment[] att = new Attachment[num];
                try
                {
                    for (int i = 0; i < num; i++)
                    {

                        att[i] = new Attachment(pat[i]);
                    }
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("pumpath.kant@bumail.net");
                    mail.To.Add(txtTo.Text);
                    mail.Subject = txtSubject.Text;
                    mail.Body = txtBody.Text;
                    for (int y = 0; y < num; y++)
                    {
                        mail.Attachments.Add(att[y]);
                    }

                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("pumpath.kant@bumail.net", "Pumpathkao9");

                    SmtpServer.Send(mail);
                    Navigation.PushAsync(new main());
                }
                catch (Exception ex)
                {
                    DisplayAlert("Faild", ex.Message, "OK");
                }
            }
        }
    }
}
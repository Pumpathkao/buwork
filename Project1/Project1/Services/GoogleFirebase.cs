using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project1.Models;
using Firebase.Database.Query;
using System.Linq;
using System.Diagnostics;

namespace Project1.Services
{
    public static class GoogleFirebase
    {
        public static FirebaseClient firebase =
         new FirebaseClient("https://projectfinal-1e184-default-rtdb.asia-southeast1.firebasedatabase.app/");


        public static async Task<bool> AddUser(Userdata users)
        {
            await firebase
              .Child("User")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> AddJob(Job users)
        {
            await firebase
              .Child("Job")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> addlisall(listall users)
        {
            await firebase
              .Child("checklistname")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> addlocation(location users)
        {
            await firebase
              .Child("location")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> addproblem(problem users)
        {
            await firebase
              .Child("problem")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> addexterianame(exteria users)
        {
            await firebase
              .Child("exterianame")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> AddJobCheckList(jobchecklist users)
        {
            await firebase
              .Child("JobChecklist")
              .PostAsync(users);
            return true;
        }
        public static async Task<bool> UpdateUser(Job users)
        {
            try
            {

                var toUpdateUser = (await firebase
                .Child("Job")
                .OnceAsync<Job>()).Where(a => a.Object.username == users.username && a.Object.jobid == users.jobid).FirstOrDefault();
                await firebase
                .Child("Job")
                .Child(toUpdateUser.Key)
                .PutAsync(users);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> UpdateProfile(Userdata users)
        {
            try
            {

                var toUpdateUser = (await firebase
                .Child("User")
                .OnceAsync<Userdata>()).Where(a => a.Object.Username == users.Username).FirstOrDefault();
                await firebase
                .Child("User")
                .Child(toUpdateUser.Key)
                .PutAsync(users);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> Updateex(exteria users)
        {
            try
            {

                var toUpdateUser = (await firebase
                .Child("exterianame")
                .OnceAsync<exteria>()).Where(a => a.Object.exteriaid == users.exteriaid).FirstOrDefault();
                await firebase
                .Child("exterianame")
                .Child(toUpdateUser.Key)
                .PutAsync(users);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> updateclosejob(Job users)
        {
            try
            {

                var toUpdateUser = (await firebase
                .Child("Job")
                .OnceAsync<Job>()).Where(a => a.Object.jobid == users.jobid).FirstOrDefault();
                await firebase
                .Child("Job")
                .Child(toUpdateUser.Key)
                .PutAsync(users);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> updateproblem(problem users)
        {
            try
            {

                var toUpdateUser = (await firebase
                .Child("problem")
                .OnceAsync<problem>()).Where(a => a.Object.problemid == users.problemid).FirstOrDefault();
                await firebase
                .Child("problem")
                .Child(toUpdateUser.Key)
                .PutAsync(users);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<List<Job>> GetAllJob()
        {
            try
            {
                var Codelist = (await firebase
                .Child("Job")
                .OnceAsync<Job>()).Select(item =>
                new Job
                {
                    address = item.Object.address,
                    email = item.Object.email,
                    jobid = item.Object.jobid,
                    phone = item.Object.phone,
                    title = item.Object.title,
                    date = item.Object.date,
                    name = item.Object.name,
                    username = item.Object.username,
                    img = item.Object.img,
                    round = item.Object.round,
                    dateround = item.Object.dateround,
                    ImageUrl = item.Object.ImageUrl,
                    ImageName1 = item.Object.ImageName1,
                    closejob = item.Object.closejob

                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<jobchecklist>> GetAllJobCheckList()
        {
            try
            {
                var Codelist = (await firebase
                .Child("JobChecklist")
                .OnceAsync<jobchecklist>()).Select(item =>
                new jobchecklist
                {
                    checklist = item.Object.checklist,
                    jobid = item.Object.jobid,
                    checklistid = item.Object.checklistid
                    

                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<location>> Getalllocation()
        {
            try
            {
                var Codelist = (await firebase
                .Child("location")
                .OnceAsync<location>()).Select(item =>
                new location
                {
                    checklistid = item.Object.checklistid,
                    exteriaId = item.Object.exteriaId,
                    jobid = item.Object.jobid,
                    locationidd = item.Object.locationidd,
                    locationname = item.Object.locationname


                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<exteria>> GetAllexterianame()
        {
            try
            {
                var Codelist = (await firebase
                .Child("exterianame")
                .OnceAsync<exteria>()).Select(item =>
                new exteria
                {
                    exteriaid = item.Object.exteriaid,
                    jobid = item.Object.jobid,
                    checklistid = item.Object.checklistid,
                    name = item.Object.name,
                    ImageUrl = item.Object.ImageUrl



                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<problem>> Getallproblem()
        {
            try
            {
                var Codelist = (await firebase
                .Child("problem")
                .OnceAsync<problem>()).Select(item =>
                new problem
                {
                  abountproblem=item.Object.abountproblem,
                  checklistid=item.Object.checklistid,
                  exteriaId=item.Object.exteriaId,
                  ImageName=item.Object.ImageName,
                  ImageUrl=item.Object.ImageUrl,
                  jobid=item.Object.jobid,
                  level=item.Object.level,
                  partfile=item.Object.partfile,
                  problemid=item.Object.problemid,
                  



                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<problem>> GetAllProblem()
        {
            try
            {
                var Codelist = (await firebase
                .Child("problem")
                .OnceAsync<problem>()).Select(item =>
                new problem
                {
                    abountproblem = item.Object.abountproblem,
                    checklistid = item.Object.checklistid,
                    exteriaId = item.Object.exteriaId,
                    jobid = item.Object.jobid,
                    level = item.Object.level,
                    problemid = item.Object.problemid,
                    ImageName=item.Object.ImageName,
                    partfile=item.Object.partfile
                    ,ImageUrl=item.Object.ImageUrl,
                    deciption=item.Object.deciption
                    ,exteria=item.Object.exteria,
                    location=item.Object.location

                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<listall>> Getlistall()
        {
            try
            {
                var Codelist = (await firebase
                .Child("checklistname")
                .OnceAsync<listall>()).Select(item =>
                new listall
                {
                    checkllistname = item.Object.checkllistname


                }).ToList();
                return Codelist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<bool> DeleteLocation(string checkid, string exid, string jobid)
        {


            var toDeletePerson = (await firebase
                .Child("location")
                .OnceAsync<location>()).Where(a => a.Object.checklistid == checkid && a.Object.exteriaId == exid && a.Object.jobid == jobid).FirstOrDefault();
            await firebase.Child("location").Child(toDeletePerson.Key).DeleteAsync();



            return true;
        }
        public static async Task<bool> DeleteJob(string jobid)
        {


            var toDeletePerson = (await firebase
                .Child("problem")
                .OnceAsync<problem>()).Where(a => a.Object.problemid == jobid).FirstOrDefault();
            await firebase.Child("problem").Child(toDeletePerson.Key).DeleteAsync();



            return true;
        }
        public static async Task<List<Userdata>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("User")
                .OnceAsync<Userdata>()).Select(item =>
                new Userdata
                {
                    Username = item.Object.Username,
                    Password = item.Object.Password,
                    Name = item.Object.Name,
                    ImageUrl=item.Object.ImageUrl


                }).ToList();

                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<Userdata> GetUser(string username)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("User")
                .OnceAsync<Userdata>();
                var check = allUsers.Where(a => a.Username == username).FirstOrDefault();
                if (check != null)
                {
                    Global.NameGlobal = check.Name;

                    Global.UsernameGlobal = check.Username;
                    Global.username = check.Username;
                }
                return allUsers.Where(a => a.Username == username).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<Job> Getjob(string username, string jobid)
        {
            try
            {
                var allUsers = await GetAllJob();
                await firebase
                .Child("Job")
                .OnceAsync<Job>();
                var check = allUsers.Where(a => a.username == username && a.jobid == jobid).FirstOrDefault();

                return check;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<exteria> Getex(string exid)
        {
            try
            {
                var allUsers = await GetAllexterianame();
                await firebase
                .Child("exterianame")
                .OnceAsync<exteria>();
                var check = allUsers.Where(a => a.exteriaid == exid).FirstOrDefault();

                return check;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<problem> Getproblem(string exid)
        {
            try
            {
                var allUsers = await GetAllProblem();
                await firebase
                .Child("problem")
                .OnceAsync<problem>();
                var check = allUsers.Where(a => a.problemid == exid).FirstOrDefault();

                return check;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<bool> Updatelocation(location users)
        {
            try
            {

                var toUpdateUser = (await firebase
                .Child("location")
                .OnceAsync<location>()).Where(a => a.Object.checklistid == users.checklistid && a.Object.jobid == users.jobid&&a.Object.locationidd==users.locationidd).FirstOrDefault();
                await firebase
                .Child("location")
                .Child(toUpdateUser.Key)
                .PutAsync(users);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
      
        public static async Task<location> Getlocation(string exid)
        {
            try
            {
                var allUsers = await Getalllocation();
                await firebase
                .Child("location")
                .OnceAsync<location>();
                var check = allUsers.Where(a => a.exteriaId == exid).FirstOrDefault();

                return check;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<bool> DeleteJobChecklist(string checkid, string checklist, string jobid)
        {


            var toDeletePerson = (await firebase
                .Child("JobChecklist")
                .OnceAsync<jobchecklist>()).Where(a => a.Object.checklistid == checkid && a.Object.checklist == checklist && a.Object.jobid == jobid).FirstOrDefault();
            await firebase.Child("JobChecklist").Child(toDeletePerson.Key).DeleteAsync();



            return true;
        }
        public static async Task<bool> Deleteexteria(string checkid, string checklist, string jobid,string exid)
        {


            var toDeletePerson = (await firebase
                .Child("exterianame")
                .OnceAsync<exteria>()).Where(a => a.Object.checklistid == checkid && a.Object.name == checklist && a.Object.jobid == jobid&&a.Object.exteriaid==exid).FirstOrDefault();
            await firebase.Child("exterianame").Child(toDeletePerson.Key).DeleteAsync();



            return true;
        }

        public static async Task<bool> deleteproblem(string checkid)
        {


            var toDeletePerson = (await firebase
                .Child("problem")
                .OnceAsync<problem>()).Where(a => a.Object.checklistid == checkid).FirstOrDefault();
            await firebase.Child("problem").Child(toDeletePerson.Key).DeleteAsync();



            return true;
        }
        public static async Task<bool> deleteproblem1(string checkid)
        {


            var toDeletePerson = (await firebase
                .Child("problem")
                .OnceAsync<problem>()).Where(a => a.Object.exteriaId == checkid).FirstOrDefault();
            await firebase.Child("problem").Child(toDeletePerson.Key).DeleteAsync();



            return true;
        }

    }
}

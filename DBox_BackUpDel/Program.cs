using Dropbox.Api;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DBox_BackUpDel
{
    class Program
    {
        /// <summary>
        /// DBox Backup
        /// </summary>
        /// <param name="args">Input to String Paramater.</param>
        static void Main(string[] args)
        {
            try
            {
                var task = Task.Run((Func<Task>)Program.Run);//to know Email From Token Keys 
                task.Wait();
            }
            catch (Exception ex)
            {
                 ex.ToString();
            }
        }

        /// <summary>
        /// Dropbox Client.
        /// </summary>      
        public static async Task Run()// Put Run() function in Main Args
        {
            ///<remark>
            ///Process of Dropbox Client.
            ///</remark>
            using (var dbx = new DropboxClient("ErgVXRLR7YAAAAAAAAAHR3pQ2vqeFCce7Ym3yKnHoBIRvd9z5q096lDN0R1S6UfC")) //logistics@qbei.co.jp 
            {
                try
                {
                    var delpath = "/物流部/CKM共有フォルダ/Backup/";
                    bool del = Delete(delpath, dbx);
                    if (del)
                    {
                        //have been deleted because of existence
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Deleting Files!!!!!!!!!!!!!!!!!!!!" + ex);
                    Environment.Exit(0);
                }

            }
        }
        public static bool check(string path, DropboxClient dbx)
        {
            try
            {
                DateTime.Now.AddDays(-14).ToString();
                var med = dbx.Files.GetMetadataAsync(path);
                var result = med.Result;
                return true;
            }
            catch (Exception ex)
            {
               return false;
            }

        }
        public static bool Delete(string path, DropboxClient dbx)
        {
            try
            {
               var folders = dbx.Files.DeleteV2Async(path);
               var result = folders.Result;
               return true;
               
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}

























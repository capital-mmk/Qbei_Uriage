using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropBoxClient.Entities;
using DropBoxClient;
using Dropbox.Api.Async;
using Dropbox.Api.FileRequests;
using Dropbox.Api.Files;
using Dropbox.Api.Files.Routes;
using Dropbox.Api;
using System.IO.Compression;
using System.Text.RegularExpressions;
//using System.IO.Compression.FileSystem;
//using Rebex.IO.Compression;
using System.Configuration;

namespace DBox_Upload
{
    /// <summary>
    /// DBox Upload.
    /// </summary>
    class Program
    {
     
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();

        /// <summary>
        /// Process of DBox Upload.
        /// </summary>
        /// <param name="args">Input to String Paramater.</param>
        static void Main(string[] args)
        {
            Console.Title = "Qbei Uriage DBox_Upload Console";
            try
            {
                //test
                var task = Task.Run((Func<Task>)Program.Run);//to know Email From Token Keys 
                task.Wait();
            }
            catch (Exception ex) {
                ConsoleWriteLine_Tofile("Qbei Uriage DBox_Upload Console :" + ex.ToString() + " " + DateTime.Now);
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
            ///
            // System.IO.Compression.ZipFile.CreateFromDirectory(@"C:\Qbei_Uriage\Output", "Output");
            // ZipArchive zip = new ZipArchive(@"C:\archive.zip", ArchiveOpenMode.Open);

            //provide the folder to be zipped
            //string folderToZip = @"C:\Qbei_Uriage\Output";

            //provide the path and name for the zip file to create
            //string zipFile = @"C:\Qbei_Uriage\Output.zip";

            //call the ZipFile.CreateFromDirectory() method
            //ZipFile.CreateFromDirectory(folderToZip, zipFile);

            //t0aOJWxAEtkAAAAAABucL2EqxH_413uaCjUzhckYFjmC8T3kN6zG4-1nFXDKE77k
            using (var dbx = new DropboxClient("ErgVXRLR7YAAAAAAAAAHR3pQ2vqeFCce7Ym3yKnHoBIRvd9z5q096lDN0R1S6UfC")) //logistics@qbei.co.jp 
            //using (var dbx = new DropboxClient("t0aOJWxAEtkAAAAAABnMoNJ4r3uS43_NAlJD57AO-YlUeA5n2bsGlYdT7DtjBXcm")) //for use-zenten@qbei.co.jp, pass-i0lhck5qke
            //using (var fs = new FileStream(fn, FileMode.Open, FileAccess.Read))
            {
                try
                {

                    string[] strarr = Directory.GetFiles(@"C:\Qbei_Uriage\Output");
                    foreach (string fle in strarr)
                    {
                        var file = Path.GetFileName(fle);
                        var folder = "/物流部/CKM共有フォルダ/motionboard/";
                        //var folder = "/CKM共有フォルダ/システム開発/local→MBC/";
                        //Dropbox (きゅうべえ)\CKM共有フォルダ\システム開発\local→MBC,for use-zenten@qbei.co.jp
                        var mem = new MemoryStream(File.ReadAllBytes(@"C:\Qbei_Uriage\Output\" + file));
                        var lastpath = folder + file;
                        //var newpath = "/CKM共有フォルダ/システム開発/local→MBC/t/";
                        var newpath = "/物流部/CKM共有フォルダ/Backup/";



                        //check path
                        bool chk = check(lastpath, dbx);

                        if (chk)
                        {
                            //delete path
                            bool del = Delete(lastpath, dbx);
                            if (del)
                            {
                                //have been deleted because of existence
                            }



                        }
                        //upload path
                        const int ChunkSize = 4096 * 1024;
                        //var task =  Upload("","", dbx);
                        var fileStream = File.Open(fle, FileMode.Open);
                        if (fileStream.Length <= ChunkSize)
                        {
                            //await dbx.Files.UploadAsync(remotePath, body: fileStream);
                            var upload = await dbx.Files.UploadAsync(lastpath, WriteMode.Overwrite.Instance, body: mem);// original code for Small size
                            Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, upload.Rev);

                        }
                        else
                        {
                            ///<remark>
                            ///Check to Data of size.
                            ///</remark>
                            try
                            {
                                int chunkSize = ChunkSize;
                                string path = lastpath;
                                FileStream stream = fileStream;
                                //var cu = ChunkUpload(lastpath, fileStream, ChunkSize, dbx); }
                                ulong numChunks = (ulong)Math.Ceiling((double)stream.Length / chunkSize);
                                byte[] buffer = new byte[chunkSize];
                                string sessionId = null;
                                for (ulong idx = 0; idx < numChunks; idx++)
                                {
                                    var byteRead = stream.Read(buffer, 0, chunkSize);

                                    using (var memStream = new MemoryStream(buffer, 0, byteRead))
                                    {
                                        if (idx == 0)
                                        {
                                            var result = await dbx.Files.UploadSessionStartAsync(false, memStream);

                                            sessionId = result.SessionId;
                                        }
                                        else
                                        {
                                            var cursor = new UploadSessionCursor(sessionId, (ulong)chunkSize * idx);

                                            if (idx == numChunks - 1)
                                            {
                                                FileMetadata fileMetadata = await dbx.Files.UploadSessionFinishAsync(cursor, new CommitInfo(path), memStream);
                                                Console.WriteLine(fileMetadata.PathDisplay);
                                            }
                                            else
                                            {
                                                await dbx.Files.UploadSessionAppendV2Async(cursor, false, memStream);
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                         

                        }
                        //var ul = await Upload("sd","sd");
                        //var upload = await dbx.Files.UploadAsync(lastpath, WriteMode.Overwrite.Instance, body: mem);// original code for Small size
                        //Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, upload.Rev);


                        //Move path (Copy)
                        try {
                            bool move = Move1(lastpath, newpath, dbx);
                        }
                      
                        catch
                {
                }
                      
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Uploading Files!!!!!!!!!!!!!!!!!!!!" + ex);
                    Environment.Exit(0);
                }

            }

        }

        //public static async Task Upload(string localPath, string remotePath, DropboxClient dbx)
        //{
           
        //    using (var fileStream = File.Open(localPath, FileMode.Open))
        //    {
              
        //        //else
        //        //{
        //        ChunkUpload(remotePath, fileStream, (int)ChunkSize);
        //        //}
        //    }
        //}

        //public static async Task ChunkUpload(String path, FileStream stream, int chunkSize, DropboxClient dbx)
        //{
            //ulong numChunks = (ulong)Math.Ceiling((double)stream.Length / chunkSize);
            //byte[] buffer = new byte[chunkSize];
            //string sessionId = null;
            //for (ulong idx = 0; idx < numChunks; idx++)
            //{
            //    var byteRead = stream.Read(buffer, 0, chunkSize);

            //    using (var memStream = new MemoryStream(buffer, 0, byteRead))
            //    {
            //        if (idx == 0)
            //        {
            //            var result = await dbx.Files.UploadSessionStartAsync(false, memStream);
                       
            //            sessionId = result.SessionId;
            //        }
            //        else
            //        {
            //            var cursor = new UploadSessionCursor(sessionId, (ulong)chunkSize * idx);

            //            if (idx == numChunks - 1)
            //            {
            //                FileMetadata fileMetadata = await dbx.Files.UploadSessionFinishAsync(cursor, new CommitInfo(path), memStream);
            //                Console.WriteLine(fileMetadata.PathDisplay);
            //            }
            //            else
            //            {
            //                await dbx.Files.UploadSessionAppendV2Async(cursor, false, memStream);
            //            }
            //        }
            //    }
            //}
        //}
        public static bool check(string path, DropboxClient dbx)
        {
            try
            {
                var med = dbx.Files.GetMetadataAsync(path);
                var result = med.Result;
                return true;
            }
            catch(Exception ex)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage DBox_Upload Console :" + ex.ToString() + " " + DateTime.Now);
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
                ConsoleWriteLine_Tofile("Qbei Uriage DBox_Delete Console :" + ex.ToString() + " " + DateTime.Now);
                return false;
            }
        }
        public static bool Move1(string path, string newpath, DropboxClient dbx)
        {
            try
            {
                // here to Check Copy Data
                var med = dbx.Files.CopyV2Async(path,newpath+Path.GetFileNameWithoutExtension(path)+DateTime.Now.ToString("yyyymmdd")+".csv");
                //var med = dbx.Files.CopyV2Async(path, Path.Combine(newpath ,string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(path) + DateTime.Now.ToString("yymmdd"), ".csv")));
                var result = med.Result;
                ConsoleWriteLine_Tofile(" Dropbox Upload is completed: " + DateTime.Now);
                return true;
            }
            catch (Exception ex)       //private static async Task FileUploadToDropbox(string filePath, string fileName,string fileSource)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage DBox_Upload Console :" + ex.ToString() + " " + DateTime.Now);
                return false;
            }
        }
        public static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Uriage_DBoxUpload_Console.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
        ///For DropBOX TPOKEN and API
        ///To get Accesss token Key and password From Dropbox you  have to make 
        ///Go  https://dl.dropboxusercontent.com/spa/pjlfdak1tmznswp/api_keys.js/public/index.html?key=&secret= to Decrypt and Encrypt
        ///Go https://www.dropbox.com/developers/documentation/dotnet#tutorial For Download and Upload Tutorial
        ///Go https://www.dropbox.com/developers/apps/create to Create Access token words and key, pass  and API,
        ///You Will Found three Steps in that link, And then Choose Drop.api... and Choose Full... and put Name... And Press Create and after Wil appear Steps in EMAIL STEP and You will find All  
    }
}

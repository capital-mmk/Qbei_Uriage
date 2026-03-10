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
using System.Threading.Tasks;
using System.IO.Compression;

namespace DBox_Up
{
   class Program
    {
       //static string fileSource = @"C:\Users\Dell\Desktop\Up\Output.rar";
       //static string filePath = @"‪Dropbox\DB_1\phyoe.png";
       //static string fileName = @"‪C:\Users\Dell\Desktop\new.docx";
        static void Main(string[] args)
        {
           //var task = Task.Run((Func<Task>)Program.FileUploadToDropbox);
           //task.Wait();
            var task = Task.Run((Func<Task>)Program.Run);//to know Email From Token Keys 
            task.Wait();

        }
        static async Task Run()// Put Run() function in Main Args
        {
            //using (var dbx = new DropboxClient("EhWz-yXGpmAAAAAAAAAABhsNHY7CbJhEEI-wJCv6-kn2u0QDjWt-PVlPSSV--PXF"))
            //{
            //    var full = await dbx.Users.GetCurrentAccountAsync();
            //    Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            //}
           
            //using (var dbx = new DropboxClient("EhWz-yXGpmAAAAAAAAAABhsNHY7CbJhEEI-wJCv6-kn2u0QDjWt-PVlPSSV--PXF"))
            string fileSource = @"C:\Qbei_Uriage\Uriage_CSV.zip";
            string fname=@"C:\Qbei_Uriage\Output";
            try
            {
                ZipFile.CreateFromDirectory(fname, fileSource, CompressionLevel.Optimal, true);
            }
            catch
            {
                Environment.Exit(0);
            }

          
            //using (var dbx = new DropboxClient("EhWz-yXGpmAAAAAAAAAABhsNHY7CbJhEEI-wJCv6-kn2u0QDjWt-PVlPSSV--PXF"))


//            App key
//k17e2hw3ibuetcj
//App secret
//e2u5oxpgsadgaw4
            using (var dbx = new DropboxClient("t0aOJWxAEtkAAAAAABnMoNJ4r3uS43_NAlJD57AO-YlUeA5n2bsGlYdT7DtjBXcm"))
            using (var fs = new FileStream( fileSource, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    var updated = await dbx.Files.UploadAsync("/CKM共有フォルダ/システム開発/【output】urigae/" + DateTime.Now.ToShortDateString() + Path.GetFileName(fileSource), WriteMode.Overwrite.Instance, body: fs);
                    if (File.Exists(fileSource))
                    {
                        File.Delete(fileSource);
                    }  
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Uploading Files!!!!!!!!!!!!!!!!!!!!" + ex);
                    Environment.Exit(0);
                }
                
            }

        }

       
        //private static async Task FileUploadToDropbox(string filePath, string fileName,string fileSource)
        //{
        //    using (var dbx = new DropboxClient("EhWz-yXGpmAAAAAAAAAABhsNHY7CbJhEEI-wJCv6-kn2u0QDjWt-PVlPSSV--PXF"))
        //    using (var fs = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
        //    {
        //        var updated = await dbx.Files.UploadAsync( (filePath + "/" + fileName), WriteMode.Overwrite.Instance, body: fs);
        //    }
        //}


        //private async Task Upload(string localPath, string remotePath)
        //{
        //    const int ChunkSize = 4096 * 1024;
        //    using (var fileStream = File.Open(localPath, FileMode.Open))
        //    {
        //        if (fileStream.Length <= ChunkSize)
        //        {
        //            //await this.client.Files.UploadAsync(remotePath, body: fileStream);
        //        }
        //        else
        //        {
        //            await this.ChunkUpload(remotePath, fileStream, (int)ChunkSize);
        //        }
        //    }
        //}
   
      
        //private void uploasd()
        //{
            //var appKey = "<APP KEY>";
            //var appSecret = "<APP SECRET>";
            
            //this.client = new DropboxAppClient(appKey, appSecret);


            //var oauth1AccessTokenKey = "<OAUTH 1 ACCESS TOKEN KEY>";
            //var oauth1AccessTokenSecret = "<OAUTH 1 ACCESS TOKEN SECRET>";
            
            //var tokenFromOAuth1Result = await this.client.Auth.TokenFromOauth1Async(oauth1AccessTokenKey, oauth1AccessTokenSecret);
            //Console.WriteLine(tokenFromOAuth1Result.Oauth2Token);
        //}
    

     

        //private async Task ChunkUpload(String path, FileStream stream, int chunkSize)
        //{
        //    ulong numChunks = (ulong)Math.Ceiling((double)stream.Length / chunkSize);
        //    byte[] buffer = new byte[chunkSize];
        //    string sessionId = null;
        //    for (ulong idx = 0; idx < numChunks; idx++)
        //    {
        //        var byteRead = stream.Read(buffer, 0, chunkSize);

        //        using (var memStream = new MemoryStream(buffer, 0, byteRead))
        //        {
        //            if (idx == 0)
        //            {
        //                //var result = await this.client.Files.UploadSessionStartAsync(false, memStream);
        //                //sessionId = result.SessionId;
        //            }
        //            else
        //            {
        //                var cursor = new UploadSessionCursor(sessionId, (ulong)chunkSize * idx);

        //                if (idx == numChunks - 1)
        //                {
        //                    //FileMetadata fileMetadata = await this.client.Files.UploadSessionFinishAsync(cursor, new CommitInfo(path), memStream);
        //                    //Console.WriteLine(fileMetadata.PathDisplay);
        //                }
        //                else
        //                {
        //                    //await this.client.Files.UploadSessionAppendV2Async(cursor, false, memStream);
        //                }
        //            }
        //        }
        //    }
        //}
        ///For DropBOX TPOKEN and API
       ///To get Accesss token Key and password From Dropbox you  have to make 
        ///Go  https://dl.dropboxusercontent.com/spa/pjlfdak1tmznswp/api_keys.js/public/index.html?key=&secret= to Decrypt and Encrypt
        ///Go https://www.dropbox.com/developers/documentation/dotnet#tutorial For Download and Upload Tutorial
        ///Go https://www.dropbox.com/developers/apps/create to Create Access token words and key, pass  and API,
        ///You Will Found three Steps in that link, And then Choose Drop.api... and Choose Full... and put Name... And Press Create and after Wil appear Steps in EMAIL STEP and You will find All  
    }
}

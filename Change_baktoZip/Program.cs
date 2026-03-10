using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO.Compression;
using System.IO;
namespace Change_baktoZip
{
     class Program
    {
        /// <summary>
        /// Qbei Uriage of backup.
        /// </summary>
        public static string dbname = "Qbei_Uriage";
        //public static string Connection = ConfigurationManager.ConnectionStrings["Qbei_Uriage"].ToString();
        public static string BackUpLoc = ConfigurationManager.AppSettings["BackUp_Loc"].ToString();

        /// <summary>
        /// Backup to change of zip file.
       /// </summary>
        static void Main(string[] args)
        {
            //provide the path and name for the zip file to create
            //string zipFile = BackUpLoc;

            ////call the ZipFile.CreateFromDirectory() method
            //ZipFile.CreateFromDirectory(zipFile, zipFile);


            //    //provide the folder to be zipped
            //    string folderToZip = @"c:\Temp\ZipSample";

            //    //provide the path and name for the zip file to create
            //    string zipFile = @"c:\Temp\ZipSampleOutput\MyZippedDocuments.zip";

            //    //call the ZipFile.CreateFromDirectory() method
            //    ZipFile.CreateFromDirectory(folderToZip, zipFile);
            //}

            string[] fileEntries = Directory.GetFiles(BackUpLoc);


            foreach (var fname in fileEntries)
            {
                if (Path.GetExtension(fname).Contains("bak"))
                {
                    changezip(fname);
                    File.Delete(fname);
                }
            }
           
           
        }
     protected static void changezip(string fname)
     {
         using (FileStream fs = new FileStream(@"C:\DBBackUp\" + Path.GetFileNameWithoutExtension(fname) + ".zip", FileMode.Create))
         using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
         {
             arch.CreateEntryFromFile(@"C:\DBBackUp\" + Path.GetFileName(fname), Path.GetFileName(fname));
         }
     }
    }
}

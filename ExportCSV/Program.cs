using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;
using CsvHelper;
using System.Data.SqlClient;
using System.Configuration;

namespace Qbei_Uriage_ExportCSV
{
    /// <summary>
    /// Uriage_ExportCSV Process.
    /// </summary>
    class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Qbei Uriage ExportCSV Console";
                ConsoleWriteLine_Tofile("Qbei Uriage ExportCSV Console : " + DateTime.Now);
                string sortedDate = "売上日 DESC";
                string path1 = string.Empty;
                string path2 = string.Empty;
                DataTable dtAll = new DataTable();
                DataTable dtAll2 = new DataTable();
                //string MyConnection = "Data Source=WIN-OIL4TFU9NBH\\LOCAL2014;Initial Catalog=Qbei_Uriage;User ID=sa;Password=admin123456!";

                ///<remark>
                ///DataBase of Connection.
                ///</remark>
                //string MyConnection = "Data Source=WIN-SLTEP5OTBR0\\SQLEXPRESS;Initial Catalog=Qbei_Uriage;User ID=Qbei;Password=dW7MmEtnL";
                string MyConnection = "Data Source= 133.167.121.63, 1433;Initial Catalog=Qbei_Uriage;Persist Security Info=True;User ID=sa;Password=manager@0";
                SqlConnection con = new SqlConnection(MyConnection);
                SqlCommand cmd = new SqlCommand("ExportCSV1", con);
                SqlDataAdapter MyAdapter = new SqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dtCSV1 = new DataTable();
                //cmd.CommandTimeout = 3000;
                MyAdapter.Fill(dtCSV1);

                int year = DateTime.Now.Year;
                String sorted = "売上日 DESC";
                //DataRow[] rows = newTable.Select("date >= #" + from_date + "# AND date <= #" + to_date + "#");

                DataTable temp1 = new DataTable();

                ///<remark>
                ///FOR OUTPUT 1.
                ///</remark>
                for (int i = 0; i < 4; i++)
                {
                    DateTime firstDay = new DateTime(year, 1, 1);
                    DateTime lastDay = new DateTime(year, 12, 31);
                    String f = String.Format("{0:yyyy-MM-dd}", firstDay);
                    String l = String.Format("{0:yyyy-MM-dd}", lastDay);

                    if (!File.Exists("C:\\Qbei_Uriage\\Output\\" + year + "Output1.csv"))
                    {
                        ///<remark>
                        ///for 4 years of CSV path.
                        ///</remark>
                        //path1 = "D:\\Qbei_Uriage\\Output\\" + year + "Output1.csv";

                        path1 = "C:\\Qbei_Uriage\\Output\\Output1.csv";

                        DataRow[] results = dtCSV1.Select("売上日 >= #" + f + "# AND 売上日 <= #" + l + "#", sorted);

                        if (results.Count() != 0)
                        {

                            temp1 = results.CopyToDataTable();
                            temp1.Columns.Remove("Date");

                            //for 4 CSV path
                            //temp1.ToCSV(path1);

                        }

                    }

                    year = year - 1;
                    dtAll.Merge(temp1, true);
                    if (i == 3)
                    {
                        DataView dv1 = dtAll.DefaultView;
                        dv1.Sort = sortedDate;

                        //DataRow[] drow = dtAll.Select("*",sortedDate);
                        dtAll = dv1.ToTable();
                        dtAll.ToCSV(path1);
                        break;
                    }
                }
                ConsoleWriteLine_Tofile("Output1 Exoport_CSV complete successfully!!!" + DateTime.Now);
                dtAll.Clear();
                //temp1.ToCSV(path1);

                ///<remark>
                ///DataBase of Connection.
                ///</remark>
                SqlCommand cmd1 = new SqlCommand("ExportCSV2", con);
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd1;
                cmd1.CommandTimeout = 0;
                cmd1.CommandType = CommandType.StoredProcedure;
                DataTable dtCSV2 = new DataTable();
                adp.Fill(dtCSV2);


                int syear = DateTime.Now.Year;
                DataTable temp = new DataTable();

                ///<remark>
                ///FOR OUTPUT 2.
                ///</remark>

                for (int i = 0; i < 4; i++)
                {
                    DateTime firstDay = new DateTime(syear, 1, 1);
                    DateTime lastDay = new DateTime(syear, 12, 31);
                    String f1 = String.Format("{0:yyyy-MM-dd}", firstDay);
                    String l1 = String.Format("{0:yyyy-MM-dd}", lastDay);

                    //string path2 = "D:\\Qbei_Uriage\\Output\\" + syear + "Output2.csv";
                    path2 = "C:\\Qbei_Uriage\\Output\\Output2.csv";
                    if (!File.Exists("C:\\Qbei_Uriage\\Output\\" + syear + "Output2.csv"))
                    {

                        DataRow[] results = dtCSV2.Select("売上日 >= #" + f1 + "# AND 売上日 <= #" + l1 + "#", sorted);
                        if (results.Count() != 0)
                        {
                            temp = results.CopyToDataTable();
                            temp.Columns.Remove("Date");
                            //temp.ToCSV(path2);
                        }
                    }


                    syear = syear - 1;

                    dtAll2.Merge(temp, true);
                    if (i == 3)
                    {
                        DataView dv1 = dtAll2.DefaultView;
                        dv1.Sort = sortedDate;

                        //DataRow[] drow = dtAll2.Select("*", sortedDate);
                        dtAll2 = dv1.ToTable();
                        dtAll2.ToCSV(path2);
                        break;
                    }
                }
                ConsoleWriteLine_Tofile("Output2 Exoport_CSV complete successfully!!!" + DateTime.Now);
            }
            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage Export_CSV Console :" + ex.ToString() + " " + DateTime.Now);
            }

        }


        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Uriage_ExportCSV_Console.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }
    }
}

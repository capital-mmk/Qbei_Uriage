using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Qbei_Uriage
{
    public partial class DBBackUp : System.Web.UI.Page
    {
        public static string dbname = "Qbei_Uriage";
        public static string Connection = ConfigurationManager.ConnectionStrings["Qbei_Uriage"].ToString();
        public static string BackUpLoc = ConfigurationManager.AppSettings["BackUp_Loc"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_meg.Visible = false;
        }
        protected void btn_bkp(object sender, EventArgs e)
        {
            try
            {
                string pok = BackupUriage();
                if (pok == "true")
                {
                    lbl_meg.Visible = true;
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());

            }
        }
        public static string BackupUriage()
        {
            BackUp(dbname);
            return "true";
        }
         public static void BackUp(string DBName)
        {
            try
            {
                DateTime start = DateTime.Now;
                SqlConnection conn = new SqlConnection(Connection);
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = new SqlCommand(" BACKUP DATABASE @DBName TO DISK = @PATH ", conn);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddWithValue("@DBName", DBName);
                string FormattedDate = DateTime.Now.ToString("yyyyMMddHHmmss");


                string Path = BackUpLoc + "System" + DBName + "(" + FormattedDate + ").bak";
                sqlCommand.Parameters.AddWithValue("@PATH", Path);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                //GlobalUI.MessageBox("Database Backup Successful!!!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
    }
         private void delete_Extra()
         {
             string formatString = "yyyyMMddHHmmss";
             string sample = "20100611221912";
             DateTime dt = DateTime.ParseExact(sample, formatString, null);
         }
}
}
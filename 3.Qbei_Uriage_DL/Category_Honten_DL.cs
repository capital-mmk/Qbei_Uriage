using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace _3.Qbei_Uriage_DL
{
    public class Category_Honten_DL : Base_DL
    {
        public bool Category_Honten_InsertXML(DataTable dt)
        {
            string xmlCsv = DataTableToXml(dt);
            SqlConnection con = GetConnection();
            //con.ConnectionString = ConfigurationSettings.AppSettings["QbeiUriage_DB"];
            SqlCommand cmd = new SqlCommand("InsertXMLCategory_Honten", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@xmlCsv", xmlCsv);
            cmd.CommandTimeout = 0;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //string xml = DataTableToXml(dt);
            return true;
        }

       
    }
}

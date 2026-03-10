using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Qbei_Uriage_DL
{
    public class Site_Category_DL : Base_DL
    {
        public bool SiteCategory_InsertXml(DataTable dt)
        {
            string xmlCsv = DataTableToXml(dt);
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("InsertXML_SiteCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@xmlCsv", xmlCsv);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
    }
}

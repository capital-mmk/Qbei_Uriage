using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Qbei_Uriage_DL
{
    public class Item_DL : Base_DL
    {
        public bool Item_InsertXml(DataTable dt)
        {
            string xmlCsv = DataTableToXml(dt);
            //xmlCsv = System.Security.SecurityElement.Escape(xmlCsv);
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("InsertXML_Item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@xmlCsv", xmlCsv.Replace("&", "&amp;"));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        public bool ItemSKU_InsertXml(DataTable dt)
        {
            string xmlCsv = DataTableToXml(dt);
            //xmlCsv = System.Security.SecurityElement.Escape(xmlCsv);
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("InsertXML_ItemSKU", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@xmlCsv", xmlCsv.Replace("&", "&amp;"));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
    }
}

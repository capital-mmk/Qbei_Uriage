using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4.Qbei_Uriage_Common;

namespace _3.Qbei_Uriage_DL
{
    public class Brand_DL : Base_DL
    {
        public bool Brand_InsertXml(DataTable dt)
        {
            string xmlCsv = DataTableToXml(dt);
            SqlConnection con = GetConnection();
            //con.ConnectionString = ConfigurationSettings.AppSettings["QbeiUriage_DB"];
            SqlCommand cmd = new SqlCommand("InsertXML_Brand", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@xmlCsv", xmlCsv);
            cmd.CommandTimeout = 0;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //string xml = DataTableToXml(dt);
            return true;
        }
        public DataTable brand_SelectAll()
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter("Brand_SelectAll", con);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public DataTable Brand_Search(BrandEntity be)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();
           
            SqlDataAdapter cmd = new SqlDataAdapter("Brand_Search", sqlcon);
            if (string.IsNullOrWhiteSpace(be.BrandCode))
                cmd.SelectCommand.Parameters.AddWithValue("@BrandCode", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BrandCode", be.BrandCode);

            if (string.IsNullOrWhiteSpace(be.BrandName))
                cmd.SelectCommand.Parameters.AddWithValue("@BrandName", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BrandName", be.BrandName);

            if (string.IsNullOrWhiteSpace(be.BrandUrl))
                cmd.SelectCommand.Parameters.AddWithValue("@BrandUrl", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BrandUrl", be.BrandUrl);

            if (string.IsNullOrWhiteSpace(be.Modified_Date))
                cmd.SelectCommand.Parameters.AddWithValue("@Modified_Date", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Modified_Date", be.Modified_Date);

            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmd.SelectCommand.Connection.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
    }
}

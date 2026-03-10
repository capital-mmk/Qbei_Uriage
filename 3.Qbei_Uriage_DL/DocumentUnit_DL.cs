using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4.Qbei_Uriage_Common;
using System.Data;
using System.Data.SqlClient;

namespace _3.Qbei_Uriage_DL
{
   public class DocumentUnit_DL:Base_DL
    {
        public DataTable DocumentUnit_Search(DocumentUnit_Entity de)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();

            SqlDataAdapter cmd = new SqlDataAdapter("DocumentUnit_Search", sqlcon);

            if (string.IsNullOrWhiteSpace(de.SaleDate))
                cmd.SelectCommand.Parameters.AddWithValue("@SaleDate", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@SaleDate", de.SaleDate);

            if (string.IsNullOrWhiteSpace(de.ShippingCost))
                cmd.SelectCommand.Parameters.AddWithValue("@ShippingCost", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@ShippingCost", de.ShippingCost);

            if (string.IsNullOrWhiteSpace(de.OrderNo))
                cmd.SelectCommand.Parameters.AddWithValue("@OrderNo", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@OrderNo", de.OrderNo);

            if (string.IsNullOrWhiteSpace(de.ConsumptionTax))
                cmd.SelectCommand.Parameters.AddWithValue("@ConsumptionTax", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@ConsumptionTax", de.ConsumptionTax);

            if (string.IsNullOrWhiteSpace(de.TotalAmount))
                cmd.SelectCommand.Parameters.AddWithValue("@TotalAmount", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@TotalAmount", de.TotalAmount);

            if (string.IsNullOrWhiteSpace(de.BranchName))
                cmd.SelectCommand.Parameters.AddWithValue("@BranchName", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BranchName", de.BranchName);

            if (string.IsNullOrWhiteSpace(de.Cod))
                cmd.SelectCommand.Parameters.AddWithValue("@Cod", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Cod", de.Cod);

            if (string.IsNullOrWhiteSpace(de.UsagePoint))
                cmd.SelectCommand.Parameters.AddWithValue("@UsagePoint", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@UsagePoint", de.UsagePoint);

            if (string.IsNullOrWhiteSpace(de.AdditionalPoint))
                cmd.SelectCommand.Parameters.AddWithValue("@AdditionalPoint", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@AdditionalPoint", de.AdditionalPoint);

         
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmd.SelectCommand.Connection.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public DataTable DocumentUnit_SelectAll()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();

            SqlDataAdapter cmd = new SqlDataAdapter("DocumentUnit_SelectAll", sqlcon);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlcon.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
    }
}


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
    public class DetailItemUnit_DL : Base_DL
    {
       public DataTable DetailItemUnit_Search(DetailItemUnit_Entity de)
       {
           DataTable dt = new DataTable();
           SqlConnection sqlcon = GetConnection();

           SqlDataAdapter cmd = new SqlDataAdapter("DetailItemUnit_Search", sqlcon);

           if (string.IsNullOrWhiteSpace(de.SaleDate))
               cmd.SelectCommand.Parameters.AddWithValue("@SaleDate", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@SaleDate", de.SaleDate);

           if (string.IsNullOrWhiteSpace(de.Coupon))
               cmd.SelectCommand.Parameters.AddWithValue("@Coupon", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@Coupon", de.Coupon);

           if (string.IsNullOrWhiteSpace(de.OrderNo))
               cmd.SelectCommand.Parameters.AddWithValue("@OrderNo", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@OrderNo", de.OrderNo);

           if (string.IsNullOrWhiteSpace(de.PartNo))
               cmd.SelectCommand.Parameters.AddWithValue("@PartNo", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@PartNo", de.PartNo);

           if (string.IsNullOrWhiteSpace(de.Quantity))
               cmd.SelectCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@Quantity", de.Quantity);

           if (string.IsNullOrWhiteSpace(de.Cost))
               cmd.SelectCommand.Parameters.AddWithValue("@Cost", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@Cost", de.Cost);

           if (string.IsNullOrWhiteSpace(de.Amount))
               cmd.SelectCommand.Parameters.AddWithValue("@Amount", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@Amount", de.Amount);

           if (string.IsNullOrWhiteSpace(de.UnitPrice))
               cmd.SelectCommand.Parameters.AddWithValue("@UnitPrice", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@UnitPrice", de.UnitPrice);

           if (string.IsNullOrWhiteSpace(de.Discount))
               cmd.SelectCommand.Parameters.AddWithValue("@Discount", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@Discount", de.Discount);

           if (string.IsNullOrWhiteSpace(de.BranchName))
               cmd.SelectCommand.Parameters.AddWithValue("@BranchName", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@BranchName", de.BranchName);

           if (string.IsNullOrWhiteSpace(de.ModifiedDate))
               cmd.SelectCommand.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@ModifiedDate", de.ModifiedDate);

           cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
           cmd.SelectCommand.Connection.Open();
           cmd.Fill(dt);

           cmd.SelectCommand.Connection.Close();
           return dt;
       }
       public DataTable DetailItemUnit_SelectAll()
       {
           DataTable dt = new DataTable();
           SqlConnection sqlcon = GetConnection();

           SqlDataAdapter cmd = new SqlDataAdapter("DetailItemUnit_SelectAll", sqlcon);
           cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
           sqlcon.Open();
           cmd.Fill(dt);

           cmd.SelectCommand.Connection.Close();
           return dt;
       }
    }
}

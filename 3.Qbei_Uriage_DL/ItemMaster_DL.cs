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
    public class ItemMaster_DL : Base_DL
    {
        
        public void Stage(string proName)
        {
            try
            {
            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand(proName, sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        public bool ItemMaster_InsertXml(DataTable dt)
        {
            string xmlCsv = DataTableToXml(dt);
            SqlConnection con = GetConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Qbei_Uriage"].ToString();
            SqlCommand cmd = new SqlCommand("InsertXML_ItemMaster", con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@xmlCsv", xmlCsv);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
            //string xml = DataTableToXml(dt);
            return true;
        }

        public DataTable GetItemMaster()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Qbei_Uriage_GetItemMaster", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                cmd.Connection.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return new DataTable();
            }
            finally
            {
                cmd.Connection.Close();
            }
         }
        public DataTable Item_SelectAll()
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter("Item_SelectAll", con);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public DataTable ItemMaster_SelectAll()
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter("ItemMaster_SelectAll", con);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public DataTable Item_Search(ItemEntity ie)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();

            SqlDataAdapter cmd = new SqlDataAdapter("Item_Search", sqlcon);
          
            if (string.IsNullOrWhiteSpace(ie.PartNo))
                cmd.SelectCommand.Parameters.AddWithValue("@PartNo", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@PartNo", ie.PartNo);

            if (string.IsNullOrWhiteSpace(ie.BrandCode))
                cmd.SelectCommand.Parameters.AddWithValue("@BrandCode", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BrandCode", ie.BrandCode);

            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmd.SelectCommand.Connection.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public DataTable ItemMaster_Search(ItemMaster_Entity ie)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();

            SqlDataAdapter cmd = new SqlDataAdapter("ItemMaster_Search", sqlcon);

            if (string.IsNullOrWhiteSpace(ie.SaleDate))
                cmd.SelectCommand.Parameters.AddWithValue("@SaleDateFrom", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@SaleDateFrom", ie.SaleDate);

            if (string.IsNullOrWhiteSpace(ie.SaleDateTo))
                cmd.SelectCommand.Parameters.AddWithValue("@SaleDateTo", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@SaleDateTo", ie.SaleDateTo);

            if (string.IsNullOrWhiteSpace(ie.CancelDate))
                cmd.SelectCommand.Parameters.AddWithValue("@CancelDate", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@CancelDate", ie.CancelDate);

            if (string.IsNullOrWhiteSpace(ie.OrderNo))
                cmd.SelectCommand.Parameters.AddWithValue("@OrderNo", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@OrderNo", ie.OrderNo);

            if (string.IsNullOrWhiteSpace(ie.PartNo))
                cmd.SelectCommand.Parameters.AddWithValue("@PartNo", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@PartNo", ie.PartNo);

            if (string.IsNullOrWhiteSpace(ie.UnitPrice))
                cmd.SelectCommand.Parameters.AddWithValue("@UnitPrice", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@UnitPrice", ie.UnitPrice);

            if (string.IsNullOrWhiteSpace(ie.Quantity))
                cmd.SelectCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Quantity", ie.Quantity);

            if (string.IsNullOrWhiteSpace(ie.Cost))
                cmd.SelectCommand.Parameters.AddWithValue("@Cost", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Cost", ie.Cost);

            if (string.IsNullOrWhiteSpace(ie.Amount))
                cmd.SelectCommand.Parameters.AddWithValue("@Amount", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Amount", ie.Amount);

            if (string.IsNullOrWhiteSpace(ie.ShippingCost))
                cmd.SelectCommand.Parameters.AddWithValue("@ShippingCost", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@ShippingCost", ie.ShippingCost);

            if (string.IsNullOrWhiteSpace(ie.BranchCode))
                cmd.SelectCommand.Parameters.AddWithValue("@BranchCode", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BranchCode", ie.BranchCode);

            if (string.IsNullOrWhiteSpace(ie.DeliveryCharge))
                cmd.SelectCommand.Parameters.AddWithValue("@DeliveryCharge", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@DeliveryCharge", ie.DeliveryCharge);

            if (string.IsNullOrWhiteSpace(ie.UsagePoint))
                cmd.SelectCommand.Parameters.AddWithValue("@UsagePoint", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@UsagePoint", ie.UsagePoint);

            if (string.IsNullOrWhiteSpace(ie.Coupon))
                cmd.SelectCommand.Parameters.AddWithValue("@Coupon", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Coupon", ie.Coupon);

            if (string.IsNullOrWhiteSpace(ie.Discount))
                cmd.SelectCommand.Parameters.AddWithValue("@Discount", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Discount", ie.Discount);

            if (string.IsNullOrWhiteSpace(ie.Modified_Date))
                cmd.SelectCommand.Parameters.AddWithValue("@Modified_Date", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Modified_Date", ie.Modified_Date.Replace("/","-"));

            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmd.SelectCommand.Connection.Open();
            cmd.Fill(dt);


            cmd.SelectCommand.Connection.Close();
            return dt;
        }
    }
}

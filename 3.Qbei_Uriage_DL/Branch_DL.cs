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
   public class Branch_DL:Base_DL
    {
        public DataTable Branch_SelectAll()
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter("Branch_SelectAll", con);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public DataTable Branch_Search(Branch_Entity be)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlcon = GetConnection();

            SqlDataAdapter cmd = new SqlDataAdapter("Branch_Search", sqlcon);

            if (string.IsNullOrWhiteSpace(be.BranchCode))
                cmd.SelectCommand.Parameters.AddWithValue("@BranchCode", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BranchCode", be.BranchCode);

            if (string.IsNullOrWhiteSpace(be.BranchName))
                cmd.SelectCommand.Parameters.AddWithValue("@BranchName", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BranchName", be.BranchName);

            if (string.IsNullOrWhiteSpace(be.BrandShortName))
                cmd.SelectCommand.Parameters.AddWithValue("@BrandShortName", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@BrandShortName", be.BrandShortName);

            if (string.IsNullOrWhiteSpace(be.SotreCategory))
                cmd.SelectCommand.Parameters.AddWithValue("@SotreCategory", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@SotreCategory", be.SotreCategory);

            if (string.IsNullOrWhiteSpace(be.Summary))
                cmd.SelectCommand.Parameters.AddWithValue("@Summary", DBNull.Value);
            else cmd.SelectCommand.Parameters.AddWithValue("@Summary", be.Summary);

        

            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmd.SelectCommand.Connection.Open();
            cmd.Fill(dt);

            cmd.SelectCommand.Connection.Close();
            return dt;
        }
        public bool IsDuplicateBranch(string branchCode)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("DuplicateBranchCheck", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(branchCode))
                cmd.Parameters.AddWithValue("@BranchCode", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@BranchCode", branchCode);

            cmd.Connection.Open();
            object objCount = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return (int)objCount > 0;
        }
        public bool Branch_Save(Branch_Entity be)
        {

            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Branch_Save", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("@BranchCode", be.BranchCode);
            cmd.Parameters.AddWithValue("@BranchName", be.BranchName);
            cmd.Parameters.AddWithValue("@BrandShortName", be.BrandShortName);
            cmd.Parameters.AddWithValue("@SotreCategory", be.SotreCategory);
            cmd.Parameters.AddWithValue("@Summary", be.Summary);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;


        }
        public bool Branch_Delete(Branch_Entity be)
        {

            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Branch_Delete", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("@BranchCode", be.BranchCode);
            cmd.Parameters.AddWithValue("@BranchName", be.BranchName);
            cmd.Parameters.AddWithValue("@BrandShortName", be.BrandShortName);
            cmd.Parameters.AddWithValue("@SotreCategory", be.SotreCategory);
            cmd.Parameters.AddWithValue("@Summary", be.Summary);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;


        }
        public DataTable Branch_Edit(Branch_Entity be)
        {

            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Branch_Edit", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrWhiteSpace(be.BranchCode))
                cmd.Parameters.AddWithValue("@BranchCode", DBNull.Value);
            else cmd.Parameters.AddWithValue("@BranchCode", be.BranchCode);

          
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            return dt;

        }
        public bool Branch_Update(Branch_Entity be)
        {

            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Branch_Update", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@BranchCode", be.BranchCode);
            cmd.Parameters.AddWithValue("@BranchName", be.BranchName);
            cmd.Parameters.AddWithValue("@BrandShortName", be.BrandShortName);
            cmd.Parameters.AddWithValue("@StoreCategory", be.SotreCategory);
            cmd.Parameters.AddWithValue("@Summary", be.Summary);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;

        }
        public bool BranchDelete(Branch_Entity be)
        {

            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Branch_Delete", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@BranchCode", be.BranchCode);
          

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;

        }
       
    }
}

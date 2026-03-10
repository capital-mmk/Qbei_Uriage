using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using _4.Qbei_Uriage_Common;

namespace _3.Qbei_Uriage_DL
{
   public class QbeiUser_DL : Base_DL
    {
        //CheckExist 
        //Delete_User
        public bool Delete_User(QbeiUser_Entity que)
        {
            try
            {
                SqlConnection sqlcon = GetConnection();
                SqlCommand cmd = new SqlCommand("Delete_User", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@UserID", que.UserID);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public DataTable CheckExist(QbeiUser_Entity que)
        {
            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("CheckExist", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;


            if (string.IsNullOrWhiteSpace(que.UserName))
                cmd.Parameters.AddWithValue("@UserName", DBNull.Value);
            else
                 cmd.Parameters.AddWithValue("@UserName", que.UserName);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            return dt;
        }
        public DataTable Qbei_User_Select(QbeiUser_Entity que)
       {
            SqlConnection sqlcon = GetConnection();
            SqlCommand cmd = new SqlCommand("Qbei_User_Select_by", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;

          
            if (string.IsNullOrWhiteSpace(que.UserName))
                cmd.Parameters.AddWithValue("@UserName", DBNull.Value);
            else cmd.Parameters.AddWithValue("@UserName", que.UserName);

            if (string.IsNullOrWhiteSpace(que.Password))
                cmd.Parameters.AddWithValue("@Password", DBNull.Value);
            else cmd.Parameters.AddWithValue("@Password", que.Password);

          
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
           
                da.Fill(dt);
                return dt;
        }
       public DataTable UserList_SelectAll()
       {
           DataTable dt = new DataTable();
           SqlConnection con = GetConnection();
           SqlDataAdapter cmd = new SqlDataAdapter("UserList_SelectAll", con);
           cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

           con.Open();
           cmd.Fill(dt);

           cmd.SelectCommand.Connection.Close();
           return dt;
       }
       public bool User_Save(QbeiUser_Entity que)
       {
          
           SqlConnection sqlcon = GetConnection();
           SqlCommand cmd = new SqlCommand("UserSave", sqlcon);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.AddWithValue("@UserName", que.UserName);
           cmd.Parameters.AddWithValue("@Password", que.Password);

           cmd.Parameters.AddWithValue("@ModifiedDate", que.ModifiedDate);

           cmd.ExecuteNonQuery();
           cmd.Connection.Close();
           return true;


       }
       public DataTable Qbei_User_Edit(QbeiUser_Entity qe)
       {
         
           SqlConnection sqlcon = GetConnection();
           SqlCommand cmd = new SqlCommand("Qbei_User_Edit", sqlcon);
           cmd.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrWhiteSpace(qe.UserID))
                cmd.Parameters.AddWithValue("@ID", DBNull.Value);
            else cmd.Parameters.AddWithValue("@ID", qe.UserID);

            if (string.IsNullOrWhiteSpace(qe.ModifiedDate))
               cmd.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);
           else cmd.Parameters.AddWithValue("@ModifiedDate", qe.ModifiedDate);

           if (string.IsNullOrWhiteSpace(qe.UserName))
               cmd.Parameters.AddWithValue("@UserName", DBNull.Value);
           else cmd.Parameters.AddWithValue("@UserName", qe.UserName);

           if (string.IsNullOrWhiteSpace(qe.Password))
               cmd.Parameters.AddWithValue("@Password", DBNull.Value);
           else cmd.Parameters.AddWithValue("@Password", qe.Password);

           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dt = new DataTable();
          
           da.Fill(dt);
           return dt;
          
       }
       public bool User_Update(QbeiUser_Entity que)
       {
          
           SqlConnection sqlcon = GetConnection();
           SqlCommand cmd = new SqlCommand("Qbei_UserUpdate_by", sqlcon);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();
           cmd.Parameters.AddWithValue("@UserID", que.UserID);
           cmd.Parameters.AddWithValue("@UserName", que.UserName);
           cmd.Parameters.AddWithValue("@Password", que.Password);
          // cmd.Parameters.AddWithValue("@ModifiedDate", que.ModifiedDate);
            cmd.Parameters.AddWithValue("@Updatedby", que.Modified_By);
           cmd.ExecuteNonQuery();
           cmd.Connection.Close();
           return true;

       }

       public DataTable UserList_Search(QbeiUser_Entity qe)
       {
           DataTable dt = new DataTable();
           SqlConnection sqlcon = GetConnection();

           SqlDataAdapter cmd = new SqlDataAdapter("UserList_Search", sqlcon);

           if (string.IsNullOrWhiteSpace(qe.UserID))
               cmd.SelectCommand.Parameters.AddWithValue("@UserID", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@UserID", qe.UserID);

           if (string.IsNullOrWhiteSpace(qe.UserName))
               cmd.SelectCommand.Parameters.AddWithValue("@UserName", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@UserName", qe.UserName);

           if (string.IsNullOrWhiteSpace(qe.Password))
               cmd.SelectCommand.Parameters.AddWithValue("@Password", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@Password", qe.Password);

           if (string.IsNullOrWhiteSpace(qe.ModifiedDate))
               cmd.SelectCommand.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);
           else cmd.SelectCommand.Parameters.AddWithValue("@ModifiedDate", qe.ModifiedDate);

           cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
           cmd.SelectCommand.Connection.Open();
           cmd.Fill(dt);

           cmd.SelectCommand.Connection.Close();
           return dt;
       }
       }
    }


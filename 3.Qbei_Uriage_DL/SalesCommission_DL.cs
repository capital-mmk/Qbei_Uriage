using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Qbei_Uriage_Common;

namespace _3.Qbei_Uriage_DL
{
    public class SalesCommission_DL : Base_DL
    {
       public bool SalesCommission_Insert(SalesCommission_Entity se)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand("SalesCommission_Insert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            if (string.IsNullOrWhiteSpace(se.AccountTitle))
                cmd.Parameters.AddWithValue("@AccountTitle", DBNull.Value);
            else cmd.Parameters.AddWithValue("@AccountTitle", se.AccountTitle);

            if (string.IsNullOrWhiteSpace(se.ShopCode))
                cmd.Parameters.AddWithValue("@ShopCode", DBNull.Value);
            else cmd.Parameters.AddWithValue("@ShopCode", se.ShopCode);

            if (string.IsNullOrWhiteSpace(se.Percent))
                cmd.Parameters.AddWithValue("@Percent", DBNull.Value);
            else cmd.Parameters.AddWithValue("@Percent", se.Percent);

            if (string.IsNullOrEmpty(se.Expire_SDate))
                cmd.Parameters.AddWithValue("@Expire_SDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Expire_SDate", se.Expire_SDate);

            if (string.IsNullOrEmpty(se.Expire_EDate))
                cmd.Parameters.AddWithValue("@Expire_EDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Expire_EDate", se.Expire_EDate);

            if (string.IsNullOrWhiteSpace(se.CreatedBy))
                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
            else cmd.Parameters.AddWithValue("@CreatedBy", se.CreatedBy);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }
       public bool SalesCommissionMaster_Delete(SalesCommission_Entity se)
       {
           SqlConnection conn = GetConnection();
           SqlCommand cmd = new SqlCommand("SalesCommission_Delete", conn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           if (string.IsNullOrWhiteSpace(se.SalesCommissionID))
               cmd.Parameters.AddWithValue("@SalesCommissionID", DBNull.Value);
           else cmd.Parameters.AddWithValue("@SalesCommissionID", se.SalesCommissionID);

           if (string.IsNullOrWhiteSpace(se.CreatedBy))
               cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
           else cmd.Parameters.AddWithValue("@ModifiedBy", se.ModifiedBy);

           cmd.ExecuteNonQuery();
           cmd.Connection.Close();
           return true;
         
       }
       public DataTable SalesCommissionMaster_Select(SalesCommission_Entity se)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           dic.Add("@SalesCommissionID", se.SalesCommissionID);
           dic.Add("@AccountTitle", se.AccountTitle);
           dic.Add("@ShopCode", se.ShopCode);
           dic.Add("@Percent", se.Percent);
           dic.Add("@StartDate", se.Expire_SDate);
           dic.Add("@EndDate", se.Expire_EDate);
           return SelectData(dic, "SalesCommissionMaster_Select");
       }
       public bool SalesCommission_Update(SalesCommission_Entity se)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           dic.Add("@SalesCommissionID", se.SalesCommissionID);
           dic.Add("@AccountTitle", se.AccountTitle);
           dic.Add("@ShopCode", se.ShopCode);
           dic.Add("@Percent", se.Percent);
           dic.Add("@Expire_SDate", se.Expire_SDate);
           dic.Add("@Expire_EDate", se.Expire_EDate);
           dic.Add("@ModifiedBy", se.ModifiedBy);


           return InsertUpdateDeleteData(dic, "SalesCommissionMaster_Update");
       }

       public DataTable SalesCommission_SelectAll()
       {
           SqlConnection con = GetConnection();
           try
           {
               DataTable dtSComm = new DataTable();
               SqlDataAdapter objDA = new SqlDataAdapter("SalesCommissionMaster_SelectAll", con);
               objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
               con.Open();
               objDA.Fill(dtSComm);
               con.Close();
               return dtSComm;
           }
           catch (Exception ex)
           {
               con.Close();
               return null;
           }
       }
    }
}

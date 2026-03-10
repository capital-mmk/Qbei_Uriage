using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Qbei_Uriage_Common;

namespace _3.Qbei_Uriage_DL
{
   public class CommercialTax_DL:Base_DL
    {
       public bool CommercialTax_Insert(CommercialTax_Entity ce)
       {
           SqlConnection conn = GetConnection();
           SqlCommand cmd = new SqlCommand("CommercialTax_Insert", conn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();
           if (string.IsNullOrWhiteSpace(ce.AccountTitle))
               cmd.Parameters.AddWithValue("@AccountTitle", DBNull.Value);
           else cmd.Parameters.AddWithValue("@AccountTitle", ce.AccountTitle);

           if (string.IsNullOrWhiteSpace(ce.ShopSectionCode))
               cmd.Parameters.AddWithValue("@ShopSectionCode", DBNull.Value);
           else cmd.Parameters.AddWithValue("@ShopSectionCode", ce.ShopSectionCode);

           if (string.IsNullOrWhiteSpace(ce.UnitPrice))
               cmd.Parameters.AddWithValue("@UnitPrice", DBNull.Value);
           else cmd.Parameters.AddWithValue("@UnitPrice", ce.UnitPrice);

           if (string.IsNullOrWhiteSpace(ce.Percent))
               cmd.Parameters.AddWithValue("@Percent", DBNull.Value);
           else cmd.Parameters.AddWithValue("@Percent", ce.Percent);

          if (string.IsNullOrWhiteSpace(ce.Expire_SDate))
               cmd.Parameters.AddWithValue("@Expire_SDate", DBNull.Value);
           else cmd.Parameters.AddWithValue("@Expire_SDate", ce.Expire_SDate);

           if (string.IsNullOrWhiteSpace(ce.Expire_EDate))
               cmd.Parameters.AddWithValue("@Expire_EDate", DBNull.Value);
           else cmd.Parameters.AddWithValue("@Expire_EDate", ce.Expire_EDate);

           if (string.IsNullOrWhiteSpace(ce.CreatedBy))
               cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
           else cmd.Parameters.AddWithValue("@CreatedBy", ce.CreatedBy);

           cmd.ExecuteNonQuery();
           cmd.Connection.Close();
           return true;
       }
       public bool CommercialTax_Delete(CommercialTax_Entity ce)
       {

           SqlConnection conn = GetConnection();
           SqlCommand cmd = new SqlCommand("CommercialTax_Delete", conn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           if (string.IsNullOrWhiteSpace(ce.CommercialTaxID))
               cmd.Parameters.AddWithValue("@ID", DBNull.Value);
           else cmd.Parameters.AddWithValue("@ID", ce.CommercialTaxID);

           if (string.IsNullOrWhiteSpace(ce.CreatedBy))
               cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
           else cmd.Parameters.AddWithValue("@ModifiedBy", ce.ModifiedBy);

           cmd.ExecuteNonQuery();
           cmd.Connection.Close();
           return true;
       }
       public DataTable CommercialTaxMaster_Select(CommercialTax_Entity ce)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           dic.Add("@CommercialTaxID", ce.CommercialTaxID);
           dic.Add("@AccountTitle", ce.AccountTitle);
           dic.Add("@ShopSectionCode", ce.ShopSectionCode);
           dic.Add("@Percent",ce.Percent);
           dic.Add("@UnitPrice", ce.UnitPrice);
           dic.Add("@Expire_SDate", ce.Expire_SDate);
           dic.Add("@Expire_EDate", ce.Expire_EDate);

           return SelectData(dic, "CommercialTaxMaster_Select");
       }


       public bool CommercialTax_Update(CommercialTax_Entity ce)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           dic.Add("@ID", ce.CommercialTaxID);
           dic.Add("@AccountTitle", ce.AccountTitle);
           dic.Add("@ShopSectionCode", ce.ShopSectionCode);
           dic.Add("@Percent", ce.Percent);
           dic.Add("@UnitPrice", ce.UnitPrice);
           dic.Add("@Expire_SDate", ce.Expire_SDate);
           dic.Add("@Expire_EDate", ce.Expire_EDate);
           dic.Add("@ModifiedBy", ce.ModifiedBy);

           return InsertUpdateDeleteData(dic, "CommercialTaxMaster_Update");
       }

       public DataTable CommercialTax_SelectAll()
       {
           SqlConnection con;
           try
           {
               DataTable dtCom = new DataTable();
               con = GetConnection();
               SqlDataAdapter objDA = new SqlDataAdapter("CommercialTaxMaster_SelectAll", con);
               objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
               con.Open();
               objDA.Fill(dtCom);
               con.Close();
               return dtCom;
           }
           catch (Exception ex)
           {
               return null;
               con.Close();
           }
       }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qbei_Uriage_Common;
using System.Data;
using System.Data.SqlClient;

namespace _3.Qbei_Uriage_DL
{
    public class FixedCost_DL:Base_DL
    {
        public DataTable FixedCost_SelectAll()
        {
            SqlConnection con = GetConnection();
            try
            {
                DataTable dtFC = new DataTable();
                SqlDataAdapter objDA = new SqlDataAdapter("FixedCostMaster_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtFC);
                con.Close();
                return dtFC;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }
        public DataTable FixedCost_Select(FixedCost_Entity objFC)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("@FixedCostID", objFC.FixedCostID);
                dic.Add("@AccountTitle", objFC.AccountTitle);
                dic.Add("@ShopCategory", objFC.ShopCategory);

                return SelectData(dic, "FixedCostMaster_Select");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable ShopCategory_Select()
        {
            SqlConnection con = GetConnection();
            try
            {
                DataTable dtShop = new DataTable();
                SqlDataAdapter objDA = new SqlDataAdapter("ShopCategory_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtShop);
                con.Close();
                return dtShop;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
 
        }
        public bool FixedCost_Delete(FixedCost_Entity objFC)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@FixedCostID", objFC.FixedCostID);
            //dic.Add("@ModifiedBy", objHoliday.CreatedBy);

            return InsertUpdateDeleteData(dic, "FixedCostMaster_Delete");
        }
        public bool FixedCost_Insert(FixedCost_Entity objFC)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@AccountTitle", objFC.AccountTitle);
            dic.Add("@ShopCategory", objFC.ShopCategory);
           // dic.Add("@CreatedBy", objFC.CreatedBy);

            return InsertUpdateDeleteData(dic, "FixedCostMaster_Insert");
        }

        public bool FixedCost_Update(FixedCost_Entity objFC)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@FixedCostID", objFC.FixedCostID);
            dic.Add("@AccountTitle", objFC.AccountTitle);
            dic.Add("@ShopCategory", objFC.ShopCategory);
            //dic.Add("@ModifiedBy", objFC.ModifiedBy);

            return InsertUpdateDeleteData(dic, "FixedCostMaster_Update");
        }
    }
}

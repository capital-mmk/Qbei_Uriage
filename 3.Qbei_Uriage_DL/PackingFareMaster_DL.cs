using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qbei_Uriage_Common;

namespace _3.Qbei_Uriage_DL
{
  public  class PackingFareMaster_DL : Base_DL    {
        public DataTable PackingFareMaster_Select(PackingFareMaster_Entity pme)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@ID",pme.ID);
            dic.Add("@AccountTitle", pme.AccountTitle);
            dic.Add("@OrderType", pme.OrderType);
            dic.Add("@DeliveryCompanyCode", pme.DeliveryCompanyCode);
            dic.Add("@RegionCode", pme.RegionCode);
            dic.Add("@UnitPrice", pme.UnitPrice);
            dic.Add("@Expire_SDate", pme.Expire_SDate);
            dic.Add("@Expire_EDate", pme.Expire_EDate);

            return SelectData(dic, "PackingFareMaster_Select");
        }

        //public DataTable PackingFareMaster_SelectAll(PackingFareMaster_Entity pme)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();

        //    return SelectData(dic, "PackingFareMaster_SelectAll");
        //}

        public bool PackingFareMaster_Insert(PackingFareMaster_Entity pme)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@AccountTitle", pme.AccountTitle);
            dic.Add("@OrderType", pme.OrderType);
            dic.Add("@DeliveryCompanyCode", pme.DeliveryCompanyCode);
            dic.Add("@RegionCode", pme.RegionCode);
            dic.Add("@UnitPrice", pme.UnitPrice);
            dic.Add("@Expire_SDate", pme.Expire_SDate);
            dic.Add("@Expire_EDate", pme.Expire_EDate);
            dic.Add("@CreatedBy", pme.CreatedBy);
           

            return InsertUpdateDeleteData(dic, "PackingFareMaster_Insert");
        }

        public bool PackingFareMaster_Update(PackingFareMaster_Entity pme)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@ID", pme.ID);
            dic.Add("@AccountTitle", pme.AccountTitle);
            dic.Add("@OrderType", pme.OrderType);
            dic.Add("@DeliveryCompanyCode", pme.DeliveryCompanyCode);
            dic.Add("@RegionCode", pme.RegionCode);
            dic.Add("@UnitPrice", pme.UnitPrice);
            dic.Add("@Expire_SDate", pme.Expire_SDate);
            dic.Add("@Expire_EDate", pme.Expire_EDate);
            dic.Add("@ModifiedBy", pme.ModifiedBy);


            return InsertUpdateDeleteData(dic, "PackingFareMaster_Update");
        }


        public bool PackingFareMaster_Delete(PackingFareMaster_Entity pme)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@ID", pme.ID);
            dic.Add("@ModifiedBy", pme.ModifiedBy);

            return InsertUpdateDeleteData(dic, "PackingFareMaster_Delete");
        }
        public DataTable PackingFare_SelectAll()
        {
            SqlConnection con = GetConnection();
            try
            {
                DataTable dtPacking = new DataTable();
                SqlDataAdapter objDA = new SqlDataAdapter("PackingFareMaster_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtPacking);
                con.Close();
                return dtPacking;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }
 
    }
}

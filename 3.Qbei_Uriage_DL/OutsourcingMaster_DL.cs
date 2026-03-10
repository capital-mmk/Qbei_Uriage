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
  public  class OutsourcingMaster_DL : Base_DL 
    {

      public DataTable Outsourcing_SelectAll()
      {
          SqlConnection con = GetConnection();
          try 
          {
              DataTable dtOutsourcing = new DataTable();
              SqlDataAdapter objDA = new SqlDataAdapter("OutsourcingMaster_SelectAll", con);
              objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
              con.Open();
              objDA.Fill(dtOutsourcing);
              con.Close();
              return dtOutsourcing;
          }
          catch (Exception ex)
          {
              con.Close();
              return null;
          }
      }

      public DataTable OutsourcingMaster_Select(OutSourcingMaster_Entity ome)
      {
          Dictionary<string, string> dic = new Dictionary<string, string>();
          dic.Add("@OutsourcingID", ome.OutsourcingID);
          dic.Add("@AccountTitle", ome.AccountTitle);
          dic.Add("@UnitPrice", ome.UnitPrice);
          dic.Add("@Expire_SDate", ome.Expire_SDate);
          dic.Add("@Expire_EDate", ome.Expire_EDate);

          return SelectData(dic, "OutsourcingMaster_Select");
      }

        public bool OutsourcingMaster_Insert(OutSourcingMaster_Entity ome)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("@OutsourcingID", ome.OutsourcingID);
            dic.Add("@AccountTitle",ome.AccountTitle);
            dic.Add("@UnitPrice",ome.UnitPrice);
            dic.Add("@Expire_SDate", ome.Expire_SDate);
            dic.Add("@Expire_EDate", ome.Expire_EDate);
            dic.Add("@CreatedBy",ome.CreatedBy);

            return InsertUpdateDeleteData(dic, "OutsourcingMaster_Insert");
        }

        public bool OutsourcingMaster_Update(OutSourcingMaster_Entity ome)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@OutsourcingID", ome.OutsourcingID);
            dic.Add("@AccountTitle", ome.AccountTitle);
            dic.Add("@UnitPrice", ome.UnitPrice);
            dic.Add("@Expire_SDate", ome.Expire_SDate);
            dic.Add("@Expire_EDate", ome.Expire_EDate);
            dic.Add("@ModifiedBy", ome.ModifiedBy);

            return InsertUpdateDeleteData(dic, "OutsourcingMaster_Update");
        }

        public bool OutsourcingMaster_Delete(OutSourcingMaster_Entity ome)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@OutsourcingID", ome.OutsourcingID);
            dic.Add("@ModifiedBy", ome.ModifiedBy);

            return InsertUpdateDeleteData(dic, "OutsourcingMaster_Delete");
        }
    }
}

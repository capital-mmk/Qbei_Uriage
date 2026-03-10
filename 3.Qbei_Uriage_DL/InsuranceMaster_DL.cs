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
    public class InsuranceMaster_DL : Base_DL
    {
        public DataTable Insurance_SelectAll()
        {
            SqlConnection con ;
            try
            {
                DataTable dtInsurance = new DataTable();
                con = GetConnection();
                SqlDataAdapter objDA = new SqlDataAdapter("InsuranceMaster_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtInsurance);
                con.Close();
                return dtInsurance;
            }
            catch (Exception ex)
            {
                return null;
                con.Close();
            }
        }

        public DataTable InsuranceMaster_Select(InsuranceMaster_Entity ime)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@InsuranceID", ime.InsuranceID);
            dic.Add("@AccountTitle", ime.AccountTitle);
            dic.Add("@Percent", ime.Percent);
            dic.Add("@Expire_SDate", ime.Expire_SDate);
            dic.Add("@Expire_EDate", ime.Expire_EDate);

            return SelectData(dic, "InsuranceMaster_Select");
        }

        public bool InsuranceMaster_Insert(InsuranceMaster_Entity ime)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@AccountTitle", ime.AccountTitle);
            dic.Add("@Percent", ime.Percent);
            dic.Add("@Expire_SDate", ime.Expire_SDate);
            dic.Add("@Expire_EDate", ime.Expire_EDate);
            dic.Add("@CreatedBy", ime.CreatedBy);

            return InsertUpdateDeleteData(dic, "InsuranceMaster_Insert");
        }

        public bool InsuranceMaster_Update(InsuranceMaster_Entity ime)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@InsuranceID", ime.InsuranceID);
            dic.Add("@AccountTitle", ime.AccountTitle);
            dic.Add("@Percent", ime.Percent);
            dic.Add("@Expire_SDate", ime.Expire_SDate);
            dic.Add("@Expire_EDate", ime.Expire_EDate);
            dic.Add("@ModifiedBy", ime.ModifiedBy);

            return InsertUpdateDeleteData(dic, "InsuranceMaster_Update");
        }

        public bool InsuranceMaster_Delete(InsuranceMaster_Entity ime)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@InsuranceID", ime.InsuranceID);
            dic.Add("@ModifiedBy", ime.ModifiedBy);

            return InsertUpdateDeleteData(dic, "InsuranceMaster_Delete");
        }

    }
}


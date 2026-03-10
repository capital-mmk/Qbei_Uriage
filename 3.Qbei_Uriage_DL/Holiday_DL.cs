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
    public class Holiday_DL:Base_DL
    {
        public DataTable Holiday_SelectAll()
        {
            SqlConnection con = GetConnection();
            try
            {
                DataTable dtHoliday = new DataTable();
                SqlDataAdapter objDA = new SqlDataAdapter("HolidayMaster_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtHoliday);
                con.Close();
                return dtHoliday;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }
        public DataTable Holiday_Select(Holiday_Entity objHoliday)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("@HolidayID", objHoliday.HolidayID);
                dic.Add("@HolidayDate", objHoliday.HolidayDate);
                dic.Add("@HolidayName", objHoliday.HolidayName);

                return SelectData(dic, "HolidayMaster_Select");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Holiday_Delete(Holiday_Entity objHoliday)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@HolidayID", objHoliday.HolidayID);
            //dic.Add("@ModifiedBy", objHoliday.CreatedBy);

            return InsertUpdateDeleteData(dic, "HolidayMaster_Delete");
        }
        public bool Holiday_Insert(Holiday_Entity objHoliday)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@HolidayDate", objHoliday.HolidayDate);
            dic.Add("@HolidayName", objHoliday.HolidayName);
            // dic.Add("@CreatedBy", objFC.CreatedBy);

            return InsertUpdateDeleteData(dic, "HolidayMaster_Insert");
        }

        public bool Holiday_Update(Holiday_Entity objHoliday)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@HolidayID", objHoliday.HolidayID);
            dic.Add("@HolidayDate", objHoliday.HolidayDate);
            dic.Add("@HolidayName", objHoliday.HolidayName);
            //dic.Add("@ModifiedBy", objFC.ModifiedBy);

            return InsertUpdateDeleteData(dic, "HolidayMaster_Update");
        }
    }
}

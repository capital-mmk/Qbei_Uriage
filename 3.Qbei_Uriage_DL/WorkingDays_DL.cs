using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qbei_Uriage_Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace _3.Qbei_Uriage_DL
{
    public class WorkingDays_DL:Base_DL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter objDA;
        DataTable dtWD;

        public DataTable WorkingDays_SelectAll()
        {
            con = GetConnection();
            try
            {
                dtWD = new DataTable();
                objDA = new SqlDataAdapter("WorkingDaysMaster_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtWD);
                con.Close();
                return dtWD;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }
        public DataTable WorkingDays_Select(WorkingDays_Entity objWD)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("@WorkingDaysID", objWD.WorkingDaysID);
                dic.Add("@YM", objWD.YearMonth);
                dic.Add("@ShopName", objWD.ShopName);
                dic.Add("@WorkingDays", objWD.WorkingDays);

                return SelectData(dic, "WorkingDaysMaster_Select");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool WorkingDays_Delete(WorkingDays_Entity objWD)
        {
            con = GetConnection();
            try 
            {
                if (UseTransaction)
                {
                    cmd = new SqlCommand("WorkingDaysMaster_Delete", con, transaction);
                }
                else
                    cmd = new SqlCommand("WorkingDaysMaster_Delete", con);
                cmd.Parameters.AddWithValue("@WorkingDaysID", objWD.WorkingDaysID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 6000;
                objDA = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                return false;
            }
        }
        public bool WorkingDays_Insert(WorkingDays_Entity objWD)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@YM", objWD.YearMonth);
            dic.Add("@ShopName", objWD.ShopName);
            dic.Add("@WorkingDays", objWD.WorkingDays);

            return InsertUpdateDeleteData(dic, "WorkingDaysMaster_Insert");
        }

        public bool WorkingDays_Update(WorkingDays_Entity objWD)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("@WorkingDaysID", objWD.WorkingDaysID);
            dic.Add("@YM", objWD.YearMonth);
            dic.Add("@ShopName", objWD.ShopName);
            dic.Add("@WorkingDays", objWD.WorkingDays);

            return InsertUpdateDeleteData(dic, "WorkingDaysMaster_Update");
        }
    }
}

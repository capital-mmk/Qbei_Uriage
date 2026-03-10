using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using _4.Qbei_Uriage_Common;

namespace _3.Qbei_Uriage_DL
{
    public class Cost_DL : Base_DL
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        string strCon;
        Common objCom = new Common();

        public DataTable CostMaster_Select(string strStoreProcedureNm)
        {
            try
            {
                objCom.WriteLog("CostMaster_Select Start");
                DataTable dtData = new DataTable();
                conn = GetConnection();
                cmd = new SqlCommand(strStoreProcedureNm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dtData);
                cmd.Connection.Close();
                if (dtData.Rows.Count > 0)
                {
                    return dtData;
                }
                else
                {
                    objCom.WriteLog("Data does not exist in " + strStoreProcedureNm);
                    return null;
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog(ex.Message);
                return null;
            }
            finally
            {
                objCom.WriteLog("CostMaster_Select End");
                cmd.Connection.Close();
            }
        }

        public bool CostData_Insert(string strStoreProcedureNm, string strParam)
        {
            try
            {
                objCom.WriteLog("CostData_Insert Start");
                conn = GetConnection();
                if (UseTransaction)
                {
                    cmd = new SqlCommand(strStoreProcedureNm, conn, transaction);
                }
                else
                    cmd = new SqlCommand(strStoreProcedureNm, conn);
                cmd.Parameters.AddWithValue("@xmlCsv", strParam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 6000;
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                objCom.WriteLog(strStoreProcedureNm + ex.Message);
                return false;
            }
            finally
            {
                objCom.WriteLog("CostData_Insert End");
            }
        }

        public DataTable DailyCost_Select()
        {
            try
            {
                objCom.WriteLog("DailyCost_Select Start");
                DataTable dtData = new DataTable();
                conn = GetConnection();
                cmd = new SqlCommand("DailyCost_Output", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dtData);
                cmd.Connection.Close();
                if (dtData.Rows.Count > 0)
                    return dtData;
                else
                {
                    objCom.WriteLog("Daily Cost data does not exist!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                objCom.WriteLog(ex.Message);
                return null;
            }
            finally
            {
                objCom.WriteLog("DailyCost_Select End");
                cmd.Connection.Close();
            }
        }

        public DataTable Initial_Select(string strStoreProcedureNm)
        {
            try
            {
                DataTable dtData = new DataTable();
                objCom.WriteLog("Initial_Select Start");
                conn = GetConnection();
                cmd = new SqlCommand(strStoreProcedureNm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dtData);
                cmd.Connection.Close();
                if (dtData.Rows.Count > 0)
                    return dtData;
                else
                {
                    objCom.WriteLog("Sale data does not exist!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog(strStoreProcedureNm + ex.Message);
                return null;
            }
            finally
            {
                objCom.WriteLog("Initial_Select End");
                cmd.Connection.Close();
            }
        }

        public DataTable SyainCost_Select()
        {
            try
            {
                objCom.WriteLog("SyainCost_Select Start");
                DataTable dtData = new DataTable();
                conn = GetConnection();
                cmd = new SqlCommand("SelectSyainCost", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dtData);
                cmd.Connection.Close();
                if (dtData.Rows.Count > 0)
                    return dtData;
                else
                {
                    objCom.WriteLog("Syain Cost data does not exist!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog("SelectSyainCost" + ex.Message);
                return null;
            }
            finally
            {
                objCom.WriteLog("SyainCost_Select End");
                cmd.Connection.Close();
            }
        }

        public bool OverwriteActual(string strParam)
        {
            try
            {
                objCom.WriteLog("Overwrite Actual Start");
                conn = GetConnection();
                if (UseTransaction)
                {
                    cmd = new SqlCommand("OverwriteActual", conn, transaction);
                }
                else
                    cmd = new SqlCommand("OverwriteActual", conn);
                cmd.Parameters.AddWithValue("@xmlCsv", strParam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 6000;
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                objCom.WriteLog("OverwriteActual" + ex.Message);
                return false;
            }
            finally
            {
                objCom.WriteLog("Overwrite Actual End");
                cmd.Connection.Close();
            }
        }

        public DataTable PlaningCost_Select(Dictionary<string,string> dicParam)
        {
            try {
                objCom.WriteLog("PlaningCost_Select Start");
                DataTable dtData = new DataTable();
                conn = GetConnection();
                cmd = new SqlCommand("PlaningData_Select", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrEmpty (dicParam["StartDate"]))
                    cmd.Parameters.AddWithValue("@StartDate",DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@StartDate", dicParam["StartDate"]);
                if (String.IsNullOrEmpty(dicParam["EndDate"]))
                    cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@EndDate", dicParam["EndDate"]);
                if (String.IsNullOrEmpty(dicParam["AccTitle"]))
                    cmd.Parameters.AddWithValue("@AccTitle", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@AccTitle", dicParam["AccTitle"]);
                if (String.IsNullOrEmpty(dicParam["Amount"]))
                    cmd.Parameters.AddWithValue("@Amount", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Amount", dicParam["Amount"]);
                if (String.IsNullOrEmpty(dicParam["ShopCategory"]))
                    cmd.Parameters.AddWithValue("@ShopCategory",DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ShopCategory", dicParam["ShopCategory"]);
                da = new SqlDataAdapter(cmd);
                cmd.Connection.Open();
                da.Fill(dtData);
                cmd.Connection.Close();
                if (dtData.Rows.Count > 0)
                    return dtData;
                else
                {
                    objCom.WriteLog("Planing data does not exist!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog("PlaningCost_Select" + ex.Message);
                return null;
            }
            finally
            {
                objCom.WriteLog("PlaningCost_Select End");
                cmd.Connection.Close();
            }
        }

        public DataTable ShopCategory_Select()
        {
            SqlConnection con = GetConnection();
            try
            {
                objCom.WriteLog("ShopCategory_Select Start");
                DataTable dtShop = new DataTable();
                SqlDataAdapter objDA = new SqlDataAdapter("ShopCategory_SelectAll", con);
                objDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                objDA.Fill(dtShop);
                con.Close();
                if (dtShop.Rows.Count > 0)
                    return dtShop;
                else
                {
                    objCom.WriteLog("Branch data does not exist!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
            finally {
                objCom.WriteLog("ShopCategory_Select End");
            }
        }
    }
}

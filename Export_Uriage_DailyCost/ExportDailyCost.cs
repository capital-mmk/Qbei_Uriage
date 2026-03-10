using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;
using _4.Qbei_Uriage_Common;
using System.Data;
using System.Data.SqlClient;

namespace Export_Uriage_DailyCost
{
    public class ExportDailyCost
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remark>
        /// Data Table and Common Function and Field
        /// </remark>
        static void Main(string[] args)
        {
            string strFileNm = @"C:\Qbei_Uriage\Output\CostOuput.csv";
            Common objCom = new Common();
            Cost_DL dl_Cost = new Cost_DL();
            DataTable dtData = new DataTable();
            DataTable dtTemp = new DataTable();
            string[] strColNm = new string[] { "年月日", "集計部門", "勘定科目", "金額" };
            DataRow drData;
            /// <remark>
            /// Process of Export Daily Cost.
            /// </remark>
            try
            {
                objCom.WriteLog("Export Daily Cost Start");
                dtTemp = dl_Cost.DailyCost_Select();
                if (dtTemp != null)
                {
                    foreach (string strCol in strColNm)
                    {
                        dtData.Columns.Add(strCol);
                    }
                    foreach (DataRow dr in dtTemp.AsEnumerable())
                    {
                        drData = dtData.NewRow();
                        drData["年月日"] = dr.Field<DateTime>("SaleDate").ToString("yyyyMMdd");
                        drData["集計部門"] = dr.Field<string>("Category");
                        //drData["勘定科目"] = !String.IsNullOrEmpty(dr.Field<string>("CAcc")) ? dr.Field<string>("CAcc") : !String.IsNullOrEmpty(dr.Field<string>("FAcc")) ? dr.Field<string>("FAcc") : !String.IsNullOrEmpty(dr.Field<string>("OAcc")) ? dr.Field<string>("OAcc") : !String.IsNullOrEmpty(dr.Field<string>("PAcc")) ? dr.Field<string>("PAcc") : dr.Field<string>("SAcc"); 
                        drData["勘定科目"] = !String.IsNullOrEmpty(dr.Field<string>("CAcc")) ? dr.Field<string>("CAcc") : !String.IsNullOrEmpty(dr.Field<string>("FAcc")) ? dr.Field<string>("FAcc") : !String.IsNullOrEmpty(dr.Field<string>("OAcc")) ? dr.Field<string>("OAcc") : !String.IsNullOrEmpty(dr.Field<string>("PAcc")) ? dr.Field<string>("PAcc") : !String.IsNullOrEmpty(dr.Field<string>("SAcc")) ? dr.Field<string>("SAcc") : !String.IsNullOrEmpty(dr.Field<string>("IAcc")) ? dr.Field<string>("IAcc") : "荷造運賃（混在）";
                        drData["金額"] = dr.Field<decimal>("Amt").ToString();
                        dtData.Rows.Add(drData);
                    }
                    if (objCom.ExportCsv(dtData, strFileNm))
                    {
                        objCom.WriteLog("Daily Cost Export Complete!");
                    }
                    else
                        objCom.WriteLog("Daily Cost Export Fail!");
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog(ex.Message);
            }
            finally
            {
                objCom.WriteLog("Export Daily Cost End");
                Environment.Exit(0);
            }
        }
    }
}

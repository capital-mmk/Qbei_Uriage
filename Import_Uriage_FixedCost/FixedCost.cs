using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using _3.Qbei_Uriage_DL;
using _4.Qbei_Uriage_Common;

namespace Import_Uriage_FixedCost
{
    public class FixedCost
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remark>
        /// Data Table and Common Function and Field
        /// </remark>
        static void Main(string[] args)
        {
            Common objComm = new Common();
            Cost_DL dl_Cost = new Cost_DL();
            try
            {
                T_Sale_Entity saleentity = new T_Sale_Entity();
                DataTable dtFCost = new DataTable();
                DataTable dtHoliday = new DataTable();
                DataTable dtCsv = new DataTable();
                T_Sale_Entity objsale = new T_Sale_Entity();
                DataTable dt = new DataTable();
                int intHolidayId = 0;
                String strXml = string.Empty;

                /// <remark>
                /// Process of Fixed Cost.
                /// </remark>
                objComm.WriteLog("Fixed Cost Start");
                dtCsv = objComm.ReadCsv(@"C:\Qbei_Uriage\Input\日別費用予算(分析連携あり).csv");
                if (dtCsv != null)
                {
                    //Get Holiday Master Data
                    dtHoliday = dl_Cost .CostMaster_Select("HolidayMaster_SelectAll");
                    //Get Fixed Cost Master Data
                    dtFCost = dl_Cost.CostMaster_Select("FixedCostMaster_SelectAll");
                    //Get Csv data by group
                    var CsvData = (from tempdata in dtCsv.AsEnumerable()
                                   group tempdata by new { date = tempdata.Field<string>("日付"), category = tempdata.Field<string>("部門"), costname = tempdata.Field<string>("勘定科目") } into tempcsv
                                   select new
                                   {
                                       date = tempcsv.Key.date,
                                       shopCategory = tempcsv.Key.category,
                                       costNm = tempcsv.Key.costname,
                                       amount = tempcsv.Sum(y => decimal.Parse(y.Field<string>("金額")))
                                   }).ToList();
                    saleentity.lstSale = new List<T_Sale_Entity>();
                    foreach (var data in CsvData)
                    {
                        objsale = new T_Sale_Entity();
                        objsale.dtSaleDate = Convert.ToDateTime(data.date);
                        objsale.intBranchCode = 0;
                        objsale.intPackingID = 0;
                        objsale.intInsuranceID = 0;
                        objsale.intCommercialTaxID = 0;
                        objsale.intOutsourcingID = 0;
                        objsale.intSalesCommissionID = 0;
                        objsale.strShopCategory = data.shopCategory;
                        intHolidayId = dtHoliday.AsEnumerable().Where(x => DateTime.Compare(x.Field<DateTime>("HolidayDate"), objsale.dtSaleDate) == 0).Select(y => y.Field<int>("HolidayID")).SingleOrDefault();
                        objsale.intHolidayID = intHolidayId;
                        objsale.bolFinFlag = false;
                        objsale.decUnitPrice = 0;
                        objsale.intQTY = 0;
                        objsale.decOverwrite_Amt = 0;
                        objsale.strCostType = "Fixed";
                        objsale.decAmount = data.amount;
                        objsale.intFixedCostID = dtFCost.AsEnumerable().Where(x => x.Field<string>("AccountTitle").Equals(data.costNm) && x.Field<string>("ShopCategory").Equals(data.shopCategory)).Select(y => y.Field<int>("FixedCostID")).SingleOrDefault();
                        saleentity.lstSale.Add(objsale);
                    }
                    strXml = objComm.ConvertListToXml(saleentity.lstSale);
                    if (dl_Cost.CostData_Insert("InsertXML_Cost", strXml))
                        objComm.WriteLog("Fixed Cost import complete!");
                    else
                        objComm.WriteLog("Fixed Cost import fail!");
                }
            }
            catch (Exception ex)
            {
                objComm.WriteLog(ex.Message);
            }
            finally {
                objComm.WriteLog("Fixed Cost End");
                Environment.Exit(0);
            }
        }
    }
}

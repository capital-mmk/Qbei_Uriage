using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;
using _4.Qbei_Uriage_Common;
using System.Data;

namespace Import_Uriage_VariableCost
{
    public class VariableCost
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remark>
        /// Data Table and Common Function and Field
        /// </remark>
        static void Main(string[] args)
        {
            DataTable dtInitialSale = new DataTable();
            DataTable dtPreviousSaleData = new DataTable();
            DataTable dtOutSourcing = new DataTable();
            DataTable dtSaleCommission = new DataTable();
            DataTable dtCommercialTax = new DataTable();
            DataTable dtPack = new DataTable();
            //
            DataTable dtInsurance = new DataTable();
            DataTable dtSyain = new DataTable();
            //
            Common objCom = new Common();
            Cost_DL dl_Cost = new Cost_DL();
            DataTable dtCsv = new DataTable();
            DateTime dtStart, dtEnd;
            T_Sale_Entity saleEntity1 = new T_Sale_Entity();
            T_Sale_Entity saleEntity = new T_Sale_Entity();
            saleEntity1.lstSale = new List<T_Sale_Entity>();
            decimal decPercent = 0;
            int intUnitP = 0;
            string strXml = string.Empty;
            //
            int intInsurance = 0;
            decimal decSCost = 0;
            //

            ///<remark>
            ///Using to output2.CSV. 
            ///</remark>
            string strFileNm = @"C:\Qbei_Uriage\Output\Output2.csv";
            try
            {
                objCom.WriteLog("Variable Cost Start");
                dtCsv = objCom.ReadCsv(strFileNm);
                if (dtCsv != null)
                {
                    dtInitialSale = dl_Cost.Initial_Select("Initial_Sale_Select");
                    //
                    dtSyain = dl_Cost.SyainCost_Select();
                    //
                    ///<remark>
                    ///Check to 売上日.
                    ///</remark>
                    if (dtInitialSale != null)
                    {
                        dtPreviousSaleData = dl_Cost.Initial_Select("Sale_Select");
                        dtStart = dtPreviousSaleData.Rows[0]["StartDt"] != DBNull.Value ? dtPreviousSaleData.Rows[0].Field<DateTime>("StartDt") : DateTime.Now.AddMonths(-2);
                        dtEnd = DateTime.Now;
                        dtStart = new DateTime(dtStart.Year, dtStart.Month, 1);
                        dtCsv = dtCsv.AsEnumerable().Where(x => Convert.ToDateTime(x.Field<string>("売上日")) >= dtStart && Convert.ToDateTime(x.Field<string>("売上日")) <= dtEnd).CopyToDataTable();
                        //
                        ///<remark>
                        ///Check to SaleDate.
                        ///</remark>
                        if (dtSyain != null)
                        {
                            var SyainCost = dtSyain.AsEnumerable().Where(x => x.Field<DateTime>("SaleDate") >= dtStart && x.Field<DateTime>("SaleDate") < dtEnd.Date);
                            if (SyainCost.Any())
                                dtSyain = SyainCost.CopyToDataTable();
                        }
                        //
                    }

                    ///<remark>
                    ///Process of Variable Cost .
                    ///</remark>
                    dtPack = dl_Cost.CostMaster_Select("PackingFareMaster_SelectAll");
                    dtCommercialTax = dl_Cost.CostMaster_Select("CommercialTaxMaster_SelectAll");
                    dtSaleCommission = dl_Cost.CostMaster_Select("SCommissionMaster_SelectAll");
                    dtOutSourcing = dl_Cost.CostMaster_Select("OutsourcingMaster_SelectAll");
                    //
                    dtInsurance = dl_Cost.CostMaster_Select("InsuranceMaster_SelectAll");
                    //

                    ///<remark>
                    ///Process for OutsourceData.
                    ///</remark>
                    if (dtPack != null && dtCommercialTax != null && dtSaleCommission != null && dtOutSourcing != null && dtInsurance != null)
                    {
                        var OutSourcingCost = (from temp in dtCsv.AsEnumerable()
                                               where temp.Field<string>("注文タイプ").Equals("自転車以外") && temp.Field<string>("集計部門").Equals("ネット")
                                               group temp by new
                                               {
                                                   SaleDate = temp.Field<string>("売上日"),
                                                   Category = temp.Field<string>("集計部門")
                                               } into OutsourceData
                                               select new
                                               {
                                                   SDate = OutsourceData.Key.SaleDate,
                                                   Shop = 0,
                                                   Cnt = OutsourceData.ToList().Count,
                                                   ShopCategory = OutsourceData.Key.Category,
                                               }).ToList();

                        foreach (var data in OutSourcingCost)
                        {
                            saleEntity = new T_Sale_Entity();
                            saleEntity.dtSaleDate = Convert.ToDateTime(data.SDate);
                            saleEntity.intBranchCode = data.Shop;
                            saleEntity.intOutsourcingID = int.Parse(dtOutSourcing.AsEnumerable().Where(x => x.Field<DateTime>("Expire_SDate") <= saleEntity.dtSaleDate && (x.Field<DateTime?>("Expire_EDate") == null) || (x.Field<DateTime?>("Expire_EDate") != null && Convert.ToDateTime(x.Field<DateTime?>("Expire_EDate")) >= saleEntity.dtSaleDate)).Select(y => y.Field<int>("OutsourcingID")).SingleOrDefault().ToString());
                            saleEntity.intQTY = data.Cnt;
                            saleEntity.decUnitPrice = dtOutSourcing.AsEnumerable().Where(x => x.Field<int>("OutsourcingID").Equals(saleEntity.intOutsourcingID)).Select(y => y.Field<int>("UnitPrice")).SingleOrDefault();
                            saleEntity.decAmount = saleEntity.intQTY * saleEntity.decUnitPrice;
                            saleEntity.intCommercialTaxID = 0;
                            saleEntity.intFixedCostID = 0;
                            saleEntity.intInsuranceID = 0;
                            saleEntity.intPackingID = 0;
                            saleEntity.intSalesCommissionID = 0;
                            saleEntity.bolFinFlag = false;
                            saleEntity.strCostType = "Variable";
                            saleEntity.strShopCategory = data.ShopCategory;
                            saleEntity.intHolidayID = 0;
                            saleEntity.decOverwrite_Amt = 0;
                            saleEntity.strAccTitle = dtOutSourcing.AsEnumerable().Where(x => x.Field<int>("OutsourcingID") == saleEntity.intOutsourcingID).Select(y => y.Field<string>("AccountTitle")).SingleOrDefault();
                            saleEntity1.lstSale.Add(saleEntity);
                        }

                        ///<remark>
                        ///Process for CommercialTax.
                        ///</remark>
                        var CommercialTax = (from tempct in dtCsv.AsEnumerable()
                                             group tempct by new
                                             {
                                                 PaymentType = tempct.Field<string>("支払区分コード"),
                                                 SaleDate = tempct.Field<string>("売上日"),
                                                 ShopCategory = tempct.Field<string>("集計部門")
                                             } into comtax
                                             select new
                                             {
                                                 shop = 0,
                                                 sDate = comtax.Key.SaleDate,
                                                 payment = int.Parse(comtax.Key.PaymentType),
                                                 TotalAmt = comtax.Sum(y => decimal.Parse(y.Field<string>("合計金額"))),
                                                 TotalCnt = comtax.ToList().Count,
                                                 ShopCategory = comtax.Key.ShopCategory
                                             }).ToList();

                        CommercialTax = CommercialTax.Where(x => dtCommercialTax.AsEnumerable().Any(y => y.Field<int>("ShopSectionCode") == x.payment)).ToList();

                        foreach (var data in CommercialTax)
                        {
                            saleEntity = new T_Sale_Entity();
                            saleEntity.dtSaleDate = Convert.ToDateTime(data.sDate);
                            saleEntity.intBranchCode = data.shop;
                            saleEntity.intCommercialTaxID = int.Parse(dtCommercialTax.AsEnumerable().Where(x => x.Field<DateTime>("Expire_SDate") <= saleEntity.dtSaleDate && (x.Field<DateTime?>("Expire_EDate") == null || (x.Field<DateTime?>("Expire_EDate") != null) && Convert.ToDateTime(x.Field<DateTime?>("Expire_EDate")) >= saleEntity.dtSaleDate) && x.Field<int>("ShopSectionCode") == data.payment).Select(y => y.Field<int>("CommercialTaxID")).SingleOrDefault().ToString());
                            decPercent = dtCommercialTax.AsEnumerable().Where(x => x.Field<int>("CommercialTaxID") == saleEntity.intCommercialTaxID).SingleOrDefault() != null ? decimal.Parse(dtCommercialTax.AsEnumerable().Where(x => x.Field<int>("CommercialTaxID") == saleEntity.intCommercialTaxID).Select(y => y.Field<decimal>("Percent")).SingleOrDefault().ToString()) : 0;
                            intUnitP = dtCommercialTax.AsEnumerable().Where(x => x.Field<int>("CommercialTaxID") == saleEntity.intCommercialTaxID).SingleOrDefault() != null ? int.Parse(dtCommercialTax.AsEnumerable().Where(x => x.Field<int>("CommercialTaxID") == saleEntity.intCommercialTaxID).Select(y => y.Field<int>("UnitPrice")).SingleOrDefault().ToString()) : 0;
                            if (intUnitP > 0)
                            {
                                saleEntity.intQTY = data.TotalCnt;
                                saleEntity.decUnitPrice = intUnitP;
                            }
                            else if (decPercent > 0)
                            {
                                saleEntity.intQTY = int.Parse(data.TotalAmt.ToString());
                                saleEntity.decUnitPrice = decPercent;
                            }
                            saleEntity.decAmount = saleEntity.intQTY * saleEntity.decUnitPrice;
                            saleEntity.intFixedCostID = 0;
                            saleEntity.intInsuranceID = 0;
                            saleEntity.intOutsourcingID = 0;
                            saleEntity.intPackingID = 0;
                            saleEntity.intSalesCommissionID = 0;
                            saleEntity.strCostType = "Variable";
                            saleEntity.bolFinFlag = false;
                            saleEntity.strShopCategory = data.ShopCategory;
                            saleEntity.intHolidayID = 0;
                            saleEntity.decOverwrite_Amt = 0;
                            saleEntity.strAccTitle = dtCommercialTax.AsEnumerable().Where(x => x.Field<int>("CommercialTaxID") == saleEntity.intCommercialTaxID).Select(y => y.Field<string>("AccountTitle")).SingleOrDefault();
                            saleEntity1.lstSale.Add(saleEntity);
                        }

                        ///<remark>
                        ///Process for SalesCommission.
                        ///</remark>
                        var SaleCom = (from tempsc in dtCsv.AsEnumerable()
                                       where tempsc.Field<string>("店舗区分").Contains("モール")
                                       group tempsc by new
                                       {
                                           ShopCd = tempsc.Field<string>("支店コード"),
                                           SaleDate = tempsc.Field<string>("売上日"),
                                           ShopCategory = tempsc.Field<string>("集計部門")
                                       } into scomm
                                       select new
                                       {
                                           shop = int.Parse(scomm.Key.ShopCd),
                                           sDate = scomm.Key.SaleDate,
                                           TotalAmount = scomm.Sum(y => decimal.Parse(y.Field<string>("合計金額"))),
                                           ShopCategory = scomm.Key.ShopCategory
                                       }).ToList();

                        SaleCom = SaleCom.Where(x => dtSaleCommission.AsEnumerable().Any(y => y.Field<int>("ShopCode") == x.shop)).ToList();

                        foreach (var data in SaleCom)
                        {
                            saleEntity = new T_Sale_Entity();
                            saleEntity.dtSaleDate = Convert.ToDateTime(data.sDate);
                            saleEntity.intBranchCode = data.shop;
                            saleEntity.intSalesCommissionID = int.Parse(dtSaleCommission.AsEnumerable().Where(x => x.Field<int>("ShopCode") == saleEntity.intBranchCode && Convert.ToDateTime(x.Field<DateTime?>("Expire_SDate")) <= saleEntity.dtSaleDate && (x.Field<DateTime?>("Expire_EDate") == null || (x.Field<DateTime?>("Expire_EDate") != null && Convert.ToDateTime(x.Field<DateTime?>("Expire_EDate")) >= saleEntity.dtSaleDate))).Select(y => y.Field<int>("SalesCommissionID")).SingleOrDefault().ToString());
                            saleEntity.decUnitPrice = dtSaleCommission.AsEnumerable().Where(x => x.Field<int>("SalesCommissionID") == saleEntity.intSalesCommissionID).Select(y => y.Field<decimal>("Percent")).SingleOrDefault();
                            saleEntity.intQTY = int.Parse(data.TotalAmount.ToString());
                            saleEntity.decAmount = saleEntity.decUnitPrice * saleEntity.intQTY;
                            saleEntity.intCommercialTaxID = 0;
                            saleEntity.intFixedCostID = 0;
                            saleEntity.intInsuranceID = 0;
                            saleEntity.intOutsourcingID = 0;
                            saleEntity.intPackingID = 0;
                            saleEntity.strCostType = "Variable";
                            saleEntity.bolFinFlag = false;
                            saleEntity.strShopCategory = data.ShopCategory;
                            saleEntity.intHolidayID = 0;
                            saleEntity.decOverwrite_Amt = 0;
                            saleEntity.strAccTitle = dtSaleCommission.AsEnumerable().Where(x => x.Field<int>("SalesCommissionID") == saleEntity.intSalesCommissionID).Select(y => y.Field<string>("AccountTitle")).SingleOrDefault();
                            saleEntity1.lstSale.Add(saleEntity);
                        }

                        ///<remark>
                        ///Process for PackingFare.
                        ///</remark>
                        var PackingFare = (from tempPF in dtCsv.AsEnumerable()
                                           where tempPF.Field<string>("集計部門").Equals("ネット")
                                           group tempPF by new
                                           {
                                               ordertype = tempPF.Field<string>("注文タイプ"),
                                               deliverycd = tempPF.Field<string>("配送会社コード"),
                                               regioncd = tempPF.Field<string>("配送先県コード"),
                                               sdate = tempPF.Field<string>("売上日"),
                                               bqty = tempPF.Field<string>("自転車台数"),
                                               shopCategory = tempPF.Field<string>("集計部門")
                                           } into packfare
                                           select new
                                           {
                                               shop = 0,
                                               otype = packfare.Key.ordertype == "自転車含む" ? true : false,
                                               otypeNm = packfare.Key.ordertype,
                                               sDate = packfare.Key.sdate,
                                               deliveryCd = int.Parse(packfare.Key.deliverycd),
                                               regionCd = packfare.Key.regioncd,
                                               qty = packfare.ToList().Count,
                                               BicycleQty = packfare.Sum(x => int.Parse(x.Field<string>("自転車台数"))),
                                               ShopCategory = packfare.Key.shopCategory
                                           }).ToList();

                        PackingFare = PackingFare.Where(x => dtPack.AsEnumerable().Any(y => y.Field<int>("DeliveryCompanyCode") == x.deliveryCd)).ToList();

                        //<remark Add Logic for regionCd 2021/02/26 Start >
                        DataTable NEED = PackingFare.ConvertListToDt();
                        NEED = NEED.Select("regionCd <> ''").CopyToDataTable();
                        NEED = NEED.Select("regionCd <> '0'").CopyToDataTable();
                        PackingFare = PackingFare.Where(x => NEED.AsEnumerable().Any(y => y.Field<string>("regionCd") == x.regionCd)).ToList();
                        //</remark 2021/02/26 END>

                        foreach (var pack in PackingFare.ToList())
                        {
                            saleEntity = new T_Sale_Entity();
                            saleEntity.dtSaleDate = Convert.ToDateTime(pack.sDate);
                            saleEntity.intBranchCode = pack.shop;
                            saleEntity.intPackingID = int.Parse(dtPack.AsEnumerable().Where(x => x.Field<bool>("OrderType") == pack.otype && x.Field<int>("DeliveryCompanyCode") == pack.deliveryCd
                                && x.Field<int>("RegionCode") == int.Parse(pack.regionCd) && x.Field<DateTime>("Expire_SDate") <= saleEntity.dtSaleDate
                                && (x.Field<DateTime?>("Expire_EDate") == null || (x.Field<DateTime?>("Expire_EDate") != null && Convert.ToDateTime(x.Field<DateTime?>("Expire_EDate")) >= saleEntity.dtSaleDate))).Select(y => y.Field<int>("ID")).SingleOrDefault().ToString());
                            saleEntity.decUnitPrice = dtPack.AsEnumerable().Where(x => x.Field<int>("ID") == saleEntity.intPackingID).SingleOrDefault() != null ? int.Parse(dtPack.AsEnumerable().Where(x => x.Field<int>("ID") == saleEntity.intPackingID).Select(y => y.Field<int>("UnitPrice")).SingleOrDefault().ToString()) : 0;
                            if (pack.otypeNm.Equals("自転車含む"))
                                saleEntity.intQTY = pack.BicycleQty;
                            else
                                saleEntity.intQTY = pack.qty;
                            saleEntity.decAmount = saleEntity.decUnitPrice * saleEntity.intQTY;
                            saleEntity.intCommercialTaxID = 0;
                            saleEntity.intFixedCostID = 0;
                            saleEntity.intHolidayID = 0;
                            saleEntity.intInsuranceID = 0;
                            saleEntity.intOutsourcingID = 0;
                            saleEntity.intSalesCommissionID = 0;
                            saleEntity.strCostType = "Variable";
                            saleEntity.bolFinFlag = false;
                            saleEntity.strShopCategory = pack.ShopCategory;
                            saleEntity.decOverwrite_Amt = 0;
                            saleEntity.strAccTitle = dtPack.AsEnumerable().Where(x => x.Field<int>("ID") == saleEntity.intPackingID).Select(y => y.Field<string>("AccountTitle")).SingleOrDefault();
                            saleEntity1.lstSale.Add(saleEntity);
                        }
                        //
                        ///<remark>
                        ///Process for Insurance.
                        ///</remark>
                        if (dtSyain != null)
                        {
                            foreach (DataRow dr in dtSyain.Rows)
                            {
                                intInsurance = dtInsurance.AsEnumerable().Where(x => (dr.Field<DateTime>("SaleDate") >= x.Field<DateTime>("Expire_SDate") && x.Field<DateTime?>("Expire_EDate") == null) || (dr.Field<DateTime>("SaleDate") >= x.Field<DateTime>("Expire_SDate") && dr.Field<DateTime>("SaleDate") <= x.Field<DateTime?>("Expire_SDate"))).Select(y => y.Field<int>("InsuranceID")).SingleOrDefault();
                                saleEntity = new T_Sale_Entity();
                                saleEntity.dtSaleDate = dr.Field<DateTime>("SaleDate");
                                saleEntity.intBranchCode = 0;
                                saleEntity.intPackingID = 0;
                                saleEntity.intSalesCommissionID = 0;
                                saleEntity.intCommercialTaxID = 0;
                                saleEntity.intOutsourcingID = 0;
                                saleEntity.intInsuranceID = intInsurance;
                                saleEntity.intFixedCostID = 0;
                                decSCost = dr.Field<decimal>("Amount");
                                saleEntity.intQTY = int.Parse(Math.Round(decSCost, 0).ToString());
                                saleEntity.decUnitPrice = dtInsurance.AsEnumerable().Where(x => x.Field<int>("InsuranceID") == intInsurance).Select(y => y.Field<decimal>("Percent")).SingleOrDefault();
                                saleEntity.decAmount = decSCost * saleEntity.decUnitPrice;
                                saleEntity.strCostType = "Variable";
                                saleEntity.bolFinFlag = false;
                                saleEntity.strShopCategory = dr.Field<string>("Category");
                                saleEntity.decOverwrite_Amt = 0;
                                saleEntity.strAccTitle = dtInsurance.AsEnumerable().Where(x => x.Field<int>("InsuranceID") == intInsurance).Select(y => y.Field<string>("AccountTitle")).SingleOrDefault();
                                saleEntity1.lstSale.Add(saleEntity);
                            }
                        }
                        //
                        strXml = objCom.ConvertListToXml(saleEntity1.lstSale);
                        //
                        if (dl_Cost.OverwriteActual(strXml))
                        {
                            objCom.WriteLog("Overwrite Actual Cost (Variable) Complete!");
                        }
                        else
                        {
                            objCom.WriteLog("Overwrite Actual Cost (Variable) Fail!");
                        }
                        //
                        if (dl_Cost.CostData_Insert("InsertXML_VariableCost", strXml))
                            objCom.WriteLog("Import Variable Cost Complete!");
                        else
                            objCom.WriteLog("Import Variable Cost Fail!");
                    }
                    else
                    {
                        objCom.WriteLog("Insert Master data for Variable Cost!");
                    }
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog(ex.Message);
            }
            finally
            {
                objCom.WriteLog("Variable Cost End");
                Environment.Exit(0);
            }
        }

    }
}

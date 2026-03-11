using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4.Qbei_Uriage_Common;
using _3.Qbei_Uriage_DL;
using System.Data;
using System.Web.Services;
using System.IO;

namespace Qbei_Uriage.PlaningData
{
    public partial class ImportPlaningData : System.Web.UI.Page
    {
        static Cost_DL dalCost = new Cost_DL();
        Dictionary<string, string> dicData;
        DataTable dtPlanning;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvPlaning.PageSize = int.Parse(ddlPageSize.SelectedValue);

                DataTable dtShopName = dalCost.ShopCategory_Select();
                if (dtShopName != null)
                {
                    ddlShopCategory.DataMember = "ID";
                    ddlShopCategory.DataValueField = "Shop";
                    ddlShopCategory.DataSource = dtShopName;
                    ddlShopCategory.DataBind();
                }
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
            gvPlaning.PageIndex = 0;
            gvPlaning.PageSize = int.Parse(ddlPageSize.SelectedValue);
        }

        protected void gvPlaning_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlaning.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPlaning.PageIndex = 0;
            gvPlaning.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }
        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvPlaning.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvPlaning.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                txtGoto.Text = string.Empty;
                BindData();
            }
        }
        private void BindData()
        {
            dicData = new Dictionary<string, string>();

            dicData["StartDate"] = String.IsNullOrEmpty(txtSD.Value) ? null : txtSD.Value;
            dicData["EndDate"] = String.IsNullOrEmpty(txtED.Value) ? null : txtED.Value;
            dicData["AccTitle"] = String.IsNullOrEmpty(txtFCAccountTitle.Text) ? null : txtFCAccountTitle.Text;
            dicData["Amount"] = String.IsNullOrEmpty(txtAmt.Text) ? null : decimal.Parse(txtAmt.Text).ToString();
            dicData["ShopCategory"] = String.IsNullOrEmpty(ddlShopCategory.SelectedItem.Text) ? null : ddlShopCategory.SelectedItem.Text;
            dtPlanning = dalCost.PlaningCost_Select(dicData);

            gvPlaning.DataSource = dtPlanning;
            gvPlaning.DataBind();

            if (dtPlanning == null) lblCnt.Text = "0";
            else lblCnt.Text = dtPlanning.Rows.Count.ToString();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            Common objComm = new Common();
            T_Sale_Entity saleentity = new T_Sale_Entity();
            List<T_Sale_Entity> saleTemp;
            DataTable dtFCost = new DataTable();
            DataTable dtHoliday = new DataTable();
            DataTable dtCsv = new DataTable();
            T_Sale_Entity objsale = new T_Sale_Entity();
            DataTable dt = new DataTable();
            int intHolidayId = 0;
            String strXml = string.Empty;
            double dTotal, dTime;
            int intRowCnt = 0;
            try
            {
                objComm.WriteLog("Planning Data Import Start");
                fileUpload.SaveAs(Server.MapPath(fileUpload.PostedFile.FileName));
                dtCsv = objComm.ReadCsv(Server.MapPath(fileUpload.PostedFile.FileName));
                if (dtCsv != null)
                {
                    //Get Holiday Master Data
                    dtHoliday = dalCost.CostMaster_Select("HolidayMaster_SelectAll");
                    //Get Fixed Cost Master Data
                    dtFCost = dalCost.CostMaster_Select("FixedCostMaster_SelectAll");
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
                    dTotal = saleentity.lstSale.Count;
                    dTime = int.Parse(Math.Ceiling(dTotal / 25000).ToString());
                    for (int i = 1; i <= dTime; i++)
                    {
                        saleTemp = new List<T_Sale_Entity>();
                        if (i == 1)
                            intRowCnt = 0;
                        else
                            intRowCnt += 25000;
                        if (i == dTime)
                        {
                            dTotal = dTotal - intRowCnt;
                            saleTemp = saleentity.lstSale.GetRange(intRowCnt, int.Parse(dTotal.ToString()));
                        }
                        else
                            saleTemp = saleentity.lstSale.GetRange(intRowCnt, 25000);
                        strXml = objComm.ConvertListToXml(saleTemp);
                        if (dalCost.CostData_Insert("InsertXML_Cost", strXml))
                        {
                            objComm.WriteLog("Planning Data import complete!");

                        }
                        else
                            objComm.WriteLog("Planning Data import fail!");
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('取り込み処理完了しました。')", true);
                    BindData();
                }
            }
            catch (Exception ex)
            {
                objComm.WriteLog(ex.Message);
            }
            finally
            {
                File.Delete(Server.MapPath(fileUpload.PostedFile.FileName));
                objComm.WriteLog("Planning Data Import End");
            }
        }
    }
}
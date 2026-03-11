using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Qbei_Uriage_Common;
using Qbei_Uriage_BL;

namespace Qbei_Uriage.SalesCommission
{
    public partial class SalesCommissionEntry : System.Web.UI.Page
    {
        static SalesCommission_BL balSalesC = new SalesCommission_BL();
        static SalesCommission_Entity salesEntity;
        DataTable dtSales;
        static string strUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] != null)
                    strUser = (Session["UserInfo"] as string[])[1];
                if (String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hdSaleCommissionId.Value = "0";
                }
                else {
                    hdSaleCommissionId.Value = Request.QueryString["ID"];
                    btnSave.Text = "更新";
                    BindData();
                    txtSAcc.ReadOnly = true;
                }
            }
        }

        private void BindData() 
        {
            DateTime? dtED;
            decimal dPercent;
            salesEntity = new SalesCommission_Entity();
            salesEntity.SalesCommissionID = hdSaleCommissionId.Value;
            dtSales = balSalesC.SalesCommissionMaster_Select(salesEntity);
            if (dtSales != null)
            {
                txtSAcc.Text = dtSales.AsEnumerable().Single(x => x.Field<int>("SalesCommissionID") == int.Parse(hdSaleCommissionId.Value)).Field<string>("AccountTitle");
                txtShopCd.Text = dtSales.AsEnumerable().Single(x => x.Field<int>("SalesCommissionID") == int.Parse(hdSaleCommissionId.Value)).Field<int>("ShopCode").ToString();
                dPercent = dtSales.AsEnumerable().Single(x => x.Field<int>("SalesCommissionID") == int.Parse(hdSaleCommissionId.Value)).Field<decimal>("Percent");
                txtPercent.Text = (dPercent * 100).ToString("###");
                txtSD.Value = dtSales.AsEnumerable().Single(x => x.Field<int>("SalesCommissionID") == int.Parse(hdSaleCommissionId.Value)).Field<DateTime>("Expire_SDate").ToString("yyyy/MM/dd");
                dtED = dtSales.AsEnumerable().Single(x => x.Field<int>("SalesCommissionID") == int.Parse(hdSaleCommissionId.Value)).Field<DateTime?>("Expire_EDate");
                txtED.Value = dtED==null ? string.Empty : dtED.Value.ToString("yyyy/MM/dd");
            }
        }

        [WebMethod]
        public static string SalesCommissionSave(int intId, string strAcc, string strShopCd, string strPer, string strSD, string strED)
        {
            try {
                string strMsg = string.Empty;
                salesEntity = new SalesCommission_Entity();
                salesEntity.AccountTitle = strAcc;
                salesEntity.ShopCode = strShopCd;
                salesEntity.Percent = (decimal.Parse( strPer) / 100).ToString();
                salesEntity.Expire_SDate = strSD;
                if (!String.IsNullOrEmpty(strED))
                    salesEntity.Expire_EDate = strED;
                if (intId == 0)
                {
                    salesEntity.CreatedBy = strUser;
                    if (balSalesC.SalesCommission_Insert(salesEntity))
                        strMsg = "登録完了しました";
                }
                else {
                    salesEntity.SalesCommissionID = intId.ToString();
                    salesEntity.ModifiedBy = strUser;
                    if (balSalesC.SalesCommission_Update(salesEntity))
                        strMsg = "更新完了しました";
                }
                return strMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qbei_Uriage_BL;
using Qbei_Uriage_Common;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data;

namespace Qbei_Uriage.SalesCommission
{
    public partial class SaleCommissionListing : System.Web.UI.Page
    {
        static SalesCommission_Entity scommEntity;
        static SalesCommission_BL balSComm = new SalesCommission_BL();
        DataTable dtSComm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) gvSaleCommission.PageSize = int.Parse(ddlPageSize.SelectedValue);
            BindData();
        }

        private void BindData()
        {
            scommEntity = new SalesCommission_Entity();
            dtSComm = new DataTable();
            if (String.IsNullOrEmpty(txtAcc.Text) && String.IsNullOrEmpty(txtShopCd.Text) && String.IsNullOrEmpty(txtSCPer.Text) && String.IsNullOrEmpty(txtSD.Value) && String.IsNullOrEmpty(txtED.Value))
            {
                dtSComm = balSComm.SalesCommission_SelectAll();
            }
            else
            {
                scommEntity.AccountTitle = String.IsNullOrEmpty(txtAcc.Text) ? null : txtAcc.Text;
                scommEntity.ShopCode = String.IsNullOrEmpty(txtShopCd.Text) ? null : txtShopCd.Text;
                scommEntity.Percent = String.IsNullOrEmpty(txtSCPer.Text) ? null : (decimal.Parse(txtSCPer.Text) / 100).ToString();
                scommEntity.Expire_SDate = String.IsNullOrEmpty(txtSD.Value) ? null : txtSD.Value;
                scommEntity.Expire_EDate = String.IsNullOrEmpty(txtED.Value) ? null : txtED.Value;
                dtSComm = balSComm.SalesCommissionMaster_Select(scommEntity);
            }
            if (dtSComm != null)
            {
                gvSaleCommission.DataSource = dtSComm;
                gvSaleCommission.DataBind();
                lblCnt.Text = dtSComm.Rows.Count.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
            gvSaleCommission.PageIndex = 0;
            gvSaleCommission.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
        }

        protected void gvSaleCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSaleCommission.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSaleCommission.PageIndex = 0;
            gvSaleCommission.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvSaleCommission.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvSaleCommission.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                txtGoto.Text = string.Empty;
                BindData();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdId = (HiddenField)row.FindControl("hdSaleCommissionId");
            Response.Redirect("~/SalesCommission/SalesCommissionEntry.aspx?ID=" + hdId.Value);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SalesCommission/SalesCommissionEntry.aspx");
        }
        [WebMethod]
        public static string SCommissionDelete(string strId)
        {
            try
            {
                scommEntity = new SalesCommission_Entity();
                scommEntity.SalesCommissionID = strId;
                if (balSComm.SalesCommissionMaster_Delete(scommEntity))
                {
                    return "Ok";
                }
                else return "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
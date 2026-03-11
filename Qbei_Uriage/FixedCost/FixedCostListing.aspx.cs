using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Qbei_Uriage_Common;
using Qbei_Uriage_BL;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace Qbei_Uriage.FixedCost
{
    public partial class FixedCostListing : System.Web.UI.Page
    {
        static FixedCost_BL balFC = new FixedCost_BL();
        static FixedCost_Entity fcEntity;
        DataTable dtFixedC;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvFixedC.PageSize = int.Parse(ddlPageSize.SelectedValue);
                BindData();
                DataTable dtShopName = balFC.ShopCategory_Select();
                if (dtShopName != null)
                {
                    ddlShopCategory.DataMember = "ID";
                    ddlShopCategory.DataValueField = "Shop";
                    ddlShopCategory.DataSource = dtShopName;
                    ddlShopCategory.DataBind();
                }

            }
        }

        private void BindData()
        {
            fcEntity = new FixedCost_Entity();
            if (String.IsNullOrEmpty(txtFCAccountTitle.Text) && String.IsNullOrEmpty(ddlShopCategory.SelectedItem.Text))
            {
                dtFixedC = balFC.FixedCost_SelectAll();
            }
            else 
            {
                fcEntity.AccountTitle = String.IsNullOrEmpty(txtFCAccountTitle.Text) ? null : txtFCAccountTitle.Text;
                fcEntity.ShopCategory = String.IsNullOrEmpty(ddlShopCategory.SelectedValue.ToString()) ? null : ddlShopCategory.SelectedItem.Text;
                dtFixedC = balFC.FixedCost_Select(fcEntity);
            }
            if (dtFixedC != null)
            {
                gvFixedC.DataSource = dtFixedC;
                gvFixedC.DataBind();
                lblCnt.Text = dtFixedC.Rows.Count.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvFixedC.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            gvFixedC.PageIndex = 0;
            BindData();
        }

        protected void gvFixedC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFixedC.PageIndex = e.NewPageIndex;
            gvFixedC.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvFixedC.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                gvFixedC.PageIndex = int.Parse(txtGoto.Text) - 1;
                BindData();
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvFixedC.PageIndex = 0;
            gvFixedC.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdId = (HiddenField)row.FindControl("hdFixedCostId");
            Response.Redirect("~/FixedCost/FixedCostEntry.aspx?ID=" + hdId.Value);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FixedCost/FixedCostEntry.aspx");
        }

        [WebMethod]
        public static string FixedCostDelete (string strId)
        {
            try {
                fcEntity = new FixedCost_Entity();
                fcEntity.FixedCostID = strId;
                if (balFC.FixedCost_Delete(fcEntity))
                    return "Ok";
                else return "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
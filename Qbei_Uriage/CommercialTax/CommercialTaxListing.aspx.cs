using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using Qbei_Uriage_BL;
using Qbei_Uriage_Common;
using System.Data;

namespace Qbei_Uriage.CommercialTax
{
    public partial class CommercialTaxListing : System.Web.UI.Page
    {
        static CommercialTax_Entity commEntity;
        static CommercialTax_BL balComm = new CommercialTax_BL();
        DataTable dtComm;
        static string strUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] != null)
                strUser = (Session["UserInfo"] as string[])[1];

            if (!IsPostBack)
                gvCommercial.PageSize = int.Parse(ddlPageSize.SelectedValue);
            BindData();
        }

        private void BindData()
        {
            commEntity = new CommercialTax_Entity();
            dtComm = new DataTable();
            if (String.IsNullOrEmpty(txtCAcc.Text) && String.IsNullOrEmpty(txtPayment.Text) && String.IsNullOrEmpty(txtCUPrice.Text) && String.IsNullOrEmpty(txtCPer.Text) && String.IsNullOrEmpty(txtSD.Value) && String.IsNullOrEmpty(txtED.Value))
            {
                dtComm = balComm.CommercialTax_SelectAll();
            }
            else
            {
                commEntity.AccountTitle = String.IsNullOrEmpty(txtCAcc.Text) ? null : txtCAcc.Text;
                commEntity.ShopSectionCode = String.IsNullOrEmpty(txtPayment.Text) ? null : txtPayment.Text;
                commEntity.UnitPrice = String.IsNullOrEmpty(txtCUPrice.Text) ? null : txtCUPrice.Text;
                commEntity.Percent = String.IsNullOrEmpty(txtCPer.Text) ? null : (decimal.Parse(txtCPer.Text) / 100).ToString();
                commEntity.Expire_SDate = String.IsNullOrEmpty(txtSD.Value) ? null : txtSD.Value;
                commEntity.Expire_EDate = String.IsNullOrEmpty(txtED.Value) ? null : txtED.Value;
                dtComm = balComm.CommercialTaxMaster_Select(commEntity);
            }
            if (dtComm != null)
            {
                gvCommercial.DataSource = dtComm;
                gvCommercial.DataBind();
                lblCnt.Text = dtComm.Rows.Count.ToString();
            }
        }

        protected void gvCommercial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommercial.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
            gvCommercial.PageIndex = 0;
            gvCommercial.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdnId = (HiddenField)row.FindControl("hdCommercialId");
            Response.Redirect("~/CommercialTax/CommercialTaxEntry.aspx?ID=" + hdnId.Value);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CommercialTax/CommercialTaxEntry.aspx");
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            BindData();
            gvCommercial.PageIndex = int.Parse(txtGoto.Text) - 1;
            gvCommercial.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            txtGoto.Text = string.Empty;
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCommercial.PageIndex = 0;
            gvCommercial.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        [WebMethod]
        public static string CommercialTaxDelete(string strId)
        {
            try
            {
                commEntity = new CommercialTax_Entity();
                commEntity.CommercialTaxID = strId;
                commEntity.ModifiedBy = strUser;
                if (balComm.CommercialTax_Delete(commEntity))
                    return "Ok";
                else
                    return "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
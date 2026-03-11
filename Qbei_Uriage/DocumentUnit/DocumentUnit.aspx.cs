using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2.Qbei_Uriage_BL;
using System.Data;
using _4.Qbei_Uriage_Common;

namespace Qbei_Uriage.DocumentUnit
{
    public partial class DocumentUnit : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DocumentUnit_Entity de = new DocumentUnit_Entity();
        DocumentUnit_BL dbl = new DocumentUnit_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvDocumentUnit.PageSize = int.Parse(ddlPageSize.SelectedValue);
                dt = dbl.DocumentUnit_SelectAll();
                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvDocumentUnit.DataSource = dt;
                gvDocumentUnit.DataBind();
                hide_panel.Style.Add("display", "none");
            }

        }
        protected void gvDocumentUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocumentUnit.PageIndex = e.NewPageIndex;
            Getdata();
        }
        protected void gvDocumentUnit_Indexchanged(object sender, EventArgs e)
        {
            gvDocumentUnit.PageIndex = 0;
            gvDocumentUnit.PageSize = Convert.ToInt32(ddlPageSize.Text);
            Getdata();
        }
        public void btnSearch_Click(object sender, EventArgs e)
        {
            Getdata();

        }
        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGoto.Text))
            {
                gvDocumentUnit.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                txtGoto.Text = string.Empty;
                //gvDocumentUnit.PageSize = Convert.ToInt32(ddlPageSize.Text);
                Getdata();
            }
        }
        private void Getdata()
        {
            de.SaleDate = txtSalesDate.Value;
            de.OrderNo = txtOrderNo.Text;
            de.ShippingCost = txtShippingCost.Text;
            de.ConsumptionTax = txtConsumptionTax.Text;
            de.TotalAmount = txtTotalAmount.Text;
            de.BranchName = txtBranchName.Text;
            de.Cod = txtCod.Text;
            de.UsagePoint = txtUsagePoint.Text;
            de.AdditionalPoint = txtAdditionalPoint.Text;
          
            dt = dbl.DocumentUnit_Search(de);
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvDocumentUnit.DataSource = dt;
                gvDocumentUnit.DataBind();
            }
            else
            {
                gvDocumentUnit.DataSource = dt;
                gvDocumentUnit.DataBind();

            }
        }

    }
}
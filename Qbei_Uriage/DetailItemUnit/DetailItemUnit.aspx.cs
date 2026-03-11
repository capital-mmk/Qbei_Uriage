using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2.Qbei_Uriage_BL;
using System.Data;
using _4.Qbei_Uriage_Common;

namespace Qbei_Uriage.DetailItemUnit
{
    public partial class DetailItemUnit : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DetailItemUnit_Entity de = new DetailItemUnit_Entity();
        DetailItemUnit_BL dbl = new DetailItemUnit_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvDetailItemUnit.PageSize = int.Parse(ddlPageSize.SelectedValue);
                dt = dbl.DetailItemUnit_SelectAll();
                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvDetailItemUnit.DataSource = dt;
                gvDetailItemUnit.DataBind();
                hide_panel.Style.Add("display", "none");
            }
        }
        protected void gvDetailItemUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetailItemUnit.PageIndex = e.NewPageIndex;
            Getdata();
        }
        protected void gvDetailItemUnit_Indexchanged(object sender, EventArgs e)
        {
            gvDetailItemUnit.PageIndex = 0;
            gvDetailItemUnit.PageSize = Convert.ToInt32(ddlPageSize.Text);
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
                gvDetailItemUnit.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                txtGoto.Text = string.Empty;
               // gvDetailItemUnit.PageSize = Convert.ToInt32(ddlPageSize.Text);
                Getdata();
            }
        }
        private void Getdata()
        {
            de.SaleDate = txtSalesDate.Value;
            de.OrderNo = txtOrderNo.Text;
            de.PartNo = txtPartNo.Text;
            de.UnitPrice = txtUnitPrice.Text;
            de.Quantity = txtQuantity.Text;
            de.Cost = txtCost.Text;
            de.Amount = txtAmount.Text;
            de.Coupon = txtCoupon.Text;
            de.Discount = txtDiscount.Text;
            de.BranchName = txtBranchName.Text;
            de.ModifiedDate = txtModifiedDate.Value;
            dt = dbl.DetailItemUnit_Search(de);
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvDetailItemUnit.DataSource = dt;
                gvDetailItemUnit.DataBind();
            }
            else
            {
                gvDetailItemUnit.DataSource = dt;
                gvDetailItemUnit.DataBind();

            }
        }

    }
}
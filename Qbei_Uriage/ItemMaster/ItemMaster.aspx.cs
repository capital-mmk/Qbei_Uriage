using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4.Qbei_Uriage_Common;
using _2.Qbei_Uriage_BL;
using System.Data;
using System.Web.ClientServices;
using System;
namespace Qbei_Uriage.ItemMaster
{
    public partial class ItemMaster : System.Web.UI.Page
    {
        ItemMaster_BL ibl = new ItemMaster_BL();

        DataTable dt = new DataTable();

        ItemMaster_Entity ie = new ItemMaster_Entity();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = ibl.ItemMaster_SelectAll();
                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvItemMaster.DataSource = dt;
                gvItemMaster.DataBind();
                //gvItemMaster.HeaderStyle.a
                //d();
                //collapses();
                //d();

            }
        }

        protected void d()
        {
            string[] arr;
            arr = new string[gvItemMaster.Rows.Count];
            for (int i = 0; i < gvItemMaster.Columns.Count;)
            {
                //gvItemMaster.Columns[i].HeaderStyle.Width = gvItemMaster.Columns[i].ItemStyle.Width;


            }
            //for (int i = 0; i < GridView1.Columns.Count; i++)
            //{

            //}
        }

        protected void collapses()
        {

            Page.ClientScript.RegisterStartupScript(
     this.GetType(),
     "clickLink",
     "ClickLink();",
     true);
            //click_collapse.Attributes.Remove("class");
            ////click_collapse.Attributes.Add("class", "pull-right clickable panel-collapsed");
            //cep.Attributes.Remove("class");
            //cep.Attributes.Add("class", "glyphicon glyphicon-chevron-up");



            //ch
            //click_collapse.Attributes.Add("class", "pull-right clickable panel-collapsed");
            //glyphicon glyphicon-chevron-down


        }

        //private Type getType()
        //{
        //    throw new NotImplementedException();
        //}

        public void btnSearch_Click(object sender, EventArgs e)
        {
            Getdata();

        }

        protected void gvItemMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemMaster.PageIndex = e.NewPageIndex;
            Getdata();

        }

        protected void gvItemMaster_Indexchanged(object sender, EventArgs e)
        {
            gvItemMaster.PageIndex = 0;
            gvItemMaster.PageSize = Convert.ToInt32(ddlPageSize.Text);
            Getdata();
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGoto.Text))
            {
                gvItemMaster.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                // gvItemMaster.PageSize = Convert.ToInt32(ddlPageSize.Text);
                txtGoto.Text = string.Empty;
                Getdata();
            }
        }

        private void Getdata()
        {
            ie.SaleDate = txtSaleDate.Value;
            ie.CancelDate = txtCancelDate.Value;
            ie.OrderNo = txtOrderNo.Text;
            ie.PartNo = txtPartNo.Text;
            ie.UnitPrice = txtUnitPrice.Text;
            ie.Quantity = txtQuantity.Text;
            ie.Cost = txtCost.Text;
            ie.Amount = txtAmount.Text;
            ie.ShippingCost = txtShippingCost.Text;
            ie.BranchCode = txtBranchCode.Text;
            ie.DeliveryCharge = txtDeliveryCharge.Text;
            ie.UsagePoint = txtUsagePoint.Text;
            ie.Coupon = txtCoupon.Text;
            ie.Discount = txtDiscount.Text;
            ie.Modified_Date = txtModifiedDate.Value;

            dt = ibl.ItemMaster_Search(ie);
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvItemMaster.DataSource = dt;
                gvItemMaster.DataBind();
            }
            else
            {
                gvItemMaster.DataSource = dt;
                gvItemMaster.DataBind();

            }
        }

        protected void rv_rowdataBound(object sender, GridViewRowEventArgs e)
        {
            //DataTable dt_temp = new DataTable();
            //string[] arr;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    arr = new string[e.Row.Cells.Count];
            //    for (int i = 0; i < e.Row.Cells.Count; i++)
            //    {

            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //Label lbl = new Label();
            //for (int i = 0; i < e.Row.Cells.Count; i++)
            //{

            //    string w = e.Row.Cells[i].Text;
            //    e.Row.Cells[i].Attributes.CssStyle.Add("width","200px");
            //}

            //    rv_rowdataBound}


            //System.Data.DataRowView drv;
            //drv = (System.Data.DataRowView)e.Row.DataItem;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (drv != null)
            //    {
            //        String catName = drv[1].ToString();
            //        Response.Write(catName + "/");

            //        int catNameLen = catName.Length;
            //        if (catNameLen > widestData)
            //        {
            //            widestData = catNameLen;
            //            GridView1.Columns[2].ItemStyle.Width =
            //              widestData * 30;
            //            GridView1.Columns[2].ItemStyle.Wrap = false;
            //        }

            //    }
            //}
        }

    }
    }


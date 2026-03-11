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
//using System;

namespace Qbei_Uriage.ItemMaster
{
    public partial class IMtest : System.Web.UI.Page
    {
        ItemMaster_BL ibl = new ItemMaster_BL();
        public bool status = false;
        DataTable dt = new DataTable();

        ItemMaster_Entity ie = new ItemMaster_Entity();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int currentYear = DateTime.Now.Year;
                string fromDate = new DateTime(currentYear - 1, 1, 1).ToString("yyyy/MM/dd");
                string toDate = new DateTime(currentYear, 12, 31).ToString("yyyy/MM/dd");

                txtSaleDateFrom.Value = fromDate;
                txtSaleDateTo.Value = toDate;

                ie = new ItemMaster_Entity();
                ie.SaleDate = fromDate;
                ie.SaleDateTo = toDate;
                dt = ibl.ItemMaster_Search(ie);

                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvItemMaster.DataSource = dt;
                gvItemMaster.DataBind();
                //gvItemMaster.HeaderStyle.a
                //d();
                //collapses();
                //d();
                hide_now.Style.Add("display", "none");
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
            ie = new ItemMaster_Entity();
            ie.SaleDate = txtSaleDateFrom.Value;
            ie.SaleDateTo = txtSaleDateTo.Value;
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
            if (dt.Rows.Count > 0)
            {
                status = true;
            }
        }

        protected void gvpre_render(object sender, EventArgs e)
        {
            gvItemMaster.UseAccessibleHeader = true;
            gvItemMaster.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4.Qbei_Uriage_Common;
using _2.Qbei_Uriage_BL;
using System.Data;


namespace Qbei_Uriage.Item
{
    public partial class Item : System.Web.UI.Page
    {
        ItemMaster_BL ibl = new ItemMaster_BL();
        DataTable dt = new DataTable();
        ItemEntity ie = new ItemEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvItem.PageSize = int.Parse(ddlPageSize.SelectedValue);
                dt = ibl.Item_SelectAll();
                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvItem.DataSource = dt;
                gvItem.DataBind();
                hide_panel.Style.Add("display", "none");
            }
        }


        
        public void btnSearch_Click(object sender, EventArgs e)
        {
            Getdata();
         
        }

        protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItem.PageIndex = e.NewPageIndex;
            Getdata();
          
        }
        protected void gvItem_Indexchanged(object sender, EventArgs e)
        {
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            gvItem.PageIndex = 0;
            gvItem.PageSize = Convert.ToInt32(ddlPageSize.Text);
            Getdata();
        }
        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGoto.Text))
            {
               
                gvItem.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                //gvItem.PageSize = Convert.ToInt32(ddlPageSize.Text);
                txtGoto.Text = string.Empty;
                Getdata();
            }
        }
        private void Getdata()
        {
            dt = new DataTable();
            ie.PartNo = txtPartNo.Text;
            ie.BrandCode = txtBrandCode.Text;
          
            dt = ibl.Item_Search(ie);
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvItem.DataSource = dt;
                gvItem.DataBind();
            }
            else
            {
                gvItem.DataSource = dt;
                gvItem.DataBind();

            }
        }
    }
}
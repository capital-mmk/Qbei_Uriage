using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4.Qbei_Uriage_Common;
using _2.Qbei_Uriage_BL;
using System.Data;


namespace Qbei_Uriage.Brand
{
    public partial class BrandList : System.Web.UI.Page
    {
        Brand_BL bbl = new Brand_BL();
        DataTable dt = new DataTable();
        BrandEntity be = new BrandEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvBrand.PageSize = int.Parse(ddlPageSize.SelectedValue);
                dt = bbl.brand_SelectAll();
                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvBrand.DataSource = dt;
                gvBrand.DataBind();
                hide_panel.Style.Add("display", "none");
            }
        }
        public void btnSearch_Click(object sender, EventArgs e)
        {
            Getdata();
         
            //dt = new DataTable();
            //be.BrandCode = txtBrandCode.Text;
            //be.BrandName = txtBrandName.Text;
            //be.BrandUrl = txtBrandUrl.Text;
            //be.Modified_Date = txtModifiedDate.Text;
         
            //dt = bbl.Brand_Search(be);
          


            //if (dt.Rows.Count > 0)
            //{
                

            //    gvBrand.DataSource = dt;
            //    gvBrand.DataBind();
            //}

            //else
            //{
            //    gvBrand.DataSource = dt;
            //    gvBrand.DataBind();
               
            //}
        }
        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGoto.Text))
            {
                gvBrand.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                txtGoto.Text = string.Empty;
               // gvBrand.PageSize = Convert.ToInt32(ddlPageSize.Text);
                Getdata();
            }
        }
        protected void gvBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBrand.PageIndex = e.NewPageIndex;
            Getdata();

        }
        protected void gvBrand_Indexchanged(object sender, EventArgs e)
        {
            gvBrand.PageIndex = 0;
            gvBrand.PageSize = Convert.ToInt32(ddlPageSize.Text);
            Getdata();
        }
        private void Getdata()
        {
            dt = new DataTable();
            be.BrandCode = txtBrandCode.Text;
            be.BrandName = txtBrandName.Text;
            be.BrandUrl = txtBrandUrl.Text;
            be.Modified_Date = txtModifiedDate.Value;

            dt = bbl.Brand_Search(be);
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvBrand.DataSource = dt;
                gvBrand.DataBind();
            }
            else
            {
                gvBrand.DataSource = dt;
                gvBrand.DataBind();

            }
        }
    }
}
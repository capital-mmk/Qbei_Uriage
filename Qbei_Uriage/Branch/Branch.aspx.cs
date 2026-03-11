using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2.Qbei_Uriage_BL;
using _4.Qbei_Uriage_Common;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace Qbei_Uriage.Branch
{
    public partial class Branch : System.Web.UI.Page
    {

        static Branch_BL bbl = new Branch_BL();
        DataTable dt = new DataTable();
        static Branch_Entity be = new Branch_Entity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvBranch.PageSize = int.Parse(ddlPageSize.SelectedValue);
                dt = bbl.Branch_SelectAll();
                lblrowCount.Text = Convert.ToString(dt.Rows.Count);
                gvBranch.DataSource = dt;
                gvBranch.DataBind();
                hide_panel.Style.Add("display", "none");
            }
        }
        public void btnSearch_Click(object sender, EventArgs e)
        {
            Getdata();

        }
        protected void gvBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBranch.PageIndex = e.NewPageIndex;
            Getdata();

        }
        protected void gvBranch_Indexchanged(object sender, EventArgs e)
        {
            gvBranch.PageIndex = 0;
            gvBranch.PageSize = Convert.ToInt32(ddlPageSize.Text);
            Getdata();
        }
        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGoto.Text))
            {
                gvBranch.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                txtGoto.Text = string.Empty;
                Getdata();
            }
        }
        private void Getdata()
        {
            be.BranchCode = txtBranchCode.Text;
            be.BranchName = txtBranchName.Text;
            be.BrandShortName = txtBrandShortName.Text;
            be.SotreCategory = txtSotreCategory.Text;
            be.Summary = txtSummary.Text;

            dt = bbl.Branch_Search(be);
            lblrowCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvBranch.DataSource = dt;
                gvBranch.DataBind();
            }
            else
            {
                gvBranch.DataSource = dt;
                gvBranch.DataBind();

            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor anc = sender as HtmlAnchor;
            GridViewRow Grow = (GridViewRow)anc.NamingContainer;
            string BranchCode = ((Label)Grow.FindControl("lblBranchCode")).Text;
            Response.Redirect("BranchEntry.aspx?ID=" + BranchCode);

        }
        [WebMethod]
        public static string BranchDelete(string strId)
        {
            try
            {
                be = new Branch_Entity();
                be.BranchCode = strId;
                if (bbl.BranchDelete(be))
                    return "Ok";
                else
                    return "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Branch/BranchEntry.aspx");
        }
    }
}
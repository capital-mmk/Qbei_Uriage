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

namespace Qbei_Uriage.WorkingDays
{
    public partial class WorkingDaysListing : System.Web.UI.Page
    {
        static WorkingDays_BL balWD = new WorkingDays_BL();
        static WorkingDays_Entity workingEntity;
        DataTable dtWD;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvWorkingDays.PageSize = int.Parse(ddlPageSize.SelectedValue);
                BindData();
            }
        }

        private void BindData()
        {
            workingEntity = new WorkingDays_Entity();
            if (String.IsNullOrEmpty(txtYM.Value) && String.IsNullOrEmpty(ddlType.SelectedItem.Text)
                && String.IsNullOrEmpty(txtDays.Text))
            {
                dtWD = balWD.WorkingDays_SelectAll();
            }
            else
            {
                workingEntity.YearMonth = String.IsNullOrEmpty(txtYM.Value) ? null : txtYM.Value.Replace("/", string.Empty);
                workingEntity.ShopName = String.IsNullOrEmpty(ddlType.SelectedItem.Text) ? null : ddlType.SelectedItem.Text;
                workingEntity.WorkingDays = String.IsNullOrEmpty(txtDays.Text) ? null : txtDays.Text;
                dtWD = balWD.WorkingDays_Select(workingEntity);
            }
            if (dtWD != null)
            {
                gvWorkingDays.DataSource = dtWD;
                gvWorkingDays.DataBind();
                lblCnt.Text = dtWD.Rows.Count.ToString();
            }
        }

        protected void gvWorkingDays_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorkingDays.PageIndex = e.NewPageIndex;
            gvWorkingDays.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnWorkingDaysId");
            Response.Redirect("~/WorkingDays/WorkingDaysEntry.aspx?ID=" + hdnId.Value);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WorkingDays/WorkingDaysEntry.aspx");
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvWorkingDays.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvWorkingDays.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                txtGoto.Text = string.Empty;
                BindData();
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvWorkingDays.PageIndex = 0;
            gvWorkingDays.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvWorkingDays.PageIndex = 0;
            gvWorkingDays.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        [WebMethod]
        public static string WorkingDaysDelete(string strId)
        {
            try
            {
                workingEntity = new WorkingDays_Entity();
                workingEntity.WorkingDaysID = strId;
                if (balWD.WorkingDays_Delete(workingEntity))
                {
                    return "Ok";
                }
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
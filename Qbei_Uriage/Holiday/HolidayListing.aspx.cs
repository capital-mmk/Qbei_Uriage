using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using Qbei_Uriage_Common;
using Qbei_Uriage_BL;

namespace Qbei_Uriage.Holiday
{
    public partial class HolidayListing : System.Web.UI.Page
    {
        static Holiday_BL balHoliday = new Holiday_BL();
        static Holiday_Entity holidayEntity;
        DataTable dtHoliday;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvHoliday.PageSize = int.Parse(ddlPageSize.SelectedValue);
                BindData();
            }
        }

        private void BindData()
        {
            holidayEntity = new Holiday_Entity();
            dtHoliday = new DataTable();
            if (String.IsNullOrEmpty(txtHolidayDT.Value) && String.IsNullOrEmpty(txtHName.Text))
            {
                dtHoliday = balHoliday.Holiday_SelectAll();
            }
            else
            {
                holidayEntity.HolidayDate = String.IsNullOrEmpty(txtHolidayDT.Value) ? null : txtHolidayDT.Value;
                holidayEntity.HolidayName = String.IsNullOrEmpty(txtHName.Text) ? null : txtHName.Text;
                dtHoliday = balHoliday.Holiday_Select(holidayEntity);
            }
            if (dtHoliday != null)
            {
                gvHoliday.DataSource = dtHoliday;
                gvHoliday.DataBind();
                lblCnt.Text = dtHoliday.Rows.Count.ToString();
            }
        }

        protected void gvHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHoliday.PageIndex = e.NewPageIndex;
            gvHoliday.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvHoliday.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvHoliday.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) 
        {
            gvHoliday.PageIndex = 0;
            gvHoliday.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdId = (HiddenField)row.FindControl("hdHolidayId");
            Response.Redirect("~/Holiday/HolidayEntry.aspx?ID=" + hdId.Value);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Holiday/HolidayEntry.aspx");
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e) {
            gvHoliday.PageIndex = 0;
            gvHoliday.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        [WebMethod]
        public static string HolidayDelete(string strId)
        {
            try {
                holidayEntity = new Holiday_Entity();
                holidayEntity.HolidayID = strId;
                if (balHoliday.Holiday_Delete(holidayEntity))
                    return "Ok";
                else return "Error";
            }
            catch (Exception ex)
            { return ex.Message; }
        }
    }
}
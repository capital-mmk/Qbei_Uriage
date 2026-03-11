using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qbei_Uriage_BL;
using Qbei_Uriage_Common;
using System.Data;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace Qbei_Uriage.OutsourcingMaster
{
    public partial class OutsourcingListing : System.Web.UI.Page
    {
        static OutSourcingMaster_Entity outsourcingEntity;
        static OutsourcingMaster_BL balOutsourcing = new OutsourcingMaster_BL();
        DataTable dtOutsourcing;
        static string strUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] != null)
                strUser = (Session["UserInfo"] as string[])[1];

            if (!IsPostBack)
            {
                gvOutsourcing.PageSize = int.Parse(ddlPageSize.SelectedValue);

                BindData();
            }
        }

        private void BindData()
        {
            dtOutsourcing = new DataTable();
            outsourcingEntity = new OutSourcingMaster_Entity();
            if (String.IsNullOrEmpty(txtAcc.Text) && String.IsNullOrEmpty(txtUPrice.Text) && String.IsNullOrEmpty(txtSD.Value) && String.IsNullOrEmpty(txtED.Value))
                dtOutsourcing = balOutsourcing.Outsourcing_SelectAll();
            else
            {
                outsourcingEntity.AccountTitle = txtAcc.Text;
                outsourcingEntity.UnitPrice = txtUPrice.Text;
                outsourcingEntity.Expire_SDate = txtSD.Value;
                outsourcingEntity.Expire_EDate = txtED.Value;
                dtOutsourcing = balOutsourcing.OutsourcingMaster_Select(outsourcingEntity);
            }
            if (dtOutsourcing != null)
            {
                gvOutsourcing.DataSource = dtOutsourcing;
                gvOutsourcing.DataBind();
                lblCnt.Text = dtOutsourcing.Rows.Count.ToString();
            }
        }

        protected void gvOutsourcing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOutsourcing.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvOutsourcing.PageIndex = 0;
            gvOutsourcing.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvOutsourcing.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvOutsourcing.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                txtGoto.Text = string.Empty;
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dtOutsourcing = new DataTable();
            outsourcingEntity = new OutSourcingMaster_Entity();
            outsourcingEntity.AccountTitle = String.IsNullOrEmpty(txtAcc.Text) ? null : txtAcc.Text;
            outsourcingEntity.UnitPrice = String.IsNullOrEmpty(txtUPrice.Text) ? null : txtUPrice.Text;
            outsourcingEntity.Expire_SDate = string.IsNullOrEmpty(txtSD.Value) ? null : txtSD.Value;
            outsourcingEntity.Expire_EDate = string.IsNullOrEmpty(txtED.Value) ? null : txtED.Value;
            dtOutsourcing = balOutsourcing.OutsourcingMaster_Select(outsourcingEntity);
            if (dtOutsourcing != null)
            {
                gvOutsourcing.PageIndex = 0;
                gvOutsourcing.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                gvOutsourcing.DataSource = dtOutsourcing;
                gvOutsourcing.DataBind();
                lblCnt.Text = dtOutsourcing.Rows.Count.ToString();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdId = (HiddenField)row.FindControl("hdOutsourcingId");
            Response.Redirect("~/OutsourcingMaster/OutsourcingEntry.aspx?ID=" + hdId.Value);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OutsourcingMaster/OutsourcingEntry.aspx");
        }

        [WebMethod]
        public static string OutsourcingDelete(string strId)
        {
            try
            {
                outsourcingEntity = new OutSourcingMaster_Entity();
                outsourcingEntity.OutsourcingID = strId;
                outsourcingEntity.ModifiedBy = strUser;
                if (balOutsourcing.OutsourcingMaster_Delete(outsourcingEntity))
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
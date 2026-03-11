using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qbei_Uriage_BL;
using Qbei_Uriage_Common;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace Qbei_Uriage.InsuranceMaster_List
{
    public partial class InsuranceListing : System.Web.UI.Page
    {
        static InsuranceMaster_Entity insuranceEntity;
        static InsuranceMaster_BL balInsurance = new InsuranceMaster_BL();
        DataTable dtInsurance;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvInsurance.PageSize = int.Parse(ddlPageSize.SelectedValue);
                BindData();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdId = (HiddenField)row.FindControl("hdInsuranceId");
            Response.Redirect("~/InsuranceMaster/InsuranceEntry.aspx?ID=" + hdId.Value);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InsuranceMaster/InsuranceEntry.aspx");
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvInsurance.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvInsurance.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                txtGoto.Text = string.Empty;
                BindData();
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvInsurance.PageIndex = 0;
            gvInsurance.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString ());
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dtInsurance = new DataTable();
            insuranceEntity = new InsuranceMaster_Entity();
            insuranceEntity.AccountTitle = String.IsNullOrEmpty(txtAcc.Text) ? null : txtAcc.Text;
            insuranceEntity.Percent = String.IsNullOrEmpty(txtPer.Text) ? null : (Decimal.Parse(txtPer.Text) / 100).ToString();
            insuranceEntity.Expire_SDate = string.IsNullOrEmpty(txtSD.Value) ? null : txtSD.Value;
            insuranceEntity.Expire_EDate = string.IsNullOrEmpty(txtED.Value) ? null : txtED.Value;
            dtInsurance = balInsurance.InsuranceMaster_Select(insuranceEntity);
            if (dtInsurance != null)
            {
                gvInsurance.PageIndex = 0;
                gvInsurance.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                gvInsurance.DataSource = dtInsurance;
                gvInsurance.DataBind();
                lblCnt.Text = dtInsurance.Rows.Count.ToString();
            }
        }

        protected void BindData()
        {
            dtInsurance = new DataTable();
            insuranceEntity = new InsuranceMaster_Entity();
            if (String.IsNullOrEmpty(txtAcc.Text) && String.IsNullOrEmpty(txtPer.Text) && String.IsNullOrEmpty(txtSD.Value) && String.IsNullOrEmpty(txtED.Value))
                dtInsurance = balInsurance.Insurance_SelectAll();
            else
            {
                insuranceEntity = new InsuranceMaster_Entity();
                insuranceEntity.AccountTitle = String.IsNullOrEmpty(txtAcc.Text) ? string.Empty : txtAcc.Text;
                insuranceEntity.Percent = String.IsNullOrEmpty(txtPer.Text) ? string.Empty : txtPer.Text;
                insuranceEntity.Percent = (decimal.Parse(insuranceEntity.Percent) / 100).ToString();
                insuranceEntity.Expire_SDate = string.IsNullOrEmpty(txtSD.Value) ? string.Empty : txtSD.Value;
                insuranceEntity.Expire_EDate = string.IsNullOrEmpty(txtED.Value) ? string.Empty : txtED.Value;
                dtInsurance = balInsurance.InsuranceMaster_Select(insuranceEntity);
            }
            if (dtInsurance != null)
            {
                lblCnt.Text = dtInsurance.Rows.Count.ToString();
                gvInsurance.DataSource = dtInsurance;
                gvInsurance.DataBind();
            }
        }
        protected void gvInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInsurance.PageIndex = e.NewPageIndex;
            BindData();
        }

        [WebMethod]
        public static string InsuranceDelete(string strId)
        {
            try 
            {
                insuranceEntity = new InsuranceMaster_Entity();
                insuranceEntity.InsuranceID = strId;
                if (balInsurance.InsuranceMaster_Delete(insuranceEntity))
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
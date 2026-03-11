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
using System.Xml;

namespace Qbei_Uriage.PackingFareMaster
{
    public partial class PackingFareListing : System.Web.UI.Page
    {
        static PackingFareMaster_BL balPacking = new PackingFareMaster_BL();
        static PackingFareMaster_Entity packEntity;
        DataTable dtPack, dtRegion;
        static string strUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            dtRegion = new DataTable();
            ds.ReadXml(Server.MapPath("~/Region.xml"));
            dtRegion = ds.Tables["Region"];
            if (!IsPostBack)
            {
                gvPackingFare.PageSize = int.Parse(ddlPageSize.SelectedValue);

                if (Session["UserInfo"] != null)
                    strUser = (Session["UserInfo"] as string[])[1];
                BindData();

                ddlRegion.DataSource = ds;
                ddlRegion.DataTextField = "name";
                ddlRegion.DataValueField = "id";
                ddlRegion.DataBind();
            }
        }

        private void BindData()
        {
            packEntity = new PackingFareMaster_Entity();
            dtPack = new DataTable();
            if (String.IsNullOrEmpty(txtPAcc.Text) && String.IsNullOrEmpty(ddlOrderType.SelectedItem.Text)
                && String.IsNullOrEmpty(txtDeliveryCd.Text) && String.IsNullOrEmpty(ddlRegion.SelectedItem.Text)
                && String.IsNullOrEmpty(txtPUnitPrice.Text) && String.IsNullOrEmpty(txtSD.Value) && String.IsNullOrEmpty(txtED.Value))
            {
                dtPack = balPacking.PackingFare_SelectAll();
            }
            else
            {
                packEntity.AccountTitle = String.IsNullOrEmpty(txtPAcc.Text) ? null : txtPAcc.Text;
                packEntity.OrderType = String.IsNullOrEmpty(ddlOrderType.SelectedItem.Text) ? null : ddlOrderType.SelectedValue.ToString();
                packEntity.DeliveryCompanyCode = String.IsNullOrEmpty(txtDeliveryCd.Text) ? null : txtDeliveryCd.Text;
                packEntity.RegionCode = String.IsNullOrEmpty(ddlRegion.SelectedItem.Text) ? null : ddlRegion.SelectedValue.ToString();
                packEntity.UnitPrice = String.IsNullOrEmpty(txtPUnitPrice.Text) ? null : txtPUnitPrice.Text;
                packEntity.Expire_SDate = String.IsNullOrEmpty(txtSD.Value) ? null : txtSD.Value;
                packEntity.Expire_EDate = String.IsNullOrEmpty(txtED.Value) ? null : txtED.Value;
                dtPack = balPacking.PackingFareMaster_Select(packEntity);
            }
            if (dtPack != null)
            {
                dtPack.Columns.Add("Region");
                foreach (DataRow dr in dtPack.Rows)
                    dr["Region"] = dtRegion.AsEnumerable().Single(x => x.Field<string>("id").Equals(dr["RegionCode"].ToString())).Field<string>("name");
                gvPackingFare.DataSource = dtPack;
                gvPackingFare.DataBind();
                lblCnt.Text = dtPack.Rows.Count.ToString();
            }
        }

        protected void gvPackingFare_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPackingFare.PageIndex = e.NewPageIndex;
            gvPackingFare.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor data = sender as HtmlAnchor;
            GridViewRow row = data.NamingContainer as GridViewRow;
            HiddenField hdnId = (HiddenField)row.FindControl("hdnPackingId");
            Response.Redirect("~/PackingFareMaster/PackingFareEntry.aspx?ID=" + hdnId.Value);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PackingFareMaster/PackingFareEntry.aspx");
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGoto.Text))
            {
                gvPackingFare.PageIndex = int.Parse(txtGoto.Text) - 1;
                gvPackingFare.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
                txtGoto.Text = string.Empty;
                BindData();
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPackingFare.PageIndex = 0;
            gvPackingFare.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvPackingFare.PageIndex = 0;
            gvPackingFare.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindData();
        }

        [WebMethod]
        public static string PackingFareDelete(string strId)
        {
            try
            {
                packEntity = new PackingFareMaster_Entity();
                packEntity.ID = strId;
                packEntity.ModifiedBy = strUser;
                if (balPacking.PackingFareMaster_Delete(packEntity))
                    return "Ok";
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
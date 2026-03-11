using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Qbei_Uriage_Common;
using Qbei_Uriage_BL;
using System.Xml;

namespace Qbei_Uriage.PackingFareMaster
{
    public partial class PackingFareEntry : System.Web.UI.Page
    {
        static PackingFareMaster_BL balPack = new PackingFareMaster_BL();
        static PackingFareMaster_Entity packEntity;
        DataTable dtPack;
        static string strUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (!IsPostBack)
            {
                if (Session["UserInfo"] != null)
                    strUser = (Session["UserInfo"] as string[])[1];
                ds.ReadXml(Server.MapPath("~/Region.xml"));
                ddlRegion.DataSource = ds;
                ddlRegion.DataTextField = "name";
                ddlRegion.DataValueField = "id";
                ddlRegion.DataBind();               
                if (String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hdPackingFareID.Value = "0";
                }
                else 
                {
                    hdPackingFareID.Value = Request.QueryString["ID"];
                    btnSave.Text = "更新";
                    BindData();
                    txtPAcc.ReadOnly = true;
                }
            }
        }

        private void BindData()
        {
            bool bolType;
            DateTime? dtED;
            packEntity = new PackingFareMaster_Entity();
            packEntity.ID = hdPackingFareID.Value;
            dtPack = balPack.PackingFareMaster_Select(packEntity);
            if (dtPack != null)
            {
                txtPAcc.Text = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<string>("AccountTitle");
                bolType = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<bool>("OrderType");
                ddlOrderType.SelectedValue = bolType.ToString();
                txtDeliveryCd.Text = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<int>("DeliveryCompanyCode").ToString();
                ddlRegion.SelectedValue = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<int>("RegionCode").ToString();
                txtPPrice.Text = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<int>("UnitPrice").ToString("#,###");
                txtSD.Value = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<DateTime>("Expire_SDate").ToString("yyyy/MM/dd");
                dtED = dtPack.AsEnumerable().Single(x => x.Field<int>("ID") == int.Parse(hdPackingFareID.Value)).Field<DateTime?>("Expire_EDate");
                txtED.Value = (dtED == null) ? string.Empty : dtED.Value.ToString("yyyy/MM/dd");
            }
        }

        [WebMethod]
        public static string PackingFareSave(List<string> lstParam)
        {
            try 
            {
                string strMsg = string.Empty;
                packEntity = new PackingFareMaster_Entity();
                packEntity.AccountTitle = lstParam[1];
                packEntity.OrderType = lstParam[2];
                packEntity.DeliveryCompanyCode = lstParam[3];
                packEntity.RegionCode = lstParam[4];
                packEntity.UnitPrice = int.Parse (lstParam[5],System.Globalization.NumberStyles.AllowThousands).ToString();
                packEntity.Expire_SDate = lstParam[6];
                if (!String.IsNullOrEmpty(lstParam[7]))
                    packEntity.Expire_EDate = lstParam[7];
                if (lstParam[0].Equals("0"))
                {
                    packEntity.CreatedBy = strUser;
                    if (balPack.PackingFareMaster_Insert(packEntity))
                        strMsg = "登録完了しました";
                }
                else {
                    packEntity.ModifiedBy = strUser;
                    packEntity.ID = lstParam[0];
                    if (balPack.PackingFareMaster_Update(packEntity))
                        strMsg = "更新完了しました";
                }
                return strMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
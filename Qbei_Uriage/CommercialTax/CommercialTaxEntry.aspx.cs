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

namespace Qbei_Uriage.CommercialTax
{
    public partial class CommercialTaxEntry : System.Web.UI.Page
    {
        static CommercialTax_BL balCom = new CommercialTax_BL();
        static CommercialTax_Entity comEntity;
        DataTable dtCom;
        static string strUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] != null)
                    strUser = (Session["UserInfo"] as string[])[1];
                if (String.IsNullOrEmpty(Request.QueryString["ID"]))
                    hdCommercialTaxID.Value = "0";
                else {
                    hdCommercialTaxID.Value = Request.QueryString["ID"];
                    btnSave.Text = "更新";
                    txtCAcc.ReadOnly = true;
                    BindData();
                }
            }
        }

        private void BindData() 
        {
            decimal dPercent;
            int intPrice;
            DateTime? dtED;
            comEntity = new CommercialTax_Entity();
            comEntity.CommercialTaxID = hdCommercialTaxID.Value;
            dtCom = balCom.CommercialTaxMaster_Select(comEntity);
            if (dtCom != null)
            {
                txtCAcc.Text = dtCom.AsEnumerable().Single(x => x.Field<int>("CommercialTaxID") == int.Parse(hdCommercialTaxID.Value)).Field<string>("AccountTitle");
                txtPayment.Text = dtCom.AsEnumerable().Single(x => x.Field<int>("CommercialTaxID") == int.Parse(hdCommercialTaxID.Value)).Field<int>("ShopSectionCode").ToString();
                intPrice = dtCom.AsEnumerable().Single(x => x.Field<int>("CommercialTaxID") == int.Parse(hdCommercialTaxID.Value)).Field<int>("UnitPrice");
                txtCPrice.Text = intPrice.ToString("#,###");
                dPercent = dtCom.AsEnumerable().Single(x => x.Field<int>("CommercialTaxID") == int.Parse(hdCommercialTaxID.Value)).Field<decimal>("Percent");
                txtCPercent.Text = (dPercent * 100).ToString("###");
                txtSD.Value = dtCom.AsEnumerable().Single(x => x.Field<int>("CommercialTaxID") == int.Parse(hdCommercialTaxID.Value)).Field<DateTime>("Expire_SDate").ToString("yyyy/MM/dd");
                dtED = dtCom.AsEnumerable().Single(x => x.Field<int>("CommercialTaxID") == int.Parse(hdCommercialTaxID.Value)).Field<DateTime?>("Expire_EDate");
                txtED.Value = (dtED == null) ? string.Empty : dtED.Value.ToString("yyyy/MM/dd");
                txtCPrice.ReadOnly = (intPrice == 0) ? true :false ;
                txtCPercent .ReadOnly = (dPercent == 0)? true : false;
            }        
        }

        [WebMethod]
        public static string CommercialTaxSave(List<string> lstParam)
        {
            try {
                string strMsg = string.Empty;
                comEntity = new CommercialTax_Entity();
                comEntity.AccountTitle = lstParam[1];
                comEntity.ShopSectionCode = lstParam[2];
                comEntity.UnitPrice = String.IsNullOrEmpty( lstParam[3]) ? "0" : int.Parse( lstParam[3],System.Globalization.NumberStyles.AllowThousands).ToString();
                comEntity.Percent = String.IsNullOrEmpty(lstParam[4])? "0" : lstParam[4];
                comEntity.Expire_SDate = lstParam[5];
                if (!String.IsNullOrEmpty(lstParam[6]))
                    comEntity.Expire_EDate = lstParam[6];
                if (lstParam[0].Equals("0"))
                {
                    comEntity.CreatedBy = strUser;
                    if (balCom.CommercialTax_Insert(comEntity))
                        strMsg = "登録完了しました";
                }
                else {
                    comEntity.CommercialTaxID = lstParam[0];
                    comEntity.ModifiedBy = strUser;
                    if (balCom.CommercialTax_Update(comEntity))
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
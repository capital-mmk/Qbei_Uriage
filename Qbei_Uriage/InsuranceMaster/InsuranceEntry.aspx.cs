using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Qbei_Uriage_Common;
using Qbei_Uriage_BL;
using System.Data;

namespace Qbei_Uriage.InsuranceMaster
{
    public partial class InsuranceEntry : System.Web.UI.Page
    {
        static InsuranceMaster_BL balInsurance = new InsuranceMaster_BL();
        static InsuranceMaster_Entity insuEntity;
        DataTable dtInsurance;
        static string strUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] != null)
                {
                    strUser = (Session["UserInfo"] as string[])[1];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    btnSave.Text = "更新";
                    hdInsuranceId.Value = Request.QueryString["ID"];
                    BindData();
                }
                else {
                    hdInsuranceId.Value = "0";
                }
            }
        }

        private void BindData()
        {
            decimal dPercent;
            DateTime? dtED;
            insuEntity = new InsuranceMaster_Entity();
            insuEntity.InsuranceID = hdInsuranceId.Value;
            dtInsurance = balInsurance.InsuranceMaster_Select(insuEntity);
            if (dtInsurance != null)
            {
                txtIAcc.Text = dtInsurance.AsEnumerable().Single(x => x.Field<int>("InsuranceID") == int.Parse(hdInsuranceId.Value)).Field<string>("AccountTitle");
                dPercent = dtInsurance.AsEnumerable().Single(x => x.Field<int>("InsuranceID") == int.Parse(hdInsuranceId.Value)).Field<decimal>("Percent");
                txtPercent.Text = (dPercent * 100).ToString("###");
                txtSD.Value = dtInsurance.AsEnumerable().Single(x => x.Field<int>("InsuranceID") == int.Parse(hdInsuranceId.Value)).Field<DateTime>("Expire_SDate").ToString("yyyy/MM/dd");
                dtED = dtInsurance.AsEnumerable().Single(x => x.Field<int>("InsuranceID") == int.Parse(hdInsuranceId.Value)).Field<DateTime?>("Expire_EDate");
                txtED.Value = dtED == null ? string.Empty : dtED.Value.ToString ("yyyy/MM/dd");
                txtIAcc.ReadOnly = true;
            }
        }

        [WebMethod]
        public static string InsuranceSave(int intId,string strAcc,string strPercent,string strStart,string strEnd )
        {
            try {
                string strMsg = string.Empty ;
                insuEntity = new InsuranceMaster_Entity();
                insuEntity.AccountTitle = strAcc;
                insuEntity.Percent = (decimal.Parse(strPercent) / 100).ToString();
                insuEntity.Expire_SDate = strStart;
                if (!String.IsNullOrEmpty(strEnd))
                    insuEntity.Expire_EDate = strEnd;
                if (intId == 0)
                {
                    insuEntity.CreatedBy = strUser;
                    if (balInsurance.InsuranceMaster_Insert(insuEntity))
                        strMsg = "登録完了しました";
                }
                else {
                    insuEntity.InsuranceID = intId.ToString();
                    insuEntity.ModifiedBy = strUser;
                    if (balInsurance.InsuranceMaster_Update(insuEntity))
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
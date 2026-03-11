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

namespace Qbei_Uriage.OutsourcingMaster
{
    public partial class OutsourcingEntry : System.Web.UI.Page
    {
        static OutsourcingMaster_BL balOutsourcing = new OutsourcingMaster_BL();
        static OutSourcingMaster_Entity outsourcingEntity;
        DataTable dtOutsourcing;
        static string strUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] != null)
                {
                    strUser = (Session["UserInfo"] as string[])[1];
                }
                if (String.IsNullOrEmpty(Request.QueryString["ID"]))
                    hdOutsourcingId.Value = "0";
                else {
                    btnSave.Text = "更新";
                    hdOutsourcingId.Value = Request.QueryString["ID"];
                    BindData();
                    txtOAcc.ReadOnly = true;
                }
            }
        }

        private void BindData() 
        {
            DateTime? dtED;
            outsourcingEntity = new OutSourcingMaster_Entity();
            outsourcingEntity.OutsourcingID = hdOutsourcingId.Value;
            dtOutsourcing = balOutsourcing.OutsourcingMaster_Select(outsourcingEntity);
            if (dtOutsourcing != null)
            {
                txtOAcc.Text = dtOutsourcing.AsEnumerable().Single(x => x.Field<int>("OutsourcingID") == int.Parse(hdOutsourcingId.Value)).Field<string>("AccountTitle");
                txtOPrice.Text = dtOutsourcing.AsEnumerable().Single(x => x.Field<int>("OutsourcingID") == int.Parse(hdOutsourcingId.Value)).Field<int>("UnitPrice").ToString("#,###");
                txtSD.Value = dtOutsourcing.AsEnumerable().Single(x => x.Field<int>("OutsourcingID") == int.Parse(hdOutsourcingId.Value)).Field<DateTime>("Expire_SDate").ToString("yyyy/MM/dd");
                dtED = dtOutsourcing.AsEnumerable().Single(x => x.Field<int>("OutsourcingID") == int.Parse(hdOutsourcingId.Value)).Field<DateTime?>("Expire_EDate");
                txtED.Value = dtED == null ? string.Empty : dtED.Value.ToString("yyyy/MM/dd");
            }
        }

        [WebMethod]
        public static string OutsourcingSave(int intId, string strAcc, string strPrice, string strSD, string strED)
        {
            try {
                string strMsg = string.Empty;
                outsourcingEntity = new OutSourcingMaster_Entity();
                outsourcingEntity.AccountTitle = strAcc;
                outsourcingEntity.UnitPrice = int.Parse(strPrice,System.Globalization.NumberStyles.AllowThousands).ToString();
                outsourcingEntity.Expire_SDate = strSD;
                if (!String.IsNullOrEmpty(strED))
                    outsourcingEntity.Expire_EDate = strED;
                if (intId == 0)
                {
                    outsourcingEntity.CreatedBy = strUser;
                    if (balOutsourcing.OutsourcingMaster_Insert(outsourcingEntity))
                        strMsg = "登録完了しました";
                }
                else
                {
                    outsourcingEntity.OutsourcingID = intId.ToString();
                    outsourcingEntity.ModifiedBy = strUser;
                    if (balOutsourcing.OutsourcingMaster_Update(outsourcingEntity))
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
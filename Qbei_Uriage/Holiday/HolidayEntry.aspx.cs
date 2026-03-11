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

namespace Qbei_Uriage.Holiday
{
    public partial class HolidayEntry : System.Web.UI.Page
    {
        static Holiday_BL balHoliday = new Holiday_BL();
        static Holiday_Entity holEntity;
        DataTable dtHoliday;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hdHolidayId.Value = Request.QueryString["ID"];
                    btnSave.Text = "更新";
                    BindData();
                }
                else {
                    hdHolidayId.Value = "0";
                }
            }
        }

        private void BindData()
        {
            holEntity = new Holiday_Entity();
            holEntity.HolidayID = hdHolidayId.Value;
            dtHoliday = balHoliday.Holiday_Select(holEntity);
            if (dtHoliday != null)
            {
                txtHolidayDT.Value = dtHoliday.AsEnumerable().Single(x => x.Field<int>("HolidayID") == int.Parse(holEntity.HolidayID)).Field<DateTime>("HolidayDate").ToString("yyyy/MM/dd");
                txtHolidayName.Text = dtHoliday.AsEnumerable().Single(x => x.Field<int>("HolidayID") == int.Parse(holEntity.HolidayID)).Field<string>("HolidayName");
            }
        }

        [WebMethod]
        public static string HolidaySave(int intId, string strDate, string strNm)
        {
            try {
                string strMsg = string.Empty;
                holEntity = new Holiday_Entity();
                holEntity.HolidayDate = strDate;
                holEntity.HolidayName = strNm;

                if (intId == 0)
                {
                    if (balHoliday.Holiday_Insert(holEntity))
                        strMsg = "登録完了しました";
                }
                else
                {
                    holEntity.HolidayID = intId.ToString();
                    if(balHoliday.Holiday_Update(holEntity))
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
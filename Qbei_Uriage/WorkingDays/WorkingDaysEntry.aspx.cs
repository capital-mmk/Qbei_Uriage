using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qbei_Uriage_Common;
using Qbei_Uriage_BL;
using System.Web.Services;
using System.Data;

namespace Qbei_Uriage.WorkingDays
{
    public partial class WorkingDaysEntry : System.Web.UI.Page
    {
        static WorkingDays_BL balWD = new WorkingDays_BL();
        static WorkingDays_Entity workingEntity;
        DataTable dtWorking;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hdWorkingDaysID.Value = "0";
                }
                else
                {
                    hdWorkingDaysID.Value = Request.QueryString["ID"];
                    btnSave.Text = "更新";
                    BindData();
                }
            }
        }
        private void BindData()
        {
            workingEntity = new WorkingDays_Entity();
            workingEntity.WorkingDaysID = hdWorkingDaysID.Value;
            dtWorking = balWD.WorkingDays_Select(workingEntity);
            if (dtWorking != null)
            {
                string holidayYM = dtWorking.AsEnumerable().Single(x => x.Field<int>("WorkingDays_ID") == int.Parse(hdWorkingDaysID.Value)).Field<string>("YearMonth");
                txtYM.Value = holidayYM.Insert(4, "/");
                ddlType.SelectedValue = dtWorking.AsEnumerable().Single(x => x.Field<int>("WorkingDays_ID") == int.Parse(hdWorkingDaysID.Value)).Field<string>("ShopName");
                txtDays.Text = dtWorking.AsEnumerable().Single(x => x.Field<int>("WorkingDays_ID") == int.Parse(hdWorkingDaysID.Value)).Field<int>("WorkingDays").ToString();
            }
        }

        [WebMethod]
        public static string WorkingDaysSave(List<string> lstParam)
        {
            string strMsg = string.Empty;
            try
            {
                workingEntity = new WorkingDays_Entity();
                workingEntity.YearMonth = lstParam[1].Replace("/", string.Empty);
                workingEntity.ShopName = lstParam[2];
                workingEntity.WorkingDays = lstParam[3];
                if (lstParam[0].Equals("0"))
                {
                    if (balWD.WorkingDays_Insert(workingEntity))
                        strMsg = "登録完了しました";
                }
                else
                {
                    workingEntity.WorkingDaysID = lstParam[0];
                    if (balWD.WorkingDays_Update(workingEntity))
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
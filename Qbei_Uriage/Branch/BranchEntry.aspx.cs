using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4.Qbei_Uriage_Common;
using _2.Qbei_Uriage_BL;
using System.Data;
using System.Web.Services;

namespace Qbei_Uriage.Branch
{

    public partial class BranchEntry : System.Web.UI.Page
    {
        static Branch_Entity be = new Branch_Entity();
        static Branch_BL bbl = new Branch_BL();
        static bool isNew;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(Request.QueryString["ID"]))
                    isNew = true;
                else
                {
                    txtBranchCode.ReadOnly = true;
                    txtBranchCode.Text = Request.QueryString["ID"];
                    isNew = false;
                    btnSave.Text = "更新";
                    Edit();
                }
            }
        }

        protected void Edit()
        {
            be = new Branch_Entity();
            be.BranchCode = txtBranchCode.Text;
            DataTable dt = bbl.Branch_Edit(be);
            if (dt != null)
            {
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtBrandShortName.Text = dt.Rows[0]["BrandShortName"].ToString();
                txtStoreCategory.Text = dt.Rows[0]["StoreCategory"].ToString();
                object summary = dt.Rows[0]["Summary"];
                txtSummary.Text = (summary == null) ? string.Empty : dt.Rows[0]["Summary"].ToString();
            }
        }

        [WebMethod]
        public static string BranchSave(List<string> lstParam)
        {
            try
            {
                string strMsg = string.Empty;
                be = new Branch_Entity();
                be.BranchCode = lstParam[0];
                be.BranchName = lstParam[1];
                be.BrandShortName = lstParam[2];
                be.SotreCategory = lstParam[3];
                be.Summary = lstParam[4];

                if (isNew)
                {
                    if (bbl.IsDuplicateBranch(be.BranchCode))
                        return "0";
                    else if (bbl.Branch_Save(be))
                        strMsg = "登録完了しました";
                }
                else
                {
                    if (bbl.Branch_Update(be))
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
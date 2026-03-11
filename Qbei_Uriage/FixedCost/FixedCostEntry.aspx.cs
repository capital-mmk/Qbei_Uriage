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

namespace Qbei_Uriage.FixedCost
{
    public partial class FixedCostEntry : System.Web.UI.Page
    {
        static FixedCost_BL balFC = new FixedCost_BL();
        static FixedCost_Entity fixedEntity;
        DataTable dtFixed;
        DataTable dtShop;
        string strMode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDown();
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hdFixedId.Value = Request.QueryString["ID"].ToString();
                    strMode = "Edit";
                    BindData();
                    btnSave.Text = "更新";
                }
                else { 
                    strMode = "New";
                    hdFixedId.Value = "0";
                }
            }
        }

        private void BindData()
        {
            try
            {
                dtFixed = new DataTable();
                if (!String.IsNullOrEmpty(hdFixedId.Value))
                {
                    fixedEntity = new FixedCost_Entity();
                    fixedEntity.FixedCostID = hdFixedId.Value;
                    dtFixed = balFC.FixedCost_Select(fixedEntity);
                    if (dtFixed != null)
                    {
                        txtFAcc.Text = dtFixed.AsEnumerable().Single(x => x.Field<int>("FixedCostID") == int.Parse(hdFixedId.Value)).Field<string>("AccountTitle");
                        ddlShopCategory.SelectedValue = dtFixed.AsEnumerable().Single(x => x.Field<int>("FixedCostID") == int.Parse(hdFixedId.Value)).Field<string>("ShopCategory");                        
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void BindDropDown()
        {
            try
            {
                dtShop = balFC.ShopCategory_Select();
                if (dtShop != null)
                {
                    ddlShopCategory.DataValueField = "Shop";
                    ddlShopCategory.DataMember = "ID";
                    ddlShopCategory.DataSource = dtShop;
                    ddlShopCategory.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        [WebMethod]
        public static string FixedCostSave(int intId, string strName, string strShop)
        {
            try {
                string strMsg = string.Empty;
                fixedEntity = new FixedCost_Entity();
                fixedEntity.AccountTitle = strName;
                fixedEntity.ShopCategory = strShop;
                if (intId == 0)
                {
                    if (balFC.FixedCost_Insert(fixedEntity))
                        strMsg = "登録完了しました";
                }
                else {
                    fixedEntity.FixedCostID = intId.ToString();
                    if(balFC.FixedCost_Update(fixedEntity))
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
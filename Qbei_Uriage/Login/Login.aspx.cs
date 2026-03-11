using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using _4.Qbei_Uriage_Common;
using _2.Qbei_Uriage_BL;
using System.Data;

namespace Qbei_Uriage.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            QbeiUser_Entity que = new QbeiUser_Entity();
            QbeiUser_BL qubl = new QbeiUser_BL();
            que.UserName = txtUserName.Value;
            que.Password = txtPassword.Value;

            if (!String.IsNullOrWhiteSpace(que.UserName) && !String.IsNullOrWhiteSpace(que.Password))
            {
                DataTable dt = new DataTable();
                dt = qubl.checkLogin(que);
                if (dt.Rows.Count > 0)
                {
                    string[] UserInfo = new string[3];
                    UserInfo[0] = dt.Rows[0]["Qbei_user_id"].ToString();
                    UserInfo[1] = dt.Rows[0]["UserName"].ToString();
                    UserInfo[2] = dt.Rows[0]["Password"].ToString();


                    Session["UserInfo"] = UserInfo;

                    Response.Redirect("~/ItemMaster/IMtest.aspx");
                    lblErrorMsg.Visible = false;

                }
                else
                {

                    lblErrorMsg.Visible = true;
                }
            }
            else
            {

                lblErrorMsg.Visible = true;
            }
        }

      
    }
}
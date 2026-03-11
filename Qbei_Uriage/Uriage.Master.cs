using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Qbei_Uriage
{
    public partial class Uriage : System.Web.UI.MasterPage
    {
      public  string[] a;
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        }
        protected void Initialize()
        {
            try
            {
                a = Session["UserInfo"] as string[];
                hdn_info.Value = a[0] + "," + a[1] + "," + a[2];
                lbl_name.Text = a[1];
            }
            catch
            {
                Session.Abandon();
                Response.Redirect("~/login/login.aspx");
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/login/login.aspx");
        }
        protected void myprofile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserList/User_Entry.aspx?UserID=" + a[0].ToString());
        }
    }
}
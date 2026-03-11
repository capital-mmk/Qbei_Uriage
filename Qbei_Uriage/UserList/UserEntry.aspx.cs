using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _2.Qbei_Uriage_BL;
using _4.Qbei_Uriage_Common;
using System.Data;

namespace Qbei_Uriage.UserList
{
    public partial class UserEntry : System.Web.UI.Page
    {
        QbeiUser_Entity que = new QbeiUser_Entity();
        QbeiUser_BL qubl = new QbeiUser_BL();
        public string url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UserID"] != null)
                {
                    Edit();
                    url = Request.Url.AbsoluteUri;
                    Session["url"] = url;
                }
                else
                {
                    new_hide.Visible = false;
                    con_hide.Visible = false;
                    change_name.InnerText = "Password";
                    txtpassword.Attributes["placeholder"] = "パスワードを入力してください";
                }
                //else {
                //    if (Request.QueryString["ID"] != null)
                //    {
                //        Chage_Myp();
                //    }
                //}
                txtpassword.Attributes["type"] = "password";
                //placeholder = "Enter your Old Password"

            }
            else
            {
                txtpassword.Attributes["type"] = "password";
            }

        }
        protected void Chage_Myp()
        {
            Edit();
        }
        protected void Edit()
        {
            btnsave.Value = "Update";
            //txtpasswor = true;
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            que.UserID = Request.QueryString["UserID"];
            DataTable dt = qubl.Qbei_UserEdit(que);
            txtUserName.Value = dt.Rows[0]["UserName"].ToString();
            txtModifiedDate.Value = dt.Rows[0]["Modified_Date"].ToString();


            txtpassword.Value = dt.Rows[0]["Password"].ToString();
            txtpassword.Attributes["type"] = "password";
            try
            {
                txtpassword.Attributes["type"] = "secret";
            }
            catch
            {
                //txtpassword.Attributes["type"] = "sec";
            }
            txtpassword.Attributes.Add("value", dt.Rows[0]["Password"].ToString());


            txtUserName.Disabled = true;
            txtpassword.Disabled = true;
            //f = this.form1.FindControl("txtpassword") as TextBox;
            //f.Text= dt.Rows[0]["Password"].ToString(); 
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            if (btnsave.Value == "Save")
            {
                Save();
            }
            else
            {
                Update();
            }
        }
        protected void Save()
        {
            que.UserName = txtUserName.Value;
            que.Password = txtpassword.Value;
            if (String.IsNullOrWhiteSpace(txtModifiedDate.Value))
            {
                txtModifiedDate.Value = DateTime.Now.ToShortDateString();
            }
            que.ModifiedDate = txtModifiedDate.Value;
            //check Already Exist
            //QbeiUser_Entity que1 = new QbeiUser_Entity();
            //QbeiUser_BL qubl1 = new QbeiUser_BL();
            //que1.UserName = txtUserName.Value;
            //que1.Password = txtPassword.Value;

            if (!String.IsNullOrWhiteSpace(txtUserName.Value) && !String.IsNullOrWhiteSpace(txtpassword.Value))
            {
                DataTable dt = new DataTable();
                dt = qubl.checkExist(que);

                if (dt.Rows.Count == 0)
                {


                    if (qubl.User_Save(que))
                    {
                        Response.Write("<script>alert('登録完了しました');</script>");
                        clear();
                    }
                    else
                    {
                        Response.Write("<script>alert('Fail to Save!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('User you saved Already Existed');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Fill username and password');</script>");
            }
        }
        protected void Update()
        {
            que.UserID = Request.QueryString["UserID"];
            que.UserName = txtUserName.Value;
            que.Password = txtpassword.Value;
            que.ModifiedDate = txtModifiedDate.Value;

            if (!String.IsNullOrEmpty(new_pass.Value) && !String.IsNullOrEmpty(comfirm_pass.Value))
            {
                if (new_pass.Value == comfirm_pass.Value)
                {

                    que.Password = new_pass.Value;
                    if (qubl.User_Update(que))
                    {
                        Response.Write("<script>alert('更新完了しました');</script>");
                        string url_id = Request.Url.AbsoluteUri;
                        DataTable dt = new DataTable();
                        que.UserID = Request.QueryString["UserID"];
                        //que.UserName = "";
                        que.Password = "";
                        que.ModifiedDate = "";
                        dt = qubl.checkExist(que);
                        txtpassword.Value = dt.Rows[0]["Password"].ToString(); txtpassword.Attributes["type"] = "password";
                        //Response.Redirect("~/UserList/UserList.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Update Failed');</script>");
                        string url_id = Request.Url.AbsoluteUri;
                        DataTable dt = new DataTable();
                        que.UserID = Request.QueryString["UserID"];
                        //que.UserName = "";
                        que.Password = "";
                        que.ModifiedDate = "";
                        dt = qubl.checkExist(que);
                        txtpassword.Value = dt.Rows[0]["Password"].ToString();
                        txtpassword.Attributes["type"] = "password";
                        //Response.Redirect(Session["url"] as string);
                        //  clear_pass();
                        //   txtpassword.Value = "";
                    }
                }
                else
                {
                    //string url_id = Request.Url.AbsoluteUri;
                    Response.Write("<script>alert('Password you entered does not matched');</script>");
                    //Response.Redirect("");
                    //clear_pass();
                    //Response.Redirect(Session["url"] as string);
                    string url_id = Request.Url.AbsoluteUri;
                    DataTable dt = new DataTable();
                    que.UserID = Request.QueryString["UserID"];
                    //que.UserName = "";
                    que.Password = "";
                    que.ModifiedDate = "";
                    dt = qubl.checkExist(que);
                    txtpassword.Value = dt.Rows[0]["Password"].ToString(); txtpassword.Attributes["type"] = "password";
                }
            }
            else
            {
                //string url_id = Request.Url.AbsoluteUri;
                Response.Write("<script>alert('パスワードを入力してください');</script>");
                //Response.Redirect(Session["url"] as string);
                string url_id = Request.Url.AbsoluteUri;
                DataTable dt = new DataTable();
                que.UserID = Request.QueryString["UserID"];
                //que.UserName = "";
                que.Password = "";
                que.ModifiedDate = "";
                dt = qubl.checkExist(que);
                txtpassword.Value = dt.Rows[0]["Password"].ToString(); txtpassword.Attributes["type"] = "password";
            }
        }

        public void clear_pass()
        {
            new_pass.Value = "";
            comfirm_pass.Value = "";
        }

        public void clear()
        {
            txtUserName.Value = "";
            txtpassword.Value = "";
            txtModifiedDate.Value = "";

        }
    }
}

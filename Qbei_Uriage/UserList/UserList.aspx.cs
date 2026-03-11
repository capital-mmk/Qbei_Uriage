using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using _2.Qbei_Uriage_BL;
using _4.Qbei_Uriage_Common;
using System.Web.UI.HtmlControls;

namespace Qbei_Uriage.UserList
{
    public partial class UserList : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        QbeiUser_BL qbl = new QbeiUser_BL();
        QbeiUser_Entity qe = new QbeiUser_Entity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                dt = qbl.UserList_SelectAll();
                gvUserList.DataSource = dt;
                gvUserList.DataBind();
                lblrowCount.Text = dt.Rows.Count.ToString();
                //}
                disable_remove();
                hide_panel.Style.Add("display", "none");
            }
        }
    protected void disable_remove()
    {
            if (this.gvUserList.Rows.Count == 1)
            {
                //btn
                foreach (GridViewRow row in gvUserList.Rows)
                {
                    //Label anc = sender as Label;
                    // here you'll get all rows with RowType=DataRow
                    // others like Header are omitted in a foreach

                    Label l = row.FindControl("hide_now") as Label;
                    l.Enabled=false;

                }
                foreach (GridViewRow row in gvUserList.Rows)
                {
                    for (int i = 0; i < gvUserList.Columns.Count; i++)
                    {
                        //String header = gvUserList.Columns[i].HeaderText;
                        if (i == 4 || i ==5)
                        {
                            row.Cells[i].Enabled = false;
                        }
                       
                    }
                }
                //Label anc = sender as Label;
                //GridViewRow Grow = (GridViewRow)anc.NamingContainer;
                //string UserID = ((Label)Grow.FindControl("lblUserID")).Text;
            }
    }




        public void btnSearch_Click(object sender, EventArgs e)
        {
            Getdata(); 
        }
       
        private void Getdata()
        {
            dt = new DataTable();
            qe.UserID = txtUserID.Text;
            qe.Password = txtPassword.Text;
            qe.UserName = txtUserName.Text;
            qe.ModifiedDate = txtModifiedDate.Value;

            dt = qbl.UserList_Search(qe);

            if (dt.Rows.Count > 0)
            {
                gvUserList.DataSource = dt;
                gvUserList.DataBind();
            }
            else
            {
                gvUserList.DataSource = dt;
                gvUserList.DataBind();

            }
            lblrowCount.Text = dt.Rows.Count.ToString() ;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HtmlAnchor anc = sender as HtmlAnchor;
            GridViewRow Grow = (GridViewRow)anc.NamingContainer;
            string UserID = ((Label)Grow.FindControl("lblUserID")).Text;
            Response.Redirect("UserEntry.aspx?UserID=" + UserID);
        }

        protected void gvItemMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserList.PageIndex = e.NewPageIndex;
            Getdata();

        }
        protected void gvItemMaster_Indexchanged(object sender, EventArgs e)
        {
            gvUserList.PageIndex = 0;
            gvUserList.PageSize = Convert.ToInt32(ddlPageSize.Text);
            Getdata();
        }


        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGoto.Text))
            {
                gvUserList.PageIndex = Convert.ToInt32(txtGoto.Text) - 1;
                // gvItemMaster.PageSize = Convert.ToInt32(ddlPageSize.Text);
                txtGoto.Text = string.Empty;
                Getdata();
            }
        }
        protected void auser_Click(Object sender, EventArgs e)
        {
            Response.Redirect("~/UserList/UserEntry.aspx");
        }
        protected void btnDelete_Click(Object sender, EventArgs e)
        {
            HtmlAnchor btn = sender as HtmlAnchor;
            GridViewRow gr = btn.NamingContainer as GridViewRow;
            Label lbl = gr.FindControl("lblUserID") as Label;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal()", true);
            //if ()

            //lbl.Value = lbl.Text;
        }
        protected void btnDeleteCofirm_ServerClick(object sender, EventArgs e)
        {
            if (this.gvUserList.Rows.Count != 1)

            {
                Delete();
            }
        }
        protected void gvSLList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblID = e.Row.FindControl("lblUserID") as Label;



                //  Label lblname = e.Row.FindControl("lblName") as Label;

                HtmlAnchor btn = e.Row.FindControl("btnDelete") as HtmlAnchor;

                btn.Attributes.Add("onclick", "return ShowModal1(" + lblID.Text + ")");
            }
        }
        private void Delete()
        {
            QbeiUser_Entity suEN;
            QbeiUser_BL suBL;
            suEN = new QbeiUser_Entity();
            suEN.UserID = lblID.Value;
            suBL = new QbeiUser_BL();

            if (suBL.Delete_User(suEN))
            {
                string message = "Delete Successfully!";
                ShowAlert("success", "Deleted", message);

                //gvStaffUniformList.DataBind();
                //gridViewBind();
            }
            else
            {
                string message = "Delete Failed!";
                ShowAlert("danger", "Error!", message);
            }

        }
        private void ShowAlert(string HeaderClass, string Header, String Message)
        {
            divModalHeader.Attributes.Remove("class");
            divModalHeader.Attributes.Add("class", "modal-header modal-header-" + HeaderClass);
            lblModalHeader.Text = Header;
            lblModalMessage.Text = Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal()", true);
        }
        protected void refresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
    }
}
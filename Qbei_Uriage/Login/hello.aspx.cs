using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Qbei_Uriage.Login
{
    public partial class hello : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //< add name = "Qbei_Uriage" connectionString = "Data Source=WIN-SLTEP5OTBR0\SQLEXPRESS;Initial Catalog=Qbei_Uriage;User ID=Qbei;Password=dW7MmEtnL" providerName = "System.Data.SqlClient" />

                 //<add name="SalePriceChangeDB" connectionString="Data Source=devserver\sqlexpress;Initial Catalog=SalesPriceChange;Persist Security Info=True;User ID=sa;Password=12345" providerName="System.Data.SqlClient" />
                 string ConStr = @"Data Source=WIN-SLTEP5OTBR0\SQLEXPRESS;Initial Catalog=Qbei_Uriage;Persist Security Info=True;User ID=Qbei;Password=dW7MmEtnL";
            if (!IsPostBack)
            {
                string Query = "select top 100 ItemMaster.Sales_Date,ItemMaster.Cancel_Date,ItemMaster.OrderNo from ItemMaster";
                SqlConnection con = new SqlConnection(ConStr);
                SqlDataAdapter adp = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                gvDistricts.DataSource = dt.DefaultView;
                gvDistricts.DataBind();
            }

        }
        protected void gvDistricts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }
    }
}
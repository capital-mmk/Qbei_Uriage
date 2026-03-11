<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Uriage.Master" CodeBehind="Item.aspx.cs" Inherits="Qbei_Uriage.Item.Item" %>

<asp:Content runat="server" ContentPlaceHolderID="head" >
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }</script>
     <script type="text/javascript">
         $(document).on('click', '.panel-heading span.clickable', function (e) {
             var $this = $(this);
             if (!$this.hasClass('panel-collapsed')) {
                 $this.parents('.panel').find('.SearchPanel').slideUp();
                 $this.addClass('panel-collapsed');
                 $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
             } else {
                 $this.parents('.panel').find('.SearchPanel').slideDown();
                 $this.removeClass('panel-collapsed');
                 $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
             }
         })
          </script>
     <style>
        .form_date {
            border: 1px solid green;
border-radius: 3px;

        }
        .pagination {
            margin-top:0px !important;
        }
        .panel-custom1 > .panel-heading {
    color: white;
    background-color: #008080;
    border-color: #008080;
    text-align: center;
    font-size: 16px;
}

.panel-heading {
    padding: 10px 15px;
    border-bottom: 1px solid transparent;
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
}
        button, input, select, textarea {
            border: 1px solid green;
border-radius: 3px;
        }
        td, th {padding-top:5px;
        }
         td {
             padding-bottom:10px;
         }

        .panel-Search > .panel-heading {
            color: white;
            background-color: #395587;
            border-color: #395587;
            text-align: center;
            font-size: 16px;
        }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }
        .tfoot  {
             float:left !important;
         }
    </style>
</asp:Content>
      
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" >

     <div class="panel panel-custom1" style="margin-top:-40px;" onload="myFunction()">
               <div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file"></span>項目一覧</div>
		        	<div class="panel panel-Search" style="border:1px solid black; z-index:0; margin:10px;">
		        	    <div class="panel-heading">
		        		    <h3 class="panel-title"><span class="glyphicon glyphicon-search"></span>  Search Panel</h3>
		        		    <span runat="server" id="click_collapse" class="pull-right clickable" style="margin-top: -20px;"><i runat="server" id="cep" class="glyphicon glyphicon-chevron-up" style="cursor:pointer; padding-bottom:10px; vertical-align:middle"></i></span>
		        	    </div>
                           <div class="SearchPanel" runat="server" id="hide_panel">
                            <div class="panel-body" style="padding: 10px 0px 0px 0px; margin-left:10px; margin-top:10px;" align="left">

                                 <table style="width:auto">
                                  
                                    <tr>

                                        <td>
                                            自社品番
                                        </td>
                                        <td style="padding-left:10px;">
                                            
    <asp:TextBox ID="txtPartNo" runat="server"  width="130px" Height="25px"  CssClass="ddl11" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                        </td>
                                        <td style="padding-left:100px;">
                                            ブランドコード
                                        </td>
                                        <td style="padding-left:10px;">
                                            

<asp:TextBox ID="txtBrandCode" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return isNumber(event);"></asp:TextBox>


                                        </td>
                                        <td style="padding-left:100px">
 <button id="btnSearch" type="button" class="btn btn-primary ddl111" runat="server" onserverclick="btnSearch_Click"  >
                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle; "></span>&nbsp;&nbsp; 
                                    検索
                            </button>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                               </div>
                        </div>
         </div>


    <div  align="center">
          <asp:GridView runat="server" ID="gvItem" AutoGenerateColumns="False" 
         AllowPaging="True" onpageindexchanging="gvItem_PageIndexChanging" ShowHeaderWhenEmpty="true"
        CssClass="table table-striped table-bordered table-hover usd header-m"   RowStyle-CssClass="rsc1" Width="98%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
        <PagerStyle CssClass="pagination" BackColor="white" ForeColor="#330099" HorizontalAlign="left"/>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <AlternatingRowStyle   BackColor="#DAEFA1" />
        <HeaderStyle BackColor="#395587" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerSettings FirstPageText="<<" LastPageText=">>" PageButtonCount = "4"/>
        <Columns>
         <asp:TemplateField>
                <HeaderTemplate>自社品番</HeaderTemplate>
             <HeaderStyle CssClass="text-center" />
             <ItemStyle  HorizontalAlign="Center"/>
                <ItemTemplate>
                  <asp:Label runat="server" ID="lblPartNo" Text='<%# Bind("PartNo") %>'></asp:Label>

                </ItemTemplate>
             
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>ブランドコード</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblBrandCode" Text='<%# Bind("BrandCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    
            <div class="panel-footer" id="a" style="margin-top: 0px;width: 100%;background-color:#DAEFA1">
       <div >
                        <div class="row">
                        
                            <div>
                                <div class="pull-left">
                                    Go To:
                                        <asp:TextBox Width="30px" runat="server" ID="txtGoto" onkeypress="return isNumber(event)" Height="30px"></asp:TextBox>
                                    <button id="Button3" type="button" class="btn btn-primary" runat="server" onserverclick="btnGoto_Click">
                                        <span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>
                                    </button>
                                </div>
                                <div class="pull-right">
                                    Total_Count:<asp:Label runat="server" ID="lblrowCount" Text="0" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Page Size:
                                <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="gvItem_Indexchanged" Width="50px">
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
</asp:Content>
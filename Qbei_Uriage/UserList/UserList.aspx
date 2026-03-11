<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Uriage.Master" CodeBehind="UserList.aspx.cs" Inherits="Qbei_Uriage.UserList.UserList"  EnableEventValidation ="false"  %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
         function ShowModal() {
             $('#myModal').modal('show');
         }

         function ShowModal1(id) {
             var lbl = document.getElementById("<%= lblID.ClientID %>");
            lbl.value = id;

             var grid = this.document.getElementById("<%= gvUserList.ClientID %>");
             var cnt = grid.rows.length;
             //alert(cnt);
             if (cnt == 2) {
             }
             else {
                  $('#myModal1').modal('show');
             }
           
            return false;
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
    </style>
</asp:Content> 
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

 <div class="panel panel-custom1" style="margin-top:-40px;" onload="myFunction()">
      <asp:HiddenField runat="server" ID="lblID" Value="0" />
               <div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file"></span>ユーザー一覧</div>
		    	<div class="panel panel-Search" style="border:1px solid black; z-index:0; margin:10px;" align="center">
		        	    <div class="panel-heading">
		        		    <h3 class="panel-title"><span class="glyphicon glyphicon-search"></span>  Search Panel</h3>
		        		    <span runat="server" id="click_collapse" class="pull-right clickable" style="margin-top: -20px;"><i runat="server" id="cep" class="glyphicon glyphicon-chevron-up" style="cursor:pointer; padding-bottom:10px; vertical-align:middle"></i></span>
		        	    </div>
                           <div class="SearchPanel" runat="server" id="hide_panel">
                                 <div class="panel-body" style="padding: 10px 0px 0px 0px; margin-left:10px; margin-top:10px;">
                                   <table style="height: 80px; width: 1328px;">
                                       <tr>
                                                 <td>
                                                 ユーザーコード
                                                 </td>
                                                 <td>
                                                   <asp:TextBox ID="txtUserID" runat="server"  width="130px" Height="25px"  CssClass="ddl11" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
  
                                                 </td>
                                           <td>
                                                 ユーザー名
                                                 </td>
                                                 <td>
                                                 <asp:TextBox ID="txtUserName" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                                 </td>
                                           <td>
                                                 パスワード
                                                 </td>
                                                 <td>
                                                   <asp:TextBox ID="txtPassword" runat="server"  width="130px" Height="25px"  CssClass="ddl11" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
  
                                                 </td>
                                           <td>
                                                 更新日
                                                 </td>
                                                 <td>
                                                 <%--<asp:TextBox ID="txtModifiedDate" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>--%>
 <div runat="server" id="div2" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                  <input id="txtModifiedDate" runat="server" maxlength="10" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="myFunction()" class="form-control calendartxt"  style="height:30px; width: 130px;cursor:pointer;background-color:white" type="text" placeholder="yyyy/mm/dd" value=""    />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> 
                                                 </td>
                                           <td>
                                                <button id="btnSearch" type="button" class="btn btn-primary ddl111" runat="server" onserverclick="btnSearch_Click"  >
                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle; "></span>&nbsp;&nbsp; 
                                    検索
                            </button>
                                           </td>
                                           <td>
                                               <button class="btn btn-primary " id="new_users" runat="server" onserverclick="auser_Click"  > <span class="fa fa-users menucolor" style="color:white !important">   </span> Add User  </button>
                                           </td>
                                       </tr>
                                       </table>
                                     </div>
                         </div>
                        </div>      
     </div>
<%--Qbei_User_ID: 
    <asp:TextBox ID="txtUserID" runat="server"  width="130px" Height="25px"  CssClass="ddl11" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
   &nbsp;&nbsp;--%>

<%--UserName: 

<asp:TextBox ID="txtUserName" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>--%>
<%--Password: 
    <asp:TextBox ID="txtPassword" runat="server"  width="130px" Height="25px"  CssClass="ddl11" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
   &nbsp;&nbsp;--%>

<%--Modified_Date: 

<asp:TextBox ID="txtModifiedDate" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
--%>

<%-- <button id="btnSearch" type="button" class="btn btn-primary ddl111" runat="server" onserverclick="btnSearch_Click"  >
                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle; "></span>&nbsp;&nbsp; 
                                    Search
                            </button>--%>


    <div align="center">
          <asp:GridView runat="server" ID="gvUserList" AutoGenerateColumns="False" 
         AllowPaging="True" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvItemMaster_PageIndexChanging" OnRowDataBound="gvSLList_RowDataBound"
        CssClass="table table-striped table-bordered table-hover usd"   RowStyle-CssClass="rsc1" Width="98%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
      <PagerStyle CssClass="pagination" BackColor="white" ForeColor="#330099" HorizontalAlign="left" />
        <FooterStyle BackColor="white" ForeColor="#330099"  />
        <AlternatingRowStyle   BackColor="#DAEFA1" />
        <HeaderStyle BackColor=" #395587 " Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerSettings FirstPageText="<<" LastPageText=">>" PageButtonCount = "4"/>
        <Columns>
         <asp:TemplateField Visible="false">
                <HeaderTemplate>ユーザーコード</HeaderTemplate>
             <HeaderStyle CssClass="text-center" />
                <ItemTemplate>
                  <asp:Label runat="server" ID="lblUserID" Text='<%# Bind("Qbei_User_ID") %>'></asp:Label>

                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>ユーザー名</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUserName" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
             <HeaderTemplate>パスワード</HeaderTemplate>
             <HeaderStyle CssClass="text-center" />
                <ItemTemplate>
                  <asp:Label runat="server" ID="lblPassword" Text='<%# Bind("Password") %>'></asp:Label>

                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>更新日</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblModifiedDate" Text='<%# Bind("Modified_Date","{0:yyyy/MM/dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-HorizontalAlign="Center"  >
            <HeaderTemplate>
                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
            </HeaderTemplate>
                <HeaderStyle CssClass="col-md-2 text-center" />
                <ItemTemplate >
                        <asp:Label ID="hide_now" runat="server" Enabled="true">  <a id="btndelete" style="width:100px !important;"   class="btn btn-info btn-xs danger" runat="server"   onclick=""><span class="glyphicon glyphicon-remove" aria-hidden="true" style="width:41px;"> Remove</span></a></asp:Label>
             
                </ItemTemplate>
                
            </asp:TemplateField>
               
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate><span class="glyphicon glyphicon-wrench" aria-hidden="true"></span></HeaderTemplate>
                 <HeaderStyle CssClass="col-md-2 text-center" />
                <ItemTemplate >
                    <a id="btnEdit1" style="width:80px !important" class="btn btn-info btn-xs" runat="server" onserverclick="btnEdit_Click"><span class="glyphicon glyphicon-edit" aria-hidden="true" style="width:41px;"> Edit</span></a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
         <div class="panel-footer" style="margin-top: 0px;width: 100%;background-color:#DAEFA1">
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
                                <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="gvItemMaster_Indexchanged" Width="50px">
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                </asp:DropDownList>
                                </div>

                                </div>
                        </div>
              </div>
             </div>
    </div>
            <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" />
        <script src="../js/bootstrap-datetimepicker.js"></script>
  <script type="text/javascript">

      $('.form_date').datetimepicker(

          {
              language: 'es',
              format: 'yyyy/mm/dd',
              clearBtn: 1,
              autoclose: 1,
              weekStart: 1,
              startView: 2,
              todayBtn: 1,
              forceParse: 0,
              minView: 2,
              pickerPosition: "bottom-right"

          });
    </script>
      <script type="text/javascript">
          function isNumber(evt) {
              evt = (evt) ? evt : window.event;
              var charCode = (evt.which) ? evt.which : evt.keyCode;
              if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                  return false;
              }
              return true;
          }
          var isShift = false;
          var seperator = "/";
          function DateFormat(txt, keyCode) {
              if (keyCode == 16)
                  isShift = true;
              //Validate that its Numeric
              if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false && keyCode != 32) {
                  if ((txt.value.length == 4 || txt.value.length == 7) && keyCode != 8) {
                      txt.value += seperator;
                  }
                  return true;

              }
              else {
                  return false;

              }
          }

          $(document).ready(function () {
              document.getElementById("<%= txtModifiedDate.ClientID%>").onfocusout = function () { myFunction() };
            function myFunction() {
                var fwo = document.getElementById("<%=txtModifiedDate.ClientID%>");
                var bobo = fwo.value.toString();
                var lastPart1 = bobo.split("/").pop();
                var yy = bobo.split("/")[1];
                var zz = bobo.split("/")[0];
                var x = parseInt(lastPart1, 10);
                var y = parseInt(yy, 10);
                var z = parseInt(zz, 10);
                if (z > 2000 && z < 2100 && y > 0 && y < 13 && x > 0 && x < 32 && (fwo.value.length) > 7 && (fwo.value.length) < 11) {
                    return true;
                }
                else {
                    fwo.value = "";
                }
            }
        });
    </script>
    <script type = "text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false && keyCode != 32) {
                if ((txt.value.length == 4 || txt.value.length == 7) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;

            }
            else {
                return false;

            }
        }
     

    </script>
    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog">   
        <!-- Modal content-->
            <div class="modal-content">
                <div runat="server" id="div1" class="modal-header modal-header-warning">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><asp:Label runat="server" ID="Label1" Text="Confirm"></asp:Label></h4>
                </div>
                <div class="modal-body">
                  <p><asp:Label runat="server" ID="Label2" Text="Do you really want to Delete?"></asp:Label></p>
                </div>
                <div class="modal-footer">
                  <button type="button" id="btnDeleteCofirm" runat="server" onserverclick="btnDeleteCofirm_ServerClick" class="btn btn-success" data-dismiss="modal">Yes</button>
                  <button type="button" onclick="ResetID()" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div> 
        </div>
    </div>
       <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">   
        <!-- Modal content-->
            <div class="modal-content">
                <div runat="server" id="divModalHeader" class="modal-header modal-header-success">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><asp:Label runat="server" ID="lblModalHeader"></asp:Label></h4>
                </div>
                <div class="modal-body">
                  <p><asp:Label runat="server" ID="lblModalMessage"></asp:Label></p>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" onserverclick="refresh_Click">Close</button>
                </div>
            </div> 
        </div>
    </div>
</asp:Content>
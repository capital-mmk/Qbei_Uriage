<%@ Page Language="C#"  MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="Branch.aspx.cs" Inherits="Qbei_Uriage.Branch.Branch" %>


<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }</script>
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
                padding-bottom:10px;

        }
        td {
            padding-left:5px;
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
          });
          $(document).ready(function () {
              $("#<%=gvBranch.ClientID%> [id*=btnDelete]").click(function () {
                  var temp = $(this).closest("tr");
                  if (confirm("削除よろしいですか？")) {
                      $.ajax({
                          type: "POST",
                          contentType: "application/json; charset=utf-8",
                          url: "Branch.aspx/BranchDelete",
                          data: "{strId:" + temp.find("input[type*=hidden]").val() + "}",
                          dataType: "json",
                          error: function (req, msg, err) { alert(err); },
                          success: function (msg) {
                              if (msg.d == 'Ok') {
                                  temp.remove();
                                  if ($("#<%=gvBranch.ClientID%> td").length == 0)
                                  { }
                                  var count = $("#<%=lblrowCount.ClientID%>").text() - 1;
                                  $("#<%=lblrowCount.ClientID%>").text(count);
                              }
                          }
                      });
                  }
              });
          });
          </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">



         <div class="panel panel-custom1" style="margin-top:-40px;" onload="myFunction()">
               <div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file"></span>支店一覧</div>
		        	<div class="panel panel-Search" style="border:1px solid black; z-index:0; margin:10px;">
		        	    <div class="panel-heading">
		        		    <h3 class="panel-title"><span class="glyphicon glyphicon-search"></span>  Search Panel</h3>
		        		    <span runat="server" id="click_collapse" class="pull-right clickable" style="margin-top: -20px;"><i runat="server" id="cep" class="glyphicon glyphicon-chevron-up" style="cursor:pointer; padding-bottom:10px; vertical-align:middle"></i></span>
		        	    </div>
                           <div class="SearchPanel" runat="server" id="hide_panel">
                            <div class="panel-body" style="padding: 10px 0px 0px 0px; margin-left:10px; margin-top:10px;">

                                <table>

                                    <tr>
                                        <td>
支店コード
                                        </td>
                                        <td>

    <asp:TextBox ID="txtBranchCode" runat="server"   width="130px" Height="25px"  onkeypress="return isNumber(event)"></asp:TextBox>

                                        </td>
                                        <td>
支店名
                                        </td>
                                        <td>

<asp:TextBox ID="txtBranchName" runat="server"  width="150px" Height="25px"   onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                        </td>
                                         <td>
支店名略称
                                        </td>
                                        <td>
 
<asp:TextBox ID="txtBrandShortName" runat="server"  width="150px" Height="25px"  onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                        </td>
                                        <td>
店舗区分
                                        </td>
                                        <td>
 
<asp:TextBox ID="txtSotreCategory" runat="server"  width="150px" Height="25px"   onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                         <td>
集計部門
                                        </td>
                                        <td>
 
<asp:TextBox ID="txtSummary" runat="server"  width="150px" Height="25px"   onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                        </td>
                                        <td colspan="4" style="">

                                        
 <button id="btnSearch" type="button" class="btn btn-primary ddl111" runat="server" onserverclick="btnSearch_Click"  >
                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle; "></span>&nbsp;&nbsp; 
                                    検索
                            </button>
<button id="btnAdd" runat="server" class="btn btn-primary ddl111" type="button" onserverclick="btnAdd_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true" style="vertical-align:middle;"></span>&nbsp;
                                    新規支店追加
                                </button>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                               </div>
                        </div>
             </div>
    






    <div  align="center">
          <asp:GridView ID="gvBranch" runat="server"
              AllowPaging="True"
              AutoGenerateColumns="False"
              BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-striped table-bordered table-hover usd header-m"
              OnPageIndexChanging="gvBranch_PageIndexChanging"
              RowStyle-CssClass="rsc1"
              ShowHeaderWhenEmpty="true"
              Width="98%">
        <PagerStyle CssClass="pagination" BackColor="white" ForeColor="#330099" HorizontalAlign="left"/>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <AlternatingRowStyle   BackColor="#DAEFA1" />
        <HeaderStyle BackColor="#395587" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerSettings FirstPageText="<<" LastPageText=">>" PageButtonCount = "4"/>
        <Columns>
         <asp:TemplateField>
                <HeaderTemplate>支店コード</HeaderTemplate>
             <HeaderStyle CssClass="text-center" />
                <ItemTemplate>
                  <asp:HiddenField runat="server" ID ="hdBranchCode" Value ='<%#Bind("BranchCode") %>' />
                  <asp:Label runat="server" ID="lblBranchCode" Text='<%# Bind("BranchCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>支店名</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblBranchName" Text='<%# Bind("BranchName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>支店名略称</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblBrandShortName" Text='<%# Bind("BrandShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>店舗区分</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSotreCategory" Text='<%# Bind("StoreCategory") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>集計部門</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSummary" Text='<%# Bind("Summary") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
          <asp:TemplateField>
           <HeaderTemplate></HeaderTemplate>
           <ItemStyle HorizontalAlign="Center" />
          <ItemTemplate >
          <a id ="btnEdit" runat="server" class="btn btn-info btn-xs" onserverclick="btnEdit_Click">
                            <span aria-hidden="true" class="glyphicon glyphicon-edit">
                                <asp:Label ID="Label1" runat="server" Text="編集"></asp:Label>
                            </span>
                        </a>
                </ItemTemplate>
                                         
            </asp:TemplateField>

             <asp:TemplateField>
           <HeaderTemplate></HeaderTemplate>
           <ItemStyle  HorizontalAlign="Center"/>
          <ItemTemplate>
          <a id="btnDelete" runat="server" class="btn btn-info btn-xs">
            <span aria-hidden="true" class="glyphicon glyphicon-trash">
                <asp:Label ID="Label2" runat="server" Text="削除"></asp:Label>
            </span>
        </a>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
        </Columns>
    </asp:GridView>
      
  <div class="panel-footer" id="a" style="margin-top: 0px;width: 100%;background-color:#DAEFA1">
                    <div > <div class="row">
                       
                            <div>
                                <div class="pull-left">
                                    Go To:
                                        <asp:TextBox Width="30px" runat="server" ID="txtGoto" onkeypress="return isNumber(event)" Height="30px"></asp:TextBox>
                                    <button id="Button3" type="button" class="btn btn-primary" runat="server" onserverclick="btnGoto_Click">
                                        <span id="Span1" runat="server" class="glyphicon glyphicon-hand-right"></span>
                                    </button>
                                </div>
                                <div class="pull-right">
                                    Total_Count:<asp:Label runat="server" ID="lblrowCount" Text="0" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Page Size:
                                <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="gvBranch_Indexchanged" Width="50px">
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
      

      <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" />
        <script src="../js/bootstrap-datetimepicker.js"></script>
           <script type="text/javascript">

               $('.form_date').datetimepicker(

                   {
                       language: 'es',
                       format: 'yyyy-mm-dd',
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
  </asp:Content>
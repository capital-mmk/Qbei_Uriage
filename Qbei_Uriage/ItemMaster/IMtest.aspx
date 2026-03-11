<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IMtest.aspx.cs" Inherits="Qbei_Uriage.ItemMaster.IMtest" MasterPageFile="~/Uriage.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

<%--    <script src="../Scripts/webtoolkit.jscrollable.js"></script>
    <script src="../Scripts/webtoolkit.scrollabletable.js"></script>
    <script src="../Scripts/jquery-1.2.6.js"></script>
    <script src="../Scripts/jquery-1.2.6-vsdoc.js"></script>--%>



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
              isShift = false;
              
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
          
          function DateChecking(obj) {
              var data = obj.value;
              if (!/^\d{4}\/\d{1,2}\/\d{1,2}$/.test(data))
                  return false;
              data = data.split("/");
              var day = parseInt(data[2], 10);
              var month = parseInt(data[1], 10);
              var year = parseInt(data[0], 10);
              var daysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
              if (year < 1900 || year > 2500 || month < 1 || month > 12) {
                  obj.value = "";
                  return false;
              }
              if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                  daysInMonth[1] = 29;
              if (day > 0 && day <= daysInMonth[month - 1]) {
                  return true;
              }
              else {
                  obj.value = "";
                  return false;
              }
          }
          
    </script>
   <script>
       function ClickLink() {
           //alert("hi");
           //document.getElementById('').click();
           //$(document).on('click', '.panel-heading span.clickable', function (e) {
           //    var $this = $(this);
           //    $this.parents('.panel').find('.SearchPanel').slideUp();
           //    $this.addClass('panel-collapsed');
           //    $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
           //})
       }
   </script> 

    <script type="text/javascript">
        $(document).on('click', '.panel-heading span.clickable', function (e) {
            var $this = $(this);
            //alert($("#theid").css("width"))
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.SearchPanel').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
                $('.pagination').css('top', '668px');
            } else {
                $this.parents('.panel').find('.SearchPanel').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
                $('.pagination').css('top', '833px');
            }
        })
        //window.onload() = function () {
        //    alert("ds");
        //    var $this = document.getElementsByClassName("clickable").value;

        //    $this.parents('.panel').find('.SearchPanel').slideDown();
        //    $this.removeClass('panel-collapsed');
        //    $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
        //}
</script>

    <style>
        .table table tbody tr td a, .table table tbody tr td span {
            float:none !important;
        }
        .form_date {
            border: 1px solid green;
            border-radius: 3px;
        }
        .item {
            word-wrap: break-word !important;
            word-break: break-all !important;
            width:100px;
        }
        .pagination {
            margin-top:0px !important;
            position: absolute;
            top: 668px;
            width: 98% !important;
        }
        tr.pagination:hover {
            background-color: white !important;
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
        .panel-body td, .panel-body th {
            padding-top:5px;
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
        .fixedheader {
            width: 98% !important;
            background-color: white;
            border-color: white;
        }
        tr.row-bd td:last-child {
            border-right-color: transparent;
        }
        .fixedheader>thead>tr>th {
            vertical-align: middle;
            font-size: 13px;
        }
       .fixedheader>tbody, .fixedheader>thead, .fixedheader>tbody>tr {
            display: block;
        } 
        .fixedheader>tbody {
           overflow-y: auto;
           height: 435px;
        }
        .fixedheader>tbody>tr>td {
            word-break: break-all;
        }             
    </style>   
      <script type="text/javascript">
          function Init() {
              //   var all = document.getElementById("<%=     gvItemMaster.ClientID %>");   //
              //   var title = all.cloneNode(true); //cloneNode method creates an exact copy of a specified node. the cloned node clones all the child nodes of the original node.
              //   for (i = title.rows.length - 1; i > 0; i--) {
              //   title.deleteRow(i);  //delete all content of title beside the first line(header)
              //     // }
              //  all.deleteRow(0);   //delete the header of GridView1
              // document.getElementById("<%= no.ClientID%>").appendChild(title);     //append the first line of title,namely header of GirdView1 to div
          }
          window.onload = Init;
    </script>

    <style>

    .GridPager a,
.GridPager span {
    display: inline-block;
    padding: 0px 9px;
    margin-right: 4px;
    border-radius: 3px;
    border: solid 1px #c0c0c0;
    background: #e9e9e9;
    box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
    font-size: .875em;
    font-weight: bold;
    text-decoration: none;
    color: #717171;
    text-shadow: 0px 1px 0px rgba(255,255,255, 1);
}

.GridPager a {
    background-color: #f5f5f5;
    color: #969696;
    border: 1px solid #969696;
}

.GridPager span {

    background: #616161;
    box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
    color: #f0f0f0;
    text-shadow: 0px 0px 3px rgba(0,0,0, .5);
    border: 1px solid #3AC0F2;
        }
        </style>
</asp:Content>

    <asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
       <div class="panel panel-custom1" style="margin-top:-40px;">
               <div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file"></span>受注明細一覧</div>
		        	<div class="panel panel-Search" style="border:1px solid black; z-index:0; margin:10px;">
		        	    <div class="panel-heading">
		        		    <h3 class="panel-title"><span class="glyphicon glyphicon-search"></span>  Search Panel</h3>
		        		    <span runat="server" id="click_collapse" class="pull-right clickable" style="margin-top: -20px;"><i runat="server" id="cep" class="glyphicon glyphicon-chevron-up" style="cursor:pointer; padding-bottom:10px; vertical-align:middle"></i></span>
		        	    </div>
                           <div class="SearchPanel" runat="server" id="hide_now">
                            <div class="panel-body" style="padding: 10px 0px 0px 0px; margin:10px 0px 10px 10px;">
                                   <table style="width: 100%;">
                                      <tr>
                                          <td>
                                            売上日
                                          </td>
                                          <td>
                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server"> <ContentTemplate> <div runat="server" id="div1" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="txtSaleDateFrom" data-link-format="yyyy/mm/dd">
                                  <input id="txtSaleDateFrom" runat="server" class="form-control calendartxt" maxlength="10" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="DateChecking(this)"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" value="" placeholder="yyyy/mm/dd"   />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> </ContentTemplate></asp:UpdatePanel>
                                 
                                          </td>
                                          <td>
                                            ~
                                          </td>
                                          <td>
                                           <asp:UpdatePanel ID="UpdatePanel2" runat="server"> <ContentTemplate> <div runat="server" id="div4" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="txtSaleDateTo" data-link-format="yyyy/mm/dd">
                                  <input id="txtSaleDateTo" runat="server" class="form-control calendartxt" maxlength="10" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="DateChecking(this)"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" value="" placeholder="yyyy/mm/dd"   />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> </ContentTemplate></asp:UpdatePanel>
                                 
                                          </td>
                                          <td>
                                             キャンセル日
                                          </td>
                                           <td>
                                             <div runat="server" id="div2" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="txtCancelDate" data-link-format="yyyy-mm-dd">
                                  <input id="txtCancelDate" runat="server" maxlength="10" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="DateChecking(this)" class="form-control calendartxt"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" placeholder="yyyy/mm/dd" value=""    />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> 
                                              </td>
                                          <td>
                                              受注番号
                                          </td>
                                          <td>
                                              
<asp:TextBox ID="txtOrderNo" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                          </td>
                                          <td>
                                              自社品番
                                          </td>
                                          <td>
<asp:TextBox ID="txtPartNo" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                          </td>                                        
                                      </tr>
                                       <tr>                                           
                                          <td>
                                              単価
                                          </td>
                                          <td>
<asp:TextBox ID="txtUnitPrice" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
                                          </td>
                                                   <td>
                                                   数量
                                                   </td>
                                                   <td>
<asp:TextBox ID="txtQuantity" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                                       </td>
                                           <td>
                                               原価
                                           </td>
                                           <td>
<asp:TextBox ID="txtCost" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                               </td>

                                           <td>
                                               送料
                                           </td>
                                           <td>
<asp:TextBox ID="txtShippingCost" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                                </td>
                                         <td>
                                             支店コード
                                         </td>
                                           <td>
<asp:TextBox ID="txtBranchCode" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>                                           
                                       </tr>
                                       <tr>                                           
                                           <td>
                                               金額
                                           </td>
                                           <td>
<asp:TextBox ID="txtAmount" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
                                           </td>
                                           <td>
                                               配送先県電荷
                                           </td>
                                           <td>
<asp:TextBox ID="txtDeliveryCharge" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               使用ポイント
                                           </td>
                                           <td>
<asp:TextBox ID="txtUsagePoint" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               クーポン
                                           </td>
                                           <td>
<asp:TextBox ID="txtCoupon" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               値引き
                                           </td>
                                           <td>
<asp:TextBox ID="txtDiscount" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>                                           
                                       </tr>
                                       <tr>
                                           <td>
                                               更新日
                                           </td>
                                           <td>
   <div runat="server" id="div3" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="txtModifiedDate" data-link-format="yyyy-mm-dd">
                                  <input id="txtModifiedDate" runat="server" maxlength="10" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="DateChecking(this)" class="form-control calendartxt"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" placeholder="yyyy/mm/dd" value=""  />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> 
                                           </td>
                                           <td>
  <button id="btnSearch" type="button" class="btn btn-primary ddl111" runat="server" onserverclick="btnSearch_Click" >
                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle; "></span>&nbsp;&nbsp; 
                                    検索</button>
                                           </td>
                                       </tr>
                                       </table>
                                </div>
                         </div>
                        </div>
                    </div>

        <%--//table table-striped table-bordered table-hover usd--%>

 <div runat="server" id="no" align="center" style="word-wrap:break-word; height:600px; overflow:auto; " >
    <asp:GridView runat="server" ID="gvItemMaster" AutoGenerateColumns="False"
         AllowPaging="True" OnPageIndexChanging="gvItemMaster_PageIndexChanging" ShowHeaderWhenEmpty="true"
         CssClass="table table-bordered table-hover fixedheader" RowStyle-CssClass="row-bd" CellPadding="4" OnPreRender="gvpre_render" >
        <PagerStyle CssClass="pagination"/>
        <FooterStyle BackColor="white" ForeColor="#330099"  />
        <AlternatingRowStyle BackColor="#DAEFA1" />
        <HeaderStyle BackColor=" #395587 " Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerSettings FirstPageText="<<" LastPageText=">>" PageButtonCount = "4"/>
        <Columns>
       <asp:TemplateField  HeaderStyle-Width="90px" ItemStyle-Width="90px" >
              <HeaderTemplate>売上日</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSalesDate" Text='<%# Bind("Sales_Date", "{0:yyyy/MM/dd}") %>' ></asp:Label>
                </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" />   
 </asp:TemplateField>
            <asp:TemplateField  HeaderStyle-Width="91px" ItemStyle-Width="91px" >
              <HeaderTemplate>キャンセル日</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCancelDate" Text='<%# Bind("Cancel_Date", "{0:yyyy/MM/dd}") %>' ></asp:Label>
                </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" />   
 </asp:TemplateField>
             <asp:TemplateField   HeaderStyle-Width="150px" ItemStyle-Width="150px" >
                <HeaderTemplate>受注番号</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblOrderNo" Text='<%# Bind("OrderNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />                
            </asp:TemplateField>
             <asp:TemplateField  HeaderStyle-Width="150px" ItemStyle-Width="150px" >
                <HeaderTemplate>自社品番</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPartNo"  Text='<%# Bind("PartNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />               
            </asp:TemplateField >
             <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-Width="90px"   >
                <HeaderTemplate>単価</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUnitPrice" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />            
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-Width="90px"  >
                <HeaderTemplate>数量</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblQuantity" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                <HeaderTemplate>原価</HeaderTemplate>
                <HeaderStyle CssClass="text-center" />        
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCost" Text='<%# Bind("Cost") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                <HeaderTemplate>金額</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("Amount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                <HeaderTemplate>送料</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblShippingCost" Text='<%# Bind("ShippingCost") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-Width="90px" >
                <HeaderTemplate>支店コード</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblBranchCode" Text='<%# Bind("BranchCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-Width="110px" ItemStyle-Width="110px" >
                <HeaderTemplate>配送先都道府県電荷</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDeliveryCharge" Text='<%# Bind("DeliveryCharge") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderStyle-Width="95px" ItemStyle-Width="95px" >
                <HeaderTemplate>使用ポイント</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUsagePoint" Text='<%# Bind("UsagePoint") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-Width="90px" >
                <HeaderTemplate>クーポン</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCoupon" Text='<%# Bind("Coupon") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-Width="90px" >
                <HeaderTemplate>値引き</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDiscount" Text='<%# Bind("Discount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-Width="90px" >
                <HeaderTemplate>更新日</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblModifiedDate" Text='<%# Bind("Modified_Date", "{0:yyyy/MM/dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
     <footer  class="navbar-fixed-bottom footer"">    
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
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
          </footer>
    </div>
        <%--<link href="../css/bootstrap-datetimepicker.min.css" rel="stylesheet" />--%>
        <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" />
        <script src="../js/bootstrap-datetimepicker.js"></script>
        <%--<script src="../js/bootstrap-datetimepicker.min.js"></script>--%>
         <%--<script type="text/javascript" src="https://www.malot.fr/bootstrap-datetimepicker/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js?t=20130302"></script>
    
     <link href="https://www.malot.fr/bootstrap-datetimepicker/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />--%>
   
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
        <%--//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js
//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js
//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.min.js
//cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/3.1.3/js/bootstrap-datetimepicker.min.js--%>
        
        
         <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js" rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js" rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.min.js" rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/3.1.3/js/bootstrap-datetimepicker.min.js" rel="stylesheet" type="text/css" />
   
        <script>
            //HTML   JS  Result
            //Edit on 
            $(function () {
                var bindDatePicker = function () {
                    $(".date").datetimepicker(
                        {
                            format: 'YYYY-MM-DD'
                        }).find('input:first').on("blur", function () {
                            // check if the date is correct. We can accept dd-mm-yyyy and yyyy-mm-dd.
                            // update the format if it's yyyy-mm-dd
                            var date = parseDate($(this).val());

                            if (!isValidDate(date)) {
                                //create date based on momentjs (we have that)
                                date = moment().format('YYYY-MM-DD');
                            }

                            $(this).val(date);
                        });
                }

                var isValidDate = function (value, format) {
                    format = format || false;
                    // lets parse the date to the best of our knowledge
                    if (format) {
                        value = parseDate(value);
                    }

                    var timestamp = Date.parse(value);

                    return isNaN(timestamp) == false;
                }

                var parseDate = function (value) {
                    var m = value.match(/^(\d{1,2})(\/|-)?(\d{1,2})(\/|-)?(\d{4})$/);
                    if (m)
                        value = m[5] + '-' + ("00" + m[3]).slice(-2) + '-' + ("00" + m[1]).slice(-2);

                    return value;
                }

                bindDatePicker();
            });

            </script>
 

          <%-- <script src="<%= this.Page.ResolveUrl("../Scripts/jquery-1.2.6.js")%>"  type="text/javascript"></script>
        <script src="<%= this.Page.ResolveUrl("../Scripts/webtoolkit.jscrollable.js")%>"  type="text/javascript"></script>--%>
 <%--       <script src="<%= this.Page.ResolveUrl("../Scripts/webtoolkit.scrollabletable.js")%>"  type="text/javascript"></script>--%>
           <%--<script type="text/javascript">
        $(document).ready(function() {
            jQuery('#gvItemMaster').Scrollable(400, 800);
        });
    </script>--%>
 </asp:Content>

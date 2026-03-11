<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemMaster.aspx.cs" MasterPageFile="~/Uriage.Master" Inherits="Qbei_Uriage.ItemMaster.ItemMaster" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
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
              document.getElementById("<%= txtSaleDate.ClientID%>").onfocusout = function () { myFunction() };
            function myFunction() {
                var fwo = document.getElementById("<%=txtSaleDate.ClientID%>");
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
           $(document).ready(function () {
               document.getElementById("<%=   txtModifiedDate.ClientID%>").onfocusout = function () { myFunction() };
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
        $(document).ready(function () {
            document.getElementById("<%= txtCancelDate.ClientID%>").onfocusout = function () { myFunction() };
            function myFunction() {
                var fwo = document.getElementById("<%=txtCancelDate.ClientID%>");
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
            } else {
                $this.parents('.panel').find('.SearchPanel').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
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
    <script>
      Window.onload=  function() {
            alert("Page is loaded");
        }
</script>

    <style>
        .table table tbody tr td a, .table table tbody tr td span {
            float:none !important;
        }
        .table {
            width:100% !important;
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
            margin-left: -4px !important;
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
    <style type="text/css">
        .FixedHeader {

position: relative;


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
</asp:Content>

    <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
       <div class="panel panel-custom1" style="margin-top:-40px;" onload="myFunction()">
               <div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file"></span>Item Master</div>
		        	<div class="panel panel-Search" style="border:1px solid black; z-index:0; margin:10px;">
		        	    <div class="panel-heading">
		        		    <h3 class="panel-title"><span class="glyphicon glyphicon-search"></span>  Search Panel</h3>
		        		    <span runat="server" id="click_collapse" class="pull-right clickable" style="margin-top: -20px;"><i runat="server" id="cep" class="glyphicon glyphicon-chevron-up" style="cursor:pointer; padding-bottom:10px; vertical-align:middle"></i></span>
		        	    </div>
                           <div class="SearchPanel" style="height:160px">
                            <div class="panel-body" style="padding: 10px 0px 0px 0px; margin-left:10px; margin-top:10px;">
                                   <table style="height: 80px; width: 100%;">
                                      <tr>
                                          <td>
                                Sales_Date
                                          </td>
                                          <td>
                                           <asp:UpdatePanel runat="server"> <ContentTemplate> <div runat="server" id="div1" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="dtp_input2" data-link-format="yyyy/mm/dd">
                                  <input id="txtSaleDate" runat="server" class="form-control calendartxt" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="myFunction()"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" value="" placeholder="yyyy/mm/dd"   />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> </ContentTemplate></asp:UpdatePanel>
                                 
                                          </td>
                                          <td>
                                             Cancel_Date
                                          </td>
                                           <td>
                                             <div runat="server" id="div2" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                  <input id="txtCancelDate" runat="server" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="myFunction()" class="form-control calendartxt"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" placeholder="yyyy/mm/dd" value=""    />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> 
                                              </td>
                                          <td>
                                              OrderNo
                                          </td>
                                          <td>
                                              
<asp:TextBox ID="txtOrderNo" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                          </td>
                                          <td>
                                              PartNo
                                          </td>
                                          <td>
<asp:TextBox ID="txtPartNo" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                          </td>
                                          <td>
                                              UnitPrice
                                          </td>
                                          <td>
<asp:TextBox ID="txtUnitPrice" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
                                          </td>
                                      </tr>
                                       <tr>
                                                   <td>
                                                   Quantity
                                                   </td>
                                                   <td>
<asp:TextBox ID="txtQuantity" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                                       </td>
                                           <td>
                                               Cost
                                           </td>
                                           <td>
<asp:TextBox ID="txtCost" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                               </td>

                                           <td>
                                               ShippingCost
                                           </td>
                                           <td>
<asp:TextBox ID="txtShippingCost" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                                </td>
                                         <td>
                                             BranchCode
                                         </td>
                                           <td>
<asp:TextBox ID="txtBranchCode" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               Amount
                                           </td>
                                           <td>
<asp:TextBox ID="txtAmount" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               DeliveryCharge
                                           </td>
                                           <td>
<asp:TextBox ID="txtDeliveryCharge" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               UsagePoint
                                           </td>
                                           <td>
<asp:TextBox ID="txtUsagePoint" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               Coupon
                                           </td>
                                           <td>
<asp:TextBox ID="txtCoupon" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               Discount
                                           </td>
                                           <td>
<asp:TextBox ID="txtDiscount" runat="server"  width="150px" Height="25px"  CssClass="ddl111" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                                           </td>
                                           <td>
                                               Modified_Date
                                           </td>
                                           <td>
   <div runat="server" id="div3" class="input-group date form_date col-md-1" data-date=""  data-date-format="yyyy mm dd" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                  <input id="txtModifiedDate" runat="server" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="myFunction()" class="form-control calendartxt"  style="height:30px; width: 110px;cursor:pointer;background-color:white" type="text" placeholder="yyyy/mm/dd" value=""  />
					             <span class="input-group-addon" style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div> 
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
  <button id="btnSearch" type="button" class="btn btn-primary ddl111" runat="server" onserverclick="btnSearch_Click" >
                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle; "></span>&nbsp;&nbsp; 
                                    Search</button>
                                           </td>
                                       </tr>
                                       </table>

                                </div>
                         </div>
                        </div>
                    </div>

        <%--//table table-striped table-bordered table-hover usd--%>

 <div runat="server" id="no" align="center" style="word-wrap:break-word;width:100% !important; height:500px; overflow:auto; " >
          <asp:GridView runat="server" ID="gvItemMaster" AutoGenerateColumns="False"   style="border:1px solid white; width:100% !important;"
         AllowPaging="True" onpageindexchanging="gvItemMaster_PageIndexChanging" ShowHeaderWhenEmpty="true"
        HeaderStyle-CssClass="FixedHeader"  RowStyle-CssClass="rsc1" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
        <PagerStyle CssClass="pagination" BackColor="white" ForeColor="#330099" HorizontalAlign="Center"/>
        <FooterStyle BackColor="white" ForeColor="#330099"  />
        <AlternatingRowStyle   BackColor="#DAEFA1" />
        <HeaderStyle BackColor=" #395587 " Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerSettings FirstPageText="<<" LastPageText=">>" PageButtonCount = "4"/>
        <Columns>
         <asp:TemplateField >
                <HeaderTemplate  >Sales_Date</HeaderTemplate>
          
                <ItemTemplate >
                  <asp:Label runat="server" ID="lblSalesDate" Text='<%# Bind("Sales_Date", "{0:yyyy/dd/MM}") %>' ></asp:Label>

                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="true" Width="10%" />
                <HeaderStyle CssClass="text-center" Wrap="true" Width="14%" />
            </asp:TemplateField >
            <asp:TemplateField >
              
                <HeaderTemplate>Cancel_Date</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCancelDate" Text='<%# Bind("Cancel_Date", "{0:yyyy/dd/MM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="true" Width="120" />
                <HeaderStyle CssClass="text-center" Wrap="true" Width="100" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>OrderNo</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblOrderNo" Text='<%# Bind("OrderNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>PartNo</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPartNo"  Text='<%# Bind("PartNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>UnitPrice</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUnitPrice" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>Quantity</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblQuantity" Text='<%# Bind("Quantity") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>Cost</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemStyle Width="120" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCost" Text='<%# Bind("Cost") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>Amount</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("Amount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>ShippingCost</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblShippingCost" Text='<%# Bind("ShippingCost") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>BranchCode</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblBranchCode" Text='<%# Bind("BranchCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>DeliveryCharge</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDeliveryCharge" Text='<%# Bind("DeliveryCharge") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>UsagePoint</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUsagePoint" Text='<%# Bind("UsagePoint") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>Coupon</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCoupon" Text='<%# Bind("Coupon") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>Discount</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDiscount" Text='<%# Bind("Discount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField ItemStyle-Width="100">
                <HeaderTemplate>Modified_Date</HeaderTemplate>
                <HeaderStyle CssClass="text-center"/>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblModifiedDate" Text='<%# Bind("Modified_Date", "{0:yyyy/dd/MM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
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
        
        <script >
            function Calculation() {
                var grid1 = document.getElementById("ContentPlaceHolder1_txtSaleDate").value;
                //alert(grid.rows.length);

                //var sum1 = 0;
                //alert(grid1);
                var date = parseDate(grid1);
                alert(date)
                if (!isValidDate(date)) {
                    // Date isn't valid. Set the current date instead
                    date = moment().format('YYYY/MM/DD');
                }
                //grid1 = date;
               
                //$(this).val(date);
            }</script>
         <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js" rel="stylesheet" type="text/css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js" rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.min.js" rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/3.1.3/js/bootstrap-datetimepicker.min.js" rel="stylesheet" type="text/css" />
   
        <script>
            //HTML   JS  Result
            //Edit on 
            $(function () {
                var bindDatePicker = function() {
                    $(".date").datetimepicker(
                        {
                        format:'YYYY-MM-DD'
                    }).find('input:first').on("blur",function () {
                        // check if the date is correct. We can accept dd-mm-yyyy and yyyy-mm-dd.
                        // update the format if it's yyyy-mm-dd
                        var date = parseDate($(this).val());

                        if (! isValidDate(date)) {
                            //create date based on momentjs (we have that)
                            date = moment().format('YYYY-MM-DD');
                        }

                        $(this).val(date);
                    });
                }
   
                var isValidDate = function(value, format) {
                    format = format || false;
                    // lets parse the date to the best of our knowledge
                    if (format) {
                        value = parseDate(value);
                    }

                    var timestamp = Date.parse(value);

                    return isNaN(timestamp) == false;
                }
   
                var parseDate = function(value) {
                    var m = value.match(/^(\d{1,2})(\/|-)?(\d{1,2})(\/|-)?(\d{4})$/);
                    if (m)
                        value = m[5] + '-' + ("00" + m[3]).slice(-2) + '-' + ("00" + m[1]).slice(-2);

                    return value;
                }
   
                bindDatePicker();
            });

            </script>
 
 </asp:Content>
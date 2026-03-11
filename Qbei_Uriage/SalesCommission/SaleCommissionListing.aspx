<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="SaleCommissionListing.aspx.cs" Inherits="Qbei_Uriage.SalesCommission.SaleCommissionListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtShopCd.ClientID%>").on("keypress keyup", function (e) {
                $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                if (e.which < 48 || e.which > 57) {
                    e.preventDefault();
                }
            });

            $("#<%=txtSCPer.ClientID%>").on("keypress keyup", function (e) {
                $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                if (e.which < 48 || e.which > 57)
                    e.preventDefault();
            });

            $("#<%=gvSaleCommission.ClientID%> [id*=btnDelete]").click(function () {
                var temp = $(this).closest("tr");
                if (confirm("削除よろしいですか？"))
                {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "SaleCommissionListing.aspx/SCommissionDelete",
                        data: "{strId:"+temp.find("input[type=hidden]").val()+"}",
                        dataType: "json",
                        error: function (req, msg, err) { alert(err); },
                        success: function (msg) {
                            if (msg.d == 'Ok')
                            {
                                temp.remove();
                                if ($("#<%=gvSaleCommission.ClientID%> td").length == 0)
                                { }
                                var count = $("#<%=lblCnt.ClientID%>").text() - 1;
                                $("#<%=lblCnt.ClientID%>").text(count);
                            }
                        }
                    });
                }
            });
        });

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
    <style>
        .form_date {
            border : 1px solid green;
            border-radius : 3px;
        }
        .pagination {
            margin-top : 0px !important;
        }
        .panel-custom1 > .panel-heading {
            color : #ffffff;
            background-color : #008080;
            border-color : #008080;
            text-align : center ;
            font-size : 16px;
        }
        .panel-heading {
            padding : 10px 15px;
            border-bottom : 1px solid transparent;
            border-top-left-radius : 3px;
            border-top-right-radius : 3px;
        }
        button, input, select, textarea {
            border : 1px solid green;
            border-radius : 3px;
        }
        td, th {
            padding-top : 5px;
            padding-bottom : 10px;
        }
        td {
            padding-left : 5px;
        }
        .panel-Search > .panel-heading {
            color: #ffffff;
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
        .tfoot {
            float:left !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-custom1" style="margin-top:-40px;">
        <div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file">販売手数料一覧</span></div>
        <div class="panel panel-Search" style="border:1px solid black; z-index:0; margin:10px;">
            <div class="panel-heading">
                <h3 class="panel-title"><span class="glyphicon glyphicon-search"></span>Search Panel</h3>
                <span runat="server" id="click_collapse" class="pull-right clickable" style="margin-top: -20px;"><i runat="server" id="cep" class="glyphicon glyphicon-chevron-up" style="cursor:pointer; padding-bottom:10px; vertical-align:middle"></i></span>
            </div>
            <div class="SearchPanel" runat="server" id="hide_panel">
                <div class="panel-body" style="padding: 10px 0px 0px 0px; margin-left:10px; margin-top:10px;">
                    <table style="height: 80px; width: 1328px;">
                        <tr>
                            <td>勘定科目</td>
                            <td>
                                <asp:TextBox ID="txtAcc" runat="server" Width="130px" Height="25px"></asp:TextBox>
                            </td>
                            <td>支店コード</td>
                            <td>
                                <asp:TextBox ID="txtShopCd" runat="server" Width="130px" Height="25px"></asp:TextBox>
                            </td>
                            <td>割合</td>
                            <td>
                                <asp:TextBox ID="txtSCPer" runat="server" Width="130px" Height="25px"></asp:TextBox>&nbsp;%
                            </td>
                        </tr>
                        <tr>
                            <td>開始日付</td>
                            <td>
                                <div id="Div1" runat="server" class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm dd" data-link-field="dtpSD" data-link-format ="yyyy-mm-dd">
                                    <input id="txtSD" runat="server" type="text" maxlength="10" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm/dd" value=""/>
                                    <span class="input-group-addon " style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                            </td>

                            <td>終了日付</td>
                            <td>
                                <div id="Div2" runat="server" class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm dd" data-link-field="dtpED" data-link-format ="yyyy-mm-dd">
                                    <input id="txtED" runat="server" type="text" maxlength="10" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm/dd" value=""/>
                                    <span class="input-group-addon " style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                            </td>
                            <td>
                                <button id="btnSearch" runat="server" class="btn btn-primary ddl111" type="button" onserverclick="btnSearch_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true" style="vertical-align:middle;"></span>&nbsp;&nbsp;
                                    検索
                                </button>
                            </td>
                            <td>
                                <button id="btnAdd" runat="server" class="btn btn-primary ddl111" type="button" onserverclick="btnAdd_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true" style="vertical-align:middle;"></span>&nbsp;&nbsp;
                                    新規販売手数料追加
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div align="center">
        <asp:GridView ID="gvSaleCommission" runat="server" AutoGenerateColumns="false" AllowPaging="true" Width="98%"
            ShowHeaderWhenEmpty ="true" CssClass ="table table-striped table-bordered table-hover usd header-m" OnPageIndexChanging="gvSaleCommission_PageIndexChanging"
            RowStyle-CssClass="rsc1" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <PagerStyle CssClass="pagination" BackColor="white" ForeColor="#330099" HorizontalAlign="left" />
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"/>
            <AlternatingRowStyle BackColor="#DAEFA1" />
            <HeaderStyle BackColor="#395587" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerSettings FirstPageText="<<" LastPageText=">>" PageButtonCount = "4"/>
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>勘定科目</HeaderTemplate>
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:HiddenField ID="hdSaleCommissionId" runat="server" Value='<%#Bind("SalesCommissionID") %>' />
                        <asp:Label runat="server" ID="lblSCAcc" Text='<%#Bind("AccountTitle") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>支店コード</HeaderTemplate>
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblShopCode" runat="server" Text='<%#Bind("ShopCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>割合</HeaderTemplate>
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSCPercent" Text='<%#(decimal.Parse (Eval("Percent").ToString()) * 100).ToString("###") + " %" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>開始日付</HeaderTemplate>
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSCStartDT" Text='<%#Bind("Expire_SDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>終了日付</HeaderTemplate>
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSCEndDT" Text='<%#Bind("Expire_EDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a id="lblEdit" runat="server" class="btn btn-info btn-xs" onserverclick="btnEdit_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true">
                                <asp:Label Text ="編集" runat="server"></asp:Label>
                            </span>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a id="btnDelete" runat ="server" class="btn btn-info btn-xs">
                            <span class="glyphicon glyphicon-trash" >
                                <asp:Label runat="server" Text="削除"></asp:Label>
                            </span>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="panel-footer" id="footer" style="margin-top: 0px;width: 100%;background-color:#DAEFA1">
            <div>
                <div class="row">
                    <div>
                        <div class="pull-left">
                            Go To:
                            <asp:TextBox Width="30px" runat="server" ID="txtGoto" onkeypress="return isNumber(event)" Height="30px"></asp:TextBox>
                            <button id="btnGoto" type="button" class="btn btn-primary" runat="server" onserverclick="btnGoto_Click">
                                <span id="Span1" runat="server" class="glyphicon glyphicon-hand-right"></span>
                            </button>
                        </div>
                        <div class ="pull-right">
                            Total Count:
                            <asp:Label runat ="server" ID ="lblCnt"></asp:Label>
                            Page Size:
                            <asp:DropDownList runat="server" ID="ddlPageSize" AutoPostBack="true" Height="30px"  Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" >
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
</asp:Content>

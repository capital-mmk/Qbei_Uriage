<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="PackingFareEntry.aspx.cs" Inherits="Qbei_Uriage.PackingFareMaster.PackingFareEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-heading {
            padding: 10px 15px;
            border: 1px solid;
            border-top: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }
        .font {
            font-size: 14px;
            color: #fff;
            font-weight: 600;
            padding: 10px 45px;          
            outline: none;
            border: none;
        }
         input, select, textarea {
            border : 1px solid green;
            border-radius : 3px;
            height : 25px;
        }
        .btn {
            width:100px;
            height: 50px;
        }
        .error {
            border-color:#fc0a26;
        }
        td, th {
            padding-top : 5px;
            padding-bottom : 10px;
        }
        td {
            padding-left : 5px;
        }
    </style>
    <script type="text/javascript">
        var IsValid = false;
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
            if (data == "" || data.length == 0 || data == null) {
                if ($(obj).attr("id") == $("#<%=txtSD.ClientID%>").attr("id")) {
                    $("#<%=txtSD.ClientID%>").addClass("error");
                    $("#<%=spPStart.ClientID%>").text("入力してください");
                }
                return false;
            }
            else {
                if ($(obj).attr("id") == $("#<%=txtSD.ClientID%>").attr("id")) {
                    $("#<%=txtSD.ClientID%>").removeClass("error");
                    $("#<%=spPStart.ClientID%>").text("");
                }
            }
            if (!/^\d{4}\/\d{1,2}\/\d{1,2}$/.test(data))
                return false;
            data = data.split("/");
            var day = parseInt(data[2], 10);
            var month = parseInt(data[1], 10);
            var year = parseInt(data[0], 10);
            var daysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            if (year < 1900 || year > 2100 || month < 1 || month > 12) {
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

        function DateCompare() {
            var start, end;
            start = $("#<%=txtSD.ClientID%>").val();
            end = $("#<%=txtED.ClientID%>").val();
            if (end == "" || end.length == 0 || end == null)
                return true;
            else {
                start = new Date(start);
                end = new Date(end);
                if (start >= end)
                {
                    $("#<%=txtSD.ClientID%>").addClass("error");
                    $("#<%=spPStart.ClientID%>").text("終了日が開始日より前です");
                    return false;
                }
                else
                {
                    $("#<%=txtSD.ClientID%>").removeClass("error");
                    $("#<%=spPStart.ClientID%>").text("");
                    return true;
                }
            }
        }

        function NumOnly(obj, e) {
            obj.value.replace(/[^0-9\.]/g, '');
            if (e.which < 48 || e.which > 57)
                e.preventDefault();
        }

        function InputCheck()
        {
            IsValid = false;
            $("#<%=txtPAcc.ClientID%>").blur();
            if (IsValid)
            {
                $("#<%=txtDeliveryCd.ClientID%>").blur();
                if (IsValid) {
                    $("#<%=txtPPrice.ClientID%>").blur();
                    if (IsValid && DateChecking(document.getElementById("<%=txtSD.ClientID%>")) && DateCompare())
                        return true;                    
                    else return false;
                } else return false;
            }
            else return false;
        }

        $(document).ready(function () {
            $("#<%=txtDeliveryCd.ClientID%>").on("keypress keyup", function (e) {
                NumOnly(document.getElementById("<%=txtDeliveryCd.ClientID%>"), e);
            });
            $("#<%=txtPPrice.ClientID%>").on("keypress keyup", function (e) {
                NumOnly(document.getElementById("<%=txtPPrice.ClientID%>"), e);
            });
            $("#<%=txtPAcc.ClientID%>").on("blur", function () {
                IsValid = false;
                var acc = $("#<%=txtPAcc.ClientID%>").val();
                if (acc == "" || acc.length == 0 || acc == null) {
                    $("#<%=txtPAcc.ClientID%>").addClass("error");
                    $("#<%=spPAcc.ClientID%>").text("入力してください");
                }
                else
                {
                    $("#<%=txtPAcc.ClientID%>").removeClass("error");
                    $("#<%=spPAcc.ClientID%>").text("");
                    IsValid = true;
                }
            });

            $("#<%=txtDeliveryCd.ClientID%>").on("blur", function () {
                IsValid = false;
                var delivery = $("#<%=txtDeliveryCd.ClientID%>").val();
                if (delivery == "" || delivery.length == 0 || delivery == null) {
                    $("#<%=txtDeliveryCd.ClientID%>").addClass("error");
                    $("#<%=spDeliveryCd.ClientID%>").text("入力してください");
                }
                else {
                    $("#<%=txtDeliveryCd.ClientID%>").removeClass("error");
                    $("#<%=spDeliveryCd.ClientID%>").text("");
                    IsValid = true;
                }
            });

            $("#<%=txtPPrice.ClientID%>").on("blur", function () {
                IsValid = false;
                var price = $("#<%=txtPPrice.ClientID%>").val();
                if (price == "" || price.length == 0 || price == null) {
                    $("#<%=txtPPrice.ClientID%>").addClass("error");
                    $("#<%=spPPrice.ClientID%>").text("入力してください");
                }
                else {
                    $("#<%=txtPPrice.ClientID%>").removeClass("error");
                    $("#<%=spPPrice.ClientID%>").text("");
                    $("#<%=txtPPrice.ClientID%>").val(price.replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                    IsValid = true;
                }
            });

            $("#<%=btnSave.ClientID%>").click(function (e) {
                e.preventDefault();
                if (confirm("保存します。よろしいですか？"))
                {
                    IsValid = false;
                    if (InputCheck()) {
                        var param = new Array();
                        param[0] = $("#<%=hdPackingFareID.ClientID%>").val();
                        param[1] = $("#<%=txtPAcc.ClientID%>").val();
                        param[2] = $("#<%=ddlOrderType.ClientID%>").val();
                        param[3] = $("#<%=txtDeliveryCd.ClientID%>").val();
                        param[4] = $("#<%=ddlRegion.ClientID%>").val();
                        param[5] = $("#<%=txtPPrice.ClientID%>").val();
                        param[6] = $("#<%=txtSD.ClientID%>").val();
                        param[7] = $("#<%=txtED.ClientID%>").val();

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "PackingFareEntry.aspx/PackingFareSave",
                            data: JSON.stringify({lstParam:param}),
                            datatype: "json",
                            error: function (req, msg, err) { alert(err) },
                            success: function (msg) {
                                window.alert(msg.d);
                                window.location.href = "PackingFareListing.aspx";
                            }
                        });
                    }
                    else {
                        IsValid = false;
                        return;
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <div class="panel-heading" style="width:70%;border-radius:15px;height:500px;background-color:#ffffff;">
            <div class="row">
                <div style="background-color:#395587;height: 40px; vertical-align:middle; line-height:40px;
                    border-top-left-radius: 10px;border-top-right-radius: 10px;margin-top: -9px;background-color: #395587;">
                    <span class="font" style="font-size:16px; font-weight:bolder;color:white">荷造運賃設定</span>
                </div>
            </div>
            <div style="padding-top:20px;">
                <table class="table-responsive" style="width:100%">
                    <tr>
                        <td>
                            勘定科目
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPAcc"></asp:TextBox>
                        </td>
                        <td>
                            <span id="spPAcc" runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td>
                            <asp:HiddenField runat="server" ID="hdPackingFareID" />
                        </td>
                    </tr>
                    <tr>
                        <td>注文タイプ</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlOrderType" Height="30px"  CssClass="dropdown">
                                <asp:ListItem Text="自転車含む" Value="True"></asp:ListItem>
                                <asp:ListItem Text="自転車以外" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>配送会社コード</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDeliveryCd"></asp:TextBox>
                        </td>
                        <td>
                            <span id="spDeliveryCd"  runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>配送先県</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlRegion" CssClass="dropdown" Height="30px">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>単価</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPPrice"></asp:TextBox>&nbsp;円
                        </td>
                        <td>
                            <span id="spPPrice" runat ="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>開始日付</td>
                        <td>
                                <div id="Div1" runat="server" class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm dd" data-link-field="dtpSD" data-link-format ="yyyy-mm-dd">
                                    <input id="txtSD" runat="server" type="text" maxlength="10" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm/dd" value=""/>
                                    <span class="input-group-addon " style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                        </td>
                        <td>
                            <span id="spPStart" runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>終了日付</td>
                        <td>
                                <div id="Div2" runat="server" class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm dd" data-link-field="dtpED" data-link-format ="yyyy-mm-dd">
                                    <input id="txtED" runat="server" type="text" maxlength="10" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm/dd" value=""/>
                                    <span class="input-group-addon " style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                        </td>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
            <div class="row col-md-6 pad-adjust" style="padding-top:50px;">
                <asp:Button ID="btnSave" CssClass="btn btn-info" runat="server" Text="登録"  />
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

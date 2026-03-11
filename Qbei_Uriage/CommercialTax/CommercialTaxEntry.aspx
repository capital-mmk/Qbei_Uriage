<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="CommercialTaxEntry.aspx.cs" Inherits="Qbei_Uriage.CommercialTax.CommercialTaxEntry" %>
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
                    $("#<%=spCStart.ClientID%>").text("入力してください");
                }
                return false;
            }
            else {
                if ($(obj).attr("id") == $("#<%=txtSD.ClientID%>").attr("id")) {
                    $("#<%=txtSD.ClientID%>").removeClass("error");
                    $("#<%=spCStart.ClientID%>").text("");
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

        function DateCompare()
        {
            var start, end;
            start = $("#<%=txtSD.ClientID%>").val();
            end = $("#<%=txtED.ClientID%>").val();
            if (end == "" || end.length == 0 || end == null) {
                return true;
            } else
            {
                start = new Date(start);
                end = new Date(end);
                if (start >= end) {
                    $("#<%=txtSD.ClientID%>").addClass("error");
                    $("#<%=spCStart.ClientID%>").text("終了日が開始日より前です");
                    return false;
                }
                else
                {
                    $("#<%=txtSD.ClientID%>").removeClass("error");
                    $("#<%=spCStart.ClientID%>").text("");
                    return true;
                }
            }
        }

        function NumOnly(obj,e)
        {
            obj.value.replace(/[^0-9\.]/g, '');
            if (e.which < 48 || e.which > 57)
                e.preventDefault();
        }

        function InputCheck()
        {
            var percent, price;
            IsValid = false;
            $("#<%=txtCAcc.ClientID%>").blur();
            if (IsValid)
            {
                $("#<%=txtPayment.ClientID%>").blur();
                if (IsValid)
                {
                    percent = $("#<%=txtCPercent.ClientID%>").val();
                    price = $("#<%=txtCPrice.ClientID%>").val();
                    if((percent == "" || percent.length == 0 || percent == null) && (price == "" || price.length == 0 || price == null))
                    {
                        $("#<%=txtCPrice.ClientID%>").addClass("error");
                        $("#<%=spCPrice.ClientID%>").text("単価または割合に入力してください");
                        return false;
                    }
                    else if ((percent != "" && percent.length > 0 && percent != null) && (price != "" && price.length > 0 && price != null))
                    {
                        if (parseInt(percent) == 0 && parseInt(price) == 0) {
                            $("#<%=txtCPrice.ClientID%>").addClass("error");
                            $("#<%=spCPrice.ClientID%>").text("単価または割合に入力してください");
                            return false;
                        }
                        else if (parseInt(percent) > 0 && parseInt(price) > 0) {
                            $("#<%=txtCPrice.ClientID%>").addClass("error");
                            $("#<%=spCPrice.ClientID%>").text("単価または割合のみ入力してください");
                            return false;
                        }
                        else return true;
                    }
                    else
                    {                        
                        if (percent != "" && percent.length > 0 && percent != null)
                        {
                            percent = parseInt(percent);
                            if (percent < 0 || percent > 100) {
                                $("#<%=txtCPercent.ClientID%>").addClass("error");
                                $("#<%=spCPer.ClientID%>").text("割合は0~100までです");
                                return false;
                            }
                        }
                        if (DateChecking(document.getElementById("<%=txtSD.ClientID%>")) && DateCompare())
                            return true;
                        else return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        $(document).ready(function () {

            $("#<%=txtPayment.ClientID%>").on("keypress keyup", function (e) {
                NumOnly(document.getElementById("<%=txtPayment.ClientID%>"),e);
            });

            $("#<%=txtCPrice.ClientID%>").on("keypress keyup", function (e) {
                NumOnly(document.getElementById("<%=txtCPrice.ClientID%>"), e);
            });

            $("#<%=txtCPercent.ClientID%>").on("keypress keyup", function (e) {
                NumOnly(document.getElementById("<%=txtCPercent.ClientID%>"), e);
            });

            $("#<%=txtCAcc.ClientID%>").on("blur", function () {
                IsValid = false;
                var acc = $("#<%=txtCAcc.ClientID%>").val();
                if (acc == "" || acc.length == 0 || acc == null) {
                    $("#<%=txtCAcc.ClientID%>").addClass("error");
                    $("#<%=spCAcc.ClientID%>").text("入力してください");
                }
                else {
                    $("#<%=txtCAcc.ClientID%>").removeClass("error");
                    $("#<%=spCAcc.ClientID%>").text("");
                    IsValid = true;
                }
            });

            $("#<%=txtPayment.ClientID%>").on("blur", function () {
                IsValid = false;                
                var payment = $("#<%=txtPayment.ClientID%>").val();
                if (payment == "" || payment.length == 0 || payment == null) {
                    $("#<%=txtPayment.ClientID%>").addClass("error");
                    $("#<%=spPayment.ClientID%>").text("入力してください");
                }
                else {
                    $("#<%=txtPayment.ClientID%>").removeClass("error");
                    $("#<%=spPayment.ClientID%>").text("");
                    IsValid = true;
                }
            });

            $("#<%=txtCPrice.ClientID%>").on("blur", function () {
                var price = $("#<%=txtCPrice.ClientID%>").val();
                if (price != null && price.length != 0 && price != "")
                    $("#<%=txtCPrice.ClientID%>").val(price.replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            });

            $("#<%=btnSave.ClientID%>").click(function (e) {
                e.preventDefault();
                if (confirm("保存します。よろしいですか？"))
                {
                    $("#<%=txtCPrice.ClientID%>").removeClass("error");
                    $("#<%=txtCPercent.ClientID%>").removeClass("error");
                    $("#<%=spCPrice.ClientID%>").text("");
                    $("#<%=spCPer.ClientID%>").text("");
                    if (InputCheck())
                    {
                        var param = new Array();
                        param[0] = $("#<%=hdCommercialTaxID.ClientID%>").val();
                        param[1] = $("#<%=txtCAcc.ClientID%>").val();
                        param[2] = $("#<%=txtPayment.ClientID%>").val();
                        param[3] = $("#<%=txtCPrice.ClientID%>").val();
                        param[4] = $("#<%=txtCPercent.ClientID%>").val();
                        param[5] = $("#<%=txtSD.ClientID%>").val();
                        param[6] = $("#<%=txtED.ClientID%>").val();

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "CommercialTaxEntry.aspx/CommercialTaxSave",
                            data: JSON.stringify({ lstParam: param }),
                            datatype: "json",
                            error: function (req, msg, err) { },
                            success: function (msg) {
                                window.alert(msg.d);
                                window.location.href = "CommercialTaxListing.aspx";
                            }
                        });
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
                    <span class="font" style="font-size:16px; font-weight:bolder;color:white">決済手数料設定</span>
                </div>
            </div>
            <div style="padding-top:20px;">
                <table class="table-responsive" style="width:100%">
                    <tr>
                        <td>
                            勘定科目
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCAcc"></asp:TextBox>
                        </td>
                        <td>
                            <span id="spCAcc" runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td>
                            <asp:HiddenField runat="server" ID="hdCommercialTaxID" />
                        </td>
                    </tr>
                    <tr>
                        <td>支払区分コード</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPayment"></asp:TextBox>
                        </td>
                        <td>
                            <span id="spPayment"  runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>単価</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCPrice"></asp:TextBox>&nbsp;円
                        </td>
                        <td>
                            <span id="spCPrice" runat ="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>割合</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCPercent"></asp:TextBox>&nbsp;%
                        </td>
                        <td>
                            <span id="spCPer" runat ="server" style="color:#fc0a26;text-align:left;"></span>
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
                            <span id="spCStart" runat="server" style="color:#fc0a26;text-align:left;"></span>
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

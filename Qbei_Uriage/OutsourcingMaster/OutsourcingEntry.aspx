<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="OutsourcingEntry.aspx.cs" Inherits="Qbei_Uriage.OutsourcingMaster.OutsourcingEntry" %>
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
                    $("#<%=spOStart.ClientID%>").text("入力してください");
                }
                return false;
            }
            else {
                if ($(obj).attr("id") == $("#<%=txtSD.ClientID%>").attr("id")) {
                    $("#<%=txtSD.ClientID%>").removeClass("error");
                    $("#<%=spOStart.ClientID%>").text("");
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
            if (end == "" || end.length == 0 || end == null)
            {
                return true;
            }
            start = new Date(start);
            end = new Date(end);
            if (start >= end) {
                $("#<%=spOStart.ClientID%>").text("終了日が開始日より前です");
                $("#<%=txtSD.ClientID%>").addClass("error");
                return false;
            }
            else {
                $("#<%=spOStart.ClientID%>").text("");
                $("#<%=txtSD.ClientID%>").removeClass("error");
                return true;
            }
        }

        $(document).ready(function () {

            IsValid = false;

            $("#<%=txtOPrice.ClientID%>").on("keypress keyup", function (e) {
                $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
                if (e.which < 48 || e.which > 57)
                    e.preventDefault();
            });

            $("#<%=txtOPrice.ClientID%>").on("blur", function () {
                IsValid = false;
                var price = $("#<%=txtOPrice.ClientID%>").val();
                if (price == "" || price.length == 0 || price == null) {
                    $("#<%=spOPrice.ClientID%>").text("入力してください");
                    $("#<%=txtOPrice.ClientID%>").addClass("error");
                }
                else {
                    $("#<%=spOPrice.ClientID%>").text("");
                    $("#<%=txtOPrice.ClientID%>").removeClass("error");
                    $("#<%=txtOPrice.ClientID%>").val(price.replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                    IsValid = true;
                }
            });

            $("#<%=txtOAcc.ClientID%>").on("blur", function () {
                IsValid = false;
                var acc = $("#<%=txtOAcc.ClientID%>").val();
                if (acc == "" || acc.length == 0 || acc == null) {
                    $("#<%=txtOAcc.ClientID%>").addClass("error");
                    $("#<%=spOAcc.ClientID%>").text("入力してください");
                }
                else {
                    $("#<%=txtOAcc.ClientID%>").removeClass("error");
                    $("#<%=spOAcc.ClientID%>").text("");
                    IsValid = true;
                }
            });

            $("#<%=btnSave.ClientID%>").click(function (e) {
                e.preventDefault();
                if (confirm("保存します。よろしいですか？"))
                {
                    $("#<%=txtOAcc.ClientID%>").blur();
                    if (IsValid) {
                        $("#<%=txtOPrice.ClientID%>").blur();
                        if (IsValid && DateChecking(document.getElementById("<%=txtSD.ClientID%>")) && DateCompare()) {
                            var Id, Acc, Price, SD, ED;
                            Id = parseInt($("#<%=hdOutsourcingId.ClientID%>").val());
                            Acc = $("#<%=txtOAcc.ClientID%>").val();
                            Price = $("#<%=txtOPrice.ClientID%>").val();
                            SD = $("#<%=txtSD.ClientID%>").val();
                            ED = $("#<%=txtED.ClientID%>").val();
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "OutsourcingEntry.aspx/OutsourcingSave",
                                data: "{'intId':" + Id + ",'strAcc':'" + Acc + "','strPrice':'" + Price + "','strSD':'" + SD + "','strED':'" + ED + "'}",
                                datatype: "json",
                                error: function (req, msg, err) { },
                                success: function (msg) {
                                    window.alert(msg.d);
                                    window.location.href = "OutsourcingListing.aspx";
                                }
                            });
                        } else {
                            IsValid = false;
                            return;
                        }
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
                    <span class="font" style="font-size:16px; font-weight:bolder;color:white">外注作業費設定</span>
                </div>
            </div>
            <div style="padding-top:20px;">
                <table class="table-responsive" style="width:100%" >
                    <tr>
                        <td>
                            勘定科目
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOAcc" ></asp:TextBox>
                        </td>
                        <td>
                            <span id="spOAcc" runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td>
                            <asp:HiddenField runat="server" ID="hdOutsourcingId" />
                        </td>
                    </tr>
                    <tr>
                        <td>単価</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOPrice"></asp:TextBox>&nbsp;円
                        </td>
                        <td><span id="spOPrice" runat="server" style="color:#fc0a26;text-align:left;"></span></td>
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
                            <span id="spOStart" runat="server" style="color:#fc0a26;text-align:left;"></span>
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

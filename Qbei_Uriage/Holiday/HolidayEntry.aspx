<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="HolidayEntry.aspx.cs" Inherits="Qbei_Uriage.Holiday.HolidayEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form_date {
            border : 1px solid green;
            border-radius : 3px;
        }
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
        input, select , textarea {
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
                $("#<%=spHolidayDT.ClientID%>").text("入力してください");
                $("#<%=txtHolidayDT.ClientID%>").addClass("error");
                return;
            }
            else {
                $("#<%=spHolidayDT.ClientID%>").text("");
                $("#<%=txtHolidayDT.ClientID%>").removeClass("error");
            }
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

        $(document).ready(function () {
            $("#<%=btnSave.ClientID%>").click(function (e) {
                e.preventDefault();
                if (confirm("保存します。よろしいですか？"))
                {
                    var Id, HolDate, HolNm;
                    Id = parseInt($("#<%=hdHolidayId.ClientID%>").val());
                    HolDate = $("#<%=txtHolidayDT.ClientID%>").val();
                    HolNm = $("#<%=txtHolidayName.ClientID%>").val();
                    if (HolDate == "" || HolDate.length == 0 || HolDate == null) {
                        $("#<%=spHolidayDT.ClientID%>").text("入力してください");
                        $("#<%=txtHolidayDT.ClientID%>").addClass("error");
                        return;
                    }
                    else {
                        $("#<%=spHolidayDT.ClientID%>").text("");
                        $("#<%=txtHolidayDT.ClientID%>").removeClass("error");
                    }
                    if (HolNm == "" || HolNm.length == 0 || HolNm == null) {
                        $("#<%=spHNm.ClientID%>").text("入力してください");
                        $("#<%=txtHolidayName.ClientID%>").addClass("error");
                        return;
                    }
                    else {
                        $("#<%=txtHolidayName.ClientID%>").removeClass("error");
                        $("#<%=spHNm.ClientID%>").text("");
                    }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "HolidayEntry.aspx/HolidaySave",
                        data: "{'intId' : " + Id + ",'strDate' : '" + HolDate + "','strNm' :'"+HolNm+"'}",
                        datatype: "json",
                        error: function (req, msg, err) { alert(err); },
                        success: function (msg) {
                            window.alert(msg.d);
                            window.location.href = "HolidayListing.aspx";
                        }
                    });
                }
            });

            $("#<%=txtHolidayName.ClientID%>").on("blur", function () {
                var name = $("#<%=txtHolidayName.ClientID%>").val();
                if (name == "" || name.length == 0 || name == null) {
                    $("#<%=spHNm.ClientID%>").text("入力してください");
                    $("#<%=txtHolidayName.ClientID%>").addClass("error");
                }
                else {
                    $("#<%=spHNm.ClientID%>").text("");
                    $("#<%=txtHolidayName.ClientID%>").removeClass("error");
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
                    <span class="font" style="font-size:16px; font-weight:bolder;color:white">休業日設定</span>
                </div>
            </div>
            <div style="padding-top:20px;">
                <table class="table-responsive" style="width:100%;">
                    <tr>
                        <td class="col-md-3">
                            <asp:Label ID="Label2" runat="server" Text ="日付"></asp:Label>
                        </td>
                        <td class="col-md-3">
                            <div id="Div1" runat="server"  class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm dd" data-link-field="dtpSD" data-link-format ="yyyy-mm-dd">
                                <input id="txtHolidayDT" runat="server" type="text" maxlength="10" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm/dd" value=""/>
                                <span class="input-group-addon" style="background-color:white">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </td>
                        <td class="col-md-3">
                            <span runat="server" id="spHolidayDT" style="color:#fc0a26;text-align:left;"></span>                            
                        </td>
                        <td class="col-md-3">
                            <asp:HiddenField ID="hdHolidayId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="col-md-3">
                            <asp:Label ID="Label1" runat="server" Text ="休業日"></asp:Label>
                        </td>
                        <td class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHolidayName" ></asp:TextBox>
                        </td>
                        <td class="col-md-3">
                            <span runat="server" id="spHNm" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td class="col-md-3"></td>
                    </tr>
                </table>
<%--                <div class="row col-md-6 pad-adjust">
                     <asp:Label ID="Label2" runat="server" Text ="日付"></asp:Label>
                    <div id="Div1" runat="server"  class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm dd" data-link-field="dtpSD" data-link-format ="yyyy-mm-dd">
                        <input id="txtHolidayDT" runat="server" type="text" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm/dd" value=""/>
                        <span class="input-group-addon" style="background-color:white">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                    <span runat="server" id="spHolidayDT" style="color:#fc0a26;text-align:left;"></span>
                    <asp:HiddenField ID="hdHolidayId" runat="server" />
                </div>
                <div class="row col-md-6 pad-adjust" style="padding-top:20px;">
                    <asp:Label ID="Label1" runat="server" Text ="休業日"></asp:Label>
                    <asp:TextBox runat="server" ID="txtHolidayName" ></asp:TextBox>
                    <span runat="server" id="spHNm" style="color:#fc0a26;text-align:left;"></span>
                </div>--%>
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

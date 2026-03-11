<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="WorkingDaysEntry.aspx.cs" Inherits="Qbei_Uriage.WorkingDays.WorkingDaysEntry" %>
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
        function NumOnly(obj, e) {
            obj.value.replace(/[^0-9\.]/g, '');
            if (e.which < 48 || e.which > 57)
                e.preventDefault();
        }
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            isShift = false;

            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false && keyCode != 32) {
                if (txt.value.length == 4 && keyCode != 8) {
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
            if (!/^\d{4}\/\d{1,2}$/.test(data))
                return false;
            data = data.split("/");
            var month = parseInt(data[1], 10);
            var year = parseInt(data[0], 10);

            if (year < 1900 || year > 2500 || month < 1 || month > 12) {
                obj.value = "";
                return false;
            }
        }
        $(document).ready(function () {
            $("#<%=txtDays.ClientID%>").on("keypress keyup", function (e) {
                NumOnly(document.getElementById("<%=txtDays.ClientID%>"), e);
            });
            $("#<%=txtDays.ClientID%>").on("blur", function () {
                IsValid = false;
                var workdays = $("#<%=txtDays.ClientID%>").val();
                if (workdays == "" || workdays.length == 0 || workdays == "") {
                    $("#<%=txtDays.ClientID%>").addClass("error");
                    $("#<%=spDays.ClientID%>").text("");
                }
                else {
                    IsValid = true;
                    $("#<%=txtDays.ClientID%>").removeClass("error");
                    $("#<%=spDays.ClientID%>").text("");
                }
            });
            $("#<%=btnSave.ClientID%>").on("click", function (e) {
                e.preventDefault();
                if (confirm("保存します。よろしいですか？"))
                {
                    var param = new Array();
                    param[0] = $("#<%=hdWorkingDaysID.ClientID%>").val();
                    param[1] = $("#<%=txtYM.ClientID%>").val();
                    param[2] = $("#<%=ddlType.ClientID%>").val();
                    param[3] = $("#<%=txtDays.ClientID%>").val();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "WorkingDaysEntry.aspx/WorkingDaysSave",
                        data: JSON.stringify({ lstParam: param }),
                        datatype: "json",
                        error: function (req, msg, err) { },
                        success: function (msg) {
                            window.alert(msg.d);
                            window.location.href = "WorkingDaysListing.aspx";
                        }
                    });
                }
                else {
                    IsValid = false;
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
                    <span class="font" style="font-size:16px; font-weight:bolder;color:white">出勤日設定</span>
                </div>
            </div>
            <div style="padding-top:20px;">
                <table class="table-responsive" style="width:100%">
                    <tr>
                        <td>
                            年月
                        </td>
                        <td>
                                <div id="Div3" runat="server" class="input-group date form_date col-md-1" data-date="" data-date-format="yyyy mm" data-link-field="txtYM" data-link-format ="yyyy-mm">
                                    <input id="txtYM" runat="server" type="text" maxlength="7" onkeydown="return DateFormat(this,event.keyCode)" onblur="DateChecking(this)" class="form-control calendartxt" style="height:30px; width: 110px;cursor:pointer;background-color:white" placeholder="yyyy/mm" value=""/>
                                    <span class="input-group-addon " style="background-color:white"><span class="glyphicon glyphicon-calendar"></span></span>
                                </div>
                        </td>
                        <td>
                            <span id="spYM" runat="server" style="color:#fc0a26;text-align:left;"></span>
                        </td>
                        <td>
                            <asp:HiddenField runat="server" ID="hdWorkingDaysID" />
                        </td>
                    </tr>
                    <tr>
                        <td>部門</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlType" Height="30px"  CssClass="dropdown">
                                <asp:ListItem Text="経営企画室" Value="経営企画室"></asp:ListItem>
                                <asp:ListItem Text="ネット" Value="ネット"></asp:ListItem>
                                <asp:ListItem Text="実店舗" Value="実店舗"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>出勤日数</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDays"></asp:TextBox>
                        </td>
                        <td>
                            <span id="spDays"  runat="server" style="color:#fc0a26;text-align:left;"></span>
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
                       language: 'en',
                       format: 'yyyy/mm',
                       clearBtn: 1,
                       autoclose: 1,
                       startView: 3,
                       forceParse: 0,
                       minView: 3,
                       pickerPosition: "bottom-right"
                   });
    </script>
</asp:Content>

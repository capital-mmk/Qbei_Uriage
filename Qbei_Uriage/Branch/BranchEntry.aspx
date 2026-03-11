<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchEntry.aspx.cs" MasterPageFile="~/Uriage.Master" Inherits="Qbei_Uriage.Branch.BranchEntry" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    
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
    function NumOnly(obj, e) {
        obj.value.replace(/[^0-9\.]/g, '');
        if (e.which < 48 || e.which > 57)
            e.preventDefault();
    }
    function IsBlank(ctl, ctlMsg)
    {
        var ctlValue = $(ctl).val();
        if (ctlValue == "" || ctlValue.length == 0 || ctlValue == null) {
            $(ctl).addClass("error");
            $(ctlMsg).text("入力してください");

            return true;
        }
        else {
            $(ctl).removeClass("error");
            $(ctlMsg).text("");

            return false;
        }
    }

    $(document).ready(function () {
        $("#<%=txtBranchCode.ClientID%>").on("keypress keyup", function (e) {
            NumOnly(document.getElementById("<%=txtBranchCode.ClientID%>"), e);
        });
        $("#<%=txtBranchCode.ClientID%>").on("blur", function () {
            IsBlank($("#<%=txtBranchCode.ClientID%>"), $("#<%=msgBranchCode.ClientID%>"));
        });
        $("#<%=txtBranchName.ClientID%>").on("blur", function () {
            IsBlank($("#<%=txtBranchName.ClientID%>"), $("#<%=msgBranchName.ClientID%>"));
        });
        $("#<%=txtBrandShortName.ClientID%>").on("blur", function () {
            IsBlank($("#<%=txtBrandShortName.ClientID%>"), $("#<%=msgBrandShortName.ClientID%>"));
        });
        $("#<%=txtStoreCategory.ClientID%>").on("blur", function () {
            IsBlank($("#<%=txtStoreCategory.ClientID%>"), $("#<%=msgStoreCategory.ClientID%>"));
        });

        $("#<%=btnSave.ClientID%>").click(function (e) {
            e.preventDefault();
            if (confirm("保存します。よろしいですか？")) {
                
                var isBlank = IsBlank($("#<%=txtBranchCode.ClientID%>"), $("#<%=msgBranchCode.ClientID%>"));
                isBlank = IsBlank($("#<%=txtBranchName.ClientID%>"), $("#<%=msgBranchName.ClientID%>"));
                isBlank = IsBlank($("#<%=txtBrandShortName.ClientID%>"), $("#<%=msgBrandShortName.ClientID%>"));
                isBlank = IsBlank($("#<%=txtStoreCategory.ClientID%>"), $("#<%=msgStoreCategory.ClientID%>"));
                if (!isBlank)
                {                
                    var param = new Array();
                    param[0] = $("#<%=txtBranchCode.ClientID%>").val();
                    param[1] = $("#<%=txtBranchName.ClientID%>").val();
                    param[2] = $("#<%=txtBrandShortName.ClientID%>").val();
                    param[3] = $("#<%=txtStoreCategory.ClientID%>").val();
                    param[4] = $("#<%=txtSummary.ClientID%>").val();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "BranchEntry.aspx/BranchSave",
                        data: JSON.stringify({ lstParam: param }),
                        datatype: "json",
                        error: function (req, msg, err) { },
                        success: function (msg) {
                            var alertMsg;
                            if (msg.d == "0") {
                                alert("支店コードは既に存在しています");
                                $("#<%=txtBranchCode.ClientID%>").focus();
                            }
                            else {
                                window.alert(msg.d);
                                window.location.href = "Branch.aspx";
                            }
                        }
                    });
                  }
             }
        });
    });
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">   

    <div class="" align="center" >
 <div class="panel-heading" style="width:70%;border-radius:15px;height:500px;background-color:#ffffff;">
     
     <div class="row">
         <div style="background-color:#395587;    height: 40px; vertical-align:middle; line-height:40px;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    margin-top: -9px;
    background-color: #395587;">

          <span class="font" style="font-size:16px; font-weight:bolder;color:white">支店設定</span>
             
         </div>
         <br />
     </div>
     <div>
        <table class="table-responsive" style="width:100%">
            <tr>
                <td>
                    支店コード
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBranchCode"></asp:TextBox>                           
                </td>
                <td>
                    <span id="msgBranchCode" runat="server" style="color:#fc0a26;text-align:left;"></span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    支店名
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBranchName"></asp:TextBox>                           
                </td>
                <td>
                    <span id="msgBranchName" runat="server" style="color:#fc0a26;text-align:left;"></span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>支店名略称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtBrandShortName"></asp:TextBox>
                </td>
                <td>
                    <span id="msgBrandShortName"  runat="server" style="color:#fc0a26;text-align:left;"></span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>店舗区分</td>
                <td>
                    <asp:TextBox runat="server" ID="txtStoreCategory"></asp:TextBox>
                </td>
                <td>
                    <span id="msgStoreCategory" runat ="server" style="color:#fc0a26;text-align:left;"></span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>集計部門</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSummary"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
     </div>
     <div class="row col-md-6 pad-adjust" style="padding-top:50px;">
        <asp:Button ID="btnSave" CssClass="btn btn-info" runat="server" Text="登録"  />
     </div>
</div>
</div>
  
</asp:Content>

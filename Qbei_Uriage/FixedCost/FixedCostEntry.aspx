<%@ Page Title="" Language="C#" MasterPageFile="~/Uriage.Master" AutoEventWireup="true" CodeBehind="FixedCostEntry.aspx.cs" Inherits="Qbei_Uriage.FixedCost.FixedCostEntry" %>
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
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnSave.ClientID%>").click(function (e) {
                e.preventDefault();
                if (confirm("保存します。よろしいですか？")) {
                    var Id, Acc, Shop;
                    if ($("#<%=hdFixedId.ClientID%>").val() == "0")
                        Id = 0;
                    else
                        Id = parseInt($("#<%=hdFixedId.ClientID%>").val());
                    Acc = $("#<%=txtFAcc.ClientID%>").val();
                    Shop = $("#<%=ddlShopCategory.ClientID%>").val(); 
                    if (Acc == "" || Acc.length == 0 || Acc == null) {
                        $("#<%=spAcc.ClientID%>").text("入力してください");
                        $("#<%=txtFAcc.ClientID%>").addClass("error");
                        return;
                    }
                    else {
                        $("#<%=txtFAcc.ClientID%>").removeClass("error");
                        $("#<%=spAcc.ClientID%>").text("");
                    }
                    if (Shop == 0) {
                        $("#<%=spShop.ClientID%>").text("入力してください");
                        $("#<%=ddlShopCategory.ClientID%>").addClass("error");
                        return;
                    }
                    else {
                        $("#<%=ddlShopCategory.ClientID%>").removeClass("error");
                        $("#<%=spShop.ClientID%>").text("");
                    }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "FixedCostEntry.aspx/FixedCostSave",
                        data: "{'intId' : " + Id + ",'strName' : '" + Acc + "','strShop' : '" + Shop + "'}",
                        datatype: "json",
                        error: function (req, msg, err) { alert(err); },
                        success: function (msg) {
                                window.alert(msg.d);
                                window.location.href = "FixedCostListing.aspx";
                        }
                        });
                    }
            });

            $("#<%=txtFAcc.ClientID%>").on("blur", function () {
                var account = $("#<%=txtFAcc.ClientID%>").val();
                if (account == "" || account.length == 0 || account == null) {
                    $("#<%=spAcc.ClientID%>").text("入力してください");
                    $("#<%=txtFAcc.ClientID%>").addClass("error");
                }
                else {
                    $("#<%=txtFAcc.ClientID%>").removeClass("error");
                    $("#<%=spAcc.ClientID%>").text("");
                }
            });

            $("#<%=ddlShopCategory.ClientID%>").change(function () {
                var shop = $("#<%=ddlShopCategory.ClientID%>").val();
                if (shop == 0) {
                    $("#<%=ddlShopCategory.ClientID%>").addClass("error");
                    $("#<%=spShop.ClientID%>").text("入力してください");
                }
                else {
                    $("#<%=ddlShopCategory.ClientID%>").removeClass("error");
                    $("#<%=spShop.ClientID%>").text("");
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  align="center">
        <div class="panel-heading" style="width:70%;border-radius:15px;height:500px;background-color:#ffffff;">
            <div class="row">
                <div style="background-color:#395587;height: 40px; vertical-align:middle; line-height:40px;
                    border-top-left-radius: 10px;border-top-right-radius: 10px;margin-top: -9px;background-color: #395587;">
                    <span class="font" style="font-size:16px; font-weight:bolder;color:white">固定費設定</span>
                </div>
            </div>
            <div class="col-md-12"  style="padding-top:20px;">
                <div class="row" >
                    <asp:Label ID="Label1" runat="server" CssClass ="col-md-3" Text ="勘定科目"></asp:Label>
                    <asp:TextBox runat="server" ID="txtFAcc" CssClass="col-md-3"></asp:TextBox>
                    <span runat="server" id="spAcc" class="col-md-3" style="color:#fc0a26;text-align:left;"></span>
                    <asp:HiddenField ID="hdFixedId" runat="server" />
                </div>
                <div class="row" style ="padding-top:20px;" >
                    <asp:Label ID="Label2" runat="server" CssClass="col-md-3" Text="部門" ></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlShopCategory" AppendDataBoundItems="true" CssClass="col-md-3 dropdown dropdown-toggle">
                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="spShop" runat="server" class="col-md-3" style="color:#fc0a26;text-align:left;"></span>
                </div>
            </div>
            <div class="row col-md-6 pad-adjust" style="padding-top:50px;">
                <asp:Button ID="btnSave" CssClass="btn btn-info" runat="server" Text="登録"  />
            </div>
        </div>
    </div>
</asp:Content>

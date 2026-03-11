<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hello.aspx.cs" Inherits="Qbei_Uriage.Login.hello" MasterPageFile="~/Uriage.Master" %>
<asp:Content ID="header" runat="server" ContentPlaceHolderID="head">

    <style>
        .headers
        {
            position:absolute;
            }
    </style>
</asp:Content>
<asp:Content runat="server" ID="d" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="height:200px; overflow:auto" align="center">

    <asp:GridView ID="gvDistricts" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDistricts_RowDataBound" AllowPaging="true" AllowSorting="true" ShowHeaderWhenEmpty="true" >  
       <HeaderStyle CssClass="headers" />
        <Columns>
            <asp:TemplateField HeaderText="ID"  HeaderStyle-Width="80px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" 

                    Text='<%#Eval("Sales_Date")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Preferences"  HeaderStyle-Width="120px" ItemStyle-Width="120px">
                <ItemTemplate>
                    <asp:Label ID="lblDistName" runat="server" 

                    Text='<%#Eval("Cancel_Date")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Description" 

            HeaderStyle-Width="200px" ItemStyle-Width="200px">
                <ItemTemplate>
                    <asp:Label ID="lblDistDesc" runat="server" 

                    Text='<%#Eval("OrderNo")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView> 
            
        </div>
 </asp:Content>
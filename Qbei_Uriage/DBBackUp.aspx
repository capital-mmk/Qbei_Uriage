<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBBackUp.aspx.cs" Inherits="Qbei_Uriage.DBBackUp" %>

<!DOCTYPE html>
<script type="text/javascript">
    $(function () {
        $('#btnBackup').click(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DB_Backup.aspx/BackupUriage",
                dataType: "json",
                success: function (data) {
                    var obj = data.d;
                    if (obj == 'true') {
                        alert("Backuped Successfully");
                    }
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }
        )
    });
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
      
          <%--  <table>
                <tr>
<td>
    <input type = "button" id="btnBackup" value="Backup" />
    
</td>
</tr>
<tr>
<td>
<asp:Label runat="server" ID="lblMessage"></asp:Label>
</td>
<td>
    &nbsp;</td>
</tr>
</table>--%>


                                      <div align="center" style="margin-top:10%"> 
                                   <input runat="server" type = "button" id="btnBackup" value="Backup"   onserverclick="btn_bkp"/>
                                              <asp:Label runat="server" ID="lbl_meg" >Backup was finished successfully</asp:Label>  
                                   
                                      </div>
    </form>
</body>
</html>

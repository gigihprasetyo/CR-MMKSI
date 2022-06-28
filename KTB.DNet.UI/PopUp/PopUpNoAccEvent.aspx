<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpNoAccEvent.aspx.vb" Inherits=".PopUpNoAccEvent" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>BABIT - Pencarian Accrued</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">
        function GetSelectedBabitEvent() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgAccrued");
            var BabitEventInfo = '';
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        //BabitEventInfo = replace(table.rows[i].innerText, ' ', '') + ";" + replace(table.rows[i].innerText, ' ', '');
                        BabitEventInfo = table.rows[i].getElementsByTagName("span")[0].innerHTML + ";" + table.rows[i].getElementsByTagName("span")[1].innerHTML;
                        window.returnValue = BabitEventInfo;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        //BabitEventInfo = replace(table.rows[i].innerText, ' ', '') + ";" + replace(table.rows[i].innerText, ' ', '');
                        BabitEventInfo = table.rows[i].getElementsByTagName("span")[0].innerHTML + ";" + table.rows[i].getElementsByTagName("span")[1].innerHTML;
                        opener.dialogWin.returnFunc(BabitEventInfo);
                        bcheck = true;
                    }
                    else{
                        BabitEventInfo = table.rows[i].getElementsByTagName("span")[0].innerHTML + ";" + table.rows[i].getElementsByTagName("span")[1].innerHTML;
                        opener.dialogWin.returnFunc(BabitEventInfo);
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih No. Accrued");
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">BABIT - Pencarian Accrued</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" alt="" src="../images/dot.gif" border="0" /></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Accrued Key</td>
                            <td style="height: 10px" width="1%">:</td>
                            <td style="width: 215px; height: 10px" width="215">
                                <asp:TextBox ID="txtAccKey" runat="server" Width="192px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Description</td>
                            <td style="height: 10px" width="1%">:</td>
                            <td style="width: 215px; height: 10px" width="215">
                                <asp:TextBox ID="txtDesc" runat="server" Width="192px" />
                            </td>
                        </tr>
                        <tr>

                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px">&nbsp;</td>
                            <td valign="bottom" style="width: 225px; height: 13px;">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                        </tr>
                        <tr>
                            <td>
                                <span style="height: 20px"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgAccrued" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="#F7F7F7" BackColor="#4A3C8C" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Accrued Key">
                                                <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccKey" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Deskripsi">
                                                <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedBabitEvent()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMRTCHead.aspx.vb" Inherits=".PopUpMRTCHead" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Popup Head MRTC</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <base target="_self">
    <script language="javascript">
        function GetSelectedUser() {
            var table;
            var bcheck = false;
            table = document.getElementById('dtgUserInfo');
            var User = '';
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer" || navigator.appName == "Netscape") {
                        User = table.rows[i].cells[1].innerText + '-' + table.rows[i].cells[2].innerText;
                        break;
                    }
                    else {
                        User = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + '-' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
                        break;
                    }
                }
            }

            if (navigator.appName == "Microsoft Internet Explorer") {
                window.returnValue = User;
                bcheck = true;
            }
            else {
                window.opener.dialogWin.returnFunc(User);
                bcheck = true;
            }

            if (bcheck) {
                window.close();
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Head MRTC</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                         <tr>
                            <td class="titleField" width="24%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtDealerCode" onblur="omitSomeCharacter('txtEmail','<>?*%$;')"
                                    runat="server" Width="248px" MaxLength="50" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Nama Trainee</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtName" onblur="omitSomeCharacter('txtUserName','<>?*%$;')"
                                    runat="server" Width="248px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                       
                        <tr>
                            <td class="titleField" width="24%"></td>
                            <td width="1%"></td>
                            <td width="75%">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="64px"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" colspan="3">
                    <div id="div1" style="overflow: auto; height: 330px">
                        <asp:DataGrid ID="dtgUserInfo" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
                            CellPadding="3" GridLines="None" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
                                BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%-- <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn SortExpression="ID" HeaderText="ID Trainee">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Name" HeaderText="Nama Trainee">
                                    <HeaderStyle Width="35%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTraineeName"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDealerCode"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40" align="center">&nbsp;<input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedUser()" type="button"
                    value="Pilih" name="btnChoose" runat="server"><input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel"></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>

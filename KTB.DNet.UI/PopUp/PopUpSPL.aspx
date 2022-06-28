<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSPL.aspx.vb" Inherits="PopUpSPL" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUp Pencarian SPL</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <%--<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>--%>
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript">></script>
    <base target="_self">
    <script language="javascript" type="text/javascript">

        function GetSelectedSPL() {
            var table;
            var bcheck = false;
            var SPL = '';
            table = document.getElementById("dgSPLHeader");
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        SPL = table.rows[i].cells[1].innerText;
                        window.returnValue = SPL;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        SPL = table.rows[i].cells[1].innerText;
                        try {
                            opener.dialogWin.returnFunc(SPL);
                        } catch (e) {
                            var a1 = e;
                            try {
                                window.opener.SPLSelection(SPL);
                            }
                            catch (err) {
                                var a2 = err;
                            }
                            window.close();

                        }
                        bcheck = true;
                    }
                    else {
                        SPL = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;

                        opener.dialogWin.returnFunc(SPL);
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer") {
                    if (opener && !opener.closed) {
                        window.opener.dialogWin.returnFunc(SPL);
                    }
                }
            }
            else {
                alert("Silahkan pilih SPL");
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">SPL&nbsp;- Pencarian SPL</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 17px" width="24%">
                                <asp:Label ID="lblSPLNumber" runat="server">Nomor SPL</asp:Label></td>
                            <td style="height: 17px" width="1%">
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td style="height: 17px" width="25%">
                                <asp:TextBox ID="txtSPLNumber" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                    onblur="omitSomeCharacter('txtSPLNumber','<>?\*%$');"></asp:TextBox></td>
                            <td class="titleField" style="height: 17px" width="20%">
                                <asp:Label ID="lblCustName" runat="server">Nama Customer</asp:Label></td>
                            <td style="height: 17px" width="1%">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td style="height: 17px" width="29%">
                                <asp:TextBox ID="txtCustName" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                    onblur="omitSomeCharacter('txtCustName','<>?\/*%$');"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 10px" width="24%">
                                <asp:Label ID="lblName" runat="server">Nama Dealer</asp:Label></td>
                            <td style="height: 10px" width="1%">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td style="height: 10px" width="25%">
                                <asp:TextBox ID="txtDealerName" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                    onblur="omitSomeCharacter('txtCustName','<>?\/*%$');"></asp:TextBox></td>
                            <td class="titleField" style="height: 10px" width="20%"></td>
                            <td style="height: 10px" width="1%"></td>
                            <td style="height: 10px" width="29%">
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 330px">
                                    <asp:DataGrid ID="dgSPLHeader" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
                                        BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="radioBtn" runat="server"></asp:RadioButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="SPLNumber" HeaderText="Nomor SPL">
                                                <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblSPLNumber">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
                                                <HeaderStyle Width="50%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="CustomerName" SortExpression="CustomerName" HeaderText="Nama Customer">
                                                <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="7" align="center">
                    <input id="btnChoose" style="width: 60px" disabled="disabled" onclick="GetSelectedSPL()" type="button" value="Pilih"
                        name="btnChoose" runat="server">
                    <asp:Button ID="btnPilih" runat="server" Text="Pilih" Width="60px" Visible="false"></asp:Button>
                    &nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel"></td>
            </tr>
        </table>
    </form>
</body>
</html>

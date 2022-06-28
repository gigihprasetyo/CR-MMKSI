<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMPMaster.aspx.vb" Inherits="PopUpMPMaster" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUpMPMaster</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <base target="_self">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function trim(str) {
            if (!str || typeof str != 'string')
                return null;

            return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
        }
        function GetSelectedMP() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgMPMaster");
            var MPMaster = '';
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        goodno = trim(table.rows[i].cells[1].innerText) + ";" + trim(table.rows[i].cells[2].innerText);
                        window.returnValue = goodno;
                        bcheck = true;
                        break;
                    }
                    else if (navigator.appName == "Netscape") {
                        goodno = trim(table.rows[i].cells[1].innerText) + ";" + trim(table.rows[i].cells[2].innerText);
                        opener.dialogWin.returnFunc(MPMaster);
                        bcheck = true;
                        break;
                    }
                    else {
                        if (MPMaster == '') {
                            MPMaster = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        window.close();
                        opener.dialogWin.returnFunc(MPMaster);
                        bcheck = true;
                        break;
                    }
                }
            }

            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Material Promotion");
            }
        }

        function ClosePopUp() {
            window.close();
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titlePage" style="height: 21px">Material Promotion - Master</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="91" style="width: 91px">Kode&nbsp;Barang</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtNoBarang" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoBarang','<>?*%$;')"
                                    runat="server" Width="230px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="91" style="width: 91px">Nama Barang</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtNmBarang" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNmBarang','<>?*%$;')"
                                    runat="server" Width="228px"></asp:TextBox></td>
                            <tr>
                                <td class="titleField" width="91" style="width: 91px"></td>
                                <td width="1%"></td>
                                <td width="75%">
                                    <asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari"></asp:Button></td>
                            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgMPMaster" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
                            BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                            AllowCustomPaging="True" AllowPaging="True" PageSize="25">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        &nbsp;Pilih
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="radio" id="x" name="y" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="GoodNo" HeaderText="Nomor Barang">
                                    <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoBarang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GoodNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Price" HeaderText="Nama Barang">
                                    <HeaderStyle Width="50%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNmBarang" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Unit" HeaderText="Satuan">
                                    <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSatuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Unit") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br>
                    <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedMP()" type="button"
                        value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                            name="btnCancel">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

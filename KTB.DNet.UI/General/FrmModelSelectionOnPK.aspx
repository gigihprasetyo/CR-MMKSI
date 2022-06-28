<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmModelSelectionOnPK.aspx.vb" Inherits="FrmModelSelectionOnPK" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pilihan Kode Tipe</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">

        function GetSelectedModel() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgVechileType");
            var Model = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[1].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Model == '') {
                            Model = replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        else {
                            Model = Model + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
                        }
                        window.returnValue = Model;
                        bcheck = true;
                    }
                    else {
                        if (Model == '') {
                            Model = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            Model = Model + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(Model); }
            }
            else {
                alert("Silahkan Pilih Tipe Kendaraan terlebih dahulu");
            }
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table11" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td height="20">&nbsp;<b>PESANAN KENDARAAN&nbsp;- Kode Tipe</b></td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" height="1">
                                <img height="1" src="../images/bg_hor_sales.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td class="titleField" style="height: 13px; width: 120px">Kode Tipe Kendaraan</td>
                                        <td width="1%">:</td>
                                        <td width="25%">
                                            <asp:TextBox ID="txtKodeTipe" Width="100px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"></asp:TextBox></td>
                                        <td style="width: 17px; height: 13px"></td>
                                        <td class="titleField" style="height: 13px"></td>
                                        <td style="height: 13px"></td>
                                        <td style="height: 13px"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 13px">Deskripsi Kendaraan</td>
                                        <td style="height: 13px">:</td>
                                        <td style="height: 13px">
                                            <asp:TextBox ID="txtDeskripsi" Width="250px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;')" runat="server"></asp:TextBox></td>
                                        <td style="width: 17px; height: 13px"></td>
                                        <td class="titleField" style="height: 13px"></td>
                                        <td style="height: 13px"></td>
                                        <td style="height: 13px"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 15px"></td>
                                        <td style="height: 15px"></td>
                                        <td style="height: 15px">
                                            <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                                        <td style="width: 17px; height: 15px"></td>
                                        <td style="height: 15px"></td>
                                        <td style="height: 15px"></td>
                                        <td style="height: 15px"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgVechileType" runat="server" Width="100%" BorderColor="#CDCDCD" BorderWidth="0px"
                                        BackColor="#CDCDCD" CellPadding="2" GridLines="None" CellSpacing="1" ForeColor="Black" AutoGenerateColumns="False">
                                        <SelectedItemStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
                                        <FooterStyle BackColor="Tan"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="VechileTypeCode" HeaderText="Tipe" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Description" HeaderText="Deskripsi">
                                                <HeaderStyle Width="80%" CssClass="titleTableSales"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderStyle-Width="0px">
                                                <HeaderStyle Width="0px"  CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Width="0px"></ItemStyle>
                                                <ItemTemplate>
                                                   <asp:Label id=lblVechileTypeID runat="server" Width="0px" style="display:none" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
											        </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="PaleGoldenrod"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedModel()"
                                    type="button" value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

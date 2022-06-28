<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TOPPopUpTransferActual.aspx.vb" Inherits=".TOPPopUpTransferActual" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUpTransferActual</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">

        function GetSelectedCustomer() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgCustomerSelection");
            var Customer = '';
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        Customer = replace(table.rows[i].cells[1].innerText, ' ', '');
                        window.returnValue = Customer;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        Customer = replace(table.rows[i].cells[1].innerText, ' ', '');
                        opener.dialogWin.returnFunc(Customer);
                        bcheck = true;
                    }

                    else {
                        Customer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        opener.dialogWin.returnFunc(Customer);
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan Pilih Customer terlebih dahulu");
            }
        }


    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">Detail Transfer Actual</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label runat="server" ID="lblDeaelerCode"></asp:Label>
                            </td>
                            <td style="width: 17px" width="2%"></td>
                            <td class="titleField" style="height: 20px">Total Amount</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <asp:Label runat="server" ID="lblTOtalAmount"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 20px">Nama Dealer</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <asp:Label runat="server" ID="lblNamaDealer"></asp:Label></td>
                            <td style="width: 17px; height: 20px"></td>
                            <td class="titleField" style="height: 20px">Total Transfer</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <asp:Label runat="server" ID="lblTotalTransfer"></asp:Label></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="height: 20px">Nomor Reg</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <asp:Label runat="server" ID="lblNoreg"></asp:Label></td>
                            <td style="width: 17px; height: 20px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>


                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 310px">
                                    <asp:DataGrid ID="dtgCustomerSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        llowPaging="False" AllowSorting="False" OnItemDataBound="dtgCustomerSelection_ItemDataBound">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle ForeColor="White"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Ref Bank">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRefbank" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Posting Date">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPostingDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Transfer Amount">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCODOutstanding.aspx.vb" Inherits=".FrmCODOutstanding" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmCODOutstanding</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {

            showPopUp('../PopUp/PopUpDealerSelectionTOP.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealer = document.getElementById("txtKodeDealer");
            txtDealer.value = selectedDealer;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">TOP Spare Part -&nbsp; COD Outstanding</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td align="left">
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td><strong>Kode Dealer</strong></td>
                                <td>:</td>
                                <td>&nbsp;
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server" Width="108px"></asp:TextBox><asp:Label ID="lblDealerCodePopUp" onclick="ShowPPDealerSelection();" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Billing Number</td>
                                <td>:</td>
                                <td>&nbsp;
                                    <asp:TextBox ID="txtBillingNumber" Width="150px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Delivery Order</td>
                                <td>:</td>
                                <td>&nbsp;
                                    <asp:TextBox ID="txtDeliveryOrder" Width="150px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" valign="top">Tanggal Billing</td>
                                <td valign="top">:</td>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <cc1:IntiCalendar ID="icBillingDateFrom" runat="server"></cc1:IntiCalendar></td>
                                            <td>s/d</td>
                                            <td>
                                                <cc1:IntiCalendar ID="icBillingDateTo" runat="server"></cc1:IntiCalendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td colspan="2" style="align-content: center; align-items: center; align-self: center">
                                    <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <div id="div1" style="overflow: auto; height: 280px">
                            <asp:DataGrid ID="dtgARList" runat="server" Width="100%" AllowSorting="True" BorderWidth="0px"
                                BorderColor="Gainsboro" BackColor="#CDCDCD" CellSpacing="1" CellPadding="3" AutoGenerateColumns="False"
                                AllowCustomPaging="True" PageSize="10" AllowPaging="True" ShowFooter="false">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn ReadOnly="True" HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Kode Dealer">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDealerCodeGrid"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tipe Pembayaran">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTipePembayaran"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Nomor DO">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNomorDO"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Nomor Billing">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNomorBilling"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tanggal Billing">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTglBilling"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tanggal Pembuatan">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTglPembuatan"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Net Amount">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNetAmount"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tax Amount">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTaxAmount"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="C2 Amount">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblC2Amount"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Total">
                                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnDnload" runat="server" Text="Download"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

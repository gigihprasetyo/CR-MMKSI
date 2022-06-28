<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTransferCeiling.aspx.vb" Inherits=".FrmTransferCeiling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="0; url=FrmTransferCeilingReport.aspx" />

    <title>Transfer Ceiling</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script>

        function ShowPPAccountSelection() {
            showPopUp('../General/../PopUp/PopUpCreditAccountSelection2.aspx', '', 500, 760, AccountSelection2);
        }

        function AccountSelection2(selectedAccount) {
            var tempParam = selectedAccount;
            var txtAccountSelection = document.getElementById("txtCreditAccount");
            txtAccountSelection.value = selectedAccount;
        }

    </script>
</head>
<body ms_positioning="GridLayout" style="margin: 5px;">
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						<asp:Label ID="lblTitle" runat="server">TRANSFER - Status Ceiling Pembayaran Transfer</asp:Label></td>
                </tr>
                <tr style="height: 1px;">
                    <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="300px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr>
                                <td><strong>Product Category</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <div style="display: none">
                                        <asp:TextBox runat="server" ID="txtID" Visible="false"></asp:TextBox>
                                    </div>
                                    <asp:DropDownList ID="ddlProductCategory" runat="server" Width="160px"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Credit Account</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtCreditAccount" 
                                        onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>

                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Tipe Pembayaran</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" Width="160px"></asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Tgl Report</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <cc1:IntiCalendar id="calEffectiveStart" runat="server"></cc1:IntiCalendar></td>
                                            <td width="1px"></td>
                                            <td>
                                                <cc1:IntiCalendar id="calEffectiveEnd" runat="server"></cc1:IntiCalendar></td>
                                        </tr>

                                    </table>

                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <div id="divHidden" style="overflow: auto; width: 100%; height: 290px">
                                        <asp:DataGrid ID="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                            AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                            DataKeyField="ID" ShowFooter="True">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle ForeColor="White" Width="40px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                                        <div style="display: none;">
                                                            <asp:Label runat="server" ID="lblID" Visible="false"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                
                                                <asp:TemplateColumn HeaderText="Kategori Produk" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblProductCategory"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Credit Account" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCreditAccount"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Tipe Pembayaran" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Tgl Report">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEffectiveDate"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Available Ceiling">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAvailableCeiling"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6"></td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

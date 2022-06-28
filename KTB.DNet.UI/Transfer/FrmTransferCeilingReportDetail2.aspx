<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>


<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTransferCeilingReportDetail2.aspx.vb" Inherits=".FrmTransferCeilingReportDetail2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Ceiling Report</title>
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
    <form id="form2" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						<asp:Label ID="lblTitle" runat="server">TRANSFER - Report Ceiling Pembayaran Transfer</asp:Label></td>
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
                                <td width="1300px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Product Category</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <div style="display: none">
                                        <asp:TextBox runat="server" ID="txtID" Visible="false"></asp:TextBox>
                                    </div>
                                    <asp:DropDownList ID="ddlProductCategory" runat="server" Width="160px" Enabled="false" ></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Credit Account</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" Enabled="false" ID="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>

                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Tipe Pembayaran</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" Width="160px" Enabled="false"></asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Tgl Report</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <cc1:IntiCalendar id="calEffective" runat="server" Enabled="false"></cc1:IntiCalendar>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Plafon</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPlafon" text="0" Enabled="false" style="text-align:right;" Width="148px" ></asp:TextBox>
                                </td>
                                <td><b>Available Ceiling</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAvailableCeiling" text="0" Enabled="false" style="text-align:right;" Width="148px" ></asp:TextBox>
                                </td>
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
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="SO" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSONumber"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalSO"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Payment" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumber"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPayment"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Outsanding PO" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPONumber"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPO"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="+" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmountPlus"></asp:Label>
                                                     </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPlus"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="-" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmountMinus"></asp:Label>
                                                     </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalMinus"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>

                                        <asp:Button runat="server" ID="btnBack" text="Kembali" />
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

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPSPList.aspx.vb" Inherits=".FrmTOPSPList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Daftar Pembayaran Transfer</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script>
        function DummyFunc() {

        }

        function showPopupEdit() {
            showPopUp("../TOPSparepart/FrmTOPSPTransferPaymentDaftar.aspx?Mode=Ngedit&DetailID=" & ID.ToString(), '', 9500, 760, TOPSPDaftar)
        }
        function TOPSPDaftar(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionTOP.aspx?Mode=Dealer', '', 500, 760, DealerTOPSelection);
        }
        function DealerTOPSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPBillingSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionTOP.aspx?Mode=Billing', '', 500, 760, BillingTOPSelection);
        }
        function BillingTOPSelection(selectedBilling) {
            var txtBillingSelection = document.getElementById("txtNoKodeBilling");
            txtBillingSelection.value = selectedBilling;
        }

        function ShowPPCreditAccountSelection() {
            showPopUp('../PopUp/PopUpCreditAccountSelection2.aspx', '', 500, 760, CreditAccountTOPSelection);
        }
        function CreditAccountTOPSelection(selectedCreditAccount) {
            var txtCreditAccountSelection = document.getElementById("txtCreditAccount");
            txtCreditAccountSelection.value = selectedCreditAccount;
        }
    </script>

</head>
<body>

    <form id="form2" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						TOP Sparepart - Daftar Pembayaran</td>
                </tr>
                <tr style="height: 1px;">
                    <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="200px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="200px"></td>
                                <td width="1px"></td>
                                <td width="200px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr>
                                <td><strong>Credit Account</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <div runat="server" id="divDealer" visible="false">
                                        <asp:Label ID="lblCreditAccount" runat="server"></asp:Label>
                                    </div>
                                    <div runat="server" id="divMKS" visible="false">
                                        <asp:TextBox onblur="omitSomeCharacter('txtCreditAccount','<>?*%$')" ID="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            runat="server"></asp:TextBox><asp:Label ID="lblPopCreditAccount" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                    </div>
                                </td>
                                <td><b>No Reg Pemercepat</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtRegPemercepat"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><b>Kode Dealer Pembayaran</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealerPembayaran" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
                                <td><b>No Reg Dipercepat</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtRegDipercepat"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Kode Dealer Billing</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtNoKodeBilling','<>?*%$')" ID="txtNoKodeBilling" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchDealerBilling" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                    <asp:Label runat="server" ID="lblGroup" style="display:none"></asp:Label>
                                </td>
                                <td><b>No Billing</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNoBilling"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Tgl Transfer</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <table border="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkPlanTransferDate" Text="" />
                                            </td>
                                            <td>
                                                <cc1:IntiCalendar ID="calPlanTransferDateStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            <td>&nbsp;s.d&nbsp;</td>
                                            <td>
                                                <cc1:IntiCalendar ID="calPlanTransferDateEnd" runat="server" TextBoxWidth="70" CanPostBack="False"></cc1:IntiCalendar></td>
                                        </tr>
                                    </table>
                                </td>
                                <td><b>
                                    <asp:Label runat="server" ID="lblStatus" Text="Status" />
                                </b></td>
                                <td><b>
                                    <asp:Label runat="server" ID="lblStatusSeparator" Text=":" /></b></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlStatus"></asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>Tgl&nbsp; Billing</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <table border="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkDueDate" Text="" />
                                            </td>
                                            <td>
                                                <cc1:IntiCalendar ID="calDueDateStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            <td>&nbsp;s.d&nbsp;</td>
                                            <td>
                                                <cc1:IntiCalendar ID="calDueDateEnd" runat="server" TextBoxWidth="70" CanPostBack="False"></cc1:IntiCalendar></td>
                                        </tr>
                                    </table>
                                </td>
                                <td><strong>Total Amount</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:Label ID="lblTAmount" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><b>No Reg Permbayaran</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNoRegPermbayaran"></asp:TextBox>
                                </td>
                                <td><b>Amount</b></td>
                                <td><b>:</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAmount"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>                                
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="70px"/>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div id="divHidden" style="overflow: auto; width: 100%; height: 290px">
                                        <asp:DataGrid ID="dtgMain" runat="server"   
                                            Width="100%" CellSpacing="1" GridLines="Horizontal"
                                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                            AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="true"
                                            DataKeyField="ID" ShowFooter="false" >
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
                                                        <asp:HiddenField runat="server" ID="hdnID" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
                                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Credit Account" SortExpression="Dealer.CreditAccount">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCreditAcc"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDealerCode"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Rencana Transfer" SortExpression="TransferPlanDate">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPlanTransferDate"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Hari Ini">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDate"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Jatuh Tempo" SortExpression="DueDate">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDueDate"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Keterlambatan Hari" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDueDateLeft"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Reg" SortExpression="RegNumber">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumber"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Reg Pemercepat">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumberPemercepat"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Reg DiPercepat">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRegNumberDipercepat" CssClass="textRight"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Total Amount">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount" CssClass="textRight"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Selisih Transfer">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSelisih"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nilai Transfer">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnTransferactual" runat="server">
                                                            <asp:Label runat="server" ID="lblNilaiTransfer" CssClass="textRight"></asp:Label>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Aktual Transfer">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTglTransfer"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Jumlah Kliring">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblJmlKliring"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Kliring">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTglKliring"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="No. TR">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNoTR"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Status">
                                                    <HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:Button Text="Download" runat="server" ID="btnDownload" />
                                    <asp:Button Text="Download Detail" runat="server" ID="btnDownloadDetail" />
                                </td>
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

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPSPTransferPayment.aspx.vb" Inherits="FrmTOPSPTransferPayment" SmartNavigation="False" %>

<%@ Register Assembly="KTB.DNet.WebCC" Namespace="KTB.DNet.WebCC" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Form TOP SparePart</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script>

        //ini function yang di panggil oleh behind code saat label pop up di clik

        function ShowPPBillingNumber() {
            var lblNoRegPembayaran = document.getElementById("lblNoRegPembayaran");
            showPopUp('../General/../PopUp/PopUpTOPSPBilling.aspx?', '', 500, 900, BillingSelection);
        }

        function BillingSelection(selectedBilling) {

            var txtReturnBilling = document.getElementById("txtReturnBilling");

            var strSelectedBilling = selectedBilling;
            txtReturnBilling.value = selectedBilling;
            
            if (navigator.appName != "Microsoft Internet Explorer") {
                if (selectedBilling != null && selectedBilling != "") {
                    txtReturnBilling.onchange();
                }
            }
            else {
                txtReturnBilling.onblur();
            
            }
        }



    </script>


</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px"></td>
            </tr>
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px"></td>
            </tr>
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px">SparePart TOP Payment</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 1px" background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 8px" height="8">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Credit Account</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblCreditAccount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Nama Dealer</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblNamaDealer" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 29px" width="24%">No. Reg Pembayaran</td>
                            <td style="height: 29px" width="1%">:</td>
                            <td style="width: 75%; height: 29px">
                                <asp:Label ID="lblNoRegPembayaran" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 12px" width="24%">Status</td>
                            <td style="height: 12px" width="1%">:</td>
                            <td style="width: 261px; height: 12px" width="261">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="height: 12px" width="24%">Bank Transfer</td>
                            <td style="height: 12px" width="1%">:</td>
                            <td style="width: 261px; height: 12px" width="261">
                                <asp:DropDownList runat="server" ID="ddlBankTranfer"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 12px" width="24%">Billing Number</td>
                            <td style="height: 12px" width="1%">:</td>
                            <td style="width: 261px; height: 12px" width="261">
                                <asp:LinkButton ID="lbtnBillingNumber" runat="server">
                                    <asp:Label ID="lblBillingNumber" runat="server">
                                    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </asp:LinkButton>
                                <asp:TextBox ID="txtReturnBilling" onblur="omitSomeCharacter('txtReturnBilling','<>?*%$')" runat="server" Style="display: none" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>


                    </table>
                </td>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Tanggal Dibuat</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblTanggalDibuat" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Tanggal Transfer</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <cc1:IntiCalendar ID="calTanggalTransfer" runat="server" TextBoxWidth="150"></cc1:IntiCalendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 29px" width="24%">Tanggal Jatuh Tempo</td>
                            <td style="height: 29px" width="1%">:</td>
                            <td style="width: 75%; height: 29px">
                                <asp:Label ID="lblTglJatuhTempo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 12px" width="24%">&nbsp;</td>
                            <td style="height: 12px" width="1%">&nbsp;</td>
                            <td style="width: 261px; height: 12px" width="261">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 12px" width="24%">Total Transfer</td>
                            <td style="height: 12px" width="1%">:</td>
                            <td style="width: 261px; height: 12px" width="261">
                                <asp:Label ID="lblTotalTransfer" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 330px; width: 100%;">
                                    <asp:DataGrid ID="dtgTOPSP" runat="server" Width="100%" AllowSorting="True" CellPadding="3" BorderWidth="0px"
                                        CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" PageSize="50">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn Visible="False" HeaderText="ID">
                                                <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTOPSPID" runat="server" Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn Visible="false" HeaderText="Status">
                                                <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowStatus" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.RowStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="SparePartBilling.Dealer.DealerCode" HeaderText="Dealer Code">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="SparePartBilling.BillingNumber" HeaderText="Billing Number">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillingNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartBIlling.BillingNumber")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Tanggal Billing" SortExpression="SparePartBilling.BillingDate">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillingDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartBIlling.BillingDate", "{0:dd/MM/yyyy}")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Amount Billing + Tax">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmountBillTax" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Amount C2">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmountC2" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Total Amount">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Total Amount" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmountHide" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
                                                        CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>


                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                        </Columns>
                                        <PagerStyle Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" ID="btnKembali" CausesValidation="false" Text="Kembali" />
                    <asp:Button runat="server" ID="btnSimpan" CausesValidation="true" Text="Simpan" />
                    <asp:Button runat="server" ID="btnBaru" CausesValidation="false" Text="Baru" />
                    <asp:Button runat="server" ID="btnValidasi" CausesValidation="false" Text="Validasi" />
                    <asp:Button runat="server" ID="btnPercepatan" CausesValidation="false" Text="Percepatan" />
                    <asp:Button runat="server" ID="btnBatalPembayaran" CausesValidation="false" Text="Batal Pembayaran" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>

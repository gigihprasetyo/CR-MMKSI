<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPExtendedPayment.aspx.vb" Inherits=".FrmMSPExtendedPayment" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html>
<head runat="server">
    <title>Pembayaran MSP Extended</title>
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPChassisNumberSelection() {
            var dealerID = document.getElementById("hdnDealerID");
            var txtChassisNumberList = document.getElementById("txtChassisNumberList");
            showPopUp('../PopUp/PopUpMSPExtendedRegistrationChassisSelection.aspx?Tyjiuy678=' + dealerID.value + '&ChassisNumberList=' + txtChassisNumberList.value + '', '', 500, 760, MSPRegistrationSelection);
        }

        function MSPRegistrationSelection(selectedDt) {
            var txtChassisNumberList = document.getElementById("txtChassisNumberList");
            txtChassisNumberList.value = selectedDt;
            var btnLoaddtFromPopUp = document.getElementById('btnLoaddtFromPopUp');
            if (btnLoaddtFromPopUp) btnLoaddtFromPopUp.click();

        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="tblMSPpayment" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MSP Extended - Input Pembayaran</td>
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
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" width="23%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <asp:HiddenField ID="hdnDealerID" runat="server" />
                                <asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%">&nbsp;</td>
                            <td width="1%">&nbsp;</td>
                            <td width="25%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="23%">Credit Account</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <asp:Label ID="lblCreditAccount" runat="server"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="23%">No Registrasi Pembayaran</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <asp:Label ID="lblPaymentRegNo" runat="server" Text="[belum ada nomer registrasi]"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="23%">Tipe Program</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <asp:Label ID="lblTipeMSP" runat="server"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="23%">Status</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <asp:Label ID="lblStatus" runat="server" Text="Baru"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="23%">Tgl Dibuat</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="23%">Tgl Transfer</td>
                            <td width="1%">:</td>
                            <td width="26%">
                                <cc1:IntiCalendar ID="txtTransferDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                <asp:Label runat="server" ID="lblTransferDate" Visible="false"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>

                        <tr>
                            <td class="titleField" width="23%">
                                <asp:Label runat="server" ID="lblSearchParameter">No Rangka</asp:Label></td>
                            <td width="1%">
                                <asp:Label runat="server" ID="lblDoubleDot">:</asp:Label></td>
                            <td width="26%">
                                <asp:Label ID="lblParameterPencarianPopUp" onclick="ShowPPChassisNumberSelection();" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" alt="Parameter Pencarian">
                                </asp:Label>
                                <asp:HiddenField ID="txtChassisNumberList" runat="server"></asp:HiddenField>
                                <asp:Button ID="btnLoaddtFromPopUp" Style="display: none;" runat="server" Text="" CausesValidation="false" />
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>

                        <tr>
                            <td class="titleField" width="23%">Total Transfer</td>
                            <td width="1%">
                                <asp:Label runat="server" ID="Label2">:</asp:Label></td>
                            <td width="26%">
                                <asp:Label ID="lblTotalTransferHdn" runat="server" Text="0" Visible="false"></asp:Label>
                                <asp:Label ID="lblTotalTransfer" runat="server" Text="0"></asp:Label>
                            </td>
                            <td width="1%"></td>
                            <td class="titleField" width="23%"></td>
                            <td width="1%"></td>
                            <td width="25%"></td>
                        </tr>

                        <tr>
                            <td colspan="7"></td>
                        </tr>
                        <tr>
                            <td colspan="7" valign="top">
                                <div id="div1" style="overflow: auto">
                                    <asp:DataGrid ID="dtgMSPPayment" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="False">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMSPRegistrationHistoryID" runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No Rangka">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtEditChassisNumber"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" ID="txtChassisNumber"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Kode Dealer">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No MSP Extended">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoMSP" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblEditNoMSP"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Tipe MSP Extended">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMSPType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No Debit Charge">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebitChargeNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Harga (Incl. PPN 10%)">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" ID="lblEditEmount"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>


                                            <asp:TemplateColumn HeaderText="No. Faktur Pajak">
                                                <HeaderStyle CssClass="titleTableService" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFakturPajak" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Total Amount">
                                                <HeaderStyle CssClass="titleTableService" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFakturPajakTotalAmount" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Download Debit Memo">
                                                <HeaderStyle CssClass="titleTableService" Width="13%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDownloadDM" runat="server" CommandName="DownloadDM">
												    <img src="../images/download.gif" border="0" alt="DownloadDM">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Download Faktur Pajak">
                                                <HeaderStyle CssClass="titleTableService" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDownloadFakturPajak" runat="server" Text="DownloadFakturPajak" CausesValidation="False" CommandName="DownloadFakturPajak" Visible="false">
										    <img src="../images/download.gif" border="0" alt="Download Faktur Pajak">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%-- <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="edit" Visible="true">
											        <img src="../images/edit.gif" border="0" alt="Edit">
                                                </asp:LinkButton> --%>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="delete" Visible="true">
											        <img src="../images/trash.gif" border="0" alt="Hapus">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%-- <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="add">
											        <img src="../images/add.gif" border="0" alt="Tambah">
                                                </asp:LinkButton> --%>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>

                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:Button ID="btnSave" runat="server" Text="Simpan"></asp:Button>
                                <asp:Button ID="btnValidasi" runat="server" Text="Validasi" Visible="false"></asp:Button>
                                <asp:Button ID="btnConfirm" runat="server" Text="Konfirmasi" Visible="false"></asp:Button>
                                <asp:Button ID="btnNew" runat="server" Text="Baru"></asp:Button>
                                <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Tutup"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

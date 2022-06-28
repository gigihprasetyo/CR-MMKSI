<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPPayment.aspx.vb" Inherits=".FrmMSPPayment" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html>
<head runat="server">
    <title>Pembayaran MSP</title>
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPChassisNumberSelection() {
            var dealerID = document.getElementById("hdnDealerID");
            var txtChassisNumberList = document.getElementById("txtChassisNumberList");
            showPopUp('../PopUp/PopUpMSPRegistrationSelection.aspx?Tyjiuy678=' + dealerID.value + '&ChassisNumberList=' + txtChassisNumberList.value + '', '', 500, 760, MSPRegistrationSelection);
        }

        function MSPRegistrationSelection(selectedDt) {
            var txtChassisNumberList = document.getElementById("txtChassisNumberList");
            txtChassisNumberList.value = selectedDt;
            var btnLoaddtFromPopUp = document.getElementById('btnLoaddtFromPopUp');
            if (btnLoaddtFromPopUp) btnLoaddtFromPopUp.click();
           
        }
    </script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="form1" runat="server">
        <table id="tblMSPpayment" cellSpacing="0" cellPadding="0" width="100%" border="0">
            <tr>
				<td class="titlePage">MSP - Input Pembayaran</td>
			</tr>
            <tr>
				<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
			</tr>
            <tr>
				<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
			</tr>
            <tr>
                <td align="left">
                    <table id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
						<tr>
							<td class="titleField" width="23%">Kode Dealer</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:HiddenField ID="hdnDealerID" runat="server" />
                                <asp:label id="lblDealerCodeName" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Tujuan Pembayaran</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblTglPkt" runat="server" Text="MSP"></asp:label>
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Credit Account</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblCreditAccount" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">
                                 
                            </td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">No Registrasi Pembayaran</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblPaymentRegNo" runat="server" Text="[auto]"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Status</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblStatus" runat="server" Text="Baru"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Tgl Dibuat</td>
							<td width="1%">:</td>
							<td width="26%">
                                <asp:label id="lblCreatedDate" runat="server"></asp:label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
                                
							</td>
						</tr>
                        <tr>
							<td class="titleField" width="23%">Tgl Transfer</td>
							<td width="1%">:</td>
							<td width="26%">
                                <cc1:inticalendar id="txtTransferDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                <asp:Label runat="server" ID="lblTransferDate" Visible="false"></asp:Label>
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%"></td>
							<td width="1%"></td>
							<td width="25%">
							</td>
						</tr>

                        <tr>
							<td class="titleField" width="23%"><asp:label runat="server" ID="lblSearchParameter">No Rangka</asp:label></td>
							<td width="1%"><asp:label runat="server" ID="lblDoubleDot">:</asp:label></td>
							<td width="26%">
                                <asp:Label ID="lblParameterPencarianPopUp" onclick="ShowPPChassisNumberSelection();" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" alt="Parameter Pencarian">
                                </asp:Label>
                                <asp:HiddenField  ID="txtChassisNumberList" runat="server"></asp:HiddenField>
                                <asp:Button ID="btnLoaddtFromPopUp" style="display:none;" runat="server"  Text="" CausesValidation="false" />
                            </td>
							<td width="1%"></td>
							<td class="titleField" width="23%">Total Transfer</td>
							<td width="1%">:</td>
							<td width="25%">
                                <asp:label id="lblTotalTransferHdn" runat="server" text="0" Visible="false"></asp:label>
                                <asp:label id="lblTotalTransfer" runat="server" text="0"></asp:label>
							</td>
						</tr>
                        
                        <tr><td colspan="7"></td></tr>
                        <tr>
                            <td colspan="7" valign="top">
                               <div id="div1" style="OVERFLOW: auto">
                                    <asp:datagrid id="dtgMSPPayment" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="False">
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
									            <asp:Label id="lblMSPRegistrationHistoryID" runat="server" Visible="false"></asp:Label>
									            <asp:Label id="lblNo" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>
                                       
                                        <asp:TemplateColumn HeaderText="No Rangka">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblChassisNumber" runat="server"></asp:Label>
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
									            <asp:Label id="lblDealerCode" runat="server"></asp:Label>
								            </ItemTemplate>
							            </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="No MSP">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblNoMSP" runat="server"></asp:Label>
								            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label runat="server" ID="lblEditNoMSP"></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Tipe Pengajuan">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblRequestType" runat="server"></asp:Label>
								            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label runat="server" ID="lblEditRequestType"></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Tipe MSP">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblMSPType" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="No Debit Charge">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblDebitChargeNo" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Harga MSP(Incl. PPN 10%)">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblAmount" runat="server"></asp:Label>
								            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label runat="server" ID="lblEditEmount"></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>

                                        <%--<asp:TemplateColumn HeaderText="PPN 10%">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblPPN" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Total Biaya(Incl.PPN 10%)">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblTotalAmountPPN" runat="server"></asp:Label>
								            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label runat="server" ID="lblEditTotalAmountPPN"></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>--%>

                                          <asp:TemplateColumn HeaderText="Download Debit Charge">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									             <asp:LinkButton id="lbtnDownloadDC" runat="server" CommandName="DownloadDC">
												    <img src="../images/download.gif" border="0" alt="DownloadDC">
                                                </asp:LinkButton>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Download Debit Memo">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:LinkButton id="lbtnDownloadDM" runat="server" CommandName="DownloadDM">
												    <img src="../images/download.gif" border="0" alt="DownloadDM">
                                                </asp:LinkButton>
											</ItemTemplate>
                                           
                                        </asp:TemplateColumn>

                                        <%--<asp:TemplateColumn HeaderText="Deskripsi">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblDeskription" runat="server"></asp:Label>
								            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:textbox runat="server" ID="txtEditDesciption"></asp:textbox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:textbox runat="server" ID="txtDesciption"></asp:textbox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>--%>

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
                                </asp:datagrid>
                                </div>
                            </td>
                        </tr>
                        <tr><td colspan="7"></td></tr>
                        <tr>
                            <td colspan="7">
                                <asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
                                <asp:button id="btnValidasi" runat="server" Text="Validasi" Visible="false"></asp:button>
                                <asp:button id="btnConfirm" runat="server" Text="Konfirmasi" Visible="false"></asp:button>
                                <asp:button id="btnNew" runat="server" Text="Baru"></asp:button>
                                <asp:button id="btnBack" runat="server" CausesValidation="False" Text="Tutup"></asp:button>
                            </td>
                        </tr>
                    </table>    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

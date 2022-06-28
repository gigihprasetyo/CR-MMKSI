<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListClaim.aspx.vb" Inherits="FrmListClaim" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListClaim</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function KTBNote(selectedCode)
			{
				//Don't Delete
			}		
			
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
						elm.checked = checkVal
						}
					}
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">CLAIM - Daftar Status Claim</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField"><asp:label id="lblCodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD class="titleField"><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td class="titleField"><asp:textbox id="txtKodeDealer" runat="server" onkeypress="return NumericOnlyWith(event,'');"
													Visible="False"></asp:textbox>
												<asp:Label id="lblKodeDealer" runat="server"></asp:Label></td>
											<td><asp:Panel ID="pnlSearch" Runat="server">
													<asp:label id="lblSearchDealer" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
												</asp:Panel></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField" nowrap><asp:label id="lblClaimNo" runat="server">Nomor Claim</asp:label></TD>
								<TD><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD class="titleField"><asp:textbox id="txtClaimNo" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtClaimNo','<>?*%$;')"
										runat="server"></asp:textbox></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField">
									<asp:CheckBox id="chkFakturDate" runat="server" Text="Tanggal Faktur"></asp:CheckBox></TD>
								<TD><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD class="titleField"><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><cc1:inticalendar id="icFakturDateFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icFakturDateUntil" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField" rowspan="2">
									<asp:label id="lblStatu" runat="server">Status</asp:label></TD>
								<TD rowspan="2"><asp:label id="lblColon5" runat="server">:</asp:label></TD>
								<TD rowspan="2" class="titleField">
									<asp:ListBox id="lstStatus" runat="server" Width="96px" Rows="3" SelectionMode="Multiple"></asp:ListBox></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:CheckBox id="chkClaimDate" runat="server" Text="Tanggal Claim" Checked="True"></asp:CheckBox></TD>
								<TD><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD class="titleField"><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><cc1:inticalendar id="icClaimDateFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icClaimDateUntil" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td>
									<asp:button id="btnSearch" runat="server" Text=" Cari " Width="60px"></asp:button></td>
								<TD class="titleField">
									Total</TD>
								<TD>:</TD>
								<TD>
									Rp
									<asp:Label id="lblGrandTotal" runat="server" Width="120px"></asp:Label></TD>
							</tr>
							<TR>
								<TD valign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dtgListClaim" runat="server" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
											CellPadding="3" CellSpacing="1" AllowSorting="True" AutoGenerateColumns="False" Width="100%" DataKeyField="ID" PageSize="25" AllowPaging="True"
											AllowCustomPaging="True">
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SparePartPOStatus.BillingNumber" HeaderText="No. Faktur">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNoFaktur" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingNumber") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SparePartPOStatus.BillingDate" HeaderText="Tgl. Faktur">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTglFaktur" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingDate"),"dd/MM/yyyy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ClaimDate" SortExpression="ClaimDate" HeaderText="Tgl. Claim" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="ClaimNo" HeaderText="No. Claim">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnbNoClaim" runat="server" />
														<asp:Label id="lblNoClaim" runat="server" /></asp:label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total Amount Disetujui">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTotal" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="E.T.A">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblETA" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Download Credit Memo">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="btnDownloadCM" runat="server" Width="20px" Text="DownloadCM" CausesValidation="False" CommandName="DownloadCM" 
                                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingNumber") %>'>
															<img src="../images/Download.gif" border="0" alt="DownloadCM"></asp:LinkButton>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="FakturRetur" HeaderText="Faktur Retur">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblFakturRetur" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FakturRetur") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SORetur" HeaderText="SO Retur">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblSORetur" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SORetur")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDownload" Visible="False" CausesValidation="False" CommandName="download" text="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.UploadFileName") %>' Runat="server">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
														<asp:LinkButton id="lbtnDetails" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Lihat" Runat="server">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Ubah" Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnPopUp" CausesValidation="False" text="Catatan MMKSI" Runat="server">
															<img src="../images/popup.gif" border="0" alt="Catatan MMKSI"></asp:LinkButton>
														<asp:LinkButton id="lbtnFrmClaim" CausesValidation="False" text="Form Claim" Runat="server" Visible="False">
															<img src="../images/print.gif" border="0" alt="Form Claim"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Apakah anda akan menghapus claim ini ?')"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<asp:Panel ID="pnlChangeStatus" Runat="server" Visible="False">
								<TR>
									<TD vAlign="top" colSpan="6">Mengubah Status:
										<asp:dropdownlist id="ddlStatus2" runat="server" Width="140"></asp:dropdownlist>
										<asp:button id="btnSave" runat="server" Text="Proses" Width="60px"></asp:button>
										<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button></TD>
								</TR>
							</asp:Panel>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

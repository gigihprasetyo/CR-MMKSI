<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimList.aspx.vb" Inherits="FrmClaimList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
		</script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				//var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;
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
			function KTBNote(selectedCode)
			{
			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">CLAIM&nbsp;- Daftar Status Claim</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD width="29%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" size="14" Width="200px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" noWrap width="20%">Nomor Claim</TD>
								<TD width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD width="29%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoClaim" onblur="omitSomeCharacter('txtNoClaim','<>?*%$;')"
										runat="server" MaxLength="50" size="22"></asp:textbox></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" noWrap><asp:checkbox id="chkTglFaktur" runat="server" Text="Tanggal Faktur"></asp:checkbox></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD>
									<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td style="WIDTH: 101px"><cc1:inticalendar id="icTglFakturFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icTglFakturUntil" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD valign="top" class="titleField" rowspan="2">Status</TD>
								<TD rowspan="2" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD rowspan="2" width="29%">
									<asp:ListBox id="lstStatus" runat="server" Width="104px" SelectionMode="Multiple" Rows="3"></asp:ListBox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:checkbox id="chkTglClaim" runat="server" Text="Tanggal Claim" Checked="True"></asp:checkbox></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD noWrap width="30%">
									<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icTglClaimFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icTglClaimUntil" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></td>
								<TD class="titleField" width="20%">Total Claim</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="29%"><b>Rp &nbsp;<asp:label id="lblTotalClaim" runat="server" Width="136px"></asp:label></b></TD>
							</tr>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgClaimList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" AllowPaging="True"
											PageSize="25">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="NO">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Progress">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblProgress" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimProgress.Progress") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnStatusChanges" runat="server" Width="20px" Text="StatusChanges" CausesValidation="False"
															CommandName="StatusChanges">
															<img src="../images/alur_flow.gif" border="0" alt="Perubahan Status"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SparePartPOStatus.BillingNumber" HeaderText="No. Faktur">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNoFaktur" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingNumber") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SparePartPOStatus.BillingDate" HeaderText="Tgl Faktur">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTglFaktur" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SparePartPOStatus.BillingDate"),"dd/MM/yyyy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ClaimDate" HeaderText="Tgl Claim">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTglClaim" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ClaimDate"),"dd/MM/yyyy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ClaimNo" HeaderText="No Claim">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnNoClaim" runat="server"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>  
												<asp:TemplateColumn HeaderText="Total Amount Disetujui">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTotalClaim" runat="server"></asp:Label>
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
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEvidence" runat="server" Width="20px" Text="Evidence" CausesValidation="False" CommandName="DownloadCE" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.UploadFileName") %>'>
															<img src="../images/Download.gif" border="0" alt="Evidence"></asp:LinkButton>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img src="../images/detail.gif" border="0" alt="Detail Claim"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Ubah" Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnPopUp" runat="server" Width="20px" Text="Catatan MMKSI" CausesValidation="False"
															CommandName="NoteKTB">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Catatan MMKSI popup"></asp:LinkButton>
														<asp:LinkButton id="lbtnClaimForm" runat="server" Width="20px" Text="Form Claim" CausesValidation="False"
															CommandName="ClaimForm">
															<img src="../images/print.gif" style="cursor:hand" border="0" alt="Form Claim"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="Status MMKSI">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatusKTB" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusKTB") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><STRONG><EM>Mengubah Status &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</EM></STRONG>
						<asp:dropdownlist id="ddlStatusChange" Runat="server"></asp:dropdownlist><asp:button id="btnSimpan" Text="Proses" Runat="server"></asp:button><asp:button id="btnDownload" Text="Transfer ke SAP" Runat="server"></asp:button><asp:button id="btnDownloadExcel" Text="Download Excel" Runat="server"></asp:button>
						<asp:button id="btnDownloadAll" Text="Download All Page" Runat="server"></asp:button></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"><STRONG><EM>Mengubah Progress :</EM></STRONG>
						<asp:dropdownlist id="ddlProgressChanges" Runat="server"></asp:dropdownlist><asp:button id="btnSimpanProgress" Text="Simpan" Runat="server"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

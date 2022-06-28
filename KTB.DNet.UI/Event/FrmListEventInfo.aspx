<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListEventInfo.aspx.vb" Inherits="FrmListEventInfo" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListEvenetInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam[0];
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
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="#ffffff">
				<TR>
					<TD class="titlePage">EVENT - Daftar Info EVENT</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtKodeDealer" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">No Pengajuan</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox id="txtNo" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Jenis Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:dropdownlist id="ddlEventType" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Jadwal Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD>
									<TABLE id="Table3" border=0 cellspacing=0 cellpadding=0>
										<TR>
											<TD><cc1:inticalendar id="icDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icDateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Tanggal Pengajuan&nbsp;Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD>
									<TABLE id="Table4" border=0 cellpadding=0 cellspacing=0>
										<TR>
											<TD><cc1:inticalendar id="icStartEventReq" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icEndEventReq" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Tanggal Konfirmasi Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD>
									<TABLE id="Table5" border=0 cellspacing=0 cellpadding=0>
										<TR>
											<TD><cc1:inticalendar id="icStartEventConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icEndEventConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px"></TD>
								<TD style="WIDTH: 2px"></TD>
								<TD><asp:button id="btnSearch" runat="server" Width="72px" Text="Cari"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dtgList" runat="server" Width="100%" AllowPaging="True" PageSize="25" AllowCustomPaging="True"
								DataKeyField="ID" AutoGenerateColumns="False" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px"
								AllowSorting="True">
								<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
										<HeaderStyle CssClass="titleTablePromo" />
										<ItemTemplate>
											<asp:Label ID="lblDealerCity" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.City.CityName")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn headerText="No Pengajuan Event" sortExpression="EventRequestNo">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEventNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventRequestNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn headerText="No Persetujuan Event" SortExpression="EventApprovalNo">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEventNoApproval" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventApprovalNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jenis Event" SortExpression="Description">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEventType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventType.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Mulai" SortExpression="DateStart">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStart" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DateStart"),"dd/MM/yyyy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Selesai" SortExpression="DateEnd">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEnd" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DateEnd"),"dd/MM/yyyy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jumlah Undangan" SortExpression="NumOfInvitation">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAudience" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NumOfInvitation") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Area Koordinator" SortExpression="AreaCoordinator">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAreaCoor" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AreaCoordinator") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Manajemen KTB/Observer" SortExpression="Observer">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblManagement" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Observer") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn headerText="Mulai">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStartFix" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn headertext="Selesai">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEndFix" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tempat Acara" SortExpression="Location">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblEventLocation" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Location") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jumlah Undangan" SortExpression="ConfirmedNumOfInvitation">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblFixAudience" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ConfirmedNumOfInvitation") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total Biaya" SortExpression="ConfirmedTotalCost">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblConfirmedTotalCost" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ConfirmedTotalCost"),"###,##0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Komentar Dealer">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Button ID="btnShowComment" Runat="server" Text="Lihat"></asp:Button>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap=False></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetails" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Lihat" Runat="server">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Ubah" Runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lbtnDelete" CausesValidation = "False" CommandName = "delete" CommandArgument = '<%# DataBinder.Eval(Container,"DataItem.ID")%>' text = "Hapus" Runat = "server">
												<img onclick="return confirm('Anda yakin?');" src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnAttachment" CausesValidation="False" CommandName="attachment" text="Attachment" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Runat="server" Visible="False">
												<img src="../images/popup.gif" border="0" alt="Attachment"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnStreaming" CausesValidation="False" CommandName="streaming" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Video Streaming" Runat="server" Visible="False">
												<img src="../images/icon_video.gif" border="0" alt="Video Streaming"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnCost" CausesValidation="False" CommandName="cost" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="File Perkiraan Biaya" Runat="server" Visible="False">
												<img src="../images/alur_flow.gif" border="0" alt="File Perkiraan Biaya"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD width="100%" ><asp:panel id="pnlApproval" Visible="False" Runat="server" BackColor="White">Mengubah Status : 
<asp:DropDownList id="ddlConfirmed" runat="server"></asp:DropDownList>
<asp:Button id="btnProcess" runat="server" Text="Proses"></asp:Button></asp:panel></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

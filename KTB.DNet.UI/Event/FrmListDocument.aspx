<%@ Register TagPrefix="uc1" TagName="Clock" Src="../UserControl/Clock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListDocument.aspx.vb" Inherits="FrmListDocument" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maintenance Merek Kompetitor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtStockKodeDealer = document.getElementById("txtDealerCode");
				txtStockKodeDealer.value = tempParam[0];			
			}
			function ShowEventSelection()
			{
				showPopUp('../General/../PopUp/PopUpEventMaster.aspx','',500,760,EventSelection);
			}
			function EventSelection(selectedEvent)
			{
				var txtEventNumber = document.getElementById("txtEventNumber");
				txtEventNumber.value = selectedEvent;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">EVENT - List Dokumen</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">No Event</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtEventNumber"
										onblur="omitSomeCharacter('txtKode','<>?*%$;');" runat="server" Width="200px"></asp:textbox><asp:label id="lblSearchEvent" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtDealerCode"
										onblur="omitSomeCharacter('txtDeskripsi','<>?*%^():|\@#$;+=`~{}');" Width="200px" Runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Periode&nbsp;Event</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" vAlign="middle" width="262">
									<asp:textbox ID="txtJadwal" onkeypress="return NumericOnlyWith(event,'');" Width="64px" Runat="server"
										MaxLength="4"></asp:textbox></TD>
							</TR>
							<TR valign=top>
								<TD class="titleField" style="WIDTH: 88px" width="88">Tanggal&nbsp;Event</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" vAlign="middle" width="262">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td style="WIDTH: 100px"><cc1:inticalendar id="ICDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<TD class="titleField">&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="ICSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 262px" width="262"><asp:button id="btnCari" runat="server" width="70px" Text="Cari"></asp:button>&nbsp;</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dgListDocument" runat="server" Width="100%" PageSize="25" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EventMaster.EventNo" HeaderText="No Event">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventMaster.EventNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblDealerName" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
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
									<asp:TemplateColumn HeaderText="Jadwal Event">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriode" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dokumentasi">
										<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDpwnload" runat="server" CommandName="Download">
												<img src="../images/simpan.gif" border="0" alt="Download Dokumen">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

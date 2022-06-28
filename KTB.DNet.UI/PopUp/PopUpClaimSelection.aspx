<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClaimSelection.aspx.vb" Inherits="FrmPopUpClaimSelection" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
  
  
		<title>FrmPopUpClaimSelection</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="6">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">CLAIM - Daftar Claim</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="15%"><asp:label id="Label1" runat="server">Kode</asp:label>Dealer</TD>
					<td width="1%">:</td>
					<TD width="40%">
						<asp:textbox id="txtDealerCode" runat="server" Width="152px"></asp:textbox><asp:label id="lblSearchClaim" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<td width="15%">Nomor Claim</td>
					<td width="1%">:</td>
					<td width="30%">
						<asp:textbox id="Textbox1" runat="server" Width="152px"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField"><INPUT type="checkbox" value="on">&nbsp;Tanggal Faktur</TD>
					<td>:</td>
					<TD style="WIDTH: 344px">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td><cc1:inticalendar id="icValidFrom" runat="server"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icValidTo" runat="server"></cc1:inticalendar></td>
							</tr>
						</table>
					<td style="WIDTH: 96px">Status</td>
					<td style="WIDTH: 7px">:</td>
					<td>
						<asp:dropdownlist id="ddlStatus" runat="server" Width="140px"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD><INPUT type="checkbox" value="on">&nbsp;Tanggal Claim</TD>
					<td>:</td>
					<TD style="WIDTH: 344px">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td><cc1:inticalendar id="Inticalendar1" runat="server"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="Inticalendar2" runat="server"></cc1:inticalendar></td>
							</tr>
						</table>
					<td style="WIDTH: 96px"></td>
					<td style="WIDTH: 7px"></td>
					<td></td>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD style="WIDTH: 344px">
						<asp:button id="btnSearch" runat="server" Text="Cari" Width="104px"></asp:button>
					</TD>
					<td style="WIDTH: 96px"></td>
					<td style="WIDTH: 7px"></td>
					<td></td>
				</TR>
				<tr>
					<td colspan="6" align="center">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgClaimDealerETA" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No Faktur">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# setCategoryName(DataBinder.Eval(Container, "DataItem.Dealer.ID")) %>' ID=lblDealerCode >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl. Faktur">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl.Claim">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No.Claim">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="E.T.A">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Penjelasan">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Faktur Retur">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap=False></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False" 
 CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False" 
 CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</td>
				</tr>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}
			
			
				
		</script>
	</body>
</HTML>

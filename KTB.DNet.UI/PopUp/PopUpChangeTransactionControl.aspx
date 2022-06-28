<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpChangeTransactionControl.aspx.vb" Inherits="PopUpChangeTransactionControl" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar Perubahan Status Transaction Control</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 18px" colSpan="7">MAINTENANCE - Daftar Perubahan Status</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
											<TR>
					<TD class="titleField" width="34%" style="HEIGHT: 17px">
						<asp:Label id="lblDealer" runat="server">Kode Dealer</asp:Label></TD>
					<TD width="1%" style="HEIGHT: 17px">:</TD>
					<TD width="35%" style="HEIGHT: 17px">
						<asp:Label id="lblDealerValue" runat="server"></asp:Label></TD>
					<td width="30%" style="HEIGHT: 17px"></td>
				</TR>
				<TR>
					<TD class="titleField">
						<asp:Label id="lblTipeTransaksi" runat="server">Tipe Transaksi</asp:Label></TD>
					<TD>:</TD>
					<TD>
						<asp:Label id="lblTipeTransaksiValue" runat="server"></asp:Label></TD>
					<td width="50%"></td>
				</TR>
				<TR valign="top" height="250">
					<TD colSpan="4">
					<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px">
						<asp:DataGrid id="dtgStatusChangeHistory" runat="server" AutoGenerateColumns="False" Width="100%"
							OnItemDataBound="dtgStatusChangeHistory_itemdataBound">
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle HorizontalAlign="Center" Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Status Lama">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Status Baru">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedTime" HeaderText="Diproses Tanggal" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
									<HeaderStyle HorizontalAlign="Center" Width="26%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedBy" HeaderText="Diproses Oleh">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:DataGrid></div></TD>
				</TR>
				<TR>
					<TD colSpan="4" align="center"><INPUT id="btnCancel" style="WIDTH: 55px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

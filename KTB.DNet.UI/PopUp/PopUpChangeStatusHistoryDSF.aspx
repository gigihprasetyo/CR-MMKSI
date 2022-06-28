<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpChangeStatusHistoryDSF.aspx.vb" Inherits="PopUpChangeStatusHistoryDSF" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
	
		<title>Daftar Perubahan Status</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titleField" width="40%">
						<asp:Label id="Label1" runat="server">Jenis Dokumen</asp:Label></TD>
					<TD width="1%">:</TD>
					<TD width="60%">
						<asp:Label id="Label2" runat="server">DSF Leasing Claim</asp:Label></TD>
				</TR>
				<TR>
					<TD class="titleField" width="40%">
						<asp:Label id="lblNoRegDokumen" runat="server">Nomor Reg Dokumen</asp:Label></TD>
					<TD width="1%">:</TD>
					<TD>
						<asp:Label id="lblNoRegDokumenValue" runat="server"></asp:Label></TD>
				</TR>
				<TR valign="top" height="250">
					<TD colSpan="4">
						<asp:DataGrid id="dtgStatusChangeHistory" runat="server" AutoGenerateColumns="False" Width="100%"
							OnItemDataBound="dtgStatusChangeHistory_itemdataBound">
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle HorizontalAlign="Center" Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status Lama">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStatusLama" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status Baru">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStatusBaru" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CreatedTime" HeaderText="Diproses Tanggal" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
									<HeaderStyle HorizontalAlign="Center" Width="26%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Diproses Oleh">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCreatedBy" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid></TD>
				</TR>
				<TR>
					<TD colSpan="4" align="center"><INPUT id="btnCancel" style="WIDTH: 55px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

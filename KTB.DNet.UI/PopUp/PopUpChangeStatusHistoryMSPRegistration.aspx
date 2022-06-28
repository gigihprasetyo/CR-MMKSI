<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpChangeStatusHistoryMSPRegistration.aspx.vb" Inherits=".PopUpChangeStatusHistoryMSPRegistration" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Daftar Perubahan Status</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
</head>
<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titleField" width="34%" style="HEIGHT: 17px">
						<asp:Label id="lblJenisDokumen" runat="server">Jenis Dokumen</asp:Label></td>
					<td width="1%" style="HEIGHT: 17px">:</td>
					<td width="35%" style="HEIGHT: 17px">
						<asp:Label id="lblJenisDokumenValue" runat="server" Text="MSP Registrasi"></asp:Label></td>
					<td width="30%" style="HEIGHT: 17px"></td>
				</tr>
				<tr>
					<td class="titleField">
						Tipe MSP
					</td>
					<td>:</td>
					<td>
						<asp:Label id="lblMSPType" runat="server"></asp:Label></td>
					<td width="50%"></td>
				</tr>
				<tr valign="top" height="250">
					<td colSpan="4">
						<asp:DataGrid id="dtgStatusChangeHistory" runat="server" AutoGenerateColumns="False" Width="100%"
							OnItemDataBound="dtgStatusChangeHistory_itemdataBound">
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle HorizontalAlign="Center" Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Status Lama">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Status Baru">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedTime" HeaderText="Diproses Tanggal" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
									<HeaderStyle HorizontalAlign="Center" Width="26%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedBy" HeaderText="Diproses Oleh">
									<HeaderStyle HorizontalAlign="Center" Width="18%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:DataGrid></td>
				</tr>
				<TR>
					<TD colSpan="4" align="center"><INPUT id="btnCancel" style="WIDTH: 55px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</table>
		</form>
	</body>
</html>

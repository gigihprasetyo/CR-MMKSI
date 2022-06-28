<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarStatusPerRequest.aspx.vb" Inherits="FrmDaftarStatusPerRequest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDaftarStatusPerRequest</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td>Kode Dealer</td>
					<td>&nbsp;:&nbsp;&nbsp;<asp:Label Runat="server" ID="lblDealerCode"></asp:Label></td>
					<td></td>
					<td><asp:Label Runat="server" ID="lblDealerName" Visible="False"></asp:Label></td>
				</tr>
				<!--<tr>
					<td>Kode Konsumen</td>
					<td>&nbsp;:&nbsp;&nbsp;<asp:Label Runat="server" ID="lblCustomerCode"></asp:Label></td>
					<td></td>
					<td><asp:Label Runat="server" ID="lblCustomerName"></asp:Label></td>
				</tr>-->
			</table>
			<table>
				<tr>
					<td>
						<asp:DataGrid ID="dtListStatus" runat="server" Width="100%" GridLines="None" CellPadding="3" BackColor="#CDCDCD"
							AllowCustomPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
							CellSpacing="1" AllowSorting="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="OldStatus" HeaderText="Status Lama" HeaderStyle-CssClass="titleTableMrk"></asp:BoundColumn>
								<asp:BoundColumn DataField="NewStatus" HeaderText="Status Baru" HeaderStyle-CssClass="titleTableMrk"></asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedTime" HeaderText="Di Proses Tanggal" HeaderStyle-CssClass="titleTableMrk"></asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedBy" HeaderText="Di Proses Oleh" HeaderStyle-CssClass="titleTableMrk"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
			<table>
				<tr align="center">
					<td>
						<asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

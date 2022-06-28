<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRParts.aspx.vb" Inherits="FrmPQRParts" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPQRParts</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td>
						Kode Parts
					</td>
					<td>:</td>
					<td>
						<asp:TextBox ID="txtKode" Runat="server"></asp:TextBox>
					</td>
					<td>
						Model
					</td>
					<td>:</td>
					<td>
						<asp:TextBox ID="txtModel" Runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						Nama Parts
					</td>
					<td>:</td>
					<td>
						<asp:TextBox ID="Textbox1" Runat="server"></asp:TextBox>
					</td>
					<td colspan="3">
						<asp:Button ID="btnCari" Runat="server" Text="Cari"></asp:Button>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:DataGrid ID="dgKerusakan" Runat="server"></asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:Button ID="btnPilih" Runat="server" Text="Pilih"></asp:Button>
						<asp:Button ID="btnBatal" Runat="server" Text="Batal"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

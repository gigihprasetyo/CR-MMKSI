<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRKerusakan.aspx.vb" Inherits="FrmPQRKerusakan" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPQRKerusakan</title>
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
						Kode
					</td>
					<td>:</td>
					<td>
						<asp:TextBox ID="txtKode" Runat="server"></asp:TextBox>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td>
						Description
					</td>
					<td>:</td>
					<td>
						<asp:TextBox ID="Textbox1" Runat="server"></asp:TextBox>
					</td>
					<td>
						<asp:Button ID="btnCari" Runat="server" Text="Cari"></asp:Button>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:DataGrid ID="dgKerusakan" Runat="server"></asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:Button ID="btnPilih" Runat="server" Text="Pilih"></asp:Button>
						<asp:Button ID="btnBatal" Runat="server" Text="Batal"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

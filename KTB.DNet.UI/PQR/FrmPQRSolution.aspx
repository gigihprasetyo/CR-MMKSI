<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRSolution.aspx.vb" Inherits="FrmPQRSolution"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPQRSolution</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PRODUCT QUALITY REPORT  -&nbsp; Solusi PQR</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</table>
			<table width="100%" border=0 cellpadding="2" cellspacing="1">
				<tr>
					<td class="titleField" width="20%">No PQR</td>
					<td width="1%" class="titleField">:</td>
					<td width="29%"><asp:textbox id="txtPQRNo" Runat="server"></asp:textbox></td>
					<td width="20%" class="titleField">Kode Kerusakan</td>
					<td class="titleField" width="1%">:</td>
					<td width="29%"><asp:textbox id="txtKodeKerusakan" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField">Kategori</td>
					<td class="titleField">:</td>
					<td ><asp:dropdownlist id="ddlKategori" Runat="server"></asp:dropdownlist></td>
					<td class="titleField">Kode Part</td>
					<td class="titleField">:</td>
					<td ><asp:textbox id="txtKodePart" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField">Tipe
					</td>
					<td class="titleField">:</td>
					<td><asp:dropdownlist id="ddlTipe" Runat="server"></asp:dropdownlist></td>
					<td colSpan="3"></td>
				</tr>
				<tr>
					<td class="titleField">Subject
					</td>
					<td class="titleField">:</td>
					<td><asp:textbox id="txtSubject" Runat="server"></asp:textbox></td>
					<td colSpan="3"><asp:button id="btnCari" Runat="server" Text="Cari" Width="60"></asp:button>&nbsp;
						<asp:Button id="btnBack" runat="server" Text="Kembali" Visible =False ></asp:Button></td>
				</tr>
				<tr>
					<td colSpan="6">
						<asp:Panel id="pnlResult" runat="server"></asp:Panel></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

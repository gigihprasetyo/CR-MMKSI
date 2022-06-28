<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformInvoice2.aspx.vb" Inherits="FrmSalesmanUniformInvoice2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanUniformInvoice2</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">SERAGAM - Kwitansi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">Kode Pesanan</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%"><asp:Literal ID="ltrlKodePSeragam" Runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">
						Deskripsi</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%"><asp:Literal ID="ltrlDeskripsi" Runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">No Kwitansi</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%">
						<asp:TextBox id="txtKwitansi" runat="server" Width="168px"></asp:TextBox></td>
				</tr>
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
					<TD style="HEIGHT: 20px" width="1%"></TD>
					<TD style="HEIGHT: 20px" width="75%"></TD>
				</TR>
			</table>
			<table DESIGNTIMEDRAGDROP="867">
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">Untuk Pembayaran</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%">
						<asp:TextBox id="txtDescription" runat="server" MaxLength="200" Width="240px" ReadOnly="True"
							TextMode="MultiLine"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="titleField" style="HEIGHT: 20px" width="24%">
						Catatan</td>
					<TD style="HEIGHT: 20px" width="1%">:</TD>
					<td style="HEIGHT: 20px" width="75%">
						<asp:TextBox id="txtNote" runat="server" MaxLength="80" Width="240px"></asp:TextBox></td>
				</tr>
				<tr>
					<td></td>
					<td></td>
					<td><asp:button id="btnPrint" runat="server" Width="60px" Text="Cetak"></asp:button>
						<asp:button id="btnBack" runat="server" Width="60px" Text="Kembali"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

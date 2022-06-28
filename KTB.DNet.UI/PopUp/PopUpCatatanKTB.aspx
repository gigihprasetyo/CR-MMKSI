<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpCatatanKTB.aspx.vb" Inherits="PopUpCatatanKTB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
	
		<title>Catatan MKS</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="260" border="0">
				<TR>
					<TD Class="titleTableSales" align="center">Catatan MKS</TD>
				</TR>
				<TR>
					<TD>
						<asp:TextBox id="txtNote" runat="server" TextMode="MultiLine" Width="300px" Rows="4"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="btnSimpan" runat="server" Text="Simpan"></asp:Button>&nbsp; <INPUT type="button" size="25" onclick="window.close();" value="Tutup"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

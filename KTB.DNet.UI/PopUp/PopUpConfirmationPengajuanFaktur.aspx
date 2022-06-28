<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpConfirmationPengajuanFaktur.aspx.vb" Inherits="PopUpConfirmationPengajuanFaktur" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpConfirmationPengajuanFaktur</title>
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
			<table>
				<tr>
					<td align="left" class="titleField">Warning</td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblPesan" runat="server">Label</asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnYes" runat="server" Text="Ya"></asp:Button>
						<asp:Button id="btnNo" runat="server" Text="Tidak"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

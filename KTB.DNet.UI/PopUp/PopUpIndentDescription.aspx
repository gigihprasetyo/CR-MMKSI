<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpIndentDescription.aspx.vb" Inherits="PopUpIndentDescription"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpIndentDescription</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="50%">
				<tr>
					<td>Keterangan&nbsp; :</td>
				</tr>
				<tr>
					<td>
						<asp:TextBox id="txtDescription" runat="server" Width="344px" Height="96px" TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Button id="btnSave" runat="server" Text="Simpan"></asp:Button>
						<asp:Button id="btnCancel" runat="server" Width="64px" Text="Batal"></asp:Button>
						<asp:Button id="btnExit" runat="server" Text="Keluar"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

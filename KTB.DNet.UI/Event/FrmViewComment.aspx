<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmViewComment.aspx.vb" Inherits="FrmViewComment" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmViewComment</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="4" cellPadding="4" width="100%" border="0">
				<tr>
					<td>
						<asp:Label ID="lblNote" Runat="server" CssClass="titlePage">Komentar</asp:Label></td>
				</tr>
				<tr valign="top">
					<td valign="top"><div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 120px">
							<asp:Literal ID="ltrComment" Runat="server" /></div>
					</td>
				</tr>
				<tr>
					<td>
						<INPUT id="btnCancel" class="hideButtonOnPrint" onclick="window.close()" type="button"
							value="Tutup" name="btnCancel"></P>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

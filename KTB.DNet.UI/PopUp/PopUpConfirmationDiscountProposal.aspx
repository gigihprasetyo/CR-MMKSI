<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpConfirmationDiscountProposal.aspx.vb" Inherits="PopUpConfirmationDiscountProposal" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Konfirmasi Data Fleet Customer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<base target="_self">
		<script language="javascript" type="text/javascript">
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<tr>
					<td align="left" class="titleField">
						<asp:Label id="lblPesan" Font-Size="15px" runat="server">Label</asp:Label></td>
				</tr>
                <tr>
                    <td><hr /></td>
                </tr>
				<tr align="center">
					<td align="center">
						<asp:Button id="btnYes" runat="server" Text="Ya" Width="50px"></asp:Button>&nbsp;&nbsp;
						<asp:Button id="btnNo" runat="server" Text="Tidak" Width="50px"></asp:Button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

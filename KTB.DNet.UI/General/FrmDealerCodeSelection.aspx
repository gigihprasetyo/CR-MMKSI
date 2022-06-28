<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerCodeSelection.aspx.vb" Inherits="FrmDealerCodeSelection" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDealerCodeSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1"  cellSpacing="0" cellPadding="0" width="740" border="0">
			<tr>
					<td class="titlePage">DEALER - Dealer Selection</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
				
					<td>
						<TABLE id="Table2"  cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<TD width="24%"  colSpan="1" rowSpan="1"><STRONG>Kode Dealer</STRONG></TD>
								<TD>&nbsp;<STRONG>:</STRONG>&nbsp;</TD>
								<TD><asp:textbox id="txtDealerCode" runat="server" Width="375px"></asp:textbox></TD>
								<TD>&nbsp;<asp:button id="cmdExtend" runat="server" Text=" Cari "></asp:button></TD>
							</tr>
						</table>					
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

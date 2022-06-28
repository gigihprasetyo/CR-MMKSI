<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmToolsDownload.aspx.vb" Inherits="FrmToolsDownload" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCity</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">TOOLS - Download Report</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Laporan</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:dropdownlist id="ddlReport" runat="server" Width="350px" AutoPostBack="True"></asp:dropdownlist></td>
							</TR>
							<asp:panel id="pnlInfo" Runat="server">
								<TR>
									<TD class="titleField" width="24%">Kriteria</TD>
									<TD width="1%">:</TD>
									<TD width="75%">
										<asp:Label id="lblCondition" Runat="server"></asp:Label></TD>
								</TR>
								<TR vAlign="top">
									<TD class="titleField">SQL Query</TD>
									<TD>:</TD>
									<TD>
										<asp:textbox id="txtSql" runat="server" Width="100%" Wrap="True" Height="150px" MaxLength="2000"></asp:textbox></TD>
								</TR>
							</asp:panel>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD>
									<asp:button id="btnDownload" runat="server" Width="60px" Text="Download"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>

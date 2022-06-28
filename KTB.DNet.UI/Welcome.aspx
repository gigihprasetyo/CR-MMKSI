<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Welcome.aspx.vb" Inherits="Welcome" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Welcome</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="./WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="./WebResources/TextEffects.js"></script>
		
		<script language="javascript">
		document.oncontextmenu=new Function("return false");	
		</script>

	</HEAD>
	<body bgColor="aqua" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="400" cellSpacing="1" cellPadding="1" width="100%" align="center"
				border="0">
				<TR>
					<td vAlign="middle" align="center">
						<TABLE id="Table2" height="200" cellSpacing="2" cellPadding="0" width="500" align="center"
							border="0">
							<TR>
								<TD>
									<TABLE id="Table3" height="200" cellSpacing="1" cellPadding="2" width="500" align="center"
										border="0">
										<TR>
											<TD align="center"><asp:label id="lblSelamatDatang" runat="server" ForeColor="blue" Font-Bold="True" Font-Size="Large">SELAMAT DATANG</asp:label></TD>
										</TR>
										<TR>
											<TD class="titlePage" noWrap align="center"><asp:label id="lblDealerName" runat="server" Font-Bold="True" Font-Size="Large"></asp:label></TD>
										</TR>
										<TR>
											<TD align="center"><asp:label id="lblSeachTerm" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label>-
												<asp:label id="lblDealerCode" runat="server" Font-Bold="True" Font-Size="Medium"></asp:label></TD>
										</TR>
										<tr>
											<td align="center"><asp:label id="lblAlert" Visible="False" Runat="server"></asp:label></td>
										</tr>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<br>
						<table cellSpacing="1" cellPadding="6" width="100%" border="0">
							<tr vAlign="top">								
								<td width="80%" height="200">
									<table>
										<tr>
											<td><asp:placeholder id="phOneTimeAlert" Runat="server"></asp:placeholder>
											</td>
										</tr>
									</table>
									
								</td>
								<td width="20%">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 220px; HEIGHT: 240px">
										<table style="BORDER-RIGHT: white 1px solid; BORDER-TOP: white 1px solid; BORDER-LEFT: white 1px solid; BORDER-BOTTOM: white 1px solid; BACKGROUND-COLOR: white"
											cellSpacing="0" cellPadding="10" width="200" border="0">
											<tr>
												<td>
													<asp:placeholder id="phDashboardAlertTransaction" Runat="server"></asp:placeholder>
												</td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (document.parentWindow.name != "frMain")
				{
				  self.opener = null;
				  self.close();
				}
		</script>
		<asp:placeholder id="phTextEffectsScript" Runat="server"></asp:placeholder>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAccessDenied.aspx.vb" Inherits="FrmAccessDenied" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAccessDenied</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="./WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<br>
		<br>
		<br>
			<TABLE id="Table1" cellSpacing="10" cellPadding="1" width="500" border="0" align="center">
				<TR>
					<TD align="center" class="titlePage">I N F O R M A S I</TD>
				</TR>
				<TR>
					<TD width="100%" style="width: 100%;" align="center">
						<asp:Label width="100%" style="width: 100%;" id="lblInfo" runat="server">MAAF : ANDA TIDAK PUNYA AKSES PADA MODUL INI </asp:Label>
				</TR>
                <tr><TD align="left">
						<asp:Label style="text-align: left;" id="lblDescription" runat="server"></asp:Label></TD>
				</tr>
				<TR>
					<TD runat="server" id="labelCallAdmin" align="center"><STRONG>HUBUNGI ADMINISTRATOR !!</STRONG></TD>
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

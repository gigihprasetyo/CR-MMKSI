<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFSSynchronization.aspx.vb" Inherits="FrmFSSynchronization" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 32px; WIDTH: 888px; POSITION: absolute; TOP: 16px; HEIGHT: 48px; cellSpacing: "
				cellPadding="1" width="888" border="1">
				<tr>
					<td style="HEIGHT: 8px" align="center"><STRONG>Free Service Synchronization</STRONG>
					</td>
				</tr>
				<TR>
					<TD style="HEIGHT: 3px" vAlign="top" align="left">
						<TABLE id="Table2" style="WIDTH: 592px; HEIGHT: 16px" cellSpacing="1" cellPadding="1" width="592"
							border="0">
							<TR>
								<TD style="WIDTH: 123px; HEIGHT: 15px">File .txt&nbsp; Upload</TD>
								<TD style="WIDTH: 428px; HEIGHT: 15px"><INPUT id="DataFile" style="WIDTH: 319px; HEIGHT: 24px" type="file" size="34" name="File1"
										runat="server" onkeypress="return false;"></TD>
								<TD style="WIDTH: 153px; HEIGHT: 15px"><asp:button id="btnUpload" runat="server" Width="96px" Text="Upload"></asp:button></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
						</TABLE>
						<asp:Button id="Button1" runat="server" Text="Button"></asp:Button>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmUELA.aspx.vb" Inherits="frmUELA" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Syarat dan Ketentuan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="./WebResources/InputValidation.js"></script>
	<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" width="100%" cellSpacing="6" cellPadding="10" border="0" bgcolor="#cccccc">
				<TR>
					<TD bgcolor="white">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td align="center"><b><B>ANDA TELAH MEMASUKI SITUS APLIKASI DEALER NETWORK (D-NET) 
											<BR>
											PT MITSUBISHI MOTORS KRAMA YUDHA SALES INDONESIA </B></b>
								</td>
							</tr>
							<tr>
								<td background="images/bg_hor.gif" height="1"><IMG height="1" src="images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="0" src="images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<asp:Label id="Label1" runat="server"></asp:Label>
						<br>
						<div align="center"><asp:button id="btnApprove" runat="server" Text="Setuju"></asp:button><asp:button id="btnDisapprove" runat="server" Text="Tidak Setuju"></asp:button></div>
						<br>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

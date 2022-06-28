<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpPicture.aspx.vb" Inherits="PopUpPicture"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpPicture</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Image id="imgDisplay" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Width="480px" Height="415px"></asp:Image>
			<asp:LinkButton id="lbtnDownload" style="Z-INDEX: 104; LEFT: 384px; POSITION: absolute; TOP: 424px"
				runat="server" Width="105px" Font-Size="X-Small" Font-Names="Arial" Visible="False">Download this file</asp:LinkButton><INPUT id="btnClose" onclick="window.close();" style="Z-INDEX: 103; LEFT: 192px; WIDTH: 72px; POSITION: absolute; TOP: 424px; HEIGHT: 24px"
				type="button" value="Tutup">
			<asp:Label id="lblError" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 424px" runat="server"
				Width="144px" ForeColor="Red" Font-Names="Arial" Font-Size="X-Small"></asp:Label>
		</form>
	</body>
</HTML>

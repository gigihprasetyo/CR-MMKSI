<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DownloadContainer.aspx.vb" Inherits="DownloadContainer" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DownloadContainer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="txtFromURL" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 88px" runat="server"></asp:textbox><asp:label id="txtMessage" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"></asp:label><asp:textbox id="txtDownloadLocation" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 64px"
				runat="server"></asp:textbox><asp:button id="btnBack" style="Z-INDEX: 104; LEFT: 8px; POSITION: absolute; TOP: 32px" runat="server"
				Text="Kembali"></asp:button><INPUT id="btnClose" style="Z-INDEX: 105; LEFT: 120px; POSITION: absolute; TOP: 32px" type="button"
				value="Tutup" onclick="window.close();"></form>
		<script language="javascript">
			document.getElementById("txtDownloadLocation").style.visibility="hidden";
			document.getElementById("txtFromURL").style.visibility="hidden";
			document.getElementById("btnClose").style.visibility="hidden";
			document.location.href="downloadlocal.aspx?file="+document.getElementById("txtDownloadLocation").value;			
		</script>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DownloadPRP.aspx.vb" Inherits="KTB.DNet.UI.SparePart.DownloadPRP" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDisplayPRPToko</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		function downloadPRP()
		{
			window.open('./downloadPRP.aspx','_blank','fullscreen=no,menubar=yes,status=yes,titlebar=yes,toolbar=no,height=480,width=640,resizable=yes');
		}
		</script>
	</HEAD>
	<body>
		<asp:DataGrid id="dtgData" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
			BackColor="White" CellPadding="4" GridLines="Horizontal" ForeColor="Black">
			<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></SelectedItemStyle>
			<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#333333"></HeaderStyle>
			<FooterStyle ForeColor="Black" BackColor="#CCCC99"></FooterStyle>
			<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="White"></PagerStyle>
		</asp:DataGrid>
	</body>
</HTML>

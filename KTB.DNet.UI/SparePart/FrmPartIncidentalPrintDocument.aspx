<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalPrintDocument.aspx.vb" Inherits="FrmPartIncidentalPrintDocument" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPartIncidentalPrintDocument</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		
		//function Back()
		//{
		//if(navigator.appName == "Microsoft Internet Explorer")
		//{
		//window.history.go(-1);
		//}
		//else
		//{
		//var hidden = document.getElementById("Hidden1")
		//var i = hidden.value * -1
		//window.history.go(i);
		//}
		//}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 872px; POSITION: absolute; TOP: 8px; HEIGHT: 56px"
				cellSpacing="1" cellPadding="1" width="872" border="0">
				<TR>
					<TD colSpan="2"><asp:label id="lblStringBuilder" runat="server"></asp:label></TD>
				</TR>
				<TR class="hideTrOnPrint">
					<TD style="WIDTH: 133px">
						<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button>&nbsp;&nbsp;
						<asp:Button id="btnPrinter" runat="server" Text="Print" Width="56px"></asp:Button></TD>
					<TD><INPUT id="Hidden1" type="hidden" name="Hidden1" runat="server"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

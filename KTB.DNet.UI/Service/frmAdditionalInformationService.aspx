<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmAdditionalInformationService.aspx.vb" Inherits="frmAdditionalInformationService" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Informasi Tambahan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		
		function GetSelectedText()
		{
			var txt = document.getElementById("txtComment");
			if(navigator.appName == "Microsoft Internet Explorer")
			{
			window.returnValue = txt.innerText;
			}
			else
			{
			//alert(txt.value);
			opener.dialogWin.returnFunc(txt.value);
			}
			window.close();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 3px; WIDTH: 98%; POSITION: absolute; TOP: 3px; HEIGHT: 98%"
				cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td colspan="2" class="titleTableService">
						<asp:Label id="lblTitle" runat="server" BackColor="#666666"> Pembelian Equipment</asp:Label></td>
				</tr>
				<tr>
					<td colspan="2" height="1" background="../images/bg_hor_sales.gif"><img src="../images/bg_hor_sales.gif" height="1" border="0"></td>
				</tr>
				<tr>
					<td colspan="2" height="10"><img src="../images/dot.gif" height="1" border="0"></td>
				</tr>
				<TR valign="top">
					<TD width="35%" class="titleField">
						<asp:Label id="lblKomentar" runat="server">Detail Penjelasan Pesanan: </asp:Label></TD>
					<TD width="65%">
						<asp:TextBox id="txtComment" runat="server" TextMode="MultiLine" Rows="18" Cols="50"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="titleField">
						<asp:Label id="lblCreatedBy" runat="server" Visible="False">Diajukan oleh:</asp:Label></TD>
					<TD>
						<asp:Label id="lblCreatorName" runat="server" Visible="False"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="titleField">
						<asp:Label id="lblCreatedDate" runat="server" Visible="False">Pada Tanggal:</asp:Label></TD>
					<TD>
						<asp:Label id="lblDate" runat="server" Visible="False"></asp:Label></TD>
				</TR>
				<TR height="40">
					<td></td>
					<TD><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedText()" type="button" value="Simpan"
							name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

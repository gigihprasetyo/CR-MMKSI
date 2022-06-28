<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmComment.aspx.vb" Inherits="FrmComment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmComment</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; WIDTH: 98%; HEIGHT: 98%; TOP: 3px; LEFT: 3px"
				cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<tr>
					<td colspan="2" class="titleTableSales">
						<asp:Label id="lblTitle" runat="server" BackColor="#F28625">Comment</asp:Label></td>
				</tr>
				<tr>
					<td colspan="2" height="1" background="../images/bg_hor_sales.gif"><img src="../images/bg_hor_sales.gif" height="1" border="0"></td>
				</tr>
				<tr>
					<td colspan="2" height="10"><img src="../images/dot.gif" height="1" border="0"></td>
				</tr>
				<TR valign="top">
					<TD width="35%" class="titleField">
						<asp:Label id="lblKomentar" runat="server">Comment: </asp:Label></TD>
					<TD width="65%">
						<asp:TextBox id="txtComment" runat="server" TextMode="MultiLine" Rows="18" Cols="50"></asp:TextBox></TD>
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

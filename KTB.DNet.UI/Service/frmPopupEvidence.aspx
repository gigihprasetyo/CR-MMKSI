<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmPopupEvidence.aspx.vb" Inherits="frmPopupEvidence" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Keterangan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 336px; POSITION: absolute; TOP: -8px; HEIGHT: 306px"
				height="306" cellSpacing="0" cellPadding="0" width="336" border="0">
				<tr>
					<td class="titleTableService" colSpan="2"><asp:label id="lblTitle" runat="server" BackColor="#666666"> Daftar Bukti</asp:label></td>
				</tr>
				<tr>
					<td style="WIDTH: 816px" background="../images/bg_hor_sales.gif" colSpan="2" height="2"><IMG height="1" src="../images/bg_hor_sales.gif" border="0"></td>
				</tr>
				<tr>
					<td style="WIDTH: 816px" colSpan="2" height="2"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR vAlign="top">
					<TD class="titleField" style="WIDTH: 207px; HEIGHT: 243px" width="207"><asp:label id="lblKomentar" runat="server"> Keterangan : </asp:label></TD>
					<TD style="WIDTH: 526px; HEIGHT: 243px" width="526">
					<div id="div1" style="OVERFLOW: auto; HEIGHT: 220px">
						<asp:Label id="txtComment" runat="server" Width="223px"></asp:Label>
					</div>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="4"><INPUT id="btnCancel" style="WIDTH: 55px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

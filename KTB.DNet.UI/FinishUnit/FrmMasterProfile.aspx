<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterProfile.aspx.vb" Inherits="FrmMasterProfile" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMasterProfile</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="1"
				cellPadding="1" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 17px">
									<asp:Label id="lblHeader" runat="server">FAKTUR KENDARAAN - Customer Profile</asp:Label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Panel id="pnlMasterProfile" runat="server"></asp:Panel></TD>
				</TR>
				<TR>
					<TD>
						<asp:Button id="btnSimpan" runat="server" Text="Simpan"></asp:Button>
						<asp:Button id="BtnTutup" runat="server" Text="Tutup" CausesValidation="False"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

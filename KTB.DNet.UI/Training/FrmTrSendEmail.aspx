<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrSendEmail.aspx.vb" Inherits="FrmTrSendEmail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrSendEmail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<script language="javascript">
		//function Back() { window.history.go(-1); } </script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleTableParts3" align="center" colSpan="5">&nbsp;&nbsp;KIRIM-EMAIL 
						-&nbsp;&nbsp;Training</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5" height="10">&nbsp;</TD>
				</TR>
				<TR>
					<td></td>
					<td></td>
					<td></td>
					<td></td>
				</TR>
				<TR vAlign="top">
					<TD>&nbsp;</TD>
					<TD width="95%">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD colSpan="4" height="20"><b>Kepada Yth:</b></TD>
							</TR>
							<tr>
								<td colSpan="4">
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
										</TR>
									</TABLE>
								</td>
							</tr>
							<TR>
								<TD colSpan="4"><STRONG><asp:label id="lblDealer" runat="server"></asp:label></STRONG></TD>
							</TR>
							<TR>
								<TD colSpan="4"><STRONG><asp:label id="lblKota" runat="server"></asp:label></STRONG></TD>
							</TR>
							<TR vAlign="top">
								<TD width="20%"><b>Up</b></TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:listbox id="lsbUp" runat="server" AutoPostBack="True" Width="160px" Rows="3" SelectionMode="Multiple"></asp:listbox><asp:listbox id="lsbUPvisible" runat="server" Width="160px" Rows="3" SelectionMode="Multiple"
										Visible="False" Height="24px"></asp:listbox></TD>
							</TR>
							<TR vAlign="top">
								<TD><asp:label id="Label3" runat="server"><b>CC</b></asp:label></TD>
								<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlCC" runat="server" AutoPostBack="True" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 3px"></TD>
								<TD style="WIDTH: 1px"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><STRONG></STRONG></TD>
					<TD>
						<TABLE id="Table31" cellSpacing="1" cellPadding="2" width="100%" border="0">
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><STRONG></STRONG></TD>
					<TD style="WIDTH: 296px"><STRONG><asp:label id="lblerror" runat="server" Width="208px" Visible="False" Font-Bold="True" ForeColor="Red"
								Font-Size="Small">Email User Tidak Terdaftar</asp:label></STRONG></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD style="WIDTH: 296px"><asp:button id="btnkirim" runat="server" Text="Kirim Email"></asp:button>&nbsp;
						<INPUT id="btnCancel" style="WIDTH: 72px; HEIGHT: 21px" onclick="window.close()" type="button"
							value="Tutup" name="btnCancel"></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>

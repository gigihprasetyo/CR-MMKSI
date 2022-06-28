<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalSendEmail.aspx.vb" Inherits="FrmPartIncidentalSendEmail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmPartIncidentalSendEmail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		//function Back() { window.history.go(-1); } 
			function SendMail(){
				document.getElementById("btnSendMail").disabled=true;
				document.getElementById("btnkirim").click();
			}
		</script>
		
</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center" colSpan="5" class="titleTableParts3">&nbsp;&nbsp;KIRIM-EMAIL 
						-&nbsp;&nbsp;FORM PART INCIDENTIAL</TD>
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
				<TR valign="top">
					<TD>&nbsp;</TD>
					<TD width="95%">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD colSpan="4" height="20"><b>Kepada Yth:</b></TD>
							</TR>
							<tr>
								<td colspan="4">
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
										</TR>
									</TABLE>
								</td>
							</tr>
							<TR>
								<TD colSpan="4"><STRONG>TECHNICAL INFORMATION SEC.</STRONG></TD>
							</TR>
							<TR>
								<TD colSpan="4"><STRONG>MMKSI SPARE PART CIBITUNG</STRONG></TD>
							</TR>
							<TR valign="top">
								<TD WIDTH="20%"><b>Up</b></TD>
								<TD WIDTH="1%">:</TD>
								<TD WIDTH="75%"><asp:listbox id="lsbUp" runat="server" SelectionMode="Multiple" Rows="3" Width="160px" AutoPostBack="True"></asp:listbox><asp:listbox id="lsbUPvisible" runat="server" SelectionMode="Multiple" Rows="3" Width="160px"
										Height="24px" Visible="False"></asp:listbox></TD>
							</TR>
							<TR valign="top">
								<TD><asp:label id="Label3" runat="server"><b>CC</b></asp:label></TD>
								<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlCC" runat="server" Width="144px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 3px"><asp:label id="lblJabatan" runat="server" Visible="False"><b>Jabatan</b></asp:label></TD>
								<TD style="WIDTH: 1px"><asp:label id="Label2" runat="server" Visible="False">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlPositionCC" runat="server" Width="144px" AutoPostBack="True" Visible="False">
<asp:ListItem Value="SparePart Group Head">SparePart Group Head</asp:ListItem>
<asp:ListItem Value="Manager">Manager</asp:ListItem>
<asp:ListItem Value="Kepala Seksi">Kepala Seksi</asp:ListItem>
<asp:ListItem Value="Direktur">Direktur</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><STRONG></STRONG></TD>
					<TD>
						<TABLE id="Table31" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR height="20">
								<TD style="WIDTH: 14px"><b>Sebagai</b></TD>
								<TD width="1%">:</TD>
								<TD width="90%"><asp:label id="lblas" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><STRONG></STRONG></TD>
					<TD style="WIDTH: 296px"><STRONG><asp:label id="lblerror" runat="server" Width="272px" Visible="False" Font-Bold="True" Font-Size="Small"
								ForeColor="Red">Email User Tidak Terdaftar</asp:label></STRONG></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD style="WIDTH: 296px">
						<input type="button" id="btnSendMail" value="Kirim Email" onclick="SendMail();" />
						<asp:button id="btnkirim" runat="server" Text="Kirim Email" style="display:none;"></asp:button>						
						&nbsp;
						<INPUT id="btnCancel" style="WIDTH: 72px; HEIGHT: 21px" onclick="window.close()" type="button"
							value="Tutup" name="btnCancel"></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>

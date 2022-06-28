<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalDisplayDocument.aspx.vb" Inherits="FrmPartIncidentalDisplayDocument" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPartIncidentalDisplayDocument</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
        //function Back()
		//{
		//	window.history.go(-1);
		//}
		</script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleTableParts3" style="HEIGHT: 15px" align="center" colSpan="5">DISPLAY-DOKUMEN 
						-&nbsp;&nbsp;PART &nbsp;INCIDENTIAL DISPLAY DOKUMEN</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="5" height="10">&nbsp;</TD>
				</TR>
				<TR>
					<td></td>
					<td></td>
					<TD style="WIDTH: 18px" align="center" width="18" bgColor="#cdcdcd" rowSpan="7"><IMG height="1" src="../images/dot.gif" border="0"></TD>
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
								<TD colSpan="4"><STRONG>TECHNICAL INFORMATION SEC.</STRONG></TD>
							</TR>
							<TR>
								<TD colSpan="4"><STRONG>MMKSI SPARE PART CIBITUNG</STRONG></TD>
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
								<TD style="WIDTH: 3px"><asp:label id="lblJabatan" runat="server" Visible="False"><b>Jabatan</b></asp:label></TD>
								<TD style="WIDTH: 1px"><asp:label id="Label2" runat="server" Visible="False">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlPositionCC" runat="server" AutoPostBack="True" Width="144px" Visible="False">
										<asp:ListItem Value="SparePart Group Head">SparePart Group Head</asp:ListItem>
										<asp:ListItem Value="Manager">Manager</asp:ListItem>
										<asp:ListItem Value="Kepala Seksi">Kepala Seksi</asp:ListItem>
										<asp:ListItem Value="Direktur">Direktur</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="5%"></TD>
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
					<TD style="WIDTH: 296px"><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD style="WIDTH: 296px"><asp:button id="btnDisplay" runat="server" Text="Display Dokumen"></asp:button>&nbsp;
						<asp:button id="btnBack" runat="server" Width="70px" Text="Kembali"></asp:button></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</BODY>
</HTML>

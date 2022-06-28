<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmViewTrClassRegistration2.aspx.vb" Inherits="FrmViewTrClassRegistration2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmViewTrClassRegistration2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="740" border="0">
				<tr>
					<td class="titlePage">TRAINING&nbsp;- Pendaftaran - Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<tr>
								<td class="generalTable" colspan="4"><strong>Informasi Pendaftaran</strong></td>
							</tr>
							<TR>
								<TD class="titleField">Tanggal Pendaftaran</TD>
								<TD>
									<asp:TextBox id="txtTglPendaftaran" runat="server" Enabled="False"></asp:TextBox></TD>
							</TR>
							<tr>
								<td class="generalTable" colspan="4"><strong>Informasi Siswa</strong></td>
							</tr>
							<TR>
								<TD class="titleField" width="20%">No Reg</TD>
								<TD width="30%"><asp:textbox id="txtRegistrationCode" runat="server" onkeypress="return HtmlCharUniv(event)"></asp:textbox></TD>
								<td width="20%"></td>
								<td width="30%"></td>
							</TR>
							<TR>
								<TD class="titleField">Nama Siswa</TD>
								<TD>
									<asp:textbox id="txtTraineeName" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"
										Width="150px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>
									<asp:textbox id="txtDealerName" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"
										Width="150px"></asp:textbox></TD>
							</TR>
							<tr>
								<td class="generalTable" colspan="4"><strong>Informasi Kelas</strong></td>
							</tr>
							<TR>
								<TD class="titleField">Kode Kelas</TD>
								<TD>
									<asp:textbox id="txtClassCode" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Kelas</TD>
								<TD>
									<asp:textbox id="txtClassName" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"
										Width="200px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Lokasi</TD>
								<TD>
									<asp:textbox id="txtLocation" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"
										Width="200px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Mulai</TD>
								<TD>
									<asp:textbox id="txtStartDate" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Selesai</TD>
								<TD>
									<asp:textbox id="txtFinishDate" runat="server" Enabled="False" onkeypress="return HtmlCharUniv(event)"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD><asp:label id="lblLastStatus" runat="server" Font-Size="Smaller"></asp:label></TD>
							</TR>
							<TR>
								<TD><STRONG>Perubahan Terakhir</STRONG>
								</TD>
								<TD><asp:label id="lblLastChange" runat="server" Font-Size="Smaller"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<asp:button id="btnBack" runat="server" Width="60px" Text="Kembali"></asp:button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Visible="False"></asp:button></td>
				</tr>
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
	</body>
</HTML>

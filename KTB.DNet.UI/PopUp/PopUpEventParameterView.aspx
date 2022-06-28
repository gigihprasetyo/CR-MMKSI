<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventParameterView.aspx.vb" Inherits="PopUpEventParameterView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpEventParameterView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">EVENT&nbsp;-&nbsp;Parameter</TD>
				</TR>
				<TR>
					<TD class="titlePage"></TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="25%">&nbsp; Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="74%"><asp:label id="lblKodeDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Area</TD>
								<TD>:</TD>
								<TD><asp:label id="lblSalesmanArea" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Periode Kegiatan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblEventPeriod" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Jenis Kegiatan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblActivityType" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Nama Kegiatan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblEventName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Material</TD>
								<TD>:</TD>
								<TD><asp:label id="lblFileMaterial" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Juklak</TD>
								<TD>:</TD>
								<TD><asp:label id="lblJuklak" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 1</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung1" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 2</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung2" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 3</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung3" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 4</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung4" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 5</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung5" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 6</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung6" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 7</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung7" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 8</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung8" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 9</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung9" Runat="server"></asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; File Pendukung 10</TD>
								<TD>:</TD>
								<TD><asp:HyperLink ID="hplFilePendukung10" Runat="server"></asp:HyperLink></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td align="center"><INPUT id="btnTutup" style="WIDTH: 77px; HEIGHT: 24px" onclick="window.close()" type="button"
							value="Tutup">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClassInformation.aspx.vb" Inherits="PopUpClassInformation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpClassInformation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">TRAINING&nbsp;-&nbsp;Informasi Kelas</TD>
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
								<TD class="titleField" width="25%">&nbsp; Kode Kelas</TD>
								<TD width="1%">:</TD>
								<TD width="74%"><asp:label id="lblClassCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Nama Kelas</TD>
								<TD>:</TD>
								<TD><asp:label id="lblClassName" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">&nbsp; Tipe Kelas</TD>
								<TD>:</TD>
								<TD><asp:label id="lblClassType" runat="server"></asp:label></TD>
							</TR>
                            <TR id="trMRTC" runat="server">
								<TD class="titleField">&nbsp; MRTC</TD>
								<TD>:</TD>
								<TD><asp:label id="lblMRTC" runat="server"></asp:label></TD>
							</TR>
							<TR id="trCtg" runat="server" visible="false">
								<TD class="titleField">&nbsp; Kode Kategori</TD>
								<TD>:</TD>
								<TD><asp:label id="lblCourseID" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">&nbsp; Nama Lokasi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblLocationName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Lokasi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblLocation" runat="server"></asp:label></TD>
							</TR>
                            <TR id="trPenginapan" runat="server">
								<TD class="titleField">&nbsp; Penginapan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblPenginapan" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">&nbsp; Kota</TD>
								<TD>:</TD>
								<TD><asp:label id="lblKota" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Pengajar 1</TD>
								<TD>:</TD>
								<TD><asp:label id="lblTrainer1" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Pengajar 2</TD>
								<TD>:</TD>
								<TD><asp:label id="lblTrainer2" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Pengajar 3</TD>
								<TD>:</TD>
								<TD><asp:label id="lblTrainer3" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">&nbsp; Kapasitas</TD>
								<TD>:</TD>
								<TD><asp:label id="lblCapasity" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">&nbsp; Tahun Fiskal</TD>
								<TD>:</TD>
								<TD><asp:label id="lblFiscalYear" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Tanggal Mulai</TD>
								<TD>:</TD>
								<TD><asp:label id="lblStartDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Tanggal Selesai</TD>
								<TD>:</TD>
								<TD><asp:label id="lblFinishDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp; Keterangan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDescription" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField">&nbsp; Status</TD>
								<TD>:</TD>
								<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <INPUT id="btnTutup" type="button" onclick="window.close()" value="Tutup" style="WIDTH: 77px; HEIGHT: 24px">
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>

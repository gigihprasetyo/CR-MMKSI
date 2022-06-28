<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBabitProposalUploadRealization.aspx.vb" Inherits="FrmBabitProposalUploadRealization"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Upload Realization</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">BABIT - Upload Realization</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 146px">No Pengajuan</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblNoPengajuan" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Nama Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Kota</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblCity" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Propinsi</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblProvince" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Nomor Alokasi</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblNoPerjanjian" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Periode</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblPeriode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Upload File</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><input id="UploadFileRealization" style="WIDTH: 296px; HEIGHT: 20px" type="file" size="30"
										runat="server">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<asp:button id="btnUpload" Runat="server" Text="Upload"></asp:button><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Kembali"
				name="btnCancel" runat="server">
		</form>
	</body>
</HTML>

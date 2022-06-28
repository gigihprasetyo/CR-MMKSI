<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SalesmanCreateAnnouncement.aspx.vb" Inherits="SalesmanCreateAnnouncement" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SalesmanCreateAnnouncement</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">PELATIHAN TENAGA 
						PENJUAL&nbsp;-&nbsp;Buat Pengumuman
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 214px; HEIGHT: 19px" width="214">Kode Training</TD>
								<TD style="HEIGHT: 19px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 19px" width="25%" colSpan="3"><asp:dropdownlist id="ddlTrainingCode" runat="server" Width="168px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Nama Training</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><asp:textbox id="txtTrainingTitle" runat="server" Width="280px" MaxLength="30" size="22"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 18px" width="215">Periode 
									Training</TD>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<TD style="HEIGHT: 18px" width="25%" colSpan="3">
									<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
										<TR>
											<TD><CC1:INTICALENDAR id="icTglCreate" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></TD>
											<TD>s/d</TD>
											<TD><CC1:INTICALENDAR id="icTglCreate2" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Jenis 
									Training</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><asp:textbox id="txtSalesmanTrainingType" runat="server" ReadOnly="True" Width="160px" MaxLength="20"
										size="22"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 62px" width="215">Isi 
									Pemberitahuan</TD>
								<TD style="HEIGHT: 62px" width="1%">:</TD>
								<TD style="HEIGHT: 62px" width="25%" colSpan="3"><asp:textbox id="txtAnnouncementContent" runat="server" Width="408px" Height="62px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Upload Surat 
									Pengumuman</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><INPUT onkeypress="return false;" id="fileUploadAnnouncementFileName" style="WIDTH: 296px; HEIGHT: 20px"
										type="file" size="30" name="fileUpload" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Upload 
									Material Training</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><INPUT onkeypress="return false;" id="FileUploadMaterialFileName" style="WIDTH: 296px; HEIGHT: 20px"
										type="file" size="30" name="fileUpload" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 20px" width="215"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnRelease" runat="server" Width="60px" Text="Release" CausesValidation="False"
										Enabled="False"></asp:button></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

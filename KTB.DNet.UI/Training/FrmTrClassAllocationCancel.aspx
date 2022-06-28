<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClassAllocationCancel.aspx.vb" Inherits="FrmTrClassAllocationCancel" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmNewTrTrainee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
		
		function ShowPPCourseSelection()
		{
			showPopUp('../General/../PopUp/PopUpCourse.aspx','',550,760,courseSelection);
		}
		
		function courseSelection(selectedCourse)
		{
			
			var txtKodekategori = document.getElementById("txtKodeKategori");
			txtKodekategori.value = selectedCourse;	
		}
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" style="WIDTH: 825px; HEIGHT: 20px">
						<asp:Label ID="lblHeaderCaption" Runat="server"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 826px" vAlign="top">
						<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 300px" cellSpacing="1" cellPadding="1" width="800"
							border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1"></TD>
								<TD width="1%" height="1"></TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1">Jumlah 
									dialokasikan</TD>
								<TD width="1%" height="1">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428" align="left">
									<asp:Label id="lblAllocated" runat="server" Width="0px" Visible="False">0</asp:Label>
									<asp:TextBox ID="txtAllocated" Runat="server" Width="72px"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1" vAlign="top">
									<asp:Label ID="lblReason" Runat="server"></asp:Label>
								</TD>
								<TD width="1%" height="1" vAlign="top">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtReason" runat="server" Width="424px"
										MaxLength="500" Height="56px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1"></TD>
								<TD width="1%" height="1"></TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428">
									<asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>
									<asp:button id="btnBatal" runat="server" width="60px" CausesValidation="False" Text="Batal"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1" colspan="3"><hr style="WIDTH: 605px; HEIGHT: 2px" SIZE="2">
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1">Detail Kelas :
								</TD>
								<TD width="1%" height="1"></TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1">Kode Kelas</TD>
								<TD width="1%" height="1">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeKelas" runat="server" MaxLength="20"
										Width="428px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px" width="177" height="1">Nama Kelas</TD>
								<TD width="1%" height="1">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 1px" noWrap width="428"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNamaKelas" runat="server" MaxLength="50"
										Width="428px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px; HEIGHT: 24px" width="177" height="6">Lokasi</TD>
								<TD style="HEIGHT: 6px" width="1%" height="6">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 6px" noWrap width="428"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtLokasi" runat="server" MaxLength="100"
										Width="428px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px; HEIGHT: 24px" width="177" height="6">Capacity</TD>
								<TD style="HEIGHT: 6px" width="1%" height="6">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 6px" noWrap width="428">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCapacity" runat="server" Width="120px"
										MaxLength="100" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px; HEIGHT: 12px">Tanggal Mulai</TD>
								<TD style="HEIGHT: 12px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 12px" width="428"><cc1:inticalendar id="ICTanggalMulai" runat="server" TextBoxWidth="80" Enabled="False"></cc1:inticalendar>&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px; HEIGHT: 17px">&nbsp;<STRONG>Tanggal Selesai 
										: </STRONG>
								</TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 17px" width="428"><cc1:inticalendar id="ICTanggalSelesai" runat="server" TextBoxWidth="80" Enabled="False"></cc1:inticalendar>&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 177px"><asp:button id="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:button></TD>
								<TD></TD>
								<TD style="WIDTH: 381px" width="381" colSpan="2">
									<P>&nbsp;</P>
								</TD>
							</TR>
						</TABLE>
					</TD>
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
	</body>
</HTML>

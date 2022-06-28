<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmNewTrTrainee.aspx.vb" Inherits="FrmNewTrTrainee" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmNewTrTrainee</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function TxtBlurName(objTxt) {
		        omitSomeCharacterExcludeSingleQuote(objTxt, '<>?*%$;');
		    }

		    function ShowPPDealerBranchSelection() {
		    var lblDealer = document.getElementById("lblDealerCode");
		    var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
		    showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
		}

		function TemporaryOutlet(selectedDealer) {
		    if (selectedDealer.indexOf(";") > 0) {
		        var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		        txtDealerSelection.value = selectedDealer.split(";")[0];
		    }
		    else {
		        var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		        txtDealerSelection.value = selectedDealer;
		    }
		}

		function ShowJobPosSelection()
		{
			//alert('bisa');
		    showPopUp('../PopUp/PopUpJobPosition.aspx?category=2', '', 600, 600, JobPosSelection);
		}
		function JobPosSelection(selectedJobPos)
		{
			var txtPosisi = document.getElementById("txtJobPosition");
			selectedJobPos = selectedJobPos + ';';
			
			var arrValue = selectedJobPos.split(';');
			txtPosisi.value = arrValue[0];
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" encType="multipart/form-data">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" style="WIDTH: 825px">Training&nbsp;- Pendaftaran Siswa Baru</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 826px" vAlign="top">
						<TABLE id="Table2" style="WIDTH: 742px; HEIGHT: 232px" cellSpacing="1" cellPadding="1"
							width="742" border="0">
							<TR>
								<TD class="titleField" width="24%" height="23">Kode Dealer</TD>
								<TD width="1%" height="23">:</TD>
								<TD noWrap width="420" style="HEIGHT: 23px">
									<asp:Label id="lblDealerCode" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="22">Nama Dealer</TD>
								<TD width="1%" height="22">:</TD>
								<TD noWrap width="420" style="HEIGHT: 22px">
									<asp:Label id="lblDealerName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="22" style="HEIGHT: 22px">Kota</TD>
								<TD width="1%" height="22" style="HEIGHT: 22px">:</TD>
								<TD noWrap width="420" style="HEIGHT: 22px">
									<asp:Label id="lblCity" runat="server"></asp:Label></TD>
							</TR>
                            <TR>
								<TD class="titleField" width="24%" height="22" style="HEIGHT: 22px">Kode Cabang Dealer</TD>
								<TD width="1%" height="22" style="HEIGHT: 22px">:</TD>
								<TD noWrap width="420" style="HEIGHT: 22px">
                                    <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="4" style="HEIGHT: 4px">Nama Siswa</TD>
								<TD width="1%" height="4" style="HEIGHT: 4px">:</TD>
								<TD noWrap width="420" style="HEIGHT: 4px">
									<asp:textbox id="txtName" runat="server" MaxLength="50" onblur="TxtBlurName('txtName');" 
										Width="400px"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" ErrorMessage="Nama Siswa harus diisi"
										ControlToValidate="txtName" Display="Dynamic">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 18px">Tanggal Lahir</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 18px" valign="middle">
									<table cellpadding="0" cellspacing="0"  border="0">
										<tr>
											<td><cc1:inticalendar id="icBirthDate" runat="server"></cc1:inticalendar></td>
											<td>&nbsp; Format dd/mm/yyyy</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Jenis Kelamin</TD>
								<TD>:</TD>
								<TD style="WIDTH: 428px" width="428"><asp:dropdownlist id="ddlGender" tabIndex="7" runat="server"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" ControlToValidate="ddlGender" ErrorMessage="Jenis Kelamin Harus dipilih">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px">Ukuran Baju</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<TD style="HEIGHT: 16px" width="420"><asp:dropdownlist id="ddlShirtSize" runat="server"></asp:dropdownlist></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 5px">Email</TD>
								<TD style="HEIGHT: 5px">:</TD>
								<TD width="420" style="HEIGHT: 5px">
                                    <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEmail" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email harus diisi">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Format email salah" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
								</TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 5px">No. KTP</TD>
								<TD style="HEIGHT: 5px">:</TD>
								<TD width="420" style="HEIGHT: 5px">
                                    <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKTP" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtKTP" ErrorMessage="No. KTP harus diisi">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtKTP" ErrorMessage="Format No KTP salah" ValidationExpression="[0-9]{16}"></asp:RegularExpressionValidator>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Mulai Bekerja</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><cc1:inticalendar id="ICStartWork" runat="server" Saturday="True"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px">Posisi Pekerjaan</TD>
								<TD style="HEIGHT: 15px">:</TD>
								<TD width="420" style="HEIGHT: 15px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJobPosition" runat="server"
										Width="376px" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Display="Dynamic" ControlToValidate="txtJobPosition"
										ErrorMessage="Posisi Pekerjaan harus diisi">*</asp:requiredfieldvalidator>
									<asp:label id="lblSearchJobPos" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 5px">Level Pendidikan</TD>
								<TD style="HEIGHT: 5px">:</TD>
								<TD width="420" style="HEIGHT: 5px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEducationLevel" runat="server" Width="400px"
										MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ControlToValidate="txtEducationLevel"
										ErrorMessage="Level Pendidikan harus diisi">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px">Foto (Maks. 20KB)</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px" width="420"><INPUT id="photoSrc" style="WIDTH: 400px; HEIGHT: 20px" type="file" size="47" name="photoSrc"
										onkeydown="return false" runat="server">
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD width="420" colspan="2">
									<P><asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>&nbsp;
										<asp:button id="btnBatal" runat="server" width="60px" Text="Batal" CausesValidation="False"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
						<asp:ValidationSummary id="messageValidationSummary" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
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

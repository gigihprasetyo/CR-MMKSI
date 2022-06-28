<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmParameterEvent.aspx.vb" Inherits="FrmParameterEvent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ParameterEvent</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 31px">EVENT&nbsp;-&nbsp;Parameter Event</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%">Kode Dealer</td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
													runat="server" Width="312px"></asp:textbox></td>
											<td><asp:label id="lblSearchDealer" runat="server">
													<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
														border="0"></asp:label></td>
											<td><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Kode Dealer harus diisi"
													ControlToValidate="txtDealerCode" Display="Dynamic">*</asp:requiredfieldvalidator></td>
										</tr>
									</table>
								</TD>
								<td class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Area</td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlSalesmanArea" Width="142px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Periode Kegiatan</TD>
								<TD style="WIDTH: 1px" width="1">:</TD>
								<TD width="82%" colSpan="5">
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><cc1:inticalendar id="calDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
											<TD>&nbsp;&nbsp;s.d&nbsp;&nbsp;</TD>
											<TD><cc1:inticalendar id="calSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Jenis Kegiatan</TD>
								<TD style="WIDTH: 1px" width="1">:</TD>
								<TD colSpan="5">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<TD><asp:dropdownlist id="ddlJenisKegiatan" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
											<td>
												<TABLE id="tblCategoryModelType" cellPadding="0" border="0" runat="server">
													<TR>
														<TD class="titleField">Kategori</TD>
														<TD><asp:dropdownlist id="ddlCategory" Width="100px" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
														<TD class="titleField">Model</TD>
														<TD><asp:dropdownlist id="ddlModel" Width="100px" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
														<TD class="titleField">Tipe</TD>
														<TD><asp:dropdownlist id="ddlType" Width="100px" Runat="server"></asp:dropdownlist></TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Nama Kegiatan</TD>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px" colSpan="5">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:textbox id="txtNamaKegiatan" Runat="server" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Nama kegiatan harus diisi"
													ControlToValidate="txtNamaKegiatan" Display="Dynamic">*</asp:requiredfieldvalidator></td>
											<td class="titleField" style="WIDTH: 50px; HEIGHT: 18px; TEXT-ALIGN: right">Tahun</td>
											<TD style="WIDTH: 10px; HEIGHT: 18px; TEXT-ALIGN: center" width="1">:</TD>
											<td><asp:dropdownlist id="ddlYear" Runat="server"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Material</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFileMaterial" type="file" name="fuFileMaterial" runat="server">
									<asp:regularexpressionvalidator id="RegUpload1" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFileMaterial"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvFileMaterial" runat="server" ErrorMessage="Upload file material harus diisi"
										ControlToValidate="fuFileMaterial" Display="Dynamic">*</asp:requiredfieldvalidator>
									<asp:linkbutton id="lnbFileMaterial" Runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload Juklak</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuJuklak" type="file" name="fuJuklak" runat="server">
									<asp:regularexpressionvalidator id="RegUpload2" runat="server" ErrorMessage="Invalid" ControlToValidate="fuJuklak"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvJulkak" runat="server" ErrorMessage="Upload juklak harus diisi" ControlToValidate="fuJuklak"
										Display="Dynamic">*</asp:requiredfieldvalidator>
									<asp:linkbutton id="lnbJuklak" Runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 1</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung1" type="file" name="fuFilePendukung1" runat="server">
									<asp:regularexpressionvalidator id="RegUpload3" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung1"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung1" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 2</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung2" type="file" name="fuFilePendukung2" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung2"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung2" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 3</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung3" type="file" name="fuFilePendukung3" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator2" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung3"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung3" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 4</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung4" type="file" name="fuFilePendukung4" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator3" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung4"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung4" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 5</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung5" type="file" name="fuFilePendukung5" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator4" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung5"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung5" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 6</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung6" type="file" name="fuFilePendukung6" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator5" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung6"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung6" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 7</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung7" type="file" name="fuFilePendukung7" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator6" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung7"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung7" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 8</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung8" type="file" name="fuFilePendukung8" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator7" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung8"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung8" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 9</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung9" type="file" name="fuFilePendukung9" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator8" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung9"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung9" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Upload File Pendukung 10</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5"><input id="fuFilePendukung10" type="file" name="fuFilePendukung10" runat="server">
									<asp:regularexpressionvalidator id="Regularexpressionvalidator9" runat="server" ErrorMessage="Invalid" ControlToValidate="fuFilePendukung10"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
									<asp:linkbutton id="lnbFilePendukung10" runat="server" Visible="False"></asp:linkbutton></TD>
							</TR>
							<TR>
								<td class="titleField"></td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD colSpan="5"><asp:button id="btnSave" runat="server" Width="64px" Text="Simpan"></asp:button><INPUT id="btnBack" onclick="window.history.back();return false;" type="button" value="Kembali"
										name="btnBack" runat="server"></TD>
							</TR>
						</table>
						<asp:label id="lblError" runat="server" Width="624px" EnableViewState="False" ForeColor="Red"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

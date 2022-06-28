<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanHeader.aspx.vb" Inherits="FrmSalesmanHeader" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		    /* Deddy H	validasi value *********************************** */
		    /* ini untuk handle char yg tdk diperbolehkan, saat paste */
		    function TxtBlur(objTxt) {
		        omitSomeCharacter(objTxt, '<>?*%$;');
		    }
		    function TxtBlurName(objTxt) {
		        omitSomeCharacterExcludeSingleQuote(objTxt, '<>?*%$;');
		    }
		    /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
		    function TxtKeypress() {
		        return alphaNumericExcept(event, '<>?*%$;');
		    }
		</script>
		<script language="javascript" type="text/javascript">

		    function HandleSparePartPanel() {
		        var chkSparePart = document.getElementById("SparePartIndicator");
		        var pnl = document.getElementById("pnlSparePart");
		        if (chkSparePart.checked) {
		            pnl.style.display = "block";
		        }
		        else {
		            pnl.style.display = "none";
		        }

		    }

		    function ShowPopUpSuperior() {
		        var ddlJobPositionDesc = document.getElementById("ddlJobPositionDesc");
		        var Position = ddlJobPositionDesc.value;
		        var lblDealerCodes = document.getElementById("lblDealerCode");
		        var oDealerSalesman = lblDealerCodes.innerHTML
		        if (ddlJobPositionDesc.value == '') {
		            alert('Kategori harus dipilih dulu !');
		            return;
		        }
		        //alert('../PopUp/PopUpSalesman.aspx?PositionID=' + Position);
		        showPopUp('../PopUp/PopUpSalesman.aspx?PositionID=' + Position + '&DealerSalesman=' + oDealerSalesman, '', 600, 600, SuperiorSelection);
		    }

		    function SuperiorSelection(SelectedSuperior) {

		        var tempParam = SelectedSuperior.split(';');
		        var txtSuperior = document.getElementById("txtSuperior");
		        var txtSuperiorName = document.getElementById("txtSuperiorName");
		        //alert(txtSuperior);

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            txtSuperior.innerText = tempParam[0];
		            txtSuperiorName.innerText = tempParam[1];
		        }
		        else {
		            txtSuperior.value = tempParam[0];
		            txtSuperiorName.value = tempParam[1];
		        }
		    }


		    function ShowPopUpDealerHistory() {
		        showPopUp('../PopUp/PopUpSelectingDealer.aspx', '', 600, 600, DealerSelectionHistory);
		    }


		    function DealerSelectionHistory(SelectedHistory) {
		        var dtgHistory = document.getElementById("dtgHistory");
		        var indek = GetCurrentInputIndex(dtgHistory);
		        //alert(indek);
		        var tempParam = SelectedHistory.split(';');
		        var KodeArea = dtgHistory.rows[indek].getElementsByTagName("INPUT")[0];
		        var DescArea = dtgHistory.rows[indek].getElementsByTagName("SPAN")[1];

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            KodeArea.innerText = tempParam[0];
		            DescArea.innerHTML = tempParam[1];
		        }
		        else {
		            KodeArea.value = tempParam[0];
		            DescArea.value = tempParam[1];
		        }
		    }

		    function ShowPopUpSalesManArea() {
		        //var myDate = new Date( );
		        //showPopUp('../PopUp/PopUpSalesmanArea.aspx?time='+myDate.getTime( ),'',600,600,AreaSelection);
		        showPopUp('../PopUp/PopUpSalesmanArea.aspx', '', 600, 600, AreaSelection);
		    }


		    function AreaSelection(SelectedArea) {

		        var dtgArea = document.getElementById("dtgArea");
		        var indek = GetCurrentInputIndex(dtgArea);
		        var tempParam = SelectedArea.split(';');
		        var KodeArea = dtgArea.rows[indek].getElementsByTagName("INPUT")[0];
		        var DescArea = dtgArea.rows[indek].getElementsByTagName("SPAN")[1];

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            KodeArea.innerText = tempParam[0];
		            DescArea.innerHTML = tempParam[1];
		        }
		        else {
		            KodeArea.value = tempParam[0];
		            DescArea.value = tempParam[1];
		        }
		    }

		    function GetCurrentInputIndex(dtg) {
		        //var dtgArea = document.getElementById("dtgArea");
		        var currentRow;
		        var index = 0;
		        var inputs;
		        var indexInput;
		        for (index = 0; index < dtg.rows.length; index++) {
		            inputs = dtg.rows[index].getElementsByTagName("INPUT");

		            if (inputs != null && inputs.length > 0) {
		                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
		                    if (inputs[indexInput].type != "hidden")
		                        return index;
		                }
		            }
		        }
		        return -1;
		    }


		    function SetDealer(Parameter) {
		        arrParam = Parameter.split(";");
		        document.Form1.txtDealerCode.value = trim(arrParam[0]);
		        document.Form1.txtDealerName.value = trim(arrParam[1]);

		    }

		    function ShowPPDealerSelection() {
		        showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=' + true, '', 600, 600, DealerSelection);
		    }
		    function DealerSelection(selectedDealer) {
		        var txtDealerCodeSelection = document.getElementById("txtDealerCode");
		        var txtDealerNameSelection = document.getElementById("txtDealerName");
		        var arrValue = selectedDealer.split(';');

		        txtDealerCodeSelection.value = arrValue[0];
		        txtDealerNameSelection.value = arrValue[1];
		    }

		    function ShowJobPosSelection() {
		        showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=1', '', 600, 600, JobPosSelection);
		    }
		    function JobPosSelection(selectedJobPos) {
		        var txtJobPos = document.getElementById("txtJobPosition");
		        var txtJobPosDesc = document.getElementById("txtJobPositionDesc");
		        var arrValue = selectedJobPos.split(';');
		        txtJobPos.value = arrValue[0];
		        txtJobPosDesc.value = arrValue[1];

		    }
		    // popUp City
		    function ShowCitySelection() {
		        showPopUp('../PopUp/PopUpCity.aspx', '', 600, 600, CitySelection);
		    }
		    function CitySelection(selectedCity) {
		        var txtKota = document.getElementById("txtKota");
		        var arrValue = selectedCity.split(';');
		        txtKota.value = arrValue[1];
		    }

		    // comment
		    function ShowLeadJobPosSelection() {
		        showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=1', '', 600, 600, LeadJobPosSelection);
		    }
		    function LeadJobPosSelection(selectedLeadJobPos) {
		        var txtLeadJobPos = document.getElementById("txtLeadJobPosition");
		        var txtLeadJobPosDesc = document.getElementById("txtLeadJobPositionDesc");
		        var arrValue = selectedLeadJobPos.split(';');
		        txtLeadJobPos.value = arrValue[0];
		        txtLeadJobPosDesc.value = arrValue[1];
		    }

		    function ShowSalesmanSelection() {
		        var lblSalesmanCode = document.getElementById("lblSalesmanCode");
		        alert(lblSalesmanCode.innerText);
		        showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?SSCode=' + lblSalesmanCode.innerText, '', 600, 600, SalesmanSelection);
		    }

		    function SalesmanSelection(SelectedSalesman) {
		        var tempParam = SelectedSalesman.split(';');
		        var txtLeaderName = document.getElementById("txtLeaderName");
		        var txtLeadJobPositionDesc = document.getElementById("txtLeadJobPositionDesc");
		        var txtLeadJobPosition = document.getElementById("txtLeadJobPosition");
		        var txtSalesmanCode = document.getElementById("txtSalesmanCode");

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            txtLeaderName.innerText = tempParam[1];
		            txtLeadJobPositionDesc.innerText = tempParam[3];
		        }
		        else {
		            txtLeaderName.value = tempParam[1];
		            txtLeadJobPositionDesc.value = tempParam[3];
		        }
		        txtSalesmanCode.value = tempParam[0]
		        txtLeadJobPosition.value = tempParam[2];
		    }

		    function ShowSalesmanResign() {
		        var txtMode = document.getElementById("txtMode");
		        showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?Code=Resign' + '&Mode=' + txtMode.value, '', 500, 760, SalesmanResignSelection);
		    }

		    function ShowCity() {
		        showPopUp('../PopUp/PopUpCity.aspx', '', 600, 600, CityBirthSelection);
		    }

		    function CityBirthSelection(selectedBirth) {
		        var tempParam = selectedBirth.split(';');
		        var txtKotaLahir = document.getElementById("txtPlaceOfBirth");
		        var hdnID = document.getElementById("hdnKotaLahir");
		        txtKotaLahir.value = tempParam[1];
		        hdnID.value = tempParam[0];
		    }


		    function SalesmanResignSelection(selectedVal) {
		        var tempParam = selectedVal.split(';');
		        var txtRefSalesman = document.getElementById("txtRefSalesman");
		        txtRefSalesman.value = tempParam[0];
		    }
		</script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="700" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px"><asp:label id="lblPageTitle" runat="server"></asp:label></td>
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
								<TD class="titleField" width="260">Indicator</TD>
								<TD width="102"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD width="263"><asp:dropdownlist id="ddlIndicator" runat="server"></asp:dropdownlist><asp:label id="lblIndicator" runat="server" Visible="False"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" ErrorMessage="Indicator Harus dipilih"
										ControlToValidate="ddlIndicator">*</asp:requiredfieldvalidator></TD>
								<TD colSpan="3"><asp:label id="lblRef" runat="server">Ref :</asp:label>&nbsp;&nbsp;&nbsp;
									<asp:textbox onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" id="txtRefSalesman"
										onblur="omitSomeCharacter('txtRefKodePelanggan','<>?*%$;');" runat="server" MaxLength="10"
										Width="88px"></asp:textbox><asp:label id="lbtnRefSalesman" onclick="ShowSalesmanResign();" Runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label><asp:linkbutton id="lnkReloadSalesman" runat="server" CausesValidation="False">
										<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"></asp:linkbutton>

								</TD>
							</TR>
							<TR vAlign="top">
								<TD clasKode Dealer</TD>
								<TD width="102"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD width="263"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
								<TD colSpan="3" rowSpan="8">
									<table cellSpacing="1" cellPadding="2" width="100%" border="0">
										<tr>
											<td>
                                                <span id="labelPhoto">Insert KTP</span></td>
										</tr>
                                        
										<tr>
											<td>
												<DIV id="divPhoto" style="OVERFLOW: auto; WIDTH: 300px; HEIGHT: 150px" align="center"><asp:image onclick="imageOnClick()" id="photoView" runat="server" Width="300px" Height="150px"></asp:image></DIV>
												<asp:button id="btnRemoveFile" Visible="False" Runat="server" CausesValidation="False" Text="Remove Picture" Width="113px"></asp:button><asp:linkbutton id="lblRemoveImage" Runat="server" CausesValidation="False" text="Hapus Photo" CommandName="deleteImage" Visible="False"></asp:linkbutton></td>
										</tr>
										<tr>
											<td><INPUT onkeypress="return false;" id="photoSrc" tabIndex="19" type="file" size="29" name="File1"
													runat="server"><asp:label id="lblIdentityId" runat="server"></asp:label><asp:requiredfieldvalidator Enabled="false" id="RequiredFieldValidatorForPhotoSrc" runat="server" ErrorMessage="Scan KTP harus diisi!" ControlToValidate="photoSrc">*</asp:requiredfieldvalidator></td>
										</tr>
                                        <tr>
                                            <td>
                                                <asp:Label Text="*KTP Wajib untuk diupload. Ukuran maks 500Kb." ID="lblMandatoryKTP" runat="server" ForeColor="Red" Font-Size="XX-Small"></asp:Label>
                                            </td>
                                        </tr>
										<tr>
											<td><asp:checkbox id="SalesUnitIndicator" tabIndex="20" runat="server" Visible="False" Text="Sales Unit"></asp:checkbox><br>
												<asp:checkbox id="MechanicIndicator" tabIndex="21" runat="server" Visible="False" Text="Mekanik"></asp:checkbox><br>
												<asp:checkbox id="SparePartIndicator" onclick="HandleSparePartPanel();" tabIndex="22" runat="server"
													Visible="False" Text="Spare Part"></asp:checkbox></td>
										</tr>
										<tr>
											<td><asp:panel id="pnlSparePart" runat="server" Width="178px">
													<asp:CheckBox id="AdmIndicator" tabIndex="23" runat="server" Visible="False" Text="Administrasi (ADM)"></asp:CheckBox>
													<BR>
													<asp:CheckBox id="WHIndicator" tabIndex="24" runat="server" Visible="False" Text="Warehouse (WH)"></asp:CheckBox>
													<BR>
													<asp:CheckBox id="CounterIndicator" tabIndex="25" runat="server" Visible="False" Text="Counter"></asp:CheckBox>
													<BR>
													<asp:CheckBox id="SalesIndicator" tabIndex="26" runat="server" Visible="False" Text="Sales"></asp:CheckBox>
												</asp:panel></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="250">Nama Dealer</TD>
								<TD width="102"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="263"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250"><asp:label id="lblBranch" runat="server" Visible="True">Cabang Dealer</asp:label></TD>
								<TD width="102"><asp:label id="Label11" runat="server" Visible="True">:</asp:label></TD>
								<TD noWrap width="263"><asp:dropdownlist id="ddlDealerBranch" Width="230px" Runat="server"></asp:dropdownlist><asp:label id="lblDealerBranch" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250"><asp:label id="lblKodeSalesman" runat="server"></asp:label></TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:label id="lblSalesmanCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250"><asp:label id="lblAreaSingleTitle" runat="server" Visible="False">Area</asp:label></TD>
								<TD width="102"><asp:label id="lblAreasd" runat="server" Visible="False">:</asp:label></TD>
								<TD noWrap width="263"><asp:dropdownlist id="DdlArea" Runat="server"></asp:dropdownlist><asp:label id="lblArea" runat="server"></asp:label><asp:requiredfieldvalidator id="Areafieldvalidator" runat="server" ErrorMessage="Area Harus dipilih" ControlToValidate="DdlArea">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250">Nama Lengkap Sesuai KTP</TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:textbox onkeypress="TxtKeypress();" id="txtName" onblur="TxtBlurName('txtName');" tabIndex="4"
										runat="server" MaxLength="60" Width="128px"></asp:textbox><asp:label id="lblName" runat="server"></asp:label><asp:requiredfieldvalidator id="valName" runat="server" ErrorMessage="Nama Harus diisi" ControlToValidate="txtName">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="250">Tempat/Tgl Lahir</TD>
								<TD width="102">:</TD>
								<TD style="WIDTH: 263px">
									<table cellSpacing="0" cellPadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkLuarNegri" runat="server" AutoPostBack="true" Text=" Luar Negeri" />
                                            </td>
                                        </tr>
										<tr>
											<td style="WIDTH: 117px"><asp:textbox onkeypress="TxtKeypress();" id="txtPlaceOfBirth" onblur="TxtBlur('txtPlaceOfBirth');"
													tabIndex="5" runat="server" MaxLength="60" Width="88px"></asp:textbox>
                                                <asp:label id="lblKotaLahir" onclick="ShowCity();" Runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                                <asp:HiddenField ID="hdnKotaLahir" runat="server" />
                                               
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Tempat Lahir Harus diisi"
													ControlToValidate="txtPlaceOfBirth">*</asp:requiredfieldvalidator>
                                                <asp:label id="lblPlaceOfBirth" runat="server"></asp:label>

											</td>
											<td style="WIDTH: 4px">/</td>
											<td><cc1:inticalendar id="ICDateOfBirth" tabIndex="6" runat="server"></cc1:inticalendar><asp:label id="lblDateOfBirth" runat="server"></asp:label></td>
										</tr>
                                        <tr>
                                            <td> <asp:label id="Label9" ForeColor="Red" Text ="Tempat lahir penamaan harus sama dengan data master kota" runat="server"></asp:label></td>
                                        </tr>
									</table>
								</TD>
							</TR>
                            <div style="width:100%"></div>
							<TR>
								<TD class="titleField" width="250">Jenis Kelamin</TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:dropdownlist id="ddlGender" tabIndex="7" runat="server"></asp:dropdownlist><asp:label id="lblGender" runat="server"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" ErrorMessage="Jenis Kelamin Harus dipilih"
										ControlToValidate="ddlGender">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250">Status Perkawinan</TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:dropdownlist id="ddlMarriedStatus" tabIndex="7" runat="server"></asp:dropdownlist><asp:label id="lblMarriedStatus" runat="server"></asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Status Perkawinan Harus dipilih"
										ControlToValidate="ddlMarriedStatus">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="250">Alamat</TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:textbox onkeypress="TxtKeypress();" id="txtAlamat" onblur="TxtBlur('txtAlamat');" tabIndex="8"
										runat="server" MaxLength="200" Width="232px" TextMode="MultiLine"></asp:textbox><asp:label id="lblAlamat" runat="server"></asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Alamat Harus diisi" ControlToValidate="txtAlamat">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250">Propinsi</TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:dropdownlist id="ddlPropinsi" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblPropinsi" runat="server" Visible="False"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator8" runat="server" ErrorMessage="Propinsi Harus dipilih"
										ControlToValidate="ddlPropinsi">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250">Kota</TD>
								<TD width="102">:</TD>
								<TD noWrap width="263"><asp:dropdownlist id="ddlKota" runat="server"></asp:dropdownlist><asp:label id="lblKota" runat="server"></asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Kota Harus dipilih" ControlToValidate="ddlKota">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250"></TD>
								<TD width="102"></TD>
								<TD noWrap width="263"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="250"></TD>
								<TD width="102"></TD>
								<TD noWrap width="263"></TD>
							</TR>
							<tr>
								<td colSpan="3">
									<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR>
											<TD><asp:panel id="Panel1" tabIndex="18" runat="server" Width="100%"></asp:panel></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250">Kategori</TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102">:</TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><asp:dropdownlist id="ddlJobPositionDesc" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:label id="lblJobPositionDesc" runat="server"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ErrorMessage="Kategori Harus dipilih"
										ControlToValidate="ddlJobPositionDesc">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250">Level</TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102">:</TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><asp:dropdownlist id="ddlSalesmanLevel" tabIndex="12" runat="server"></asp:dropdownlist><asp:label id="lblSalesmanLevel" runat="server"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" ErrorMessage="Level Harus dipilih" ControlToValidate="ddlSalesmanLevel">*</asp:requiredfieldvalidator></TD>
								<TD class="titleField" colSpan="4"><asp:panel id="pnlSuperior" Runat="server">
										<asp:label id="lblSuperior" runat="server">Atasan :</asp:label>
										<TABLE>
											<TR>
												<TD width="70">
													<asp:textbox onkeypress="TxtKeypress();" id="txtSuperior" onblur="TxtBlur('txtSuperior');" tabIndex="4"
														runat="server" Width="60px" MaxLength="10"></asp:textbox>
													<asp:requiredfieldvalidator id="ValAtasan" runat="server" ControlToValidate="txtSuperior" ErrorMessage="Atasan Harus diisi">*</asp:requiredfieldvalidator></TD>
												<TD>
													<asp:textbox onkeypress="TxtKeypress();" id="txtSuperiorName" onblur="TxtBlur('txtSuperiorName');" tabIndex="4"
														runat="server" BorderColor="Silver" BorderStyle="None"></asp:textbox></TD>
												<TD>
													<asp:label id="Label4" onclick="ShowPopUpSuperior();" Runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
													</asp:label></TD>
											</TR>
										</TABLE>
									</asp:panel></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250">Tgl Masuk</TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102">:</TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><cc1:inticalendar id="ICStartWork" tabIndex="15" runat="server"></cc1:inticalendar><asp:label id="lblStartWork" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"><INPUT id="txtMode" style="WIDTH: 88px; HEIGHT: 20px" type="hidden" size="9" name="txtMode"
										runat="server"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250">Tgl Keluar</TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102">:</TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><cc1:inticalendar id="ICEndWork" runat="server" Enabled="False"></cc1:inticalendar><asp:label id="lblEndWork" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"><INPUT id="txtJobPosition" style="WIDTH: 88px; HEIGHT: 20px" type="hidden" size="9" name="txtJobPosition"
										runat="server"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250">Alasan</TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102">:</TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtResignReason" tabIndex="17" runat="server"
										MaxLength="50" Width="200px" Enabled="False"></asp:textbox><asp:label id="lblResignReason" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250">Status</TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102">:</TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist><asp:label id="lblStatus" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250"></TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250"><asp:label id="TitleHistory" Runat="server"> History Pekerjaan Salesman</asp:label></TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
								<TD noWrap colSpan="4">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 100px"><asp:datagrid id="dtgHistory" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Tempat Kerja Sebelumnya">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") + " - " + DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtDealerHistory" tabIndex="10" MaxLength="6" runat="server" width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
														<asp:Label id="Label7" tabIndex="20" runat="server" height="10px">
															<img style="cursor:hand" alt="Klik Disini untuk memilih Dealer" src="../images/popup.gif"
																border="0" onclick="ShowPopUpDealerHistory();"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DateIn" HeaderText="Tanggal Masuk">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label8" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DateIn"),"dd/MM/yyyy") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<cc1:inticalendar id="dtDateIn" tabIndex="6" runat="server"></cc1:inticalendar>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DateOut" HeaderText="Tanggal Keluar">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label10" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DateOut"),"dd/MM/yyyy") %>' CssClass="textRight">
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<cc1:inticalendar id="dtDateOut" tabIndex="6" runat="server"></cc1:inticalendar>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="LbtnDeleteHistory" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton9" CausesValidation="False" CommandName="add" text="Tambah" Runat="server">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250"><asp:label id="lblTarget" Runat="server">Target</asp:label></TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
								<TD noWrap colSpan="4">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 100px"><asp:datagrid id="dtgTarget" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="YearTarget" HeaderText="Tahun">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblYearTarget" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.YearTarget") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList id="ddlYearTargetAdd" runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList id="ddlYearTargetEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.YearTarget") %>'>
														</asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="MonthTarget" HeaderText="Bulan">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblMonthTarget" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList id="ddlMonthTargetAdd" runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList id="ddlMonthTargetEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MonthTarget") %>' >
														</asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ValueTarget" HeaderText="Target Penjualan (Rp)">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lbValueTarget" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ValueTarget"),"#,##0") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtValueTargetAdd" runat="server" onkeyup="pic(this,this.value,'999999999','N')"
															onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtValueTargetEdit" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'999999999','N')" onblur="NumOnlyBlurWithOnGridTxt(this,'');" Text='<%# DataBinder.Eval(Container, "DataItem.ValueTarget") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEditTarget" CausesValidation="False" CommandName="editTarget" text="Ubah"
															Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDeleteTarget" CausesValidation="False" CommandName="deleteTarget" text="Hapus"
															Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton5" runat="server" CausesValidation="False" CommandName="addTarget">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSaveTarget" tabIndex="40" CommandName="saveTarget" text="Simpan" Runat="server"
															CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="Linkbutton7" tabIndex="50" CommandName="cancelTarget" text="Batal" Runat="server"
															CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250"></TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="263"><asp:validationsummary id="ValidationSummary1" runat="server" Width="208px"></asp:validationsummary></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 432px" noWrap colSpan="3"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 50%; HEIGHT: 20px" width="50%"><asp:button id="btnSimpan" tabIndex="27" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnRequestID" tabIndex="28" runat="server" Text="Request ID"></asp:button><asp:button id="btnBatal" tabIndex="29" runat="server" Width="60px" CausesValidation="False"
										Text="Batal"></asp:button></TD>
								<TD class="titleField" style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250"><asp:button id="btnSearch" tabIndex="30" runat="server" Width="60px" CausesValidation="False"
										Text="Kembali"></asp:button></TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="238"></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 250px; HEIGHT: 20px" width="250"></TD>
								<TD style="WIDTH: 102px; HEIGHT: 20px" width="102"></TD>
								<TD style="WIDTH: 263px; HEIGHT: 20px" noWrap width="238"></TD>
								<TD class="titleField" style="WIDTH: 136px; HEIGHT: 20px" width="136"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
						</TABLE>
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField"><asp:label id="lblAreaPemasaran" Runat="server">Area Pemasaran</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 100px"><asp:datagrid id="dtgArea" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanArea.AreaCode" HeaderText="Kode Area">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanArea.AreaCode") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtAreaCode" tabIndex="10" runat="server" width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
														<asp:Label id="lblPopUpSalesManArea" tabIndex="20" runat="server" height="10px">
															<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
																border="0"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanArea.AreaDesc" HeaderText="Nama Area">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanArea.AreaDesc") %>' CssClass="textRight">
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label id="lblAreaDesc" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" CausesValidation="False" CommandName="add" text="Tambah" Runat="server">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTraining" Runat="server">Training</asp:label></TD>
							</TR>
							<tr>
								<td>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 100px"><asp:datagrid id="dtgTraining" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrainingModule" HeaderText="Modul">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblModulTraining" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingModule") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtModulTrainingAdd" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtModulTrainingEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingModule") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrainingPlaceAndDate" HeaderText="Tempat/Tanggal">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTempatTanggal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingPlaceAndDate") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtTempatTanggalAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtTempatTanggalEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingPlaceAndDate") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrainingProvider" HeaderText="Penyelenggara">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPenyelenggara" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingProvider") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtPenyelenggaraAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtPenyelenggaraEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingProvider") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEditTrain" CausesValidation="False" CommandName="editTrain" text="Ubah"
															Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDeleteTrain" CausesValidation="False" CommandName="deleteTrain" text="Hapus"
															Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton1" runat="server" CausesValidation="False" CommandName="addTrain">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSaveTrain" tabIndex="40" CommandName="saveTrain" text="Simpan" Runat="server"
															CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="Linkbutton6" tabIndex="50" CommandName="cancelTrain" text="Batal" Runat="server"
															CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
							<TR>
								<TD class="titleField"><asp:label id="lblPengalaman" Runat="server">Pengalaman</asp:label></TD>
							</TR>
							<tr>
								<td>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 100px"><asp:datagrid id="dtgExperience" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="YearExperience" HeaderText="Tahun">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblYearExperience" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.YearExperience") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtYearExperienceAdd" runat="server" MaxLength="4" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtYearExperienceEdit" runat="server" MaxLength=4 onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');" Text='<%# DataBinder.Eval(Container, "DataItem.YearExperience") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Company" HeaderText="Perusahaan">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Company") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtCompanyAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtCompanyEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.Company") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="JobPosition" HeaderText="Posisi">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPosisi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtJobPositionAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtJobPositionEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEditExp" CausesValidation="False" CommandName="editExp" text="Ubah" Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDeleteExp" CausesValidation="False" CommandName="deleteExp" text="Hapus"
															Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton2" runat="server" CausesValidation="False" CommandName="addExp">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="saveExp" text="Simpan" Runat="server" CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancelExp" text="Batal" Runat="server"
															CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblPrestasi" Runat="server">Prestasi</asp:label></td>
							</tr>
							<tr>
								<td>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 100px"><asp:datagrid id="dtgPrestasi" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AccomplishYear" HeaderText="Tahun">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblAccomplishYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AccomplishYear") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtAccomplishYearAdd" runat="server" MaxLength="4" onkeypress="return NumericOnlyWith(event,'');"
															onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtAccomplishYearEdit" runat="server" MaxLength=4 onkeypress="return NumericOnlyWith(event,'');" onblur="NumOnlyBlurWithOnGridTxt(this,'');" Text='<%# DataBinder.Eval(Container, "DataItem.AccomplishYear") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Accomplishment" HeaderText="Prestasi">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblAccomplishment" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Accomplishment") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtAccomplishmentAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtAccomplishmentEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.Accomplishment") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEditAccomp" CausesValidation="False" CommandName="editAccomp" text="Ubah"
															Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDeleteAccomp" CausesValidation="False" CommandName="deleteAccomp" text="Hapus"
															Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton3" runat="server" CausesValidation="False" CommandName="addAccomp">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSaveAccomp" tabIndex="40" CommandName="saveAccomp" text="Simpan" Runat="server"
															CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="Linkbutton4" tabIndex="50" CommandName="cancelAccomp" text="Batal" Runat="server"
															CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
						<INPUT id="hdnVal" type="hidden" name="hdnVal" runat="server">
                         <INPUT id="IsInsertSuccess" type="hidden" name="IsInsertSuccess" runat="server">
					</TD>
				</TR>
			</TABLE>
		</form>
        <%--<div id="panelOverlay" style="
			position: fixed;
			top: 0;
			left: 0;
			height: 100%;
			width: 100%;
			background-color: rgba(0, 0, 0, 0.4);
			text-align: center;
		">
			<img alt="Zoom" id="zoomImage" 
				 style="
						width: 300px;
						height: 300px;
						opacity: 1;
						vertical-align: middle;
						position: absolute;
					    top: 50%;
					    left: 50%;
					    transform: translate(-50%, -50%);
					   "
				 src="tulips.jpg"
			/>
		</div>--%>
		<script type="text/javascript" language="javascript">
		    //console.log('This is form Salesman Header');

		    var IsInsertSuccess = document.getElementById("IsInsertSuccess");
		    if (IsInsertSuccess.value == '1') {
		        alert('Data berhasil disimpan');
		    }

		    //addOverlayPanel();
		    addEventListeners();

		    function addEventListeners() {
		        document.getElementById('photoView').addEventListener('click', imageOnClick);
		    }

		    function addOverlayPanel() {
		        var overlayPanelHtml = '<iframe id="iFramePhotoView" src="https://www.w3schools.com"></iframe>';
		        document.body.innerHTML = document.body.innerHTML + overlayPanelHtml;
		    }

		    function imageOnClick(evt) {
		        var photoView = document.getElementById('photoView');
		        var imageSource = photoView.getAttribute('src');
		        window.open(
                    imageSource,
                    '1511239697381',
                    'width=700,height=500,toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'
                );
		        return false;
		    }
		</script>
	</BODY>
</HTML>

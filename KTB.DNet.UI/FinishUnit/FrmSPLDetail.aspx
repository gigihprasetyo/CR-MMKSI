<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPLDetail.aspx.vb" Inherits="FrmSPLDetail" smartNavigation="False" MaintainScrollPositionOnPostback="true" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">

		    function ValidatePage() {
		        if (typeof (Page_ClientValidate) == 'function') {
		            Page_ClientValidate();
		        }

		        if (Page_IsValid) {
		            return true;
		        }
		        else {
		            alert('Field mandatory mohon diisi dahulu !');
		            return false;
		        }
		    }

		    function KonfirmasiSimpan() {
		        var btnSave = document.getElementById("btnSave");
		        if (!confirm('Anda Yakin Mau Simpan ?')) {
		            btnSave.disabled = false;
		            return false;
		        }
		        else {
		            if (!ValidatePage()) { return false;}
		            btnSave.disabled = true;
		            document.body.style.cursor = 'wait';
		            document.getElementById('btnSave2').click();
		            return true;
		        }
		    }

		    function ValidateRadioButton(obj)
		    {
			    var rbtnDate = document.getElementById("rbtnDate");
			    var rbtnDay = document.getElementById("rbtnDay");
			    var rbtnNone = document.getElementById("rbtnNone");
			    rbtnDate.checked = false;
			    rbtnDay.checked = false;
			    rbtnNone.checked = false;
			    obj.checked = true;			
		    }
		    function ShowPPKodeModelSelection(ddlModelValue)
		    {
		        if (ddlModelValue == '') {
		            alert('Harap pilih model dahulu');
		            return;
		        }
		        showPopUp('../General/FrmModelSelection.aspx?cat=test&modelID=' + ddlModelValue, '', 400, 400, KodeTipeSelection)
		    }
	
		    function GetCurrentInputIndex()
		    {
			    var dgSPDetail = document.getElementById("dgSPDetail");
			    var currentRow;
			    var index = 0;
			    var inputs;
			    var indexInput;
			
			    for (index = 0; index < dgSPDetail.rows.length; index++)
			    {
				    inputs = dgSPDetail.rows[index].getElementsByTagName("INPUT");
				
				    if (inputs != null && inputs.length > 0)
				    {
					    for (indexInput = 0; indexInput < inputs.length; indexInput++)
					    {	
						    if (inputs[indexInput].type != "hidden")
							    return index;
					    }
				    }
			    }				
			    return -1;
		    }
				
		    function KodeTipeSelection(selectedType)
		    {
			    var indek = GetCurrentInputIndex();
			    var dgSPDetail = document.getElementById("dgSPDetail");
			    var KodeTipe = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[0];
			    KodeTipe.value = selectedType;
			    var btnRetrieveDetailDiscount = document.getElementById("btnRetrieveDetailDiscount");
			    btnRetrieveDetailDiscount.click();
			    if (navigator.appName == "Microsoft Internet Explorer") {
			        KodeTipe.focus();
			        KodeTipe.blur();
			    }
			    else {
			        KodeTipe.onchange();
			    }

		    }
		    function ShowPPDealerSelection()
		    {
			    showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		    }
		    function DealerSelection(selectedDealer)
		    {
			    var txtDealerSelection = document.getElementById("txtDealerName");
			    if (txtDealerSelection.value == '')
			    {
				    txtDealerSelection.value = selectedDealer + ';';	
			    }
			    else
			    {
				    txtDealerSelection.value = txtDealerSelection.value + selectedDealer + ';';	
			    }
					
		    }
			
		    function ShowPPCategoryDiscountSelection() {
		        showPopUp('../General/../PopUp/PopUpDiscountMasterSelection.aspx', '', 500, 760, CategoryDiscountSelection);
		    }
		    function CategoryDiscountSelection(selectedCategory) {
		        var hdnDiscountMasterID = document.getElementById("hdnDiscountMasterID");
		        var txtDiscountCategorySelection = document.getElementById("txtDiscountCategory");
		        var result = selectedCategory.split(';');
		        hdnDiscountMasterID.value = result[0];
		        txtDiscountCategorySelection.value = result[01];
            }

		    function ShowPPFasilitasTOPSelection()
		    {
			    showPopUp('../FinishUnit/FrmTOPFacility.aspx?x=100c','',400,400,TOP)
		    }

		    function GetCurrentInputIndex()
		    {
			    var dgSPDetail = document.getElementById("dgSPDetail");
			    var currentRow;
			    var index = 0;
			    var inputs;
			    var indexInput;
			
			
			    for (index = 0; index < dgSPDetail.rows.length; index++)
			    {
				    inputs = dgSPDetail.rows[index].getElementsByTagName("INPUT");
				
				    if (inputs != null && inputs.length > 0)
				    {
					    for (indexInput = 0; indexInput < inputs.length; indexInput++)
					    {	
						    if (inputs[indexInput].type != "hidden")
							    return index;
					    }
				    }
			    }				
			    return -1;
		    }
			
		    function GetCurrentSpanIndex()
		    {
			    var dgSPDetail = document.getElementById("dgSPDetail");
			    var currentRow;
			    var index = 0;
			    var inputs;
			    var indexInput;
			
			
			    for (index = 0; index < dgSPDetail.rows.length; index++)
			    {
				    inputs = dgSPDetail.rows[index].getElementsByTagName("SPAN");
				
				    if (inputs != null && inputs.length > 0)
				    {
					    for (indexInput = 0; indexInput < inputs.length; indexInput++)
					    {	
						    if (inputs[indexInput].type != "hidden")
							    return index;
					    }
				    }
			    }				
			    return -1;
		    }		
		    function TOP(selectedTOP)
		    {
					
			    var indek = GetCurrentInputIndex();
			    var dgSPDetail = document.getElementById("dgSPDetail");
			    var KodeTipe = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[5];
			    //var KodeTipe = dgSPDetail.rows[indek].getElementsById("txtFooterTop");
			    KodeTipe.value = selectedTOP;
		    }
		
		    function ShowPKHeader()
		    {
			    var indek = GetCurrentSpanIndex();
			    var _splnumber =  document.getElementById("txtSPLNumber");
			    var dgSPDetail = document.getElementById("dgSPDetail"); 
			    var _period = dgSPDetail.rows[indek].getElementsByTagName("SPAN")[8];
			    var _kodetipe = dgSPDetail.rows[indek].getElementsByTagName("SPAN")[1];	
			    showPopUp('../FinishUnit/FrmPKHeaderSPL.aspx?_splnumber='+_splnumber.value+'&_kodetipe='+_kodetipe.innerHTML+'&_period='+_period.innerHTML,'',400,500,'')
			
		    }

		    function showPopupSearchFooterPriceReff(obj) {
		        var hdnIndexSPLDetailGrid = document.getElementById("hdnIndexSPLDetailGrid");
		        var idx = GetCurrentInputIndex();
		        //var idx = getRowIndex(obj);
		        hdnIndexSPLDetailGrid.value = idx
		        var dgSPDetail = document.getElementById("dgSPDetail");
		        var priceReff = dgSPDetail.rows[idx].getElementsByTagName("INPUT")[5];
		        var today = new Date();
		        var yyyy = today.getFullYear();
		        var mm = today.getMonth() + 1;
		        mm = '0' + mm;
		        mm = mm.substr(mm.length - 2);
		        var mmyyyy = String(mm) + String(yyyy);

		        if (priceReff.value == '' || priceReff.value == '0') { priceReff.value = mmyyyy; }
		        showPopUp('../PopUp/PopUpReferensiHarga.aspx?priceReff=' + priceReff.value, '', 150, 350, selectedPriceReff);
		    }

		    function selectedPriceReff(selectedOpt) {
		        var tempParams = selectedOpt.split(';');

		        var hdnIndexSPLDetailGrid = document.getElementById("hdnIndexSPLDetailGrid");
		        var idx = hdnIndexSPLDetailGrid.value;
		        var dgSPDetail = document.getElementById("dgSPDetail");
		        var priceReff = dgSPDetail.rows[idx].getElementsByTagName("INPUT")[5];
		        priceReff.value = trim(tempParams[0]);
		        if (navigator.appName == "Microsoft Internet Explorer") {
		            priceReff.focus();
		            priceReff.blur();
		        }
		        else {
		            priceReff.onchange();
		        }
		    }

		    function showPopupSearchFooterApplicationNo(obj) {
		        var hdnIndexSPLDetailGrid = document.getElementById("hdnIndexSPLDetailGrid");
		        var idx = GetCurrentInputIndex();
		        //var idx = getRowIndex(obj);
		        hdnIndexSPLDetailGrid.value = idx

		        var dgSPDetail = document.getElementById("dgSPDetail");
		        var txtTipeKendaraan = dgSPDetail.rows[idx].getElementsByTagName("INPUT")[0];
		        var ddlDiscountType = dgSPDetail.rows[idx].getElementsByTagName("SELECT")[1];
		        var tipeKendaraanValue = txtTipeKendaraan.value;
		        var selectedDiscountTypeValue = ddlDiscountType.options[ddlDiscountType.selectedIndex].value;

		        if (txtTipeKendaraan.value == "") {
		            alert("Tipe harus di isi !");
		            return;
		        }
		        if (ddlDiscountType.selectedIndex == 0) {
		            alert("Discount Type harus di isi !");
		            return;
		        }

		        showPopUp('../PopUp/PopUpAplikasiSPLSelection.aspx?TipeKendaraan=' + tipeKendaraanValue + '&DiscountType=' + selectedDiscountTypeValue, '', 520, 700, selectedSPL);
		    }

		    function selectedSPL(selectedSPL) {
		        var tempParams = selectedSPL.split(';');

		        var hdnIndexSPLDetailGrid = document.getElementById("hdnIndexSPLDetailGrid");
		        var idx = hdnIndexSPLDetailGrid.value;

		        var btnRetrieveDetailDiscount = document.getElementById("btnRetrieveDetailDiscount");
		        var dgSPDetail = document.getElementById("dgSPDetail");
		        var noreg = dgSPDetail.rows[idx].getElementsByTagName("INPUT")[3];
		        var id = dgSPDetail.rows[idx].getElementsByTagName("INPUT")[2];
		        id.value = tempParams[0];
		        noreg.value = trim(tempParams[1]);
		        btnRetrieveDetailDiscount.click();
		        if (navigator.appName == "Microsoft Internet Explorer") {
		            noreg.focus();
		            noreg.blur();
		        }
		        else {
		            noreg.onchange();
		        }
		    }

		    function UploadFile1(fileUpload) {
		        var btnUpload = document.getElementById('btnUploadLampiranPerhitunganDiskon');
		        try {
		            if (fileUpload.value != '') {
		                if (btnUpload) btnUpload.click();
		            }
		        } catch (e) {
		            alert(e.message);
		        }
		    }

		    function UploadFile2(fileUpload) {
		        var btnUpload = document.getElementById('btnUploadAttachment');
		        try {
		            if (fileUpload.value != '') {
		                if (btnUpload) btnUpload.click();
		            }
		        } catch (e) {
		            alert(e.message);
		        }
		    }
		</script>
	    <style type="text/css">
            .auto-style1 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                height: 11px;
                width: 1277px;
            }
            .auto-style2 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                width: 1277px;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <asp:HiddenField ID="hdnSPLID" runat="server" />
            <input id="hdnIndexSPLDetailGrid" type="hidden" value="" runat="server">
            <asp:Button ID="btnRetrieveDetailDiscount" runat="server" Text=".." Style="width: 70px;display:none"></asp:Button>

			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">UMUM&nbsp;- Detail Aplikasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
            </TABLE>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="90%" border="0">
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 15px" width="17%">
                                    <asp:label id="lblSPLNumber" runat="server">Nomor Aplikasi</asp:label>
								</TD>
								<TD style="HEIGHT: 15px" width="1%">
                                    <p><asp:label id="lblColon3" runat="server">:</asp:label></p>
                                    <p></p>
								</TD>
								<TD style="HEIGHT: 15px" width="25%" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;,{}=+^`~');" id="txtSPLNumber"
										            onblur="omitSomeCharacter('txtSPLNumber','<>?*%$;');" runat="server" Width="200px" MaxLength="50"></asp:textbox>
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtSPLNumber" ErrorMessage="*"></asp:requiredfieldvalidator>
                                            </td>
                                        </tr>
                                    </table>
								</TD>
								<TD class="titleField" style="HEIGHT: 15px" width="17%">
                                    <asp:label id="lblDesc" runat="server">Deskripsi</asp:label>
								</TD>
								<TD style="HEIGHT: 15px" width="1%">
                                    :
								</TD>
								<TD style="HEIGHT: 15px" width="29%">
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;');" id="txtDescription" onblur="omitSomeCharacter('txtDescription','<>?*%$;');"
										runat="server" Width="200" MaxLength="250" TextMode="MultiLine" Height="40px"></asp:textbox>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtDescription" ErrorMessage="*"></asp:requiredfieldvalidator>
								</TD>
							</TR>
                            <tr valign="top">
								<TD class="titleField" style="HEIGHT: 23px" width="17%"><asp:label id="lblName" runat="server">Nama Dealer</asp:label></TD>
								<TD style="HEIGHT: 23px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 23px" width="30%"><asp:textbox id="txtDealerName" runat="server" Width="200px" MaxLength="5000"  
										TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtDealerName" ErrorMessage="*"></asp:requiredfieldvalidator>
                                    <asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
                                <td valign="top">
                                    <asp:label id="lblComment" runat="server">Comment</asp:label>
                                </td>
                                <td valign="top"><asp:Label ID=lblTitikComment runat="server" Text=":"></asp:Label></td>
                                <td>
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;');" id="txtComment" Enabled="false" onblur="omitSomeCharacter('txtComment','<>?*%$;');"
										runat="server" Width="200" MaxLength="255" TextMode="MultiLine" Height="40px"></asp:textbox>
                                </td>
                            </tr>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="17%">
                                    <asp:label id="lblCustName" runat="server">Nama Customer</asp:label>
								</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%">
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;,/#@!~^&amp;()_-+=|\{}[]:');"
										id="txtCustName" onblur="omitSomeCharacter('txtCustName','<>?*%$;');" runat="server" Width="200px" MaxLength="50"></asp:textbox>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtCustName" ErrorMessage="*"></asp:requiredfieldvalidator>
								</TD>
								<TD class="titleField" style="HEIGHT: 20px" width="17%">Cicilan | Jumlah Hari TOP</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%" >
                                    <asp:textbox 
										id="txtNumOfInstallment" Text="1" runat="server" Width="69px" MaxLength="5"  onkeypress="return numericOnlyUniv(event)"
                                        style="text-align:right;"></asp:textbox>
                                 <%--   onkeypress="return alphaNumericExcept(event,'<>?*%$;,/#@!~^&amp;()_-+=|\{}[]:');"--%>
                                    &nbsp;|&nbsp;
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;,/#@!~^&amp;()_-+=|\{}[]:');"
										id="txtMaxTOPDay" Text="0" runat="server" Width="69px" MaxLength="5"
                                        style="text-align:right;"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="17%"><asp:label id="lblValidFrom" runat="server">Periode Tebus Dari</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD valign="top" style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtValidFrom" runat="server" Width="72px"
										MaxLength="6"></asp:textbox><asp:label id="Label2" runat="server">&nbsp;<b>MMyyyy</b></asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtValidFrom" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="17%"><asp:label id="Label5" runat="server">Periode Tebus Sampai</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtValidTo" runat="server" Width="72px"
										MaxLength="6"></asp:textbox><asp:label id="Label7" runat="server">MMyyyy</asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtValidTo" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
                            <tr>
                                <td class="titleField" style="width: 24%">Lampiran Perhitungan Diskon</td>
                                <td style="width: 2px">:</td>
                                <td valign="top">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtLampiranPerhitunganDiskon" ReadOnly="true" BackColor="#ebebe0"
                                        onblur="omitSomeCharacter('txtLampiranPerhitunganDiskon','<>?*%$')" runat="server" BorderStyle="Solid" BorderColor="#999999" Width="162px" style="height:18px"></asp:TextBox>

                                    <input id="FULampiranPerhitunganDiskon" onkeydown="return false;" style="width: 65px" type="file" size="25"
                                        name="FULampiranPerhitunganDiskon" runat="server">
                                    <asp:Label ID="lblLampiranPerhitunganDiskon" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="btnUploadLampiranPerhitunganDiskon" runat="server" Text="Upload" Style="display: none"></asp:Button>
                                    <asp:LinkButton ID="lbtnDeleteLampiranPerhitunganDiskon" OnClientClick="return confirm('Anda yakin mau hapus?');" Text="Hapus" runat="server"> <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </td>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="17%" colSpan="3"><asp:label id="Label6" runat="server" Width="200px">*.PDF</asp:label></TD>
                            </tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="17%"><asp:label id="Label8" runat="server">Attachment</asp:label></TD>
								<TD style="HEIGHT: 20px" vAlign="top" width="1%"><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%">
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtAttachment" ReadOnly="true" BackColor="#ebebe0"
                                        onblur="omitSomeCharacter('txtAttachment','<>?*%$')" runat="server" BorderStyle="Solid" BorderColor="#999999" Width="162px" style="height:18px"></asp:TextBox>

                                    <input id="FUAttachment" name="FUAttachment" onkeydown="return false;" style="width: 65px" type="file" size="25" runat="server">
                                    <asp:Label ID="lblAttachment" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="btnUploadAttachment" runat="server" Text="Upload" Style="display: none"></asp:Button>
                                    <asp:LinkButton ID="lbtnDeleteAttachment" OnClientClick="return confirm('Anda yakin mau hapus?');" Text="Hapus" runat="server"> <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>

                                    <%--<INPUT id="inFileLocation" onkeydown="return false;" style="WIDTH: 240px" type="file" name="File1" runat="server">
									<asp:label id="lblAttachment" runat="server"></asp:label>--%>
								</TD>
								<TD class="titleField" style="HEIGHT: 20px" vAlign="top" width="17%" colSpan="3"><asp:label id="Label12" runat="server" Width="200px">*.DOC, *.PDF, *.XLS, *.ZIP, *.RAR</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="17%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:dropdownlist id="ddlStatus" runat="server" Width="80px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="17%">
                                    <p>
                                        <asp:CheckBox id="chkFinalApproval" runat="server" Text="Final Approval oleh CFO"></asp:CheckBox>
                                    </p>
                                    <p>
                                        <asp:CheckBox id="chkIsAutoApprovedDealer" runat="server" Text="Setuju PK Otomatis"></asp:CheckBox>
                                    </p>
								</TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>

							</TR>
							<TR id="tr1" runat="server">
								<TD class="titleField" style="HEIGHT: 20px" width="17%">Dibuat Oleh</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDibuatOleh" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="17%">Dibuat Pada</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDibuatPada" runat="server"></asp:label></TD>
							</TR>
							<TR id="tr3" runat="server">
								<TD class="titleField" style="HEIGHT: 20px" width="17%">Diubah Oleh</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDiubahOleh" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="17%">Diubah Pada</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:label id="lblDiubahPada" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
            <table>
			    <TR>
				    <TD class="auto-style1" colSpan="6">
					    <TABLE width="40%" align="right">
						    <TR>
							    <td align="right" noWrap><b>Periode Tebus : </b></td>
							    <TD style="WIDTH: 21px"><asp:linkbutton id="lbtnPrevMonth" onclick="lbtnPrevMonth_Click" Runat="server">
									    <img src="../images/page_prev.gif" alt="Previous Month" align="right" border="0">
								    </asp:linkbutton></TD>
							    <td align="center" width="30%"><asp:label id="lblCurrentPeriode" runat="server"></asp:label></td>
							    <TD style="WIDTH: 21px"><asp:linkbutton id="lbtnNextMonth" onclick="lbtnNextMonth_Click" Runat="server">
									    <img src="../images/page_next.gif" alt="Next Month" align="right" border="0">
								    </asp:linkbutton></TD>
						    </TR>
					    </TABLE>
				    </TD>
			    </TR>
				<TR>
					<TD class="auto-style2" colSpan="6">
						<div id="div1" style="OVERFLOW: scroll; width: 129%; HEIGHT: 100%">
                            <asp:datagrid id="dgSPDetail" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True" OnItemDataBound="dgSPDetail_ItemDataBound" OnItemCommand="dgSPDetail_ItemCommand">
								<FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
								<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle VerticalAlign="Top"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Periode">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblPeriodMonth" runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Periode Year">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" ></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblPeriodYear" runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Model">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblModelKendaraan" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFooterModelKendaraan" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlFooterModelKendaraan_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlEditModelKendaraan" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlFooterModelKendaraan_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="Tipe">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNamaType" Runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterTipeKendaraan" runat="server" Width="50" MaxLength="4" OnTextChanged="txtFooterTipeKendaraan_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
											<asp:Label id="lblFooterTipeKendaraan" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEditTipeKendaraan" runat="server" Width="50" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "SPLDetail.VechileType.VechileTypeCode")%>' 
                                                OnTextChanged="txtFooterTipeKendaraan_TextChanged" AutoPostBack="true" MaxLength="4">
											</asp:TextBox>
											<asp:Label id="lblEditTipeKendaraan" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Unit">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" Width="4%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblViewUnit runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterUnit" runat="server" Width="20" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
												MaxLength="11" onkeyup="pic(this,this.value,'9999999999','N')"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id=txtEditUnit runat="server" width="20" tooltip="Unit Permintaan harus lebih besar dari 0" MaxLength="11" 
                                                Text='<%# DataBinder.Eval(Container.DataItem, "SPLDetail.Quantity")%>' CssClass="textRight" onkeyup="pic(this,this.value,'9999999999','N')" 
                                                onkeypress="return numericOnlyUniv(event)">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Unit">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSisaUnit" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Discount Type">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewDiscountType" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFooterDiscountType" runat="server">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlEditDiscountType" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Application No.">
                                        <HeaderStyle Width="16%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="16%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewApplicationNo" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:HiddenField ID="hdnFooterSPLID" runat="server" OnValueChanged="hdnFooterSPLID_ValueChanged" />
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtFooterApplicationNo" OnTextChanged="txtFooterApplicationNo_TextChanged"
                                                onblur="omitSomeCharacter(this.id,'<>?*%$')" runat="server" Width="100px" AutoPostBack="true">
                                            </asp:TextBox>
                                            <asp:Label ID="lblSearchFooterApplicationNo" runat="server" TabIndex="0">
                                            <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hdnEditSPLID" runat="server" OnValueChanged="hdnFooterSPLID_ValueChanged" />
                                            <asp:TextBox ID="txtEditApplicationNo" name="txtEditApplicationNo" Width="100px" OnTextChanged="txtFooterApplicationNo_TextChanged" 
                                                onkeypress="return alphaNumericExcept(event,'<>?*%$;')" AutoPostBack="true"
                                                onblur="omitSomeCharacter(this.id,'<>?*%$;')" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblSearchEditApplicationNo" runat="server" TabIndex="0">
                                            <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Detail Discount">
                                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewDetailDiscount" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFooterDetailDiscount" Style="text-align: right" runat="server" 
                                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="55px" />
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditDetailDiscount" Style="text-align: right" runat="server" 
                                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="55px" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Price Reff">
                                        <HeaderStyle Width="11%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewPriceRefDate" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFooterPriceRefDate" name="txtFooterPriceRefDate" onkeypress="return NumericOnlyWith(event,'');" Width="50px" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblSearchFooterPriceRefDate" runat="server" TabIndex="0">
                                            <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditPriceRefDate" name="txtEditPriceRefDate" onkeypress="return NumericOnlyWith(event,'');" Width="50px" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblSearchEditPriceRefDate" runat="server" TabIndex="0">
                                            <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="TOP">
                                        <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewTOP" runat="server" Text="0">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFooterTOP" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                                onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" Text="0" />
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditTOP" Style="text-align: right" runat="server" Text="0"
                                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="Bebas Bunga">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblViewInterest" runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:DropDownList id="ddlFooterInterest" runat="server"></asp:DropDownList>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:DropDownList id="ddlEditInterest" runat="server"></asp:DropDownList>
										</EditItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Delivery Time [MMyyyy]">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewDeliveryTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFooterDeliveryTime" Style="text-align: left" runat="server" MaxLength="6"
                                                onkeypress="return NumericOnlyWith(event,'');" Width="50px" />
                                            <asp:Label ID="lblFooterFormatDeliveryTime" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditDeliveryTime" Style="text-align: left" runat="server" MaxLength="6"
                                                onkeypress="return NumericOnlyWith(event,'');" Width="50px" />
                                            <asp:Label ID="lblEditFormatDeliveryTime" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

									<asp:TemplateColumn Visible="False" HeaderText="Periode Kirim">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" Width="5%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDeliveryTime" runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterDeliveryDate" runat="server" Width="50" CssClass="textRight" MaxLength="6"
												onkeypress="return numericOnlyUniv(event)" tooltip="MMyyyy"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEditDeliveryDate" runat="server" Width="50" tooltip="MMyyyy" MaxLength="6" 
                                                Text='<%# DataBinder.Eval(Container.DataItem, "SPLDetail.DeliveryDate")%>' CssClass="textRight" onkeypress="return numericOnlyUniv(event)">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="16%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" width="16%"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus">
											</asp:LinkButton>
											<asp:Label id="lblViewPK" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="PK Header">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CausesValidation="true" CommandName="Add">
												<img src="../images/add.gif" border="0" alt="Tambah">
											</asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
												text="Simpan">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
												text="Batal">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD class="auto-style1" colSpan="6">
                        <asp:Button ID="btnSave2" Style="display: none" TabIndex="0" runat="server" Text="Simpan"></asp:Button>&nbsp;&nbsp;
                        <asp:Button ID="btnSave" TabIndex="0" runat="server" Text="Simpan"></asp:Button>&nbsp;&nbsp;
                        <asp:button id="btnBack" runat="server" Width="60px" Text="Kembali" CausesValidation="False"></asp:button>
                        &nbsp;&nbsp;<asp:LinkButton ID="lnkAppointmentLetter"  visible="false" runat="server" Text="..."></asp:LinkButton>
					</TD>
				</TR>

            </table>
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
		</form>
	</body>
</HTML>

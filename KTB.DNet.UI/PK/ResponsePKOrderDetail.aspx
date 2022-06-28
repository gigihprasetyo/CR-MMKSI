<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResponsePKOrderDetail.aspx.vb" Inherits="ResponsePKOrderDetail" smartNavigation="False" MaintainScrollPositionOnPostback="true"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResponsePKOrderDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		    //function Back()
		    //{
		    //var hidden = document.getElementById("Hidden1")
		    //var i = hidden.value * -1
		    //window.history.go(i);
		    //}
		
		    function Penjelasan(Text)
			{
				var txtDescription = document.getElementById("txtDescription");
				if(navigator.appName == "Microsoft Internet Explorer")
				{
		    		txtDescription.innerText=Text;		
				}
				else
				{
			    	txtDescription.value=Text;	
				}
			}		
			
			function ShowPPPenjelasan()
			{
			    var txtPenjelasan = document.getElementById("txtDescription");
			    var enter = 13;
			    var feedline = 10;
			    var newstring = replace(txtPenjelasan.value, String.fromCharCode(enter), '@');
			    newstring = replace(newstring, String.fromCharCode(feedline), '*');
			    var opentag = 60;
			    newstring = replace(newstring, String.fromCharCode(opentag), '|');
			    showPopUp('../PK/FrmAdditionalInformationPK.aspx?text='+newstring+'&type=1','',400,400,Penjelasan)
			}
			
			function Konfirmasi(Text)
			{
				var txtKonfirmasi = document.getElementById("txtKTBResponse");
				if(navigator.appName == "Microsoft Internet Explorer")
				{
    				txtKonfirmasi.innerText=Text;		
				}
				else
				{
	    			txtKonfirmasi.value=Text;
				}				
			}		
			
			function ShowPPKonfirmasi()
			{
			    var txtKonfirmasi = document.getElementById("txtKTBResponse");
			    var enter = 13;
			    var feedline = 10;
			    var newstring = replace(txtKonfirmasi.value, String.fromCharCode(enter), '@');
			    newstring = replace(newstring, String.fromCharCode(feedline), '*');
			    var opentag = 60;
			    newstring = replace(newstring, String.fromCharCode(opentag), '|');				
			    showPopUp('../PK/frmAdditionalInformationPK.aspx?text='+newstring+'&type=2','',400,400,Konfirmasi)
			}
			
			function ShowPPKodeWarnaSelection()
			{
			    var indek = GetCurrentInputIndex();
			    var dgPKOrderDetail = document.getElementById("dgPKOrderDetail");
			    var KodeTipe = dgPKOrderDetail.rows[indek].getElementsByTagName("SPAN")[1];		
			    showPopUp('../General/FrmKodeWarna.aspx?type='+KodeTipe.innerHTML+'&pktype=2','',400,400,KodeWarna)
			}
			
			function KodeWarna(selectedColor)
			{
				var indek = GetCurrentInputIndex();
				var dtgPesananKendaraan = document.getElementById("dgPKOrderDetail");
				var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
				var tempParam = selectedColor.split(';');
			    KodeWarna.value = tempParam[0];
				var hiddenField = document.getElementById("HideField")
				hiddenField.value = tempParam[1];			
		
			}
			
			function GetCurrentInputIndex()
			{
				var dtgPesananKendaraan = document.getElementById("dgPKOrderDetail");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgPesananKendaraan.rows.length; index++)
				{
					inputs = dtgPesananKendaraan.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].className != "textRight")
								return index;
						}
					}
				}				
				return -1;
			}
			
		    function ShowPPSPLSelection()
		    {
		        var strProjectName='';
			    var lblProjectName = document.getElementById("lblProjectName");
			    var lblRencanaTebus = document.getElementById("lblOrderPlan");

			    if (lblProjectName != null && lblProjectName != 'undefined') {
			        strProjectName = lblProjectName.innerHTML;
			    }
			    var ddlModelValue = 1
			    showPopUp('../PopUp/PopUpSPL.aspx?projectname=' + strProjectName + '&rencanatebus=' + lblRencanaTebus.innerHTML, '', 500, 760, SPLSelection);
		    }
		
		    function SPLSelection(selectedSPL)
		    {
		        var btnRefresh = document.getElementById("btnRefresh");
		        var txtSPLSelection = document.getElementById("txtNomorSPL");
			    txtSPLSelection.value = selectedSPL;
			    if (navigator.appName == "Microsoft Internet Explorer") {
			        txtSPLSelection.focus();
			        txtSPLSelection.blur();
                }
			    else {
			        txtSPLSelection.onchange();
			    }
			    var btnRilis = document.getElementById("btnRilis");
			    btnRilis.disabled = true;
			    btnRefresh.click();
            }
		
		    function SetKonfirmasi()
		    {
			    var RbtnKonfirmasi1 = document.getElementById("RbtnKonfirmasi1");
			    var RbtnKonfirmasi2 = document.getElementById("RbtnKonfirmasi2");
			    var RbtnKonfirmasi3 = document.getElementById("RbtnKonfirmasi3");
			    var RbtnKonfirmasi4 = document.getElementById("RbtnKonfirmasi4");
			    var lblKonfirmasi1 = document.getElementById("lblKonfirmasi1");
			    var lblKonfirmasi2 = document.getElementById("lblKonfirmasi2");
			    var lblKonfirmasi3 = document.getElementById("lblKonfirmasi3");
			    var lblSearchKonfirmasi = document.getElementById("lblSearchKonfirmasi");
			    var txtKTBResponse =document.getElementById("txtKTBResponse");

			    if (RbtnKonfirmasi1 != null){
			        if (RbtnKonfirmasi1.checked)
			        {		
				        lblSearchKonfirmasi.style.visibility="hidden";
				        txtKTBResponse.value=lblKonfirmasi1.innerHTML;
			        }
			    }
			
			    if (RbtnKonfirmasi2 != null){
			        if (RbtnKonfirmasi2.checked)
			        {		
				        lblSearchKonfirmasi.style.visibility="hidden";
				        txtKTBResponse.value=lblKonfirmasi2.innerHTML;
			        }
			    }

			    if (RbtnKonfirmasi3 != null){
			        if (RbtnKonfirmasi3.checked)
			        {		
				        lblSearchKonfirmasi.style.visibility="hidden";
				        txtKTBResponse.value=lblKonfirmasi3.innerHTML;
			        }
			    }

			    if (RbtnKonfirmasi4 != null){
			        if (RbtnKonfirmasi4.checked)
			        {
				        lblSearchKonfirmasi.style.visibility="visible";
				
			        }
			    }
		    }

		    function clearInputFile(f) {
		        if (f.value) {
		            try {
		                f.value = ''; //for IE11, latest Chrome/Firefox/Opera...
		            } catch (err) {
		            }
		            if (f.value) { //for IE5 ~ IE10
		                var form = document.createElement('form'), ref = f.nextSibling;
		                form.appendChild(f);
		                form.reset();
		                ref.parentNode.insertBefore(f, ref);
		            }
		        }
		    }

		    function UploadFiles(fileUpload) {
		        if (fileUpload.value != '') {
		            document.getElementById("btnUpload").click();
		        }
		    }


		    function SetPath(obj) {
		        document.getElementById("lblPath").innerText = obj.lowsrc;
		    }

		    function ShowEvidenceImage(obj) {
		        var fraImageTest = document.getElementById("fraImageTest");
		        fraImageTest.src = "../WebResources/GetImageGlobal.aspx?file=" + obj.lowsrc + "&hg=200&wd=200&type=ImageFile";

		        var divImageTest = document.getElementById("imgBox");
		        if (navigator.appName != "Microsoft Internet Explorer") {
		            divImageTest = obj.parentNode.parentNode.childNodes[1];
		        }
		        divImageTest.style.visibility = "visible";
		        divImageTest.innerHTML = '';
		        divImageTest.appendChild(fraImageTest);
		        divImageTest.style.left = (getElementLeft(obj)) + 'px';
		        divImageTest.style.top = (getElementTop(obj)) + 'px';

		        document.getElementById("lblPath").innerText = obj.lowsrc;
		    }

		    function HideEvidenceImage(obj) {
		        var divImageTest = document.getElementById("imgBox");
		        if (navigator.appName != "Microsoft Internet Explorer") {
		            divImageTest = obj.parentNode.parentNode.childNodes[1];
		        }
		        divImageTest.style.visibility = "hidden";
		    }

		    function getElementLeft(elm) {
		        var x = 0;
		        x = elm.offsetLeft;
		        elm = elm.offsetParent;
		        while (elm != null) {
		            x = parseInt(x) + parseInt(elm.offsetLeft) - 34;
		            elm = elm.offsetParent;
		        }
		        return x;
		    }

		    function getElementTop(elm) {
		        var y = 0;
		        y = elm.offsetTop;
		        elm = elm.offsetParent;
		        while (elm != null) {
		            y = parseInt(y) + parseInt(elm.offsetTop) - 24;
		            elm = elm.offsetParent;
		        }
		        return y;
		    }

		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN - Respon Pesanan Kendaraan Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="10%"><asp:label id="Label15" runat="server">Nomor Reg PK</asp:label></TD>
								<TD width="1%"><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 250px; HEIGHT: 18px"><asp:label id="lblNomorPKValue" runat="server"></asp:label></TD>
								<TD class="titleField" width="17%"><asp:label id="Label19" runat="server">Kategori</asp:label></TD>
								<TD width="1%"><asp:label id="Label25" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:dropdownlist id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist></TD>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px"><asp:label id="Label7" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 18px"><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 250px; HEIGHT: 18px"><asp:label id="lblStatus" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label21" runat="server">Tahun Perakitan</asp:label></TD>
								<TD><asp:label id="Label27" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblProductionYear" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="18%"><asp:label id="Label4" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD style="HEIGHT: 17px"><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 250px; HEIGHT: 17px"><asp:label id="lblPKDate" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Label20" runat="server">Jenis Pesanan</asp:label></TD>
								<TD><asp:label id="Label26" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlOrderType" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px"><asp:label id="Label23" runat="server">Nomor Pesanan</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:label id="Label29" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:label id="lblPKNumber" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px"><asp:label id="Label22" runat="server">Rencana Penebusan </asp:label></TD>
								<TD style="HEIGHT: 17px"><asp:label id="Label28" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:label id="lblOrderPlan" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="15%"><asp:label id="Label1" runat="server">Kode Dealer</asp:label>&nbsp;</TD>
								<TD width="1%"><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 200px; HEIGHT: 18px"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
                                <TD class="titleField" style="HEIGHT: 17px"><asp:label id="Label9" runat="server">Nomor Aplikasi </asp:label></TD>
								<TD style="HEIGHT: 17px"><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px">
									<asp:textbox onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" id="txtNomorSPL" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										runat="server" AutoPostBack="True"></asp:textbox>
									<%--<asp:LinkButton id="lbtnSearchSPL" Runat="server">
										<img style="cursor:hand" src="../images/popup.gif" border="0" alt="Klik Popup">
									</asp:LinkButton>--%>
									<asp:label id="lblSearchSPL" runat="server">
										<img style="cursor:hand" src="../images/popup.gif" border="0" alt="Klik Popup"></asp:label>&nbsp;
                                    <asp:linkbutton id="lnkReloadSPL" runat="server" Width="8px" CausesValidation="False">
								        <img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"></asp:linkbutton>
									<asp:button id="btnRefresh" runat="server" Text="Refresh" style="display:none"></asp:button>
								</TD>
							</TR>
                            <tr runat="server" id="trSPL" width="100%" border="0"></tr>
                            <tr valign="top">
								<TD class="titleField">
                                    <p>
                                        <asp:label id="Label2" runat="server">Nama Dealer</asp:label>
                                    </p>
                                    <p>
                                        <asp:label id="Label3" runat="server">Kota</asp:label>
                                    </p>
                                    <p><asp:label style="Z-INDEX: 0" id="Label33" runat="server">Cabang Dealer</asp:label></p>
								</TD>
								<TD>
                                    <p>:</p>
                                    <p>:</p>
                                    <p>:</p>
								</TD>
								<TD style="WIDTH: 200px; HEIGHT: 18px">
                                    <p>
                                        <asp:label id="lblDealerName" runat="server"></asp:label>
                                    </p>
                                    <p>
                                        <asp:label id="lblCity" runat="server"></asp:label>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblDealerBranch" runat="server" ></asp:Label>
                                    </p>
								</TD>
								<TD class="titleField" vAlign="top"><asp:label id="Label24" runat="server">Konfirmasi</asp:label></TD>
								<TD vAlign="top"><asp:label id="Label30" runat="server">:</asp:label></TD>
								<TD valign="top">
									<table>
										<tr>
											<td vAlign="top">
												<asp:RadioButton Visible="false" id="RbtnKonfirmasi1" onclick="SetKonfirmasi();" runat="server" Text=" " GroupName="Konfirmasi"></asp:RadioButton>
											</td>
											<td>
												<asp:Label Visible="false" id="lblKonfirmasi1" runat="server">Mohon segera kirim PO/SPK customer untuk dapat dibuatkan OC</asp:Label>
											</td>
										</tr>
										<tr>
											<td vAlign="top">
												<asp:RadioButton id="RbtnKonfirmasi2" onclick="SetKonfirmasi();" runat="server" Text=" " GroupName="Konfirmasi"></asp:RadioButton>
											</td>
											<td>
												<asp:Label id="lblKonfirmasi2" runat="server">Maaf permintaan unit tidak bisa dipenuhi karena keterbatasan stok</asp:Label>
											</td>
										</tr>
										<tr>
											<td vAlign="top">
												<asp:RadioButton Visible="false" id="RbtnKonfirmasi3" onclick="SetKonfirmasi();" runat="server" Text=" " GroupName="Konfirmasi"></asp:RadioButton>
											</td>
											<td>
												<asp:Label Visible="false" id="lblKonfirmasi3" runat="server">Maaf permintaan unit tidak bisa dipenuhi karena keterbatasan stok, silakan diatur dari alokasi reguler yang telah diberikan</asp:Label>
											</td>
										</tr>
										<tr>
											<td vAlign="top">
												<asp:RadioButton id="RbtnKonfirmasi4" onclick="SetKonfirmasi();" runat="server" Text=" " GroupName="Konfirmasi"></asp:RadioButton>
											</td>
											<td>
												<asp:textbox id="txtKTBResponse" runat="server" TextMode="MultiLine"   BackColor="#E0E0E0"
													Rows="3" Width="252px"></asp:textbox><asp:label id="lblSearchKonfirmasi" runat="server">
													<img id="imgSearchKonfirmasi" style="cursor:hand" src="../images/popup.gif" border="0"
														alt="Klik Popup"></asp:label>
											</td>
										</tr>
									</table>
								</TD>
                            </tr>
							<TR id="trProjectName" runat="server">
								<TD class="titleField" style="HEIGHT: 26px"><asp:label id="lblProjectNameTitle" runat="server">Nama Pesanan Khusus</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:label id="lblProjectNameTtk" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 200px; HEIGHT: 26px"><asp:label id="lblProjectName" runat="server"></asp:label></TD>

								<TD style="HEIGHT: 20px" class="titleField" runat="server" id="TOPTitle1">Fasilitas Bebas Bunga</TD>
								<TD style="HEIGHT: 20px" runat="server" id="TOPTtk1">:</TD>
								<TD style="HEIGHT: 26px" runat="server" id="TOPCol1">
									<asp:DropDownList id="ddlInterest" runat="server" Enabled="False">
										<asp:ListItem Value="0">Ya</asp:ListItem>
										<asp:ListItem Value="1">Tidak</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR id="trPenjelasanKonfirmasi" runat="server" valign="top">
								<TD id="tdPenjelasan" runat="server" class="titleField" vAlign="top"><asp:label id="lblPenjelasan" runat="server">Penjelasan</asp:label></TD>
								<TD id="tdTitik2" runat="server" vAlign="top"><asp:label id="lblTitik2" runat="server">:</asp:label></TD>
								<TD id="tdDescription" runat="server" style="WIDTH: 200px" vAlign="top"><asp:textbox id="txtDescription" runat="server" TextMode="MultiLine"   BackColor="#E0E0E0"
										Rows="3"></asp:textbox><asp:label id="lblSearchPenjelasan" runat="server">
										<img style="cursor:hand" src="../images/popup.gif" border="0" alt="Klik Popup"></asp:label></TD>

								<TD style="HEIGHT: 20px" class="titleField" runat="server" id="TOPTitle2">Fasilitas TOP</TD>
								<TD style="HEIGHT: 20px" runat="server" id="TOPTtk2">:</TD>
								<TD style="HEIGHT: 26px" runat="server" id="TOPCol2">
                                    <table>
                                        <tr>
                                            <td><asp:radiobutton id="rbtnDate" runat="server" GroupName="TOP" Text="s/d Tanggal&nbsp;" AutoPostBack="True" Enabled="False"></asp:radiobutton></td>
                                            <td><cc1:inticalendar id="icMaxDate" runat="server" Enabled="False"></cc1:inticalendar></td>
                                        </tr>
                                        <tr>
                                            <td><asp:radiobutton id="rbtnDay" runat="server" GroupName="TOP" Text="Hari&nbsp;&nbsp;" AutoPostBack="True" Enabled="False"></asp:radiobutton></td>
                                            <td><asp:textbox id="txtMaxDay" runat="server" Width="69px" ReadOnly="True" MaxLength="3">0</asp:textbox></td>
                                        </tr>
                                        <tr>
                                            <td><asp:radiobutton id="rbtnNone" runat="server" GroupName="TOP" Text="Tidak Ada" AutoPostBack="True" Enabled="False" Checked="True"></asp:radiobutton></td>
                                        </tr>
                                    </table>																			
								</TD>
							</TR>
							<TR id="trIsConfirmation" runat="server" valign="top">
								<td colspan="3">
                                    <asp:CheckBox runat="server" AutoPostBack="true" ID="cbxIsConfirmation" Visible="true" Text="Dealer bersedia dikirim unit tanpa menunggu form A"/>
                                </td>

								<TD style="HEIGHT: 20px" class="titleField" runat="server" id="TOPTitle3">Fasilitas Cicilan | Jumlah Hari TOP</TD>
								<TD style="HEIGHT: 20px" runat="server" id="TOPTtk3">:</TD>
								<TD style="HEIGHT: 26px" runat="server" id="TOPCol3">
									<asp:label id="lblNumOfInstallment" runat="server"></asp:label>
                                    &nbsp;|&nbsp;
                                    <asp:label id="lblMaxTOPDay" runat="server"></asp:label>
								</TD>
							</TR>
                            <TR id="trUploadDok" valign="top" runat="server">
                                <TD class="titleField">&nbsp;<asp:label id="lblUploadDok" runat="server" Visible="false">Upload Surat</asp:label></TD>
								<TD><asp:label id="lbltitik2Upload" runat="server" Visible="false">:</asp:label></TD>
								<TD>
                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                <INPUT id="UploadFile" style="WIDTH: 264px; HEIGHT: 20px" type="file" size="24" name="fileUpload" runat="server" Visible="false">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lblFileName" runat="server"></asp:LinkButton>
                                                <asp:Label ID="lblEvidencePath" runat="server" Visible="false"></asp:Label>
                                                <asp:button id="btnUpload" runat="server" Text="Upload" Style="display:none"></asp:button>
                                            </td>
                                            <td>
                                                <div id="imgbox">
                                                    <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                </div>
                                                <asp:LinkButton ID="lnkbtnFileName" runat="server" ></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnDeleteFile" Visible="false" OnClientClick="return confirm('Anda yakin mau hapus?');" Text="Hapus" runat="server">
                                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
								</TD>
                                <TD style="HEIGHT: 10px" class="titleField">
                                    <p>
                                        <asp:label style="Z-INDEX: 0" id="lblKodeDiskonFleet" runat="server" Text="Kode Diskon Fleet" Visible="false"></asp:label>
                                    </p>
                                    <p><asp:label id="Label42" runat="server" Visible="false">Deskripsi SPL</asp:label></p>
								</TD>
								<TD style="HEIGHT: 2px">
                                    <p><asp:label style="Z-INDEX: 0" id="lblKodeDiskonFleet2" runat="server" Text=":" Visible="false"></asp:label></p>
                                    <p><asp:label style="Z-INDEX: 0" id="Label6" runat="server" Text=":" Visible="false"></asp:label></p>
								</TD>
								<TD style="HEIGHT: 18px">
                                    <p>
                                        <asp:textbox onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
									            id="txtFleetDiscountCode" runat="server" Width="140px" MaxLength="20" Visible="false"></asp:textbox>
                                        <%--   <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Kode Diskon Fleet tidak boleh kosong"
									    ControlToValidate="txtFleetDiscountCode" EnableClientScript="false" Enabled="false">*</asp:requiredfieldvalidator>--%>
                                    </p>
                                    <p>
                                        <asp:label style="Z-INDEX: 0" id="lblDeskripsiSPL" runat="server" Visible="false"></asp:label>
                                    </p>
                                </TD>
							</TR>
							<TR id="trGuarantee" valign="top" runat="server">
								<TD style="HEIGHT: 24px" class="titleField">
									<asp:label style="Z-INDEX: 0" id="lblGuarantee" runat="server">Jaminan</asp:label></TD>
								<TD style="HEIGHT: 24px">
									<asp:label style="Z-INDEX: 0" id="lblColonGuarantee" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 200px; HEIGHT: 24px">
									<asp:CheckBox id="chkDeposit" runat="server" Text=" "></asp:CheckBox></TD>

								<TD class="titleField" style="HEIGHT: 10px"><asp:label id="lblHeadPKNumberTitle" runat="server"><asp:label id="Label43" runat="server" Visible="false">Nomor Induk PK</asp:label></asp:label></TD>
								<TD style="HEIGHT: 2px"><asp:label id="lblHeadPKNumberTtk" runat="server" Visible="false">:</asp:label></TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblHeadPKNumberValue" runat="server" Visible="false"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div id="div1" style="HEIGHT: 140px; OVERFLOW: auto"><asp:datagrid id="dgPKOrderDetail" runat="server" Width="100%" BackColor="#E0E0E0" CellSpacing="1"
											OnUpdateCommand="dtgPKOrderDetail_Update" OnEditCommand="dtgPKOrderDetail_Edit" OnItemCommand="dtgPKOrderDetail_ItemCommand" OnCancelCommand="dtgPKOrderDetail_Cancel"
											GridLines="Horizontal" CellPadding="3" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False">
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%# container.itemindex+1 %>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Model/Type/Warna">
													<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblModel" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label id="lblEditModel" runat="server"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tipe">
													<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblType" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False"></FooterStyle>
													<EditItemTemplate>
														<asp:Label id="lblEditType" runat="server"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Warna">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblColor runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.ID") %>' Visible="False">
														</asp:Label>
														<asp:Label id="lblColorString" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False"></FooterStyle>
													<EditItemTemplate>
														<asp:TextBox id=txtEditKodeWarna runat="server" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleColorCode" ) %>' MaxLength="4" Size="2">
														</asp:TextBox>
														<asp:Label id="lblEditKodeWarna" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Permintaan Dealer">
													<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblUnitDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TargetQty") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="Persetujuan MMKSI">
													<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblResponKTB runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ResponseQty") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="Harga(PD) (Rp)" DataFormatString="{0:#,###}">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="PPH22(PD)" DataFormatString="{0:#,###}">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Diskon Per Unit(Rp)">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="txtDiskon" readonly="True" onkeypress="return numericOnlyUniv(event)" onblur="omitSomeCharacter('txtDiskon','<>?\/*%-$');"
															onKeyUp="pic(this,this.value,'9999999999','N')" runat="server" Size="9" CssClass="textRight">0</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deposit A(Rp)" Visible="false">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="txtSurcharge" readonly="True" onkeypress="return numericOnlyUniv(event)" onblur="omitSomeCharacter('txtSurcharge','<>?\/*%-$');"
															onKeyUp="pic(this,this.value,'9999999999','N')" runat="server" Size="9" CssClass="textRight">0</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="Disetujui MMKSI (Unit)">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="Harga(SK) (Rp)" DataFormatString="{0:#,###}">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="PPh22(SK)" DataFormatString="{0:#,###}">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
													CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; alt=&quot;Batal&quot;&gt;"
													EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah Warna&quot;&gt;">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
												</asp:EditCommandColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnHapus" runat="server" CommandName="Hapus">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="MaterialNumber" ReadOnly="True" HeaderText="MaterialNumber"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
									<asp:label id="lblMessage" runat="server" Width="440px" EnableViewState="False" Font-Bold="True"
										ForeColor="Red"></asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 22px" colSpan="6">
									<TABLE id="Table3" style="WIDTH: 738px; HEIGHT: 22px" cellSpacing="1" cellPadding="1" width="738"
										border="0">
										<TR>
											<TD style="WIDTH: 63px"><asp:label id="Label31" runat="server">
													<b>Total Unit</b></asp:label></TD>
											<TD style="WIDTH: 5px"><asp:label id="Label32" runat="server">:</asp:label></TD>
											<TD style="WIDTH: 275px"><asp:label id="lblTotalUnitValue" runat="server"></asp:label></TD>
											<TD style="WIDTH: 117px"><asp:label id="Label35" runat="server">
													<b>Total Harga Tebus</b></asp:label></TD>
											<TD style="WIDTH: 1px"><asp:label id="Label36" runat="server">:</asp:label></TD>
											<TD><asp:label id="lblTotalHargaUnitPD" runat="server"></asp:label></TD>
										</TR>
									</TABLE>
									<%--<b>Surcharge :</b> Potongan harga untuk Option Part, Body Part, atau Deposit A--%>
								</TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<TABLE id="tblOperator" cellSpacing="1" cellPadding="1" border="0" runat="server">
										<TR>
											<TD><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Height="22px"></asp:button></TD>
											<TD><asp:button id="btnBack" Text="Kembali" Runat="server" CausesValidation="false"></asp:button></TD>
                                            <TD><asp:button id="btnRilis" Text="Rilis" Width="60px" Runat="server" CausesValidation="false" Style="display:none"></asp:button></TD>
											<td><asp:label id="lblPath" runat="server" Style="display:none"></asp:label>
											</td>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
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
			SetKonfirmasi();
		</script>
	</body>
</HTML>

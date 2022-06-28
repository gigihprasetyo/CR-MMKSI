<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPengajuanFaktur.aspx.vb" Inherits="FrmPengajuanFaktur" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
		<title>FAKTUR KENDARAAN - Pengajuan Faktur</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function CheckAll(aspCheckBoxID, checkVal) {
		        re = new RegExp(':' + aspCheckBoxID + '$')
		        for (i = 0; i < document.forms[0].elements.length; i++) {
		            elm = document.forms[0].elements[i]
		            if (elm.type == 'checkbox') {
		                if (re.test(elm.name)) {
		                    elm.checked = checkVal
		                }
		            }
		        }
		    }
		    function Selection() {
		        alert('susausa');
		        var reply = confirm('Hai?');
		        if (reply) {
		            alert('yes');



		        }
		    }
		    function ShowPPTujuanSelection() {
		        var txtSPKNumber = document.getElementById('txtSPKNumber');
		        var NoRangka = document.getElementById('txtNoRangka');
		        
		        if (txtSPKNumber.value == "") {
		            alert("Silahkan pilih nomor SPK terlebih dahulu");
		            return;
		        }
		        else {
		            showPopUp('../PopUp/PopUpCustomerSelectionOne.aspx?FilterLoginDealer=True&SPKNumber=' + txtSPKNumber.value + "&Chassis=" + NoRangka.value, '', 500, 760, TujuanSelection);
		        }
		    }
		    function ShowPPTujuanSelection2() {
		        var txtSPKNumber = document.getElementById('txtSPKNumber');
		       
		        if (txtSPKNumber.value == "") {
		            alert("Silahkan pilih nomor SPK terlebih dahulu");
		            return;
		        }
		        else {
		            showPopUp('../PopUp/PopUpCustomerSelectionOne.aspx?FilterLoginDealer=True&SPKNumber=' + txtSPKNumber.value, '', 500, 760, TujuanSelection);
		        }
		    }


		    

		    function TujuanSelection(selectedTujuan) {
		        var txtCustomerCode = document.getElementById('txtCustomerCode');
		        var lblName = document.getElementById('lblName');
		        var lblAlamat = document.getElementById('lblAlamat');
		        var lblGedung = document.getElementById('lblGedung');
		        var lblKelurahan = document.getElementById('lblKelurahan');
		        var lblKecamatan = document.getElementById('lblKecamatan');
		        var lblKodePos = document.getElementById('lblKodePos');
		        var lblKodya = document.getElementById('lblKodya');
		        var lblPropinsi = document.getElementById('lblPropinsi');
		        var lblEmail = document.getElementById('lblEmail');
		        var lblPhone = document.getElementById('lblPhone');
		        var lblName2 = document.getElementById('lblName2');
		        var lblNoKTP = document.getElementById('lblNoKTP');
		        var btntxtCustomerCode = document.getElementById("btntxtCustomerCode");
		        var lbltxtCustomerCode = document.getElementById('lbltxtCustomerCode');
		        var hdnValid = document.getElementById("hdnValid");
		        var hfSPKDetailCustomerID = document.getElementById("hfSPKDetailCustomerID");

		        selectedTujuan = selectedTujuan.replace(/&amp;/g, '&');//&amp=>'&'
		        var arrValue = selectedTujuan.split(';');
		        
		        //CustomerRequest COde Chekc
		        if (arrValue[0] == null || arrValue[0] == 'null') {
		            arrValue[0] = '';
		        }
		        //CustomerRequest ID Chekc
		        if (arrValue[1] == null || arrValue[7] == 'null' || arrValue[1] == '0') {
		            arrValue[1] = 0;
		        }

		        txtCustomerCode.value = arrValue[0];
		        lbltxtCustomerCode.innerHTML = arrValue[0];

		        var _pengajuanKOnsumen = arrValue[1];
		        var _CodeKomsumen = arrValue[0];

		       
		        if (_pengajuanKOnsumen == "0" || _pengajuanKOnsumen == null || _pengajuanKOnsumen == 0) {
		            ClearInfoPelanggan();
		            hdnValid.value = -1;
		            alert("Belum Pengajuan Konsumen");
		            return;
		        }

		        if (_CodeKomsumen == '') {
		            ClearInfoPelanggan();
		            hdnValid.value = -1;
		            alert("Kode Pelanggan Belum Ada");
		            return;
		        }
		        hdnValid.value = 1;
		        
		        lblName.innerHTML = arrValue[11];
		        lblGedung.innerHTML = arrValue[2];
		        lblAlamat.innerHTML = arrValue[3];
		        lblKelurahan.innerHTML = arrValue[4];
		        lblKecamatan.innerHTML = arrValue[5];
		        lblKodePos.innerHTML = arrValue[6];
		        lblKodya.innerHTML = arrValue[7];
		        lblPropinsi.innerHTML = arrValue[8];
		        lblName2.innerHTML = arrValue[12];
		        lblNoKTP.innerHTML = arrValue[13];
		     
		        hfSPKDetailCustomerID.value = arrValue[14];

		        if (navigator.appName == 'Microsoft Internet Explorer') {
		            txtCustomerCode.focus();
		            txtCustomerCode.blur();
		        }
		        else {
		            //txtCustomerCode.onchange();
		        }
		        try {
		            if (btntxtCustomerCode) btntxtCustomerCode.click();
		        } catch (e) {
		            console.log(e.message);
		        }
		        
		    }

		    function ShowSalesmanSelection() {
		        showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=1', '', 500, 760, SalemanSelection);

		    }
		    function SalemanSelection(selectedSales) {
		        var temp = selectedSales.split(";")
		        var txtSalesman = document.getElementById('txtSalesmanCode');
		        var txtSalesNama = document.getElementById('lblNamaSales');
		        var txtSalesLevel = document.getElementById('lblLevel');
		        var txtSalesJabatan = document.getElementById('lblPosisi');
		        var lblDealerBranch = document.getElementById('lblDealerBranch');

		        txtSalesman.value = temp[0];
		        txtSalesNama.innerHTML = temp[1];
		        txtSalesLevel.innerHTML = temp[4];
		        txtSalesJabatan.innerHTML = temp[3];
		        lblDealerBranch.innerHTML = temp[5];

		    }

		    function ShowSPKSelection() {
		        //showPopUp('../PopUp/PopUpSPKHeader.aspx?IsGroupDealer=1','',500,800,SPKSelection);
		        showPopUp('../PopUp/PopUpSPKTersedia.aspx?IsGroupDealer=1', '', 500, 800, SPKSelection);
		    }
		    function SPKSelection(selectedSPK) {
		        var temp = selectedSPK.split(";")
		        var txtSPKNumber = document.getElementById('txtSPKNumber');
		        var txtSalesman = document.getElementById('txtSalesmanCode');
		        var txtSalesNama = document.getElementById('lblNamaSales');
		        var txtSalesLevel = document.getElementById('lblLevel');
		        var txtSalesJabatan = document.getElementById('lblPosisi');
		        var lblDealerBranch = document.getElementById('lblDealerBranch');

		        //var txtCustomerCode = document.getElementById('txtCustomerCode');
		        //var lbltxtCustomerCode = document.getElementById('lbltxtCustomerCode');
		        //var btntxtCustomerCode = document.getElementById("btntxtCustomerCode");
		        //var hdnValid = document.getElementById("hdnValid");

		        txtSPKNumber.value = temp[0];
		        txtSalesman.value = temp[1];
		        txtSalesNama.innerHTML = temp[2];
		        txtSalesLevel.innerHTML = temp[3];
		        txtSalesJabatan.innerHTML = temp[4];
		        if (temp[5] != null && temp[5] != 'null') {
		            lblDealerBranch.innerHTML = temp[5];
		        }
		        ////CustomerRequest COde Chekc
		        //if (temp[6] == null || temp[6] == 'null') {
		        //    temp[6] = '';
		        //}
		        ////CustomerRequest ID Chekc
		        //if (temp[7] == null || temp[7] == 'null' || temp[7] == '0') {
		        //    temp[7] = 0;
		        //}

		        ////txtCustomerCode.value = temp[6];
		        ////lbltxtCustomerCode.innerHTML = temp[6];

		        //var _pengajuanKOnsumen = temp[7];
		        //var _CodeKomsumen = temp[6];


		        ////console.log(temp);
		        //if (_pengajuanKOnsumen == "0" || _pengajuanKOnsumen == null || _pengajuanKOnsumen == 0) {
		        //    ClearInfoPelanggan();
		        //    hdnValid.value = -1;
		        //    alert("Belum Pengajuan Konsumen");
		        //    return;
		        //}


		        //if (_CodeKomsumen == '') {
		        //    ClearInfoPelanggan();
		        //    hdnValid.value = -1;
		        //    alert("Kode Pelanggan Belum Ada");
		        //    return;
		        //}
		        //hdnValid.value = 1;
		        //if (btntxtCustomerCode) btntxtCustomerCode.click();

		    }

		    function ClearInfoPelanggan() {
		        var txtCustomerCode = document.getElementById('txtCustomerCode');
		        var lbltxtCustomerCode = document.getElementById('lbltxtCustomerCode');


		        txtCustomerCode.value = "";
		        lbltxtCustomerCode.innerHTML = "";

		        var lblName = document.getElementById('lblName');
		        var lblAlamat = document.getElementById('lblAlamat');
		        var lblGedung = document.getElementById('lblGedung');
		        var lblKelurahan = document.getElementById('lblKelurahan');
		        var lblKecamatan = document.getElementById('lblKecamatan');
		        var lblKodePos = document.getElementById('lblKodePos');
		        var lblKodya = document.getElementById('lblKodya');
		        var lblPropinsi = document.getElementById('lblPropinsi');
		        var lblEmail = document.getElementById('lblEmail');
		        var lblPhone = document.getElementById('lblPhone');
		        var lblName2 = document.getElementById('lblName2');
		        var lblNoKTP = document.getElementById('lblNoKTP');


		        txtCustomerCode.value = '';
		        lblName.innerHTML = '';
		        lblGedung.innerHTML = '';
		        lblAlamat.innerHTML = '';
		        lblKelurahan.innerHTML = '';
		        lblKecamatan.innerHTML = '';
		        lblKodePos.innerHTML = '';
		        lblKodya.innerHTML = '';
		        lblPropinsi.innerHTML = '';
		        lblName2.innerHTML = '';
		        lblNoKTP.innerHTML = '';
		    }


		    function RebindVehicleKind(txt) {
		        var indek = GetIndex(txt);


		        //var noRangka = dtgPengajuanFaktur.rows[indek].getElementsByTagName("INPUT")[0];
		        var dtgPengajuanFaktur = document.getElementById("dtgPengajuanFaktur");
		        var btnRangka = document.getElementById("dtgPengajuanFaktur__ctl" + (indek + 1) + "_btnVehicleKindF");
		        // console.log(btnRangka);
		        if (btnRangka) btnRangka.click();
		    }

		    function RebindVehicleKindOri(txt) {
		        var sPre = txt.id.substr(0, 25);
		        var sDDLID = sPre + 'btnVehicleKindF';
		        var btnVehicleKindF = txt.parentNode.childNodes[1];
		        if (btnVehicleKindF) btnVehicleKindF.click();
		    }

		    function BindVehicleModel(ddl) {
		        var indek = GetIndex(ddl);
		        var val = ddl.options[ddl.selectedIndex].value;

		        var dtgPengajuanFaktur = document.getElementById("dtgPengajuanFaktur");
		        var btnVehicleModelF = document.getElementById("dtgPengajuanFaktur__ctl" + (indek + 1) + "_btnVehicleModelF");
		        if (btnVehicleModelF) btnVehicleModelF.click();
		    }

		    function GetIndex(CtlID) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            var row = CtlID.parentElement.parentElement;
		            indexRow = row.rowIndex;
		            return row.rowIndex;
		        }
		        else {
		            var row = CtlID.parentNode.parentNode;
		            indexRow = row.rowIndex;
		            return row.rowIndex;
		        }
		    }

		    function ShowFleetReqSelection() {
		        showPopUp('../PopUp/PopUpFleetReqTersedia.aspx?IsGroupDealer=1', '', 500, 800, FleetReqSelection);
		    }

		    function FleetReqSelection(selectedFleetReq) {
		        try {
		            var temp = selectedFleetReq.split(";");
		            var txtNoFleetReq = document.getElementById('txtNoFleetReq');
		            txtNoFleetReq.value = temp[0];
		        } catch (e) {
		            alert(e.message);
		        }

		    }

		    function ShowMCPSelection() {
		        showPopUp('../PopUp/PopUpMCPTersedia.aspx?IsGroupDealer=0', '', 500, 800, MCPSelection);
		    }

		    function MCPSelection(selectedMCP) {
		        var temp = selectedMCP.split(";")
		        var txtMCPNumber = document.getElementById('txtMCPNumber');
		        var lblIntitutionName = document.getElementById('lblIntitutionName');
		        txtMCPNumber.value = temp[0];
		        lblIntitutionName.innerHTML = temp[1];
		    }

		    function ShowLKPPSelection() {
		        var hdnVC = document.getElementById("hdnVC");
		        if (hdnVC.value == '') {
		            showPopUp('../PopUp/PopUpLKPPTersedia.aspx?IsGroupDealer=1', '', 500, 800, LKPPSelection);
		        } else {
		            showPopUp('../PopUp/PopUpLKPPTersedia.aspx?IsGroupDealer=1&VC=' + hdnVC.value, '', 500, 800, LKPPSelection);
		        }
		        
		    }

		    function LKPPSelection(selectedLKPP) {
		        var temp = selectedLKPP.split(";")
		        //console.log(temp);
		        //console.log(selectedLKPP);
		        var txtLKPPNumber = document.getElementById('txtLKPPNumber');
		        var lblinstitutionName2 = document.getElementById('lblinstitutionName2');
		        txtLKPPNumber.value = temp[0];
		        lblinstitutionName2.innerHTML = temp[1];
		    }

		    function showAlert(e) {
		        if (e.checked) {
		            alert("Apakah anda yakin akan membuat faktur temporary.");
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
                height: 19px;
            }

            .auto-style2 {
                height: 19px;
            }
        </style>
</head>
<body ms_positioning="GridLayout" onload="javascript:window.history.forward(1);">
		<form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="1" width="100%">
							<tr>
                <td>
                    <asp:TextBox ID="temp" Visible="False" runat="server"></asp:TextBox>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 17px">FAKTUR KENDARAAN&nbsp;- Buat Permohonan</td>
							</tr>
							<tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
                            <td style="height: 6px" height="6">
                                <img height="1" src="../images/dot.gif" border="0"></td>
							</tr>
                    </table>
                </td>
            </tr>
							<tr>
                <td>
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" style="width: 24%">Kode Dealer</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 75%">
                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNamaDealer" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="Label1" runat="server">No. Reg. SPK</asp:Label></td>
								<td width="1%">:</td>
                            <td width="25%">
                                <asp:TextBox ID="txtSPKNumber" runat="server" Width="104px"></asp:TextBox><asp:Label ID="lblSPKNumber" runat="server" Width="16px" Visible="true">
                                    
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" visible="false" border="0" >
                                </asp:Label>&nbsp;</td>
                            <td class="titleField" width="24%"></td>
								<td width="1%"></td>
                            <td width="25%"><asp:HiddenField ID="txtNoRangka" runat="server"> </asp:HiddenField></td>
							</tr>
							<tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="Label2" runat="server">Kode Konsumen</asp:Label></td>
								<td width="1%">:
								</td>
								<td width="25%">
                                    <asp:Label runat="server" ID="lbltxtCustomerCode" Style="display:none;"></asp:Label>
                                    <asp:Button runat="server" ID="btntxtCustomerCode" Style="display:none;"  CausesValidation="false"/>
                                    <asp:HiddenField runat="server" ID="hdnValid" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdnVC" />
                                    <asp:HiddenField runat="server" ID="hfSPKDetailCustomerID" />
                                    <%--<div style="display: none;">--%>
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="104px"></asp:TextBox>
                                        <asp:ImageButton ID="imgDealer" runat="server" ImageUrl="../images/popup.gif" Visible="False"></asp:ImageButton>
                                        <asp:Label ID="lblPopUp" runat="server" Width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                    <%--</div>--%>
								</td>
                            <td class="titleField" width="24%">Salesman</td>
								<td width="1%">:
								</td>
                            <td width="25%">
                                <asp:TextBox ID="txtSalesmanCode" runat="server" Width="104px"></asp:TextBox><asp:Label ID="lblShowSalesman" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label></td>
							</tr>
						</table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table4" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" style="width: 24%">Nama 1</td>
                            <td style="width: 1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                            <td class="titleField" width="24%">Cabang Dealer</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblDealerBranch" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 24%">Nama 2</td>
                            <td style="width: 1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblName2" runat="server"></asp:Label></td>
                            <td class="titleField" width="24%">Nama</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblNamaSales" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Gedung</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:Label ID="lblGedung" runat="server"></asp:Label></td>
                            <td class="titleField" width="24%">Level</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblLevel" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Alamat</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblAlamat" runat="server"></asp:Label></td>
                            <td class="titleField" style="height: 18px">Jabatan</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:Label ID="lblPosisi" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Kelurahan</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblKelurahan" runat="server"></asp:Label></td>
                            <td runat="server" id="fleetTD1" class="titleField" style="height: 18px;">No Extended Free Service</td>
                            <td runat="server" id="fleetTD2">:</td>
                            <td runat="server" id="fleetTD3">
                                <asp:TextBox ID="txtNoFleetReq" runat="server" Width="150px"></asp:TextBox>
                                <asp:Label ID="lblNoFleetReq" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kecamatan</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblKecamatan" runat="server"></asp:Label></td>
                            <td runat="server" id="MCP_TD1" class="titleField" style="height: 18px">Nomor MCP</td>
                            <td runat="server" id="MCP_TD2">:</td>
                            <td runat="server" id="MCP_TD3">
                                <asp:TextBox ID="txtMCPNumber" runat="server" Width="150px"></asp:TextBox>
                                <asp:Label ID="lblMCPNumber" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Pos</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblKodePos" runat="server"></asp:Label></td>
                            <td runat="server" id="MCP_TD4" class="titleField" style="height: 18px">Nama Institusi</td>
                            <td runat="server" id="MCP_TD5">:</td>
                            <td runat="server" id="MCP_TD6">
                                <asp:Label ID="lblIntitutionName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Kodya/Kabupaten</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblKodya" runat="server"></asp:Label></td>
								<%--<TD><asp:CheckBox ID="chkIsTemporary" runat="server" OnClick="showAlert(this)" Text=" Temporary Faktur" class="titleField" Visible="false"></asp:CheckBox></TD>--%>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">Propinsi</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblPropinsi" runat="server"></asp:Label></td>
                            <td runat="server" id="Pengadaan_TD1" class="titleField" style="height: 18px">Nomor Pengadaan</td>
                            <td runat="server" id="Pengadaan_TD2">:</td>
                            <td runat="server" id="Pengadaan_TD3">
                                <asp:TextBox ID="txtLKPPNumber" runat="server" Width="150px"></asp:TextBox>
                                <asp:Label ID="lblsearchLKPP" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nomor KTP/TDP</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoKTP" runat="server"></asp:Label></td>
                            <td runat="server" id="Pengadaan_TD4" class="titleField" style="height: 18px">Nama Institusi</td>
                            <td runat="server" id="Pengadaan_TD5">:</td>
                            <td runat="server" id="Pengadaan_TD6">
                                <asp:Label ID="lblinstitutionName2" runat="server"></asp:Label></td>
                        </tr>
                        <asp:Panel ID="Phone" Visible="False" runat="server">
                            <tr>
                                <td class="titleField">Email</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="titleField">Telephone / Fax</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                                <td> <asp:Label ID="lblNoRabgka" runat="server" Visible="false"></asp:Label></td>
                                <td></td>
                                <td></td>
                            </tr>
                           
                        </asp:Panel>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dtgPengajuanFaktur" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
							BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Check">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
                                    <input id="chkAllItems" onclick="CheckAll('chkSelect', document.forms[0].chkAllItems.checked)"
											type="checkbox">
									</HeaderTemplate>
									<ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Rangka">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
                                    <asp:Label ID="lblNomorRangka" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChassisNumber" )  %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
                                    <asp:TextBox ID="txtFooterNomorRangka" runat="server" Width="120px" BackColor="White" size="2"
											OnChange="RebindVehicleKind(this);"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnVehicleKindF" Text="" CommandName="RebindVehicleKind" Style="display: none;"></asp:Button>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jenis ">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblVehicleKind"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlVehicleKindF" Width="120" OnChange="BindVehicleModel(this)"></asp:DropDownList>
                                    <asp:Button runat="server" ID="btnVehicleModelF" Text="" CommandName="RebindVehicleModel" Style="display: none;"></asp:Button>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblVehicleModel"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlVehicleModelF" Width="120"></asp:DropDownList>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model Tipe Warna">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
                                    <asp:Label ID="lblTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialDescription") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EngineNumber" HeaderText="Nomor Mesin">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Tanggal Faktur">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
                                    <asp:Label ID="lblTanggalFaktur" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.FakturDate"),"dd/MM/yyy") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
                                    <cc1:IntiCalendar ID="icMaxDate" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
									</FooterTemplate>
									<EditItemTemplate>
                                    <cc1:IntiCalendar ID="icEditMaxDate" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Rangka Pengganti">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
                                    <asp:Label ID="lblNoRangkaPengganti" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
                                    <asp:TextBox ID="txtFooterNoRangkaPengganti" runat="server" Width="120px" size="2"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
                                    <asp:TextBox ID="txtEditNoRangkaPengganti" runat="server" Width="120px" size="2"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CommandName="delete" CausesValidation="False">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnEdit" Visible="False" runat="server" Text="Edit" CommandName="edit" CausesValidation="False">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
                                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add" CausesValidation="False">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
                                    <asp:LinkButton ID="lbtnSave" TabIndex="40" runat="server" Text="Simpan" CommandName="update" CausesValidation="True">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" TabIndex="50" runat="server" Text="Batal" CommandName="cancel" CausesValidation="True">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                    <asp:Label ID="lblError" runat="server" Width="100%" ForeColor="Red"></asp:Label>
                    <div id="dvLabelerror" runat="server" style="overflow-y: auto; overflow-x: auto">
                       
                        <style type="text/css">
                                a:visited {
                                    color: Blue;
                                }

                                a:hover {
                                    color: Purple;
                                }
                        </style>
                        <table id="tblError" runat="server" style="width: 100%">
                            <tr id="trHeader" runat="server">
                                <td style="width=100%">
                                    <asp:Label ID="lblError3" Font-Size="12px" Font-Bold="true" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:LinkButton ID="lnkShow" Text="Show Detail" Visible="false" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="trDetail" runat="server">
                                <td>
                                    <asp:BulletedList ID="bltError" ForeColor="Red" runat="server"></asp:BulletedList>
                                </td>
                               <%-- <td style="width=50%">
                                    <asp:Label ID="lblError1" runat="server" Width="100%" ForeColor="Red"></asp:Label>
                                </td>
                                <td style="width=50%">
                                    <asp:Label ID="lblError2" runat="server" Width="100%" ForeColor="Red"></asp:Label>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Width="88px" Text="Simpan"></asp:Button>&nbsp;
						<asp:Button ID="btnUpdateProfil" runat="server" Width="88px" Text="Update Profil" Enabled="False"></asp:Button></td>
            </tr>
        </table>
        <input id="hdnIsMCP" type="hidden" runat="server" name="hdnIsMCP">
        <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
        <input id="hdnVerifyMCP" type="hidden" runat="server" name="hdnVerifyMCP">

        <input id="hdnIsLKPP" type="hidden" runat="server" name="hdnIsLKPP">
        <input id="hdnLKPPConfirmation" type="hidden" runat="server" name="hdnLKPPConfirmation">
        <input id="hdnVerifyLKPP" type="hidden" runat="server" name="hdnVerifyLKPP">
		</form>
	</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPengajuanFaktur.aspx.vb" Inherits="FrmPengajuanFaktur" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FAKTUR KENDARAAN - Pengajuan Faktur</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
		        showPopUp('../PopUp/PopUpCustomerSelectionOne.aspx?FilterLoginDealer=True', '', 500, 760, TujuanSelection);
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

		        selectedTujuan = selectedTujuan.replace(/&amp;/g, '&');//&amp=>'&'
		        var arrValue = selectedTujuan.split(';');
		        txtCustomerCode.value = arrValue[0];
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

		        if (navigator.appName == 'Microsoft Internet Explorer') {
		            txtCustomerCode.focus();
		            txtCustomerCode.blur();
		        }
		        else {
		            //txtCustomerCode.onchange();
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
		        txtSalesman.value = temp[0];
		        txtSalesNama.innerHTML = temp[1];
		        txtSalesLevel.innerHTML = temp[4];
		        txtSalesJabatan.innerHTML = temp[3];
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
		        txtSPKNumber.value = temp[0];
		        txtSalesman.value = temp[1];
		        txtSalesNama.innerHTML = temp[2];
		        txtSalesLevel.innerHTML = temp[3];
		        txtSalesJabatan.innerHTML = temp[4];
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


		    function ShowSalesmanSelection() {
		        showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=1', '', 500, 760, SalemanSelection);

		    }
		    function SalemanSelection(selectedSales) {
		        var temp = selectedSales.split(";")
		        var txtSalesman = document.getElementById('txtSalesmanCode');
		        var txtSalesNama = document.getElementById('lblNamaSales');
		        var txtSalesLevel = document.getElementById('lblLevel');
		        var txtSalesJabatan = document.getElementById('lblPosisi');
		        txtSalesman.value = temp[0];
		        txtSalesNama.innerHTML = temp[1];
		        txtSalesLevel.innerHTML = temp[4];
		        txtSalesJabatan.innerHTML = temp[3];
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
		        txtSPKNumber.value = temp[0];
		        txtSalesman.value = temp[1];
		        txtSalesNama.innerHTML = temp[2];
		        txtSalesLevel.innerHTML = temp[3];
		        txtSalesJabatan.innerHTML = temp[4];
		    }
		    //function RebindVehicleKind(txt)
		    //{
		    //	var sPre =txt.id.substr(0,25);
		    //	var sDDLID= sPre+'btnVehicleKindF';
		    //	var btnVehicleKindF = txt.parentNode.childNodes[2];

		    //	if(btnVehicleKindF) btnVehicleKindF.click();
		    //}

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
		        showPopUp('../PopUp/PopUpLKPPTersedia.aspx?IsGroupDealer=0', '', 500, 800, LKPPSelection);
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
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="javascript:window.history.forward(1);">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%">
				<TR>
					<TD><asp:textbox id="temp" Visible="False" Runat="server"></asp:textbox>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 17px">FAKTUR KENDARAAN&nbsp;- Buat Permohonan</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" style="WIDTH: 24%">Kode Dealer</TD>
								<TD style="WIDTH: 1%">:</TD>
								<TD style="WIDTH: 75%"><asp:label id="lblKodeDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD><asp:label id="lblNamaDealer" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%"><asp:label id="Label1" runat="server">No. Reg. SPK</asp:label></td>
								<td width="1%">:</td>
								<td width="25%"><asp:textbox id="txtSPKNumber" runat="server" Width="104px"></asp:textbox><asp:label id="lblSPKNumber" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>&nbsp;</td>
								<TD class="titleField" width="24%"></TD>
								<td width="1%"></td>
								<TD width="25%"></TD>
							</tr>
							<tr>
								<td class="titleField" width="24%"><asp:label id="Label2" runat="server">Kode Konsumen</asp:label></td>
								<td width="1%">:
								</td>
								<td width="25%">
									<asp:textbox id="txtCustomerCode" runat="server" Width="104px"></asp:textbox>
									<asp:ImageButton ID="imgDealer" Runat="server" ImageUrl="../images/popup.gif" Visible="False"></asp:ImageButton>
									<asp:label id="lblPopUp" runat="server" width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
								</td>
								<TD class="titleField" width="24%">Salesman</TD>
								<td width="1%">:
								</td>
								<TD width="25%"><asp:textbox id="txtSalesmanCode" runat="server" Width="104px"></asp:textbox><asp:label id="lblShowSalesman" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table4" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" style="WIDTH: 24%">Nama 1</TD>
								<TD style="WIDTH: 1%">:</TD>
								<TD width="25%"><asp:label id="lblName" runat="server"></asp:label></TD>
								<TD class="titleField" width="24%">Nama</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblNamaSales" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%">Nama 2</TD>
								<TD style="WIDTH: 1%">:</TD>
								<TD width="25%"><asp:label id="lblName2" runat="server"></asp:label></TD>
								<TD class="titleField" width="24%">Level</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblLevel" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Gedung</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblGedung" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px">Jabatan</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblPosisi" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Alamat</TD>
								<TD>:</TD>
								<TD><asp:label id="lblAlamat" runat="server"></asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField">Kelurahan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblKelurahan" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px" >No Extended Free Service</TD>
								<TD>:</TD>
								<TD>
                                    <asp:textbox id="txtNoFleetReq" runat="server" Width="150px"></asp:textbox>
                                    <asp:label id="lblNoFleetReq" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Kecamatan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblKecamatan" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px">Nomor MCP</TD>
								<TD>:</TD>
								<TD>
                                    <asp:textbox id="txtMCPNumber" runat="server" Width="150px"></asp:textbox>
                                    <asp:label id="lblMCPNumber" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Kode Pos</TD>
								<TD>:</TD>
								<TD><asp:label id="lblKodePos" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px">Nama Institusi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblIntitutionName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Kodya/Kabupaten</TD>
								<TD>:</TD>
								<TD><asp:label id="lblKodya" runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField">Propinsi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblPropinsi" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px">Nomor Pengadaan</TD>
								<TD>:</TD>
								<TD>
                                    <asp:textbox id="txtLKPPNumber" runat="server" Width="150px"></asp:textbox>
                                    <asp:label id="lblsearchLKPP" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor KTP/TDP</TD>
								<TD>:</TD>
								<TD><asp:label id="lblNoKTP" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px">Nama Institusi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblinstitutionName2" runat="server"></asp:label></TD>
							</TR>
							<asp:panel id="Phone" Visible="False" Runat="server">
								<TR>
									<TD class="titleField">Email</TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblEmail" runat="server"></asp:label></TD>
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="titleField">Telephone / Fax</TD>
									<TD>:</TD>
									<TD>
										<asp:label id="lblPhone" runat="server"></asp:label></TD>
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
							</asp:panel></TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgPengajuanFaktur" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
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
										<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Check">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<INPUT id="chkAllItems" onclick="CheckAll('chkSelect', document.forms[0].chkAllItems.checked)"
											type="checkbox">
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Rangka">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblNomorRangka runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChassisNumber" )  %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterNomorRangka" runat="server" Width="120px" BackColor="White" size="2"
											OnChange="RebindVehicleKind(this);"></asp:TextBox>
										<asp:Button Runat="server" ID="btnVehicleKindF" Text="" CommandName="RebindVehicleKind" style="display:none;"></asp:Button>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jenis ">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblVehicleKind"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:dropdownlist Runat="server" ID="ddlVehicleKindF" Width="120" OnChange="BindVehicleModel(this)"></asp:dropdownlist>
										<asp:Button Runat="server" ID="btnVehicleModelF" Text="" CommandName="RebindVehicleModel" style="display:none;"></asp:Button>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblVehicleModel"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:dropdownlist Runat="server" ID="ddlVehicleModelF" Width="120"></asp:dropdownlist>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model Tipe Warna">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialDescription") %>' >
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
										<asp:Label id=lblTanggalFaktur runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.FakturDate"),"dd/MM/yyy") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<cc1:inticalendar id="icMaxDate" runat="server" TextBoxWidth="60"></cc1:inticalendar>
									</FooterTemplate>
									<EditItemTemplate>
										<cc1:inticalendar id="icEditMaxDate" runat="server" TextBoxWidth="60"></cc1:inticalendar>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Rangka Pengganti">
									<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNoRangkaPengganti" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterNoRangkaPengganti" runat="server" Width="120px" size="2"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditNoRangkaPengganti" runat="server" Width="120px" size="2"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnDelete" Runat="server" text="Hapus" CommandName="delete" CausesValidation="False">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										<asp:LinkButton id="lbtnEdit" Visible="False" Runat="server" text="Edit" CommandName="edit" CausesValidation="False">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add" CausesValidation="False">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" text="Simpan" CommandName="update" CausesValidation="True">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" text="Batal" CommandName="cancel" CausesValidation="True">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<asp:label id="lblError" runat="server" ForeColor="Red"></asp:label>
					</TD>
				</TR>
				<TR>
					<TD><asp:button id="btnSave" runat="server" Width="88px" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnUpdateProfil" runat="server" Width="88px" Text="Update Profil" Enabled="False"></asp:button></TD>
				</TR>
			</TABLE>
			<INPUT id="hdnIsMCP" type="hidden" runat="server" name="hdnIsMCP"> <INPUT id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
			<INPUT id="hdnVerifyMCP" type="hidden" runat="server" name="hdnVerifyMCP">

            <INPUT id="hdnIsLKPP" type="hidden" runat="server" name="hdnIsLKPP"> <INPUT id="hdnLKPPConfirmation" type="hidden" runat="server" name="hdnLKPPConfirmation">
			<INPUT id="hdnVerifyLKPP" type="hidden" runat="server" name="hdnVerifyLKPP">
		</form>
	</body>
</HTML>

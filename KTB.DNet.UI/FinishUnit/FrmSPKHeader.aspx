<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPKHeader.aspx.vb" Inherits="FrmSPKHeader" smartNavigation="False" MaintainScrollPositionOnPostback="true"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SPK Awal</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    var indexRow;
		    function BackToPrev() {
		        var url = document.getElementById("txtUrlToBack").value;
		        window.location = url;
		    }
		    function ShowSalesmanSelection() {
		        showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=0', '', 500, 760, SalemanSelection);
		    }
		    function SalemanSelection(selectedSales) {
		        var temp = selectedSales.split(";")
		        var txtSalesman = document.getElementById('txtSalesmanCode');
		        var txtSalesNama = document.getElementById('lblNamaSalesman');
		        var txtSalesLevel = document.getElementById('lblLevelSalesman');
		        var txtSalesJabatan = document.getElementById('lblJabatan');
		        txtSalesman.value = temp[0];
		        txtSalesNama.innerHTML = temp[1];
		        txtSalesLevel.innerHTML = temp[4];
		        txtSalesJabatan.innerHTML = temp[3];
		    }


		    function GetCurrentInputIndex() {
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var currentRow;
		        var index = 0;
		        var inputs;
		        var indexInput;
		        var i;

		        for (index = 0; index < dtgPesananKendaraan.rows.length; index++) {
		            inputs = dtgPesananKendaraan.rows[index].getElementsByTagName("INPUT");
		            if (inputs != null && inputs.length > 0) {
		                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
		                    if (inputs[indexInput].type != "hidden")
		                        return index;
		                }
		            }
		        }
		        return -1;
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

		    function CategoryChanged(ddl) {
		        var indek = GetIndex(ddl);
		        var val = ddl.options[ddl.selectedIndex].value;
		        var text = ddl.options[ddl.selectedIndex].text;

		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var KodeTipe = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
		        KodeTipe.value = '';
		        var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[1];
		        KodeWarna.value = '';
		        var KodeBody = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[2];
		        KodeBody.value = '';
		        var lblBody = document.getElementById("dtgPesananKendaraan__ctl" + (indek + 1) + "_lblFooterKodeBody");
		        if (!lblBody)
		            lblBody = document.getElementById("dtgPesananKendaraan__ctl" + (indek + 1) + "_lblEditKodeBody");
		        if (val == "1") {
		            KodeBody.style.visibility = 'hidden';
		            lblBody.style.visibility = 'hidden';
		        }
		        else {
		            KodeBody.style.visibility = 'visible';
		            lblBody.style.visibility = 'visible';
		        }

		    }

		    function ShowPPEventDealerSelection() {
		        showPopUp('../PopUp/PopUpBabitEventProposalSelectionOne.aspx', '', 500, 760, EventDealerSelection);
		    }

		    function EventDealerSelection(selectedEvent) {
		        var data = selectedEvent.split(";");

		        var txtEventTypeID = document.getElementById("txtEventTypeID");
		        var txtCampaignName = document.getElementById("txtCampaignName");
		        txtEventTypeID.value = data[0];
		        txtCampaignName.value = data[1];

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            txtCampaignName.focus();
		            txtCampaignName.blur();
		        }
		        else {
		            txtCampaignName.onchange();
		    }
		    }

		    function ShowPPKodeModelSelection(lblId) {
		        var indek = GetIndex(lblId);
		        var ddlCategory = document.getElementById("dtgPesananKendaraan__ctl" + (indek + 1) + "_ddlFooterKategori");
		        if (!ddlCategory)
		            ddlCategory = document.getElementById("dtgPesananKendaraan__ctl" + (indek + 1) + "_ddlEditKategori");
		        var val = ddlCategory.options[ddlCategory.selectedIndex].value;
		        if (val == 0) {
		            alert('Pilih kategori kendaraan');
		        }
		        else {
		            //showPopUp('../General/FrmModelSelection.aspx?cat=' + val, '', 400, 400, KodeTipe)
		            //showPopUp('../PopUp/PopUpVechileType.aspx?CategoryID=0&IsActive=A', '', 500, 760, VechileTypeSelection);
		            showPopUp('../PopUp/PopUpVechileType.aspx?CategoryID='+val+'&IsActive=A&Filtered=1', '', 500, 760, VechileTypeSelection);
		        }
		    }

		    function VechileTypeSelection(SelectedVechileType) {
		        var tempParam = SelectedVechileType.split(';');
		        //var txtVechileTypeCode = document.getElementById("txtVechileTypeCode");
		        var selectedType;
		        if (navigator.appName == "Microsoft Internet Explorer") {
		            selectedType = replace(tempParam[0], ' ', '');
		        }
		        else {
		            selectedType = replace(tempParam[0], ' ', '');
		        }
		        var indek = indexRow;
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var KodeTipe = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
		        KodeTipe.value = selectedType
		        var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[1];
		        KodeWarna.value = '';
		        var KodeBody = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[2];
		        KodeBody.value = '';
		    }

		    function KodeTipe(selectedType) {
		        var indek = indexRow;
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var KodeTipe = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
		        KodeTipe.value = selectedType
		        var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[1];
		        KodeWarna.value = '';
		        var KodeBody = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[2];
		        KodeBody.value = '';
		    }

		    function ShowPPKodeWarnaSelection(lblId) {
		        var indek = GetIndex(lblId);
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var KodeTipe = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
		        showPopUp('../General/FrmKodeWarna.aspx?type=' + KodeTipe.value + '&pktype=0', '', 400, 400, KodeWarna)
		    }

		    function KodeWarna(selectedColor) {
		        var indek = indexRow;
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[1];
		        var tempParam = selectedColor.split(';');
		        var hiddenField = document.getElementById("hideKodeWarna")
		        if (navigator.appName == "Microsoft Internet Explorer") {
		            KodeWarna.innerText = tempParam[0];
		            hiddenField.innerText = tempParam[1];
		        }
		        else {
		            KodeWarna.value = tempParam[0];
		            hiddenField.value = tempParam[1];
		        }
		    }

		    function ShowPPKodeBodySelection(lblId) {
		        var indek = GetIndex(lblId);
		        var ddlCategory = document.getElementById("dtgPesananKendaraan__ctl" + (indek + 1) + "_ddlFooterKategori");
		        if (!ddlCategory)
		            ddlCategory = document.getElementById("dtgPesananKendaraan__ctl" + (indek + 1) + "_ddlEditKategori");
		        var val = ddlCategory.options[ddlCategory.selectedIndex].value;
		        showPopUp('../PopUp/PopUpBentukBody.aspx?cat=' + val, '', 400, 400, KodeBody)
		    }

		    function KodeBody(selectedBody) {
		        var indek = indexRow;
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var KodeBody = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[2];
		        var tempParam = selectedBody.split(';');
		        var hiddenField = document.getElementById("hideKodeBody")
		        if (navigator.appName == "Microsoft Internet Explorer") {
		            KodeBody.innerText = tempParam[1];
		            hiddenField.innerText = tempParam[0];
		        }
		        else {
		            KodeBody.value = tempParam[1];
		            hiddenField.value = tempParam[0];
		        }
		    }

		    function ShowPPGridEventDealerSelection(lblId, isEdit) {
		        var indek = GetIndex(lblId);
		        if (isEdit == 1)
		        {
		            showPopUp('../PopUp/PopUpBabitEventProposalSelectionOne.aspx', '', 500, 760, EventDealerSelectionGridEdit);
		        } else
		        {
		            showPopUp('../PopUp/PopUpBabitEventProposalSelectionOne.aspx', '', 500, 760, EventDealerSelectionGridNew);
		        }
            }

		    function EventDealerSelectionGridNew(selectedEvent) {
                var indek = indexRow;
                var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
                var txtEventTypeID = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[5];
                var txtCampaignName = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[6];
                var data = selectedEvent.split(';');
                txtEventTypeID.value = data[0];
                txtCampaignName.value = data[1];

                if (navigator.appName == "Microsoft Internet Explorer") {
                    txtCampaignName.focus();
                    txtCampaignName.blur();
                }
                else {
                    txtCampaignName.onchange();
                }
            }

		    function EventDealerSelectionGridEdit(selectedEvent) {
		        var indek = indexRow;
		        var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
		        var txtEventTypeID = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[6];
		        var txtCampaignName = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[7];
		        var data = selectedEvent.split(';');
		        txtEventTypeID.value = data[0];
		        txtCampaignName.value = data[1];

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            txtCampaignName.focus();
		            txtCampaignName.blur();
		        }
		        else {
		            txtCampaignName.onchange();
		        }
		    }

		    function changesCampaign(obj)
		    {
		        __doPostBack(obj);
		    }

		    function ShowConfirm(msg) {
		        if (msg !== "") {
		            if (confirm(msg)) {
		                return true;
		            }
		            else {
		                return false;
		            }
		        }

		        return false;
		    }
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MARKETING - SPK Awal</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px">Kode Dealer</TD>
								<TD style="WIDTH: 1%">:</TD>
								<TD style="width: 25%"><asp:label id="lblDealer" runat="server"></asp:label></TD>
								<TD class="titleField" style="width: 24%">Nomor SPK Dealer</TD>
								<TD style="width: 1%">:</TD>
								<TD style="width: 25%"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNomorPesanan" onblur="alphaNumericPlusBlur(txtNomorPesanan)"
										runat="server" MaxLength="20" Width="120px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomorPesanan"
										ErrorMessage="Nomor SPK Dealer tidak boleh kosong">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px">Nomor Reg. SPK</TD>
								<TD>:</TD>
								<TD><asp:label id="lblNoSPK" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblSalesman" runat="server" Font-Bold="True">Salesman</asp:label></TD>
								<TD>:</TD>
								<TD class="titleField"><asp:textbox id="txtSalesmanCode" runat="server" Width="120px"></asp:textbox><asp:label id="lblShowSalesman" runat="server" width="16px"> <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px"><asp:label id="Label4" runat="server" Font-Bold="True">Status SPK</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="txtKondisiPesanan" runat="server" Font-Bold="True">Nama</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblNamaSalesman" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px"><asp:label id="Label1" runat="server" Font-Bold="True" >Tanggal Buka SPK</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblSPKOpenDate" runat="server"></asp:label>									
								</TD>
								<TD class="titleField"><asp:label id="Label9" runat="server"> Level</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblLevelSalesman" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px"><asp:label id="Label13" runat="server" Font-Bold="True">Total Unit</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblTotalUnit" runat="server"></asp:label>
								</TD>
								<TD class="titleField"><asp:label id="Label12" runat="server"> Jabatan</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblJabatan" runat="server"></asp:label></TD>
							</TR>

							<TR valign="top">
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px">
                                    <asp:label id="Label16" runat="server" Font-Bold="True">Dibuat oleh</asp:label>
								</TD>
								<TD></TD>
								<TD>
                                    <asp:label id="lblDibuatOleh" runat="server"></asp:label>
                                    <asp:label id="lblTotalHarga" runat="server" Visible="false"></asp:label>
								</TD>
								<TD class="titleField"><asp:label id="Label5" runat="server">Dealer Babit & Event</asp:label></TD>
								<TD>:</TD>
								<TD>
                                    <asp:DropDownList ID="ddlBabitEventType" AutoPostBack="true" runat="server" ></asp:DropDownList>
                                    <asp:TextBox ID="txtCampaignName" runat="server" Width="120px" MaxLength="40" 
									    onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" 
									    onblur="omitSomeCharacterExcludeSingleQuote('txtCampaignName','<>?*%$;');changesCampaign('txtCampaignName');" 
									    onchange="this.value=this.value.toUpperCase();">
								    </asp:TextBox>
                                    <asp:Label ID="lblPopUpEvent" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>
                                    <asp:TextBox ID="txtEventTypeID" runat="server" style="display:none"></asp:TextBox>
                                    <br />
								    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
										    ControlToValidate="txtCampaignName"
										    ValidationExpression="^[\s\S]{0,40}$"
										    ErrorMessage="Panjang maksimal 100 karakter"></asp:RegularExpressionValidator>
                                </TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px"><asp:label id="Label2" runat="server"> Tanggal SPK Dealer</asp:label></TD>
								<TD>:</TD>
								<TD>
                                    <cc1:inticalendar id="icDealerSPKDate" runat="server" TextBoxWidth="60"></cc1:inticalendar>
								</TD>
                                <td colspan="3" rowspan="3" style="border-width: 1px; border-style: solid; padding: 10px; width:40%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3" class="titlePanel"><b>ADDITIONAL DATA FOR DMS</b></td>
                                        </tr>
                                        <tr>
                                            <TD class="titleField" style="WIDTH: 24%; height: 24px" width="149">Nomor SPK Referensi</TD>
								            <TD style="width: 1%">:</TD>
								            <TD style="WIDTH: 25%"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtSPKReferenceNumber" onblur="alphaNumericPlusBlur(txtSPKReferenceNumber)"
										            runat="server" MaxLength="20" Width="120px"></asp:textbox></TD>
                                        </tr>
                                        <tr>
                                            <td colspan="3">Silakan isi dengan Nomor Reg. SPK yang sebelumnya sebagai referensi, jika akan melakukan <b>Revisi Faktur</b>
                                                <br />Contoh: 1911000126
                                            </td>
                                        </tr>
                                    </table>
                                </td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px" ><asp:label id="Label15" runat="server" Font-Bold="True">Dokumen SPK</asp:label></TD>
								<TD>:</TD>
								<TD>
                                     <asp:label id="lblEvidenceFile" runat="server" > </asp:label>
                                    <asp:ImageButton ID="btnDeleteFile" runat="server" ImageUrl="../images/trash.gif" border="0"> </asp:imageButton>
                                    <INPUT onkeypress="return false;"  id="DataFile" style="HEIGHT: 20px" type="file" size="29" name="File1" runat="server">
								</TD>
								
							</TR>
                            <TR>
								<TD class="titleField" style="WIDTH: 24%; HEIGHT: 24px"></TD>
								<TD></TD>
								<TD>
                                    <asp:label id="Label14" runat="server" Font-Italic="true" ForeColor="Red">* File maksimal 1 MB (jpg, jpeg, bmp, png, pdf)</asp:label>
								</TD>
							</TR>
                            <tr>
                                <TD class="titleField">
                                    <asp:label id="Label3" runat="server" Font-Bold="True" Visible="false">Rencana Pengajuan Faktur</asp:label>
								</TD>
								<TD></TD>
								<TD>
                                    <table id="tblInvoice" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlInvoiceMonth" runat="server" Width="80px" Visible="false"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlInvoiceYear" runat="server" Width="50px" Visible="false"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="Label6" runat="server" Font-Bold="True"  Visible="false">Kategori Kendaraan</asp:label></TD>
								<TD></TD>
								<TD>
                                    <asp:dropdownlist id="ddlKategori" runat="server" Width="130px" Visible="False" AutoPostBack="True"></asp:dropdownlist>
								</TD>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <TD class="titleField" valign="top">                                    
                                    <asp:label id="Label11" runat="server" Font-Bold="True" Visible="false">Rencana Pengiriman Kendaraan</asp:label>
								</TD>
								<TD></TD>
								<TD>
                                    <table id="tblDelivery" cellSpacing="0" cellPadding="0" border="0" visible="false">
										<tr>
											<td><asp:dropdownlist id="ddlDeliveryMonth" runat="server" Width="80px" Visible="false"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlDeliveryYear" runat="server" Width="50px"  Visible="false"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
                            </tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<table style="WIDTH:100%">
							<tr>
								<td>
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100px">
										<asp:datagrid id="dtgPesananKendaraan" runat="server" Width="100%" AutoGenerateColumns="False"
											OnUpdateCommand="dtgPesananKendaraan_Update" OnCancelCommand="dtgPesananKendaraan_Cancel"
											OnEditCommand="dtgPesananKendaraan_Edit" OnItemCommand="dtgPesananKendaraan_ItemCommand" OnItemDataBound="dtgPesananKendaraan_ItemDataBound"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="1" ShowFooter="True"
											BackColor="#E0E0E0">
											<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="30px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Font-Size="Small"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="Model / Tipe / Warna">
													<HeaderStyle Width="220px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle VerticalAlign="Top" Width="220px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Kategori">
													<HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKategori" runat="server" NAME="lblKategori" Text='<%# DataBinder.Eval(Container.DataItem, "VechileColor.VechileType.Category.CategoryCode" )  %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:DropDownList Runat="server" ID="ddlEditKategori" Width="50px" onchange="CategoryChanged(this);"></asp:DropDownList>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:DropDownList Runat="server" ID="ddlFooterKategori" Width="50px" onchange="CategoryChanged(this);"></asp:DropDownList>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Tipe">
													<HeaderStyle Width="90px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewKodeModel runat="server" NAME="lblViewKodeModel" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleTypeCode" )  %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterKodeModel" runat="server" size="2" BackColor="White" MaxLength="4"></asp:TextBox>
														<asp:Label id="lblFooterKodeModel" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtEditKodeModel runat="server" size="2" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleTypeCode" ) %>' MaxLength="4">
														</asp:TextBox>
														<asp:Label id="lblEditKodeModel" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Warna">
													<HeaderStyle Width="90px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewKodeWarna runat="server" NAME="lblViewKodeWarna" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleColorCode" ) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterKodeWarna" runat="server" size="2" BackColor="White"></asp:TextBox>
														<asp:Label id="lblFooterKodeWarna" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtEditKodeWarna runat="server" size="2" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleColorCode" ) %>'>
														</asp:TextBox>
														<asp:Label id="lblEditKodeWarna" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Model Body">
													<HeaderStyle Width="90px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblViewModelBody" runat="server" NAME="lblViewModelBody" Text='<%# DataBinder.Eval(Container.DataItem, "ProfileDetail.Code" ) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterKodeBody" runat="server" size="2" BackColor="White"></asp:TextBox>
														<asp:Label id="lblFooterKodeBody" runat="server" NAME="LblFooterKodeBody">
															<img src="../images/popup.gif" name="imgFooterlbnBody" id="imgFooterlbnBody" style="cursor:hand"
																border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditKodeBody" runat="server" size="2" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "ProfileDetail.Code" ) %>'>
														</asp:TextBox>
														<asp:Label id="lblEditKodeBody" runat="server" NAME="LblEditKodeBody">
															<img src="../images/popup.gif" name="imgEditlbnBody" id="imgEditlbnBody" style="cursor:hand"
																border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tambahan">
													<HeaderStyle Width="100px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTambahan" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:DropDownList Runat="server" ID="ddlEditTambahan" Width="100px"></asp:DropDownList>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:DropDownList Runat="server" ID="ddlFooterTambahan" Width="100px"></asp:DropDownList>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Remarks" HeaderText="Remarks">
													<HeaderStyle Width="100px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblRemarks" runat="server" NAME="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterRemarks" runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Unit">
													<HeaderStyle Width="60px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblViewUnitPermintaanDealer runat="server" NAME="lblViewUnitPermintaanDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity", "{0:#,###}" ) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterUnit" onkeypress="return numericOnlyUniv(event)" runat="server" size="2"
															CssClass="textRight"></asp:TextBox>
														<asp:RangeValidator id="RangeValidator2" runat="server" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
															ControlToValidate="txtFooterUnit" MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtEditUnit onkeypress="return numericOnlyUniv(event)" runat="server" size="2" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>' CssClass="textRight">
														</asp:TextBox>
                                                        <asp:HiddenField ID="hdnEditUnit" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>' />
														<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
															ControlToValidate="txtEditUnit" MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Harga Per Unit (OTR / Deal Price)(Rp)" visible="false">
													<HeaderStyle Width="100px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblViewHarga" runat="server" NAME="lblViewHarga" Text='<%# DataBinder.Eval(Container.DataItem, "Amount", "{0:#,###,###,###}" ) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterHarga" onkeypress="return numericOnlyUniv(event)" runat="server" Width="80px"
															size="2" CssClass="textRight"></asp:TextBox>
														<asp:RangeValidator id="Rangevalidator3" runat="server" ErrorMessage="Harga Per Unit harus lebih besar dari 0"
															ControlToValidate="txtFooterHarga" MaximumValue="10000000000" MinimumValue="1" Type="Double">*</asp:RangeValidator>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditHarga" onkeypress="return numericOnlyUniv(event)" runat="server" size="2" Width="80px" CssClass="textRight" Text='<%# DataBinder.Eval(Container.DataItem, "Amount", "{0:##########}") %>'>
														</asp:TextBox>
														<asp:RangeValidator id="Rangevalidator4" runat="server" ErrorMessage="Harga Per Unit harus lebih besar dari 0"
															ControlToValidate="txtEditHarga" MaximumValue="10000000000" MinimumValue="1" Type="Double">*</asp:RangeValidator>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn ReadOnly="True" HeaderText="Total Harga (Rp)" DataFormatString="{0:#,###,###,###}" visible="false">
													<HeaderStyle Width="120px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="120px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="RejectedReason" HeaderText="Alasan Batal">
													<HeaderStyle Width="120px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="120px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlRejectedReason" runat="server" Width="120px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
													<%--<ItemTemplate>
														<asp:TextBox id="txtRejectedReason" runat="server">
														</asp:TextBox>
													</ItemTemplate>
                                                    <EditItemTemplate>
														<asp:Label id="lblRejectedReason" runat="server">
														</asp:Label>
													</EditItemTemplate>--%>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Campaign">
													<HeaderStyle Width="120px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="120px"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblCampaignName" runat="server" NAME="lblCampaignName">
														</asp:Label>
													</ItemTemplate>
													<FooterStyle></FooterStyle>
													<FooterTemplate>
                                                        <asp:TextBox ID="txtFEventTypeID" runat="server" style="display:none"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlFBabitEventType" OnSelectedIndexChanged="ddlFBabitEventType_SelectedIndexChanged" 
                                                            AutoPostBack="true" runat="server" ></asp:DropDownList><br />
                                                        <asp:TextBox ID="txtFCampaignName" runat="server" Width="80px" MaxLength="40"
									                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
									                        onblur="omitSomeCharacterExcludeSingleQuote(this.id,'<>?*%$;');" 
									                        onchange="this.value=this.value.toUpperCase();">
								                        </asp:TextBox>
                                                        <asp:Label ID="lblFPopUpEvent" runat="server">
                                                                <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                                        </asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
                                                        <asp:TextBox ID="txtEEventTypeID" runat="server" style="display:none"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlEBabitEventType" OnSelectedIndexChanged="ddlFBabitEventType_SelectedIndexChanged" 
                                                            AutoPostBack="true" runat="server" ></asp:DropDownList><br />
                                                        <asp:TextBox ID="txtECampaignName" runat="server" Width="80px" MaxLength="40" 
									                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
									                        onblur="omitSomeCharacterExcludeSingleQuote(this.id,'<>?*%$;');" 
									                        onchange="this.value=this.value.toUpperCase();">
								                        </asp:TextBox>
                                                        <asp:Label ID="lblEPopUpEvent" runat="server" Width="16px">
                                                                <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                                        </asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
													CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; alt=&quot;Batal Ubah&quot;&gt;"
													EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												</asp:EditCommandColumn>
												<asp:TemplateColumn Visible="false">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete" Visible="false">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn >
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnBatal" runat="server" CommandName="Batal">
															<img src="../images/in-aktif.gif" border="0" alt="Batal Pengajuan"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>


                                                <asp:TemplateColumn HeaderText="Konsumen Faktur" Visible="false">
													<HeaderStyle Width="40px" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                                    	<ItemTemplate>
														<asp:LinkButton id="lbtnAddFaktur" runat="server" CommandName="AddFaktur">
															<img src="../images/add.gif" border="0" alt="Tambah" id="imgConsumentFaktur" runat="server"/></asp:LinkButton>
													</ItemTemplate>
													 
												</asp:TemplateColumn>

											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
										<INPUT id="hideKategori" type="hidden" name="HideKategori" runat="server"> <INPUT id="hideKodeTipe" type="hidden" name="HideKodeTipe" runat="server">
										<INPUT id="hideKodeWarna" type="hidden" name="HideKodeWarna" runat="server"> <INPUT id="hideKodeBody" type="hidden" name="HideKodeBody" runat="server">
										<INPUT id="hideNamaWarna" type="hidden" name="HideNamaWarna" runat="server">
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="lblError" runat="server" Width="624px" EnableViewState="False" ForeColor="Red"></asp:label><BR>
									<asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD height="40">
						<asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>
						<asp:button id="btnKonsumen" runat="server" Text="Konsumen SPK"></asp:button>
                        <asp:button id="btnJadiKonsumen" runat="server" Text="Jadi Konsumen" Visible="false"></asp:button>
						<input onclick="BackToPrev();" type="button" value="Kembali">
						<asp:button id="btnProfile" runat="server" Text="Profile SPK" Visible = "False"></asp:button>
						<asp:textbox id="txtUrlToBack" style="VISIBILITY: hidden" ReadOnly="True" Text="" Runat="server"></asp:textbox>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
	</body>
</HTML>

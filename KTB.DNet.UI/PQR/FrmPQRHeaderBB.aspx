<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRHeaderBB.aspx.vb" Inherits="FrmPQRHeaderBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>FrmPQRHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">


        function ShowPPDealerBranchSelection() {
            debugger;
            var lblDealer = document.getElementById("lblDealerVal");
            var dealerCode = lblDealer.innerText.split("-")[0].replace(/\s/g, '').trim();
            showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealer) {
            if (selectedDealer.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                var txtBranchName = document.getElementById("txtBranchName");
                txtDealerSelection.value = selectedDealer.split(";")[0];
                txtBranchName.value = selectedDealer.split(";")[2];
            }
            else {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer;
            }
        }

        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var txtDealerName = document.getElementById("lblDealerVal");
            txtDealerSelection.value = tempParam[0];
            txtDealerName.innerHTML = tempParam[1];
        }
        function GetCurrentInputIndex(GridName) {
            var dtgDamageCode = document.getElementById(GridName);
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dtgDamageCode.rows.length; index++) {
                inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function GetSelectedDamageCode(selectedCode) {
            var indek = GetCurrentInputIndex("dgKerusakan");
            var dtgDamageCode = document.getElementById("dgKerusakan");
            var tempParam = selectedCode.split(';');
            var KodeDamage = dtgDamageCode.rows[indek].getElementsByTagName("INPUT")[0];
            var DescDamage = dtgDamageCode.rows[indek].getElementsByTagName("SPAN")[1];

				KodeDamage.value = tempParam[0];				
				DescDamage.innerHTML = tempParam[1];				

			}
			
			function GetSelectedPartsCode(selectedCode)
			{
				var indek = GetCurrentInputIndex("dgParts");
				var dtgPartsCode = document.getElementById("dgParts");
				var tempParam = selectedCode.split(';');
				var KodeParts = dtgPartsCode.rows[indek].getElementsByTagName("INPUT")[0];
				var DescParts = dtgPartsCode.rows[indek].getElementsByTagName("SPAN")[1];

				KodeParts.value = tempParam[0];				
				DescParts.innerHTML = tempParam[1];				

			}

        function ShowPopUp() {
        }

        function BackButton() {
            //var ret = (parseInt(document.getElementById("hid_History").value) + 1)* (-1)
            //document.getElementById("btnBack").disabled=true
            //history.go(ret)
            document.location.href = "../SparePart/FrmPQRList.aspx";
        }
        function focusSave() {
            document.getElementById("btnSimpan").focus();
        }

        function setLastPos(lPosID) {
            var hiddenField = document.getElementById("hfLastPostId");
            hiddenField.value = lPosID;
        }
    </script>
</head>
<body>

    <form id="Form1" method="post" runat="server">
        <input id="hfLastPostId" style="width: 1px; height: 1px" type="hidden" size="1">
        <table id="TableHeader" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
            <tr>
                <td class="titlePage">PRODUCT QUALITY REPORT "CONFIDENTIAL"</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="TableForm" cellspacing="1" cellpadding="4" width="764" border="0" runat="server">
            <tr valign="top">
                <td width="50%">
                    <table cellspacing="1" cellpadding="2" width="360" border="0">
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblDealer" runat="server">Dealer : </asp:Label></td>
                            <td>:</td>
                            <td width="34%">
                                <asp:Label ID="lblDealerVal" runat="server" Font-Size="8"></asp:Label>
                            </td>
                        </tr>
                       <tr>
                            <td class="titleField" style="width: 126px">Kode Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblPopUpDealerBranch" onclick="ShowPPDealerBranchSelection()" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>

                                <asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>

                            </td>
                        </tr>
                         <tr>
                            <td class="titleField" style="width: 126px">Nama Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:textbox id="txtBranchName" Width="150px" Runat="server" BackColor="LightGray"></asp:textbox>

                            </td>

                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">PQR Type</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlPqrType" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">No PQR</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblPQRNoVal" runat="server">Value Of PQR Number</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblRefPQRNo" runat="server">No PQR Ref</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtRefPQRNo" runat="server" Width="160px"></asp:TextBox><asp:Label ID="lblRefPQRNoVal" runat="server" Font-Size="8" Width="196px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglPembuatan" runat="server">Tgl Pembuatan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglPembuatanVal" runat="server">Value Of Tgl Pembuatan</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblWONumber" runat="server">Nomor WO</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtWONumber"
                                    runat="server" Width="109px"></asp:TextBox><asp:Label ID="lblWONumberVal" runat="server"></asp:Label>
                                  <asp:LinkButton ID="lnkBtnCheckWONumber" runat="server" CausesValidation="False" ToolTip="Validate WO Number">
										<img style="cursor:hand" alt="Check WO Number" src="../images/tanya.gif" border="0"></asp:LinkButton>
                            </td>
                                                   
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNoChasis" runat="server">No Rangka</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoChasis" onblur="omitSomeCharacter('txtNoChasis','<>?*%$;')"
                                    runat="server" Width="160px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvChassisNumber" runat="server" ErrorMessage="No Rangka harus diisi" ControlToValidate="txtNoChasis">*</asp:RequiredFieldValidator>
                                <asp:LinkButton ID="lnkbtnCheckChassis" runat="server" CausesValidation="False" ToolTip="Validate Chassis">
										<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0">
									</asp:linkbutton><asp:linkbutton id="lnkbtnPopUpInfoKendaraan" Runat="server" CausesValidation="False" ToolTip="View Info Kendaraan">
										<img style="cursor:hand" alt="View Info Kendaraan" src="../images/popup.gif" border="0">
									</asp:linkbutton>
                                    <br /><asp:label id="lblNoChasisVal" Runat="server" Font-Size="8"></asp:label>
								</td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblNoMesin" Runat="server">No Mesin</asp:label></td>
								<td style="HEIGHT: 16px">:</td>
								<td style="HEIGHT: 16px"><asp:label id="lblNoMesinVal" Runat="server" Width="213">Value Of No Mesin</asp:label></td>
							</tr>
							<TR>
								<TD class="titleField" style="WIDTH: 126px">Model</TD>
								<TD>:</TD>
								<TD><asp:label id="lbModel" Runat="server" Width="213">Value Of Model</asp:label></TD>
							</TR>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblTypeColor" Runat="server">Tipe / Warna</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTypeColorVal" Runat="server" Width="213">Value Of Type / Color</asp:label></td>
							</tr>
							<tr style="display: none">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblNama" Runat="server">Nama Pemilik</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblNamaVal" Runat="server" Width="213">Value Of Nama</asp:label></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblThnProduksi" Runat="server">Tahun Produksi</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblThnProduksiVal" Runat="server" Width="213">Value Of Tahun Produksi/Perakitan</asp:label></td>
							</tr>
							<tr style="display: none">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblTglDelivery" Runat="server">Tanggal Delivery</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTglDeliveryVal" Runat="server" Width="213">Tanggal Delivery</asp:label></td>
							</tr>
							<tr style="display: none"">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblTglFaktur" Runat="server">Tanggal Buka Faktur</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTglFakturVal" Runat="server" Width="213">Tanggal Buka Faktur</asp:label></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblTglKerusakan" Runat="server">Tanggal Kerusakan</asp:label></td>
								<td>:</td>
								<td><cc1:inticalendar id="icTglKerusakan" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglKerusakanVal" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblOdometer" Runat="server">Odometer</asp:label></td>
								<td>:</td>
								<td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtOdometer" onkeyup="pic(this,this.value,'9999999999','N')"
										Runat="server" Width="109"></asp:textbox><asp:requiredfieldvalidator id="rfvOdometer" runat="server" ErrorMessage="Odometer harus diisi" ControlToValidate="txtOdometer">*</asp:requiredfieldvalidator><asp:label id="lblOdometerVal" Runat="server"></asp:label>&nbsp;<span style="FONT-SIZE: 8pt">Km</span>
								</td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblKecepatan" Runat="server">Kecepatan</asp:label></td>
								<td>:</td>
								<td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtKecepatan" onkeyup="pic(this,this.value,'9999999999','N')"
										Runat="server" Width="109px"></asp:textbox><asp:requiredfieldvalidator id="rfvKecepatan" runat="server" ErrorMessage="Kecepatan harus diisi" ControlToValidate="txtKecepatan">*</asp:requiredfieldvalidator><asp:label id="lblKecepatanVal" Runat="server"></asp:label>&nbsp;<SPAN style="FONT-SIZE: 8pt">Km 
										/ Jam</SPAN>
								</td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblSubject" Runat="server">Subject</asp:label></td>
								<td>:</td>
								<td><asp:textbox id="txtSubject" tabIndex="0" Runat="server" Width="209px"></asp:textbox><asp:requiredfieldvalidator id="rfvSubject" runat="server" ErrorMessage="Subject harus diisi" ControlToValidate="txtSubject">*</asp:requiredfieldvalidator><asp:label id="lblSubjectVal" Runat="server"></asp:label></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblGejala" Runat="server">Gejala</asp:label></td>
								<td>:</td>
								<td><asp:textbox id="txtGejala" tabIndex="0" Runat="server" Width="209" TextMode="MultiLine" Height="60px"></asp:textbox><asp:requiredfieldvalidator id="rfvGejala" runat="server" ErrorMessage="Gejala harus diisi" ControlToValidate="txtGejala">*</asp:requiredfieldvalidator></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblPenyebab" Runat="server">Penyebab</asp:label></td>
								<td>:</td>
								<td><asp:textbox id="txtPenyebab" tabIndex="0" Runat="server" Width="208px" TextMode="MultiLine"
										Height="60px"></asp:textbox><asp:requiredfieldvalidator id="rfvPenyebab" runat="server" ErrorMessage="Penyebab harus diisi" ControlToValidate="txtPenyebab">*</asp:requiredfieldvalidator></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblHasil" Runat="server">Perbaikan</asp:label></td>
								<td>:</td>
								<td><asp:textbox id="txtHasil" tabIndex="0" Runat="server" Width="208px" TextMode="MultiLine" Height="60px"></asp:textbox></td>
							</tr>
							<tr vAlign="top">
								<td class="titleField" style="WIDTH: 126px"><asp:label id="lblCatatan" Runat="server">Catatan</asp:label></td>
								<td>:</td>
								<td><asp:textbox id="txtCatatan" tabIndex="0" Runat="server" Width="209" TextMode="MultiLine" Height="60px"></asp:textbox></td>
							</tr>
						</table>
						<br>
						<font class="titleField">QRS</font>
						<br>
						<div id="div2" style="HEIGHT: 100px; OVERFLOW: auto"><asp:datagrid id="dgQRS" tabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
								CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No Rangka">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblNoRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.ChassisNumber") %>'>
											</asp:label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtNoRangkaFooter" TabIndex="0" runat="server" Width="60px"></asp:TextBox>
											<asp:Label id="lblPopUpNoRangkaFooter" runat="server" Visible="False">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtNoRangkaEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.ChassisNumber") %>' width="60">
											</asp:TextBox>
											<asp:Label id="lblPopUpNoRangkaEdit" tabIndex="0" runat="server" height="10px" Visible="False">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Kerusakan" src="../images/popup.gif"
													border="0"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Kerusakan">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTglKerusakanQRS" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.TglKerusakan"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<cc1:inticalendar id="icTglKerusakanFooter" runat="server" TextBoxWidth="60"></cc1:inticalendar>
										</FooterTemplate>
										<EditItemTemplate>
											<cc1:inticalendar id="icTglKerusakanEdit" runat="server" TextBoxWidth="60" Value='<%# DataBinder.Eval(Container, "DataItem.TglKerusakan") %>'>
											</cc1:inticalendar>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Odometer">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOdometerQRS" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.Odometer"),"#,##0") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtOdometerFooter" runat="server" Width="40px" onkeypress="return NumericOnlyWith(event,'');"
												onkeyup="pic(this,this.value,'9999999999','N')" TabIndex="0"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtOdometerEdit" runat="server" Width="40px" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# DataBinder.Eval(Container, "DataItem.Odometer") %>' TabIndex="0">
											</asp:TextBox>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Catatan">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCatatanQRS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Note") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox MaxLength="100" id="txtCatatanFooter" runat="server" Width="60px" TabIndex="0"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox MaxLength=100 id="txtCatatanEdit" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.Note") %>' TabIndex="0">
											</asp:TextBox>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="Edit" CausesValidation="False" TabIndex="0">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="Delete" CausesValidation="False" TabIndex="0">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton3" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="Linkbutton4" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False"
												TabIndex="0">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="Linkbutton5" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False"
												TabIndex="0">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
					<td vAlign="top" width="50%">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><asp:panel id="Panel1" tabIndex="99" runat="server" Height="100%" BorderWidth="0" BorderStyle="Solid"></asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel2" tabIndex="99" runat="server" Height="100%" BorderWidth="0" BorderStyle="Solid"></asp:panel></td>
							</tr>
						</table>
						<br>
						<font class="titleField">Posisi Kerusakan</font>
						<br>
						<div id="div1" style="HEIGHT: 100px; OVERFLOW: auto"><asp:datagrid id="dgKerusakan" tabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
								CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode ">
										<HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblKodeDamage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.KodePosition") %>'>
											</asp:label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDamageFooter"
												runat="server" Width="84px" TabIndex="0"></asp:TextBox>
											<asp:Label id="lblSearchDamageFooter" runat="server" TabIndex="0">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id=txtKodeDamageEdit runat="server" onblur="omitSomeCharacter('txtKodeDamageEdit','<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.KodePosition") %>' width="70">
											</asp:TextBox>
											<asp:Label id="lblSearchDamageEdit" tabIndex="0" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Kerusakan" src="../images/popup.gif"
													border="0"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Deskripsi">
										<HeaderStyle Width="300px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDescDamage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.Description") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblDescDamageFooter" runat="server"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id="lblDescDamageEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.Description") %>'>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkbtnEditDamage" runat="server" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lnkbtnDeleteDamage" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lnkbtnAddDamage" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lnkbtnSaveDamage" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lnkbtnCancelDamage" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<font class="titleField">Posisi Parts</font>
						<br>
						<div id="div2" style="HEIGHT: 100px; OVERFLOW: auto"><asp:datagrid id="dgParts" tabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
								CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode ">
										<HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblKodeParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
											</asp:label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodePartsFooter"
												runat="server" Width="84px" TabIndex="0"></asp:TextBox>
											<asp:Label id="lblSearchPartsFooter" runat="server" TabIndex="0">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodePartsEdit" runat="server" onblur="omitSomeCharacter('txtKodePartsEdit','<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' width="70">
											</asp:TextBox>
											<asp:Label id="lblSearchPartsEdit" tabIndex="0" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Kerusakan" src="../images/popup.gif"
													border="0"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Deskripsi">
										<HeaderStyle Width="300px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDescParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblDescPartsFooter" runat="server"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id="lblDescPartsEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkbtnEditParts" runat="server" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lnkbtnSaveParts" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lnkbtnCancelParts" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<table cellspacing="0" cellpadding="0" border="0" runat="server" id="tblKodeKerusakan"
							Width="100%">
							<tr>
								<td colspan="4"><STRONG>Kode Kerusakan</STRONG></td>
							</tr>
							<tr>
								<td style="HEIGHT: 2px" Width="10%" align="center"><strong>A</strong></td>
								<td width="5%" style="HEIGHT: 2px">:</td>
								<td style="HEIGHT: 2px" Width="85%">
									<asp:DropDownList Runat="server" ID="ddlKodeWSCA" Width="100%"></asp:DropDownList>
								</td>
								<td style="HEIGHT: 2px"></td>
							</tr>
							<tr>
								<td Width="10%" align="center"><strong>B</strong></td>
								<td width="5%">:</td>
								<td Width="85%">
									<asp:DropDownList Runat="server" ID="ddlKodeWSCB" Width="100%"></asp:DropDownList>
								</td>
								<td></td>
							</tr>
							<tr>
								<td Width="10%" align="center"><strong>C</strong></td>
								<td width="5%">:</td>
								<td Width="85%">
									<asp:DropDownList Runat="server" ID="ddlKodeWSCC" Width="100%"></asp:DropDownList>
								</td>
								<td></td>
							</tr>
						</table>
						<br>
						<STRONG>Lampiran</STRONG>&nbsp;
						<br>
						<div id="div1" style="HEIGHT: 100px; OVERFLOW: auto"><asp:datagrid id="dgFileAttachmentTop" tabIndex="99" runat="server" Width="100%" BorderWidth="1px"
								BorderColor="#CDCDCD" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Literal id="ltrFileAttachmentTopNo" runat="server"></asp:Literal>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="File">
										<HeaderStyle Width="90%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkbtnFileAttachmentTop" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Attachment") %>' >
												<%# DataBinder.Eval(Container, "DataItem.FileName") %>
											</asp:LinkButton>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<INPUT type="file" id="iFileAttachmentTop" runat="server" TabIndex="0">
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkbtnFileAttachmentTopDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lnkbtnFileAttachmentTopAdd" runat="server" CommandName="Add" CausesValidation="False"
												TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<table style="MARGIN: 5% 0px" align="center" border="0">
							<tr>
								<td class="titleField"><asp:label id="lblStatus" Runat="server" Font-Size="8">Status</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblStatusVal" Runat="server" Font-Size="8"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblAppliedBy" Runat="server" Font-Size="8"> Dealer</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblAppliedByVal" Runat="server" Font-Size="8"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblTglJam" Runat="server" Font-Size="8">Tanggal/Jam</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTglJamVal" Runat="server" Font-Size="8"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblProcessBy" Runat="server" Font-Size="8">MKS</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblProcessByVal" Runat="server" Font-Size="8"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblTglJamProcess" Runat="server" Font-Size="8"> Tanggal/Jam</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblTglJamProcessVal" Runat="server" Font-Size="8"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td style="WIDTH: 398px"><font class="titleField">Tambahan Info :</font>
						<asp:literal id="ltrStatusAdditionalInfo" runat="server"></asp:literal><asp:label id="lblLastPostedInfo" runat="server">
							<img src="../images/icon_mail.gif" border="0" runat="server" ID="img">
						</asp:label><asp:linkbutton id="lnkbtnAdditionalInfoPopUp" runat="server" CausesValidation="False">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
						</asp:linkbutton><br>
						<font class="titleField">Penjelasan KTB&nbsp;:</font>
						<br>
						<asp:textbox id="txtSolution" tabIndex="0" runat="server" Width="360px" TextMode="MultiLine"
							Height="130px"></asp:textbox><br>
					</td>
					<td><asp:label class="titleField" id="lblBobot" Runat="server">Bobot : </asp:label><asp:dropdownlist id="ddlBobot" tabIndex="1" Runat="server"></asp:dropdownlist><br>
						<font class="titleField">Lampiran</font>
						<div id="div1" style="HEIGHT: 100px; OVERFLOW: auto"><asp:datagrid id="dgFileAttachmentBottom" tabIndex="99" runat="server" Width="100%" BorderWidth="1px"
								BorderColor="#CDCDCD" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Literal id="ltrFileAttachmentBottomNo" runat="server"></asp:Literal>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="File">
										<HeaderStyle Width="400px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkbtnFileAttachmentBottom" CommandName="Download" runat="server" CommandArgument ='<%# DataBinder.Eval(Container, "DataItem.Attachment") %>' >
												<%# DataBinder.Eval(Container, "DataItem.FileName") %>
											</asp:LinkButton>
										</ItemTemplate>
										<FooterStyle Wrap="False"></FooterStyle>
										<FooterTemplate>
											<INPUT type="file" id="iFileAttachmentBottom" runat="server" NAME="iFileAttachmentBottom">
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkbtnFileAttachmentBottomDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lnkbtnFileAttachmentBottomAdd" runat="server" CommandName="Add" CausesValidation="False">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
                </tr>
				<tr>
					<td align="center" colSpan="2"><asp:button id="btnSimpan" tabIndex="0" Runat="server" Text="Simpan"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnCancelStatusChange" Runat="server"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnStatusChange" Runat="server"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnBatal" Runat="server" CausesValidation="False" Text="Kembali"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnCetak" Runat="server" CausesValidation="False" Text="Cetak"></asp:button><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
		</form>
		</SPAN>
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
	</BODY>
</HTML>

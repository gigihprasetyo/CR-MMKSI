<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntrySparePartPO.aspx.vb" Inherits="FrmEntrySparePartPO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEntrySparePartPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		    function onBlurPartNo(txtPartNo) {
		        var e = document.getElementById("ddlOrderType");
		        var txtPQRNo = document.getElementById("txtPQRNo");
		        var strOrderType = e.options[e.selectedIndex].value;
		        if (strOrderType != '') {
		            if (strOrderType == 'P') {
		                if (txtPQRNo.value == '') {
		                    txtPartNo.value = '';
		                    alert('Harap isi Nomor PQR');
		                }
		            }
		        }
		        else {
		            txtPartNo.value = '';
		            alert('Harap isi Tipe Order.')
		        }
		    }

		    function ShowPQRNoSelection() {
		        showPopUp('../PopUp/PopUpPQRSelectionSingle.aspx', '', 500, 760, PQRNoSelection);
		    }
		    function PQRNoSelection(selectedPQRNo) {
		        var txtPQRNo = document.getElementById("txtPQRNo");
		        txtPQRNo.value = selectedPQRNo;
		        __doPostBack("txtPQRNo", "");
		    }

		    function blurPQRNoText() {
		        __doPostBack("txtPQRNo", "");
		    }

		    function SetKirimDisable() {
		        document.getElementById("btnSubmit").disabled = true;
		    }

		    function GetCurrentInputIndex() {
		        var dgPODetail = document.getElementById("dtgPODetail");
		        var currentRow;
		        var index = 0;
		        var inputs;
		        var indexInput;

		        for (index = 0; index < dgPODetail.rows.length; index++) {
		            inputs = dgPODetail.rows[index].getElementsByTagName("INPUT");

		            if (inputs != null && inputs.length > 0) {
		                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
		                    if (inputs[indexInput].type != "hidden")
		                        return index;
		                }
		            }
		        }

		        return -1;
		    }

		    function BackButton() {
		        document.location.href = "../SparePart/frmSPPOList.aspx";
		    }

		    function ShowPopUpSparePart(intPQRHeaderID) {
		        var e = document.getElementById("ddlOrderType");
		        var txtPQRNo = document.getElementById("txtPQRNo");
		        var strOrderType = e.options[e.selectedIndex].value;
		        if (strOrderType != '') {
		            if (strOrderType == 'P') {
		                if (txtPQRNo.value != '') {
		                    if (intPQRHeaderID > 0) {
		                        showPopUp('../PopUp/PopUpSparePart.aspx?PQRHeaderID=' + intPQRHeaderID, '', 510, 700, SparePart);
		                    }
		                    else
		                        alert('Nomor PQR tidak terdaftar');
		                }
		                else
		                    alert('Harap isi Nomor PQR');
		            }
		            else
		                showPopUp('../PopUp/PopUpSparePart.aspx', '', 510, 700, SparePart);
		        }
		        else {
		            alert('Harap isi Tipe Order.')
		        }
		    }

		    function SparePart(selectedCode) {
		        var tempParam = selectedCode.split(';');
		        var indek = GetCurrentInputIndex();
		        var dgPODetail = document.getElementById("dtgPODetail");
		        var partCode = dgPODetail.rows[indek].getElementsByTagName("INPUT")[0];
		        var partName = dgPODetail.rows[indek].getElementsByTagName("SPAN")[1];
		        var partPrice = dgPODetail.rows[indek].getElementsByTagName("SPAN")[2];
		        var partAmount = dgPODetail.rows[indek].getElementsByTagName("SPAN")[3];
		        var partQTY = dgPODetail.rows[indek].getElementsByTagName("INPUT")[1];

		        partCode.value = tempParam[0];
		        partName.innerHTML = tempParam[1];
		        if (tempParam[2] == "")
		            partPrice.innerHTML = "N.A";
		        else
		            partPrice.innerHTML = parseFloat(tempParam[2]);
		        partAmount.innerHTML = "0";

		        partQTY.value = "";
		        partQTY.focus();
		    }

		    function CalculateAmount() {
		        var indek = GetCurrentInputIndex();
		        var dgPODetail = document.getElementById("dtgPODetail");
		        var partQTY = parseInt(dgPODetail.rows[indek].getElementsByTagName("INPUT")[1].value);
		        var partAmount = dgPODetail.rows[indek].getElementsByTagName("SPAN")[3];
		        var partPrice = parseFloat(dgPODetail.rows[indek].getElementsByTagName("SPAN")[2].innerHTML);
		        if (isNaN(partPrice) || partPrice == "") {
		            partPrice = 0;
		        }
		        var amount = parseInt(partQTY) * parseFloat(partPrice);
		        partAmount.innerHTML = amount;
		    }

		    function focusSave() {
		        document.getElementById("btnSave").focus();
		    }

		    function QtyValidate(source, arguments) {
		        var indek = GetCurrentInputIndex();
		        var dgPODetail = document.getElementById("dtgPODetail");
		        var partQTY = parseInt(dgPODetail.rows[indek].getElementsByTagName("INPUT")[1].value);
		        if (partQTY > 0) {
		            arguments.IsValid = true;
		        }
		        else {
		            arguments.IsValid = false;
		        }
		    }

		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PEMESANAN - Pesanan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">Kode Dealer</td>
								<td width="1%">:</td>
								<TD width="75%" colSpan="2"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</tr>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<td width="1%">:</td>
								<TD colSpan="2"><asp:label id="lblDealerName" runat="server"></asp:label>/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe Order</TD>
								<td width="1%">:</td>
								<TD colSpan="2"><asp:dropdownlist id="ddlOrderType" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR id="trPQRNo" runat="server" visible="false">
								<TD class="titleField">Nomor PQR</TD>
								<td width="1%">:</td>
								<TD colSpan="2">
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtPQRNo"
										onblur="omitSomeCharacter('txtPQRNo','<>?*%^():|\@#$;+=`~{}');blurPQRNoText();" runat="server" Width="150px"></asp:textbox>&nbsp;
                                    <asp:Label ID="lblSearchPQRNo" runat="server">
										    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Nomor /Tanggal PO</TD>
								<td width="1%">:</td>
								<TD width="20%"><asp:textbox id="txtPONumber" runat="server" ReadOnly="True" size="22">[Dibuat oleh sistem]</asp:textbox></TD>
								<TD width="55%"><cc1:inticalendar id="icOrderDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>
							<tr>
								<td class="titleField">Nilai Pemesanan</td>
								<td width="1%">:</td>
								<td>Rp.
									<asp:label id="lblTotPOAmount" runat="server">0</asp:label></td>
								<td><asp:checkbox id="chkRequestForCanceled" runat="server" AutoPostBack="True" Visible="False" Text="Diajukan untuk dibatalkan ke MMKSI"></asp:checkbox><asp:label id="lblReqbatalKTB" runat="server" ForeColor="Red"></asp:label></td>
							</tr>
						</TABLE>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgPODetail" runat="server" Width="100%" AllowSorting="True" BorderWidth="0px"
								BorderColor="Gainsboro" BackColor="#CDCDCD" CellSpacing="1" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn ReadOnly="True" HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox id="txtFPartNumber" tabIndex="10" runat="server" width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
												onblur="omitCharsOnCompsTxt(this,'<>?*%$;');onBlurPartNo(this);"></asp:TextBox>
											<asp:Label id="lblFPopUpSparePart" tabIndex="20" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
													border="0"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id=txtEPartNumber title='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' tabIndex=10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' width="70">
											</asp:TextBox>
											<asp:Label id="lblEPopUpSparePart" tabIndex="20" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
													border="0"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblPartname runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblFPartName" runat="server"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id=lblEPartName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Quantity" HeaderText="Jumlah">
										<HeaderStyle HorizontalAlign="Right" Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFQTY" tabIndex="20" runat="server"
												size="5" CssClass="textRight" MaxLength="6" onchange="CalculateAmount()"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtEQTY tabIndex=30 runat="server" size="5" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' CssClass="textRight" MaxLength="6" onchange="CalculateAmount()">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Eceran (Rp)">
										<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblRetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice","{0:#,##0}") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblFRetailPrice" runat="server" CssClass="textRight"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id=lblERetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice","{0:###}") %>' CssClass="textRight">
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Amount" HeaderText="Total Harga (Rp)">
										<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblPOAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>' CssClass="textRight">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblFPOAmount" runat="server" CssClass="textRight"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id=lblEPOAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>' CssClass="textRight">
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="aksi">
										<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lbtnAdd" tabIndex="40" CommandName="add" text="Tambah" Runat="server">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="save" text="Simpan" Runat="server">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 24px"><asp:button id="btnNew" tabIndex="60" runat="server" Width="50px" Text="Baru" CausesValidation="False"></asp:button><asp:button id="btnCancel" tabIndex="70" runat="server" Width="48px" Text="Batal" CausesValidation="False"
							Enabled="False"></asp:button><asp:button id="btnSave" tabIndex="50" runat="server" Text="Simpan" CausesValidation="False"></asp:button><asp:button id="btnPrint" tabIndex="80" runat="server" Width="48px" Text="Cetak" CausesValidation="False"
							Enabled="False"></asp:button><asp:button id="btnSubmit" tabIndex="90" runat="server" Width="48px" Text="Kirim" CausesValidation="False"
							Enabled="false"></asp:button><INPUT id="btnBack" onclick="BackButton();" type="button" value="Kembali" runat="server">&nbsp;
						<asp:button id="btnDnload" runat="server" Visible="False" Text="Download"></asp:button></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			<INPUT id="hid_f" type="hidden" value="0" runat="server"> <INPUT id="hid_History" type="hidden" value="0" runat="server">
			<INPUT id="hid_back" type="hidden" value="true" runat="server">
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

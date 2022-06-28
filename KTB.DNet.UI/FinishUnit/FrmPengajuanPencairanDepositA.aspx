<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPengajuanPencairanDepositA.aspx.vb" Inherits="FrmPengajuanPencairanDepositA"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPengajuanPencairanDepositA</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function ShowPPDealerSelection() {
		        //showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		        showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var tempParam = selectedDealer.split(';');
		        var txtDealerSelection = document.getElementById("txtKodeDealer");
		        var lblDealerName = document.getElementById("lblDealerName");
		        txtDealerSelection.value = tempParam[0];
		        lblDealerName.innerHTML = tempParam[1] + " - " + tempParam[3];
		        var imgDealer = document.getElementById("imgDealer");
		        //imgDealer.onclick();
		    }

		    function CalculatePPn(txtAmount, txtPPn) {
		        //var txtPPn = document.getElementById("txtPPn");
		        //var txtHeaderAmount = document.getElementById("txtHeaderAmount");				
		        alert(txtAmount.value);
		        txtPPn.value = 0.1 * txtAmount.value;
		        var lblTotal = document.getElementById("lblTotal");
		        lblTotal.value = lblTotal.value + txtAmount.value;

		    }

		    function CalculatePPn1() {
		        var indek = GetCurrentInputIndex();
		        var dgEntryPencairanDepositA = document.getElementById("dgEntryPencairanDepositA");
		        // input berupa teks box, urutan dikolom
		        var txtJumlahPencairan = dgEntryPencairanDepositA.rows[indek].getElementsByTagName("INPUT")[0];
		        var txtPPn = dgEntryPencairanDepositA.rows[indek].getElementsByTagName("INPUT")[1];
		        //txtPPn.value = 0.1 * txtJumlahPencairan.value;
		        var lblTotal = document.getElementById("lblTotal");

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            //txtPPn.innerText = numberFormat(0.1 * parseFloat(txtJumlahPencairan.value));																				
		            lblTotal.innerHTML = numberFormat(parseFloat(txtJumlahPencairan.value) + parseFloat(0.1 * txtJumlahPencairan.value));
		            //txtJumlahPencairan.value = numberFormat(txtHid.value);
		        }
		        else {
		            //txtPPn.value = numberFormat(0.1 * txtJumlahPencairan.value);										
		            lblTotal.value = numberFormat(parseFloat(txtJumlahPencairan.value) + parseFloat(0.1 * txtJumlahPencairan.value));
		            //txtJumlahPencairan.value = numberFormat(txtHid.value);
		        }
		    }


		    function GetCurrentInputIndex() {
		        var dtgArea = document.getElementById("dgEntryPencairanDepositA");
		        var currentRow;
		        var index = 0;
		        var inputs;
		        var indexInput;

		        for (index = 0; index < dtgArea.rows.length; index++) {
		            inputs = dtgArea.rows[index].getElementsByTagName("INPUT");

		            if (inputs != null && inputs.length > 0) {
		                for (indexInput = 0; indexInput < inputs.length; indexInput++) {
		                    if (inputs[indexInput].type != "hidden")
		                        return index;
		                }
		            }
		        }
		        return -1;
		    }



		    function ShowPPDealerBankAccountSelection(DealerID) {
		        var kodeDealer = document.getElementById("txtKodeDealer");
		        if (kodeDealer.value != null && kodeDealer.value.length > 0) {
		            showPopUp('../PopUp/PopUpDealerBankAccountSelectionOne.aspx?DealerCode=' + kodeDealer.value, '', 500, 760, DealerBankAccountSelection);
		        }
		        else {
		            alert('Tentukan dealer terlebih dahulu !');
		        }
		    }

		    function DealerBankAccountSelection(selectedAccount) {
		        var txtDealerBankAccountSelection = document.getElementById("txtNomorRekening");
		        txtDealerBankAccountSelection.value = selectedAccount;
		    }

		    function numberFormat(nStr, prefix) {
		        var prefix = prefix || '';
		        nStr += '';
		        x = nStr.split('.');
		        x1 = x[0];
		        x2 = x.length > 1 ? ',' + x[1] : '';
		        var rgx = /(\d+)(\d{3})/;
		        while (rgx.test(x1))
		            x1 = x1.replace(rgx, '$1' + '.' + '$2');
		        return prefix + x1 + x2;
		    }





		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Sales - DepositA - Pengajuan Pencairan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</table>
			<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titleField">Kode Dealer</td>
					<td>:</td>
					<td><asp:literal id="ltrDealerCode" runat="server"></asp:literal>
						<asp:TextBox ID="txtKodeDealer" runat="server" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
						<asp:ImageButton ID="imgDealer" Visible="False" Runat="server" ImageUrl="../images/popup.gif"></asp:ImageButton>
						<asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
						</asp:label>
					</td>
					<td class="titleField" style="HEIGHT: 32px">Tanggal Pengajuan</td>
					<td style="HEIGHT: 32px">:</td>
					<td style="HEIGHT: 32px"><asp:label id="lblPostingDate" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="titleField" style="HEIGHT: 32px">Nama Dealer</td>
					<td style="HEIGHT: 32px">:</td>
					<td style="HEIGHT: 32px"><asp:literal id="ltrDealerName" runat="server"></asp:literal><asp:label id="lblDealerName" Runat="server"></asp:label></td>
					<td class="titleField">No. Ref&nbsp;Surat Pengajuan</td>
					<td>:</td>
					<td><asp:textbox id="txtNomerSuratPengajuan" Runat="server" MaxLength="18"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField" style="WIDTH: 146px"><asp:label id="lblCode" Runat="server">Tipe Pengajuan</asp:label></td>
					<td style="WIDTH: 2px">:</td>
					<td class="titleField"><asp:dropdownlist id="ddlTipePengajuan" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td class="titleField"><asp:label id="lblNomorRekening" Runat="server" text="Nomor Rekening"></asp:label></td>
					<td>:</td>
					<td><asp:textbox id="txtNomorRekening" onblur="omitSomeCharacter('txtNomorRekening','<>?*%$')" Runat="server"></asp:textbox>
						<!--<asp:linkbutton id="lnkAccount" runat="server" Visible="True">
							<img src="../images/popup.gif" border="0" style="cursor:hand" alt="Klik popup">
						</asp:linkbutton>-->
						<asp:label id="lblBankAccount" runat="server">
							<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
						</asp:label>
						<asp:TextBox id="txtDealerID" runat="server" Width="1px" style="DISPLAY:none"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="titleField"><asp:label id="lblTipeDokumen" Runat="server" text="Tipe Dokumen"></asp:label></td>
					<td>:</td>
					<td><asp:radiobutton id="rbDN" runat="server" AutoPostBack="True" GroupName="Tipe" Checked="True" Text="Debit Note"></asp:radiobutton><asp:radiobutton id="rbSO" runat="server" AutoPostBack="True" GroupName="Tipe" Text="Sales Order"></asp:radiobutton></td>
					<td class="titleField"><asp:label id="Label1" Runat="server" text="Nomor Rekening">No. Reg. Pengajuan</asp:label></td>
					<td>:</td>
					<td>
						<asp:label id="lblNoReg" Runat="server" text=""></asp:label></td>
				</tr>
				<tr>
					<td class="titleField"><asp:label id="lblDNSONumber" Runat="server" text="DN Number"></asp:label></td>
					<td><asp:label id="lblSONumber" runat="server">:</asp:label></td>
					<td colSpan="4"><asp:dropdownlist id="ddlDN" Runat="server" AutoPostBack="True" Visible="True" Width="300px"></asp:dropdownlist><asp:dropdownlist id="ddlSO" Runat="server" AutoPostBack="True" Visible="False"></asp:dropdownlist><asp:textbox id="txtSONumber" runat="server" Visible="False"></asp:textbox></td>
				</tr>
				<TR>
					<TD class="titleField"><asp:label id="rbPeriode" Runat="server">Periode</asp:label></TD>
					<TD><asp:label id="lblPeriode" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlYear" Runat="server" Visible="False"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="rbBulan" Runat="server">Bulan</asp:label></TD>
					<TD><asp:label id="lblBulan" runat="server">:</asp:label></TD>
					</TD>
					<TD><asp:dropdownlist id="ddlPeriode" Runat="server" AutoPostBack="True" Visible="False" Width="104px"></asp:dropdownlist></TD>
				</TR>

                <TR>
					<TD class="titleField"><asp:label id="lblProduk" Runat="server">Produk</asp:label></TD>
					<TD><asp:label id="rbProduk" runat="server">:</asp:label></TD>
					</TD>
					<TD><asp:dropdownlist id="ddlProductCategory" Runat="server" AutoPostBack="True" Visible="true" Width="104px"></asp:dropdownlist></TD>
				</TR>


				<TR>
					<TD class="titleField"><asp:button id="btnCari" runat="server" Visible="False" Text="Cari" Width="64px"></asp:button></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
			<div id="div1">
                <asp:DataGrid ID="dgEntryPencairanDepositA" runat="server" Width="100%" BackColor="#CDCDCD" ShowFooter="True"
                    AutoGenerateColumns="False" AllowSorting="False" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro"
                    BorderWidth="0px">
                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                    <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text="1"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Size="Small"></FooterStyle>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Produk" Visible="false">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblProdukHeader" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryCode") %>'></asp:Label>
                                  <asp:Label ID="lblProdukID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCategoryID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Size="Small"></FooterStyle>
                        </asp:TemplateColumn>


                        <asp:TemplateColumn HeaderText="Jumlah Total">
                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblHeaderAmount" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderAmount","{0:#,###}") %>' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHeaderAmountEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderAmount","{0:#,###}" ) %>' BackColor="White" Width="95px" MaxLength="18" Enabled="False">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Jumlah Pencairan">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblJumlahPencairan" Text='<%# DataBinder.Eval(Container.DataItem, "DealerAmount","{0:#,###}") %>' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtJumlahPencairan" runat="server" BackColor="White" Width="150px" MaxLength="18"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJumlahPencairanEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerAmount","{0:#,###}" ) %>' BackColor="White" Width="95px" MaxLength="18">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PPn">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPPn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PPn","{0:#,###.00}") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPPn" runat="server" BackColor="White" Width="150px" MaxLength="18" ReadOnly="True"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPPnEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PPn","{0:#,###.00}" ) %>' BackColor="White" Width="95px" MaxLength="18" Enabled="False">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Penjelasan">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPenjelasan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Penjelasan") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Left"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPenjelasanEntry" Width="200px" runat="server" CssClass="textLeft" />
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPenjelasanEntryEdit" Width="200px" runat="server" CssClass="textLeft" Text='<%# DataBinder.Eval(Container.DataItem, "Penjelasan") %>' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
                            CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
                            EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
			<TABLE>
				<tr vAlign="top">
					<td class="titleField" width="5%">Total</td>
					<td width="2%">:</td>
					<td align="right" width="20%"><asp:label id="lblTotal" runat="server" Text="0" Font-Bold="True"></asp:label></td>
					<td colSpan="3"></td>
				</tr>
			</TABLE>
			<br>
			<TABLE>
				<tr>
					<td class="titleField" colSpan="3">Catatan:
					</td>
				</tr>
				<tr>
					<td class="titleField" colSpan="3">1.
						<asp:label id="lblNote1" runat="server">Dokumen ini merupakan bagian tidak terpisahkan dari Akta Perjanjian Penunjukan Dealer</asp:label></td>
				</tr>
				<TR>
					<TD class="titleField" colSpan="3">&nbsp;&nbsp;&nbsp;
						<asp:label id="lblPersetujuan" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="3">2.
						<asp:label id="Label2" runat="server">Dokumen ini dibuat dalam bentuk elektronik dan diperlakukan  sebagai alat bukti yang sah</asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="3">&nbsp;&nbsp;&nbsp;
						<asp:label id="lblPersetujuan2" runat="server"></asp:label></TD>
				</TR>
			</TABLE>
			<br>
			<asp:button id="btnSave" Runat="server" Text="Simpan"></asp:button>
            <asp:button id="btnDelete" Runat="server" Text="Hapus" Visible="false"></asp:button>
			<asp:button id="btnCancel" runat="server" Text="Kembali"></asp:button>
			<asp:button id="btnNew" Runat="server" Text="Pengajuan Baru"></asp:button>
			<asp:Button ID="btnValidasi" Runat="server" Text="Validasi"></asp:Button>
		</form>
	</body>
</HTML>

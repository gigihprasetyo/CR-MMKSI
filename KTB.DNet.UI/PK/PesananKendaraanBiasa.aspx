<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PesananKendaraanBiasa.aspx.vb" Inherits="PesananKendaraanBiasa" smartNavigation="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PesananKendaraanBiasa</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
		<!--
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		
		<meta url="/KTB.DNet/PK/PesananKendaraanBiasa.aspx">
		-->
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		    function ShowPPDealerBranchSelection() {
		        showPopUp('../PK/../PopUp/PopUpDealerBranchSelectionOne.aspx?m=d', '', 500, 760, DealerBranchSelection);
		    }

		    function DealerBranchSelection(selectedDealer) {
		        if (selectedDealer.indexOf(";") > 0)
                    {
		            var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		            var txtBranchName = document.getElementById("txtBranchName");
		            txtDealerSelection.value = selectedDealer.split(";")[0];
		            txtBranchName.value = selectedDealer.split(";")[2];
		        }
		        else
		        {
		            var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		            txtDealerSelection.value = selectedDealer;
		        }
		    }

		
			function BackToPrev()
			{
				var url=document.getElementById("txtUrlToBack").value;
				if (url == "") return;

				window.location = url;
			}
			
		
			function ShowPPKodeModelSelection()
			{
			    var ddlCategory = document.getElementById("ddlKategori");
			    var ddlTahunPerakitan = document.getElementById("ddlTahunPerakitanAtauImport");
			    showPopUp('../General/FrmModelSelection.aspx?cat=' + ddlCategory.value + '&caller=PK' + '&assemblyyear=' + ddlTahunPerakitan.value, '', 400, 400, KodeTipe)
			}
		
				function GetCurrentInputIndex()
				{
				var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
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
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}				
				return -1;
			}
			
			function KodeTipe(selectedType)
			{
				var indek = GetCurrentInputIndex();
				var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
				var KodeTipe = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
				KodeTipe.value = selectedType
				
			}
				
			function ShowPPKodeWarnaSelection()
			{
			var indek = GetCurrentInputIndex();
			var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
			var KodeTipe = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[0];
			var ddlTahunPerakitan = document.getElementById("ddlTahunPerakitanAtauImport");
			showPopUp('../General/FrmKodeWarna.aspx?type=' + KodeTipe.value + '&pktype=1' + '&assemblyyear=' + ddlTahunPerakitan.value, '', 400, 400, KodeWarna)
			}
			
			function KodeWarna(selectedColor)
			{
				var indek = GetCurrentInputIndex();
				var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
				var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[1];
				var tempParam = selectedColor.split(';');
				var hiddenField = document.getElementById("HideField")
				KodeWarna.value = tempParam[0];
				hiddenField.value = tempParam[1];
				
							
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
			    var txtNomorPesanan = document.getElementById("txtNomorPesanan");
			    if (txtNomorPesanan.value == '')
			    {
			        alert('Nomor Pesanan masih kosong');
			        clearInputFile(fileUpload);
			        return;
			    }
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN - Pengajuan PK Biasa</td>
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
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblNomorPK" runat="server">Nomor Reg PK</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblNomorPKValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTanggalPesanan" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTanggalPesananValue" runat="server">20 Jun 2005</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblStatus" runat="server">Status</asp:label> </TD>
								<TD> :</TD>
								<TD>  	
								    <asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 24px"><asp:label id="lblNomorPesanan" runat="server">Nomor Pesanan Dealer</asp:label></TD>
								<TD style="HEIGHT: 24px"><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 24px"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" onblur="alphaNumericPlusBlur(txtNomorPesanan)"
										id="txtNomorPesanan" runat="server" Width="140px" MaxLength="20"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Nomor Pesanan tidak boleh kosong"
										ControlToValidate="txtNomorPesanan">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKodeDealerValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlKategori" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNamaDealerValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTahunPerakitanAtauImport" runat="server">Tahun Perakitan</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlTahunPerakitanAtauImport" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField"><asp:label id="lblKota" runat="server">Kota</asp:label>&nbsp;Dealer</TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKotaValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblJenisPesanan" runat="server">Jenis Pesanan</asp:label></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlJenisPesanan" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR id="trBranchHilang" runat="server" valign="top">
								<TD class="titleField"> <asp:Label ID="lbl2KodeCabang" runat="server">Kode Cabang</asp:Label> </TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD>
                                    <span id="spanPopUpDB" runat="server" visible="true">
                                    <asp:textbox id="txtDealerBranchCode" Width="150px" Runat="server"></asp:textbox>
									<asp:label id="lblPopUpDealerBranch" runat="server" width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
                                         </span>

                                    <asp:Label ID="lblDealerBranch" runat="server" Visible="False" ></asp:Label>
								</TD>
								<TD class="titleField"><asp:label id="lblRencanaPenebusan" runat="server">Rencana Penebusan</asp:label></TD>
								<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlRencanaPenebusan" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR valign="top">
								<TD id="tdDealerBranch" runat="server" class="titleField" style="HEIGHT: 24px" ><STRONG>
										<label id="trDealerBranch"  runat="server">Nama Cabang</label></STRONG> </TD>
								<TD id="tdTitik2Branch" runat="server" style="HEIGHT: 24px"><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD id="tdBranchName" runat="server" style="HEIGHT: 24px"><asp:textbox id="txtBranchName" Width="150px" Runat="server"></asp:textbox>
								</TD>
								<TD class="titleField"><asp:label id="lblKonfirmasi" runat="server">Konfirmasi</asp:label></TD>
								<TD><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD><asp:textbox style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BACKGROUND-COLOR: #eeeeee; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid"
										id="txtKonfirmasi" runat="server" Rows="3" Columns="30" BackColor="#F5F1EE" disabled="true"
										TextMode="MultiLine"></asp:textbox><asp:label id="lblSearchKonfirmasi" Visible="false" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
                            <TR>
								<TD><STRONG>
										<asp:label style="Z-INDEX: 0" id="lblGuarantee" runat="server">Jaminan</asp:label></STRONG></TD>
								<TD> <asp:label style="Z-INDEX: 0" id="lblColonGuarantee" runat="server">:</asp:label></TD>
								<TD>  	
									<asp:CheckBox style="Z-INDEX: 0" id="chkGuarantee" runat="server" Text=" " Enabled="False"></asp:CheckBox></TD>
								<TD colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" AutoPostBack="true" ID="cbxIsConfirmation" Visible="true" Text="Dealer bersedia dikirim unit tanpa menunggu form A"/>
                                            </td>
                                        </tr>
                                    </table>
								</TD>
							</TR>

                             <TR>
                                <td></td>
                                <td></td>
                                <td></td>
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
							</TR>

                            <TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>


							<TR>
								<TD colSpan="6"><asp:datagrid id="dtgPesananKendaraan" runat="server" Width="100%" OnUpdateCommand="dtgPesananKendaraan_Update"
										OnCancelCommand="dtgPesananKendaraan_Cancel" OnEditCommand="dtgPesananKendaraan_Edit" OnItemCommand="dtgPesananKendaraan_ItemCommand"
										ShowFooter="True" CellPadding="3" BorderWidth="1px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
										OnItemDataBound="dtgPesananKendaraan_ItemDataBound" BackColor="#E0E0E0">
										<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle VerticalAlign="Top"></HeaderStyle>
										<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle Font-Size="Small"></FooterStyle>
											</asp:TemplateColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Model / Tipe / Warna">
												<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Kode Tipe">
												<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=lblViewKodeModel runat="server" NAME="lblViewKodeModel" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleTypeCode" )  %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
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
												<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=lblViewKodeWarna runat="server" NAME="lblViewKodeWarna" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleColorCode" ) %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox id="txtFooterKodeWarna" runat="server" size="2" BackColor="White" MaxLength="4"></asp:TextBox>
													<asp:Label id="lblFooterKodeWarna" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id=txtEditKodeWarna runat="server" size="2" BackColor="White" MaxLength="4" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleColorCode" ) %>'>
													</asp:TextBox>
													<asp:Label id="lblEditKodeWarna" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Order (Unit)">
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=lblViewUnitPermintaanDealer runat="server" NAME="lblViewUnitPermintaanDealer" Text='<%# DataBinder.Eval(Container.DataItem, "TargetQty", "{0:#,###}" ) %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox id="txtFooterUnitPermintaanDealer" onkeypress="return numericOnlyUniv(event)" runat="server"
														size="2" CssClass="textRight"></asp:TextBox>
													<asp:RangeValidator id="RangeValidator2" runat="server" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
														ControlToValidate="txtFooterUnitPermintaanDealer" MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id=txtEditUnitPermintaanDealer onkeypress="return numericOnlyUniv(event)" runat="server" size="2" Text='<%# DataBinder.Eval(Container.DataItem, "TargetQty") %>' CssClass="textRight">
													</asp:TextBox>
													<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
														ControlToValidate="txtEditUnitPermintaanDealer" MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Harga (Rp)" DataFormatString="{0:#,###}">
												<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="PPh22 (Rp)" DataFormatString="{0:#,###}">
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Alokasi (Unit)">
												<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Harga (Rp)">
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblHarga" runat="server"></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="PPh22 (Rp)">
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
												CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
												EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
												<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											</asp:EditCommandColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
														<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<FooterStyle HorizontalAlign="Center"></FooterStyle>
												<FooterTemplate>
													<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
														<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
												</FooterTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
									</asp:datagrid>
                                    <asp:label id="lblLagend" runat="server" Width="152px" ForeColor="SteelBlue" Visible="false">PD = Permintaan Dealer</asp:label>
                                    <asp:label id="lblLagend2" runat="server" ForeColor="SteelBlue" Visible="false">SK = Disetujui MKS</asp:label><br>
									<asp:label id="lblError" runat="server" Width="624px" ForeColor="Red" EnableViewState="False"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 146px; HEIGHT: 4px"><asp:label id="lblTotalUnit" runat="server" Width="112px"><b>Total 
											Unit</b></asp:label></TD>
								<TD style="WIDTH: 12px; HEIGHT: 4px"><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 191px; HEIGHT: 4px"><asp:label id="lblTotalUnitValue" runat="server"></asp:label></TD>
								<TD style="WIDTH: 163px; HEIGHT: 4px"><asp:label id="lblTotalHargaUnit" runat="server" Width="112px"><b>Total 
											Harga Tebusan</b></asp:label></TD>
								<TD style="WIDTH: 9px; HEIGHT: 4px"><asp:label id="Label18" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 4px"><asp:label id="lblTotalHargaUnitPD" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td colSpan="6"><asp:label id="Label15" runat="server" Font-Bold="True">Perhatian : </asp:label><asp:label id="Label5" runat="server" Width="376px"> Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli</asp:label><asp:label id="lblspaNumber" runat="server"></asp:label>&nbsp;Tanggal
									<asp:label id="lblspaDate" runat="server"></asp:label></td>
							</tr>
							<TR>
								<TD colSpan="6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label14" runat="server">Pesanan Bulanan harus Divalidasi paling Lambat tanggal 20 setiap bulan n-1</asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnBaru" runat="server" Text="Baru"></asp:button><asp:button id="btnValidasi" runat="server" Text="Validasi"></asp:button><asp:button id="btnDelete" runat="server" Text="Hapus"></asp:button>
									<asp:Button ID="btnBack" Runat="server" Text="Kembali" Enabled="False" Visible="False"></asp:Button><INPUT type="hidden" id="HideField" runat="server" NAME="HideField">
									<input type="button" value="Kembali" onclick="BackToPrev();">
									<asp:TextBox ID="txtUrlToBack" Text="" Runat="server" ReadOnly="True" style="VISIBILITY:hidden"></asp:TextBox>
                                    <asp:TextBox ID="lblPath" runat="server" Width="0px" style="display:none"></asp:TextBox>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 151px" colSpan="2"></TD>
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
		</script>
	</body>
</HTML>

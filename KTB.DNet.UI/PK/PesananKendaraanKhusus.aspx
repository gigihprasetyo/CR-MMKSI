<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PesananKendaraanKhusus.aspx.vb" Inherits="PesananKendaraanKhusus" smartNavigation="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PesananKendaraanKhusus</title>
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<LINK rel="stylesheet" type="text/css" href="./WebResources/stylesheet.css">
		<LINK href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
		<!--
		<base href="http://localhost/KTB.DNet/PK/"/>
		-->
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		    function ShowPPDealerBranchSelection() {
		        showPopUp('../PK/../PopUp/PopUpDealerBranchSelectionOne.aspx?m=d', '', 500, 760, DealerBranchSelection);
		    }

		    function DealerBranchSelection(selectedDealer) {
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

			function BackToPrev()
			{
				var url=document.getElementById("txtUrlToBack").value;
				if (url == "") return;
				window.location = url;
			}
		
			function Penjelasan(Text)
			{
				var txtPenjelasan = document.getElementById("txtPenjelasan");
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				txtPenjelasan.innerText=Text;		
				}
				else
				{
				txtPenjelasan.value=Text;	
				}
			}		
			
			function ShowPPPenjelasan()
			{
			var txtPenjelasan = document.getElementById("txtPenjelasan");
			var enter = 13;
			var feedline = 10;
			var newstring = replace(txtPenjelasan.value, String.fromCharCode(enter), '@');
			newstring = replace(newstring, String.fromCharCode(feedline), '*');
			var opentag = 60;
			newstring = replace(newstring, String.fromCharCode(opentag), '|');
			showPopUp('../PK/frmAdditionalInformationPK.aspx?text='+newstring+'&type=1'+'&src=pengajuan','',400,400,Penjelasan)
			}
			
			function Konfirmasi(Text)
			{
				var txtKonfirmasi = document.getElementById("txtKonfirmasi");
				txtKonfirmasi.value=Text;
			}		
			
			function ShowPPKonfirmasi()
			{
			var txtKonfirmasi = document.getElementById("txtKonfirmasi");
			var enter = 13;
			var feedline = 10;
			var newstring = replace(txtKonfirmasi.value, String.fromCharCode(enter), '@');
			newstring = replace(newstring, String.fromCharCode(feedline), '*');
			var opentag = 60;
			newstring = replace(newstring, String.fromCharCode(opentag), '|');						
			showPopUp('../PK/frmAdditionalInformationPK.aspx?text='+newstring+'&type=2'+'&src=pengajuan','',400,400,Konfirmasi)
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
			showPopUp('../General/FrmKodeWarna.aspx?type='+KodeTipe.value+'&pktype=0' + '&assemblyyear=' + ddlTahunPerakitan.value, '', 400, 400, KodeWarna)
			}
			
			function KodeWarna(selectedColor)
			{
				var indek = GetCurrentInputIndex();
				var dtgPesananKendaraan = document.getElementById("dtgPesananKendaraan");
				var KodeWarna = dtgPesananKendaraan.rows[indek].getElementsByTagName("INPUT")[1];
				var tempParam = selectedColor.split(';');
				var hiddenField = document.getElementById("HideField")
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				KodeWarna.innerText = tempParam[0];
				hiddenField.innerText = tempParam[1];				
				}
				else
				{
				KodeWarna.value = tempParam[0];
				hiddenField.value = tempParam[1];
				}
			}

			function REmoveTAb() {

			    var ObjText = document.getElementById("txtNamaPesananKhusus").value;
			    document.getElementById("txtNamaPesananKhusus").value = rtrim(ltrim(ObjText.replace(/\t/g, '')));
			    //if (navigator.appName == "Microsoft Internet Explorer") {
			    //    console.log(ObjText.innerText);
			    //    ObjText.innerText = ObjText.innerText.replace('\t', '');
			    //}
			    //else {
			    //    console.log(ObjText.value);
			    //    ObjText.value = ObjText.value.replace('\t', '');
			    //}
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
			    var txtNamaPesananKhusus = document.getElementById("txtNamaPesananKhusus");
			    if (txtNamaPesananKhusus.value == '') {
			        alert('Nama Pesanan masih kosong');
			        clearInputFile(fileUpload);
			        return;
			    }
			    var txtNomorPesanan = document.getElementById("txtNomorPesanan");
			    if (txtNomorPesanan.value == '') {
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
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage" colSpan="2">PESANAN KENDARAAN - Pengajuan PK Khusus</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif" colSpan="2"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10" colSpan="2"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblNomorPK" runat="server">Nomor Reg PK</asp:label></TD>
								<TD width="1%"><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblNomorPKValue" runat="server"></asp:label></TD>
								<TD class="titleField" width="120px"><asp:label id="lblTanggalPesanan" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTanggalPesananValue" runat="server">20 Jun 2005</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblNomorPesanan" runat="server">Nomor Pesanan Dealer</asp:label></TD>
								<TD><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD><asp:textbox onblur="alphaNumericPlusSpaceBlur(txtNomorPesanan)" id="txtNomorPesanan" onkeypress="return alphaNumericPlusSpaceUniv(event)"
										runat="server" Width="140px" maxLength="20" size="22"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Nomor Pesanan tidak boleh kosong"
										ControlToValidate="txtNomorPesanan">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKodeDealerValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlKategori" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNamaDealerValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTahunPerakitanAtauImport" runat="server">Tahun Perakitan</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlTahunPerakitanAtauImport" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
                             <TR valign="top">
								<TD class="titleField"><asp:label id="lblKota" runat="server">Kota</asp:label>&nbsp;Dealer</TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblKotaValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblJenisPesanan" runat="server">Jenis Pesanan</asp:label></TD>
								<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlJenisPesanan" runat="server" Width="140" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
                            <TR id="trBranchHilang" runat="server">
								<TD class="titleField"> <asp:Label ID="lbl2KodeCabang" runat="server">Kode Cabang</asp:Label> </TD>
								<TD>:</TD>
								<TD>
								    <span id="spanPopUpDB" runat="server" visible="true">
                                         <input style="display: none" type="text" name="fakeusernameremembered" />
<input style="display: none" type="password" name="fakepasswordremembered" />
                                        <asp:textbox id="txtDealerBranchCode" Width="150px" Runat="server"></asp:textbox>
									    <asp:label id="lblPopUpDealerBranch" runat="server" width="10"> 
										    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									    </asp:label>
                                    </span>

                                    <asp:Label ID="lblDealerBranch" runat="server" Visible="False" ></asp:Label>
								</TD>
                                <TD class="titleField"><asp:label id="lblRencanaPenebusan" runat="server">Rencana Penebusan</asp:label></TD>
								<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlRencanaPenebusan" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>

							<TR>
								<TD class="titleField" style="HEIGHT: 24px" ><STRONG>
								    <label id="trDealerBranch"  runat="server">Nama Cabang</label></STRONG> </TD>
								<TD><asp:label id="lblBranchNameTtk" runat="server">:</asp:label></TD>
								<TD><asp:textbox id="txtBranchName" Width="150px" Runat="server"></asp:textbox></TD>

								<TD class="titleField" runat="server" id="tdNoApp">Nomor Aplikasi</TD>
								<TD runat="server" id="tdNoAppTtk">:</TD>
								<TD valign="top" runat="server" id="tdSPLNo"><asp:label id="lblSPLNumber" runat="server"></asp:label>&nbsp
                                    <asp:imagebutton id="ibtnDownload" runat="server" ToolTip="Download SPL" ImageUrl="../images/download.gif" visible="False"></asp:imagebutton></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField"><asp:label id="lblNamaPesananKhusus" runat="server">Nama Pesanan Khusus</asp:label></TD>
								<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
                                <td>
                                    <asp:TextBox ID="txtNamaPesananKhusus" runat="server" MaxLength="40"  onblur="REmoveTAb();" onchange="REmoveTAb();">

                                    </asp:TextBox>
                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nama Pesanan Khusus tidak boleh kosong"
                                        ControlToValidate="txtNamaPesananKhusus">*</asp:RequiredFieldValidator>
                                </td>
								<TD class="titleField"><asp:label id="lblKonfirmasi" runat="server">Konfirmasi</asp:label></TD>
								<TD><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD><asp:textbox style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BACKGROUND-COLOR: #eeeeee; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid"
										id="txtKonfirmasi" runat="server" Rows="3" Columns="25" BackColor="#F5F1EE" 
										TextMode="MultiLine"></asp:textbox><asp:label id="lblSearchKonfirmasi" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField"><asp:label id="lblPenjelasan" runat="server">Penjelasan</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD><asp:textbox style="BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BACKGROUND-COLOR: #eeeeee; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid"
										id="txtPenjelasan" runat="server" Rows="3" Columns="25" BackColor="#F5F1EE" 
										TextMode="MultiLine"></asp:textbox><asp:label id="lblSearchPenjelasan" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD colspan="3" valign="middle">
                                    <asp:CheckBox runat="server" AutoPostBack="true" ID="cbxIsConfirmation" Visible="true" Text="Dealer bersedia dikirim unit tanpa menunggu form A"/></TD>
							</TR>
							<TR valign="top">
								<TD style="HEIGHT: 20px" class="titleField">
									<asp:label style="Z-INDEX: 0" id="lblGuarantee" runat="server">Jaminan</asp:label></TD>
								<TD style="HEIGHT: 20px">
									<asp:label style="Z-INDEX: 0" id="lblColonGuarantee" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 213px; HEIGHT: 20px">
									<asp:CheckBox style="Z-INDEX: 0" id="chkGuarantee" runat="server" Enabled="False" Text=" "></asp:CheckBox></TD>
                                <TD class="titleField"><asp:label id="lblUploadDok" runat="server" Visible="false">Upload Surat</asp:label></TD>
								<TD><asp:label id="lbltitik2Upload" runat="server" Visible="false">:</asp:label></TD>
								<TD>
                                    <table >
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
							<TR id="trSPL" runat="server" style="display:none">
                                <td colspan="3"></td>
								<TD class="titleField"><asp:label id="lblHeadPKNumber" runat="server">Nomor PK Induk</asp:label></TD>
								<TD><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblHeadPKNumberValue" runat="server"></asp:label></TD>
							</TR>
							<asp:panel id="TOP" Runat="server">
								<TR>
									<TD style="HEIGHT: 20px" class="titleField"></TD>
									<TD style="HEIGHT: 20px"></TD>
									<TD style="HEIGHT: 20px"></TD>
									<TD style="HEIGHT: 20px" class="titleField">Fasilitas Bebas Bunga</TD>
									<TD style="HEIGHT: 20px">:</TD>
									<TD style="WIDTH: 213px; HEIGHT: 20px">
										<asp:DropDownList id="ddlInterest" runat="server">
											<asp:ListItem Value="0">Ya</asp:ListItem>
											<asp:ListItem Value="1">Tidak</asp:ListItem>
										</asp:DropDownList>
										<asp:Label id="lblFasilitasBebasBunga" runat="server" Visible="False"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 20px" class="titleField">
									<TD style="HEIGHT: 20px"></TD>
									<TD style="HEIGHT: 20px"></TD>
									<TD style="HEIGHT: 20px" class="titleField">Fasilitas TOP</TD>
									<TD style="HEIGHT: 20px">:</TD>
									<TD style="WIDTH: 213px; HEIGHT: 20px">
										<asp:Label id="lblTOP" runat="server"></asp:Label></TD>
								</TR>
							</asp:panel>
							<TR style="display:none">
                                <td colspan="3"></td>
								<TD style="HEIGHT: 20px" class="titleField">Deskripsi SPL</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px"><asp:Label ID="lblDeskripsiSPL" runat="server"></asp:Label></TD>
							</TR>

                            <TR>
								<TD> &nbsp;</TD>
								<TD>
									 &nbsp;</TD>
								<TD>
								    &nbsp;</TD>
								<TD  >&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
							</TR>


							<TR>
								<TD colSpan="6"><asp:datagrid style="Z-INDEX: 0" id="dtgPesananKendaraan" runat="server" Width="100%" BackColor="#E0E0E0"
										OnUpdateCommand="dtgPesananKendaraan_Update" OnCancelCommand="dtgPesananKendaraan_Cancel" OnEditCommand="dtgPesananKendaraan_Edit"
										OnItemCommand="dtgPesananKendaraan_ItemCommand" ShowFooter="True" CellPadding="3" BorderWidth="1px" BorderStyle="None"
										BorderColor="#CDCDCD" AutoGenerateColumns="False" OnItemDataBound="dtgPesananKendaraan_ItemDataBound">
										<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
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
												<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
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
												<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
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
											<asp:TemplateColumn HeaderText="Order (Unit)">
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
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

											<asp:TemplateColumn HeaderText="Diskon/Unit(Rp)">
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblResponseDiscount" runat="server"></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Deposit A(Rp)" Visible="false" >
												<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblResponseSalesSurcharge" runat="server"></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>

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
												CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; alt=&quot;Batal&quot;&gt;"
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
									</asp:datagrid><asp:label id="lblLagend" runat="server" Width="152px" ForeColor="SteelBlue" Visible="false">PD = Permintaan Dealer</asp:label>
                                    <asp:label id="lblLagend2" runat="server" ForeColor="SteelBlue" Visible="false">SK = Disetujui MKS</asp:label><br>
									<asp:label id="lblError" runat="server" Width="624px" ForeColor="Red" EnableViewState="False"></asp:label><BR>
									<asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
							<TR>
								<TD class="titleField"><asp:label id="lblTotalUnit" runat="server" Width="112px">Total Unit</asp:label></TD>
								<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTotalUnitValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTotalHargaUnit" runat="server" Width="112px">Total Harga Tebus</asp:label></TD>
								<TD><asp:label id="Label18" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTotalHargaUnitPD" runat="server"></asp:label></TD>
							</TR>
							<tr>
								<td colSpan="6"><asp:label id="Label20" runat="server" Font-Bold="True">Perhatian : </asp:label><asp:label id="Label17" runat="server">Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli</asp:label>No.
									<asp:label id="lblspaNumber" runat="server"></asp:label>Tanggal
									<asp:label id="lblspaDate" runat="server"></asp:label></td>
							</tr>
							<TR>
								<TD colSpan="6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label21" runat="server">Pesanan Bulanan harus Divalidasi paling Lambat tanggal 20 setiap bulan n-1</asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnBaru" runat="server" Text="Baru"></asp:button><asp:button id="btnValidasi" runat="server" Text="Validasi"></asp:button><asp:button id="btnDelete" runat="server" Text="Hapus"></asp:button><asp:button id="btnSisaAlokasi" runat="server" Text="Proses Sisa Alokasi"></asp:button><asp:button id="btnBack" Runat="server" Visible="False" Text="Kembali" Enabled="False"></asp:button><input onclick="BackToPrev();" value="Kembali" type="button" style="Z-INDEX: 0">
									<asp:textbox style="VISIBILITY: hidden" id="txtUrlToBack" ReadOnly="True" Runat="server" Text=""></asp:textbox><INPUT id="HideField" type="hidden" name="HideField" runat="server">
                                    <asp:TextBox ID="lblPath" runat="server" Width="0px" style="display:none"></asp:TextBox>
								</TD>
							</TR>
						</TABLE>
					</TD>
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

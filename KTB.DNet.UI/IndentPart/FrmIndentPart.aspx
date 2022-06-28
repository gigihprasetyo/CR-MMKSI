<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmIndentPart.aspx.vb" Inherits="FrmIndentPart" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmIndentPart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		var iseditGlobal;
		function ShowPPKodeBarangSelection(isedit)
		{
		iseditGlobal=isedit;
		var ddlMaterialType = document.getElementById("ddlMaterialType");
			if(ddlMaterialType.value=='0')
			{ 
			alert('Pilih Tipe Barang Dulu');
			return;
			}		
		showPopUp('../PopUp/PopUpSparePart.aspx?IPMaterialtype='+ddlMaterialType.value,'',700,700,KodeBarang);
		}
		
		function GetCurrentInputIndex()
			{
				var dtgIPDetail = document.getElementById("dtgIPDetail");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgIPDetail.rows.length; index++)
				{
					inputs = dtgIPDetail.rows[index].getElementsByTagName("INPUT");
					
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
			
		function KodeBarang(selectedCode)
		{
			var indek = GetCurrentInputIndex();
			var dtgIndentPart = document.getElementById("dtgIPDetail");
			var tempParam = selectedCode.split(';');
			var KodeBarang = dtgIPDetail.rows[indek].getElementsByTagName("INPUT")[0];
			if (iseditGlobal==0)
			{
			var HargaBarang = dtgIPDetail.rows[indek].getElementsByTagName("INPUT")[2];
			}
			else
			{
			var HargaBarang = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[3];
			var txtEditHargaBarang = dtgIPDetail.rows[indek].getElementsByTagName("INPUT")[2];
			}
			if(navigator.appName == "Microsoft Internet Explorer")
			{
			var partName = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[1];
			KodeBarang.innerText = tempParam[0];
			var hrgBrg = parseFloat(tempParam[2]);
			var hargaBrg = hrgBrg.toLocaleString();
			partName.innerHTML = tempParam[1];	
				if (iseditGlobal==0)
				{
				HargaBarang.innerText = replace(hargaBrg.substring(0,hargaBrg.length-3),',','.');
				}
				else
				{
				HargaBarang.innerHTML = replace(hargaBrg.substring(0,hargaBrg.length-3),',','.');
				txtEditHargaBarang.innerText = replace(hargaBrg.substring(0,hargaBrg.length-3),',','.');
				}
			}
			else
			{
			var partName = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[1];
			KodeBarang.value = tempParam[0];
			var hrgBrg = parseFloat(tempParam[2]);
			var hargaBrg = hrgBrg.toLocaleString();


				if (iseditGlobal==0)
				{
				HargaBarang.value = replace(hargaBrg,',','.');
				}
				else
				{
				HargaBarang.innerHTML = replace(hargaBrg,',','.');
				txtEditHargaBarang.value = replace(hargaBrg,',','.');
				}

			partName.innerHTML = tempParam[1];
			}
		}
		
		function ShowPPIndentDescription()
		{
		showPopUp('../PopUp/PopUpIndentDescription.aspx','',700,700);
		}
		
		function focusSave()
			{
			document.getElementById("btnSave").focus();			
			}
		function closeWindow()
		{
		window.close();
		}
		
		function SetChassis(isEvent)
		{
			
			var DdlDesc = document.getElementById("DdlDesc");
			var TxtChassisNo = document.getElementById("TxtChassisNo");
			var descValue = DdlDesc.options[DdlDesc.selectedIndex].value;
			var lntnCheckChassis = document.getElementById("lntnCheckChassis");
			var lblSearchChassis = document.getElementById("lblSearchChassis");
			
			if (descValue=='1')
			{
				TxtChassisNo.disabled=false;
				lntnCheckChassis.style.display='';
				lblSearchChassis.style.display='';
					
			}
			else
			{
				TxtChassisNo.disabled=true;
				TxtChassisNo.value='';
				lntnCheckChassis.style.display='none';
				lblSearchChassis.style.display='none';
				
			}
			if (isEvent==1){TxtChassisNo.style.backgroundColor='white';}
			
		}

		function ShowChassisSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpChassisMasterSelection.aspx?indent=1','',500,760,ChassisSelection);
		}
		function ChassisSelection(selectedChassis)
		{
			//alert(selectedChassis);
			var tempParam = selectedChassis.split(';');
			//alert(tempParam);
			var TxtChassisNo = document.getElementById("TxtChassisNo");
			var LblTypeWarna = document.getElementById("LblTypeWarna");
			var hdnTypeWarna = document.getElementById("hdnTypeWarna");
			TxtChassisNo.value = tempParam[0];		
			hdnTypeWarna.value = tempParam[1];	
			
			LblTypeWarna.innerHTML = tempParam[1];	
			TxtChassisNo.style.backgroundColor='white';	
		}
		
		function PrintForm()
		{
			
			//var lntnCheckChassis = document.getElementById("lntnCheckChassis");
			//var lblSearchChassis = document.getElementById("lblSearchChassis");
			//var dtgIndentPart = document.getElementById("dtgIPDetail");
			
			//var objrow = dtgIndentPart.rows[dtgIndentPart.rows.length-1];
			
			
			
			//lntnCheckChassis.style.display='none';
			//lblSearchChassis.style.display='none';
			//if (objrow.style.backgroundColor=='#b5c7de')
			//{objrow.style.display='none';}	
			//hide aksi column
			//for (var row=0; row<dtgIndentPart.rows.length;row++) 
			//{
			//	var cels = dtgIndentPart.rows[row].getElementsByTagName('td')
			//	cels[6].style.display='none';
			//}		

			var divGrid = document.getElementById("divGrid");
			divGrid.style.overflow='visible';
			window.print();
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				divGrid.style.overflow='auto';
			}
			//lntnCheckChassis.style.display='';
			//lblSearchChassis.style.display='';
			//if (objrow.style.backgroundColor=='#b5c7de')
			//{objrow.style.display='';}
			//for (var row=0; row<dtgIndentPart.rows.length;row++) 
			//{
			//	var cels = dtgIndentPart.rows[row].getElementsByTagName('td')
			//	cels[6].style.display='';
			//}		
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">INDENT PART&nbsp;-&nbsp;Pengajuan Indent Part &amp; 
						Accessories</td>
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
								<TD class="titleField" width="30%"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblNomorTanggalPO" runat="server">Nomor / Tanggal Pengajuan</asp:label></TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="70%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:textbox id="txtPONumber" runat="server" BackColor="#efefef" ReadOnly="True" size="22">[Dibuat oleh sistem]</asp:textbox></td>
											<td><cc1:inticalendar id="icOrderDate" runat="server" Enabled="False"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="30%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblMaterialType" runat="server">Tipe Barang</asp:label></TD>
								<TD width="1%"><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="120px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Tipe Pembayaran</TD>
								<TD width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:dropdownlist id="ddlPaymentType" runat="server" Width="120px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Keterangan</TD>
								<TD width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:dropdownlist id="DdlDesc" runat="server" Width="120px" onchange="SetChassis(1);"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Nomor Rangka</TD>
								<TD width="1%"><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:textbox id="TxtChassisNo" Runat="server"></asp:textbox><asp:linkbutton id="lntnCheckChassis" runat="server">
										<img src="../images/tanya.gif" class="hideLinkButtonOnPrint" style="cursor:hand" border="0"
											alt="Verify">
									</asp:linkbutton><asp:label class="hideSpanOnPrint" id="lblSearchChassis" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowChassisSelection();">
									</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Tipe/Warna</TD>
								<TD width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:label id="LblTypeWarna" Runat="server"></asp:label><INPUT id="hdnTypeWarna" type="hidden" runat="server">
								</TD>
							</TR>
                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:label id="LabelTOP" Runat="server"></asp:label>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>
							<TR>
								<TD colSpan="6">
									<div id="divGrid" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 280px" DESIGNTIMEDRAGDROP="245">
										<table width="100%">
											<tr>
												<td><asp:datagrid id="dtgIPDetail" runat="server" BackColor="#CDCDCD" Width="100%" ShowFooter="True"
														AutoGenerateColumns="False" AllowSorting="True" CellSpacing="0" CellPadding="3" BorderColor="Black"
														BorderWidth="1px">
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
																	<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
																	</asp:Label>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:TextBox id="txtFPartNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" tabIndex="10"
																		runat="server" width="70"></asp:TextBox>
																	<asp:Label id="lblFPopUpSparePart" tabIndex="20" runat="server" height="10px">
																		<img class="hideLinkButtonOnPrint" style="cursor:hand" alt="Klik Disini untuk memilih Part Number"
																			src="../images/popup.gif" border="0"></asp:Label>
																</FooterTemplate>
																<EditItemTemplate>
																	<asp:TextBox id=txtEPartNumber title='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' tabIndex=10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' width="70">
																	</asp:TextBox>
																	<asp:Label id="lblEPopUpSparePart" tabIndex="20" runat="server" height="10px">
																		<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
																			border="0">
																	</asp:Label>
																	<asp:Label ID="lblEPartNumber" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
																	</asp:Label>
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
															<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model/Tipe/Warna">
																<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblmodel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>'>
																	</asp:Label>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label id="lblFmodel" runat="server"></asp:Label>
																</FooterTemplate>
																<EditItemTemplate>
																	<asp:Label id="lblEmodel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>'>
																	</asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Qty" HeaderText="Unit">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>' CssClass="textRight">
																	</asp:Label>
																</ItemTemplate>
																<FooterStyle HorizontalAlign="Right"></FooterStyle>
																<FooterTemplate>
																	<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFQTY" tabIndex="20" runat="server"
																		size="5" CssClass="textRight" MaxLength="6" onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
																</FooterTemplate>
																<EditItemTemplate>
																	<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtEQTY tabIndex=30 runat="server" size="5" Text='<%# DataBinder.Eval(Container, "DataItem.qty") %>' CssClass="textRight" MaxLength="6" >
																	</asp:TextBox>
																	<asp:Label ID="lblEQTY" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.qty") %>'>
																	</asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Harga<br>(Belum Termasuk PPn)">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblPrice" Runat="server"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox ID="txtEditPrice" Runat="server" MaxLength="12" onkeypress="return NumericOnlyWith(event,'');"
																		onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
																	<asp:Label ID="lblEditPrice" Runat="server"></asp:Label>
																	<asp:TextBox ID="txtEditPriceTemp" MaxLength="12" style="display:none" Runat="server" onkeypress="return NumericOnlyWith(event,'');"
																		onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
																</EditItemTemplate>
																<FooterTemplate>
																	<asp:TextBox id="txtAddPrice" Runat="server" MaxLength="12" onkeypress="return NumericOnlyWith(event,'.');"
																		onblur="NumOnlyBlurWithOnGridTxt(this,'.');"></asp:TextBox>
																</FooterTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="aksi">
																<HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
																		<img src="../images/edit.gif" class="hideLinkButtonOnPrint" border="0" alt="Ubah"></asp:LinkButton>
																	<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
																		<img src="../images/trash.gif" class="hideLinkButtonOnPrint" border="0" alt="Hapus" OnClick="return confirm('Yakin data ini akan dihapus?');"></asp:LinkButton>
																	<asp:LinkButton id="lbtnPopUpText" tabIndex="41" CommandName="PopUpText" text="PopUp Text" Runat="server">
																		<img src="../images/popup.gif" class="hideLinkButtonOnPrint" border="0" alt="Keterangan"></asp:LinkButton>
																</ItemTemplate>
																<FooterStyle HorizontalAlign="Center"></FooterStyle>
																<FooterTemplate>
																	<asp:LinkButton id="lbtnAdd" tabIndex="40" CommandName="add" text="Tambah" Runat="server">
																		<img src="../images/add.gif" class="hideLinkButtonOnPrint" border="0" alt="Tambah"></asp:LinkButton>
																</FooterTemplate>
																<EditItemTemplate>
																	<asp:LinkButton id="lbtnSave" tabIndex="49" CommandName="save" text="Simpan" Runat="server">
																		<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
																	<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
																		<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
																</EditItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:datagrid></td>
											</tr>
										</table>
									</div>
									<div style="OVERFLOW: auto">
										<asp:button class="hideButtonOnPrint" id="btnNew" tabIndex="60" runat="server" Width="50px"
											CausesValidation="False" Text="Baru"></asp:button><asp:button class="hideButtonOnPrint" id="btnSave" tabIndex="50" runat="server" CausesValidation="False"
											Text="Simpan"></asp:button><asp:button class="hideButtonOnPrint" id="btnValidasi" tabIndex="60" runat="server" Width="50px"
											CausesValidation="False" Text="Kirim"></asp:button><asp:button class="hideButtonOnPrint" id="btnDelete" runat="server" CausesValidation="False"
											Text="Batal"></asp:button><asp:button class="hideButtonOnPrint" id="btnCancel" runat="server" Text="Kembali"></asp:button><INPUT class="hideButtonOnPrint" id="btnCetak" style="WIDTH: 48px; HEIGHT: 21px" onclick="PrintForm();"
											type="button" value="Cetak" name="btnCetak" runat="server"><INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 48px; HEIGHT: 21px" onclick="window.close();"
											type="button" value="Tutup" name="btnClose" runat="server">
										<br>
										<asp:label id="lblError" runat="server" Width="624px" EnableViewState="False" ForeColor="Red"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary>
									</div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hdnValNew" type="hidden" value="0" runat="server"><INPUT id="hdnValDel" type="hidden" value="0" runat="server">
		</form>
		<SCRIPT language="javascript">
		/*
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
		*/
			SetChassis(0);
		</SCRIPT>
	</body>
</HTML>

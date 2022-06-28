<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEstimationEquipment.aspx.vb" Inherits="FrmEstimationEquipment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEstimationEquipmentment</title>
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
		    showPopUp('../PopUp/PopUpSparePart.aspx?IPMaterialtype=2','',700,700,KodeBarang);
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
			    
			    var modelCode = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[2];
			    modelCode.innerHTML = tempParam[3];
			    
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
				    WtxtEditHargaBarang.value = replace(hargaBrg,',','.');
				}

			    partName.innerHTML = tempParam[1];
			    
			    var modelCode = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[2];
			    modelCode.innerHTML = tempParam[3];
			}
		}	
		
        function NumOnlyBlurWithOnGridTxtCustom(param1, addKey)
	    {
		    var key = document.getElementById(param1.id).value;
		    var newValue = "";
		    for (i=0;i<key.length;i++)	
		    {
			    if ((key.charCodeAt(i) >=48 && key.charCodeAt(i)<=57) || (key.charCodeAt(i) == 0))
			    {
				    newValue = newValue + key.charAt(i);
			    }	
			    else
			    {
				    if (isAccepted(key.charCodeAt(i),addKey))
				    {
					    newValue = newValue + key.charAt(i);
				    }
			    }
		    }			
		    document.getElementById(param1.id).value = newValue;
    		
		    var indek = GetCurrentInputIndex();
		    var dtgIndentPart = document.getElementById("dtgIPDetail");
		    var txtqty = dtgIPDetail.rows[indek].getElementsByTagName("INPUT")[1].value;
		    var txtprice = dtgIPDetail.rows[indek].getElementsByTagName("INPUT")[2].value;
		    if (txtqty == '') txtqty = '0';
		    if (txtprice == '') txtprice = '0';
		    txtqty = replace(txtqty, '.' ,'');
		    txtprice = replace(txtprice, '.', '');
		    var qty = parseFloat(txtqty);
		    var price = parseFloat(txtprice);
		    var total = qty * price;
		    var lblJumlah = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[3];
    		
		    if (lblJumlah.innerText != "")
		    {
		        lblJumlah = dtgIPDetail.rows[indek].getElementsByTagName("SPAN")[4];
		    }
            lblJumlah.innerText = numberFormat(parseFloat(total));
	    }
	
	    function numberFormat(nStr,prefix)
		{						
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

		function PrintForm()
		{
			var divGrid = document.getElementById("divGrid");
			divGrid.style.overflow='visible';
			window.print();
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				divGrid.style.overflow='auto';
			}
		}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblTitle" Runat="server" Text="INDENT PART EQUIPMENT - Permintaan Estimasi Indent Part Equipment"></asp:label></td>
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
											<td><asp:textbox id="txtPONumber" runat="server" size="22" ReadOnly="True" BackColor="#efefef">[Dibuat oleh sistem]</asp:textbox></td>
											<td><cc1:inticalendar id="icOrderDate" runat="server" Enabled="False"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><STRONG>Total Amount</STRONG></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblTotalAmount" runat="server">Rp. 0</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblUploadlbl" runat="server" Visible="False">Lokasi File</asp:label></TD>
								<TD width="1%"><asp:label id="lblUploadDotlbl" runat="server" Visible="False">:</asp:label></TD>
								<TD width="70%"><asp:panel id="pnlUpload" runat="server" Visible="False"><INPUT id="DataFile" type="file" size="40" name="File1" runat="server">
										<asp:regularexpressionvalidator id="RegUpload" runat="server" ErrorMessage="Invalid" ControlToValidate="DataFile"
											ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:regularexpressionvalidator>
										<asp:button id="btnUpload" runat="server" Text="Upload" CausesValidation="False" Width="70px"></asp:button>
									</asp:panel></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="30%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div id="divGrid" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 280px" DESIGNTIMEDRAGDROP="245">
										<table width="100%">
											<tr>
												<td><asp:datagrid id="dtgIPDetail" runat="server" BackColor="#CDCDCD" Width="100%" BorderWidth="1px"
														BorderColor="Black" CellPadding="3" CellSpacing="0" AllowSorting="True" AutoGenerateColumns="False"
														ShowFooter="True">
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
																		runat="server" width="60"></asp:TextBox>
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
																<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
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
																<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
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
															<asp:TemplateColumn SortExpression="EstimationUnit" HeaderText="Unit">
																<HeaderStyle HorizontalAlign="Right" Width="7%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="lblEstimationUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EstimationUnit") %>' CssClass="textRight">
																	</asp:Label>
																</ItemTemplate>
																<FooterStyle HorizontalAlign="Right"></FooterStyle>
																<FooterTemplate>
																	<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFQTY" tabIndex="20" runat="server"
																		size="5" CssClass="textRight" MaxLength="6" onblur="NumOnlyBlurWithOnGridTxtCustom(this,'');"></asp:TextBox>
																</FooterTemplate>
																<EditItemTemplate>
																	<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtEQTY tabIndex=30 runat="server" size="5" Text='<%# DataBinder.Eval(Container, "DataItem.EstimationUnit") %>' CssClass="textRight" MaxLength="6" onblur="NumOnlyBlurWithOnGridTxtCustom(this,'');" >
																	</asp:TextBox>
																	<asp:Label ID="lblEQTY" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EstimationUnit") %>'>
																	</asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Harga">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblPrice" Runat="server" style="text-align:right;" Width="100%"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox ID="txtEditPrice" Runat="server" MaxLength="12" onkeypress="return NumericOnlyWith(event,'');"
																		onblur="NumOnlyBlurWithOnGridTxt(this,'');" Width="70px" style="text-align:right;"></asp:TextBox>
																	<asp:Label ID="lblEditPrice" Runat="server"></asp:Label>
																	<asp:TextBox ID="txtEditPriceTemp" MaxLength="12" style="display:none;text-align:right" Runat="server"
																		onkeypress="return NumericOnlyWith(event,'');" Width="70px" onblur="NumOnlyBlurWithOnGridTxt(this,'');"></asp:TextBox>
																</EditItemTemplate>
																<FooterTemplate>
																	<asp:TextBox id="txtAddPrice" Runat="server" Width="70px" MaxLength="12" onkeypress="return NumericOnlyWith(event,'.');"
																		onblur="NumOnlyBlurWithOnGridTxt(this,'.');" style="text-align:right"></asp:TextBox>
																</FooterTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Jumlah">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<FooterStyle HorizontalAlign="Right" Width="10%"></FooterStyle>
																<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
																<EditItemTemplate>
																	<asp:Label Runat="server" ID="lblEJumlah"></asp:Label>
																</EditItemTemplate>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblJumlah"></asp:Label>
																</ItemTemplate>
																<FooterTemplate>
																	<asp:Label Runat="server" ID="lblFJumlah"></asp:Label>
																</FooterTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="aksi">
																<HeaderStyle HorizontalAlign="Center" Width="8%" CssClass="titleTableParts"></HeaderStyle>
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
															<asp:TemplateColumn Visible="False" HeaderText="Pesan">
																<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts" Width="17%"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblErrorMsgExcel" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:datagrid></td>
											</tr>
										</table>
									</div>
									<div style="OVERFLOW: auto">
										<asp:button class="hideButtonOnPrint" id="btnNew" tabIndex="60" runat="server" Text="Baru" Width="50px"
											CausesValidation="False"></asp:button>
										<asp:button class="hideButtonOnPrint" id="btnSave" tabIndex="50" runat="server" Text="Simpan"
											CausesValidation="False"></asp:button>
										<asp:button class="hideButtonOnPrint" id="btnValidasi" tabIndex="60" runat="server" Text="Kirim"
											Width="50px" CausesValidation="False"></asp:button>
										<asp:button class="hideButtonOnPrint" id="btnDelete" runat="server" Text="Batal" CausesValidation="False"></asp:button>
										<asp:button class="hideButtonOnPrint" id="btnCancel" runat="server" Text="Kembali"></asp:button>
										<INPUT class="hideButtonOnPrint" id="btnCetak" style="WIDTH: 48px; HEIGHT: 21px" onclick="PrintForm();"
											type="button" value="Cetak" name="btnCetak" runat="server"> <INPUT class="hideButtonOnPrint" id="btnClose" style="WIDTH: 48px; HEIGHT: 21px" onclick="window.close();"
											type="button" value="Tutup" name="btnClose" runat="server"> <INPUT id="hdnValNew" type="hidden" value="0" runat="server" NAME="hdnValNew">
										<br>
										<asp:label id="lblError" runat="server" Width="624px" ForeColor="Red" EnableViewState="False"></asp:label>
										<asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary>
									</div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

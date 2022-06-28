<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPoEstimationEquip.aspx.vb" Inherits="FrmPoEstimationEquip" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPoEstimationEquip</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/printstyle.css" media="print">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
        
        var iseditGlobal;

		function PONOSelection(POSelection)
		{
			var txtNoPO= document.getElementById("txtNoPO");
			txtNoPO.value = POSelection;				
		}
        
   		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
					elm.checked = checkVal
					}
				}
			}
		}

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
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage"><asp:label id="lblTitle" Runat="server" Text="INDENT PART EQUIPMENT - Pengajuan Indent Part Equipment"></asp:label></td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 712px" width="712"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
								<TD width="70%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712"><asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
								<TD width="70%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblNomorTanggalPO" runat="server">Nomor / Tanggal Pengajuan</asp:label></TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 712px" width="712">
									<table border="0" cellSpacing="0" cellPadding="0">
										<tr>
											<td><asp:textbox id="txtPONumber" runat="server" size="22" ReadOnly="True" BackColor="#efefef">[Dibuat oleh sistem]</asp:textbox></td>
											<td><cc1:inticalendar id="icOrderDate" runat="server" Enabled="False"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD width="70%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712"><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD width="70%">Keterangan:</TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><STRONG>Nomor Estimasi</STRONG></TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712"><asp:textbox onblur="omitSomeCharacter('txtNoPO','<>?*%$')" id="txtNoPO" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server" Width="160px"></asp:textbox><asp:label id="lblPopUpPONo" runat="server">                                            
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><br />
                                        <asp:RequiredFieldValidator ID="RequiredValidatorTxtNoPo" Display="Dynamic" runat="server" ControlToValidate="txtNoPO" ErrorMessage="No Estimasi Belum di isi"></asp:RequiredFieldValidator>							            
								</TD>
								<TD width="70%"><IMG border="0" alt="Merah" src="../images/red.gif"> &nbsp;: Item 
									sudah melebihi&nbsp;
									<asp:label id="lblnDay" runat="server">14</asp:label>hari dari tanggal 
									konfirmasi .</TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Tanggal Konfirmasi Harga</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712"><cc1:inticalendar id="icConfirmDate" runat="server"></cc1:inticalendar></TD>
								<TD width="70%"><IMG border="0" alt="Kuning" src="../images/yellow.gif">&nbsp;: 
									Item sudah konfirmasi harga dan sudah di order.</TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Tipe Pembayaran</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712"><asp:dropdownlist id="ddlPaymentType" runat="server" Width="120px"></asp:dropdownlist></TD>
								<TD width="70%"><IMG border="0" alt="Hijau" src="../images/green.gif">&nbsp;: Item 
									sudah konfirmasi harga dan belum di order.</TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"></TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712"><asp:button id="btnFind" runat="server" Text="Cari" Width="104px"></asp:button></TD>
								<TD width="70%"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px" class="titleField" width="30%">Nilai Tagihan</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 712px; HEIGHT: 17px" width="712"><asp:label id="lblTotalAmount" runat="server" Font-Bold="True">Rp. 0</asp:label></TD>
								<TD style="HEIGHT: 17px" width="70%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">PPN</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712">
									<asp:label id="lblTotalPPN" runat="server" Font-Bold="True">Rp. 0</asp:label></TD>
								<TD width="70%"></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="30%"><STRONG>Total Tagihan </STRONG>
								</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 712px" width="712">
									<asp:label id="lblTotalTagihan" runat="server" Font-Bold="True">Rp. 0</asp:label>&nbsp;&nbsp;
								</TD>
								<TD width="70%"></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHTx: 280px" id="divGrid" DESIGNTIMEDRAGDROP="245">
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
															<asp:TemplateColumn>
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Image id="imgIndikator" runat="server"></asp:Image>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
															<asp:TemplateColumn>
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<HeaderTemplate>
																	<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															                        document.forms[0].chkAllItems.checked)" />
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn ReadOnly="True" HeaderText="No">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Nomor Estimasi">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblEstimationNumber" runat="server"></asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id=lblPartname runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model/Tipe/Warna">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblmodel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Qty" HeaderText="Unit">
																<HeaderStyle HorizontalAlign="Right" Width="7%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="lblEstimationUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>' CssClass="textRight">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtEQTY" tabIndex="30" runat="server" size="5" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>' CssClass="textRight" MaxLength="6" onblur="NumOnlyBlurWithOnGridTxtCustom(this,'');" >
																	</asp:TextBox>
																	<asp:Label ID="lblEQTY" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
																	</asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Remain">
																<HeaderStyle HorizontalAlign="Right" Width="7%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="lblRemain" runat="server" Text='' CssClass="textRight"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:Label ID="Label4" Runat="server" Text=''></asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Harga">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblHarga" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Jumlah">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblJumlah"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:Label Runat="server" ID="lblEditJumlah"></asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Diskon">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblDiscount"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:Label Runat="server" ID="lblEditDiscount"></asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Tagihan">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblTagihan"></asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:Label Runat="server" ID="lblEditTagihan"></asp:Label>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="aksi">
																<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
																		<img src="../images/edit.gif" class="hideLinkButtonOnPrint" border="0" alt="Ubah"></asp:LinkButton>
																	<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
																		<img src="../images/trash.gif" class="hideLinkButtonOnPrint" border="0" alt="Hapus" OnClick="return confirm('Yakin data ini akan dihapus?');"></asp:LinkButton>
																</ItemTemplate>
																<FooterStyle HorizontalAlign="Center"></FooterStyle>
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
									<br>
									<div style="OVERFLOW: auto"><asp:button id="btnNew" class="hideButtonOnPrint" tabIndex="60" runat="server" Text="Baru" Width="50px"
											CausesValidation="False"></asp:button><asp:button id="btnSave" class="hideButtonOnPrint" tabIndex="50" runat="server" Text="Simpan"
											Enabled="False" CausesValidation="False"></asp:button><asp:button id="btnValidasi" class="hideButtonOnPrint" tabIndex="60" runat="server" Text="Kirim"
											Enabled="False" Width="50px" CausesValidation="False"></asp:button><asp:button id="btnDelete" class="hideButtonOnPrint" runat="server" Text="Batal" CausesValidation="False"></asp:button><asp:button id="btnCancel" class="hideButtonOnPrint" runat="server" Text="Kembali"></asp:button><INPUT style="WIDTH: 48px; HEIGHT: 21px" id="btnCetak" class="hideButtonOnPrint" onclick="PrintForm();"
											value="Cetak" type="button" name="btnCetak" runat="server"> <INPUT style="WIDTH: 48px; HEIGHT: 21px" id="btnClose" class="hideButtonOnPrint" onclick="window.close();"
											value="Tutup" type="button" name="btnClose" runat="server"> <INPUT id="hdnValNew" value="0" type="hidden" name="hdnValNew" runat="server">
										<br>
									</div>
								</TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

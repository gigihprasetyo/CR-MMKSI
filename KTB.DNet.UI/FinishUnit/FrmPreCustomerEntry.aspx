<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPreCustomerEntry.aspx.vb" Inherits="FrmPreCustomerEntry" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}

			//function js untuk handle alphanumeric, dengan menghilangkan karakter numeric
			function alphaNumericNonNumeric(event)
			{	
				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
					pressedKey = event.which
				
				if ((pressedKey == 32) || (pressedKey >=97 && pressedKey <=122) || (pressedKey >=65 && pressedKey <=90))
				{
					return true;
				}
				else
				{	
					return false;
				}
			}
			
			function TxtBlurNonNumeric(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;0123456789');
			}
			
			
			// ******************
			function ShowPopUpSAPRegisterSalesman()
			{	
			
				//var txtSapNo = document.getElementById("txtSapNo");
				//showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx','',460,760,SAPRegisterSelection);
				showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=0','',500,760,SAPRegisterSelection);
			}
			
			function SAPRegisterSelection(SelectedSalesman)
			{
				
				//var indek = GetCurrentInputIndex();
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var tempParam = SelectedSalesman.split(';');
				var txtSalesmanID = document.getElementById("txtSalesmanID");
				var txtSalesmanName = document.getElementById("txtSalesmanName");
				
					txtSalesmanName.value = tempParam[1];
					txtSalesmanID.value = tempParam[0];	
					//__doPostBack('__Page', 'searchsalesman');
			}
			
			function ShowPopUpVechileType()
			{	
				showPopUp('../PopUp/PopUpVechileType.aspx?CategoryID=1&IsActive=A','',500,760,VechileTypeSelection);				
			}

			function ShowPopUpCustomer()
			{
				showPopUp('../PopUp/PopUpCustomerName.aspx','',500,760,CustomerSelection);
			}

			function CustomerSelection(SelectedCustomer)
			{
				var indek = GetCurrentInputIndex();
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var tempParam = SelectedCustomer.split(';');
				// input berupa teks box, urutan dikolom
				var txtCustomerName = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[0];
				var txtCustomerCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[1];
				var txtCustomerAddress = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[2];
				// span berupa label
				///var DescArea = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];
				
				if(navigator.appName == "Microsoft Internet Explorer")
					{
					txtCustomerName.innerText = tempParam[1];
					txtCustomerCode.innerText = tempParam[0];
					txtCustomerAddress.innerText = tempParam[2];
					//DescArea.innerHTML = tempParam[1];	
					}
				else
					{
					txtCustomerName.value = tempParam[1];
					//DescArea.value = tempParam[1];
					}
			}
			function GetCurrentInputIndex()
			{
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgSAPCustomer.rows.length; index++)
				{
					inputs = dgSAPCustomer.rows[index].getElementsByTagName("INPUT");
					
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
			
			function VechileTypeSelection(SelectedVechileType)
			{
				
				var indek = GetCurrentInputIndex();
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var tempParam = SelectedVechileType.split(';');
				var CustomerNameValue=tempParam[1];
				var VechileTypeCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[3];
				//alert(replace(tempParam[0],' ',''));
				//alert(indek);
				
				if(navigator.appName == "Microsoft Internet Explorer")
					{											
					   VechileTypeCode.innerText = replace(tempParam[0],' ','');												
					}
				else
					{					
					  VechileTypeCode.value = replace(tempParam[0],' ','');																		
					}
			}
			
			function ShowPPSAP()
			{
				showPopUp('../SparePart/../PopUp/PopUpSAP.aspx?x=Territory','',500,760,SAPSelection);
			}
			
			function SAPSelection(selectedSAP)
			{
				var tempParam= selectedSAP.split(';');
				
				var txtSAPNo = document.getElementById("txtSAPNo");
				var lblDateFrom = document.getElementById("lblDateFrom");
				var lblDateUntil = document.getElementById("lblDateUntil");
				var txtPeriod = document.getElementById("txtPeriod");
						
				txtSAPNo.value= tempParam[0];
				lblDateFrom.innerText =tempParam[1];
				lblDateUntil.innerText =tempParam[2];
				txtPeriod.value= tempParam[1] +';'+ tempParam[2];
				
				
			}
			
			function SetSalesmanCode(selectedSales,mode)
			{
				
				if (selectedSales != '')
				{
					var indek = GetCurrentInputIndex();
					var dgSAPCustomer = document.getElementById("dgSAPCustomer");
					var txtSalesmanCode = document.getElementById("txtSalesmanCode");
					// setting posisi berdasarkan urutan kolom di grid
					var lblSalesmanCode = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];
					lblSalesmanCode.innerText=selectedSales.value;
					txtSalesmanCode.value=selectedSales.value;
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="115%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">Marketing - Input Konsumen Awal</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 21px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 21px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD colSpan="3"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 21px" width="29%"><INPUT id="txtSalesmanCode" type="hidden" runat="server" NAME="txtSalesmanCode"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Nama Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD colSpan="3"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 17px" width="29%"><INPUT id="txtPeriod" type="hidden" name="Hidden1" runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Salesman</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" noWrap><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtSalesmanID" tabIndex="14" runat="server"
										 MaxLength="50" Width="70px"></asp:textbox><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtSalesmanName" tabIndex="14" runat="server"
										  MaxLength="50" Width="130px"></asp:textbox><asp:label id="lblSearchSalesman" Runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label><asp:linkbutton id="lbtnSearchSalesman" style="DISPLAY: none" Runat="server">Dont remove
									</asp:linkbutton>
								<TD class="titleField" style="HEIGHT: 20px"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px">
									<asp:button id="btnSearch" runat="server" Width="65px" Visible="True" Text="Cari"></asp:button>
									<asp:button id="btnBatal" runat="server" Width="75px" Text="Batal" CausesValidation="False"></asp:button>
									<asp:button id="btnNoSales" runat="server" Width="75px" Text="Tanpa Sales" Visible="False"></asp:button>
								</TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD colspan="6" style="HEIGHT: 11px"><asp:Label ID="lblTotalRow" Runat="server" /></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 100%; HEIGHT: 11px" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgSAPCustomer" runat="server" Width="100%" CellPadding="1" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="8"
											AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CustomerName" HeaderText="Nama Konsumen">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCustomerName" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAddCustomerName"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" MaxLength="50" Width="100px" Runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtEditCustomerName"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" MaxLength="50" Width="100px" Runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CustomerAddress" HeaderText="Alamat Konsumen">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCustomerAddress" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAddCustomerAddress"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" MaxLength="100" Runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtEditCustomerAddress"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" MaxLength="100" Runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Phone" HeaderText="Telepon">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPhone" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPhoneF" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"
															MaxLength="20" Width="100px" Runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPhoneE" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"
															MaxLength="20" Width="100px" Runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Sex" HeaderText="Gender">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblGender" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList ID="ddlGenderF" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlGenderE" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AgeSegment" HeaderText="Usia">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblAge" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList ID="ddlAgeF" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlAgeE" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatus" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList ID="ddlAddStatus" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlEditStatus" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="InformationType" HeaderText="Tipe Informasi">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblType" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList ID="ddlTypeF" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlTypeE" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CustomerPurpose" HeaderText="Tujuan Konsumen">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPurpose" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList ID="ddlPurposeF" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlPurposeE" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Tipe Kendaraan">
													<HeaderStyle Width="100px" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblVechileTypeCode" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddVechileTypeCode" Runat="server" MaxLength="100" Width="70px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$')"></asp:TextBox>
														<asp:Label id="lblAddVechileTypeCode" runat="server">
															<img style="cursor:hand" alt="Klik Disini untuk memilih Tipe Kendaraan" src="../images/popup.gif"
																border="0"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditVechileTypeCode" Runat="server" MaxLength="4" Width="70px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
														</asp:Label>
														<asp:Label id="lblEditVechileTypeCode" runat="server">
															<img style="cursor:hand" alt="Klik Disini untuk memilih Tipe Kendaraan" src="../images/popup.gif"
																border="0"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Qty" HeaderText="Qty">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblQty" Runat="server" text='<%# Format(DataBinder.Eval(Container, "DataItem.Qty"),"#,##0") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddQty" Runat="server" Width="50px" onkeypress="return NumericOnlyWith(event,'')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditQty" Runat="server" Width="50px" onkeypress="return NumericOnlyWith(event,'')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" text='<%# Format(DataBinder.Eval(Container, "DataItem.Qty"),"#,##0") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="InformationSource" HeaderText="Sumber Informasi">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSource" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:DropDownList ID="ddlSourceF" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlSourceE" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Aksi">
													<HeaderStyle Width="45px" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
														<asp:LinkButton id="lbtnRegister" tabIndex="50" CommandName="Register" text="Register" Runat="server" CausesValidation="False" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.ID")%>'>
															<img src="../images/icon_customer.gif" border="0" alt="Pendaftaran Konsumen"></asp:LinkButton>
													</ItemTemplate>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" runat="server" CausesValidation="False" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
														<asp:LinkButton id="lbtnCopy" runat="server" CausesValidation="False" CommandName="AddCopy">
															<img src="../images/simpan.gif" border="0" alt="Simpan dan copy"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

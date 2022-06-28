<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSAPCustomerList.aspx.vb" Inherits="FrmSAPCustomerList" smartNavigation="False" %>
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
				omitSomeCharacter(objTxt,'<>?*%$');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$')
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
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
			
			function ShowPopUpSAPRegisterSalesman()
			{	
			
				var txtSapNo = document.getElementById("txtSapNo");
				showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?SAPNumber='+ txtSapNo.value,'',500,760,SAPRegisterSelection);
			}
			
			function SAPRegisterSelection(SelectedSalesman)
			{
				
				var indek = GetCurrentInputIndex();
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var tempParam = SelectedSalesman.split(';');
				// input berupa teks box, urutan dikolom
				var txtName = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[0];
				// span berupa label
				var lblSalesmanCode = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];
				var txtSalesmanCode = document.getElementById("txtSalesmanCode");
				
				if(navigator.appName == "Microsoft Internet Explorer")
					{
					txtName.innerText = tempParam[1];
					lblSalesmanCode.innerHTML = tempParam[0];	
					}
				else
					{
					txtName.value = tempParam[1];
					lblSalesmanCode.value = tempParam[0];
					}
				txtSalesmanCode.value = tempParam[0];
			}
			
			function ShowPopUpVechileType()
			{	
				showPopUp('../PopUp/PopUpVechileType.aspx','',600,600,VechileTypeSelection);
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
				// input berupa teks box, urutan dikolom
				var VechileTypeCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[1];
				// span berupa label
				///var DescArea = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];
				
				if(navigator.appName == "Microsoft Internet Explorer")
					{
					VechileTypeCode.innerText = tempParam[0];
					//DescArea.innerHTML = tempParam[1];	
					}
				else
					{
					VechileTypeCode.value = tempParam[0];
					//DescArea.value = tempParam[1];
					}
			}
			
			function ShowPPSAP()
			{
				//showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?x=Territory','',500,760,SAPSelection);
				showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=0','',500,760,SAPSelection);
			}
			
			function SAPSelection(selectedSAP)
			{
				var tempParam= selectedSAP.split(';');
				
				var txtSalesmanID = document.getElementById("txtSalesmanID");	
				var hidID = document.getElementById("hdnID");			
				var lblNamaSalesman = document.getElementById("lblNamaSalesman");
				var hidName = document.getElementById("hdnName");				
						
				txtSalesmanID.value= tempParam[0];
				lblNamaSalesman.innerText =tempParam[1];			
				
			}
			function GetCurrentInputIndex()
			{
				var dtgArea = document.getElementById("dgSAPCustomer");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgArea.rows.length; index++)
				{
					inputs = dtgArea.rows[index].getElementsByTagName("INPUT");
					
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
			/*
			function DealerSelection(selectedDealer)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				txtDealerCodeSelection.value =selectedDealer;
			}
			*/
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SAP - Daftar Konsumen Prospek</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" style="TEXT-ALIGN: left" cellSpacing="1" cellPadding="2" width="100%"
							border="0">
							<TR>
								<TD class="titleField">Dealer ID</TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Salesman ID</TD>
								<TD style="HEIGHT: 26px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 261px; HEIGHT: 26px" width="261"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSalesmanID" onblur="omitSomeCharacter('txtSalesmanID','<>?*%$;')"
										runat="server" ToolTip="Dealer Search 1" MaxLength="10" Width="130px"></asp:textbox><asp:label id="lblPopUpSalesman" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label><INPUT id="hdnID" type="hidden" runat="server"></TD>
								<TD class="titleField" style="WIDTH: 47px; HEIGHT: 26px" width="47"><STRONG></STRONG></TD>
								<TD style="WIDTH: 13px; HEIGHT: 26px" width="13"></TD>
								<TD style="HEIGHT: 26px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Nama Salesman</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 261px; HEIGHT: 17px" width="261"><asp:label id="lblNamaSalesman" runat="server"></asp:label><INPUT id="hdnName" type="hidden" runat="server"></TD>
								<TD class="titleField" style="WIDTH: 47px; HEIGHT: 17px" width="47"><STRONG></STRONG></TD>
								<TD style="WIDTH: 13px; HEIGHT: 17px" width="13"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Tanggal Prospek</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="WIDTH: 261px; HEIGHT: 20px" noWrap width="261">
									<TABLE cellSpacing="0" cellPadding="0">
										<tr>
											<td><cc1:inticalendar id="icPaymentDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icPaymentDateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</TABLE>
								</TD>
								<TD class="titleField" style="WIDTH: 47px; HEIGHT: 20px" width="47"></TD>
								<TD style="WIDTH: 13px; HEIGHT: 20px" width="13"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px">Status</TD>
								<TD style="HEIGHT: 19px">:</TD>
								<TD style="WIDTH: 261px; HEIGHT: 19px"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" style="WIDTH: 47px; HEIGHT: 19px"></TD>
								<TD style="WIDTH: 13px; HEIGHT: 19px"></TD>
								<TD style="HEIGHT: 19px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 261px; HEIGHT: 11px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button><asp:button id="btnDownload" runat="server" Width="60px" Text="Download" CausesValidation="False"></asp:button></TD>
								<TD class="titleField" style="WIDTH: 47px; HEIGHT: 11px"></TD>
								<TD style="WIDTH: 13px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 261px; HEIGHT: 11px"></TD>
								<TD class="titleField" style="WIDTH: 47px; HEIGHT: 11px"></TD>
								<TD style="WIDTH: 13px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgSAPCustomer" runat="server" Width="100%" AllowPaging="True" PageSize="25"
											AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'/>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNomor" Runat="server" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CustomerName" HeaderText="Nama Konsumen">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblCustomerName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="CustomerCode" HeaderText="Kode Konsumen">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblCustomerCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatus" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Qty" HeaderText="Kuantitas">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblQty" Runat="server" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Kode Kendaraan">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblVechileTypeCode" Runat="server" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ProspectDate" HeaderText="Tanggal Prospect">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblProspectDate" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

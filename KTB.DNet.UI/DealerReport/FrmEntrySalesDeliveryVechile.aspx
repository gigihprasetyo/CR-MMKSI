<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntrySalesDeliveryVechile.aspx.vb" Inherits="FrmEntrySalesDeliveryVechile" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">

		function ShowPPTujuanSelection()
		{
			var optDealer = document.getElementById('optDealer');
			var optCust = document.getElementById('optCustomer');
			if (optDealer.checked)
			{
				showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=true','',500,860,TujuanSelection);
			}
			else if (optCust.checked)
			{
				showPopUp('../PopUp/PopUpCustomerSelectionOne.aspx?FilterLoginDealer=True','',500,760,TujuanSelection);
			}
		}
		
		function TujuanSelection(selectedTujuan)
		{

			var txtTujuanSelection = document.getElementById('txtTujuan');
			var txtTujuanNama = document.getElementById('lblNama');
			var txtTujuanAlamat = document.getElementById('lblAlamat');
			var txtTujuanKota = document.getElementById('lblKota');

			var arrValue = selectedTujuan.split(';');
			txtTujuanSelection.value = arrValue[0];
			txtTujuanNama.innerHTML = arrValue[1];
			txtTujuanAlamat.innerHTML = arrValue[3];
			txtTujuanKota.innerHTML = arrValue[7];
			if(navigator.appName == 'Microsoft Internet Explorer')
			{
				txtTujuanSelection.focus();
				txtTujuanSelection.blur();
			}
			/*else
			{
				txtTujuanSelection.onchange();
			}*/
		}
		
		function ClearInfo()
		{
			var txthidden = document.getElementById('LastOptState');
			var optDealer = document.getElementById('optDealer');
			var optCust = document.getElementById('optCustomer');

			var txtTujuanSelection = document.getElementById('txtTujuan');
			var txtTujuanNama = document.getElementById('lblNama');
			var txtTujuanAlamat = document.getElementById('lblAlamat');
			var txtTujuanKota = document.getElementById('lblKota');
			
			if (optDealer.checked)
			{
				if(txthidden.value == "Dealer")
				{
					return;
				}
			}
			else if (optCust.checked)
			{
				if(txthidden.value == "Cust")
				{
					return;
				}
			}
			
			txtTujuanSelection.value = "";
			txtTujuanNama.innerHTML = "";
			txtTujuanAlamat.innerHTML = "";
			txtTujuanKota.innerHTML = "";

			if (optDealer.checked)
			{
				txthidden.value = "Dealer";
			}
			else if (optCust.checked)
			{
				txthidden.value = "Cust";
			}
		}
		
		function ShowSalesmanSelection()
		{
			showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit','',500,760,SalemanSelection);
			
		}
		function SalemanSelection(selectedSales)
		{
			var temp = selectedSales.split(";")
			var txtSalesman = document.getElementById('txtSalesman');
			var txtSalesNama = document.getElementById('lblNamaSales');
			var txtSalesLevel = document.getElementById('lblLevel');
			var txtSalesJabatan = document.getElementById('lblJabatan');
			txtSalesman.value = temp[0];
			txtSalesNama.innerHTML = temp[1];
			txtSalesLevel.innerHTML = temp[4];
			txtSalesJabatan.innerHTML = temp[3];
			
		}
		
		function GetCurrentInputIndex(GridName)
			{
			var dtgDamageCode = document.getElementById(GridName);
			var currentRow;
			var index = 0;
			var inputs;
			var indexInput;
			
			for (index = 0; index < dtgDamageCode.rows.length; index++)
			{
				inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");
				
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

		function GetSelectedChassisCode(selectedCode)
		{
			var indek = GetCurrentInputIndex("dgSalesDelieveryVechile");
			var dtgDamageCode = document.getElementById("dgSalesDelieveryVechile");
			var NoRangka = dtgDamageCode. rows[indek].getElementsByTagName("INPUT")[0];

			NoRangka.value = selectedCode;
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">DEALER REPORT - Pengiriman Kendaraan</td>
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
								<TD class="titleField" style="HEIGHT: 10px" width="18%">No Reg Pengiriman</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblNoRegDelivery" runat="server" Width="128px"></asp:label></TD>
								<TD style="HEIGHT: 10px" width="18%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="375"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD colspan="4"><asp:label id="lblDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Tipe</TD>
								<TD style="HEIGHT: 26px" width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 26px" width="375"><asp:label id="lblTipe" Runat="server"></asp:label><asp:radiobutton id="optDealer" onclick="ClearInfo();" Runat="server" GroupName="Tipe" Text="Dealer"></asp:radiobutton><asp:radiobutton id="optCustomer" onclick="ClearInfo();" Runat="server" GroupName="Tipe" Text="Customer"></asp:radiobutton><input style="WIDTH: 4px; HEIGHT: 20px" type="hidden" size="1" name="LastOptState">
								</TD>
								<TD style="HEIGHT: 10px" width="18%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="375"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Tujuan</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblTujuan" Runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtTujuan" onblur="omitSomeCharacter('txtTujuan','<>?*%$')"
										runat="server" MaxLength="10"></asp:textbox><asp:label id="lblPopUp" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Salesman</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblSalesman" Runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtSalesman" onblur="omitSomeCharacter('txtTujuan','<>?*%$')"
										runat="server" MaxLength="10"></asp:textbox><asp:label id="lblShowSalesman" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Nama</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblNama" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Nama</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblNamaSales" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Alamat</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblAlamat" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Level</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblLevel" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kota</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblKota" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Posisi Jabatan</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblJabatan" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 3px" width="24%">Tgl Pengiriman</TD>
								<TD style="HEIGHT: 3px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 3px" width="375"><asp:label id="lblTglPengiriman" Runat="server"></asp:label><cc1:inticalendar id="icTglDelivery" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
								<TD style="HEIGHT: 10px" width="18%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="375"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">No Ref D/O</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:label id="lblRefDO" Runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtRefDO" onblur="omitSomeCharacter('txtRefDO','<>?*%$')"
										runat="server" Width="128px"></asp:textbox></TD>
								<TD style="HEIGHT: 10px" width="18%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="375"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 210px"><asp:datagrid id="dgSalesDelieveryVechile" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
											BorderColor="#CDCDCD" BackColor="White" AutoGenerateColumns="False" ShowFooter="True">
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
											<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No.Rangka">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblChassisNumber" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber")  %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtChassisNumberFooter" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Runat="server"></asp:TextBox>
														<asp:Label id="lblSearchChassisFooter" runat="server" TabIndex="0">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Tipe/Warna">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblKodeTipeWarna" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.VechileColor.VechileType.VechileTypeCode") + DataBinder.Eval(Container, "DataItem.ChassisMaster.VechileColor.ColorCode")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nama Tipe/Warna">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNamaTipeWarna" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.VechileColor.VechileType.Description") + " " + DataBinder.Eval(Container, "DataItem.ChassisMaster.VechileColor.ColorIndName")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No Mesin">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoMesin" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.EngineNumber")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No Seri">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoSeri" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.SerialNumber")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterTemplate>
														<asp:LinkButton id="lnkbtnAdd" runat="server" Width="20px" Text="Tambah" CausesValidation="False"
															CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
														</asp:LinkButton>
													</FooterTemplate>
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
					<TD style="HEIGHT: 8px"><asp:button id="btnSimpan" Width="80" Runat="server" Text="Simpan"></asp:button>
						<asp:button id="btnAbort" Width="100" Runat="server" Text="Batal Pengiriman"></asp:button>
						<asp:button id="btnCancel" Width="80" Runat="server" Text="Kembali"></asp:button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

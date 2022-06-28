<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmEntryPartIncidental.aspx.vb" Inherits="frmEntryPartIncidental" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Entry Permintaan Khusus</title>
		<%--<meta http-equiv="refresh" content="0;URL=../ClossingMessage.htm" />--%>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPKodeBarangSelection()
		{
		showPopUp('../PopUp/PopUpSparePart.aspx','',700,700,KodeBarang);
		}
			function GetCurrentInputIndex()
				{
				var dtgPartIncidental = document.getElementById("dtgPartIncidental");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgPartIncidental.rows.length; index++)
				{
					inputs = dtgPartIncidental.rows[index].getElementsByTagName("INPUT");
					
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
				var dtgPesananKendaraan = document.getElementById("dtgPartIncidental");
				var tempParam = selectedCode.split(';');
				var KodeBarang = dtgPartIncidental.rows[indek].getElementsByTagName("INPUT")[0];
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				KodeBarang.innerText = tempParam[0];				
				}
				else
				{
				KodeBarang.value = tempParam[0];
				}
			}

function alphaNumeric(event)
{	
	if(navigator.appName == "Microsoft Internet Explorer")	
		pressedKey = event.keyCode;
	else
		pressedKey = event.which
	
	if ( (pressedKey >=48 && pressedKey <=57) || (pressedKey >=65 && pressedKey <=90) || (pressedKey >=97 && pressedKey <=122) || pressedKey==8)
	{
		return true;
	}
	else
	{	
		return false;
	}
}
	
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PERMINTAAN KHUSUS&nbsp;- Pengajuan Permintaan Khusus</td>
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
								<TD class="titleField" width="24%"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:label id="lblKodeDealerValue" runat="server"></asp:label></TD>
								<TD width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblNamaDealerValue" runat="server"></asp:label></TD>
								<TD width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblNomorPermintaan" runat="server">Nomor Permintaan</asp:label></TD>
								<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblNomorPermintaanValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblNoSurat" runat="server">Nomor Surat</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="alphaNumericPlusSpaceUniv(event)" id="txtNomorSurat" onblur="alphaNumericPlusSpaceBlur(txtNomorSurat)"
										runat="server" MaxLength="20" ReadOnly="True" Width="144px"> Otomatis</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTanggalInput" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><cc1:inticalendar id="IntiCalendar1" runat="server" Enabled="False"></cc1:inticalendar></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblNomorPolisi" runat="server">Nomor Polisi</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="alphaNumericPlusSpaceUniv(event)" id="txtNomorPolisi" onblur="alphaNumericPlusSpaceBlur(txtNomorPolisi)"
										runat="server" MaxLength="15"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Nomor Polisi harus diisi"
										ControlToValidate="txtNomorPolisi">*</asp:requiredfieldvalidator></TD>
								<TD class="titleField"><asp:label id="lblWO" runat="server">W/O</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="alphaNumericPlusSpaceUniv(event)" id="txtWO" onblur="alphaNumericPlusSpaceBlur(txtWO)"
										runat="server" MaxLength="20"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="W/O Harus diisi" ControlToValidate="txtWO">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 4px"><asp:label id="lblNoRangka" runat="server">Nomor Rangka</asp:label></TD>
								<TD style="HEIGHT: 4px"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 4px"><asp:textbox onkeypress="return alphaNumeric(event)" id="txtNoRangka" onblur="alphaNumericPlusSpaceBlur(txtNomorPolisi)"
										runat="server" MaxLength="20"></asp:textbox><asp:linkbutton id="lnkbtnCheckChassis" CausesValidation="False" ToolTip="Validate Chassis" Runat="server">
										<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0">
									</asp:linkbutton>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ControlToValidate="txtNoRangka" ErrorMessage="Nomor Rangka harus diisi">*</asp:requiredfieldvalidator></TD>
								<TD class="titleField" style="HEIGHT: 4px"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 4px"><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 4px">
									<asp:Label id="lblPengajuan" runat="server" Visible="False">Pemesanan</asp:Label>
									<TABLE id="Table3" style="WIDTH: 233px; HEIGHT: 8px" height="8" cellSpacing="0" cellPadding="0"
										width="233" border="0">
										<TR>
											<TD>
												<asp:radiobuttonlist id="rblStatus" runat="server" AutoPostBack="True" Width="72px" Height="16px" Enabled="False"></asp:radiobuttonlist></TD>
											<TD vAlign="bottom">
												<asp:textbox id="txtStatusLain" onblur="alphaNumericPlusSpaceBlur(txtStatusLain)"
													runat="server" MaxLength="10" Width="106px" ReadOnly="True"></asp:textbox>
												<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtStatusLain" ErrorMessage="Status harus diisi"
													Enabled="False">*</asp:requiredfieldvalidator></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 5px"><asp:label id="lblTipe" runat="server">Tipe</asp:label></TD>
								<TD style="HEIGHT: 5px"><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblVehicleType" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 5px">
									<asp:label id="lblPIC" runat="server">PIC</asp:label></TD>
								<TD style="HEIGHT: 5px">:</TD>
								<TD style="HEIGHT: 5px"><asp:textbox id="txtPIC" runat="server" MaxLength="20" Width="140px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 1px"><asp:label id="Label12" runat="server">Tahun Produksi</asp:label></TD>
								<TD style="HEIGHT: 1px"><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 1px"><asp:label id="lblTahunProduksi" runat="server"></asp:label>
									<asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtYearProduction" runat="server"
										MaxLength="4"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" ControlToValidate="txtYearProduction"
										ErrorMessage="Tahun Produksi harus diisi">*</asp:requiredfieldvalidator></TD>
								<TD class="titleField" style="HEIGHT: 1px"><asp:label id="lblTelp" runat="server">Telp</asp:label></TD>
								<TD style="HEIGHT: 1px"><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 1px"><asp:textbox id="txtTelp" onkeypress="return numericOnlyUniv(event)" runat="server" MaxLength="20"
										Width="140px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD colSpan="6"><asp:datagrid id="dtgPartIncidental" runat="server" Width="100%" CellSpacing="1" Font-Size="Small"
										OnUpdateCommand="dtgPartIncidental_Update" OnCancelCommand="dtgPartIncidental_Cancel" OnEditCommand="dtgPartIncidental_Edit"
										OnItemCommand="dtgPartIncidental_ItemCommand" ShowFooter="True" CellPadding="3" BorderWidth="0px" BorderStyle="None"
										BorderColor="#E0E0E0" AutoGenerateColumns="False" OnItemDataBound="dtgPartIncidental_ItemDataBound" BackColor="#E0E0E0">
										<FooterStyle Font-Size="Small" ForeColor="#003399" BackColor="White"></FooterStyle>
										<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
										<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle Font-Size="Small"></FooterStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nomor Barang">
												<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=lblViewNomorBarang runat="server" NAME="lblViewNomorBarang" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartMaster.PartNumber" )  %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Left"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox id="txtFooterNomorBarang" runat="server" Width="120px" MaxLength="18" BackColor="White"></asp:TextBox>
													<asp:Label id="lblFooterNomorBarang" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id=txtEditNomorBarang runat="server" Width="120px" MaxLength="18" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartMaster.PartNumber" ) %>'>
													</asp:TextBox>
													<asp:Label id="lblEditNomorBarang" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Nama Barang">
												<HeaderStyle Width="18%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Model">
												<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="No. Chassis" Visible="False">
												<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=Label4 runat="server" NAME="lblViewChassis" Text='<%# DataBinder.Eval(Container.DataItem, "ChassisNumber", "{0:#,###}" ) %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event)" id="txtFooterChassis" runat="server"
														MaxLength="17" Width="105px" CssClass="textLeft"></asp:TextBox>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event)" id=txtEditChassis runat="server" Width="105px" MaxLength="17" Text='<%# DataBinder.Eval(Container.DataItem, "ChassisNumber") %>' CssClass="textLeft">
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Tahun Perakitan" Visible="False">
												<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label5" runat="server" NAME="lblViewPerakitan" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyYear", "{0:#,###}" ) %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Left"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFooterPerakitan" runat="server"
														MaxLength="4" Width="55px" CssClass="textLeft"></asp:TextBox>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtEditPerakitan" runat="server" Width="55px" MaxLength="4" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyYear") %>' CssClass="textLeft">
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Jumlah">
												<HeaderStyle Width="14%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=lblViewUnit runat="server" NAME="lblViewUnit" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity", "{0:#,###}" ) %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFooterUnit" runat="server" MaxLength="6"
														Width="80px" CssClass="textRight"></asp:TextBox>
													<asp:RangeValidator id="RangeValidator2" runat="server" ControlToValidate="txtFooterUnit" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
														MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtEditUnit runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>' CssClass="textRight">
													</asp:TextBox>
													<asp:RangeValidator id="RangeValidator1" runat="server" ControlToValidate="txtEditUnit" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
														MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
												CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
												EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
												<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:EditCommandColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
														<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<FooterStyle HorizontalAlign="Center"></FooterStyle>
												<FooterTemplate>
													<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
														<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
												</FooterTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><br>
									<asp:label id="lblError" runat="server" Width="624px" EnableViewState="False" ForeColor="Red"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
							</TR>
							<TR>
								<TD colSpan="6"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnDelete" runat="server" Text="Hapus" CausesValidation="False"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali" CausesValidation="False" Visible="False"></asp:button></TD>
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

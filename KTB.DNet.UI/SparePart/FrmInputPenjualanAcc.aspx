<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmInputPenjualanAcc.aspx.vb" Inherits="FrmInputPenjualanAcc" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Input Penjualan Accessories</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			var SelectedCode;
			var CurrentTxtID;
			var CurrentBtnID;
			var CurrentLblID;
			var CurrentMdlID;
			function ShowPPKodeBarangSelection(btnID,txtID,lblID,MdlID){
				var EnumAcc = document.getElementById("txtEnumAccessories").value;
				CurrentTxtID = txtID;		
				CurrentBtnID = btnID;		
				CurrentLblID = lblID;
				CurrentMdlID = MdlID;
				//showPopUp('../PopUp/PopUpSparePart.aspx?IPMaterialtype='+EnumAcc,'',700,700,KodeBarang);
				showPopUp('../PopUp/PopUpSparePart.aspx?IsAccessories=1','',700,700,KodeBarang);
				
			}
						
			function KodeBarang(selectedCode)
			{
				var tempParam = selectedCode.split(';');
				
				document.getElementById(CurrentTxtID).value=tempParam[0];
				document.getElementById(CurrentLblID).innerHTML=tempParam[1];
				document.getElementById(CurrentMdlID).innerHTML=tempParam[4];
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
		<script language="javascript">
		
		
			function Konfirmasi(Text)
			{
				var txtComment = document.getElementById("txtComment");
				txtComment.value=Text;
			}		
			
			function ShowPPKonfirmasi()
			{
			var txtComment = document.getElementById("txtComment");
			var enter = 13;
			var feedline = 10;
			var newstring = replace(txtComment.value, String.fromCharCode(enter), '@');
			newstring = replace(newstring, String.fromCharCode(feedline), '*');
			var opentag = 60;
			newstring = replace(newstring, String.fromCharCode(opentag), '|');						
			showPopUp('../SparePart/frmComment.aspx?text='+newstring+' ','',400,400,Konfirmasi)
			}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox Runat="server" ID="txtEnumAccessories" Text="" style="DISPLAY:none"></asp:TextBox>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						PENJUALAN&nbsp;- Input Penjualan Accessories</td>
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
								<TD width="20%"><STRONG>Nama Customer</STRONG></TD>
								<TD width="1%">:</TD>
								<TD width="29%">
									<asp:TextBox Runat="server" ID="txtNamaCustomer" Text="" Width="140px" />
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="txtNamaCustomer"
										ErrorMessage="Nama Customer harus diisi">*</asp:requiredfieldvalidator>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label style="Z-INDEX: 0" id="Label8" runat="server">Kategori Input</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:DropDownList id="ddlKategori" runat="server" Width="128px"></asp:DropDownList>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Kategori Input Harus Dipilih"
										ControlToValidate="ddlKategori">*</asp:requiredfieldvalidator>
								</TD>
								<TD class="titleField">
									<asp:label style="Z-INDEX: 0" id="lblNoSurat" runat="server">Telepon</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:textbox style="Z-INDEX: 0" id="txtTelp" onkeypress="return numericOnlyUniv(event)" runat="server"
										Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">
									<asp:label style="Z-INDEX: 0" id="lblNomorLaporan" runat="server">Nomor Laporan</asp:label></TD>
								<TD style="HEIGHT: 26px">
									<asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 26px">
									<asp:label id="lblReportNum" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 26px" class="titleField">
									<asp:label style="Z-INDEX: 0" id="lblWO" runat="server">Comment</asp:label></TD>
								<TD style="HEIGHT: 26px">:</TD>
								<TD rowspan="2">
									<table cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td>
												<asp:TextBox style="Z-INDEX: 0" id="txtComment" TextMode="MultiLine" Rows="3" Columns="25" Text=""
													Runat="server"></asp:TextBox>
											</td>
											<td>
												<asp:label id="lblSearchKonfirmasi" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:label style="Z-INDEX: 0" id="lblNomorPermintaan" runat="server">Nomor Referensi</asp:label></TD>
								<TD>
									<asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="alphaNumericPlusSpaceUniv(event)" id="txtRefNum" onblur="alphaNumericPlusSpaceBlur(txtRefNum)"
										runat="server" MaxLength="15"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Nomor Referensi harus diisi"
										ControlToValidate="txtRefNum">*</asp:requiredfieldvalidator></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<!--
								 <TD> </TD>
								-->
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTanggalInput" runat="server">Tanggal Penjualan</asp:label></TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><cc1:inticalendar id="calTanggal" runat="server" Enabled="True"></cc1:inticalendar></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 4px"><asp:label id="lblNoRangka" runat="server">Nomor Rangka</asp:label></TD>
								<TD style="HEIGHT: 4px"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 4px">
									<table cellspacing="0" cellpadding="0">
										<tr>
											<td>
												<asp:textbox onkeypress="return alphaNumeric(event)" id="txtNoRangka" onblur="alphaNumericPlusSpaceBlur(txtNomorPolisi)"
													runat="server" MaxLength="20"></asp:textbox>
											</td>
											<td style="DISPLAY:none">
												<asp:linkbutton id="lnkbtnCheckChassis" CausesValidation="False" ToolTip="Validate Chassis" Runat="server">
													<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0">
												</asp:linkbutton>
											</td>
											<td style="DISPLAYx:none">
												<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ControlToValidate="txtNoRangka" ErrorMessage="Nomor Rangka harus diisi">*</asp:requiredfieldvalidator>
											</td>
										</tr>
									</table>
								</TD>
								<TD class="titleField" style="HEIGHT: 4px"></TD>
								<TD style="HEIGHT: 4px"></TD>
								<TD style="HEIGHT: 4px">
								</TD>
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
								<TD colSpan="6"><asp:datagrid id="dtgMain" runat="server" Width="100%" CellSpacing="1" Font-Size="Small" ShowFooter="True"
										CellPadding="3" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
										BackColor="#E0E0E0">
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
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nomor Barang">
												<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<table cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<asp:TextBox id="txtNomorBarang" ReadOnly="True" runat="server" Width="120px" MaxLength="18" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartMaster.PartNumber" )  %>'>
																</asp:TextBox>
															</td>
															<td>
																<asp:Button Runat="server" ID="btnEditSparePart" style="display:none;" CommandName="UpdateSparePart"></asp:Button>
																<asp:Label id="lblEditNomorBarang" runat="server">
																	<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
																</asp:Label>
															</td>
															<td></td>
														</tr>
													</table>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Left"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox id="txtFooterNomorBarang" runat="server" Width="120px" MaxLength="18" BackColor="White"></asp:TextBox>
													<asp:Button Runat="server" ID="btnFooterSparePart" style="display:none;" CommandName="UpdateSparePart"></asp:Button>
													<asp:Label id="lblFooterNomorBarang" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Nama Barang">
												<HeaderStyle Width="18%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="lblNamaPart" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartMaster.PartName" )  %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Left"></FooterStyle>
												<FooterTemplate>
													<asp:Label Runat="server" ID="lblFooterNamaPart"></asp:Label>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Model">
												<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
												<ItemTemplate>
													<asp:Label Runat="server" ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "SparePartMaster.ModelCode" )  %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Left"></FooterStyle>
												<FooterTemplate>
													<asp:Label Runat="server" ID="lblFooterModel"></asp:Label>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Jumlah">
												<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtUnit" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Jumlah", "{0:#,###}") %>' CssClass="textRight">
													</asp:TextBox>
													<asp:RangeValidator id="Rangevalidator3" runat="server" ControlToValidate="txtUnit" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
														MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</ItemTemplate>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFooterUnit" runat="server" MaxLength="6"
														Width="80px" CssClass="textRight"></asp:TextBox>
													<asp:RangeValidator id="RangeValidator2" runat="server" ControlToValidate="txtFooterUnit" ErrorMessage="Unit Permintaan harus lebih besar dari 0"
														MaximumValue="1000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
														<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
												</ItemTemplate>
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
								<TD colSpan="6"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnDelete" runat="server" Text="Hapus" CausesValidation="False" Visible="False"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali" CausesValidation="False" Visible="False"></asp:button></TD>
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

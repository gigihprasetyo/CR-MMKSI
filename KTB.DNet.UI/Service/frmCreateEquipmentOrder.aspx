<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmCreateEquipmentOrder.aspx.vb" Inherits="frmCreateEquipmentOrder" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmCreateEquipmentOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		//function Back()
		//{
		//var hidden = document.getElementById("Hidden1")
		//var i = hidden.value * -1
		//window.history.go(i);
		//}
				
			function ShowPPPembayaran()
			{
				var lblPONumber = document.getElementById("lblNoReqPOValue");			
				showPopUp('../PopUp/PopUpPaymentStatus.aspx?PENumber='+lblPONumber.innerText,'',300,500,KodeEquipment)
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
			showPopUp('../Service/FrmAdditionalInformationService.aspx?text='+newstring+'&type=1'+'&src=pengajuan','',400,400,Penjelasan)
			}
			
			function Konfirmasi(Text)
			{
				var txtKonfirmasi = document.getElementById("txtKonfirmasi");
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				txtKonfirmasi.innerText=Text;		
				}
				else
				{
				txtKonfirmasi.value=Text;
				}					
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
			showPopUp('../Service/FrmAdditionalInformationService.aspx?text='+newstring+'&type=2'+'&src=pengajuan','',400,400,Konfirmasi)
			}
			
			function ShowPPKodeEquipmentSelection()
			{
			var ddlJenis = document.getElementById("ddlJenis");				
			showPopUp('../PopUp/PopUpEquipmentSelection.aspx?Kind='+ddlJenis.value,'',500,760,KodeEquipment)
			}
			
			function GetCurrentInputIndex()
			{
				var dtgEquipmentOrder = document.getElementById("dtgEquipmentOrder");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgEquipmentOrder.rows.length; index++)
				{
					inputs = dtgEquipmentOrder.rows[index].getElementsByTagName("INPUT");
					
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
			
			function KodeEquipment(selectedEquipment)
			{
				var indek = GetCurrentInputIndex();
				var dtgEquipmentOrder = document.getElementById("dtgEquipmentOrder");
				var KodeEquipment = dtgEquipmentOrder.rows[indek].getElementsByTagName("INPUT")[0];
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				KodeEquipment.innerText = selectedEquipment				
				}
				else
				{
				KodeEquipment.value = selectedEquipment	
				}
			}					
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="6">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">EQUIPMENT REPAIR - Pengajuan P3B</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="lblNoReqPO" runat="server">No Reg P3B</asp:label></TD>
					<TD width="1%">:</TD>
					<TD width="30%"><asp:label id="lblNoReqPOValue" runat="server"></asp:label></TD>
					<TD class="titleField" width="18%"><asp:label id="lblKodeDealer" runat="server">Dealer</asp:label></TD>
					<TD width="1%">:</TD>
					<TD width="30%"><asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;/
						<asp:label id="lblSearchTerm1" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNoP3B" runat="server">Nomor P3B</asp:label></TD>
					<TD>:</TD>
					<TD><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" id="txtNoP3B" onblur="alphaNumericPlusSpaceBlur(txtNoP3B)"
							runat="server" MaxLength="20"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoP3B" ErrorMessage="Nomor P3B Harus Diisi">*</asp:requiredfieldvalidator></TD>
					<TD class="titleField"><asp:label id="lblJenis" runat="server">Jenis</asp:label></TD>
					<TD>:</TD>
					<TD><asp:dropdownlist id="ddlJenis" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 23px"><asp:label id="lblPermintaanKirim" runat="server">Permintaan Tgl Kirim</asp:label></TD>
					<TD>:</TD>
					<TD><cc1:inticalendar id="iCPermintaanKirim" runat="server" Sunday="True" TextBoxWidth=70></cc1:inticalendar></TD>
					<TD class="titleField"><asp:label id="lblRencanaKirim" runat="server">Rencana Tgl Kirim</asp:label></TD>
					<TD>:</TD>
					<TD><asp:label id="lblRencanaKirimValue" Runat="server"></asp:label></TD>
				</TR>
				<TR valign=top>
					<TD class="titleField" style="HEIGHT: 13px"><asp:label id="lblPenjelasan" runat="server">Penjelasan</asp:label></TD>
					<TD style="HEIGHT: 13px">:</TD>
					<TD style="HEIGHT: 13px"><asp:textbox id="txtPenjelasan" runat="server" Height="50px" ReadOnly="True" TextMode="MultiLine"></asp:textbox><asp:label id="lblSearchPenjelasan" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<TD class="titleField" style="HEIGHT: 13px"><asp:label id="lblTanggapanKTB" runat="server">Tanggapan MMKSI</asp:label></TD>
					<TD style="HEIGHT: 13px">:</TD>
					<TD style="HEIGHT: 13px"><asp:textbox id="txtKonfirmasi" runat="server" Height="50px" ReadOnly="True" TextMode="MultiLine"></asp:textbox><asp:label id="lblSeachTanggapanKTB" runat="server" Enabled="False"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<tr>
					<td colSpan="6">
						<table id="tableAmountAndGrid" cellSpacing="1" cellPadding="2" width="100%" border="0"
							runat="server">
							<TR>
								<TD class="titleField" style="HEIGHT: 6px" width="20%"><asp:label id="lblSubsidi" runat="server">Subsidi</asp:label></TD>
								<TD style="HEIGHT: 6px" width="1%">:</TD>
								<TD style="HEIGHT: 6px" width="30%">
									<table cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblSubsidiValue" runat="server">0</asp:label></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField" style="HEIGHT: 6px" width="18%"><asp:label id="lblSubTotal" runat="server">Sub Total</asp:label></TD>
								<TD style="HEIGHT: 6px" width="1%">:</TD>
								<TD style="HEIGHT: 6px" width="30%">
									<table cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblSubTotalValue" style="TEXT-ALIGN: right" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTotalPembayaran" runat="server">Total Pembayaran</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellPadding="0" width="200" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblTotalPembayaranValue" runat="server">0</asp:label></td>
											<td width="60"><asp:label id="lblTotalPayPCT" runat="server"></asp:label></td>
											<td width="20"><asp:label id="lblSearchTotalPembayaran" runat="server" Visible="False"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblPPN" runat="server">PPN</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblPPNValue" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblSisaPembayaran" runat="server">Pembayaran Pertama</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellPadding="0" width="180" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblSisaPembayaranValue" runat="server">0</asp:label></td>
											<td width="60"><asp:label id="lblSisaPCT" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblTotal" runat="server">Total</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblTotalValue" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 18px"></TD>
								<TD style="HEIGHT: 18px"></TD>
								<TD style="HEIGHT: 18px"></TD>
							</TR>
							<TR>
								<TD colSpan="6"><div id="div1" style="OVERFLOW: auto; HEIGHT: 200px"><asp:datagrid id="dtgEquipmentOrder" runat="server" BorderWidth="1px" BorderColor="#CDCDCD" Width="100%"
										CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" OnItemCommand="dtgEquipmentOrder_ItemCommand"
										OnEditCommand="dtgEquipmentOrder_Edit" OnCancelCommand="dtgEquipmentOrder_Cancel" OnUpdateCommand="dtgEquipmentOrder_Update"
										OnItemDataBound="dtgEquipmentOrder_ItemDataBound" BackColor="White">
										<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
										<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
										<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Kode ">
												<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="LbnKode" runat="server" CommandName="Kode"></asp:LinkButton>
												</ItemTemplate>
												<FooterStyle Wrap="False"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeEquipmentFooter" runat="server" Width="84px"></asp:TextBox>
													<asp:Label id="lblSearchEquipmentFooter" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id=txtKodeEquipmentEdit onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtKodeEquipmentEdit','<>?*%$;')" runat="server" Width="84px" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentMaster.EquipmentNumber") %>'>
													</asp:TextBox>
													<asp:Label id="lblSearchKodeEquipmentEdit" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Deskripsi">
												<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Jumlah (Unit)">
												<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id=lblJumlah runat="server" NAME="lblJumlah" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity", "{0:#,###}" ) %>' CssClass="TextRight">
													</asp:Label>
												</ItemTemplate>
												<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<asp:TextBox id="txtJumlahFooter" runat="server" MaxLength="5"
														Width="100px" CssClass="TextRight"></asp:TextBox>
													<asp:RangeValidator id="RangeValidator1" runat="server" ControlToValidate="txtJumlahFooter" ErrorMessage="Input Tidak Valid"
														MaximumValue="1000000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtJumlahEdit runat="server" MaxLength="5" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity" ) %>' CssClass="TextRight">
													</asp:TextBox>
													<asp:RangeValidator id="RangeValidator2" runat="server" ControlToValidate="txtJumlahEdit" ErrorMessage="Input Tidak Valid"
														MaximumValue="1000000000" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Harga (Rp)">
												<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Harga Estimasi (Rp)">
												<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn ReadOnly="True" HeaderText="Subsidi (Rp)">
												<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
												CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; alt=&quot;Batal&quot;&gt;"
												EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
												<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											</asp:EditCommandColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
														<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
												<FooterStyle HorizontalAlign="Center"></FooterStyle>
												<FooterTemplate>
													<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
														<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn Visible="False" HeaderText="MasterEqID">
												<ItemStyle VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></div><asp:label id="lblError" runat="server" ForeColor="Red" EnableViewState="False"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR>
					<TD style="WIDTH: 100%" colSpan="6"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>&nbsp;<asp:button id="btnHapus" runat="server" Text="Hapus"></asp:button>&nbsp;<asp:button id="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:button>&nbsp;
						<asp:button id="btnValidasi" runat="server" Text="Validasi"></asp:button>&nbsp;
						<asp:button id="btnBatalValidasi" runat="server" Text="Batal Validasi"></asp:button>&nbsp;
						<asp:button id="btnBaru" runat="server" Width="70px" Text="Baru"></asp:button></TD>
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

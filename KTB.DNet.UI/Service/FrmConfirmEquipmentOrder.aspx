<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmConfirmEquipmentOrder.aspx.vb" Inherits="FrmConfirmEquipmentOrder" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>FrmConfirmEquipmentOrder</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		
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
			showPopUp('../Service/FrmAdditionalInformationService.aspx?text='+newstring+'&type=1','',400,400,Penjelasan)
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
			showPopUp('../Service/FrmAdditionalInformationService.aspx?text='+newstring+'&type=2','',400,400,Konfirmasi)
			}
			
			function ShowPPKodeEquipmentSelection()
			{
			var ddlJenis = document.getElementById("ddlJenis");				
			showPopUp('../PopUp/PopUpEquipmentSelection.aspx?Kind='+ddlJenis.value,'',500,760,KodeEquipment)
			}
			
			
		function GetCurrentInputIndex()
			{
				var dtgEquipmentOrder = document.getElementById("dtgEqMaster");
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
							if (inputs[indexInput].className != "textRight")
								return index;
						}
					}
				}				
				return -1;
			}
			
		function KodeEquipment(selectedEquipment)
			{
				var indek = GetCurrentInputIndex();
				var dtgEqMaster = document.getElementById("dtgEqMaster");
				var KodeEquipment = dtgEqMaster.rows[indek].getElementsByTagName("INPUT")[0];
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
	<body MS_POSITIONING="GridLayout" onfocus="return checkModal()" onclick="checkModal()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" width="100%" border="0" cellpadding="0">
				<tr>
					<td class="titlePage">EQUIPMENT REPAIR - Respon P3B</td>
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
								<TD class="titleField" width="20%"><asp:label id="lblNoReqPO" runat="server" Font-Bold="True">No Reg P3B</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:label id="lblNoReqPOValue" runat="server"></asp:label></TD>
								<TD class="titleField" width="18%"><asp:label id="lblKodeDealer" runat="server" Font-Bold="True"> Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:label id="lblKodeDealerValue" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblSearch1" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 6px"><asp:label id="lblP3B" runat="server" Font-Bold="True">Nomor P3B</asp:label></TD>
								<TD></TD>
								<TD><asp:label id="lblNoP3Bvalue" runat="server"></asp:label></TD>
								<TD class="titleField">
									<P>
										<asp:label id="lblJenis" runat="server" Font-Bold="True">Jenis</asp:label></P>
								</TD>
								<TD></TD>
								<TD>
									<P><asp:dropdownlist id="ddlJenis" runat="server" Width="136px" Enabled="False"></asp:dropdownlist></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 6px"><asp:label id="lblPermintaanTglPengiriman" runat="server" Font-Bold="True">Permintaan Tgl Kirim</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD>
									<P><asp:label id="lblTglPermintaanKirimValue" runat="server"></asp:label></P>
								</TD>
								<TD class="titleField"><asp:label id="lblRencanaTglPengiriman" runat="server" Font-Bold="True">Rencana Tgl Kirim</asp:label></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD>
									<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td width="20%"><asp:checkbox id="cBox" Runat="server" AutoPostBack="True"></asp:checkbox></td>
											<td width="80%"><cc1:inticalendar id="icRencanaKirim" runat="server"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" valign="top">
									<P><asp:label id="lblPenjelasan" runat="server" Font-Bold="True">Penjelasan</asp:label></P>
								</TD>
								<TD valign="top"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD>
									<P><asp:textbox id="txtPenjelasan" runat="server" ReadOnly="True" Height="59px" TextMode="MultiLine"
											Width="120px"></asp:textbox><asp:label id="lblSearchPenjelasan" runat="server" Enabled="False">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></P>
								</TD>
								<TD valign="top" class="titleField"><asp:label id="lblTanggapanKtb" runat="server" Font-Bold="True">Tanggapan MMKSI</asp:label></TD>
								<TD valign="top"><asp:label id="Label21" runat="server">:</asp:label></TD>
								<TD><asp:textbox id="txtKonfirmasi" runat="server" ReadOnly="True" Height="62px" TextMode="MultiLine"
										Width="131px"></asp:textbox><asp:label id="lblSeachTanggapanKTB" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">
									<P><asp:label id="lblSubsidi" runat="server" Font-Bold="True">Subsidi</asp:label></P>
								</TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD>
									<P>Rp<asp:label id="lblSubsidiValue" style="TEXT-ALIGN: right" runat="server"></asp:label></P>
								</TD>
								<TD class="titleField"><asp:label id="lblSubtotal" runat="server" Font-Bold="True">Subtotal</asp:label></TD>
								<TD><asp:label id="Label22" runat="server">:</asp:label></TD>
								<TD><table cellspacing="0" cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblSubTotalValue" style="TEXT-ALIGN: right" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTotalPembayaran" runat="server" Font-Bold="True">Total Pembayaran</asp:label></TD>
								<TD>:</TD>
								<TD><table cellPadding="0" width="200" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:Label id="lblTotalPembayaranValue" runat="server">0</asp:Label></td>
											<td width="60"><asp:label id="lblTotalPayPCT" runat="server"></asp:label></td>
											<td width="20"><asp:label id="lblSearchTotalPembayaran" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField">
									<P><asp:label id="lblPPN" runat="server" Font-Bold="True">PPN</asp:label></P>
								</TD>
								<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD><table cellspacing="0" cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblPPNValue" style="TEXT-ALIGN: right" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblSisaPembayaran" runat="server" Font-Bold="True">Sisa Pembayaran</asp:label></TD>
								<TD><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD><table cellPadding="0" width="180" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:Label id="lblSisaPembayaranValue" runat="server">0</asp:Label></td>
											<td width="60"><asp:label id="lblSisaPCT" runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblTotal" runat="server" Font-Bold="True">Total</asp:label></TD>
								<TD><asp:label id="Label24" runat="server">:</asp:label></TD>
								<TD><table cellspacing="0" cellPadding="0" width="120" border="0">
										<tr>
											<td noWrap width="20">Rp</td>
											<td noWrap align="right" width="100"><asp:label id="lblTotalValue" runat="server" style="TEXT-ALIGN: right" Font-Bold="True"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgEqMaster" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
							OnItemDataBound="dtgEqMaster_ItemDataBound" OnItemCommand="dtgEqMaster_ItemCommand" BorderColor="#CDCDCD"
							CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" BackColor="Silver"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="Id" HeaderText="Id"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox9" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode ">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LbnKode" runat="server" CommandName="Kode"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle Wrap="False"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtKodeEquipmentFooter" runat="server" Width="84px"></asp:TextBox>
										<asp:Label id="lblKodeEquipmentFooter" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Deskripsi">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Jumlah (Unit)">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblJumlah runat="server" NAME="lblJumlah" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity", "{0:#,###}" ) %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtJumlahFooter" runat="server" Width="77px"
											MaxLength="5" CssClass="textRight"></asp:TextBox>
										<asp:RangeValidator id="RangeValidator1" runat="server" MaximumValue="1000000" MinimumValue="1" Type="Integer"
											ErrorMessage="Input Tidak Valid" ControlToValidate="txtJumlahFooter">*</asp:RangeValidator>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Harga (Rp)">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblJumlahHarga" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Subsidi (Rp)">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox onkeypress="return numericOnlyUniv(event)" onKeyUp="pic(this,this.value,'9999999999','N')" id=txtSubsidi runat="server" Width="93px" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>' MaxLength="3" CssClass="textRight">
										</asp:TextBox>&nbsp;
										<asp:Label id="Label4" runat="server">%</asp:Label>
										<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Subsidi Tidak Boleh Kosong"
											ControlToValidate="txtSubsidi">*</asp:RequiredFieldValidator>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtSubsidiFooter" runat="server"
											Width="93px" MaxLength="3" CssClass="textRight"></asp:TextBox>&nbsp;
										<asp:Label id="Label6" runat="server">%</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" HeaderText="EquipmentMasterID"></asp:BoundColumn>
							</Columns>
						</asp:datagrid><asp:label id="lblError" runat="server" ForeColor="Red" EnableViewState="False"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
				</TR>
				<TR>
					<TD><asp:button id="btnSimpan" runat="server" Width="72px" Text="Simpan"></asp:button><asp:button id="btnKonfirmasi" runat="server" Width="69px" Text="Konfirmasi" Visible="False"></asp:button><asp:button id="btnBatalKonfirmasi" runat="server" Text="Batal Konfirmasi" Visible="False"></asp:button><asp:button id="btnRilis1" runat="server" Width="56px" Text="Rilis1" Visible="False"></asp:button><asp:button id="btnRilis2" runat="server" Width="64px" Text="Rilis2" Visible="False"></asp:button><asp:button id="btnBatalRilis" runat="server" Text="Batal Rilis" Visible="False"></asp:button><asp:button id="btnTolak" runat="server" Width="64px" Text="Tolak" Visible="False"></asp:button><asp:button id="btnBatalTolak" runat="server" Text="Batal Tolak" Visible="False"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
		</SCRIPT>
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

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmStokDealer.aspx.vb" Inherits="FrmStokDealer" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListPO</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{ 
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		
		function IsCheckedDateDuration()
		{			
			var chkTanggalCetak = document.getElementById("chkTanggalCetak");
			var chkTanggalFaktur = document.getElementById("chkTanggalFaktur");
			var chkTanggalCetakFaktur = document.getElementById("chkTanggalCetakFaktur");
			//var chkTglPengajuanFaktur = document.getElementById("chkTglPengajuanFaktur");
			var chkTglKonfirmasiFaktur = document.getElementById("chkTglKonfirmasiFaktur");
			if (chkTanggalCetak.checked == false && chkTanggalFaktur.checked == false && chkTanggalCetakFaktur.checked == false && chkTglKonfirmasiFaktur.checked == false)
			{
				alert("Periode Tanggal Cetak DO atau Tanggal Cetak Faktur atau Tanggal Faktur harus dicentang");
				return false;
			}
		}
		function CheckAll()
		{
			var chkAll=document.getElementById("chkAll");
			var chkTanggalCetak= document.getElementById("chkTanggalCetak");
			var chkTanggalFaktur = document.getElementById("chkTanggalFaktur");
			var chkTanggalCetakFaktur = document.getElementById("chkTanggalCetakFaktur");
			if(chkAll.checked)
			{
				chkTanggalCetak.checked=false;
				chkTanggalFaktur.checked=false;
				chkTanggalCetakFaktur.checked=false;
			}			
		}
		function Spaning()
		{
			var dtgMain = document.getElementById("dtgMain");
			var i=0;
			for(i=1;i<dtgMain.rows.length;i++)
			{
				if((i-1)%3==0)
				{
					dtgMain.rows[i].cells[0].rowSpan="3";
					dtgMain.rows[i].cells[1].rowSpan="3";
					dtgMain.rows[i+1].deleteCell(0);
					dtgMain.rows[i+1].deleteCell(0);
					dtgMain.rows[i+2].deleteCell(0);
					dtgMain.rows[i+2].deleteCell(0);
				}
			}
		}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<input style="VISIBILITY: hidden" onclick="Spaning();" type="button">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td style="HEIGHT: 17px" class="titlePage">DAFTAR STOK&nbsp;&nbsp;- Stok Dealer</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 188px">
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD style="WIDTH: 138px; HEIGHT: 17px" class="titleField" width="138"><asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 211px; HEIGHT: 17px" width="211"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPDealerSelection();" runat="server" style="Z-INDEX: 0">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD style="WIDTH: 108px; HEIGHT: 17px" class="titleField" width="108"><asp:label id="Label6" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 120px; HEIGHT: 17px" width="120"><asp:dropdownlist id="ddlKategori" runat="server" AutoPostBack="True" Width="140px" ></asp:dropdownlist></TD>
							</TR>
							<TR vAlign="top">
								<TD style="WIDTH: 138px; HEIGHT: 30px" class="titleField"><asp:checkbox id="chkTanggalCetak" onclick="CheckAll();" runat="server" Width="168px" Text="Tanggal Cetak DO"></asp:checkbox></TD>
								<TD style="HEIGHT: 30px">:</TD>
								<TD style="WIDTH: 211px; HEIGHT: 30px">
									<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<TD><cc1:inticalendar id="icCetakStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icCetakEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
								<td style="WIDTH: 108px; HEIGHT: 30px" class="titleField" vAlign="middle">Model</td>
								<TD style="HEIGHT: 30px">:</TD>
								<TD style="WIDTH: 120px; HEIGHT: 30px" vAlign="middle"><asp:dropdownlist id="ddlModel" runat="server" AutoPostBack="True" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<tr vAlign="top" runat="server" visible="false" id="trPengajuan">
								<TD style="WIDTH: 138px; HEIGHT: 30px" class="titleField"><asp:checkbox style="Z-INDEX: 0" id="chkTglPengajuanFaktur" onclick="CheckAll();" runat="server"
										Width="168px" Text="Tanggal Pengajuan Faktur"></asp:checkbox></TD>
								<TD style="HEIGHT: 30px">:</TD>
								<TD style="WIDTH: 211px; HEIGHT: 30px">
									<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<TD><cc1:inticalendar id="icPengajuanStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icPengajuanEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							<TD style="WIDTH: 108px; HEIGHT: 17px" class="titleField"><asp:label id="Label5" runat="server"> Tipe</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="WIDTH: 120px; HEIGHT: 17px"><asp:dropdownlist id="Dropdownlist1" runat="server" AutoPostBack="True" Width="140px"></asp:dropdownlist></TD>
							</tr>


                            <tr vAlign="top">
								<TD style="WIDTH: 138px; HEIGHT: 30px" class="titleField"><asp:checkbox style="Z-INDEX: 0" id="chkTglKonfirmasiFaktur" onclick="CheckAll();" runat="server"
										Width="168px" Text="Tanggal Konfirmasi Faktur"></asp:checkbox></TD>
								<TD style="HEIGHT: 30px">:</TD>
								<TD style="WIDTH: 211px; HEIGHT: 30px">
									<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<TD><cc1:inticalendar id="icKnfirmasiStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icKnfirmasiEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
                                	<TD style="WIDTH: 108px; HEIGHT: 17px" class="titleField"><asp:label id="lblDealerPO" runat="server"> Tipe</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="WIDTH: 120px; HEIGHT: 17px"><asp:dropdownlist id="ddlTipe" runat="server" AutoPostBack="True" Width="140px"></asp:dropdownlist></TD>
								
							</tr>



							<TR>
								<TD style="WIDTH: 138px; HEIGHT: 30px" class="titleField"><asp:checkbox style="Z-INDEX: 0" id="chkTglValidasiFaktur" onclick="CheckAll();" runat="server"
										Width="168px" Text="Tanggal Validasi Faktur"></asp:checkbox></TD>
								<TD style="HEIGHT: 30px">:</TD>
								<TD style="WIDTH: 211px; HEIGHT: 30px">
									<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<TD><cc1:inticalendar id="icValidasiStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icValidasiEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>

								<TD style="WIDTH: 108px; HEIGHT: 17px" class="titleField"><asp:label id="Label2" runat="server"> Warna</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="WIDTH: 120px; HEIGHT: 17px"><asp:dropdownlist id="ddlWarna" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>


                            
                          
							<TR>
								<TD style="WIDTH: 138px" class="titleField"><STRONG>Status Stock</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 211px"><asp:listbox id="lsbStatusStock" runat="server" Width="168px" SelectionMode="Multiple" Height="48px"></asp:listbox></TD>
								<TD style="WIDTH: 108px" class="titleField"><asp:label style="Z-INDEX: 0" id="lblNoMO" runat="server"> Nomor SO</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 120px"><asp:textbox onblur="alphaNumericPlusBlur(txtNoOC)" style="Z-INDEX: 0" id="txtNoSO" onkeypress="return alphaNumericPlusUniv(event)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 138px" class="titleField"><STRONG>Status Faktur</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 211px" ><asp:listbox id="lsbStatusFaktur" runat="server" Width="168px" SelectionMode="Multiple" Height="48px"></asp:listbox></TD>
								<TD style="WIDTH: 108px" vAlign="top"><asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Bold="True">Nomor DO</asp:label></TD>
								<TD style="WIDTH: 91px">:</TD>
								<TD><asp:textbox onblur="alphaNumericPlusBlur(txtNoSO)" style="Z-INDEX: 0" id="txtNoDO" onkeypress="return alphaNumericPlusUniv(event)"
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
								<!-- <TD style="WIDTH: 120px"></TD> -->
							</TR>
							<TR>
								<TD style="WIDTH: 138px" class="titleField"></TD>
								<TD></TD>
								<TD style="WIDTH: 108px"></TD>
								<TD style="WIDTH: 108px" vAlign="top"><asp:label style="Z-INDEX: 0" id="Label3" runat="server" Font-Bold="True">Nomor Rangka</asp:label></TD>
								<TD style="WIDTH: 91px">:</TD>
								<TD><asp:textbox  style="Z-INDEX: 0" id="txtNomerRangka" 
										runat="server" Width="140px" MaxLength="20"></asp:textbox></TD>
								<!--<TD></TD>-->
								<!--<TD style="WIDTH: 120px"></TD>-->
							</TR>
							<TR>
								<TD style="WIDTH: 138px"><STRONG>Total Qty</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 211px"><asp:label id="txtTotalRow" runat="server" Font-Bold="True">0</asp:label></TD>
								<TD style="WIDTH: 108px">
									<table cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></td>
											<td>&nbsp;</td>
											<td><asp:button id="btnDownload" runat="server" Width="80px" Text="Download"></asp:button></td>
										</tr>
									</table>
								</TD>
								<TD style="WIDTH: 120px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 138px"><STRONG></STRONG></TD>
								<TD></TD>
								<TD style="WIDTH: 211px"><asp:checkbox id="chkAll" onclick="CheckAll();" runat="server" Width="104px" Text="Semua Tanggal"
										Visible="False" Height="6px"></asp:checkbox><asp:label id="lblHariIni" runat="server" Visible="False" Font-Bold="True"></asp:label></TD>
								<TD style="WIDTH: 108px"></TD>
								<TD></TD>
								<TD style="WIDTH: 120px">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 1px">
						<DIV id="div1" style="WIDTH: 100%; HEIGHT: 238px; OVERFLOW: auto"><asp:datagrid id="dtgMain" runat="server" Width="100%" AutoGenerateColumns="False" AllowCustomPaging="True"
								PageSize="25" AllowPaging="True" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="AlreadySaled" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStockStatus" runat="server" Text='<%# IIf((CType(DataBinder.Eval(Container, "DataItem.AlreadySaled"), Byte) = 0), "Belum Terjual", "Sudah Terjual")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Dealer">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ModelDescription" HeaderText="Model">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblModelDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModelDescription")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileTypeCode" HeaderText="Tipe">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblVechileType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ColorCode" HeaderText="Warna">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblColorCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ColorCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNumber" HeaderText="Nomor Rangka">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblChassisNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProjectName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Name1" HeaderText="Nama Konsumen">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblName1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name1") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DODate" HeaderText="Tanggal Cetak DO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDODate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DODate"),"dd/MM/yyyy")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FakturDate" HeaderText="Tanggal Pengajuan Faktur" Visible="false">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRequestDate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.OpenFakturDate"),"dd/MM/yyyy")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ConfirmFakturDate" HeaderText="Tanggal Konfirmasi Faktur">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblConfirmDate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ConfirmFakturDate"),"dd/MM/yyyy")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>


									<asp:TemplateColumn SortExpression="ValidateTime" HeaderText="Tanggal Validasi Faktur">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblValidasiFakturDate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ValidateTime"),"dd/MM/yyyy")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TOPID" HeaderText="Cara Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaymentType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TOPID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SONumber" HeaderText="Nomor SO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FakturNumber" HeaderText="Nomor Faktur">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FakturNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FakturStatus" HeaderText="Status Faktur">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblFakturStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FakturStatus") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DONumber" HeaderText="Nomor DO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DONumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sudah<br>Terjual">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label Visible="False" id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
											<asp:CheckBox runat="server" ID="ChkSaled"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD><asp:button id="btnReverseAlreadySaled" runat="server" Text="Set Belum Terjual"></asp:button><asp:button id="btnAlreadySaled" runat="server" Text="Set Sudah Terjual"></asp:button></TD>
				</TR>
				<TR>
					<TD align="center"></TD>
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
		</TD></TR></TABLE></TD></TR></TABLE></FORM>
	</body>
</HTML>

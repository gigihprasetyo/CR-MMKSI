<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmEquipmentOrderList.aspx.vb" Inherits="frmEquipmentOrderList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Daftar Pembelian/Perbaikan Equipment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		</script>
</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MASTER BARANG - Daftar P3B</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD width="100%" colSpan="10">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" align="center" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 105px; HEIGHT: 26px" width="105"><asp:label id="lblkodeDealer" runat="server" Font-Bold="True">Kode Dealer </asp:label></TD>
								<TD style="HEIGHT: 26px" width="1%"><asp:label id="Label4" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD style="WIDTH: 373px; HEIGHT: 26px" width="373"><asp:textbox id="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 8px; HEIGHT: 26px" width="8"></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 26px" width="157"><asp:label id="lblJenis" runat="server" Font-Bold="True">Jenis</asp:label></TD>
								<TD class="titleField" style="WIDTH: 4px; HEIGHT: 26px" width="4"><asp:label id="Label10" runat="server">:</asp:label>
								<TD style="WIDTH: 134px; HEIGHT: 26px" width="134" colSpan="2"><asp:dropdownlist id="ddlJenis" runat="server" Width="136px"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 26px" width="20%">&nbsp;&nbsp;&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 105px"><asp:label id="lblNoP3B" runat="server" Font-Bold="True">Nomor P3B</asp:label></TD>
								<TD><asp:label id="Label5" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD style="WIDTH: 373px"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNomorP3B" onblur="alphaNumericPlusBlur(txtNomorP3B)"
										runat="server" MaxLength="20"></asp:textbox></TD>
								<TD class="titleField" style="WIDTH: 8px"></TD>
								<TD class="titleField" style="WIDTH: 157px"><asp:checkbox id="chkTanggalP3B" runat="server" Checked="True" Text="Tanggal P3B"></asp:checkbox></TD>
								<TD class="titleField" style="WIDTH: 4px"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 117px"><cc1:inticalendar id="icTanggalP3BAwal" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								<TD style="WIDTH: 12px">s.d</TD>
								<TD><cc1:inticalendar id="IcTanggalP3BAkhir" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 105px"><asp:label id="lblStatus" runat="server" Font-Bold="True">Status</asp:label></TD>
								<TD><asp:label id="Label1" runat="server" Font-Bold="True">:</asp:label></TD>
								<TD class="titleField" style="WIDTH: 373px"><asp:listbox id="lboxStatus" runat="server" Width="136px" SelectionMode="Multiple" Rows="3"></asp:listbox></TD>
								<TD class="titleField" style="WIDTH: 8px"></TD>
								<TD class="titleField" style="WIDTH: 157px"><asp:checkbox id="chkTanggalValidasi" runat="server" Text="Tanggal Validasi"></asp:checkbox></TD>
								<TD class="titleField" style="WIDTH: 4px"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 117px"><cc1:inticalendar id="icTglValidFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								<TD style="WIDTH: 12px" vAlign="middle" noWrap>s.d</TD>
								<TD vAlign="middle" noWrap><cc1:inticalendar id="IcTglValidTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" style="WIDTH: 105px"></TD>
								<TD></TD>
								<TD class="titleField" style="WIDTH: 373px"><asp:button id="btnCari" runat="server" Width="72px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="WIDTH: 8px"></TD>
								<TD class="titleField" style="WIDTH: 157px"><asp:label id="lblTotalHarga" runat="server">Total Harga</asp:label></TD>
								<TD class="titleField" style="WIDTH: 4px"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 117px"><asp:label id="lblTotalHargaValue" runat="server" Font-Bold="True"></asp:label></TD>
								<TD style="WIDTH: 12px" vAlign="top" noWrap></TD>
								<TD vAlign="top" noWrap>&nbsp;&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="10">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="1026"><asp:datagrid id="dtgEqSalesHeader" runat="server" Width="100%" AllowPaging="True" PageSize="25"
								AllowCustomPaging="True" AllowSorting="True" OnPageIndexChanged="dtgEqSalesHeader_PageIndexChanged" OnItemDataBound="dtgEqSalesHeader_ItemDataBound" AutoGenerateColumns="False"
								BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkItem',document.all.chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItem" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegPONumber" SortExpression="RegPONumber" HeaderText="No Reg P3B">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Kind" SortExpression="Kind" HeaderText="Jenis">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn DataField="PONumber" SortExpression="PONumber" HeaderText="Nomor P3B">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal P3B" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn SortExpression="ValidateDate" HeaderText="Tanggal Validasi">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ReqDeliveryDate" SortExpression="ReqDeliveryDate" HeaderText="Permintaan Pengiriman" 
 DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total (Rp)">
										<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Pembayaran (%)">
										<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="EstimateDeliveryDate" HeaderText="Rencana Pengiriman">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ResponseDetail" SortExpression="ResponseDetail" HeaderText="Tanggapan MMKSI">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:BoundColumn>
										<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Font-Bold="True" ForeColor="Red"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Font-Bold="True" ForeColor="Red"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbnEdit" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="Status" HeaderText="StatusInteger">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle></asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbnView" runat="server" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnPayment" runat="server" CommandName="Payment">
												<img src="../images/rp.gif" alt="Status Pembayaran" border="0"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Font-Bold="True" ForeColor="Red"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Status"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR id="TROperator" runat="server">
					<TD colSpan="10"><asp:label id="Label9" runat="server" Font-Bold="True" Font-Italic="True">Mengubah Status :</asp:label><asp:dropdownlist id="ddlStatus" runat="server" Width="120px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
						<asp:button id="btnProses" runat="server" Text="Proses"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnSetuju" runat="server" Text="Setuju"></asp:button><asp:button id="btnTidakSetuju" runat="server" Width="80px" Text="Tidak Setuju"></asp:button><asp:button id="btnValidasi" runat="server" Text="Validasi"></asp:button>
						<asp:button id="btnTransferUlang" runat="server" Text="Tranfer Ulang"></asp:button></TD>
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

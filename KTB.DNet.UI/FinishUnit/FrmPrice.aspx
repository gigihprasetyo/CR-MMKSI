<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrice.aspx.vb" Inherits="FrmPrice" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
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
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">UMUM - Daftar Harga Kendaraan</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" border="0" cellSpacing="2" cellPadding="1" width="100%">
							<TR>
								<TD class="titleField" width="20%">Lokasi File</TD>
								<TD width="1%">:</TD>
								<td width="79%" colSpan="4"><INPUT id="DataFile" onkeypress="return false;" size="29" type="file" name="File1" runat="server">
									<asp:button id="btnUpload" runat="server" Text="Upload" Width="60px"></asp:button><asp:button id="btnStore" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:button></td>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Status</TD>
								<TD width="1%">:</TD>
								<TD width="34" noWrap><asp:dropdownlist id="ddlStatus" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD class="titleField" width="20%">Mulai&nbsp;Berlaku</TD>
								<td width="1%">:</td>
								<TD width="25%"><asp:checkbox id="chkAll" runat="server" Text="Semua"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Kategori</TD>
								<TD>:</TD>
								<td><asp:dropdownlist id="ddlCategory" runat="server" Width="96px" AutoPostBack="True"></asp:dropdownlist><asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
								<TD></TD>
								<TD></TD>
								<TD><cc1:inticalendar id="calCalendar" runat="server"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe</TD>
								<TD>:</TD>
								<td>
									<table border="0" cellSpacing="0" cellPadding="1" width="100%">
										<tr>
											<td><asp:dropdownlist id="ddlType" runat="server" Width="96px" Height="21px"></asp:dropdownlist></td>
											<td></td>
										</tr>
									</table>
								</td>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField">Dealer</TD>
								<TD>:</TD>
								<TD><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD><asp:button style="Z-INDEX: 0" id="btnSearch" runat="server" Text="Cari" Width="60px"></asp:button></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="OVERFLOW: auto; HEIGHT: 330px" id="div1">
							<asp:datagrid id="dgPriceUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
								Visible="False" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Kendaraan">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeKendaraan" runat="server" Text=''></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Kendaraan">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNamaKendaraan" runat="server" Text=''></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text=''></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ValidFrom" HeaderText="Mulai Berlaku" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BasePrice" HeaderText="Harga Pokok (Rp)" DataFormatString="{0:#,##0}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OptionPrice" HeaderText="Harga Lain-lain (Rp) *" DataFormatString="{0:#,##0}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPN_BMAmount" HeaderText="PPN BM (Rp)" DataFormatString="{0:#,##0}" Visible="False">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPN_BM" HeaderText="PPN BM (%)"  DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPNAmount" HeaderText="PPN (Rp)"  Visible="False" DataFormatString="{0:#,##0}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
												<asp:BoundColumn DataField="PPN" HeaderText="PPN (%)" DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									
									<asp:BoundColumn DataField="PPh22Amount" HeaderText="PPh 22 (Rp)" Visible="False"  DataFormatString="{0:#,##0}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPh22" HeaderText="PPh 22 (%)" DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									
									<asp:BoundColumn DataField="Interest" HeaderText="Interest (%)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FactoringInt" HeaderText="Factoring Interest (%)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DiscountReward" HeaderText="Discount Reward (%) " DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPh23" HeaderText="PPh 23 (%)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ErrorMessage" HeaderText="Pesan">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<asp:datagrid id="dgPrice" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal"
								CellSpacing="1" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Kode Kendaraan">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileColor.MaterialDescription" HeaderText="Nama Kendaraan">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialDescription") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer" SortExpression="Dealer.DealerCode">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealer" runat="server" Text=''></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ValidFrom" SortExpression="ValidFrom" ReadOnly="True" HeaderText="Mulai Berlaku"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BasePrice" SortExpression="BasePrice" ReadOnly="True" HeaderText="Harga Pokok (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OptionPrice" SortExpression="OptionPrice" ReadOnly="True" HeaderText="Harga Lain-lain (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPN_BMAmount" SortExpression="PPN_BMAmount" Visible="False" ReadOnly="True" HeaderText="PPN BM (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPN_BM" SortExpression="PPN_BM" ReadOnly="True" HeaderText="PPN BM (%)"
										DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									
									<asp:BoundColumn DataField="PPNAmount" SortExpression="PPNAmount" Visible="False"  ReadOnly="True" HeaderText="PPN (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									
												<asp:BoundColumn DataField="PPN" SortExpression="PPN" ReadOnly="True" HeaderText="PPN (%)"
										DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									
									<asp:BoundColumn DataField="PPh22Amount" SortExpression="PPh22Amount" Visible="False"  ReadOnly="True" HeaderText="PPh 22 (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									
										<asp:BoundColumn DataField="PPh22" SortExpression="PPh22" ReadOnly="True" HeaderText="PPh 22 (%)"
										DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									
									<asp:BoundColumn DataField="Interest" SortExpression="Interest" ReadOnly="True" HeaderText="Interest (%)"
										DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FactoringInt" SortExpression="FactoringInt" ReadOnly="True" HeaderText="Factoring Interest (%)"
										DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DiscountReward" SortExpression="DiscountReward" HeaderText="Discount Reward (%) "
										DataFormatString="{0:#,##0.###}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPh23" SortExpression="PPh23" ReadOnly="True" HeaderText="PPh 23 (%)"
										DataFormatString="{0:#,##0.##}">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Del" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0"></asp:LinkButton>
											<asp:LinkButton id="lbtnActive" runat="server" Text="Undel" CommandName="Active">
												<img src="../images/aktif.gif" border="0" alt="Aktifkan"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 40px" align="left"><asp:button id="btnDnLoad" runat="server" Text="Download" Width="70px" Enabled="False"></asp:button>&nbsp;&nbsp;
                        <asp:button id="btnDnLoadtoExcel" runat="server" Width="120px" Text="Download to Excel" Enabled="false"></asp:button>
		                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* 
						Harga Lain-lain: (Body part, Auto part)+ PPN10%, Deposit A</TD>
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

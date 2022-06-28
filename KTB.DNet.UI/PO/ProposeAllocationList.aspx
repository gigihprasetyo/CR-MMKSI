<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProposeAllocationList.aspx.vb" Inherits="ProposeAllocationList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProposeAllocationList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
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
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PO HARIAN - Unit Usulan SAP</td>
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
								<TD class="titleField" width="24%"><asp:label id="Label1" runat="server">Tanggal Alokasi</asp:label></TD>
								<TD width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD width="25%"><cc1:inticalendar id="calRegAlocation" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								<TD class="titleField" width="15%">
									<asp:label style="Z-INDEX: 0" id="Label11" runat="server">Produk</asp:label></TD>
								<TD width="1%">
									<asp:label style="Z-INDEX: 0" id="Label2" runat="server">:</asp:label></TD>
								<TD width="35%">
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" AutoPostBack="True" width="140px"></asp:dropdownlisT></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">
									<asp:label style="Z-INDEX: 0" id="Label9" runat="server">Tahun Perakitan/Impor</asp:label></TD>
								<TD width="1%">
									<asp:label style="Z-INDEX: 0" id="Label10" runat="server">:</asp:label></TD>
								<TD width="25%">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlYear" runat="server" Width="112px"></asp:dropdownlist></TD>
								<TD class="titleField" width="15%">
									<asp:label style="Z-INDEX: 0" id="Label3" runat="server">Kategori</asp:label></TD>
								<TD width="1%"><asp:label id="Label7" runat="server" style="Z-INDEX: 0">:</asp:label></TD>
								<TD width="35%">
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlCategory" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD class="titleField" style="HEIGHT: 15px"><asp:label id="Label5" runat="server" style="Z-INDEX: 0">Tipe</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlVechileTypeCode" runat="server" AutoPostBack="True" Width="140px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 15px" class="titleField"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"><asp:button id="btnFind" runat="server" Width="60px" Text="Cari" style="Z-INDEX: 0"></asp:button></TD>
								<TD style="HEIGHT: 15px" class="titleField"><asp:label id="Label6" runat="server" style="Z-INDEX: 0">Tipe/Warna</asp:label></TD>
								<TD style="HEIGHT: 15px">:</TD>
								<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlMaterialNumber" runat="server" Width="140px" style="Z-INDEX: 0"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="HEIGHT: 410px; OVERFLOW: auto"><asp:datagrid id="dgAllocation" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
											BorderWidth="0px" CellSpacing="1" BackColor="#CDCDCD" BorderColor="#CDCDCD" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn HeaderText="Model/Tipe/Warna">
													<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Produk">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblProductCategory" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn SortExpression="MaterialNumber" HeaderText="Tipe/Warna">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ProductionYear" SortExpression="ProductionYear" HeaderText="Tahun Perakitan/Import">
													<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalATP" HeaderText="Stok ATP (unit)">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalPermintaan" HeaderText="Total Order (unit)">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalRilis" HeaderText="Status Rilis">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalSetuju" HeaderText="Status Setuju">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalTolak" HeaderText="Diblok, Ditolak, Tdk Setuju">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalSelesai" HeaderText="Status Selesai">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalSisa" HeaderText="Sisa Stok">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Detail">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" CommandName="edit">
															<img src="../images/detail.gif" alt="Lihat Detil" style="cursor:hand" border="0"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="6"><asp:button id="btnDownload" runat="server" Width="80px" Text="Download"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
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

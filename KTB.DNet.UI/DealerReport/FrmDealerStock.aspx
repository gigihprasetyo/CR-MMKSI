<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerStock.aspx.vb" Inherits="FrmDealerStock"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDealerStock</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">LAPORAN - Rekap Bulanan</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TBODY>
								<TR>
									<TD class="titleField">Dealer</TD>
									<TD>:</TD>
									<TD><asp:label id="lblDealer" runat="server"></asp:label><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtKodeDealer" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
									<TD class="titleField">Kategori</TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField">Area</TD>
									<TD>:</TD>
									<TD>
										<asp:DropDownList id="ddlArea" runat="server" Width="104px"></asp:DropDownList></TD>
									<TD class="titleField">Tipe</TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlType" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField">Periode</TD>
									<TD>:</TD>
									<TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtPeriode" runat="server" Width="50px"
											MaxLength="6"></asp:textbox>mmyyyy</TD>
									<TD class="titleField"></TD>
									<TD></TD>
									<TD><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></TD>
								</TR>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dgDealerStock" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD"
								BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="100" AllowCustomPaging="True"
								AllowPaging="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="DgdsLblDealer" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Tipe">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Tipe">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNamaTipe runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<STRONG>Total:</STRONG>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Carry Over(n-1)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCarryOver_Min1" Text='<%# format(DataBinder.Eval(Container, "DataItem.CarryOver_Min1"),"#,##0") %>' Runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="right"></FooterStyle>
										<FooterTemplate>
											<B></B>
											<asp:Label id="lblTotalCarryOver_Min1" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="New Order">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNewOrder Text='<%# format(DataBinder.Eval(Container,"DataItem.NewOrder"),"#,##0") %>' Runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="right"></FooterStyle>
										<FooterTemplate>
											<B></B>
											<asp:Label id="lblTotalNewOrder" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sales Volume">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblSalesVolume runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.SalesVolume"),"#,##0") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="right"></FooterStyle>
										<FooterTemplate>
											<B></B>
											<asp:Label id="lblTotalSalesVolume" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Carry Over">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblCarryOver Text='<%# format(DataBinder.Eval(Container, "DataItem.CarryOver"),"#,##0") %>' Runat="server">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="right"></FooterStyle>
										<FooterTemplate>
											<B></B>
											<asp:Label id="lblTotalCarryOver" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Begining Stock">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblBeginingStock" Runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="right"></FooterStyle>
										<FooterTemplate>
											<B></B>
											<asp:Label id="lblTotalBegStock" Runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD><asp:button id="btnDownload" runat="server" Text="Download" Visible="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

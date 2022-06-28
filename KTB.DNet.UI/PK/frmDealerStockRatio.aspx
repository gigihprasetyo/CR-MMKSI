<%@ Register assembly="KTB.DNet.WebCC" namespace="KTB.DNet.WebCC" tagprefix="cc1" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDealerStockRatio.aspx.vb" Inherits=".frmDealerStockRatio" smartNavigation="False"%>

<%@ Import Namespace="KTB.DNet.Domain" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Stock Ratio</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript">

            function ShowPPDealerSelection() {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var txtDealerSelection = document.getElementById("txtDealerCode");
                txtDealerSelection.value = selectedDealer;
            }
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	    <%--<style type="text/css">
            .auto-style3 {
                width: 24%;
            }
            .auto-style4 {
                height: 24px;
                width: 23%;
            }
            .auto-style5 {
                width: 16%;
            }
            .auto-style6 {
                height: 24px;
                width: 16%;
            }
            .auto-style7 {
                width: 23%;
            }
            .auto-style8 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                height: 24px;
            }
            .auto-style9 {
                height: 24px;
            }
        </style>--%>
	    <style type="text/css">
            .auto-style1 {
                width: 140px;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colspan="7">
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td class="titlePage" colSpan="6">PESANAN KENDARAAN - Dealer Stock Ratio</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD width="20%" class="titleField">Dealer</TD>
					<td width="1%"><asp:label id="Label4" runat="server">:</asp:label></td>
					<TD colspan="2"><asp:label id="lblDealerCode" runat="server"></asp:label><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="144px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:button id="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:button></TD>
					
					<td width="15%">&nbsp;</td>
					<td width="1%">&nbsp;</td>
					<td width="34%">&nbsp;</td>
				</TR>
				<TR>
					<TD class="titleField" >
						Kategori</TD>
					<TD>
						:</TD>
					<TD class="auto-style1" >
						<asp:dropdownlist id="ddlCategory" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD >
						&nbsp;</TD>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD class="titleField" >
						Model</TD>
					<TD>
						<asp:label id="Label5" runat="server">:</asp:label></TD>
					<TD class="auto-style1" >
						<asp:dropdownlist id="ddlModel" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD >
						&nbsp;</TD>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</TR>
				<TR valign="top">
					<TD ><strong>Periode PK</strong></TD>
					<TD  >:</TD>
					<TD colspan="3" >
						<asp:dropdownlist id="ddlMonthPeriodeFrom" Runat="server" Width="128px"></asp:dropdownlist><asp:dropdownlist id="ddlTahun" Runat="server" Width="128px"></asp:dropdownlist></TD>
					<TD  >
                        &nbsp;</TD>
					<TD  >
						</TD>
					<TD  >
						</TD>
					<TD  >
						</TD>
				</TR>
				<TR>
					<td><asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>
						</td>
					<td></td>
					<td class="auto-style1" ></td>
					<td >&nbsp;</td>
					<td></td>
					<td></td>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="7">
						<div id="div1" style="HEIGHT: 300px; OVERFLOW: auto"><asp:datagrid id="dtgDealerStockRatio" runat="server" Width="100%" CellSpacing="1" 
								GridLines="Horizontal" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="id">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealer" runat="server" Text='<%# CType(Container.DataItem, StockActual).Dealer.DealerCode%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="VechileModel.Description" HeaderText="Model">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblModel" runat="server" Text='<%# CType(Container.DataItem, StockActual).VechileModel.Description %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="GetPeriodePK"  HeaderText="Periode PK">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblValidFrom" runat="server"  Text='<%# CType(Container.DataItem, StockActual).GetPeriodePK %> '>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Target RS (Unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTarget" runat="server" Text='<%# If(IsNothing(CType(Container.DataItem, StockActual).StockTarget), "-", CType(Container.DataItem, StockActual).StockTarget.Target)%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="Stock Ratio">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTargetRatio" runat="server" Text='<%# If(IsNothing(CType(Container.DataItem, StockActual).StockTarget), "-", CType(Container.DataItem, StockActual).StockTarget.TargetRatio)%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="Stock Dealer (end of month)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStockDealerEOM" runat="server" Text='<%# CType(Container.DataItem, StockActual).Stock%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="Ratio Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRatioDealerEOM" runat="server" Text='<%# CType(Container.DataItem, StockActual).RatioSPM%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="Ratio Saat Ini">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCurrentRatio" runat="server" Text='<%# CType(Container.DataItem, StockActual).CurrentRatio.ToString("0.00")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="Unit PK Telah Diajukan">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPKTelahDiajukan" runat="server" Text='<%# CType(Container.DataItem, StockActual).TotalUnitPK%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="Kekurangan Order">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOrderLeft" runat="server" Text='<%# CType(Container.DataItem, StockActual).GetCurrentOrderLeft%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		<script language="javascript">
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
	        <p>
						<asp:button id="btnDownLoad" runat="server" Text="DownLoad" Enabled="False" Width="64px"></asp:button>
            &nbsp;
            </p>
		</form>
		</body>
</HTML>

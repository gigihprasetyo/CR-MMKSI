<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadListVDH.aspx.vb" Inherits="FrmDownloadListVDH" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDownloadListVDH</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblSelectionData" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="10" width="100%"></td>
				</tr>
				<tr>
					<td class="titlePage">List Vehicle Data Histori- Histori Data Kendaraan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10" width="100%"></td>
				</tr>
				<TR>
					<TD>
						<div id="divData" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 400px">
							<asp:datagrid id="dtgData" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
								CellPadding="3" CellSpacing="1" AutoGenerateColumns="False">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Middle" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" VerticalAlign="Middle"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ItemNo" HeaderText="Item No.">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="GridlblItemNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNo" HeaderText="Chasis No.">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridChassisNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Serial" HeaderText="Serial No.">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridserialNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Serial") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EngineNo" HeaderText="Engine No.">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblgridEngineNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EngineNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="NIKno" HeaderText="NIK No.">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblgridNikNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NikNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ProductionYear" HeaderText="Production Year " DataFormatString="{0:dd/MM/yyyy}" ></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="MMCLotNo" HeaderText="MMC Lot No.">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMMCLot" runat="server" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="InvoiceBuy" HeaderText="Invoice Buy"></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="ReceiptCBUDate" HeaderText="Receipt CBU Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblReceiptCBUDate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ReceiptCBUDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CarrosseryTransferDate" HeaderText="Carrossery Transfer Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.CarrosseryTransferDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ReceiptCarrosseryDate" HeaderText="Receipt Carrossery Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ReceiptCarrosseryDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Customer" HeaderText="Customer"></asp:BoundColumn>
									<asp:BoundColumn DataField="EndCustomerName" HeaderText="End Customer Name"></asp:BoundColumn>
									<asp:BoundColumn DataField="EndCustomerAddress" HeaderText="End Customer Address"></asp:BoundColumn>
									<asp:BoundColumn DataField="Kelurahan" HeaderText="Kelurahan"></asp:BoundColumn>
									<asp:BoundColumn DataField="Kecamatan" HeaderText="Kecamatan"></asp:BoundColumn>
									<asp:BoundColumn DataField="Kabupaten" HeaderText="Kabupaten"></asp:BoundColumn>
									<asp:BoundColumn DataField="Propinsi" HeaderText="Propinsi"></asp:BoundColumn>
									<asp:BoundColumn DataField="R" HeaderText="R"></asp:BoundColumn>
									<asp:BoundColumn DataField="Type" HeaderText="Type"></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="RequestDate" HeaderText="Request Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.RequestDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DOPrintDate" HeaderText="DOPrint Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DOPrintDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ScheduleShipDate" HeaderText="ScheduleShip Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ScheduleShipDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SCVDate1" HeaderText="SCVDate 1">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label6" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.SCVDate1"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SVCDate2" HeaderText="SVCDate 2">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label7" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.SVCDate2"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SVCCust1" HeaderText="SVCCust 1"></asp:BoundColumn>
									<asp:BoundColumn DataField="SVCCust2" HeaderText="SVCCust 2"></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="FactureOpenDate" HeaderText="Facture Open Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label8" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.FactureOpenDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FactureDate" HeaderText="Facture Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label9" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.FactureDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="FactureNo" HeaderText="Facture No."></asp:BoundColumn>
									<asp:BoundColumn DataField="FactureComment" HeaderText="Facture Comment"></asp:BoundColumn>
									<asp:BoundColumn DataField="VATNo" HeaderText="VAT N.o"></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="VATDate" HeaderText="VAT Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label10" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.VATDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StockOutDate" HeaderText="Stock Out Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label11" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.StockOutDate"),"dd/MM/yyyy") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Orders" HeaderText="Orders"></asp:BoundColumn>
									<asp:BoundColumn DataField="PIUDNo" HeaderText="PIUD No"></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="PIUDDate" HeaderText="PIUD Date">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label12" runat="server" Text='<%# iif(DataBinder.Eval(Container, "DataItem.PIUDDate") is string.empty,"",format(DataBinder.Eval(Container, "DataItem.PIUDDate"),"dd/MM/yyyy")) %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="IncoiveSell" HeaderText="Incoive Sell"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

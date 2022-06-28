<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventProposalView.aspx.vb" Inherits="PopUpEventProposalView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpEventProposalView</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 31px" colspan="2">Proposal Event</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1" colspan="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10" colspan="2"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px" colspan="2">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" style="WIDTH: 20%; HEIGHT: 19px">Kode Dealer</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD style="WIDTH: 30%"><asp:Label ID="lblDealerCode" Runat="server"></asp:Label></TD>
								<td class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Area</td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="WIDTH: 50%;HEIGHT: 18px"><asp:Label ID="lblArea" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Kota</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5"><asp:Label ID="lblCity" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Jenis Kegiatan</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5"><asp:Label ID="lblActivityType" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Nama Kegiatan</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5">
									<asp:Label id="lblActivityName" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Periode</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5"><asp:Label ID="lblEventPeriod" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Tanggal Acara</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5">
									<asp:Label id="lblActivityDate" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Tempat</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5">
									<asp:Label id="lblActivityPlace" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px">Jumlah Undangan</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD colSpan="5">
									<asp:Label id="lblInvitationNumber" Runat="server"></asp:Label></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td id="trGridTamu" runat="server" class="titleField" colspan="2">
						Tamu/PIC KTB
						<asp:datagrid id="dtgGuest" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
							ShowFooter="False">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jenis" SortExpression="EventActivityType.EventActivityTypeName">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityType.EventActivityTypeName") %>' ID="Label1" NAME="Label1">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama" SortExpression="Item">
									<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' ID="Label2" NAME="Label2">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Jabatan" SortExpression="Description">
									<HeaderStyle Width="18%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' ID="Label3" NAME="Label3">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td class="titleField" colspan="2">
						Makanan, Minuman, Sewa Tempat
						<asp:datagrid id="dtgFoodAndBeverage" runat="server" Width="100%" AutoGenerateColumns="False"
							AllowSorting="True" ShowFooter="True">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="EventActivityType.EventActivityTypeName" HeaderText="Jenis">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityType.EventActivityTypeName") %>' ID="Label4" NAME="Label4">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item" SortExpression="Item">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' ID="Label5" NAME="Label5">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty" SortExpression="Quantity">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' ID="Label6" NAME="Label6">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Biaya Satuan" SortExpression="UnitCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' ID="Label7" NAME="Label7">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblFoodUnitCostTotal" runat="server" Text="0"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sub Total Biaya" SortExpression="TotalCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>' ID="Label8" NAME="Label8">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblFoodTotalAllCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td class="titleField" colspan="2">
						Entertainment (MC, Pengisi Acara, Artis, dll)
						<asp:datagrid id="dtgEntertainment" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
							AllowSorting="True" ShowFooter="True">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="EventActivityType.EventActivityTypeName" HeaderText="Jenis">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityType.EventActivityTypeName") %>' ID="Label10" NAME="Label10">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item" SortExpression="Item">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' ID="Label11" NAME="Label11">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty" SortExpression="Quantity">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' ID="Label12" NAME="Label12">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Biaya Satuan" SortExpression="UnitCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' ID="Label13" NAME="Label13">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblEntUnitCost" runat="server" Text="0"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sub Total Biaya" SortExpression="TotalCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>' ID="Label14" NAME="Label14">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblEntTotalUnitCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td class="titleField" colspan="2">
						Dekorasi
						<asp:datagrid id="dtgDecoration" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
							AllowSorting="True" ShowFooter="True">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="EventActivityType.EventActivityTypeName" HeaderText="Jenis">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityType.EventActivityTypeName") %>' ID="Label16" NAME="Label16">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item" SortExpression="Item">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' ID="Label17" NAME="Label17">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty" SortExpression="Quantity">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' ID="Label18" NAME="Label18">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Biaya Satuan" SortExpression="UnitCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' ID="Label19" NAME="Label19">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblDecUnitCost" runat="server" Text="0"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sub Total Biaya" SortExpression="TotalCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>' ID="Label20" NAME="Label20">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblDecTotalUnitCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td class="titleField" colspan="2">
						Display Car
						<asp:datagrid id="dtgCar" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
							AllowSorting="True" ShowFooter="False">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model" SortExpression="VechileType.VechileModel.Description">
									<HeaderStyle Width="12%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileModel.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item" SortExpression="VechileType.Description">
									<HeaderStyle Width="12%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>' ID="Label23" NAME="Label23">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty" SortExpression="Quantity">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' ID="Label24" NAME="Label24">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Keterangan" SortExpression="Description">
									<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' ID="Label25" NAME="Label25">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td class="titleField" colspan="2">
						Door Prize
						<asp:datagrid id="dtgDoorPize" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
							AllowSorting="True" ShowFooter="True">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="EventActivityType.EventActivityTypeName" HeaderText="Jenis">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityType.EventActivityTypeName") %>' ID="Label26" NAME="Label26">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item" SortExpression="Item">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' ID="Label27" NAME="Label27">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty" SortExpression="Quantity">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' ID="Label28" NAME="Label28">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Biaya Satuan" SortExpression="UnitCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' ID="Label29" NAME="Label29">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblDoorPrizeUnitCost" runat="server" Text="0"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sub Total Biaya" SortExpression="TotalCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>' ID="Label30" NAME="Label30">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblDoorPrizeTotalUnitCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"></td>
				</tr>
				<tr>
					<td class="titleField" colspan="2">
						Lain-Lain
						<asp:datagrid id="dtgOthers" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
							AllowSorting="True" ShowFooter="True">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="EventActivityType.EventActivityTypeName" HeaderText="Jenis">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityType.EventActivityTypeName") %>' ID="Label32" NAME="Label32">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item" SortExpression="Item">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' ID="Label33" NAME="Label33">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty" SortExpression="Quantity">
									<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' ID="Label34" NAME="Label34">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Biaya Satuan" SortExpression="UnitCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' ID="Label35" NAME="Label35">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblOtherUnitCost" runat="server" Text="0"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sub Total Biaya" SortExpression="TotalCost">
									<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>' ID="Label36" NAME="Label36">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="lblOtherTotalCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
										</asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 78%"></td>
					<td class="titleField" style="WIDTH: 22%">
						<asp:Label ID="lblTotalAllCost" Runat="server"></asp:Label>
					</td>
				</tr>
				<TR>
					<td class="titleField" colspan="2" style="TEXT-ALIGN: center">
						<input type="button" onclick="javascript:window.close();" value="Tutup">
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

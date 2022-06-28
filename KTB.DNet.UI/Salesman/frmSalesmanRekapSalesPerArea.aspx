<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSalesmanRekapSalesPerArea.aspx.vb" Inherits="frmSalesmanRekapSalesPerArea" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">TENAGA PENJUAL - Rekap Tenaga Penjual 
						Per Area</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">
									Unit</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 151px; HEIGHT: 17px" width="151"><asp:dropdownlist id="ddlSalesmanUnit" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 17px" width="157"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"><asp:textbox id="txtCity" runat="server" Visible="False" size="22" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Area</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 151px; HEIGHT: 10px" width="151"><asp:dropdownlist id="ddlArea" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 10px" width="157"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"><asp:button id="btnSimpan" runat="server" Width="60px" Visible="False" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Visible="False" Text="Batal" CausesValidation="False"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 29px" width="24%">Periode</TD>
								<TD style="HEIGHT: 29px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 151px; HEIGHT: 29px" noWrap width="100%" colSpan="4">
									<table id="tblDate" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 151px; HEIGHT: 11px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="WIDTH: 157px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"><asp:textbox id="txtAreaDesc" runat="server" Visible="False" size="22" MaxLength="20"></asp:textbox></TD>
								<TD style="HEIGHT: 11px"><asp:textbox id="txtAreaCode" runat="server" Visible="False" size="22" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgResult" runat="server" Width="100%" GridLines="None" CellPadding="3" BackColor="#CDCDCD"
											AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
											AllowSorting="True" PageSize="25" ShowFooter="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn SortExpression="SalesmanArea.AreaCode" HeaderText="Area">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblArea"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.TotalBM" HeaderText="SL">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblBM"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalBM" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.TotalMgr" HeaderText="SC">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblManajer"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalMgr" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.TotalAMGR" HeaderText="SPV">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblAssManajer"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalAMGR" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Totalspv1" HeaderText="SM">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblspv1"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalSPV1" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Totalspv2" HeaderText="BM">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblspv2"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalspv2" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="SalesmanHeader.Totalspv3" HeaderText="Spv3">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblspv3"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalspv3" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="SalesmanHeader.Totalsl1" HeaderText="SL1">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblsl1"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalS11" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="SalesmanHeader.Totalsl2" HeaderText="SL2">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblsl2"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalS12" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="SalesmanHeader.TotalTR" HeaderText="TR">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lbltr"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalTR" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblTotal"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalTotal" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
								<TD vAlign="top"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

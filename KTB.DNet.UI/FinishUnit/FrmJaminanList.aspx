<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmJaminanList.aspx.vb" Inherits="FrmJaminanList"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmJaminanList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						UMUM&nbsp;- Daftar Jaminan</td>
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
								<TD class="titleField" style="HEIGHT: 10px" width="24%">
									<asp:label id="Label4" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%">
									<asp:textbox id="txtDealerName" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%">
									<asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%">
									<asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="29%">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Berlaku Pada</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%" nowrap>
									<asp:TextBox id="txtBerlakuPada" runat="server" MaxLength="6" Width="60px" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
									<asp:Label id="Label2" runat="server" ForeColor="Red">MMyyyy</asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%">
									<asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgSPLHeader" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
											BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblNo"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblDealerCode" Text='<%# ctype(DataBinder.Eval(Container, "DataItem.DealerCode"),string).replace(";","; ") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DepositInfo" SortExpression="DepositInfo" HeaderText="Ket. Jaminan">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Periode">
													<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblPeriode"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblStatusJaminan" Text='<%# iif(DataBinder.Eval(Container, "DataItem.Status")=0,"Aktif","Tidak Aktif") %>' >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat SPL Detail"></asp:LinkButton>
														<asp:label id="lbtnDealer" runat="server" Width="20px" Text="Detail Dealer" visible="False">
															<img src="../images/popup.gif" border="0" alt="Lihat Dealer"></asp:label>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit SPL Detail"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" visible="False" runat="server" Width="20px" Text="Ubah" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDownload" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Lihat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
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

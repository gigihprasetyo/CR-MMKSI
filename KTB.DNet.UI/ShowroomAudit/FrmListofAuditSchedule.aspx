<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListofAuditSchedule.aspx.vb" Inherits="FrmListofAuditSchedule"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListofAuditSchedule</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM -&nbsp;Daftar&nbsp;Audit</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" width="20%">Periode</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:dropdownlist id="ddlPeriode" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Kode Dealer</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%"><asp:textbox id="txtDealerCode" Runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%"></td>
								<TD style="HEIGHT: 18px" width="1%"></TD>
								<TD style="HEIGHT: 18px" width="69%"><asp:button id="btnCari" runat="server" Text="Cari" Width="56px"></asp:button></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%"></td>
								<TD width="1%"></TD>
								<TD width="69%"></TD>
							</TR>
							<TR>
								<td vAlign="top" width="100%" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px">
										<asp:datagrid id="dtgAuditScheduleDealer" runat="server" Width="100%" AutoGenerateColumns="False"
											AllowPaging="True" PageSize="50" BorderColor="Gainsboro" BorderWidth="0px" BackColor="#CDCDCD"
											CellPadding="3" AllowSorting="True" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#EFEFEF"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False">
													<HeaderStyle Width="1%" CssClass="titleTableRsd"></HeaderStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAll('chkItemChecked',document.forms[0].chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AuditSchedule.AuditParameter.Period" HeaderText="Periode">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblItemPeriod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditSchedule.AuditParameter.Period") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblItemNoAudit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" Runat="server" text="Lihat Detail" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'  CausesValidation="False">
															<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" Runat="server" text="Ubah" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'  CausesValidation="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</TR>
							<TR>
								<TD colSpan="3"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td align="center"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

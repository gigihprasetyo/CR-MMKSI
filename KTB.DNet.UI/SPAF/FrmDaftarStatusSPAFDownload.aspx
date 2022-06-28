<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC"  %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarStatusSPAFDownload.aspx.vb" Inherits="FrmDaftarStatusSPAFDownload" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ConfirmDailyPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						<asp:Label ID="lblTitle" Runat="server"></asp:Label>
					</td>
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
								<TD class="titleField" width="22%" style="HEIGHT: 17px">
									Leasing
								</TD>
								<td>
									<asp:Label ID="lblLeasing" Runat="server"></asp:Label></td>
							</TR>
							<TR>
								<TD class="titleField" width="22%" colSpan="2">
									Periode Persetujuan :
									<asp:Label ID="lblPeriodePersetujuan" Runat="server">-</asp:Label>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" rowSpan="2" colspan="2"><asp:datagrid id="dtgSPAF" runat="server" Width="100%" PageSize="2" 
										BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
				BackColor="White" CellPadding="3" AutoGenerateColumns="False" ShowFooter ="True">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<Columns>
											<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
												<HeaderStyle Width="3%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn Visible="False">
												<HeaderStyle Width="3%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<HeaderTemplate>
													<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.all.chkAllItems.checked)"
														type="checkbox">
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle ForeColor="White" Width="5%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Status">
												<HeaderStyle ForeColor="White" Width="6%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label1" runat="server" Text='<%# CType(DataBinder.Eval(Container, "DataItem.Status"), KTB.DNet.Domain.EnumSPAFSubsidy.SPAFDocStatus).ToString %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="ReffLetter" HeaderText="No Kontrak">
												<HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DateLetter" HeaderText="Tgl Kontrak" DataFormatString="{0:dd/MM/yyyy}">
												<HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="PostingDate" HeaderText="Tgl Kirim" DataFormatString="{0:dd/MM/yyyy}">
												<HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="CustomerName" HeaderText="Nama Pelanggan">
												<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DealerLeasing" HeaderText="Dealer Leasing">
												<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="NoRangka">
												<HeaderStyle ForeColor="White" Width="16%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblCM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Dealer">
												<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblKodeDealer" runat="server"></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="SPAF per Unit">
												<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblSubsidi" runat="server" Text='<%# Iif(DataBinder.Eval(Container, "DataItem.DocType") = 0, Convert.ToInt64(DataBinder.Eval(Container, "DataItem.SPAF")).ToString("#,##0.00"), Convert.ToInt64(DataBinder.Eval(Container, "DataItem.Subsidi")).ToString("#,##0.00")) %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="PPh">
												<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label10" runat="server" Text='<%# Convert.ToInt64(DataBinder.Eval(Container, "DataItem.PPh")).ToString("#,##0.00") %>' >
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="SPAF setelah PPh">
												<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label13" runat="server" Text='<%# Convert.ToInt64(DataBinder.Eval(Container, "DataItem.AfterPPh")).ToString("#,##0.00") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="PPN">
												<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="Label14" runat="server" Text='<%# Convert.ToInt64(DataBinder.Eval(Container, "DataItem.PPn")).ToString("#,##0.00") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Notes">
												<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="LblRejectedNotes" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AlasanPenolakan") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

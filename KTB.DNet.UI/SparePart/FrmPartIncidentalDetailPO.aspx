<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalDetailPO.aspx.vb" Inherits="FrmPartIncidentalDetailPO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmPartIncidentalDetailPO</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD width="24%" colSpan="6">
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">PERMINTAAN KHUSUS - Daftar Pengajuan Permintaan Khusus 
									terhadap PO</TD>
							</TR>
							<TR>
								<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD width="24%" colSpan="6">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="0">
							<TR>
								<TD style="WIDTH: 105px; HEIGHT: 18px">
									<asp:Label id="Label1" runat="server" Font-Bold="True">Kode Dealer</asp:Label></TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px">
									<asp:Label id="lblDealerCode" runat="server" Font-Bold="True"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 105px">
									<asp:Label id="Label2" runat="server" Font-Bold="True">No Permintaan</asp:Label></TD>
								<TD>:</TD>
								<TD>
									<asp:Label id="lblReqNumber" runat="server" Font-Bold="True"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 400px">
							<asp:datagrid id="dgPartList" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BackColor="#CDCDCD"
								CellSpacing="1" BorderWidth="0px" CellPadding="2">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode" 
 HeaderText="Kode Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="PartIncidentalDetail.PartIncidentalHeader.RequestNumber" 
 HeaderText="Req-No">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblReqNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.PartIncidentalHeader.IncidentalDate" HeaderText="PK Date">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPKDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.PlanDate" HeaderText="Plan Date">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPlanDate" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="PO Number">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.SparePartMaster.PartNumber" HeaderText="Part No">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.SparePartMaster.PartName" HeaderText="Part Name">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Part Sub No">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Part Sub Name">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.Quantity" HeaderText="Org Quantity">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Right></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOrgQty" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Remain Quantity">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Right></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRemainQty" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Right></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAlokasi" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<tr>
					<td>
						<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button>
						<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

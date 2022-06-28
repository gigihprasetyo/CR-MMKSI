<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadErrorSPAF.aspx.vb" Inherits="FrmDownloadErrorSPAF"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="dgUploadData" runat="server" AutoGenerateColumns="False" BorderWidth="0px" Width="100%">
				<AlternatingItemStyle></AlternatingItemStyle>
				<HeaderStyle Font-Bold="True"></HeaderStyle>
				<Columns>
					<asp:BoundColumn HeaderText="No">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ReffLetter" HeaderText="No Kontrak">
						<HeaderStyle Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="CustomerName" HeaderText="Nama Pelanggan">
						<HeaderStyle Width="8%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="DealerLeasing" HeaderText="Dealer Leasing">
						<HeaderStyle Width="8%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn  HeaderText="No Rangka">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblNoChassis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Dealer">
						<ItemTemplate>
							<asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn  HeaderText="Harga Retail (Rp)">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblRetailPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.RetailPrice"),"#,###0") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" SortExpression="SPAF" HeaderText="Assistance Fee (Rp)">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblAssistanceFee" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SPAF"),"#,###0") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Selling Type">
						<ItemTemplate>
							<asp:Label id="lblSellingType" runat="server" Text='<%# CType(DataBinder.Eval(Container, "DataItem.SellingType"), KTB.DNet.Domain.EnumSellingType.SellingType).ToString() %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Subsidi">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblSubsidi" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Subsidi"),"#,###0") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Keterangan">
						<ItemTemplate>
							<asp:Label id="lblMessage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>

				</Columns>
			</asp:datagrid>
			
		</form>
	</body>
</HTML>

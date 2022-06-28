<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PendingOrderDetail.ascx.vb" Inherits="PendingOrderDetail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div style="display:none" id="PendingOrderDetail" runat="server">
	<asp:datagrid id="dgPendingOrderDetail" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD"
		BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False">
		<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
		<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
		<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="Tipe Order">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblOrderType" Runat="server"></asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="PO Number">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblPoNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPO.PONumber") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Sales Order">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblSalesOrder" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="No Billing">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblNoBilling" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BillingNumber") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Issue Date">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblIssueDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.IssueDate"),"dd/MM/yyyy") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Sales Amount">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblSalesAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Retail") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="PPN">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblPPN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPN") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Deposit C2">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblDepositC2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DepositC2") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Sales Amount + PPN">
				<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
				<ItemTemplate>
					<asp:Label id="lblPlus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalAmount") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:datagrid>
</div>

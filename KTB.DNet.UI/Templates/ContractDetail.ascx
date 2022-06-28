<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ContractDetail.ascx.vb" Inherits="ContractDetailxxx" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="dgContractDetail" AutoGenerateColumns="False" ForeColor="Black" GridLines="None"
	CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderColor="#E0E0E0" Width="100%" runat="server"
	CellSpacing="1">
	<SelectedItemStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="#FFFFFF"></AlternatingItemStyle>
	<ItemStyle BackColor="#FFFFFF"></ItemStyle>
	<HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
	<FooterStyle BackColor="Tan"></FooterStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Item">
			<HeaderStyle Width="9%" CssClass="titleTableSales2"></HeaderStyle>
			<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
			<ItemTemplate>
				<%# container.itemindex+1 %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Kode Tipe/Warna">
			<HeaderStyle Width="20%" CssClass="titleTableSales2"></HeaderStyle>
			<ItemTemplate>
				<asp:Label id="lblMaterialNumber" runat="server"></asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Model/Tipe/Warna">
			<HeaderStyle Width="20%" CssClass="titleTableSales2"></HeaderStyle>
			<ItemTemplate>
				<asp:Label id="lblMaterialDescription" runat="server"></asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="TargetQty" HeaderText="Unit">
			<HeaderStyle Width="10%" CssClass="titleTableSales2"></HeaderStyle>
			<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
		</asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Jumlah(Rp)">
			<HeaderStyle Width="21%" CssClass="titleTableSales2"></HeaderStyle>
			<ItemStyle HorizontalAlign="right"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>' Visible="False">
				</asp:Label>
				<asp:Label id="lblAmountString" runat="server"></asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'>
				</asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="PPh-22(Rp)">
			<HeaderStyle Width="20%" CssClass="titleTableSales2"></HeaderStyle>
			<ItemStyle HorizontalAlign="right"></ItemStyle>
			<ItemTemplate>
				<asp:Label id=lblPPh22 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh22") %>' Visible="False">
				</asp:Label>
				<asp:Label id="lblPPh22String" runat="server"></asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox id=TextBox4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh22") %>'>
				</asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="PaleGoldenrod"></PagerStyle>
</asp:datagrid>

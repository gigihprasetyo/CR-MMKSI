<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PODetail.ascx.vb" Inherits="PODetail" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="dgPODetail" runat="server" Width="632px" BorderColor="Tan" BorderWidth="1px"
	BackColor="LightGoldenrodYellow" CellPadding="2" GridLines="None" ForeColor="Black" AutoGenerateColumns="False">
	<SelectedItemStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="PaleGoldenrod"></AlternatingItemStyle>
	<HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
	<FooterStyle BackColor="Tan"></FooterStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Item">
			<ItemTemplate>
				<%# container.itemindex+1 %>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox runat="server"></asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Material Number"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Order (Unit)"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Alokasi (Unit)"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Selisih (Unit)"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Harga (Rp)"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="PPH22 (Rp)"></asp:BoundColumn>
		<asp:BoundColumn HeaderText="Description"></asp:BoundColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="PaleGoldenrod"></PagerStyle>
</asp:DataGrid>

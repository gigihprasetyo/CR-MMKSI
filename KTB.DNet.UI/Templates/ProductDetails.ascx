<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductDetails.ascx.vb" Inherits="ProductDetails" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="DataGrid1" runat="server" Width="376px" BorderColor="Tan" BorderWidth="1px"
	BackColor="LightGoldenrodYellow" CellPadding="2" GridLines="None" ForeColor="Black">
	<FooterStyle BackColor="Tan"></FooterStyle>
	<SelectedItemStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="PaleGoldenrod"></AlternatingItemStyle>
	<HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
	<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="PaleGoldenrod"></PagerStyle>
</asp:DataGrid>

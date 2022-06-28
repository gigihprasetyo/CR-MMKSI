<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDetailProduct.aspx.vb" Inherits="FrmDetailProduct" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDetailProduct</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 96px; POSITION: absolute; TOP: 56px" runat="server"
				Width="112px">Product Code</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 102; LEFT: 96px; POSITION: absolute; TOP: 88px" runat="server"
				Width="112px">Product Name</asp:Label>
			<asp:Label id="Label3" style="Z-INDEX: 103; LEFT: 96px; POSITION: absolute; TOP: 120px" runat="server"
				Width="128px">Description Product</asp:Label>
			<asp:TextBox id="txt_ProductCode" style="Z-INDEX: 104; LEFT: 272px; POSITION: absolute; TOP: 48px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="txt_ProductName" style="Z-INDEX: 105; LEFT: 272px; POSITION: absolute; TOP: 80px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="txt_DescriptionProduct" style="Z-INDEX: 106; LEFT: 272px; POSITION: absolute; TOP: 112px"
				runat="server"></asp:TextBox>
			<asp:Label id="Label4" style="Z-INDEX: 107; LEFT: 184px; POSITION: absolute; TOP: 16px" runat="server">Master Table</asp:Label>
			<asp:Label id="Label5" style="Z-INDEX: 108; LEFT: 184px; POSITION: absolute; TOP: 176px" runat="server"
				Width="80px">Detail Table</asp:Label>
			<asp:Label id="Label6" style="Z-INDEX: 109; LEFT: 96px; POSITION: absolute; TOP: 216px" runat="server"
				Width="112px" Height="11px">Product ID</asp:Label>
			<asp:Label id="Label7" style="Z-INDEX: 110; LEFT: 96px; POSITION: absolute; TOP: 248px" runat="server"
				Width="112px">Basic Product ID</asp:Label>
			<asp:TextBox id="txt_ProductID" style="Z-INDEX: 111; LEFT: 272px; POSITION: absolute; TOP: 208px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="txt_BasicProductID" style="Z-INDEX: 112; LEFT: 272px; POSITION: absolute; TOP: 240px"
				runat="server"></asp:TextBox>
			<asp:Label id="Label8" style="Z-INDEX: 113; LEFT: 96px; POSITION: absolute; TOP: 280px" runat="server"
				Width="160px">Description Product Detail</asp:Label>
			<asp:TextBox id="txt_DescriptionProductDetail" style="Z-INDEX: 114; LEFT: 272px; POSITION: absolute; TOP: 272px"
				runat="server"></asp:TextBox>
			<asp:Button id="btn_insert" style="Z-INDEX: 115; LEFT: 96px; POSITION: absolute; TOP: 320px"
				runat="server" Width="96px" Text="Insert"></asp:Button>
		</form>
	</body>
</HTML>

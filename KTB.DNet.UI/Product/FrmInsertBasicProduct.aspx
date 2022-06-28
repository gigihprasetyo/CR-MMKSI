<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmInsertBasicProduct.aspx.vb" Inherits="FrmInsertBasicProduct" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmInsertBasicProduct</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="txt_BasicProductCode" style="Z-INDEX: 101; LEFT: 152px; POSITION: absolute; TOP: 48px"
				runat="server"></asp:TextBox>
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 48px" runat="server">Basic Product Code</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 80px" runat="server">Basic Product Name</asp:Label>
			<asp:Label id="Label3" style="Z-INDEX: 104; LEFT: 8px; POSITION: absolute; TOP: 112px" runat="server"
				Width="120px">Description</asp:Label>
			<asp:TextBox id="txt_BasicProductName" style="Z-INDEX: 105; LEFT: 152px; POSITION: absolute; TOP: 80px"
				runat="server"></asp:TextBox>
			<asp:TextBox id="txt_BasicProductDescription" style="Z-INDEX: 106; LEFT: 152px; POSITION: absolute; TOP: 112px"
				runat="server"></asp:TextBox>
			<asp:Button id="btn_Save" style="Z-INDEX: 107; LEFT: 88px; POSITION: absolute; TOP: 160px" runat="server"
				Width="96px" Text="Save"></asp:Button>
			<asp:Button id="btn_Update" style="Z-INDEX: 108; LEFT: 208px; POSITION: absolute; TOP: 160px"
				runat="server" Width="96px" Text="Update"></asp:Button>
			<asp:Label id="Label4" style="Z-INDEX: 109; LEFT: 8px; POSITION: absolute; TOP: 16px" runat="server"
				Width="120px">Basic Product ID</asp:Label>
			<asp:TextBox id="txt_BasicProductID" style="Z-INDEX: 110; LEFT: 152px; POSITION: absolute; TOP: 16px"
				runat="server"></asp:TextBox>
			<asp:Button id="btn_Delete" style="Z-INDEX: 111; LEFT: 328px; POSITION: absolute; TOP: 160px"
				runat="server" Width="96px" Text="Delete"></asp:Button></form>
	</body>
</HTML>

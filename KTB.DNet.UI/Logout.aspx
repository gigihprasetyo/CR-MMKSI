<%@ Register TagPrefix="uc1" TagName="DealerSelection" Src="UserControl/DealerSelection.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Logout.aspx.vb" Inherits="Logout" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Logout</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:button id="Button1" style="Z-INDEX: 101; LEFT: 208px; POSITION: absolute; TOP: 216px" runat="server"
				Text="Button"></asp:button><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 312px; POSITION: absolute; TOP: 184px"
				runat="server"></asp:textbox><asp:datagrid id="DataGrid1" style="Z-INDEX: 103; LEFT: 448px; POSITION: absolute; TOP: 208px"
				runat="server"></asp:datagrid><asp:button id="btnSpMaster" style="Z-INDEX: 104; LEFT: 192px; POSITION: absolute; TOP: 336px"
				runat="server" Text="SpMaster"></asp:button><asp:button id="Button3" style="Z-INDEX: 105; LEFT: 56px; POSITION: absolute; TOP: 368px" runat="server"
				Text="SendMail"></asp:button><asp:button id="Button4" style="Z-INDEX: 106; LEFT: 136px; POSITION: absolute; TOP: 272px" runat="server"
				Text="Annual Discount"></asp:button><asp:button id="Button5" style="Z-INDEX: 107; LEFT: 304px; POSITION: absolute; TOP: 312px" runat="server"
				Text="Buttonfff"></asp:button><asp:button id="btnSPPOEstimate" style="Z-INDEX: 108; LEFT: 312px; POSITION: absolute; TOP: 248px"
				runat="server" Text="SPPOEstimate"></asp:button><asp:button id="btnSPPOChecklist" style="Z-INDEX: 109; LEFT: 576px; POSITION: absolute; TOP: 288px"
				runat="server" Text="SPPOChecklist" CausesValidation="False" Width="112px"></asp:button><asp:button id="Button2" style="Z-INDEX: 110; LEFT: 16px; POSITION: absolute; TOP: 232px" runat="server"
				Text="DPParser" Width="88px"></asp:button>
			<asp:CheckBoxList id="CheckBoxList1" style="Z-INDEX: 111; LEFT: 296px; POSITION: absolute; TOP: 24px"
				runat="server" Width="1px" Height="24px" RepeatColumns="2" CellPadding="1" CellSpacing="1" BorderStyle="Dotted">
				<asp:ListItem Value="11">1</asp:ListItem>
				<asp:ListItem Value="22">2</asp:ListItem>
				<asp:ListItem Value="33">3</asp:ListItem>
				<asp:ListItem Value="4">4</asp:ListItem>
				<asp:ListItem Value="5">5</asp:ListItem>
				<asp:ListItem Value="66">6</asp:ListItem>
				<asp:ListItem Value="9">9</asp:ListItem>
			</asp:CheckBoxList><asp:button id="Button6" runat="server" Text="Button" style="Z-INDEX: 112; LEFT: 552px; POSITION: absolute; TOP: 328px"></asp:button>
			<asp:ListBox id="ListBox1" style="Z-INDEX: 113; LEFT: 368px; POSITION: absolute; TOP: 48px" runat="server"
				Width="176px" Height="48px" Font-Bold="True">
				<asp:ListItem Value="1">1</asp:ListItem>
				<asp:ListItem Value="2">2</asp:ListItem>
				<asp:ListItem Value="3">3</asp:ListItem>
				<asp:ListItem Value="4">4</asp:ListItem>
				<asp:ListItem Value="5">5</asp:ListItem>
				<asp:ListItem></asp:ListItem>
			</asp:ListBox>
			<asp:DropDownList id="DropDownList1" style="Z-INDEX: 114; LEFT: 304px; POSITION: absolute; TOP: 384px"
				runat="server"></asp:DropDownList>
			<asp:DropDownList id="DropDownList2" style="Z-INDEX: 115; LEFT: 712px; POSITION: absolute; TOP: 96px"
				runat="server"></asp:DropDownList>
			<asp:Button id="Button7" style="Z-INDEX: 116; LEFT: 736px; POSITION: absolute; TOP: 136px" runat="server"
				Text="Button"></asp:Button>
			<asp:TextBox id="txtpwd1" style="Z-INDEX: 117; LEFT: 72px; POSITION: absolute; TOP: 16px" runat="server"></asp:TextBox>
			<asp:TextBox id="txtpwd2" style="Z-INDEX: 118; LEFT: 72px; POSITION: absolute; TOP: 48px" runat="server"></asp:TextBox>
			<asp:Button id="Button8" style="Z-INDEX: 119; LEFT: 80px; POSITION: absolute; TOP: 80px" runat="server"
				Text="Compare"></asp:Button>
		</form>
	</body>
</HTML>

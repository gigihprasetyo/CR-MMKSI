<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDescripsion.aspx.vb" Inherits="PopUpDescripsion" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDescripsion</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="lblDescription" style="Z-INDEX: 100; LEFT: 8px; POSITION: absolute; TOP: 8px"
				runat="server" Width="88px">Keterangan   :</asp:Label>
			<asp:Button id="btnClose" style="Z-INDEX: 105; LEFT: 168px; POSITION: absolute; TOP: 120px"
				runat="server" Text="Close"></asp:Button>
			<asp:Button id="btnCancel" style="Z-INDEX: 104; LEFT: 88px; POSITION: absolute; TOP: 120px"
				runat="server" Width="72px" Text="Batal"></asp:Button>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 32px" runat="server"
				Width="400px" Height="72px" TextMode="MultiLine"></asp:TextBox>
			<asp:Button id="btnSave" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 120px" runat="server"
				Text="Simpan"></asp:Button>
		</form>
	</body>
</HTML>

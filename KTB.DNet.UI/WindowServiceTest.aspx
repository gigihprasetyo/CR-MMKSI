<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WindowServiceTest.aspx.vb" Inherits="WindowServiceTest" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WindowServiceTest</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="Button1" style="Z-INDEX: 100; LEFT: 64px; POSITION: absolute; TOP: 40px" runat="server"
				Text="WS Spare Part Master" Width="168px"></asp:Button><INPUT id="fUpLabor" style="Z-INDEX: 129; LEFT: 248px; POSITION: absolute; TOP: 376px"
				type="file" name="File1" runat="server">
			<asp:Button id="Button7" style="Z-INDEX: 128; LEFT: 64px; POSITION: absolute; TOP: 376px" runat="server"
				Width="168px" Text="Upload Labor Master"></asp:Button><INPUT id="fUpBillingSPPOs" style="Z-INDEX: 126; LEFT: 248px; POSITION: absolute; TOP: 328px"
				type="file" name="File1" runat="server">
			<asp:Button id="btnUploadBillingSPPO" style="Z-INDEX: 127; LEFT: 64px; POSITION: absolute; TOP: 328px"
				runat="server" Width="168px" Text="Upload Billing SPPO Recap"></asp:Button><INPUT id="fUpBillingSPPO" style="Z-INDEX: 124; LEFT: 248px; POSITION: absolute; TOP: 328px"
				type="file" name="File1" runat="server"><INPUT id="fUpSpecialItem" style="Z-INDEX: 123; LEFT: 248px; POSITION: absolute; TOP: 296px"
				type="file" name="File1" runat="server">
			<asp:Button id="btnUpSpecialItem" style="Z-INDEX: 122; LEFT: 64px; POSITION: absolute; TOP: 296px"
				runat="server" Width="168px" Text="Upload Special Item"></asp:Button><INPUT id="fDepoC2" style="Z-INDEX: 121; LEFT: 248px; POSITION: absolute; TOP: 264px" type="file"
				name="File1" runat="server"><INPUT id="fileDepositC2Sync" style="Z-INDEX: 120; LEFT: 248px; POSITION: absolute; TOP: 264px"
				type="file" name="File1" runat="server">
			<asp:Button id="btnDepositC2Sync" style="Z-INDEX: 119; LEFT: 64px; POSITION: absolute; TOP: 264px"
				runat="server" Width="168px" Text="DepositC2 Sync"></asp:Button><INPUT id="File2" style="Z-INDEX: 117; LEFT: 248px; POSITION: absolute; TOP: 232px" type="file"
				name="File1" runat="server"><INPUT id="fileDepositSync" style="Z-INDEX: 118; LEFT: 248px; POSITION: absolute; TOP: 232px"
				type="file" name="File1" runat="server">
			<asp:Button id="btnDepositSync" style="Z-INDEX: 116; LEFT: 64px; POSITION: absolute; TOP: 232px"
				runat="server" Width="168px" Text="Deposit Sync"></asp:Button><INPUT id="fInvoiceSync" style="Z-INDEX: 115; LEFT: 248px; POSITION: absolute; TOP: 200px"
				type="file" name="File1" runat="server"><INPUT id="fbInvoiceSync" style="Z-INDEX: 114; LEFT: 248px; POSITION: absolute; TOP: 200px"
				type="file" name="File1" runat="server">
			<asp:Button id="btnInvoiceSync" style="Z-INDEX: 113; LEFT: 64px; POSITION: absolute; TOP: 200px"
				runat="server" Width="168px" Text="Invoice Sync"></asp:Button>
			<asp:Button id="btnWSCStatus" style="Z-INDEX: 112; LEFT: 64px; POSITION: absolute; TOP: 168px"
				runat="server" Width="168px" Text="WSC Status Update"></asp:Button><INPUT id="fWSCUpdate" style="Z-INDEX: 111; LEFT: 248px; POSITION: absolute; TOP: 168px"
				type="file" name="File1" runat="server">
			<asp:Button id="Button6" style="Z-INDEX: 105; LEFT: 64px; POSITION: absolute; TOP: 136px" runat="server"
				Width="168px" Text="WS PO Status"></asp:Button><INPUT id="DF4" style="Z-INDEX: 109; LEFT: 248px; POSITION: absolute; TOP: 136px" type="file"
				name="File1" runat="server"><INPUT id="DF3" style="Z-INDEX: 108; LEFT: 248px; POSITION: absolute; TOP: 104px" type="file"
				name="File1" runat="server"><INPUT id="DF2" style="Z-INDEX: 107; LEFT: 248px; POSITION: absolute; TOP: 72px" type="file"
				name="File1" runat="server">
			<asp:Button id="Button4" style="Z-INDEX: 104; LEFT: 64px; POSITION: absolute; TOP: 136px" runat="server"
				Text="WS PO Status" Width="168px"></asp:Button>
			<asp:Button id="Button3" style="Z-INDEX: 103; LEFT: 64px; POSITION: absolute; TOP: 72px" runat="server"
				Text="WS PO Check List" Width="168px"></asp:Button>
			<asp:Button id="Button2" style="Z-INDEX: 101; LEFT: 64px; POSITION: absolute; TOP: 104px" runat="server"
				Text="WS PO Estimation"></asp:Button>
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 32px; POSITION: absolute; TOP: 8px" runat="server"
				ForeColor="Red" Font-Bold="True" Font-Size="Large">Syncronization Testing</asp:Label><INPUT id="DF1" style="Z-INDEX: 106; LEFT: 248px; POSITION: absolute; TOP: 40px" type="file"
				runat="server">
			<asp:Button id="Button5" style="Z-INDEX: 110; LEFT: 520px; POSITION: absolute; TOP: 40px" runat="server"
				Text="Button"></asp:Button>
		</form>
	</body>
</HTML>

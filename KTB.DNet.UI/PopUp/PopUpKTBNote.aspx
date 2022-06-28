<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpKTBNote.aspx.vb" Inherits="PopUpKTBNote"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpKTBNote</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td><asp:Label ID="lblNote" Runat="server" CssClass="titlePage">Catatan MKS</asp:Label>
						<br>
						<br>
						<asp:Literal ID="ltrKTBNote" Runat="server" />
						<asp:TextBox ID="txtKTBNote" Runat="server" TextMode="MultiLine" Width="392px" Height="184px" />
						<br>
						<br>
						<asp:Button ID="btnSave" Runat="server" Text="Simpan" CssClass="hideButtonOnPrint" />
						<asp:Button id="btnCancel" runat="server" Text="Kembali"></asp:Button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

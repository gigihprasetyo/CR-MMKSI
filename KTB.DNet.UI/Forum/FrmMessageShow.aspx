<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMessageShow.aspx.vb" Inherits="FrmMessageShow"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMessageShow</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPForumMemberSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpForumMemberOne.aspx','',500,760,UserSelection);
		}
		function UserSelection(selectedUser)
		{
			var userInfo = selectedUser.split(';');
			var txtUserName= document.getElementById("txtUserName");
			var txthdnField = document.getElementById("txtTempField");
			txtUserName.value = userInfo[1];		
			txthdnField.value = userInfo[0];		
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FORUM&nbsp;-&nbsp;<asp:label id="lblTitle" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titleField">Subject</td>
					<td class="titleField">:</td>
					<td><asp:textbox id="txtSubject" Runat="server" Width="384px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField">UserName</td>
					<td class="titleField">:</td>
					<td><asp:label id="lblUserReplay" runat="server"></asp:label><asp:textbox id="txtTempField" runat="server" Width="2px" BorderWidth="0px" BackColor="White"
							BorderColor="White" ForeColor="White" BorderStyle="None"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:Label ID="lblViewMsg" Runat="server" Visible="False" Width="100%" BorderWidth="1px"></asp:Label><FCKEDITORV2:CKEditorControl id="txtForumPMMsg" runat="server" BasePath="../UserControl/fckeditor/"></FCKEDITORV2:CKEditorControl></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:button id="btnSend" runat="server" Width="64px" Text="Kirim"></asp:button><asp:button id="btnReplay" runat="server" Width="64px" Text="Reply"></asp:button><asp:button id="btnResend" runat="server" Text="Kirim Ulang"></asp:button>
						<asp:button id="btnDel" runat="server" Width="64px" Text="Hapus"></asp:button>
						<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

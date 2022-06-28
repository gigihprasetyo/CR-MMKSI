<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSPAFDokumenUpload.aspx.vb" Inherits="PopUpSPAFDokumenUpload"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpSPAFDokumenUpload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<TD class="titleField">
						Upload File: <INPUT id="fileUploadSPAFDoc" style="WIDTH: 264px; HEIGHT: 20px" type="file" size="24"
							name="fileUpload" runat="server">
						<asp:Button id="btnUpload" runat="server" Text="Upload"></asp:Button>
					</TD>
				</tr>
			</TABLE>
			<asp:Panel id="pnlRunCloseWindow" runat="server" Visible="False">
				<script language="javascript">
					alert('Upload file berhasil');
					window.close();
				</script>
			</asp:Panel>
		</form>
	</body>
</HTML>

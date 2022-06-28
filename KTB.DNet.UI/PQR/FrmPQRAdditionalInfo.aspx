<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRAdditionalInfo.aspx.vb" Inherits="FrmPQRAdditionalInfo" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PQR - Detil Informasi Tambahan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<script language="javascript" type="text/javascript">
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PRODUCT QUALITY REPORT -&nbsp; Informasi Tambahan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<br>
			<table width="70%">
				<tr class="titleField">
					<td>No PQR</td>
					<td width="1%">:</td>
					<td><asp:label id="lblPQRNo" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:panel id="pnlViewPQRAdditionalInfo" Height="380px" wrap="False" Width="100%" Runat="server"></asp:panel></td>
				</tr>
				<tr>
					<td colSpan="3">
						<div id="DivEntry" style="WIDTH: 100%" runat="server">
							<table width="100%">
								<tr valign="top">
									<td class="titleField">'Pertanyaan/Jawaban'
									</td>
									<td width="1%">:</td>
									<td><asp:textbox id="txtPengirim" Width="442px" Runat="server" Rows="5" TextMode="MultiLine"></asp:textbox></td>
								</tr>
								<tr valign="top">
									<td class="titleField">Attachment
									</td>
									<td width="1%">:</td>
									<td><INPUT id="inFileLocation" onkeydown="return false;" style="WIDTH: 440px; HEIGHT: 22px"
											type="file" size="54" name="File1" runat="server">
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td colSpan="3"><asp:button id="btnSimpan" Runat="server" Text="Simpan"></asp:button><asp:button id="btnCancel" Runat="server" Text="Kembali"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

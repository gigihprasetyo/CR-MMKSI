<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpMMKSINotesClaim.aspx.vb" Inherits="PopUpMMKSINotesClaim"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpMMKSINotesClaim</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
            function RedirectAfterSave(id)
			{
                var urlDefault = "../Benefit/FrmInputClaim.aspx?Mode=View&id=" + id;
				alert("Simpan Berhasil");
				window.location = urlDefault;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
                <tr>
                    <td class="titlePage" colspan="3">SALES CAMPAIGN - Keterangan MMKSI</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1" colspan="3">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10" colspan="3">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
				<tr valign="top">
					<td><asp:Label ID="Label1" Runat="server" CssClass="titleField">Nomor Claim</asp:Label></td>
					<td>:</td>
					<td><asp:Label ID="lblClaimRegNo" Runat="server" Text="ClaimRegNo" ></asp:Label></td>
				</tr>
				<tr valign="top">
					<td><asp:Label ID="Label2" Runat="server" CssClass="titleField">Detail Keterangan MMKSI</asp:Label>
					<td>:</td>
					<td><asp:TextBox ID="txtNotesMMKSI" Runat="server" TextMode="MultiLine" Width="392px" Height="184px" /></td>
				</tr>
				<tr>
                    <td></td>
                    <td></td>
					<td>
                        <asp:Button ID="btnSave" Runat="server" Text="Simpan" CssClass="hideButtonOnPrint" />&nbsp;
						<asp:Button id="btnCancel" runat="server" Text="Kembali"></asp:Button>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmUploadWSCEvidenceBB.aspx.vb" Inherits="frmUploadWSCEvidenceBBBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WSC - Upload Bukti WSC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td colSpan="4">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">WSC - Upload Bukti WSC (Special)</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="24%"><asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></TD>
					<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD width="40%"><asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;/
						<asp:label id="lblSearchTerm1" runat="server"></asp:label></TD>
					<TD width="35%"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNomorWSC" runat="server">Nomor WSC</asp:label></TD>
					<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
					<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNomorWSC" onblur="alphaNumericPlusBlur(txtNomorWSC)"
							runat="server" Width="140"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<tr>
					<TD class="titleField"><asp:label id="Label3" runat="server">Tipe Bukti WSC</asp:label></TD>
					<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlEvidenceType" runat="server" Width="140"></asp:dropdownlist></TD>
					<TD></TD>
				</tr>
				<TR>
					<TD class="titleField"><asp:label id="lblLampiranBukti" runat="server">Lampiran Bukti</asp:label></TD>
					<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
					<TD><INPUT onkeypress="return false;" id="DataFile1" type="file" size="40" name="File1" runat="server"></TD>
					<TD><asp:label id="Label1" runat="server" ForeColor="Red" EnableViewState="False">*Max 3 MB</asp:label></TD>
				</TR>
				<TR vAlign="top">
					<TD class="titleField"><asp:label id="lblKeterangan" runat="server">Keterangan</asp:label></TD>
					<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtKeterangan" runat="server" TextMode="MultiLine" Rows="10" cols="55"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD><asp:button id="btnUpload" runat="server" Text="Upload"></asp:button><asp:button id="btnClear" runat="server" Text="Hapus"></asp:button></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>

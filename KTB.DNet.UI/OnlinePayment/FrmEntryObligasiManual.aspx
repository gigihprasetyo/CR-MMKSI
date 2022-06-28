<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryObligasiManual.aspx.vb" Inherits="FrmEntryObligasiManual"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Informasi Pembayaran - Tipe Obligasi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 20px">INFORMASI PEMBAYARAN&nbsp;- 
						Pembayaran&nbsp;Manual</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="3"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="3"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblDealerCode" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblNamaDealer" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Alamat</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:label id="lblAlamat" runat="server">Label</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<STRONG>&nbsp;</STRONG></td>
							</TR>
							<TR>
								<TD><STRONG>Perihal</STRONG></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlTipeAssignment" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD><STRONG>Tgl Transaksi</STRONG></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTglTransaksi" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD><STRONG>Tgl Jatuh Tempo</STRONG></TD>
								<TD>:</TD>
								<TD><CC1:INTICALENDAR id="icTransDate" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></TD>
							</TR>
							<TR>
								<TD><STRONG>Jumlah</STRONG></TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtJumlah" onblur="omitSomeCharacter('txtJumlah','-')"
										onkeyup="pic(this,this.value,'9999999999','N')" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><STRONG>Keterangan</STRONG></TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtKeterangan" runat="server" Width="300px"></asp:textbox></TD>
							</TR>
							<TR>
								<td></td>
								<td></td>
								<TD><asp:textbox id="txtDeskripsi" runat="server" Width="416px" ReadOnly="True" Rows="4" TextMode="MultiLine">Bla..bla..bla</asp:textbox></TD>
							</TR>
							<TR>
								<td></td>
								<td></td>
								<TD style="HEIGHT: 28px" colSpan="3"><asp:checkbox id="chkAgreement" runat="server" Text="Saya telah membaca dam verifikasi bedua datu  dana  kekuarag."></asp:checkbox></TD>
							</TR>
							<TR>
								<td></td>
								<td></td>
								<TD><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnReset" runat="server" Width="60px" Text="Baru" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
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

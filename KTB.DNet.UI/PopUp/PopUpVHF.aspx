<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpVHF.aspx.vb" Inherits="PopUpVHF" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpVHF</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblTitle" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" width="100%">Vehicle Data History Detail</td>
				</tr>
				<tr>
					<td width="100%" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td width="100%" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE id="tblSelectionData" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td align="right" width="15%"><asp:label id="Label120" runat="server" CssClass="titleField">Item No </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label121" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="220"><asp:label id="lblItemNo" runat="server"></asp:label></td>
					<td width="15%"></td>
					<td width="1%"></td>
					<td width="15%"></td>
					<td width="12%"></td>
					<td width="1%"></td>
					<td width="12%"></td>
				</tr>
				<!--tr>
					<td width="100%" colSpan="6">
						<table id="tblChassis" width="100%"-->
				<tr>
					<td align="right"><asp:label id="Label1" runat="server" CssClass="titleField">Chassis</asp:label></td>
					<td align="right"><asp:label id="Label2" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left"><asp:label id="lblChassis" runat="server"></asp:label></td>
					<td align="right"><asp:label id="Label3" runat="server" CssClass="titleField">MMC Lot#</asp:label></td>
					<td><asp:label id="Label4" CssClass="titleField" Runat="server">:</asp:label></td>
					<td><asp:label id="lblMMC" runat="server"></asp:label></td>
					<td align="right"><asp:label id="Label5" runat="server" CssClass="titleField">Serial</asp:label></td>
					<td><asp:label id="Label6" CssClass="titleField" Runat="server">:</asp:label></td>
					<td><asp:label id="lblSerial" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td align="right"><asp:label id="Label7" runat="server" CssClass="titleField">Engine</asp:label></td>
					<td align="right"><asp:label id="Label8" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left"><asp:label id="lblEngine" runat="server"></asp:label></td>
					<td align="right"><asp:label id="Label10" runat="server" CssClass="titleField">Invoice</asp:label></td>
					<td><asp:label id="Label11" CssClass="titleField" Runat="server">:</asp:label></td>
					<td><asp:label id="lblInvoice" runat="server"></asp:label></td>
					<td align="right" width="12%"><asp:label id="Label13" runat="server" CssClass="titleField">Order</asp:label></td>
					<td width="1%"><asp:label id="Label14" CssClass="titleField" Runat="server">:</asp:label></td>
					<td width="12%"><asp:label id="lblOrder" runat="server"></asp:label></td>
				</tr>
				<!--/table>
					</td>
				</tr>
				<tr>
					<td width="100%" colSpan="6">
						<table id="tblprodYear"-->
				<tr>
					<td align="right" width="15%"><asp:label id="Label9" runat="server" CssClass="titleField">Prod Year</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label12" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblProdYear" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label15" runat="server" CssClass="titleField">PIUD No </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label16" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblPIUDNo" runat="server"></asp:label></td>
					<td align="right" width="12%"><asp:label id="Label18" runat="server" CssClass="titleField">PIUD Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label19" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="12%"><asp:label id="lblPIUDDate" runat="server"></asp:label></td>
				</tr>
				<!--/table>
					</td>
				</tr-->
				<tr>
					<td align="right" width="15%"><asp:label id="Label17" runat="server" CssClass="titleField">Receipt CBU Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label20" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblReceiptCBUDate" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label21" runat="server" CssClass="titleField">Request Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label22" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblRequestDate" runat="server"></asp:label></td>
					<td width="12%"></td>
					<td width="1%"></td>
					<td width="12%"></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label23" runat="server" CssClass="titleField">Carrossery Transfer Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label24" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblCTD" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label26" runat="server" CssClass="titleField">D/O Print Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label27" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblDOdate" runat="server"></asp:label></td>
					<td width="12%"></td>
					<td width="1%"></td>
					<td width="12%"></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label25" runat="server" CssClass="titleField">Receipt Carrossery Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label28" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblRCD" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label30" runat="server" CssClass="titleField">Schedule Ship Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label31" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblSSD" runat="server"></asp:label></td>
					<td width="12%"></td>
					<td width="1%"></td>
					<td width="12%"></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label29" runat="server" CssClass="titleField">Stock Out Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label33" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="20%"><asp:label id="lblStockOutDate" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label35" runat="server" CssClass="titleField">NIK No</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label36" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblNIKNo" runat="server"></asp:label></td>
					<td width="12%"></td>
					<td width="1%"></td>
					<td width="12%"></td>
				</tr>
				<tr>
					<td colspan="9">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="9" class="titleField">CUSTOMER INFORMATION</td>
				</tr>
				<tr>
					<td colspan="9"><table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label34" runat="server" CssClass="titleField">Customer </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label37" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="80%" colSpan="4"><asp:label id="lblCustomer" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label38" runat="server" CssClass="titleField">End User Name </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label39" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="50%" colspan="4"><asp:label id="lblEndUser" runat="server"></asp:label></td>
					<td align="right" width="12%"><asp:label id="Label40" runat="server" CssClass="titleField">Type</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label41" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="12%"><asp:label id="lblType" runat="server"></asp:label></td>
				</tr>
				<tr valign="top">
					<td align="right" width="15%"><asp:label id="Label42" runat="server" CssClass="titleField">End User Address </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label43" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%" colspan="4"><asp:label id="lblEndUserAddress" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label45" runat="server" CssClass="titleField">R</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label46" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblR" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label44" runat="server" CssClass="titleField">Kelurahan </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label47" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblkelurahan" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label49" runat="server" CssClass="titleField">Kecamatan</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label50" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblKecamatan" runat="server"></asp:label></td>
					<td width="15%"></td>
					<td width="1%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label52" runat="server" CssClass="titleField">Kab / Kodya </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label53" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblKabKodya" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label55" runat="server" CssClass="titleField">Propinsi</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label56" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblPropinsi" runat="server"></asp:label></td>
					<td width="15%"></td>
					<td width="1%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td colspan="9">&nbsp;</td>
				</tr>
				<!--tr>
					<td width="100%" colSpan="6">
						<table id="tblFO"-->
				<tr>
					<td align="right" width="15%"><asp:label id="Label48" runat="server" CssClass="titleField">Facture Open Date </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label51" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblFactureDate" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label57" runat="server" CssClass="titleField">Facture No</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label58" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblFactureNo" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label60" runat="server" CssClass="titleField">Facture Command </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label61" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblFactureCommand" runat="server"></asp:label></td>
				</tr>
				<!--/table>
					</td>
				</tr-->
				<tr>
					<td align="right" width="15%"><asp:label id="Label54" runat="server" CssClass="titleField">V.A.T No </asp:label></td>
					<td align="right" width="1%"><asp:label id="Label59" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblVATNo" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label63" runat="server" CssClass="titleField">V.A.T Date</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label64" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblVatDate" runat="server"></asp:label></td>
					<td width="15%"></td>
					<td width="1%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label62" runat="server" CssClass="titleField">Svc Date I</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label65" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblSVCDateI" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label67" runat="server" CssClass="titleField">Svc Date II</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label68" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblSVCDateII" runat="server"></asp:label></td>
					<td width="15%"></td>
					<td width="1%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td align="right" width="15%"><asp:label id="Label66" runat="server" CssClass="titleField">Svc Cust I</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label69" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblSvcCustI" runat="server"></asp:label></td>
					<td align="right" width="15%"><asp:label id="Label71" runat="server" CssClass="titleField">Svc Cust II</asp:label></td>
					<td align="right" width="1%"><asp:label id="Label72" CssClass="titleField" Runat="server">:</asp:label></td>
					<td align="left" width="15%"><asp:label id="lblSvcCustII" runat="server"></asp:label></td>
					<td width="15%"></td>
					<td width="1%"></td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td width="100%" colSpan="9"></td>
				</tr>
				<tr>
					<td align="center" width="100%" colSpan="9"><asp:button id="btnClose" runat="server" CausesValidation="False" Text="Tutup" Width="56px"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanTurnOverAnalisa.aspx.vb" Inherits="FrmSalesmanTurnOverAnalisa" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 16px">Sales Force Turn Over Analisa</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:textbox id="txtDealerCode" runat="server" ReadOnly="True" MaxLength="20" size="22"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Periode Awal</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:dropdownlist id="ddlMonthStart" runat="server" AutoPostBack="true" Width="152px"></asp:dropdownlist>
									<asp:textbox id="txtYearStart" runat="server" onkeypress="return NumericOnlyWith(event,'');"
										onblur="NumericOnlyBlurWith(txtYearStart,'');" MaxLength="4"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Periode Akhir</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"><asp:dropdownlist id="ddlMonthEnd" runat="server" AutoPostBack="true" Width="152px"></asp:dropdownlist>
									<asp:textbox id="txtYearEnd" runat="server" onkeypress="return NumericOnlyWith(event,'');" onblur="NumericOnlyBlurWith(txtYearEnd,'');"
										MaxLength="4"></asp:textbox></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 36px">Analisa</TD>
								<TD style="HEIGHT: 36px"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 36px"><asp:textbox id="txtAnalisa" runat="server" MaxLength="300" size="22" Width="362px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 24px"><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button><INPUT id="btnTutup" onclick="window.close()" type="button" value="Tutup" name="btnKembali"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 230px"></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

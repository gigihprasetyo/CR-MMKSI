<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpMaxPO.aspx.vb" Inherits="PopUpMaxPO" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUp MaxPO Calculation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px"></td>
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
							<TBODY>
								<TR class="titleTableSales">
									<TD class="titleTableSales" style="WIDTH: 100%; HEIGHT: 17px" width="149">Detail 
										Perhitungan Maksimum PO Dapat Diajukan
									</TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 100%; HEIGHT: 18px" width="149"><asp:label id="lblTitle" runat="server" Width="95%"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 100%; HEIGHT: 17px" width="149">=<asp:label id="lblPattern" runat="server" Width="95%"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 100%; HEIGHT: 17px" width="149">=<asp:label id="lblCalculation" runat="server" Width="95%"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 100%; HEIGHT: 17px" width="149">=<asp:label id="lblResult" runat="server" Width="95%"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 100%; HEIGHT: 17px" width="149"></TD>
								</TR>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="7">
						<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel" runat="server"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

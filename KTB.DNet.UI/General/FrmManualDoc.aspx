<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmManualDoc.aspx.vb" Inherits="FrmManualDoc" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmManualDoc</title>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">BULETIN & MANUAL  - Manual Doc</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titleField" width="24%">Nama Manual</td>
					<td width="1%">:</td>
					<td width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtmanualName" onblur="omitSomeCharacter('txtmanualName','<>?*%$;')"
							runat="server" MaxLength="50" Width="168px"></asp:textbox></td>
				</tr>
				<TR vAlign="top">
					<td class="titleField" width="24%">Deskripsi</td>
					<td width="1%">:</td>
					<td width="75%"><TEXTAREA id="taDesc" style="WIDTH: 328px; HEIGHT: 50px" rows="3" cols="38" runat="server"></TEXTAREA></td>
				</TR>
				<TR>
					<TD class="titleField" width="24%">Sequence</TD>
					<TD width="1%">:</TD>
					<TD width="75%"><asp:textbox onkeypress="return NumericOnlyWith(event,'')" id="txtSequence" runat="server" MaxLength="3"
							Width="56px"></asp:textbox></TD>
				</TR>
				<TR>
					<td class="titleField" width="24%">Tipe</td>
					<td width="1%">:</td>
					<td width="75%"><asp:dropdownlist id="ddlType" runat="server" Width="60px"></asp:dropdownlist></td>
				</TR>
				<TR>
					<td class="titleField" width="24%">Dokumen Manual</td>
					<td width="1%">:</td>
					<td width="75%"><input id="fileUploadManual" style="WIDTH: 328px; HEIGHT: 20px" type="file" size="35" runat="server"></td>
				</TR>
				<TR>
					<td></td>
					<td></td>
					<TD class="titleField"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnBack" runat="server" Text="Kembali" Visible="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3"></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

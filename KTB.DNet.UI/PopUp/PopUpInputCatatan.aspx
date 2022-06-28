<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpInputCatatan.aspx.vb" Inherits="PopUpInputCatatan" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUp Input Catatan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		
		</script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="WIDTH: 551px; HEIGHT: 184px" cellSpacing="0" cellPadding="0"
				width="551" border="0">
				<tr>
					<td class="titlePage" align="center"><asp:label id="Label2" runat="server">Training Input Catatan</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" style="WIDTH: 551px; HEIGHT: 150px" cellSpacing="1" cellPadding="2"
							width="551" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 122px" width="122"></TD>
								<TD width="1%"></TD>
								<TD width="85%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 122px" width="122">&nbsp; No. Reg</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="LblNoReg" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 122px" width="122">&nbsp; Nama Siswa</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="LblNamaSiswa" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 122px" vAlign="top" width="122">&nbsp;<asp:label id="Label1" runat="server" Font-Bold="True" Width="88px">Catatan  </asp:label></TD>
								<TD vAlign="top" width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtCatatan)" id="txtCatatan" runat="server" TextMode="MultiLine"
										Height="56px" Width="391px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 122px" width="122"></TD>
								<TD width="1%"></TD>
								<TD width="75%"><asp:button id="btnSimpan" runat="server" Height="24px" Width="72px" CausesValidation="False"
										Text="Simpan"></asp:button><INPUT id="btnClose" style="WIDTH: 72px; HEIGHT: 24px" onclick="window.close()" type="button"
										value="Cancel" runat="server"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

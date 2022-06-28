<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpReferensiHarga.aspx.vb" Inherits="PopUpReferensiHarga" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpReferensiHarga</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<base target="_self">
		<script language="javascript">
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="40%">
						<asp:label id="Label6" runat="server">Referensi Harga</asp:label></TD>
					<TD style="HEIGHT: 20px" width="1%">
						<asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 20px" width="15%" nowrap>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0" width="100%">
							<TR>
								<TD>
									<asp:radiobutton id="rbtnMonthYear" runat="server" Text="Bulan&nbsp;&nbsp;" GroupName="TOP"></asp:radiobutton></TD>
								<TD>
                                    <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtMonthYear" runat="server" Width="72px" MaxLength="6"></asp:TextBox>
								</TD>
                                <td>&nbsp;&nbsp;<asp:Label runat="server" Text="MMYYYY"></asp:Label></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 25px" width="15%"></TD>
					<TD style="HEIGHT: 25px" width="1%">
						<asp:label id="Label16" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 25px" width="75%">
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD>
									<asp:radiobutton id="rbtnUpdatePrice" runat="server" Text="Update Price" GroupName="TOP"></asp:radiobutton></TD>
                                <td></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center" style="HEIGHT: 20px" colspan="3">
                        <table>
                            <tr>
                                <td><asp:Button id="btnPilih" runat="server" Text="Pilih" Width="60px"></asp:Button></td>
                                <td><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
							            name="btnCancel"></td>
                            </tr>
                        </table>
						&nbsp;
						
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDownloadEstimate.aspx.vb" Inherits="PopUpDownloadEstimate" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDownloadEstimate</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" height="20">DOWNLOAD File Estimasi PO Sparepart  <asp:Label runat="server" ID="lblDealerCode"></asp:Label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>					
					</td>
				</tr>
				<TR>
					<TD></TD>
				</TR>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="1" cellPadding="3" width="400" border="0">
							<TR>
								<td width="30" align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD width="370"><asp:linkbutton id="lbtnSDGroup01" runat="server">SDGROUP01.DLR</asp:linkbutton></TD>
							</TR>
							<TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup02" runat="server">SDGROUP02.DLR</asp:linkbutton></TD>
							</TR>
							<TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup03" runat="server">SDGROUP03.DLR</asp:linkbutton></TD>
							</TR>
							<TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup04" runat="server">SDGROUP04.DLR</asp:linkbutton></TD>
							</TR>
							<TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup05" runat="server">SDGROUP05.DLR</asp:linkbutton></TD>
							</TR>
                               <!--  CR 20150610 BAck Order-->
                       

                            <TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup06" runat="server" ToolTip="Pending RO B/O">SDGROUP06.DLR</asp:linkbutton></TD>
							</TR>


                            <TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup07" runat="server" ToolTip="Estimasi RO B/O">SDGROUP07.DLR</asp:linkbutton></TD>
							</TR>

                            <TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup08" runat="server" ToolTip="Estimasi EO B/O">SDGROUP08.DLR</asp:linkbutton></TD>
							</TR>

                            <TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup09" runat="server" ToolTip="Outstanding RO B/O">SDGROUP09.DLR</asp:linkbutton></TD>
							</TR>

                                 <TR>
								<td align="center"><!--img src="../images/icon_download.gif" border="0" alt="Download"--></td>
								<TD><asp:linkbutton id="lbtnSDGroup10" runat="server" ToolTip="Outstanding EO B/O">SDGROUP10.DLR</asp:linkbutton></TD>
							</TR>

                              <!-- ENd of CR-->
						</TABLE>
						<asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 7px; POSITION: absolute; TOP: 276px" runat="server"
							Text="Kembali"></asp:Button></form>
					
					</td>
				</tr>
			</table>
	</body>
</HTML>

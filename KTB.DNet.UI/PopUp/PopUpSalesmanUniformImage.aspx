<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSalesmanUniformImage.aspx.vb" Inherits="PopUpSalesmanUniformImage" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpSalesmanUniformImage</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">
									SALESMAN UNIFORM - Image</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="20%"></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD style="HEIGHT: 22px" width="25%"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 22px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="20%"></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD style="HEIGHT: 22px" width="33%"></TD>
							</TR>
							<TR>
								<TD class="titleField"  colSpan="7" align="center">
									<DIV id="divPhoto" style="OVERFLOW: auto; WIDTH: 600; height:520" align="center">
										<asp:image id="photoView" runat="server" Width="500" Height="500"></asp:image></DIV>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px" colSpan="7"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
								<TD style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px"></TD>
							</TR>

						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

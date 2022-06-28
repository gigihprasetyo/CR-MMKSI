<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmPCPhisingGuard.aspx.vb" Inherits="frmPCPhisingGuard" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PC Phising Guard</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="/WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="./WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="./WebResources/InputValidation.js"></script>
		<script language="javascript">
			
		  
			
			function OpenFullScreenWindow(Address)
			{
			       newwin=window.open(Address,"_blank", "fullscreen=no,titlebar=no,personalbar=no,toolbar=no,status=1,menubar=no,scrollbars=yes,resizable=yes,directories=no,location=no");
			  	   this.name = "origWin";
				   origWin= window.open("login.aspx", "origWin");
				   window.opener = top;
                   window.close();              
			}
			
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="FormLogin" method="post" width="100%" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" valign="middle">
				<tr>
					<td vAlign="middle">
						<table height="380" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cc0001" border="0">
							<tr>
								<td width="40%">&nbsp;</td>
								<td width="20%" background="images/login_bg.gif">
									<table height="386" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr height="74">
											<td width="612" height="74"><IMG src="images/login_r1_c1.gif" border="0"></td>
											<td width="177"><IMG src="images/new_login_r1_c2.gif" border="0"></td>
										</tr>
										<tr height="81">
											<td align="center" background="images/login_bg.gif" colSpan="2" height="69">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<%--<td align="left" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>--%>
														<td align="left" width="75%" style="padding-left:12px;"><h2><STRONG>Gambar Pilihan 1 </STRONG>
															</h2>
														</td>
														<td align="right" width="25%"><IMG hspace="10" src="images/new_mits.gif" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="144">
											<td vAlign="middle" align="center" background="images/login_bg.gif" colSpan="2" height="144">
												<table height="144" cellSpacing="0" cellPadding="0" width="787" align="center" border="0">
													<tr>
														<td align="center" bgColor="#cccccc">
															<table height="160" cellSpacing="1" width="660" align="center" border="0">
																<tr>
																	<td colspan="2" height="20">&nbsp;</td>
																</tr>
																<tr>
																	<td width="100">&nbsp;</td>
																	<td height="100">
																		<table cellSpacing="1" width="460" align="center" border="0">
																			<TR>
																				<TD width="183" height="3" class="welcomeLogin"><asp:label id="Label4" runat="server">Upload Gambar Pilihan 1</asp:label></TD>
																				<TD height="3"><INPUT id="photoSrc" onkeydown="return false" style="WIDTH: 208px; HEIGHT: 20px" type="file"
																						size="15" name="photoSrc" runat="server">&nbsp;
																					<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="photoSrc"></asp:RequiredFieldValidator></TD>
																			</TR>
																			<TR>
																				<TD class="welcomeLogin" width="183" height="23"><asp:label id="Label3" runat="server">Deskripsi Gambar Pilihan 1</asp:label></TD>
																				<TD height="23"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDescription" runat="server" Width="206px"
																						CssClass="welcomeLogin" MaxLength="8"></asp:textbox>
																					<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtDescription"></asp:RequiredFieldValidator></TD>
																			</TR>
																			<TR>
																				<td align="center" colSpan="2">&nbsp;<br>
																					<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																						<asp:button id="btnBack" runat="server" Width="56px" Text="Kembali" CausesValidation="False"></asp:button>
																						&nbsp;&nbsp;<asp:button id="btnSimpan" runat="server" Width="56px" Text="Simpan"></asp:button></p>
																					<p></p>
																					<p></p>
																					<p></p>
																					<p></p>
																				</td>
																			</TR>
																		</table>
																	</td>
																	<td valign="top" width="100"><asp:Image id="ImgPC" runat="server" Width="80px" Height="80px"></asp:Image></td>
																</tr>
																<TR>
																	<TD align="center" height="60">&nbsp;</TD>
																</TR>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr height="23">
											<td background="images/login_bg.gif" colSpan="2" height="23"><IMG height="1" src="images/dot.gif" width="1" border="0"></td>
										</tr>
										<tr height="44">
											<td style="HEIGHT: 31px" align="right" background="images/login_bg.gif">
												<table cellSpacing="1" width="70%" border="0">
													<tr>
														<td align="center">
                                                        <%--    <IMG src="images/new_logo_login2.gif" border="0">--%>
                                                              <div style="text-align:center">
                                                                  <span style="font-family:Arial; font-size:12px; font-weight:bold;">Mitsubishi Motors authorize distributor<br/></span>
                                                                 <span style="font-family:Arial; font-size:16px; font-weight:bold;">PT Mitsubishi Motors Krama Yudha Sales Indonesia</span>
                                                            </div>
														</td>
													</tr>
												</table>
											</td>
											<td style="HEIGHT: 31px" align="right"></td>
										</tr>
										<tr height="20">
											<td colSpan="2" height="20"><IMG src="images/login_r4.gif" border="0"></td>
										</tr>
									</table>
								</td>
								<td width="40%">&nbsp;</td>
							</tr>
						</table>
						<table height="80" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr vAlign="top">
								<td width="40%">&nbsp;</td>
								<td align="center" width="20%"><B>Tampilan Terbaik IE 6.0+ : 1024 x 768</B>
									<br>
									<table height="60" cellSpacing="0" cellPadding="0" width="789" border="0">
										<tr>
											<td align="center">&nbsp;PT Mitsubishi Motors Krama Yudha Sales Indonesia, Hak Cipta 
												Dilindungi Undang-Undang.<br>
												Jl. Jend A. Yani Proyek Pulomas, Jakarta.
												<br>
												Help Desk D-NET : (021) 4786-7575 atau <A class="menuLeft" href="mailto:admin.d-net@mitsubishi-motors.co.id">
													admin.d-net@mitsubishi-motors.co.id</A>
												<br>
											</td>
										</tr>
									</table>
								</td>
								<td width="40%">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

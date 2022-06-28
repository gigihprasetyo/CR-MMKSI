<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAuditAssesmentResult.aspx.vb" Inherits="FrmAuditAssesmentResult" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAuditSchedule</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM -&nbsp;Hasil Audit</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TBODY>
								<TR>
									<td class="titleField" width="20%">Periode</td>
									<TD width="1%">:</TD>
									<TD width="69%" colSpan="4"><asp:label id="lblPeriode" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="20%">Kode Dealer</td>
									<TD width="1%">:</TD>
									<TD width="69%" colSpan="4"><asp:label id="lblDealerCode" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 19px" width="20%">Jadwal Audit</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="HEIGHT: 19px" width="69%" colSpan="4"><asp:label id="lblAuditScheduleStartDate" Runat="server"></asp:label><asp:label id="lblAuditScheduleSeparator" Runat="server">s/d</asp:label><asp:label id="lblAuditScheduleEndDate" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 19px" width="20%"></TD>
									<TD style="HEIGHT: 19px" width="1%"></TD>
									<TD style="HEIGHT: 19px" width="69%" colSpan="4"><asp:hyperlink id="hypDownloadAssesmentItem" runat="server"></asp:hyperlink></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 19px" width="20%"></TD>
									<TD style="HEIGHT: 19px" width="1%"></TD>
									<TD style="HEIGHT: 19px" width="69%" colSpan="4"><asp:hyperlink id="hypDownloadJuklakFile" runat="server"></asp:hyperlink></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 19px" width="20%">Auditor</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="HEIGHT: 19px" width="29%"><asp:label id="lblAuditorType" Runat="server"></asp:label></TD>
									<td class="titleField" style="HEIGHT: 19px" width="20%">Nama</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="HEIGHT: 19px" width="29%"><asp:label id="lblAuditorName" Runat="server">Johan Hasan</asp:label></TD>
								</TR>
								<asp:panel id="pnlKTBSection" Runat="server">
									<TR>
										<TD class="titleField" style="HEIGHT: 18px" width="20%">
											<asp:Label id="lblUploadHasilPenilaian" Runat="server">Upload Hasil Penilaian</asp:Label></TD>
										<TD style="HEIGHT: 18px" width="1%">:</TD>
										<TD style="HEIGHT: 18px" width="69%" colSpan="4">
											<asp:label id="lblAssesmentResultFile" Runat="server"></asp:label><BR>
											<INPUT onkeypress="return false;" id="fileUploadAssesmentResult" type="file" size="72"
												runat="server"></TD>
									</TR>
									<TR>
										<TD class="titleField" width="20%">
											<asp:label id="lblUploadFoto" Runat="server">Upload Foto:</asp:label></TD>
										<TD width="1%"></TD>
										<TD width="69%" colSpan="4"></TD>
									</TR>
									<TR>
										<TD vAlign="top" width="100%" colSpan="6">
											<DIV id="div1" style="OVERFLOW: auto; WIDTH: 100%">
												<asp:datagrid id="dtgPhotoList" runat="server" BackColor="#E0E0E0" AutoGenerateColumns="False"
													BorderColor="#CDCDCD" BorderWidth="1px" CellPadding="3" BorderStyle="None" Width="100%" ShowHeader="False">
													<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
													<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
													<ItemStyle BackColor="White"></ItemStyle>
													<HeaderStyle VerticalAlign="Top"></HeaderStyle>
													<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Photo">
															<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
															<ItemTemplate>
																<asp:Label id=lblDesc runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
																</asp:Label>
																<asp:Label id="IDFoto" runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
															<ItemTemplate>
																<asp:Image id="imgEditItemImage" Runat="server"></asp:Image><BR>
																<INPUT onkeypress="return false;" id="fileEditItemImage" tabIndex="19" type="file" size="80"
																	name="File1" runat="server">
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid></DIV>
										</TD>
									</TR>
								</asp:panel>
								<asp:panel id="pnlDealerSection" Runat="server">
									<TR>
										<TD class="titleField" width="20%">
											<asp:label id="lblFotoPerbaikan" Runat="server">Foto Perbaikan:</asp:label></TD>
										<TD width="1%"></TD>
										<TD width="69%" colSpan="4"></TD>
									</TR>
									<TR>
										<TD vAlign="top" width="100%" colSpan="6">
											<DIV id="div1" style="OVERFLOW: auto; WIDTH: 100%">
												<asp:datagrid id="dtgFotoPerbaikan" runat="server" BackColor="#E0E0E0" AutoGenerateColumns="False"
													BorderColor="#CDCDCD" BorderWidth="1px" CellPadding="3" BorderStyle="None" Width="100%" ShowHeader="False">
													<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
													<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
													<ItemStyle BackColor="White"></ItemStyle>
													<HeaderStyle VerticalAlign="Top"></HeaderStyle>
													<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Photo">
															<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
															<ItemTemplate>
																<asp:Label id="lblDesc" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.AuditParameterPhoto.Description")%>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
															<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
															<ItemTemplate>
																<asp:Image ID="imgEditItemImage" Runat="server"></asp:Image><br>
																<INPUT onkeypress="return false;" id="fileEditItemImage" tabIndex="19" type="file" size="80"
																	name="File1" runat="server">
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid></DIV>
										</TD>
									</TR>
								</asp:panel>
								<tr>
									<td colSpan="6">&nbsp;</td>
								</tr>
								<tr>
									<td><asp:button id="btnSimpan" runat="server" Width="64px" Text="Simpan"></asp:button><asp:button id="btnRilis" runat="server" Width="64px" Text="Rilis" Visible="True"></asp:button><asp:button id="btnBack" runat="server" Text="Kembali"></asp:button></td>
									<td></td>
									<td></td>
								</tr>
							</TBODY>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

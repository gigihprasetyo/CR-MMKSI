<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanTrainingMaster.aspx.vb" Inherits="FrmSalesmanTrainingMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SALESMAN TRAINING MASTER</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			function TxtKeypressNum()
			{
				return NumericOnlyWith(event,'');
			}
			function TxtBlurNum(objTxtNum)
			{
				NumericOnlyBlurWith(objTxtNum,'');
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">PELATIHAN TENAGA PENJUAL&nbsp;- Buat 
						Kode Pelatihan</td>
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
								<TD class="titleField" style="WIDTH: 214px; HEIGHT: 17px" width="214">Kode Training</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%" colSpan="3"><asp:textbox onkeypress="TxtKeypress();" id="txtTrainingCode" onblur="TxtBlur('txtTrainingCode');"
										runat="server" size="22" MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Materi 
									Training</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><asp:textbox onkeypress="TxtKeypress();" id="txtTrainingTitle" onblur="TxtBlur('txtTrainingTitle');"
										runat="server" size="22" MaxLength="50" Width="280px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 19px" width="215">Jenis 
									Training</TD>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="25%" colSpan="3"><asp:dropdownlist id="ddlTrainingType" runat="server" Width="144px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Tgl 
									Penyelenggaraan</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3">
									<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
										<TR>
											<TD><cc1:inticalendar id="icTglCreate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>s/d</TD>
											<TD><cc1:inticalendar id="icTglCreate2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Tgl 
									Pendaftaran</TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3">
									<TABLE id="Table4" cellSpacing="1" cellPadding="1" border="0">
										<TR>
											<TD><cc1:inticalendar id="icTglDaftar" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>s/d</TD>
											<TD><cc1:inticalendar id="icTglDaftar2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Pengajar 1</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><asp:textbox onkeypress="TxtKeypress();" id="txtTrainer1" onblur="TxtBlur('txtTrainer1');" runat="server"
										size="22" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 24px" width="215">Pengajar 2</TD>
								<TD style="HEIGHT: 24px" width="1%">:</TD>
								<TD style="HEIGHT: 24px" width="25%" colSpan="3"><asp:textbox onkeypress="TxtKeypress();" id="txtTrainer2" onblur="TxtBlur('txtTrainer2');" runat="server"
										size="22" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 11px" width="215">Pengajar 3</TD>
								<TD style="HEIGHT: 11px" width="1%">:</TD>
								<TD style="HEIGHT: 11px" width="25%" colSpan="3"><asp:textbox onkeypress="TxtKeypress();" id="txtTrainer3" onblur="TxtBlur('txtTrainer3');" runat="server"
										size="22" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 10px" width="215">Target 
									Peserta</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%" colSpan="3"><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtAttendanceTarget" onblur="TxtBlurNum(txtAttendanceTarget)"
										runat="server" size="22" MaxLength="4" Width="96px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 26px" width="215">Tempat</TD>
								<TD style="HEIGHT: 26px" width="1%">:</TD>
								<TD style="HEIGHT: 26px" width="25%" colSpan="3"><asp:textbox onkeypress="TxtKeypress();" id="txtTrainingPlace" onblur="TxtBlur('txtTrainingPlace');"
										runat="server" size="22" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="215">Persyaratan Training</TD>
								<TD width="1%">:</TD>
								<TD width="25%" colSpan="3"><asp:textbox id="txtPrerequisite" runat="server" Width="296px" TextMode="MultiLine" Height="50px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 18px" width="215"></TD>
								<TD style="HEIGHT: 18px" width="1%"></TD>
								<TD style="HEIGHT: 18px" noWrap width="25%"></TD>
								<TD style="HEIGHT: 18px" noWrap width="25%"></TD>
								<TD style="HEIGHT: 18px" noWrap width="25%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 215px; HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD colSpan="5">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgTraining" runat="server" Width="100%" BorderColor="#E0E0E0" CellPadding="3"
											BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="TrainingCode" SortExpression="TrainingCode" HeaderText="Kode Training">
													<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TrainingTitle" SortExpression="TrainingTitle" HeaderText="Materi Training">
													<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="StartingDate" SortExpression="StartingDate" HeaderText="Tanggal Penyelenggaraan"
													DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
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

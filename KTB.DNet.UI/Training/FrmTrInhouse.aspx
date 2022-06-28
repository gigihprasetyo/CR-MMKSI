<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrInhouse.aspx.vb" Inherits="FrmTrInhouse" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCourse</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script>
			
			function ShowPopupCourseSelection()
			{
				showPopUp('../PopUp/PopUpClassSelection.aspx', '', 500, 760, CourseSelection)
			}
			function CourseSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str1 = document.getElementById("txtClassCode");
				//check weather the class is M2 Class
				if(tempParam[0].substring(0,2)!="M2")
				{
					alert("Inhouse Training hanya untuk Kelas M2, Silahkan pilih Kelas yang lain");
					return false;
				}
				str1.value=tempParam[0];
				str1.focus()
			}
			function ShowPopupTraineeSelection()
			{
				showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=2', '', 500, 760, TraineeSelection)
			}
			function TraineeSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str1 = document.getElementById("txtClassCode");
				//check weather the class is M2 Class
				if(tempParam[0].substring(0,2)!="M2")
				{
					alert("Inhouse Training hanya untuk Kelas M2, Silahkan pilih Kelas yang lain");
					return false;
				}
				str1.value=tempParam[0];
				str1.focus()
			}
			function ShowPopupClassSelection()
			{
				showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=2', '', 500, 760, ClassSelection)
			}
			function ClassSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str1 = document.getElementById("txtClassCode");
				//check weather the class is M2 Class
				if(tempParam[0].substring(0,2)!="M2")
				{
					alert("Inhouse Training hanya untuk Kelas M2, Silahkan pilih Kelas yang lain");
					return false;
				}
				str1.value=tempParam[0];
				str1.focus()
			}
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}
		</script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						<P>TRAINING - Training Inhouse</P>
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 33px" width="24%" colSpan="8">
									<h3><asp:label id="lblTitle" Runat="server">PT. HARJAYA TIGA BERLIAN SEMARANG</asp:label></h3>
									<P>
										<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="760" border="1">
											<TR>
												<TD style="WIDTH: 58px; HEIGHT: 18px" align="center"><STRONG>SERVICE DEPT</STRONG></TD>
												<TD style="WIDTH: 231px; HEIGHT: 18px" align="center"><STRONG>IN HOUSE TRAINING</STRONG></TD>
												<TD style="WIDTH: 12px; HEIGHT: 18px" align="center"><STRONG>No:</STRONG></TD>
												<TD style="WIDTH: 132px; HEIGHT: 18px" align="center"><STRONG><asp:textbox id="txtCode" runat="server" Width="120px"></asp:textbox></STRONG></TD>
												<TD style="WIDTH: 151px; HEIGHT: 18px" align="right"><STRONG><STRONG>Date:&nbsp;&nbsp; </STRONG>
													</STRONG>
												</TD>
												<TD style="WIDTH: 51px; HEIGHT: 18px" align="center" colSpan="2"><STRONG><cc1:inticalendar id="icReportDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></STRONG></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 291px; HEIGHT: 104px" vAlign="top" colSpan="2" rowSpan="4">Kepada 
													Yth :<br>
													Bpk. Deddy Setiawan, Training Dept. Head<br>
													PT.Krama Yudha Tiga Berlian Motors-Jakarta<br>
													<br>
													<br>
													No. Fax : 021 4758783<br>
													E-mail : mutia@ktb.co.id or dewi-m@ktb.co.id
												</TD>
												<TD style="WIDTH: 152px" align="center" colSpan="2">APPROVED</TD>
												<TD style="WIDTH: 167px" align="right" colSpan="3">REPORTER</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 152px; HEIGHT: 55px" colSpan="2">&nbsp;</TD>
												<TD style="WIDTH: 151px; HEIGHT: 55px">&nbsp;</TD>
												<TD style="HEIGHT: 55px" colSpan="2">&nbsp;</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 152px; HEIGHT: 21px" align="center" colSpan="2"><asp:textbox id="txtApproval1" style="TEXT-ALIGN: center" runat="server" Width="120px" ToolTip="Approve Officer"></asp:textbox></TD>
												<TD style="WIDTH: 151px; HEIGHT: 21px" align="center"><asp:textbox id="txtApproval2" style="TEXT-ALIGN: center" runat="server" Width="120px" ToolTip="Reporter 1"></asp:textbox></TD>
												<TD style="HEIGHT: 21px" align="center" colSpan="2"><asp:textbox id="txtApproval3" style="TEXT-ALIGN: center" runat="server" Width="120px" ToolTip="Reporter 2"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 152px; HEIGHT: 7px" align="center" colSpan="2"><asp:textbox id="txtPosition1" style="TEXT-ALIGN: center" runat="server" Width="120px" ToolTip="Job Position"></asp:textbox></TD>
												<TD style="WIDTH: 151px; HEIGHT: 7px" align="center"><asp:textbox id="txtPosition2" style="TEXT-ALIGN: center" runat="server" Width="120px" ToolTip="Job Position"></asp:textbox></TD>
												<TD style="HEIGHT: 7px" align="center" colSpan="2"><asp:textbox id="txtPosition3" style="TEXT-ALIGN: center" runat="server" Width="120px" ToolTip="Job Position"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px; HEIGHT: 16px"><STRONG>SUBJECT</STRONG></TD>
												<TD style="WIDTH: 225px; HEIGHT: 16px" colSpan="6"><STRONG>Laporan In House Training</STRONG></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px" vAlign="top" align="center" rowSpan="5"><STRONG><STRONG>I</STRONG></STRONG></TD>
												<TD style="WIDTH: 225px" colSpan="6"><STRONG>Kelengkapan Training</STRONG></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px; HEIGHT: 15px">1. Ruang Khusus Training</TD>
												<TD style="WIDTH: 152px; HEIGHT: 15px" colSpan="2"><asp:checkbox id="chkRoom" runat="server" Width="96px" Height="3px"></asp:checkbox></TD>
												<TD style="WIDTH: 151px; HEIGHT: 15px">4. Komputer</TD>
												<TD style="WIDTH: 80px; HEIGHT: 15px" colSpan="2"><asp:checkbox id="chkComputer" runat="server" Width="102px" Height="3px"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px; HEIGHT: 35px">2. Papan Tulis</TD>
												<TD style="WIDTH: 152px; HEIGHT: 35px" colSpan="2"><asp:checkbox id="chkBoard" runat="server" Width="96px" Height="3px"></asp:checkbox></TD>
												<TD style="WIDTH: 151px; HEIGHT: 35px">5. OHP Slide Projector</TD>
												<TD style="WIDTH: 51px; HEIGHT: 35px" colSpan="2"><asp:checkbox id="chkOHP" runat="server" Width="102px" Height="3px"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px; HEIGHT: 23px" vAlign="top">3. Meja/Bangku</TD>
												<TD style="WIDTH: 152px; HEIGHT: 23px" vAlign="top" colSpan="2"><asp:checkbox id="chkDesk" runat="server" Width="104px" Height="3px"></asp:checkbox></TD>
												<TD style="WIDTH: 151px; HEIGHT: 23px">6. Spidol / Kapur /&nbsp; Penghapus</TD>
												<TD style="WIDTH: 51px; HEIGHT: 23px" vAlign="top" colSpan="2"><asp:checkbox id="chkChalk" runat="server" Width="102px" Height="3px"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px" vAlign="top" align="center" rowSpan="4"><STRONG>II</STRONG></TD>
												<TD style="WIDTH: 225px" colSpan="6"><STRONG>Sumber Materi</STRONG></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px" vAlign="top">1. Soft Copy dari D-Net</TD>
												<TD style="WIDTH: 152px" vAlign="top" colSpan="2"><asp:checkbox id="chkSoftDNet" runat="server" Width="104px" Height="3px"></asp:checkbox></TD>
												<TD style="WIDTH: 151px">3. Soft Copy External Hardisk</TD>
												<TD style="WIDTH: 51px" vAlign="top" colSpan="2"><asp:checkbox id="chkSoftExt" runat="server" Width="104px" Height="3px"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px">2. Hard Copy Buku/Diktat</TD>
												<TD style="WIDTH: 152px" colSpan="2"><asp:checkbox id="chkHard" runat="server" Width="104px" Height="3px"></asp:checkbox></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px; HEIGHT: 18px" vAlign="top" align="center" rowSpan="2"><STRONG>III</STRONG></TD>
												<TD style="WIDTH: 231px; HEIGHT: 18px"><STRONG>Peserta</STRONG></TD>
												<TD style="WIDTH: 152px; HEIGHT: 18px" colSpan="2"></TD>
												<TD style="WIDTH: 151px; HEIGHT: 18px"><asp:textbox id="txtClassCode" runat="server" Width="120px" ToolTip="Job Position" Visible="False"></asp:textbox>
													<asp:label id="lblPopUpTraining" onclick="ShowPopupClassSelection();" runat="server" width="16px"
														Visible="False">
														<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
													</asp:label></TD>
												<TD style="WIDTH: 51px; HEIGHT: 18px"><asp:button id="btnAdd" runat="server" Width="136px" Text="Tambahkan ke Tabel" Visible="False"></asp:button></TD>
												<TD style="HEIGHT: 18px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 225px" colSpan="6">
													<DIV id="div1" style="OVERFLOW: auto; WIDTH: 664px; HEIGHT: 208px"><asp:datagrid id="dtgMember" runat="server" Width="664px" AutoGenerateColumns="False" GridLines="Horizontal"
															CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowSorting="True" PageSize="100" ForeColor="GhostWhite"
															CellSpacing="1" Font-Names="MS Reference Sans Serif" ShowFooter="True">
															<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
															<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
															<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
															<ItemStyle BackColor="White"></ItemStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="No">
																	<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
																	<ItemTemplate>
																		<asp:Label id="lblNo" Runat="server"></asp:Label>
																		<asp:TextBox runat="server" ID="txtID" width="10px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:TextBox>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:TextBox runat="server" ID="txtID" width="10px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:TextBox>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox runat="server" ID="txtID" width="10px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:TextBox>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseCode" HeaderText="Kategori Kelas">
																	<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<ItemTemplate>
																		<asp:Label id=lblCourseCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>'>
																		</asp:Label>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:TextBox id="txtCourseCode" runat="server" Width="50px" MaxLength="500"></asp:TextBox>
																		<asp:label id="lblPopupCourseCode" onclick="ShowPopupCourseSelection();" runat="server" width="16px">
																			<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
																		</asp:label>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox id="txtCourseCode" style="TEXT-ALIGN: left" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>' MaxLength="10">
																		</asp:TextBox>
																		<asp:label id="lblPopupCourseCode" onclick="ShowPopupCourseSelection();" runat="server" width="16px">
																			<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
																		</asp:label>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="Result" HeaderText="Hasil Test">
																	<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<ItemTemplate>
																		<asp:Label id="lblResult" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Result") %>'>
																		</asp:Label>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:TextBox id="txtResult" style="TEXT-ALIGN: right" runat="server" Width="50px"></asp:TextBox>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox id="txtResult" style="TEXT-ALIGN: right" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.Result") %>' MaxLength="10">
																		</asp:TextBox>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Regst.">
																	<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<ItemTemplate>
																		<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TraineeID") %>'>
																		</asp:Label>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:TextBox id="txtTraineeID" runat="server" Width="50px" ReadOnly="True"></asp:TextBox>
																		<asp:label id="lblPopTrainee" onclick="ShowPopupTraineeSelection();" runat="server" width="16px">
																			<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
																		</asp:label>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox id="txtTraineeID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.TraineeID") %>' readonly="True">
																		</asp:TextBox>
																		<asp:label id="lblPopupTrainee" onclick="ShowPopupTraineeSelection();" runat="server" width="16px">
																			<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
																		</asp:label>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Nama Peserta">
																	<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<ItemTemplate>
																		<asp:Label id="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
																		</asp:Label>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:TextBox id="txtName" runat="server" Width="110px" readonly="True"></asp:TextBox>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox id="txtName" runat="server" Width="110px" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>' readonly="True">
																		</asp:TextBox>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Kode Kelas">
																	<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<ItemTemplate>
																		<asp:Label id="lblClassCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
																		</asp:Label>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:TextBox id="txtClassCode" runat="server" Width="50px" MaxLength="20"></asp:TextBox>
																		<asp:label id="lblPopupClassCode" onclick="ShowPopupClassSelection();" runat="server" width="16px">
																			<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
																		</asp:label>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox id="txtClassCode" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>' MaxLength="20">
																		</asp:TextBox>
																		<asp:label id="lblPopupClassCode" onclick="ShowPopupClassSelection();" runat="server" width="16px">
																			<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
																		</asp:label>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
																	CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
																	EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
																	<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																</asp:EditCommandColumn>
																<asp:TemplateColumn HeaderText="Aksi">
																	<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<ItemTemplate>
																		<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
																			CommandName="delete">
																			<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn>
																	<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																	<FooterTemplate>
																		<asp:LinkButton id="lbtnAdd" runat="server" CommandName="add">
																			<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
																	</FooterTemplate>
																</asp:TemplateColumn>
															</Columns>
															<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
														</asp:datagrid></DIV>
												</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px"></TD>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px" vAlign="top" align="center"><STRONG><STRONG>IV</STRONG></STRONG></TD>
												<TD style="WIDTH: 231px" vAlign="top"><STRONG><STRONG>Daftar Hadir (Terlampir)</STRONG></STRONG></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 356px" colSpan="4"><INPUT id="ufAttendance" onkeydown="return false;" style="WIDTH: 152px; HEIGHT: 20px" type="file"
														size="6" name="File1" runat="server">
													<asp:button id="btnUploadAttendance" runat="server" Width="56px" Text="Upload"></asp:button><br>
													<asp:button id="btnDownloadAttendance" runat="server" Width="56px" Text="Download" Visible="False"></asp:button><asp:label id="lblFileAttendance" runat="server" Width="232px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px"></TD>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px" vAlign="top" align="center"><STRONG><STRONG>V</STRONG></STRONG></TD>
												<TD style="WIDTH: 231px" vAlign="top"><STRONG><STRONG>Evaluasi (Terlampir)</STRONG></STRONG></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 356px" colSpan="3"><INPUT id="ufEvaluation" onkeydown="return false;" style="WIDTH: 150px; HEIGHT: 20px" type="file"
														size="12" name="File1" runat="server">&nbsp;
													<asp:button id="btnUploadEvaluation" runat="server" Width="56px" Text="Upload"></asp:button><br>
													<asp:button id="btnDownloadEvaluation" runat="server" Width="56px" Text="Download" Visible="False"></asp:button><asp:label id="lblFileEvaluation" runat="server" Width="232px"></asp:label></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px"></TD>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px; HEIGHT: 16px" vAlign="top" align="center" rowSpan="2"><STRONG>VI</STRONG></TD>
												<TD style="WIDTH: 231px; HEIGHT: 16px" vAlign="top" colSpan="1"><STRONG><STRONG>Informasi 
															Tambahan</STRONG></STRONG></TD>
												<td></td>
												<td colSpan="4"><STRONG>Lampiran Tambahan</STRONG> <INPUT id="ufReport" onkeydown="return false;" style="WIDTH: 128px; HEIGHT: 20px" type="file"
														size="2" name="File1" runat="server">
													<asp:button id="btnUploadReport" runat="server" Width="56px" Text="Upload"></asp:button><br>
													<asp:button id="btnDownloadReport" runat="server" Width="56px" Text="Download" Visible="False"></asp:button>&nbsp;<asp:label id="lblFileReport" runat="server" Width="232px"></asp:label>
												</td>
											</TR>
											<TR>
												<TD style="WIDTH: 225px" colSpan="6">
													<DIV id="Div2" style="OVERFLOW: auto; WIDTH: 664px; HEIGHT: 208px"><asp:datagrid id="dtgInformation" runat="server" Width="648px" Height="0px" AutoGenerateColumns="False"
															CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" PageSize="5" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif"
															ShowFooter="True">
															<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
															<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
															<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
															<ItemStyle BackColor="White"></ItemStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="No">
																	<HeaderStyle Width="10px" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Top"></ItemStyle>
																	<ItemTemplate>
																		<asp:Label id="lblNoInf" Runat="server"></asp:Label>
																		<asp:textbox id="txtID" Runat="server" style="visibility:hidden" width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:textbox>
																	</ItemTemplate>
																	<FooterTemplate>
																		<asp:textbox id="txtID" Runat="server" style="visibility:hidden;" width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:textbox>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:textbox id="txtID" Runat="server" style="visibility:hidden;" width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:textbox>
																	</EditItemTemplate>
																	<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
																</asp:TemplateColumn>
																<asp:TemplateColumn SortExpression="Information" HeaderText="Keterangan/Tambahan Informasi">
																	<HeaderStyle Width="80%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Left" Width="80%" VerticalAlign="Top"></ItemStyle>
																	<ItemTemplate>
																		<asp:Label id=lblInformation style="TEXT-ALIGN: left" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.Information") %>'>
																		</asp:Label>
																	</ItemTemplate>
																	<FooterStyle HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
																	<FooterTemplate>
																		<asp:TextBox id="txtInformation" runat="server" Width="100%" MaxLength="500"></asp:TextBox>
																	</FooterTemplate>
																	<EditItemTemplate>
																		<asp:TextBox id=txtInformation style="TEXT-ALIGN: left" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.Information") %>' MaxLength="500">
																		</asp:TextBox>
																	</EditItemTemplate>
																</asp:TemplateColumn>
																<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
																	CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
																	EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
																	<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
																	<ItemStyle HorizontalAlign="Center"></ItemStyle>
																</asp:EditCommandColumn>
																<asp:TemplateColumn>
																	<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
																	<ItemTemplate>
																		<asp:LinkButton id="lbtnDelete" runat="server" CommandName="delete">
																			<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:TemplateColumn>
																	<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
																	<FooterStyle HorizontalAlign="Center"></FooterStyle>
																	<FooterTemplate>
																		<asp:LinkButton id="lbtnAdd" runat="server" CommandName="add">
																			<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
																	</FooterTemplate>
																</asp:TemplateColumn>
															</Columns>
															<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
														</asp:datagrid></DIV>
												</TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 23px" align="center" colSpan="7"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnCancel" runat="server" Width="58px" Text="Batal"></asp:button><INPUT class="hideButtonOnPrint" id="btnCetak" style="VISIBILITY: hidden; WIDTH: 56px; HEIGHT: 21px"
														onclick="document.getElementById('btnCetak').style.visibility='hidden';window.print();document.getElementById('btnCetak').style.visibility='visible';" type="button" value="Cetak" name="btnCetak"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px"></TD>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 58px"></TD>
												<TD style="WIDTH: 231px"></TD>
												<TD style="WIDTH: 12px"></TD>
												<TD style="WIDTH: 132px"></TD>
												<TD style="WIDTH: 151px"></TD>
												<TD style="WIDTH: 51px"></TD>
												<TD></TD>
											</TR>
										</TABLE>
									</P>
									<P>&nbsp;</P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</BODY>
</HTML>

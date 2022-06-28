<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrCertificateLine.aspx.vb" Inherits="FrmTrCertificateLine" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrCertificateLine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		
		
		function ClassSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str1 = document.getElementById("txtClassCode");
				var str2 = document.getElementById("txtClassName");
				var str3 = document.getElementById("txtRegNo");
				var str4 = document.getElementById("txtTrainee");
				var str5 = document.getElementById("txtJenisEval");
				var str6 = document.getElementById("txtEvalCode");
				//var btn1 = document.getElementById("btnEvaluasi");
				
				str1.value=tempParam[0];
				str2.value=tempParam[1];
				str3.value='';
				str4.value='';
				str5.value='';
				str6.value='';				
				//btn1.click();
				var txtRegNo = document.getElementById("txtRegNo");
				txtRegNo.focus();
			}
			
			
			function ClearTxt1()
				{	
					var str1 = document.getElementById("txtClassCode");
					var str2 = document.getElementById("txtClassName");
					var str3 = document.getElementById("txtRegNo");
					var str4 = document.getElementById("txtTrainee");
					var str5 = document.getElementById("txtEvalCode");
					var str6 = document.getElementById("txtJenisEval");
					str1.value='';
					str2.value='';
					str3.value='';
					str4.value='';
					str5.value='';
					str6.value='';
				}
				 
			function ClearTxt2()
				{	
					var str1 = document.getElementById("txtRegNo");
					var str2 = document.getElementById("txtTrainee");
					var str3 = document.getElementById("txtEvalCode");
					var str4 = document.getElementById("txtJenisEval");
					str1.value='';
					str2.value='';
					str3.value='';
					str4.value='';
				}
			function ClearTxt3()
				{	
					var str1 = document.getElementById("txtEvalCode");
					var str2 = document.getElementById("txtJenisEval");
					str1.value='';
					str2.value='';
				}								 
							

			
			
			function TraineeSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str2 = document.getElementById("txtRegNo");
				var str3 = document.getElementById("txtTrainee");
				var str4 = document.getElementById("txtJenisEval");
				var str5 = document.getElementById("txtEvalCode");
				
				//var btn1 = document.getElementById("btnEvaluasi");
				
				
				str2.value=tempParam[1];
				str3.value=tempParam[2];
				str4.value="";
				str5.value="";
				//btn1.click();
				var txtEvalCode = document.getElementById("txtEvalCode");
				txtEvalCode.focus();

			}
			
			function CourseEvalSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str1 = document.getElementById("txtEvalCode");
				var str2 = document.getElementById("txtJenisEval");
				var btn1 = document.getElementById("btnEvaluasi");
				str1.value=tempParam[0];
				str2.value=tempParam[1];
				btn1.click();
			}
			
			
						
			function ShowPopupTraineeSelection()
			{
			var txtClassCode = document.getElementById("txtClassCode");
			showPopUp('../PopUp/PopUpClassRegistration.aspx?ClassCode='+txtClassCode.value,'',500,760,TraineeSelection)
			}

			function ShowPopupCourseEvalSelection()
			{
			var txtRegNo = document.getElementById("txtRegNo");
			 if (txtRegNo.value == '')
				{
					alert('No Register masih kosong');
				}
			 else
				{
					showPopUp('../PopUp/PopupCourseEvaluation.aspx?RegNo='+txtRegNo.value,'',500,760,CourseEvalSelection)
				}
			}
			
			
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">TRAINING - Ubah Data Nilai</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kelas</TD>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtClassCode" runat="server" Width="120px"></asp:textbox><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtClassName" runat="server" Width="272px"
										ReadOnly="True"></asp:textbox><INPUT id="hapus1" style="WIDTH: 72px; HEIGHT: 21px" type="button" value="Hapus" onclick="ClearTxt1()">
									<asp:button id="btnClear1" runat="server" Width="48px" Text="X" ForeColor="Black" Visible="False"></asp:button><asp:label id="lblPopUpClass" runat="server" ToolTip="Klik PopUp" width="16px">
										<img style="cursor:hand" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">No. Reg &nbsp;Peserta Training</TD>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<td style="HEIGHT: 18px" width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtRegNo" runat="server" Width="120px"></asp:textbox><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtTrainee" runat="server" Width="272px"
										ReadOnly="True"></asp:textbox><INPUT id="hapus2" style="WIDTH: 72px; HEIGHT: 21px" type="button" value="Hapus" onclick="ClearTxt2()">
									<asp:button id="btnClear2" runat="server" Width="48px" Text="X" ForeColor="Black" Visible="False"></asp:button><asp:label id="lblPopUpTrainee" runat="server" ToolTip="Klik PopUp" width="16px">
										<img style="cursor:hand" src="../images/popup.gif" border="0"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Jenis Evaluasi</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEvalCode" runat="server" Width="120px"></asp:textbox><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJenisEval" runat="server" Width="272px"
										ReadOnly="True"></asp:textbox><INPUT id="hapus3" style="WIDTH: 72px; HEIGHT: 21px" type="button" value="Hapus" onclick="ClearTxt3()">
									<asp:button id="btnClear3" runat="server" Width="48px" Text="X" ForeColor="Red" Visible="False"></asp:button><asp:label id="lblPopUpEval" runat="server" ToolTip="Klik PopUp" width="10px">
										<img style="cursor:hand" src="../images/popup.gif" border="0"></asp:label>&nbsp;&nbsp;&nbsp;<asp:button id="btnEvaluasi" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nilai Angka</TD>
								<TD style="HEIGHT: 26px">:</TD>
								<TD style="HEIGHT: 26px"><asp:textbox onkeypress="return numericOnlyWithComa(event)" onblur="numericOnlyWithComaBlur(txtNilaiAngka)"
										id="txtNilaiAngka" runat="server" Width="32px" ToolTip="Hanya diisi dengan angka" MaxLength="3"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtNilaiAngka"
										ErrorMessage="Hanya diisi dengan angka" ValidationExpression="\d{1,3}"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Nilai Sikap</TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNilaiSikap" runat="server" Width="32px"
										ToolTip="diisi dengan Nilai A1 atau A2, A3, A4. A5, B1 .. B5, C1..C5, D1..D5" MaxLength="2"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgCertificateLine" runat="server" Width="100%" ForeColor="Gray" CellSpacing="1"
								PageSize="25" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
								BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClassRegistration.ID" HeaderText="No Reg">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblClass" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClassRegistration.TrTrainee.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClassRegistration.TrTrainee.Name" HeaderText="Nama Siswa">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblTraineeName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClassRegistration.TrTrainee.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrCourseEvaluation.Name" HeaderText="Kategori Evaluasi">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCourseEvaluation.Name") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrCourseEvaluation.Type" HeaderText="Jenis Penilaian">
										<HeaderStyle Width="11%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.TrCourseEvaluation.Type"),string) = 0, "Nilai Angka", IIf(CType(DataBinder.Eval(Container, "DataItem.TrCourseEvaluation.Type"),string) = 1, "Nilai Sikap", "Nilai Prestasi")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="NumTestResult" HeaderText="Nilai Angka">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNilaiAngka runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NumTestResult") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CharTestResult" HeaderText="Nilai Sikap">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNilaiSikap runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CharTestResult") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CommandName="View" CausesValidation="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 40px"></TD>
				</TR>
				<TR>
					<TD></TD>
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
	</body>
</HTML>

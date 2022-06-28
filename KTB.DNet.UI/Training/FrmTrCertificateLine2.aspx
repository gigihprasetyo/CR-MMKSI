<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrCertificateLine2.aspx.vb" Inherits="FrmTrCertificateLine2" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrCertificateLine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ClassSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				var str1 = document.getElementById("txtClassCode");
				var str2 = document.getElementById("txtClassName");
				var str3 = document.getElementById("txtRegNo");
				var str4 = document.getElementById("txtTrainee");
				{
					str1.value=tempParam[0];
					str2.value=tempParam[1];
					str3.value='';
					str4.value='';
					var txtRegNoVar = document.getElementById("txtRegNo");
					txtRegNoVar.focus();
				}
			}
			
			function TraineeSelection(selectedCode)
			{
				var tempParam= selectedCode.split(';');
				//var str1 = document.getElementById("txtHidenCourseID");
				var str2 = document.getElementById("txtRegNo");
				var str3 = document.getElementById("txtTrainee");
				var btn1 = document.getElementById("btnRefresh");
				//str1.value=tempParam[0];
				str2.value=tempParam[1];
				str3.value=tempParam[2];
				btn1.click();
			}
		
			
			function ShowPopupTraineeSelection()
			{
			var txtClassCode = document.getElementById("txtClassCode");
			showPopUp('../PopUp/PopUpClassRegistration.aspx?ClassCode='+txtClassCode.value,'',500,760,TraineeSelection)
			}

			
			
			
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 18px">TRAINING - Input Data Nilai Per Siswa</td>
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
								<TD style="HEIGHT: 19px" width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtClassCode" runat="server" Width="136px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Width="6px" ErrorMessage="*" ControlToValidate="txtClassCode"></asp:requiredfieldvalidator><asp:textbox id="txtClassName" runat="server" Width="272px" ReadOnly="True"></asp:textbox><asp:label id="lblPopUpClass" runat="server" ToolTip="Klik PopUp" width="16px">
										<img style="cursor:hand" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">No. Reg &nbsp;Peserta Training</TD>
								<TD style="HEIGHT: 18px" width="1%">:</TD>
								<td style="HEIGHT: 18px" width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtRegNo" runat="server" Width="136px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" Width="6px" ErrorMessage="*" ControlToValidate="txtTrainee"></asp:requiredfieldvalidator><asp:textbox id="txtTrainee" runat="server" Width="272px" ReadOnly="True"></asp:textbox><asp:label id="lblPopUpTrainee" runat="server" ToolTip="Klik PopUp" width="16px">
										<img style="cursor:hand" src="../images/popup.gif" border="0"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD style="HEIGHT: 25px"></TD>
								<TD style="HEIGHT: 25px"><asp:button id="btnRefresh" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgCertificateLine" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray"
								PageSize="25" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
								BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
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
									<asp:TemplateColumn SortExpression="EvaluationCode" HeaderText="Jenis Test">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblJenisTest" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
							
									
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama Evaluasi">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="LabelEvaluationName" runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn SortExpression="Type" HeaderText="Jenis Penilaian">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.Type"),string) = 0, "Nilai Angka", IIf(CType(DataBinder.Eval(Container, "DataItem.Type"),string) = 1, "Nilai Sikap", "Nilai Prestasi")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Angka">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="TxtAngka" runat="server" Width="40px" ToolTip="diisi dengan angka skala [0,100]"
												BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
											<asp:Label id="lblMsg" runat="server" ForeColor="Red"></asp:Label>
											<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="TxtAngka" ErrorMessage="diisi angka"
												Enabled="False" ValidationExpression="\d{1,3}"></asp:RegularExpressionValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Sikap">
										<HeaderStyle Width="9%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtSikap" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="2px"
												MaxLength="2"></asp:TextBox>
											<asp:Label id="lblIndikator" runat="server" ForeColor="Red"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Prestasi">
										<HeaderStyle Width="9%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtPrestasi" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="2px"
												MaxLength="2"></asp:TextBox>
											<asp:Label id="lblIndikator2" runat="server" ForeColor="Red"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 40px"><asp:button id="btnInsert" runat="server" Width="72px" Text="Simpan"></asp:button></TD>
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

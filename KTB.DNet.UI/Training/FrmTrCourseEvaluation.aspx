<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrCourseEvaluation.aspx.vb" Inherits="FrmTrCourseEvaluation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Form Training Course Evaluation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">TRAINING - Jenis Evaluasi</td>
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
								<TD class="titleField" style="HEIGHT: 33px" width="24%">Kategori Pelatihan</TD>
								<TD style="HEIGHT: 33px" width="1%">:</TD>
								<td style="HEIGHT: 33px" width="75%"><asp:dropdownlist id="ddlCourse" runat="server" Width="208px" AutoPostBack="True"></asp:dropdownlist>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlCourse"></asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:checkbox id="ChkTampil" runat="server" AutoPostBack="True" Checked="True" Text="Tampilkan Per Kategori Pelatihan"
										Font-Bold="True"></asp:checkbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Tipe</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlType" runat="server" Width="88px" AutoPostBack="True"></asp:dropdownlist>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlType"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 14px">Jenis Test</TD>
								<TD style="HEIGHT: 14px">:</TD>
								<TD style="HEIGHT: 14px"><asp:dropdownlist id="ddlJenisTest" runat="server" Width="88px" AutoPostBack="True"></asp:dropdownlist>&nbsp;
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ControlToValidate="ddlJenisTest" ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="rbInitialTest" runat="server" Width="120px" AutoPostBack="True" Text="Initial Test"
										Enabled="False" GroupName="1" Visible="False"></asp:radiobutton><asp:radiobutton id="rbTest" runat="server" Width="184px" AutoPostBack="True" Text="Test 1 , Test 2, ... ,Test 7"
										Enabled="False" GroupName="1" Visible="False"></asp:radiobutton><asp:radiobutton id="rbFinalTest" runat="server" AutoPostBack="True" Text="Final Test" Enabled="False"
										GroupName="1" Visible="False"></asp:radiobutton></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px">Nama Evaluasi</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<td style="HEIGHT: 23px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCourseEvaluation" runat="server"
										Width="456px" Height="24px" MaxLength="100"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCourseEvaluation"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 60px">Deskripsi</TD>
								<TD style="HEIGHT: 60px">:</TD>
								<TD style="HEIGHT: 60px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDescription" runat="server" Width="456px"
										Height="52px" MaxLength="250" Rows="3" TextMode="MultiLine"></asp:textbox>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Maksimum 250 karakter"
										ControlToValidate="txtDescription" ValidationExpression="^[\s\S]{0,250}$"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD><asp:label id="lblEvalCode" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dtgCourseEvaluation" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray"
								PageSize="50" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
								BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="EvaluationCode" HeaderText="Kode">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrCourse.CourseName" HeaderText="Kategori">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCategory" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EvaluationCode" HeaderText="Jenis Test">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblJenisTest" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Evaluasi">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Type" HeaderText="Tipe">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.Type"),string) = 0, "Nilai Angka", IIf(CType(DataBinder.Eval(Container, "DataItem.Type"),string) = 1, "Nilai Sikap", "Nilai Prestasi")) %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
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

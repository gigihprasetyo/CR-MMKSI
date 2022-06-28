<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrPreRequire_backup.aspx.vb" Inherits="FrmTrPreRequire" smartNavigation="False" %>
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
		<script language="javascript">
			function ShowPPCourseSelection1()
			{
				showPopUp('../PopUp/PopUpCourse.aspx','',500,760,courseSelection1);
			}
			
			function courseSelection1(selectedCourse)
			{
				
				var txtKodeKategori = document.getElementById("txtCourseCode");
				txtKodeKategori.value = selectedCourse;	
			}
			function ShowPPCourseSelection2()
			{
				//showPopUp('../PopUp/PopUpCourse.aspx','',500,760,courseSelection2);
				showPopUp('../PopUp/PopUpCourseCheck.aspx','',500,760,courseSelection2);
			}
			
			function courseSelection2(selectedCourse)
			{
				
				var txtKodeKategori = document.getElementById("txtPreRequireCode");
				txtKodeKategori.value = selectedCourse;	
			}
			function ShowPPCourseSelection3()
			{
				//showPopUp('../PopUp/PopUpCourse.aspx','',500,760,courseSelection2);
				showPopUp('../PopUp/PopUpCourseCheck.aspx','',500,760,courseSelection3);
			}
			
			function courseSelection3(selectedCourse)
			{
				
				var txtPreRequireCodeNoPass = document.getElementById("txtPreRequireCodeNoPass");
				txtPreRequireCodeNoPass.value = selectedCourse;	
			}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Training&nbsp;-&nbsp;Prasyarat
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
								<TD class="titleField" width="24%">Kode Kategori</TD>
								<TD width="1%">:</TD>
								<td width="75%">
									<asp:textbox id="txtCourseCode" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
										Width="120px"></asp:textbox>
									<asp:label id="lblPopUpCourse1" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator8" runat="server" ErrorMessage="*" ControlToValidate="txtCourseCode"></asp:RequiredFieldValidator></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px">Kode Prasyarat Lulus</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<td style="HEIGHT: 16px">
									<P>
										<asp:textbox id="txtPreRequireCode" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="20"
											Width="120px"></asp:textbox>
										<asp:label id="lblPopUpCourse2" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
									</P>
								</td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px">Kode Prasyarat Belum Lulus</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<td style="HEIGHT: 16px">
									<P>
										<asp:textbox id="txtPreRequireCodeNoPass" onkeypress="return HtmlCharUniv(event)" runat="server"
											MaxLength="20" Width="120px"></asp:textbox>
										<asp:label id="lblPreRequireCodeNoPass" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
									</P>
								</td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px">Deskripsi</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px"><asp:textbox id="txtDesc" onkeypress="return HtmlCharUniv(event)" runat="server" Width="300px"
										MaxLength="250" TextMode="MultiLine" Rows="3"></asp:textbox>
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ValidationExpression="^[\s\S]{0,250}$"
										ErrorMessage="*" ControlToValidate="txtDesc" Display="Dynamic"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<P><asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>&nbsp;
										<asp:button id="btnBatal" runat="server" width="60px" Text="Batal" CausesValidation="False"></asp:button>
										<asp:button id="btnCari" runat="server" width="60px" Text="Cari" CausesValidation="False"></asp:button>
									</P>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dtgPreRequire" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
								CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowSorting="True" PageSize="50"
								Width="100%" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif" AllowCustomPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrCourse.CourseCode" HeaderText="Kode Kategori">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Runat="server">
												<%# DataBinder.Eval(Container, "DataItem.TrCourse.CourseCode") %>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PreRequireCourseCode" SortExpression="PreRequireCourseCode" HeaderText="Kode Prasyarat">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Type Prasyarat" SortExpression="RequireType">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lbType" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="35%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
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

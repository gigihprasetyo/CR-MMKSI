<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClassRegistration1.aspx.vb" Inherits="FrmTrClassRegistration1" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrClassRegistration1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			/*function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
			function fillMonth()
			{
				var today = new Date();
				var cbYear = document.getElementById("ddlYear");
				var cbMonth = document.getElementById("ddlMonth");
				
				if (cbMonth.length > 0)
				{
					removeMonth(cbMonth)
				}
				cbMonth.options[0].value = "";
				if (cbYear.value == today.getFullYear())
				{

				}
				else
				{
					
				}
			}
			
			function removeMonth(control)
			{
				for(int x=0;x<control.length;x++)
				{
					control.options[x].value = null;
				}
			}
			*/

			function popUpClassInformation(kode)
			{		
				var url = '../PopUp/PopUpClassInformation.aspx?kode='+kode;
				showPopUp(url,'',320,440,null);
			}

			function ShowPPCourseSelection()
			{
				showPopUp('../PopUp/PopUpCourse.aspx','',550,760,courseSelection);
			}
			
			function courseSelection(selectedCourse)
			{
				
				var txtKodeKategori = document.getElementById("txtCourseCode");
				txtKodeKategori.value = selectedCourse;	
			}

		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">TRAINING - Pendaftaran</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD class="titleField">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="10%">Tahun Mulai</TD>
								<td width="1%">:</td>
								<TD width="10%"><asp:dropdownlist id="ddlYear" runat="server"></asp:dropdownlist></TD>
								<td class="titleField" width="10%">Bulan Mulai</td>
								<td width="1%">:</td>
								<TD width="15%"><asp:dropdownlist id="ddlMonth" runat="server">
										<asp:ListItem Value="" Selected="True">Pilih</asp:ListItem>
										<asp:ListItem Value="01">Januari</asp:ListItem>
										<asp:ListItem Value="02">Februari</asp:ListItem>
										<asp:ListItem Value="03">Maret</asp:ListItem>
										<asp:ListItem Value="04">April</asp:ListItem>
										<asp:ListItem Value="05">Mei</asp:ListItem>
										<asp:ListItem Value="06">Juni</asp:ListItem>
										<asp:ListItem Value="07">Juli</asp:ListItem>
										<asp:ListItem Value="08">Agustus</asp:ListItem>
										<asp:ListItem Value="09">September</asp:ListItem>
										<asp:ListItem Value="10">Oktober</asp:ListItem>
										<asp:ListItem Value="11">November</asp:ListItem>
										<asp:ListItem Value="12">Desember</asp:ListItem>
									</asp:dropdownlist></TD>
								<td class="titleField" width="10%">Kategori</td>
								<td width="1%">:</td>
								<TD width="30%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCourseCode" runat="server" MaxLength="20"
										Width="120px"></asp:textbox><asp:label id="lblPopUpCourse" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" width="10%"><asp:button id="btnSearch" runat="server" Text=" Cari " Font-Bold="True"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:Label id="lblInformation" runat="server" Width="792px" Height="64px"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 450px" DESIGNTIMEDRAGDROP="169"><asp:datagrid id="dtgClassAllocation" runat="server" Width="100%" PageSize="25" AllowPaging="True"
								Font-Size="Small" AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
								CellPadding="3" GridLines="Vertical" CellSpacing="1" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseCode" HeaderText="Kode Kategori">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink id="lnkClass" runat="server"></asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.Location") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Allocated" SortExpression="Allocated" ReadOnly="True" HeaderText="Alokasi">
										<HeaderStyle Width="17%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Sisa Alokasi">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblAllocationRemaining" runat="server" Width="68px"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetail" runat="server" Width="36px" CommandName="Detail" ToolTip="Pilih Siswa">
												<img src="../images/add.gif" border="0" alt="Daftar"></asp:LinkButton>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClassRegistrationByTrainee1.aspx.vb" Inherits="FrmTrClassRegistrationByTrainee1" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrClassRegistrationByTrainee1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						TRAINING - Data Status Siswa - Daftar</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<colgroup>
								<col width="14%">
								<col width="1%">
								<col width="25%">
								<col width="24%">
								<col width="1%">
								<col width="35%">
							</colgroup>
							<TR>
								<TD class="titleField">Nama Peserta</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblTraineeName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Kode Dealer</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblDealerName" runat="server"></asp:label></TD>
								<TD class="titleField"></TD>
								<td width="1%"></td>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td><strong>
							<asp:Label id="Label6" runat="server">&nbsp;</asp:Label></strong>
					</td>
				</tr>
				<tr>
					<td><strong>
							<asp:Label id="lblNote" runat="server"></asp:Label></strong>
					</td>
				</tr>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dtgClassAllocation" runat="server" Width="90%" PageSize="10" AllowPaging="True"
								Font-Size="Small" AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px"
								BackColor="#E0E0E0" CellPadding="3" GridLines="Vertical" CellSpacing="1" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:RadioButton Runat="server" ID="rbSelect" OnCheckedChanged="OnlyOneChecked" AutoPostBack="True"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseCode" HeaderText="Kode Kategori">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.Location") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Allocated" SortExpression="Allocated" ReadOnly="True" HeaderText="Alokasi">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Sisa Alokasi">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAllocationRemaining" runat="server" Width="68px"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center"><asp:button id="btnBack" runat="server" Width="80px" Font-Bold="True" Text="Kembali"></asp:button><asp:button id="btnDaftar" runat="server" Width="80px" Font-Bold="True" Text="Daftar"></asp:button></TD>
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

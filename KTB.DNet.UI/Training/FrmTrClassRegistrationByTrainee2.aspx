<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClassRegistrationByTrainee2.aspx.vb" Inherits="FrmTrClassRegistrationByTrainee2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrClassRegistrationByTrainee2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function RedirectAfterSave()
			{
				var urlDefault="../Training/FrmTrTrainee1.aspx";
				alert("Simpan Berhasil");
				location.replace(urlDefault);
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="740" border="0">
				<tr>
					<td class="titlePage">TRAINING - Data Status Siswa - Konfirmasi Pendaftaran</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 80px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="740" border="0">
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
								<TD colspan="4">
									<asp:Label id="lblTraineeName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField">Kode Dealer</TD>
								<td width="1%">:</td>
								<TD colspan="4">
									<asp:Label id="lblDealerCode" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<td width="1%">:</td>
								<TD>
									<asp:Label id="lblDealerName" runat="server"></asp:Label></TD>
								<TD class="titleField"></TD>
								<td width="1%"></td>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblErrorMessage" runat="server" ForeColor="Red"></asp:label><BR>
						<asp:label id="lblRegSuccessNote" runat="server">Nama siswa di atas akan diikutsertakan pada kelas dibawah ini. Silakan klik OK untuk memproses atau Kembali untuk melakukan perubahan. </asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id="dtgClassRegSuccess" runat="server" Font-Size="Small" AutoGenerateColumns="False"
							BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3"
							GridLines="Vertical" CellSpacing="1" Width="740px">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo1" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="Reg ID">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kelas">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<TR>
					<TD align="right"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblRegFailNote" runat="server">Maaf, ada kelas lain yang jadwalnya bersamaan</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id="dtgClassRegFail" runat="server" Font-Size="Small" AutoGenerateColumns="False"
							BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3"
							GridLines="Vertical" CellSpacing="1" Width="740px">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo2" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="Reg ID">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kelas Bentrok">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<TR>
					<TD align="right"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblRegPassRegisterNote" runat="server">Maaf sudah ke Register atau Pass.</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id="dtgClassRegPassRegister" runat="server" Font-Size="Small" AutoGenerateColumns="False"
							BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3"
							GridLines="Vertical" CellSpacing="1" Width="740px">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo3" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="Reg ID">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kelas">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblProcessNote" runat="server">Klik Ubah untuk kembali ke layar sebelumnya atau klik OK untuk memproses data ini</asp:label></TD>
				</TR>
				<TR>
					<TD align="center"><asp:button id="btnBack" runat="server" Width="80px" Text="Ubah" Font-Bold="True"></asp:button>
						<asp:button id="btnSubmit" runat="server" Width="80px" Text="OK" Font-Bold="True"></asp:button></TD>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrintTrTrainee1.aspx.vb" Inherits="FrmPrintTrTrainee1" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPrintTrTrainee1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<TD class="titlePage">TRAINING - Data Status Siswa</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgTrainee" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3"
							BackColor="#CDCDCD" BorderWidth="1px" BorderStyle="None" BorderColor="#666666" PageSize="25" ForeColor="GhostWhite"
							Font-Names="MS Reference Sans Serif" Font-Bold="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="No Reg">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Siswa">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Kode Dealer">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerCode" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="StartWorkingDate" SortExpression="StartWorkingDate" HeaderText="Mulai Bekerja"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JobPosition" SortExpression="JobPosition" HeaderText="Posisi">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EducationLevel" SortExpression="EducationLevel" HeaderText="Pendidikan">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Daftar Course">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblCourses" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Belum Lulus MSTEP">
									<HeaderStyle Font-Bold="True"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblMSTEP" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Belum Lulus General">
									<HeaderStyle Font-Bold="True"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblGeneral" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Junior">
									<ItemTemplate>
										<asp:Label id="lblJr" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox3" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Senior">
									<HeaderStyle Font-Bold="True"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSr" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Master">
									<HeaderStyle Font-Bold="True"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblMr" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Specialist E/G & F/S">
									<HeaderStyle Font-Bold="True"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSrE" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Specialist D/T & E/C">
									<HeaderStyle Font-Bold="True"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSrD" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<center><INPUT class="hideButtonOnPrint" id="btnCetak" onclick="window.print()" type="button" value="Cetak" name="btnCetak" runat="server">
					<asp:button class="hideButtonOnPrint" id="btnDownload" runat="server" Text="Download"></asp:button>
					<asp:button class="hideButtonOnPrint" id="btnBack" runat="server" Text="Kembali"></asp:button>
			</center>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrRegistrationStatus.aspx.vb" Inherits="FrmTrRegistrationStatus" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Status Pendaftaran</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			//function showPopUp(Url, Parameters, Height, Width)
			//{
			//	var strFeature = 'dialogHeight:' + Height + 'px;';	
			//	strFeature += 'dialogWidth:' + Width + 'px';
			//	strFeature += 'center:yes;';	
			//	strFeature += 'status:no;';
			//	strFeature += 'help:no;';
			//	strFeature += 'resizable:no;';
			//	
			//	window.showModalDialog(Url, Parameters,strFeature);
			//}
			
			function popUpClassInformation(kode)
			{		
				var url = '../PopUp/PopUpClassInformation.aspx?kode='+kode;
				showPopUp(url,'',320,440,null);
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">TRAINING - Status Pendaftaran</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgClassRegistration" runat="server" CellSpacing="1" GridLines="Vertical" CellPadding="3"
							BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
							Font-Size="Small" PageSize="25" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="RegistrationDate" HeaderText="Tgl Pendaftaran">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegistrationDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No Reg">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kota Dealer">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblClassName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Mulai">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblStartDate runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Selesai">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblEndDate runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TRTrainee.JobPosition" HeaderText="Posisi">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TRTrainee.Jobposition") %>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TRTrainee.Jobposition") %>' ID="Textbox1" NAME="Textbox1">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TRTrainee.ShirtSize" HeaderText="Size">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TRTrainee.ShirtSize") %>' ID="Label6" NAME="Label6">
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TRTrainee.ShirtSize") %>' ID="Textbox2" NAME="Textbox2">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="CertificateNo" SortExpression="CertificateNo" ReadOnly="True"
									HeaderText="No Sertifikat">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" height="40"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" align="center" height="40"><INPUT class="hideButtonOnPrint" id="btnPrint" style="WIDTH: 43px; HEIGHT: 21px" onclick="window.print()"
							type="button" value="Cetak" name="btnPrint"><asp:button class="hideButtonOnPrint" id="btnDownload" BackColor="#E0E0E0" Runat="server" text="Download"
							ForeColor="Black"></asp:button><asp:button class="hideButtonOnPrint" id="btnBack" Runat="server" text="Kembali"></asp:button>
					</TD>
				</TR>
			</TABLE>
		</FORM>
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

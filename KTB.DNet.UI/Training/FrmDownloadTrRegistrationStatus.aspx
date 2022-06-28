<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadTrRegistrationStatus.aspx.vb" Inherits="FrmDownloadTrRegistrationStatus" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Status Pendaftaran</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; MARGIN: 0px; COLOR: #990000; FONT-FAMILY: Sans-Serif, Arial">TRAINING 
						- Status Pendaftaran</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgClassRegistration" runat="server" Width="100%" AutoGenerateColumns="False"
							GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CDCDCD"
							ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" ForeColor="Black"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="3%" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="RegistrationDate" HeaderText="Tgl Pendaftaran">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="10%" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegistrationDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No Reg">
									<ItemTemplate>
										<asp:Label id="Label15" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="30%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="15%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="15%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kota Dealer">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="15%" BackColor="#666666"></HeaderStyle>
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
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="5%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="25%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblClassName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Mulai">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="25%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblStartDate runat="server" >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Selesai">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="25%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblEndDate runat="server" >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TRTrainee.JobPosition" HeaderText="Posisi">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="15%" BackColor="#666666"></HeaderStyle>
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
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="5%" BackColor="#666666"></HeaderStyle>
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
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" Width="7%" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" height="40"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" align="center" height="40"></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownloadTrTrainee1.aspx.vb" Inherits="FrmDownloadTrTrainee1" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Download Data Status Siswa</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; MARGIN: 0px; COLOR: #990000; FONT-FAMILY: Sans-Serif, Arial">TRAINING 
						- Data Status Siswa</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgTrainee" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
							CellPadding="3" BackColor="#CDCDCD" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CDCDCD" ForeColor="GhostWhite"
							CellSpacing="1" Font-Names="MS Reference Sans Serif">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" ForeColor="Black"></ItemStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="No Reg">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Siswa">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Kode Dealer">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerCode" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="StartWorkingDate" SortExpression="StartWorkingDate" HeaderText="Mulai Bekerja"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JobPosition" SortExpression="JobPosition" HeaderText="Posisi">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EducationLevel" SortExpression="EducationLevel" HeaderText="Pendidikan">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Daftar Course">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblCourses" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Belum Lulus MSTEP">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblMSTEP" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Belum Lulus General">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblGeneral" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Junior">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblJr" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox3" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Senior">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSr" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Lulus Master">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblMr" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox5" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

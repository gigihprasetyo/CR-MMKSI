<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterKondisiDownload.aspx.vb" Inherits="FrmMasterKondisiDownload"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMasterKondisiDownload</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Content-Type" content="application/x-download">
		<meta http-equiv="Content-Disposition" content="attachment;filename=MasterKondisi.xls">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<h3>Master Kondisi</h3>
			<asp:datagrid id="dgMasterKondisi" runat="server" Width="100%" CellSpacing="1" PageSize="50" GridLines="Vertical"
				BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
				BackColor="White" CellPadding="3" AutoGenerateColumns="False" ShowFooter ="True">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="No">
						<HeaderStyle ForeColor="White" Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblNo" runat="server"></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Type">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id=lblType runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode")%>' Width="53px">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Nama Kendaraan">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblVechileTypeDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>' >
							</asp:Label>
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Left"></FooterStyle>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Tgl Berlaku">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblTglBerlaku runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ValidFrom", "{0:dd/MM/yyyy}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Retail">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblRetailPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice", "{0:0}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="SPAF" HeaderText="Nilai SPAF (%)">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblSPAF" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SPAF", "{0:0}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Subsidi" HeaderText="SPAF per Unit (Rp)">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="lblSubsidi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Subsidi", "{0:0}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Subsidi" HeaderText="PPh">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh", "{0:0}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Subsidi" HeaderText="SPAF setelah PPh (Rp)">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AfterPPh", "{0:0}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="Subsidi" HeaderText="PPN">
						<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPn", "{0:0}") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</form>
	</body>
</HTML>

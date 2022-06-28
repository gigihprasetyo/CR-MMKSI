<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFSCampaignDetail.aspx.vb" Inherits="FrmFSCampaignDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFSKindOnVechileType</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE -&nbsp; Detail Free Service Campaign</td>
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
								<TD class="titleField" width="14%">Deskripsi</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:Label id="lblDescription" Width="400px" Runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="14%">Kode Free Service</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:Label id="lblFSKind" Width="400px" Runat="server"></asp:Label></TD>
							</TR>
							<!--<TR>
								<TD class="titleField" width="14%">Error Message</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:Label id="lblMessage" Width="400px" Runat="server"></asp:Label></TD>
							</TR>-->
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<asp:Button id="btnBackTop" runat="server" Width="64px" Text="Kembali" CausesValidation="False"></asp:Button>
					</td>
				</tr>
				<TR>
					<td><td>&nbsp;</td></td>
				</TR>
				<TR>
					<td class="titleField">Data Dealer</td>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 200px">
							<asp:datagrid id="dtgDealer" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
								ForeColor="Gray" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNoDealer" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="True" SortExpression="DealerCode" DataField="DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="True" SortExpression="DealerName" DataField="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="" HeaderText="Kota">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKota" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="" HeaderText="Group">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGroup" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<td class="titleField">Data Type Kendaraan</td>
				</TR>
				<tr>
					<td>
						<div id="div2" style="OVERFLOW: auto; HEIGHT: 200px">
							<asp:datagrid id="dtgVehicle" runat="server" Width="100%" BorderStyle="None" AllowSorting="True"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
								GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNoVehicle" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="" HeaderText="Kategori">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCategory" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="" HeaderText="Kode">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="" HeaderText="Keterangan">
										<HeaderStyle Width="60%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblVehicleDesc" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnBack" runat="server" Width="64px" Text="Kembali" CausesValidation="False"></asp:Button>
					</td>
				</tr>
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
		<!--<asp:BoundColumn Visible="True" SortExpression="DealerName" DataField="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>-->
	</body>
</HTML>

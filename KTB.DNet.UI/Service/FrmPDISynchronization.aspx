<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPDISynchronization.aspx.vb" Inherits="FrmPDISynchronization" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPDISynchronization</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="400" border="0">
				<TR>
					<TD class="titlePage">PDI synchronous to&nbsp;SAP</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 36px"><INPUT id="dfPDI" type="file" name="File2" runat="server" onkeypress="return false;">
						<asp:button id="btnUploadSave" runat="server" Text="Upload"></asp:button></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgPDI" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" CellSpacing="1"
							Width="100%" BorderWidth="0px" CellPadding="3" GridLines="Horizontal" BackColor="#CDCDCD">
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Chassis Master ID">
									<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Dealer ID">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="kind" HeaderText="Kind">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PDIDate" HeaderText="PDI Date">
									<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD>
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

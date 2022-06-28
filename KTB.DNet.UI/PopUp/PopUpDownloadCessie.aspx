<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDownloadCessie.aspx.vb" Inherits="PopUpDownloadCessie" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDownloadCessie</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
				CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
				AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True"
				DataKeyField="ID">
				<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
				<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
				<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="No">
						<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" ID="lblNo">
								<%# Container.ItemIndex+1 %>
							</asp:Label>
						</ItemTemplate>
						
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="No Cessie">
						<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" ID="Label1">
								<%# DataBinder.Eval(Container.DataItem,"CessieNumber") %>
							</asp:Label>
						</ItemTemplate>
						
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Waktu Download">
						<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" ID="Label2">
								<%# DataBinder.Eval(Container.DataItem,"DownloadedTime") %>
							</asp:Label>
						</ItemTemplate>
						
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Download Oleh">
						<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" ID="Label3">
								<%# DataBinder.Eval(Container.DataItem,"Downloadedby") %>
							</asp:Label>
						</ItemTemplate>
						
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReferenceHelper.aspx.vb" Inherits="ReferenceHelper" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReferenceHelper</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">Referensi Keseluruhan</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top"><br>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 440px"><asp:datagrid id="dtgReferences" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
								CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowCustomPaging="True" PageSize="25" ForeColor="GhostWhite"
								CellSpacing="1" Font-Names="MS Reference Sans Serif" Width="100%">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="White" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Left" ForeColor="White" VerticalAlign="Top" BackColor="#F7F7F7"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Type" HeaderText="Tipe">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblType" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Pengguna">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="70%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="txtDescription" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine"
												Rows="2"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center"><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="50px" Text="Batal"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayRole.aspx.vb" Inherits="FrmDisplayRole" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmGroup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	</HEAD>
	<BODY>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
			
		</script>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ADMIN SISTEM - Daftar Role</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kode Organisasi</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:label id="lblOrgCode" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField">Nama Organisasi</TD>
								<TD>:</TD>
								<td>
									<P><asp:label id="lblOrgName" runat="server"></asp:label></P>
								</td>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblDealer" runat="server">Organisasi</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtKodeDealer" runat="server" Width="176px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><cc1:compositefilter id="cfRole" runat="server" DataGridSouce="dtgRole"></cc1:compositefilter></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dtgRole" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
								CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True"
								AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
								<SelectedItemStyle Font-Bold="True" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Org">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblKodeOrgs runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RoleName" SortExpression="RoleName" HeaderText="Nama Role">
										<HeaderStyle Width="14%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Tanggal">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTgl runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedTime", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Update Oleh">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblUpdateBy runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedBy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RoleStatus" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RoleStatus") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Ubah" CommandName="View" CausesValidation="False"
												ToolTip="Detail">
												<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False"
												ToolTip="Ubah">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
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
	</BODY>
</HTML>

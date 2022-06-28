<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTemplateDealerSelection.aspx.vb" Inherits="FrmDealerSelection" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDealerSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">DEALER - Daftar Dealer</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" width="20%">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtDealerName" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%">Term Cari 1</TD>
								<TD width="1%">:</TD>
								<TD width="33%"><asp:textbox id="txtSearch1" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">Grup</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:dropdownlist id="ddlGroup" runat="server" Width="152px">
										<asp:ListItem Selected="True"></asp:ListItem>
									</asp:dropdownlist></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px">Term Cari 2</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox id="txtSearch2" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px">Kota</TD>
								<TD style="HEIGHT: 15px">:</TD>
								<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlCity" runat="server" Width="152px">
										<asp:ListItem Selected="True"></asp:ListItem>
									</asp:dropdownlist></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD colSpan="7"><asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False">
										<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<Columns>
											<asp:TemplateColumn>
												<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
												<ItemTemplate>
													<asp:CheckBox id="CBSelection" runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="No">
												<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Kode Dealer">
												<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="DealerName" HeaderText="Nama Dealer">
												<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Kota">
												<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblCity" runat="server" Text=""></asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox id="TextBox2" runat="server" Text="" BackColor="Transparent"></asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Grup">
												<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox id="TextBox3" runat="server" Text=""></asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SearchTerm1" HeaderText="Term Cari 1">
												<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="SearchTerm2" HeaderText="Term Cari 2">
												<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD><asp:button id="btnChoose" runat="server" Text="Pilih" width="60px"></asp:button>&nbsp;
									<asp:button id="btnClose" runat="server" Width="56px" Text="Tutup"></asp:button></TD>
								<TD></TD>
								<TD><asp:textbox id="txtSelectResult" runat="server" Visible="False" Enabled="False"></asp:textbox></TD>
								<TD style="WIDTH: 17px"></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventEmail.aspx.vb" Inherits="FrmEventEmail" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Event Email</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">EVENT - Email</td>
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
								<TD class="titleField" style="WIDTH: 88px; HEIGHT: 26px" width="88">Nama</TD>
								<TD width="1%" style="HEIGHT: 26px">:</TD>
								<TD style="WIDTH: 262px; HEIGHT: 26px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;+=`~{}');" id="txtName" onblur="omitSomeCharacter('txtKode','<>?*%$;');"
										runat="server" Width="200px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Alamat Email</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;=+`~()![]/{}');" id="txtEmail"
										onblur="omitSomeCharacter('txtDeskripsi','<>?*%$;');" Width="200px" Rows="1" Runat="server" MaxLength="30"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 262px" width="262"><asp:button id="btnSave" runat="server" width="70px" Text="Simpan"></asp:button>&nbsp;
									<asp:button id="btnCancel" runat="server" width="70px" Text="Cancel"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dgEventEmail" runat="server" Width="100%" PageSize="25" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server" text= '<%# container.itemindex+1 %>'>></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Email" HeaderText="Alamat email">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Email") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp; 
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
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
	</body>
</HTML>

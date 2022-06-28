<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmProfileHeader.aspx.vb" Inherits="FrmProfileHeader" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmProfileHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td colspan="2">
						<TABLE id="titlePage" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">UMUM &nbsp;- Daftar Profile</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class=titleField><asp:label id="lblKodeProfile" runat="server">Kode Profile</asp:label></TD>
					<TD><asp:textbox id="txtProfileCode" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtProfileCode','<>?*%$;')"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class=titleField><asp:label id="lblDescription" runat="server">Deskripsi</asp:label></TD>
					<TD><asp:textbox id="txtDescription" runat="server" Width="219px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtDescription','<>?*%$;')"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class=titleField>Status</TD>
					<TD>
						<asp:DropDownList id="ddlStatus" runat="server" Width="80px"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:button id="btnSearch" runat="server" Text="Cari" Width="72px"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="2"><div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dtgProfileHeader" runat="server" PageSize="25" Width="100%" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="1px" BackColor="#E0E0E0" CellPadding="3" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
								<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle CssClass="titleTableService" VerticalAlign="Top" ForeColor="white"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
								<Columns>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Kode">
										<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblKode runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDeskripsi runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ControlType" HeaderText="Control">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblControl" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id=lbtnEdit runat="server" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>'>
												<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
											<asp:LinkButton id=lbtnViewDetails runat="server" CommandName="Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>'>
												<img src="../images/Detail.gif" border="0" alt="Details"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" text='<%#DataBinder.Eval(Container,"DataItem.ID")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#003399" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<tr>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

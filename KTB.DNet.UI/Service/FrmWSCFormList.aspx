<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWSCFormList.aspx.vb" Inherits=".WSCSupportPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmManualDocList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Form Pendukung</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<br>
			<table cellSpacing="1" cellPadding="2" width="100%">
				<tr>
					<td class="titleField" width="30%">Nama&nbsp;
					</td>
					<td><asp:textbox id="txtName" runat="server" Width="200px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField" width="30%">Deskripsi&nbsp;
					</td>
					<td><asp:textbox id="txtDescription" runat="server" Width="200px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtDescription','<>?*%$;')"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField" width="30%">&nbsp;
					</td>
					<td><asp:button id="btnSearch" runat="server" Text="Cari" Width="56px"></asp:button></td>
				</tr>
			</table>
			<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dtgManualDoc" runat="server" Width="100%" PageSize="25" AllowPaging="True"  AllowCustomPaging="True"
								AllowSorting="True" BackColor="#E0E0E0" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="1px" CellPadding="3" BorderStyle="None">
								<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
								<FooterStyle ForeColor="#DEDEDE" BackColor="White"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNama" runat="server" Text='<%# DataBinder.eval(Container,"DataItem.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDeskripsi" runat="server" text='<%# DataBinder.eval(Container,"DataItem.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Terakhir">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLastUpdate" runat="server" Text='<%# DataBinder.eval(Container,"DataItem.LastUpdateTime") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDownload" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.eval(Container,"DataItem.FileName") %>'>
												<img src="../images/download.gif" border="0"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
											<%--<asp:Label id="lbtnUpdStatus" runat="server">
												<img src="../images/new.gif" border="0" alt=""></asp:Label>--%>
                                            <asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="ID" HeaderText="ID">
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.eval(Container,"DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAlertManagemenList.aspx.vb" Inherits="FrmAlertManagemenList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPopUpAlertSound()
		{
			showPopUp('../PopUp/PopUpAlertSound.aspx','',500,760,'');
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">ALERT MANAGEMENT - Alert 
						Managemen List</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td class="titleField" width="20%">Nama&nbsp;</td>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtName" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
							runat="server" Width="242px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField" width="20%">Kategori&nbsp;</td>
					<td><asp:dropdownlist id="ddlAlertCategory" runat="server" Width="242px"  AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="titleField" width="20%">Modul&nbsp;</td>
					<td><asp:dropdownlist id="ddlAlertModul" runat="server" Width="242px"></asp:dropdownlist></td>
				</tr>
				<tr >
					<td class="titleField" width="20%" valign="bottom"><asp:checkbox id="chkCreatedDate" runat="server"></asp:checkbox>&nbsp;Tanggal dibuat</td>
					<td>
						<table  cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s.d&nbsp;</td>
								<td><cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="titleField" width="20%">&nbsp;</td>
					<td><asp:button id="btnSearch" runat="server" Width="56px" Text="Cari"></asp:button></td>
				</tr>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 440px"><asp:datagrid id="dgAlerts" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
											PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" DataKeyField="ID">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTablePromo" ></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Alert">
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Category">
													<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblCategory" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Modul">
													<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblModul" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkbtnPopUp" runat="server" Text="Dana Babit" CausesValidation="False" CommandName="AlertSound" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Dana Babit" src="../images/popup.gif" border="0"></asp:LinkButton>
														<asp:LinkButton id="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
														<asp:LinkButton id="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
														<asp:LinkButton ID="lnkbtnDelete" Runat = "server" Width = "20px" Text = "Hapus" CausesValidation = "False" CommandName ="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Delete" onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif"
																border="0"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

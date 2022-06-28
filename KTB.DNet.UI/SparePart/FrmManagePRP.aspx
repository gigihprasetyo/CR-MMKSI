<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmManagePRP.aspx.vb" Inherits="KTB.DNet.UI.SparePart.FrmManagePRP" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmManagePRP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
			EnableDelete(aspCheckBoxID)
		}
		
		function EnableDelete(aspCheckBoxID) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			document.forms[0].btnDelete.disabled = true
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						if (elm.checked)
						{
							document.forms[0].btnDelete.disabled = false
							return
						}
					}
				}
			}
		}
		
		function yearFocus(objYear)
		{
			if (objYear.value == "-Tahun-")
			{
				objYear.value = "";
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 8px">PARTSHOP REWARD 
						PROGRAM&nbsp;-&nbsp;Pengelolaan PRP</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 26px" width="24%">Periode</TD>
								<TD style="HEIGHT: 26px" width="1%">:</TD>
								<TD style="WIDTH: 100%; HEIGHT: 26px">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td noWrap><asp:dropdownlist id="ddlBeginMonth" runat="server" Width="96px"></asp:dropdownlist><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtBeginYear" onfocus="yearFocus(this);"
													runat="server" Width="70px" MaxLength="4" Wrap="False"></asp:textbox></td>
											<td>&nbsp;-&nbsp;</td>
											<td noWrap><asp:dropdownlist id="ddlEndMonth" runat="server" Width="96px"></asp:dropdownlist><asp:textbox onkeypress="return numericOnlyUniv(event)" id="txtEndYear" onfocus="yearFocus(this);"
													runat="server" Width="70px" MaxLength="4" Wrap="False"></asp:textbox></td>
											<td><asp:customvalidator id="periodValidator" runat="server"></asp:customvalidator></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 12px" width="24%">Kategori</TD>
								<TD style="HEIGHT: 12px" width="1%">:</TD>
								<TD style="WIDTH: 261px; HEIGHT: 12px" width="261"><asp:dropdownlist id="ddlCategory" runat="server" Width="270px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px"></TD>
								<TD style="HEIGHT: 16px"></TD>
								<TD style="WIDTH: 261px; HEIGHT: 16px"><asp:button id="btnFilter" runat="server" Width="70px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 261px; HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dtgPRP" runat="server" Width="100%" AllowSorting="True" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" PageSize="25">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
													<HeaderStyle Width="0%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="0%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="cbAll" onclick="CheckAll('cbItem',document.forms[0].cbAll.checked)" type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Period" HeaderText="Periode">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPeriod" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PRPCategory.CategoryName" HeaderText="Kategori">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblCategory runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRPCategory") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Filename" SortExpression="Filename" HeaderText="Nama File">
													<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
													<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkDelete" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6"><asp:button id="btnDelete" runat="server" Width="72px" Text="Hapus" Enabled="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

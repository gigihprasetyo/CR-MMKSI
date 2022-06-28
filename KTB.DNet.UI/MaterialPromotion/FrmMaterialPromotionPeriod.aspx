<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMaterialPromotionPeriod.aspx.vb" Inherits="FrmMaterialPromotionPeriod" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMaterialPromotionPeriod</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%">
				<TR>
					<TD colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">MATERIAL PROMOSI - Periode Material Promosi</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 10px" width="15%">Periode</TD>
					<TD class="titleField" style="HEIGHT: 10px" width="2%">:</TD>
					<TD vAlign="top">
						<table>
							<tr>
								<td><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>s.d
								</td>
								<td><cc1:inticalendar id="IcEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 10px" width="15%">Nama Periode</TD>
					<TD class="titleField" style="HEIGHT: 10px" width="2%">:</TD>
					<TD vAlign="top"><asp:textbox id="txtPeriodName" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server" onblur="omitSomeCharacter('txtPeriodName','<>?*%$;')" Width="192px"></asp:textbox></TD>
				</TR>
				<TR vAlign="top">
					<TD class="titleField" style="HEIGHT: 10px" width="15%">Deskripsi</TD>
					<TD class="titleField" style="HEIGHT: 10px" width="2%">:</TD>
					<TD><asp:textbox id="txtDesc" runat="server" Rows="4" TextMode="MultiLine" Width="200px"></asp:textbox>
				</TR>
				<TR>
					<td></td>
					<td></td>
					<TD><asp:button id="btnSImpan" runat="server" Width="50px" Text="Simpan"></asp:button>&nbsp;<asp:button id="btnBatal" runat="server" Width="50px" Text="Batal"></asp:button></TD>
</TD>
				</TR>
				<TR>
					<TD colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgtPromotionPeriod" runat="server" PageSize="25" AllowCustomPaging="True" AllowPaging="True"
							AutoGenerateColumns="False" AllowSorting="True" BackColor="#dedede" CellSpacing="1" CellPadding="2" GridLines="None">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
							<HeaderStyle ForeColor="white"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblNo" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Periode" SortExpression="PeriodName">
									<HeaderStyle Width = "15%" CssClass="titleTablePromo">
									</HeaderStyle>
									<ItemStyle HorizontalAlign="left">
									</ItemStyle>
									<ItemTemplate>
										<asp:Label ID = "lblPeriodName" Runat = "server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodName") %>'/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="StartDate" HeaderText="Periode Mulai">
									<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="center"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblStartMonth" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartDate") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="EndDate" HeaderText="Periode Berakhir">
									<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="center"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblEndMonth" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndDate") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle Width="30%" CssClass="titleTablePromo"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											<img src="../images/detail.gif" border="0" alt="lihat"></asp:LinkButton>
										<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											<img onclick="return confirm('Anda yakin?');" src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

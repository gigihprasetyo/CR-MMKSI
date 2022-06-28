<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDealerInSPL.aspx.vb" Inherits="PopUpDealerInSPL" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar SPL - Detail Dealer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="7">
						<P>Daftar SPL - Detail Dealer</P>
					</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 15px">Nomor Aplikasi :
						<asp:Label id="lblNoAplikasi" runat="server"></asp:Label></TD>
					<TD style="HEIGHT: 15px" colspan="6"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 15px"></TD>
					<TD style="HEIGHT: 15px"></TD>
					<TD style="HEIGHT: 15px"></TD>
					<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
					<TD style="HEIGHT: 15px"></TD>
					<TD style="HEIGHT: 15px"></TD>
					<TD style="HEIGHT: 15px"></TD>
				</TR>
				<TR>
					<TD colSpan="7">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgDaftarDealer" runat="server" AutoGenerateColumns="False" Width="100%" AllowCustomPaging="True"
								AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral" ForeColor="#ffffff"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="3%" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle CssClass="titleTableGeneral" ForeColor="#ffffff"></HeaderStyle>
										<ItemStyle VerticalAlign="Top" Width="20%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode" )  %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="40%" CssClass="titleTableGeneral" ForeColor="#ffffff"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName" )  %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.SearchTerm1" HeaderText="Term 1">
										<HeaderStyle CssClass="titleTableGeneral" ForeColor="#ffffff" Width="30%"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblSearchTerm1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.SearchTerm1" )  %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
							<div align=center>
						<INPUT style="WIDTH: 64px; HEIGHT: 21px" type="button" onclick="window.close();" value="Tutup"></div></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

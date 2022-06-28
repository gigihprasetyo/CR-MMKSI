<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrReminder.aspx.vb" Inherits="FrmTrReminder" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrReminder</title>
		<meta name="LastUpdate" content="January 03, 2006">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/printstyle.css" type="text/css" rel="stylesheet" media="print">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">
						Training - Reminder</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD class="titleField" width="24%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD noWrap width="420"><asp:label id="lblDealerID" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD width="420">
									<P><asp:label id="lblDealerName" runat="server"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Kota</TD>
								<TD>:</TD>
								<TD width="420">
									<P>
										<asp:Label id="lblCity" runat="server"></asp:Label></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Periode</TD>
								<TD>:</TD>
								<TD width="420">
									<asp:Label id="lblPeriode" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<asp:datagrid id="dtgReminder" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
							CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
							AllowCustomPaging="True" AllowSorting="True" PageSize="75" ForeColor="GhostWhite" CellSpacing="1"
							Font-Names="MS Reference Sans Serif" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="No Daftar">
									<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No Reg">
									<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblTraineeID" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama">
									<HeaderStyle Width="22%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblName" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ID" HeaderText="Kelas">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblClass" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStartDate" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblFinishDate" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
									<HeaderStyle Width="22%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblLocation" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 10px" vAlign="top" align="left">
						<asp:Label id="lblNotes" runat="server">Notes</asp:Label>
					</TD>
				</TR>
				<TR>
					<TD align="left" valign="top">
						<asp:TextBox id="txtNotes" runat="server" Width="100%" TextMode="MultiLine" ReadOnly="True" Rows="2"
							BorderStyle="None" BackColor="#E0E0E0" Height="40px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD align="center" height="40">&nbsp;
						<asp:Button id="btnCetak" runat="server" Text="Cetak"></asp:Button>&nbsp;&nbsp;&nbsp;
						<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button>
					</TD>
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
	</body>
</HTML>

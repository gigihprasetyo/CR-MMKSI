<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryPDIViaText.aspx.vb" Inherits="FrmEntryPDIViaText" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellspacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PRE DELIVERY INSPECTION&nbsp;- Upload PDI</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD width="24%" class="titleField"><asp:label id="lblDealer" runat="server" Width="88px">Kode Dealer </asp:label></TD>
								<TD WIDTH="1%">:</TD>
								<td width="75%"><asp:label id="lblDealerCode" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<td><asp:label id="lblDealerName" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField">
									<asp:Label id="lblFileName" runat="server" ToolTip="KodeDealer, KodeCabang, No.Rangka, No.Mesin, Jenis PDI, Tgl. PDI(ddmmyyyy)">File dengan pemisah koma (,)</asp:Label></TD>
								<td>:</td>
								<TD><INPUT id="dfChassis" type="file" size="40" name="File1" runat="server">
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="dfChassis"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:RegularExpressionValidator>
									<asp:button id="btnUpload" runat="server" Width="70px" Text="Upload" Height="19px"></asp:button>
                                    <asp:button id="btnDownload" runat="server" Width="110px" Text="Download Sample" Height="19px"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 390px"><asp:datagrid id="dtgPDIUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="Gainsboro"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical" AllowPaging="True" PageSize="50" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Width="53px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Kode Cabang">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerBranch" runat="server" Width="53px" ToolTip='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. Rangka ">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblChassisNo" runat="server" Width="197px"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>									
									<asp:TemplateColumn HeaderText="Jenis PDI">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblStatusPDI runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kind") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal PDI">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglPDI runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PDIDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="WO Number">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblWONumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pesan">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblMessage runat="server" Width="189px" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button>&nbsp;&nbsp;</TD>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEquipmentBOMMaintenance.aspx.vb" Inherits="FrmEquipmentBOMMaintenance" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BOM Maintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">EQUIPMENT REPAIR - Master BOM</td>
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
								<TD class="titleField" WIDTH="24%">Pilih Lokasi File</TD>
								<td>:</td>
								<TD><INPUT id="DataFile" style="WIDTH: 205px; HEIGHT: 20px" type="file" size="15" name="File1"
										runat="server" onkeypress="return false;">
									<asp:button id="btnUpload" runat="server" Text="Upload" Width="70px"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 440px"><asp:datagrid id="dtgBOMUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical" PageSize="5" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Order Information">
										<HeaderStyle HorizontalAlign="Center" Width="80%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<TABLE cellSpacing="1" cellPadding="3" width="100%">
												<TR class="titleTableService">
													<TH align="left">
														Kode Equipment</TH>
													<TH align="left">
														Nama Equipment</TH>
													<TH align="left">
														Keterangan</TH></TR>
												<TR class="tablebody">
													<TD vAlign="top" align="left"><%# DataBinder.Eval(Container.DataItem, "EquipmentNumber") %></TD>
													<TD vAlign="top" align="left"><%# DataBinder.Eval(Container.DataItem, "EquipmentDescription") %></TD>
													<TD vAlign="top" align="left"><%# DataBinder.Eval(Container.DataItem, "ErrorMessage") %></TD>
												</TR>
											</TABLE> <!-- Order Details DataGrid goes here -->
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left"><asp:button id="btnSave" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:button>&nbsp;&nbsp;</TD>
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

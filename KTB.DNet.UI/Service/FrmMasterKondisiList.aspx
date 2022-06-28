<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterKondisiList.aspx.vb" Inherits="FrmMasterKondisiList"%>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Upload PM</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPopUp()
			{
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE - Upload Free Service</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 40px" vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 229px"><asp:label id="lblFileName" runat="server" ToolTip="Tipe, Tgl Berlaku, Harga Retail, SPAF, Subsidy">Lokasi File </asp:label></TD>
								<td>:</td>
								<TD style="WIDTH: 844px"><INPUT onkeypress="return false;" id="dfMasterKondisi" style="WIDTH: 224px; HEIGHT: 20px"
										type="file" size="18" name="dfMasterKondisi" runat="server">
									<asp:button id="btnUpload" runat="server" Height="19px" Text="Upload" Width="70px"></asp:button><asp:button id="btnSimpan" runat="server" Height="19px" Text="Simpan" Width="70px"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 229px; HEIGHT: 29px" width="229"><asp:label id="lblTipe" runat="server">Tipe</asp:label></TD>
								<TD style="HEIGHT: 29px" width="1%">:</TD>
								<td style="WIDTH: 844px; HEIGHT: 29px" width="844"><asp:dropdownlist id="ddlTipe" Width="220px" Runat="server"></asp:dropdownlist></td>
								<TD class="titleField" style="WIDTH: 429px; HEIGHT: 29px" vAlign="middle" width="229"><asp:checkbox id="chkTglBerlaku" runat="server" Height="4px" Text="Tgl Berlaku" AutoPostBack="True"></asp:checkbox><cc1:inticalendar id="icTglBerlaku" runat="server" Enabled="False" TextBoxWidth="60"></cc1:inticalendar></TD>
							<TR>
								<TD class="titleField" style="WIDTH: 229px; HEIGHT: 19px">Status</TD>
								<TD style="HEIGHT: 19px">:</TD>
								<td style="WIDTH: 844px; HEIGHT: 19px"><asp:dropdownlist id="ddlStatus" Width="124px" Runat="server"></asp:dropdownlist><asp:button id="btnSearch" runat="server" Height="19px" Text="Cari" Width="70px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto"><asp:datagrid id="dgMasterKondisi" runat="server" Width="100%" CellSpacing="1" PageSize="50" GridLines="Vertical"
								CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="Gainsboro" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer ">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' Tooltip = '<%#DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Width="53px">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No Chassis">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblChassisNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' >
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl PM">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglPM runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Stand KM">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStandKM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StandKM", "{0:#,###}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPopUpDetail" runat="server">
												<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pesan">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblMessage runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="left" height="40"><asp:button id="btnDownload" runat="server" Text="Download" Width="60px" Enabled="False"></asp:button>&nbsp;&nbsp;</TD>
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

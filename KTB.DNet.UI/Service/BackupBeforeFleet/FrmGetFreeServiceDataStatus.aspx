<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGetFreeServiceDataStatus.aspx.vb" Inherits="FrmGetFreeServiceDataStatus" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar Status Free Service</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
		
			
			function showReasonDescription(reasonID,description)
			{
				var URL = "FrmFSReasonDescription.aspx?ID=" + reasonID + "&description=" + description;
				window.showModalDialog(URL);
				return false;
			}
			
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE - Daftar Status Free Service</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 14px" width="24%"><asp:label id="lblDealer" runat="server">Kode Dealer </asp:label></TD>
								<TD style="HEIGHT: 14px" width="1%">:</TD>
								<TD style="HEIGHT: 14px" width="25%">
									<asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"
										onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<td width="20%"></td>
								<td width="1%"></td>
								<td width="29%"></td>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField">Tipe FS</TD>
								<TD>:</TD>
								<TD>
									<asp:DropDownList id="ddlFSType" runat="server" Width="104px">
										<asp:ListItem Value="0">Semua</asp:ListItem>
										<asp:ListItem Value="Z1" Selected="True">Regular</asp:ListItem>
										<asp:ListItem Value="Z3">Campaign</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="titleField">Kode Penolakan</TD>
								<TD>:
								</TD>
								<TD>
									<asp:DropDownList id="ddlRejectStatus" runat="server">
										<asp:ListItem Value="Semua">Semua</asp:ListItem>
										<asp:ListItem Value="APP" Selected="True">APP</asp:ListItem>
										<asp:ListItem Value="DAPP">DAPP</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px" vAlign="top">Periode Rilis</TD>
								<TD style="HEIGHT: 18px" vAlign="top">:</TD>
								<TD style="HEIGHT: 18px" vAlign="top">
									<table cellpadding="0" cellspacing="0">
										<tr>
											<td>
												<cc1:inticalendar id="ICDari" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>
												&nbsp;s.d&nbsp;</td>
											<td>
												<cc1:inticalendar id="ICSampai" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD><STRONG><asp:Label Runat="server" ID="lblCategori">Kategori</asp:Label></STRONG></TD>
								<TD><asp:Label Runat="server" ID="lblCategori2">:</asp:Label></TD>
								<TD>
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<td></td>
								<td></td>
								<td><asp:button id="btnRefresh" runat="server" Width="60px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="HEIGHT: 280px; OVERFLOW: auto"><asp:datagrid id="dgFreeService" runat="server" Width="100%" CellSpacing="1" AllowSorting="True"
								PageSize="50" AllowPaging="True" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF"
								AutoGenerateColumns="False" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<B>Baru:</B>
											<asp:Label id="lblTotalNew" runat="server"></asp:Label><BR>
											<B>Proses:</B>
											<asp:Label id="lblTotalProcessed" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNumber" HeaderText="No Rangka">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNoChassis runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNumber")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<b>Disetujui:</b>
											<asp:Label id="lblTotalApp" runat="server"></asp:Label>
											<br>
											<b>Tidak disetujui:</b>
											<asp:Label id="lblTotalDisapp" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CategoryCode" HeaderText="Kategori" Visible="False">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryCode")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
										</FooterTemplate>
									</asp:TemplateColumn>

<asp:TemplateColumn SortExpression="NoRegRequest" HeaderText="No Extended Free Service">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoRegRequest" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoRegRequest") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
										</FooterTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn SortExpression="KindCode" HeaderText="Jenis FS">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ServiceDate" SortExpression="ServiceDate" HeaderText="Tgl Servis"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="False" SortExpression="SoldDate" HeaderText="Tgl Penjualan">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTanggalJual" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh"
										DataFormatString="{0:#.###}">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Reject" SortExpression="Reject" HeaderText="Tolak">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<FooterStyle VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ReleaseDate" SortExpression="ReleaseDate" HeaderText="Tgl Rilis" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="NotificationNumber" HeaderText="Notifikasi">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNotifikasi runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NotificationNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Penolakan" SortExpression="ReasonCode">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAlasan" runat="server" Tooltip='<%# DataBinder.Eval(Container, "DataItem.ReasonDescription")%>' Text='<%# DataBinder.Eval(Container, "DataItem.ReasonCode")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblGrandTotalA" runat="server"></asp:Label><BR>
											<asp:Label id="lblGrandTotalD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="ReasonDescription" HeaderText="Deskripsi Alasan">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDeskripsi runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReasonDescription")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LabourAmount" HeaderText="Ongkos Kerja (Rp)">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLabourAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblTotalLabourAmountA" runat="server"></asp:Label><BR>
											<asp:Label id="lblTotalLabourAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartAmount" HeaderText="Penggantian Parts (Rp)">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblTotalPartAmountA" runat="server"></asp:Label><BR>
											<asp:Label id="lblTotalPartAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PPNAmount" HeaderText="PPN (Rp)">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPPNAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblTotalPPNAmountA" runat="server"></asp:Label><BR>
											<asp:Label id="lblTotalPPNAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PPHAmount" HeaderText="PPh (Rp)">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPPHAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblTotalPPHAmountA" runat="server"></asp:Label><BR>
											<asp:Label id="lblTotalPPHAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 40px" align="left">&nbsp;
						<asp:button id="btnDownload" runat="server" Width="70px" Text="Download" Enabled="False" Height="24px"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (document.parentWindow.name != "frMain")
				{
				  self.opener = null;
				  self.close();
				}
		</script>
	</body>
</HTML>

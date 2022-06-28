<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDelivery.aspx.vb" Inherits="FrmDelivery" smartNavigation="False"  %>
<%@ Register TagPrefix="uc1" TagName="Clock" Src="../UserControl/Clock.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDOStatusList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$');
			}
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
		</script>
		<script type="text/javascript">
		
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
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD colSpan="10">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">DO STATUS - Rekap DO</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="20%">&nbsp;Kode Dealer</TD>
					<TD style="WIDTH: 13px" width="13">:</TD>
					<TD width="40%"><asp:textbox id="txtKodeDealer" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeDealer');" runat="server" Width="152px"></asp:textbox>&nbsp;<asp:label id="lblPopUpDealer" runat="server" width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
					<TD class="titleField" width="15%">Status</TD>
					<TD width="1%">:</TD>
					<TD width="29%"><asp:dropdownlist id="ddlStatus" runat="server" Width="130px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:checkbox id="chkTglCetak" runat="server" Text="Tanggal Cetak DO" Checked="True" Height="10px"></asp:checkbox>&nbsp;</TD>
					<TD>:</TD>
					<TD>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td style="WIDTH: 100px"><cc1:inticalendar id="ICDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
								<TD class="titleField">&nbsp;s.d&nbsp;</TD>
								<TD><cc1:inticalendar id="ICSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
							</tr>
						</table>
					</TD>
					<TD class="titleField">Total Biaya Parkir</TD>
					<TD>:</TD>
					<TD>Rp.<asp:label id="lblTotalBiayaParkir" runat="server"></asp:label></TD>
				</TR>
				<TR>
				</TR>
				<TR>
					<TD class="titleField"><asp:checkbox id="chkTglKeluar" runat="server" Text="Tanggal Keluar" Height="10px"></asp:checkbox>&nbsp;</TD>
					<TD>:</TD>
					<TD>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td style="WIDTH: 100px"><cc1:inticalendar id="ICDari2" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
								<TD class="titleField">&nbsp;s.d&nbsp;</TD>
								<TD><cc1:inticalendar id="ICSampai2" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
							</tr>
						</table>
					</TD>
					<TD class="titleField">Total Unit</TD>
					<TD>:</TD>
					<TD><asp:label id="lblTotalChassis" Runat="server"></asp:label></TD>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td>
					<asp:button id="btnRefresh" runat="server" Width="50px" Text="Cari"></asp:button>
					</td></tr>
				<TR>
					<TD vAlign="top" colSpan="10" height="30">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgDeliveryOrder" runat="server" Width="736px" GridLines="Horizontal" CellPadding="3"
								BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" PageSize="25" AllowPaging="True"
								AllowSorting="True" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblIndex" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealer" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.SearchTerm1" HeaderText="Nama Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType" HeaderText="Tipe">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblType" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Unit" HeaderText="Unit">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUnit" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ParkingAmount" HeaderText="Biaya Parkir (Rp)" DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id="lblID" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<td colSpan="6">
						<table cellSpacing="0" cellPadding="2" width="100%" border="0">
							<tr>
								<TD style="WIDTH: 99px" width="99"><asp:button id="btnDownload" runat="server" Width="80px" Text="Download" Enabled="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD width="20%"></TD>
								<TD align="right" width="20%"></TD>
								<TD width="30%"></TD>
								<TD width="10%"></TD>
							</tr>
						</table>
					</td>
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

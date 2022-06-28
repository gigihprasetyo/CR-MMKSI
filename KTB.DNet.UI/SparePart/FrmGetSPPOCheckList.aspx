<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGetSPPOCheckList.aspx.vb" Inherits="GetSPPOCheckList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>GetSPPOCheckList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function Checklist()
		{
		alert ('Detail tidak ditemukan');
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
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" width="100%" cellPadding="0" border="0">
				<tr>
					<td class="titlePage">PEMESANAN&nbsp;- Barang yang Tidak Terpenuhi</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><img src="../images/bg_hor.gif" height="1" border="0"></td>
				</tr>
				<tr>
					<td height="10"><img src="../images/dot.gif" height="1" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR runat="server" id="trDealerLabel1">
								<TD width="24%" class="titleField"><asp:label id="Label2" runat="server"> Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD width="75%"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR runat="server" id="trDealerLabel2">
								<TD class="titleField">
									<asp:label id="Label9" runat="server">Nama Dealer</asp:label></TD>
								<TD></TD>
								<TD>
									<asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
							</TR>
							<TR runat="server" id="trDealerLabel3">
								<TD class="titleField" width="24%">
									<asp:label style="Z-INDEX: 0" id="Label10" runat="server"> Kode Dealer</asp:label></TD>
								<TD width="1%">
									<asp:label style="Z-INDEX: 0" id="Label11" runat="server">:</asp:label></TD>
								<TD width="75%">
									<table cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td>
												<asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
													runat="server"></asp:textbox>
											</td>
											<td>
												<asp:label id="lblSearchDealer" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
												</asp:label>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label1" runat="server">Jenis Order</asp:label></TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlOrderType" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label5" runat="server">Nomor Pesanan</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD><asp:textbox id="txtNoPO" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtNoPO','<>?*%$;')"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label7" runat="server">Tanggal Pesanan</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><table border="0" cellpadding="2" cellspacing="0">
										<tr>
											<td><cc1:inticalendar id="ccPODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="ccPODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<td>
												<asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><div id="div1" style="HEIGHT: 320px; OVERFLOW: auto"><asp:datagrid id="dtgCheckList" runat="server" AutoGenerateColumns="False" GridLines="Vertical"
								CellPadding="3" BackColor="#cdcdcd" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" Width="100%" AllowPaging="True" PageSize="50" AllowCustomPaging="True"
								CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="NO">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle HorizontalAlign="Left" Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDCode" runat="server" Text=''></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle HorizontalAlign="Left" Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDName" runat="server" Text=''></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Pesanan">
										<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPODate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartPO).PODate) %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PONumber" HeaderText="Nomor Pesanan">
										<HeaderStyle Width="24%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Jenis Order">
										<HeaderStyle Width="40%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblOrderType" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).OrderTypeDesc %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDetail" runat="server" ForeColor="Blue" Font-Underline="True">
												<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<asp:button style="Z-INDEX: 0" id="btnDownload" runat="server" Text="Download"></asp:button>
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

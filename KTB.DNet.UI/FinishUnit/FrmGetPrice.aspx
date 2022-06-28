<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGetPrice.aspx.vb" Inherits="FrmGetPrice" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmGetPrice</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
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
					<td class="titlePage" style="WIDTH: 780px">UMUM - Informasi Harga Kendaraan</td>
				</tr>
				<tr>
					<td style="WIDTH: 780px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="WIDTH: 780px" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="WIDTH: 780px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kategori</TD>
								<TD width="1%">:</TD>
								<td noWrap width="29%"><asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True" Width="122px"></asp:dropdownlist>
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlSubCategory" runat="server" AutoPostBack="True" Height="16px"></asp:dropdownlist></td>
								<TD class="titleField" width="20%">Mulai&nbsp;Berlaku :</TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:checkbox id="chkAll" runat="server" Text="Semua"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe</TD>
								<TD>:</TD>
								<td><asp:dropdownlist id="ddlType" runat="server" Width="144px"></asp:dropdownlist>&nbsp;&nbsp;</td>
								<TD></TD>
								<TD></TD>
								<td><cc1:inticalendar id="calCalendar" runat="server"></cc1:inticalendar></td>
							</TR>
							<TR runat="server" id="trDealer">
								<TD class="titleField">Dealer</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" style="Z-INDEX: 0" id="txtKodeDealer"
										onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox>
									<asp:label style="Z-INDEX: 0" id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnCari" runat="server" Width="60px" Text="Cari" style="Z-INDEX: 0"></asp:button></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="WIDTH: 100%; HEIGHT: 395px; OVERFLOW: auto"><asp:datagrid id="dgPrice" runat="server" Width="750px" CellSpacing="1" AutoGenerateColumns="False"
								GridLines="None" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="Ridge" BorderColor="#FEFEFE" AllowSorting="True" AllowCustomPaging="True"
								AllowPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#9471DE"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#C6C3C6"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Kode Kendaraan">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileColor.MaterialDescription" HeaderText="Nama Kendaraan">
										<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialDescription") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer" SortExpression="Dealer.DealerCode">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='' ID="lblDealerCode"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ValidFrom" SortExpression="ValidFrom" ReadOnly="True" HeaderText="Tgl. Mulai Berlaku"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VehiclePrice" SortExpression="VehiclePrice" ReadOnly="True" HeaderText="Harga Kendaraan (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PPh22Amount" SortExpression="PPh22Amount" ReadOnly="True" HeaderText="PPh22 (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 780px; HEIGHT: 2px" height="2"><asp:button id="btnDnLoad" runat="server" Width="60px" Text="Download" Enabled="False"></asp:button></TD>
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

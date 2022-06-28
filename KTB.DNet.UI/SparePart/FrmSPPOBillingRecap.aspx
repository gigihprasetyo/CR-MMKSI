<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPPOBillingRecap.aspx.vb" Inherits="FrmSPPOBillingRecap" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSPPOBillingRecap</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				var lblDealerName = document.getElementById("lblDealerName");
				var TextDealerName = document.getElementById("txtDealerName");
				txtDealerSelection.value = tempParam[0];
				lblDealerName.innerHTML	=tempParam[1];		
				TextDealerName.value=tempParam[1];
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">REKAPITULASI - Rekapitulasi Pembelian</TD>
							</TR>
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
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField">Kode Dealer</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:label id="lblDealerCode" runat="server" Visible="False"></asp:label><asp:textbox id="txtKodeDealer" runat="server" ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:label id="lblDealerName" runat="server"></asp:label><INPUT 
            id=txtDealerName type=hidden name=txtDealerName value="<%= lblDealerName.Text %>"></TD>
							</TR>
							<TR>
								<TD class="titleField">Jenis Order</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:dropdownlist id="ddlOrderType" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Periode</TD>
								<TD>:</TD>
								<TD colSpan="3"><asp:dropdownlist id="ddlMonth" runat="server"></asp:dropdownlist>&nbsp;-
									<asp:dropdownlist id="ddlMonthTo" runat="server"></asp:dropdownlist><asp:comparevalidator id="CompareValidator1" runat="server" Operator="GreaterThanEqual" Type="Integer"
										ControlToValidate="ddlMonthTo" ControlToCompare="ddlMonth" ErrorMessage="*"></asp:comparevalidator>&nbsp;tahun&nbsp;<asp:dropdownlist id="ddlYear" runat="server"></asp:dropdownlist><asp:button id="btnCari" runat="server" Width="56px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" width="120">Total Pembelian (Rp)</TD>
								<TD width="5">:</TD>
								<TD width="150"><asp:label id="lblGrandBillingAmount" runat="server" Font-Bold="True"></asp:label></TD>
								<TD class="titleField" width="150"><asp:label id="lblGrandPPN" runat="server" Font-Bold="True"></asp:label></TD>
								<TD class="titleField" width="200"><asp:label id="lblGrandTotal" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
							<tr>
								<td colSpan="5">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgSPPOBillingRecap" runat="server" Width="100%" GridLines="None" AllowSorting="True"
											AllowPaging="True" AllowCustomPaging="True" AutoGenerateColumns="False" ShowFooter="True" pagesize="25" CellSpacing="1" BorderWidth="0px" BorderColor="Gainsboro"
											BackColor="Gainsboro" CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#ffffcc"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server">x</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Referensi">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPONumber" runat="server">x</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="BillingNumber" HeaderText="Nomor Faktur">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblBillingNumber" runat="server">x</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="BillingDate" HeaderText="Tanggal Faktur">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblBillingDate" runat="server">x</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="BillingAmount" HeaderText="Nilai Faktur">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblBillingAmount" runat="server">x</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:Label id="lblBillingAmountSum" runat="server">x</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PPN" HeaderText="PPN">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPPN" runat="server">x</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:Label id="lblPPNSum" runat="server">x</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTotalBilling" runat="server">x</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:Label id="lblTotalBillingSum" runat="server">x</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="OrderType" HeaderText="Jenis Order">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblOrderType" runat="server">x</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</td>
							</tr>
							<tr>
								<td colSpan="5"><asp:button id="btnDownload" runat="server" Width="80px" Text="Download"></asp:button> <asp:linkbutton id="LinkButton1" runat="server" Visible="False">upload</asp:linkbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="grid" runat="server" AutoGenerateColumns="False" Visible="False"></asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
		//document.getElementById("grid").style.visibility = "hidden";
		</script>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformList.aspx.vb" Inherits="FrmSalesmanUniformList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanUniformList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;
		}
		
		function ShowPPUnifCodeSelection()
		{
			showPopUp('../PopUp/PopUpUnifDistribution.aspx','',500,760,DistributionCodeSelection);
		}
		function DistributionCodeSelection(selectedCode)
		{
			var txtDistCode = document.getElementById("txtDistributionCode");
			txtDistCode.value = selectedCode;
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" border="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">SERAGAM TENAGA PENJUAL -
						<asp:Label id="lblTitle" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="2" width="100%">
				<tr>
					<td class="titleField" width="194" style="WIDTH: 194px">Kode Dealer
					</td>
					<td width="1%">:</td>
					<td>
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerCode" onblur="omitSomeCharacter('txtNoBarang','<>?*%$;')"
							runat="server" Width="136px"></asp:textbox>
						<asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</tr>
				<tr>
					<td class="titleField" style="WIDTH: 194px; HEIGHT: 20px" width="194">Kode Pesanan
					</td>
					<td width="1%">:</td>
					<td style="HEIGHT: 20px">
						<asp:TextBox id="txtDistributionCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
							runat="server" onblur="omitSomeCharacter('txtDistributionCode','<>?*%$')"></asp:TextBox>
						<asp:label id="lblSearchDistCode" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</tr>
				<tr>
					<td class="titleField" width="194" style="WIDTH: 194px">Order No
					</td>
					<td width="1%">:</td>
					<td>
						<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtOrderNo" runat="server"
							onblur="omitSomeCharacter('txtOrderNo','<>?*%$;')"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="titleField" width="194" style="WIDTH: 194px">Order Date&nbsp;
					</td>
					<td width="1%">:</td>
					<td><table>
							<tr>
								<td><cc1:inticalendar id="icTglOrderFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>s/d</td>
								<td><cc1:inticalendar id="icTglOrderUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td></td>
					<td></td>
					<td><asp:button id="btnSearch" runat="server" Text="Cari" Width="56px"></asp:button>
						<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal"></asp:button>
						<asp:button id="btnDownload" runat="server" Width="64px" Text="Download"></asp:button></td>
				</tr>
				<tr>
					<td colspan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgSalesmanUniformPriceList" runat="server" Width="100%" AllowPaging="True"
								AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
								CellPadding="3">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanUnifDistribution.SalesmanUnifDistributionCode" HeaderText="Kode Pesanan">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDistrCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanUnifDistribution.SalesmanUnifDistributionCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="OrderNumber" HeaderText="Order No">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblOrderNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="OrderDate" HeaderText="Tgl Order ">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOrderDate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.OrderDate"),"dd/MM/yyyy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" CommandName="view" CausesValidation="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Edit" CommandName="edit" CausesValidation="False">
												<img src="../images/Edit.gif" border="0" alt="Edit"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

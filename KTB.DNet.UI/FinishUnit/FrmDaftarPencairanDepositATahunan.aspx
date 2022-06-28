<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarPencairanDepositATahunan.aspx.vb" Inherits="FrmDaftarPencairanDepositA" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDaftarDebitNote</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}

			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Sales - Daftar Pencairan Deposit A Tahunan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" style="WIDTH: 146px"><asp:label id="lblCode" Runat="server">Kode Dealer</asp:label></td>
								<td style="WIDTH: 2px">:</td>
								<td>
									<asp:TextBox ID="txtKodeDealer" runat="server" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
									</asp:label>
								</td>
							</tr>
							<tr>
								<TD class="titleField" style="WIDTH: 146px">Periode</TD>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField">
									<table>
										<tr>
											<td><cc1:inticalendar id="icPeriodeFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s/d</td>
											<td><cc1:inticalendar id="icPeriodeTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</td>
							</tr>
                            <tr>

                                	<td class="titleField" style="width: 146px">Produk</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">

                                    <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="WIDTH: 146px">Status</td>
								<td style="WIDTH: 2px">:</td>
								<TD>
									<asp:DropDownList id="ddlStatus" runat="server"></asp:DropDownList>
								</TD>
                            </tr>

							<tr>
								<td colspan="2"></td>
								<td><asp:button id="btnSearch" runat="server" Width="72px" Text="Cari"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD>
						<div>
							<asp:DataGrid id="dgDaftarDepositA" runat="server" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
								BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True" AllowCustomPaging="True"
								PageSize="25" AllowSorting="True">
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblProdukDetail runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="40%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FromDate" HeaderText="Periode">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriode" runat="server" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NettoAmount" ReadOnly="True" HeaderText="Amount" DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Keterangan">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKeterangan" runat="server" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                  

								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid>
						</div>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

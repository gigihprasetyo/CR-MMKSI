<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarInterestDepositA.aspx.vb" Inherits="FrmDaftarInterestDepositA" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDaftarInterestDepositA</title>
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

		function Toggle( commId, imageId )
			{
				
				var div = document.getElementById(commId);
				var GetImg = document.getElementById(imageId);
				if (document.all[commId].style.display == 'none')
				{	
					document.all[commId].style.display = 'block';
					document.all[imageId].src = '../Images/minus.gif';
				}
				else
				{	
					document.all[commId].style.display = 'none';
					document.all[imageId].src = '../Images/plus.gif';
				}
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Sales - DepositA - Daftar Interest Deposit A</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" style="WIDTH: 146px">Kode Dealer</td>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField">
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label>
								</td>
                                <td>

                                </td>
                                 <td class="titleField" style="width: 146px">Produk</td>
                                <td style="width: 2px">:</td>
                                <td class="titleField">

                                    <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                                </td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 146px">Periode</td>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField">
									<asp:DropDownList id="ddlPeriode" runat="server"></asp:DropDownList>
								</td>
                                <td colspan="4"></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 146px">Tahun</td>
								<td style="WIDTH: 2px">:</td>
								<TD>
									<asp:DropDownList id="ddlYear" runat="server"></asp:DropDownList>
								</TD>
                                  <td colspan="4"></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 146px">Status</td>
								<td style="WIDTH: 2px">:</td>
								<TD>
									<asp:DropDownList id="ddlStatus" runat="server"></asp:DropDownList>
								</TD>
                                  <td colspan="4"></td>
							</tr>
							<tr>
								<td colspan="2"></td>
								<td class="titleField"><asp:button id="btnSearch" runat="server" Text="Cari" Width="72px"></asp:button></td>
                                  <td colspan="4"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid id="dgDaftarDepositAInterestH" runat="server" BorderWidth="0px" CellSpacing="1"
							BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="False"
							AllowPaging="True" AllowCustomPaging="True" PageSize="25" AllowSorting="True">
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							<Columns>
								<asp:boundColumn visible="False" datafield="ID" headertext="ID"></asp:boundColumn>
								<asp:TemplateColumn HeaderText="Details">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ITEMSTYLE width="10px" verticalalign="Top"></ITEMSTYLE>
									<ITEMTEMPLATE>
										<IMG id="image_" runat="server" src="../images/plus.gif" border="0" style="cursor:hand">
									</ITEMTEMPLATE>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No.">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
										<ASP:BUTTON id="btnDepositAInterestHID" runat="server" visible="False"></ASP:BUTTON>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblProduk runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="InterestAmount" SortExpression="InterestAmount" ReadOnly="True" HeaderText="Interest"
									DataFormatString="{0:#,###}">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TaxAmount" SortExpression="TaxAmount" ReadOnly="True" HeaderText="Tax (15%)"
									DataFormatString="{0:#,###}">
									<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:templateColumn HeaderText="Netto" SortExpression="NettoAmount">
									<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="Label2" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.NettoAmount"),"0,000") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:templateColumn>
								<asp:TemplateColumn HeaderText="Status Pencairan">
									<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td colspan="3"></td>
					<td colspan="2">
						<TABLE border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td colspan="4">
									<div id="divDepositAInterestD" runat="server" style="display:none">
										<asp:DataGrid id="dgDepositAInterestD" runat="server" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
											BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="False" AllowPaging="False" AllowCustomPaging="True"
											PageSize="25" AllowSorting="False" showheader="True" cellpadding="0" borderstyle="None" visible="True"
											width="100%">
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<ASP:BOUNDCOLUMN visible="False" datafield="ID" headertext="ID"></ASP:BOUNDCOLUMN>
												<asp:templateColumn HeaderText="Month" SortExpression="Month">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Month") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:templateColumn>
												<asp:BoundColumn DataField="InterestAmount" SortExpression="InterestAmount" ReadOnly="True" HeaderText="Interest"
													DataFormatString="{0:#,###}">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="NettoAmount" SortExpression="NettoAmount" ReadOnly="True"
													HeaderText="Netto" DataFormatString="{0:#,###}">
													<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:templateColumn visible="False">
													<itemtemplate>
														<ASP:LABEL id="lblDepositAInterestDId" runat="server" text='<%# DataBinder.Eval (Container.DataItem, "Id") %>'>
														</ASP:LABEL>
														<ASP:LABEL id="lblDepositAInterestHIDForDetails" runat="server" text='<%# DataBinder.Eval (Container.DataItem, "DepositAInterestH.ID") %>'>
														</ASP:LABEL>
													</itemtemplate>
												</asp:templateColumn>
											</Columns>
										</asp:DataGrid>
									</div>
								</td>
							</tr>
						</TABLE>
						</ItemTemplate> </asp:TemplateColumn>
						<asp:TemplateColumn visible="False">
							<itemtemplate>
								<asp:label id="lblDepositAInterestHID" runat="server" text='<%# DataBinder.Eval (Container.DataItem, "Id") %>'>
								</asp:label>
							</itemtemplate>
						</asp:TemplateColumn>
						</Columns> </asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

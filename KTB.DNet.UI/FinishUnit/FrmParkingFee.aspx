<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmParkingFee.aspx.vb" Inherits="FrmParkingFee" smartNavigation="False"%>
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
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
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
					<td class="titlePage">DO STATUS - Penalty Parkir</td>
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
								<td class="titleField"><asp:textbox id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></td>
								<td class="titleField" style="WIDTH: 146px">Status</td>
								<td style="WIDTH: 2px">:</td>
								<TD><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 146px">Periode</td>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField"><asp:dropdownlist id="ddlPeriode" runat="server"></asp:dropdownlist></td>
								<td class="titleField" style="WIDTH: 146px">Kategori</td>
								<td style="WIDTH: 2px">:</td>
								<TD><asp:dropdownlist id="ddlCategory" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 146px">Tahun</td>
								<td style="WIDTH: 2px">:</td>
								<TD><asp:dropdownlist id="ddlYear" runat="server"></asp:dropdownlist></TD>
								<td class="titleField" style="WIDTH: 146px"></td>
								<td style="WIDTH: 2px"></td>
								<TD></TD>
							</tr>
							<tr align="center" >
								<td  colspan="6" class="titleField" align="center"><asp:button id="btnSearch" runat="server" Width="72px" Text="Cari"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgParkingFee" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9CD"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('cbCheck',document.all.chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="cbCheck" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblStatus"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.CreditAccount" HeaderText="Credit Account">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Periode" HeaderText="Periode">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Year" SortExpression="Year" HeaderText="Tahun">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kategori" SortExpression="Category.ID">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "Category.CategoryCode") %>
                                            <asp:Label id="lblhcategory" runat="server" Text=></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Debit Charge">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDebitCharge" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Debit Memo">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDebitMemo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Faktur Pajak">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblFakturPajak" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="File Debit Charge">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkDebitCharge" runat="server" CommandName="lnkDebitCharge"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="File Debit Memo">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkDebitMemo" runat="server" CommandName="lnkDebitMemo"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Surat">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblSurat" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Surat Biaya Parkir"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. JV">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoJV" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Amount" SortExpression="Amount" DataFormatString="{0:#,###}" HeaderText="Biaya">
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
									</asp:BoundColumn>
									<%--<asp:TemplateColumn HeaderText="PPN (10%)">--%>
                                    <asp:TemplateColumn HeaderText="PPN">
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPPN" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="24%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Dipotong dari Dep. A Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerDeposit" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="FileNameParkingFee">
										<HeaderStyle Width="0%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FileNameDebitMemo">
										<HeaderStyle Width="0%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnHistory" runat="server" Text="History" CommandName="History" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/popup.gif" border="0" alt="Lihat History"></asp:LinkButton>
											<asp:LinkButton id=lbtnDelete runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<tr>
					<td>
						<table>
							<tr>
								<td><asp:button id="btnDownload" Text="Download" Runat="server"></asp:button></td>
								<td class="titleField" style="WIDTH: 146px"><asp:label id="lblGenerate" runat="server">No. Bukti Potong PPH :</asp:label></td>
								<td style="WIDTH: 2px"></td>
								<td class="titleField">
									<asp:textbox id="txtGenerate" Width="160px" Runat="server"></asp:textbox>&nbsp;
									<asp:button id="btnGenerate" Text="Generate No. Reg Bukti Potong PPH" Runat="server"></asp:button>&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<!--
									<asp:TemplateColumn HeaderText="Bukti Potong PPh">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblBuktiPotong" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Surat Biaya Parkir"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									-->
	</body>
</HTML>

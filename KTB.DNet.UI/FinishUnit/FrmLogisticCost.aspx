<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLogisticCost.aspx.vb" Inherits="FrmLogisticCost" smartNavigation="False"%>
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


			function isChecked(data)
			{
			    var chkCheckAll = document.getElementById('chkCheckAll');

			    if (data.checked)
			    {
			        chkCheckAll.value = "True";
			    }
			    else
			    {
			        chkCheckAll.value = "False";
			    }
			    return true;
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DO STATUS - Logistic Cost</td>
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
                            <TR>
					<TD class="titleField">Tanggal Invoice</TD>
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
					<TD> </TD>
					<TD></TD>
					<TD></TD>
				</TR>
                            <tr>
                                <td><span style="font-weight:bold;">Total Biaya</span></td>
                                <td><span style="font-weight:bold;">:</span></td>
                                <td><asp:Label ID="lblTotalBiaya" runat="server">0</asp:Label></td>
                                <td colspan="3"></td>
                            </tr>
                                <tr>
                                <td><span style="font-weight:bold;">Total Pph</span></td>
                                <td><span style="font-weight:bold;">:</span></td>
                                <td><asp:Label ID="lblTotalPph" runat="server">0</asp:Label></td>
                                <td colspan="3"></td>
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
                        <asp:HiddenField ID="chkCheckAll" runat="server" Value="False" />
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgLogisticFee" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
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
											<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxesNew('cbCheck', document.all.chkAllItems.checked); isChecked(this);"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="cbCheck" runat="server" ></asp:CheckBox>
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
                                    <asp:TemplateColumn SortExpression="TanggalInvoice" HeaderText="Tanggal Invoice">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTglInvoice" runat="server"></asp:Label>
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
									<asp:BoundColumn Visible="False" DataField="FileNameLogistic"> <%--12 Debit Charge--%>
										<HeaderStyle Width="0%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FileNameDebitMemo"> <%--13 Debit Charge--%>
										<HeaderStyle Width="0%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Amount" SortExpression="Amount" DataFormatString="{0:#,###}" HeaderText="Biaya">
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="PPH (2%)">
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPPH" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
								
									<asp:TemplateColumn>
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnHistory" runat="server" Text="History" CommandName="History" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/popup.gif" border="0" alt="Lihat History"></asp:LinkButton>
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

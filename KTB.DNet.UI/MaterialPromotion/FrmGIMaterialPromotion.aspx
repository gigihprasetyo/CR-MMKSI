<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGIMaterialPromotion.aspx.vb" Inherits="FrmGIMaterialPromotion" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Material Promotion Allocation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}
		
			function ShowMaterialPromotionSelection()
			{
				showPopUp('../General/../PopUp/PopUpMaterialPromotion.aspx','',500,600,MaterialPromotionSelection);
			}
			function MaterialPromotionSelection(selectedMaterial)
			{
				var txtKodeBarang = document.getElementById("txtKodeBarang");
				txtKodeBarang.value = selectedMaterial;	
			}
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			/*function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtDealer");
				txtDealerSelection.value = selectedDealer;			
			}*/
			function DealerSelection(selectedDealer)
			{
				var splitted = selectedDealer.split(";");
				var txtDealerSelection = document.getElementById("txtDealer");
				txtDealerSelection.value = splitted[0];			
			}
			function ShowPPReqNo()
			{
				showPopUp('../PopUp/PopUpMPDetailRequestNo.aspx?time=' + Date.Now,'',500,600,ReqNoSelection);
			}
			function ReqNoSelection(selectedReqNo)
			{
				var txtRequestNo = document.getElementById("txtRequestNo");
				txtRequestNo.value = selectedReqNo;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="titlePage" style="HEIGHT: 19px">MATERIAL PROMOSI&nbsp;-&nbsp;Goods Issue
						</td>
					</tr>
					<tr>
						<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
					</tr>
					<tr>
						<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
					<tr>
						<td style="HEIGHT: 93px">
							<table cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<td class="titleField" width="30%">Kode Dealer</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealer" onblur="omitSomeCharacter('txtDealer','<>?*%$')"
											runat="server"></asp:textbox><asp:label id="lblDealers" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">No Permintaan</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtRequestNo" onblur="omitSomeCharacter('txtRequestNo','<>?*%$')"
											runat="server"></asp:textbox><asp:label id="lblRequestNo" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Kode Barang</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeBarang" onblur="omitSomeCharacter('txtKodeBarang','<>?*%$')"
											runat="server"></asp:textbox><asp:label id="lblKodeBarang" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 19px" width="30%">Periode Alokasi</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="HEIGHT: 19px" width="69%"><asp:dropdownlist id="ddlPeriod" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%"></td>
									<TD width="1%"></TD>
									<TD width="69%"><asp:button id="btnCariGoodIssue" runat="server" Height="21px" Text="Cari " Width="60px"></asp:button>&nbsp;
										<asp:button id="btnCari" runat="server" Text="Cari" Width="60px" Visible="False"></asp:button></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD>
							<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgAlokasi" runat="server" Width="100%" CellPadding="3" BackColor="#E0E0E0" BorderWidth="1px"
									BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowPaging="True" AllowCustomPaging="True" AllowSorting="True" PageSize="25">
									<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
									<HeaderStyle VerticalAlign="Top" ForeColor="white"></HeaderStyle>
									<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
									<Columns>
										<asp:TemplateColumn>
											<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
											<HeaderTemplate>
												<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkPO',
														document.forms[0].chkAllItems.checked)" />
											</HeaderTemplate>
											<ItemTemplate>
												<asp:CheckBox ID="chkPO" Runat="server"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDealer" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>'>
													<img width="100px" alt='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>' >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
											<HeaderStyle width="20%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemTemplate>
												<asp:Label ID="lblDealerName" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
											<HeaderStyle CssClass="titleTablePromo" />
											<ItemTemplate>
												<asp:Label ID="lblDealerCity" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.City.CityName")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="TempRequestNo" HeaderText="No Permintaan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblRequest" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.TempRequestNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.GoodNo" HeaderText="Kode Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblGoodsNo" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.GoodNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.Name" HeaderText="Nama Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="Label1" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.Name")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Qty" HeaderText="Qty Alokasi">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=lblQty runat="server" text='<%#DataBinder.Eval(Container,"DataItem.ValidateQty")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="TempRequestQty" HeaderText="Qty Good Issue">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox id="txtRequestQty" CssClass="textRight" onkeypress="return NumericOnlyWith(event,'')" runat="server" Width="50px" text='<%#DataBinder.Eval(Container,"DataItem.TempRequestQty")%>'>
												</asp:TextBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="QtySisa" HeaderText="Sisa Alokasi">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblSisa" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.QtySisa")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="StatusGI" HeaderText="Status">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblStatusGI" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.StatusGI")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Keterangan" HeaderText="Keterangan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblKeterangan" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn Visible="False" HeaderText="DealerID">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDealerID" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Dealer.ID")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></div>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<tr>
						<td align="center"><asp:button id="btnSimpan" runat="server" Text="Good Issue" Width="80px"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>

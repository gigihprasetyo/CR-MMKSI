<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmGRMaterialPromotion.aspx.vb" Inherits="FrmGRMaterialPromotion" smartNavigation="False" %>
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
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
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
				showPopUp('../General/../PopUp/PopUpMaterialPromotion.aspx','',440,760,MaterialPromotionSelection);
			}
			function MaterialPromotionSelection(selectedMaterial)
			{
				var txtKodeBarang = document.getElementById("txtKodeBarang");
				txtKodeBarang.value = selectedMaterial;	
			}
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',440,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtDealer");
				txtDealerSelection.value = selectedDealer;			
			}
			function ShowPPReqNo()
			{
				showPopUp('../PopUp/PopUpMPDetailRequestNo.aspx?time=' + Date.Now+'&From=GR','',440,760,ReqNoSelection);
			}
			function ReqNoSelection(selectedReqNo)
			{
				var txtRequestNo = document.getElementById("txtRequestNo");
				txtRequestNo.value = selectedReqNo;			
			}
			function ShowPPGINo()
			{
				showPopUp('../PopUp/PopUpMPGoodIssueNo.aspx?time=' + Date.Now+'&From=GR','',440,760,GINoSelection);
			}
			function GINoSelection(selectedGINo)
			{
				var txtNoGI = document.getElementById("txtNoGI");
				txtNoGI.value = selectedGINo;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="titlePage" style="HEIGHT: 18px">MATERIAL 
							PROMOSI&nbsp;-&nbsp;Goods&nbsp;Receipt</td>
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
									<TD width="69%"><asp:textbox id="txtDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
											onblur="omitSomeCharacter('txtDealer','<>?*%$')" Visible="False"></asp:textbox><asp:label id="lblDealers" runat="server" Visible="False">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label>
										<asp:Label id="lblDealer" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">No Permintaan</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox id="txtRequestNo" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
											onblur="omitSomeCharacter('txtRequestNo','<>?*%$;')"></asp:textbox><asp:label id="lblRequestNo" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Kode Barang</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox id="txtKodeBarang" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
											onblur="omitSomeCharacter('txtKodeBarang','<>?*%$;')"></asp:textbox><asp:label id="lblKodeBarang" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">No Good Issue</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox id="txtNoGI" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
											onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:textbox><asp:label id="lblNoGI" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Tgl Good Issue</td>
									<TD width="1%">:</TD>
									<TD width="69%">
										<table>
											<tr>
												<td><cc1:inticalendar id="icTglGIStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
												<td>s/d</td>
												<td><cc1:inticalendar id="icTglGIEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<td class="titleField" width="30%"></td>
									<TD width="1%"></TD>
									<TD width="69%"><asp:button id="btnCari" runat="server" Text="Cari" Width="60px"></asp:button></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dgAlokasi" runat="server" Width="100%" CellPadding="3" BackColor="#E0E0E0" BorderWidth="1px"
									BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowPaging="True" AllowCustomPaging="True" AllowSorting="True" PageSize="25">
									<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle BackColor="White"></ItemStyle>
									<HeaderStyle VerticalAlign="Top" ForeColor="white"></HeaderStyle>
									<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
									<Columns>
										<asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
											<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemTemplate>
												<asp:CheckBox ID="chkPO" Runat="server"></asp:CheckBox>
												<asp:LinkButton ID="IDGI" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>' Runat="server">
												</asp:LinkButton>
											</ItemTemplate>
											<HeaderTemplate>
												<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkPO',
														document.forms[0].chkAllItems.checked)" />
											</HeaderTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDealer" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>'>
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
										<asp:TemplateColumn SortExpression="NoGI" HeaderText="Nomor Good Issue">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblGINo" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.NoGI")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Permintaan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblRequestNoGI" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.RequestNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.GoodNo" HeaderText="Kode Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblGoodNo" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.GoodNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.Name" HeaderText="Nama Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblBarang" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.Name")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Qty" HeaderText="Qty">
											<HeaderStyle Width="7%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblQty" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Qty")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="NoGR" HeaderText="Keterangan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblKeterangan" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.NoGR")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="right" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></div>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<tr>
						<td align="center"><asp:button id="btnSimpan" runat="server" Text="Good Receipt" Width="155px"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>

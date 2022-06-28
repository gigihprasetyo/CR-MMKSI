<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListGRMaterialPromotion.aspx.vb" Inherits="FrmListGRMaterialPromotion" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtDealer");
				txtDealerSelection.value = selectedDealer;			
			}
			function ShowPPReqNo()
			{
				showPopUp('../PopUp/PopUpMPDetailRequestNo.aspx?time='+Date.Now+'&From=GIGR','',500,600,ReqNoSelection);
			}
			function ReqNoSelection(selectedReqNo)
			{
				var txtRequestNo = document.getElementById("txtRequestNo");
				txtRequestNo.value = selectedReqNo;			
			}
			function ShowPPGINo()
			{
				showPopUp('../PopUp/PopUpMPGoodIssueNo.aspx?time=' + Date.Now+'&From=GIGR','',500,620,GINoSelection);
			}
			function GINoSelection(selectedGINo)
			{
				var txtGINo = document.getElementById("txtNoGI");
				txtGINo.value = selectedGINo;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="titlePage">MATERIAL PROMOSI&nbsp;-&nbsp;Daftar Good Issue / Good Receive</td>
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
									<td class="titleField" width="30%">No Good Issue</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtNoGI" onblur="omitSomeCharacter('txtNoGI','<>?*%$')"
											runat="server"></asp:textbox><asp:label id="lblNoGI" runat="server">
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
									<TD width="69%"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD vAlign="top">
							<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dgAlokasi" runat="server" Width="100%" PageSize="25" AllowSorting="True" AllowCustomPaging="True"
									AllowPaging="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" BackColor="#E0E0E0" CellPadding="3">
									<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
									<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
									<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
									<Columns>
										<asp:TemplateColumn>
											<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Image ID="imgStatus" Runat="server"></asp:Image>
												<asp:LinkButton ID="IDGI" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>' Runat="server">
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblDealer" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>'>
													<img width="100px" alt='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>' >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
											<HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemTemplate>
												<asp:Label ID="lblDealerName" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemTemplate>
												<asp:Label ID="lblDealerCity" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.City.CityName")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="NoGI" HeaderText="Nomor Good Issue">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblGINo" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.NoGI")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Permintaan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblRequestNoGI" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.RequestNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.GoodNo" HeaderText="Kode Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblGoodNo" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.GoodNo")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.Name" HeaderText="Kode Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblBarang" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.Name")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Qty" HeaderText="Qty">
											<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblQty" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Qty")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="NoGR" HeaderText="Keterangan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblKeterangan" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.NoGR")%>'>></asp:Label>
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
						<td class="titleField">&nbsp;<asp:dropdownlist id="DdlStatusUbah" runat="server" Width="184px" Visible="False"></asp:dropdownlist><asp:button id="btnSimpan" runat="server" Width="64px" Text="Simpan" Visible="False"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>

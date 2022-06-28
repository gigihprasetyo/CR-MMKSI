<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMaterialPromotionAllocation.aspx.vb" Inherits="FrmMaterialPromotionAllocation" smartNavigation="False" %>
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
				showPopUp('../General/../PopUp/PopUpMaterialPromotion.aspx','',500,760,MaterialPromotionSelection);
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
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="titlePage" style="HEIGHT: 19px">MATERIAL PROMOSI&nbsp;-&nbsp;Alokasi 
							Material Promosi</td>
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
									<td class="titleField" width="30%">Kode Barang</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox id="txtKodeBarang" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
											onblur="omitSomeCharacter('txtKodeBarang','<>?*%$')"></asp:textbox>&nbsp;
										<asp:label id="lblKodeBarang" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Dealer Alokasi</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:textbox id="txtDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
											onblur="omitSomeCharacter('txtDealer','<>?*%$')"></asp:textbox><asp:label id="lblDealers" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 19px" width="30%">Periode Alokasi</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="HEIGHT: 19px" width="69%"><asp:dropdownlist id="ddlPeriod" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 18px" width="30%"></td>
									<TD style="HEIGHT: 18px" width="1%"></TD>
									<TD style="HEIGHT: 18px" width="69%"><asp:checkbox id="chkAlokasi" runat="server" Text="Alokasi >0"></asp:checkbox></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%"></td>
									<TD width="1%"></TD>
									<TD width="69%"><asp:button id="BtnGetData" runat="server" Text="Masukkan Jumlah Alokasi"></asp:button>&nbsp;
										<asp:button id="btnCari" runat="server" Text="Cari" Width="60px"></asp:button></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD style="HEIGHT: 46px"><div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgAlokasi" runat="server" Width="100%" CellPadding="3" BackColor="#E0E0E0" BorderWidth="1px"
									BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowPaging="True" AllowCustomPaging="True" AllowSorting="True" PageSize="15">
									<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle BackColor="White"></ItemStyle>
									<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
									<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=lblDealer runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>'>
													<img width="100px" alt='<%#DataBinder.Eval(Container,"DataItem.Dealer.DealerCode")%>' >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer ">
											<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
											<ItemTemplate>
												<asp:Label ID="lblDealerName" Runat = "server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
											<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
											<ItemTemplate>
												<asp:Label ID="lblDealerCity" Runat = "server" Text = '<%#DataBinder.Eval(Container,"DataItem.Dealer.City.CityName")%>'>
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
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblGoodsName" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.Name")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="MaterialPromotion.Stock" HeaderText="Stock">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=lblStock runat="server" text='<%#Format(DataBinder.Eval(Container,"DataItem.MaterialPromotion.Stock"),"#,##0")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Qty" HeaderText="Alokasi">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox id="txtAlokasi" onkeypress="return numericOnlyUniv(event)" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Qty")%>'>
												</asp:TextBox>
												<asp:Label id="lblAlokasi" runat="server" text='<%#Format(DataBinder.Eval(Container,"DataItem.Qty"),"#,##0")%>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="4%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="Linkbutton2" runat="server" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>'>
													<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Anda Yakin Akan Menghapus?');"></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" ForeColor="#003399" BackColor="#DEDEDE" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></div>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<tr>
						<td><asp:button id="btnSimpan" runat="server" Text="Simpan" Width="64px" Visible="False"></asp:button>
							<asp:Button id="Button1" runat="server" Text="Validasi" Width="88px" Visible="False"></asp:Button>
							<asp:button id="btnDownload" runat="server" Text="Download"></asp:button>
						</td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCeilingVsAllocation.aspx.vb" Inherits="FrmCeilingVsAllocation"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCeilingVsAllocation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPAccountSelection()
		{
			showPopUp('../General/../PopUp/PopUpCreditAccountSelection.aspx','',500,760,AccountSelection);
		}
		
		function AccountSelection(selectedAccount)
		{
			var txtCreditAccount = document.getElementById("txtCreditAccount");
			
			var str = selectedAccount.split(";");
			txtCreditAccount.value = str[0];	
		}
		
		function ShowPopupMaxPO()
		{
			var txtIsShowPopup = document.getElementById("txtIsShowPopup");
			if(txtIsShowPopup.value==1)
			{
				showPopUp('../PopUp/PopUpMaxPO.aspx','',500,760);
			}
		}
		
		
		function ViewDailyPKFlow()
		{}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">CREDIT CONTROL&nbsp;&nbsp;- Ceiling vs Alokasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="22%">Credit Account</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 17px" width="328"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchCreditAccount" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%">Cara Pembayaran</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px" width="20%"><asp:dropdownlist id="ddlPaymentType" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField">Permintaan Kirim</TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px">
									<b>
										<cc1:inticalendar id="icReqDelivery" runat="server" TextBoxWidth="70"></cc1:inticalendar></b>
								</TD>
								<td class="titleField">Sisa Hari Kerja</td>
								<TD>:</TD>
								<TD><asp:textbox id="txtRemainDay" runat="server" Width="140px" MaxLength="20" BackColor="#E0E0E0"
										ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><STRONG style="Z-INDEX: 0">System Date</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px">
									<asp:textbox id="txtReportDate" runat="server" Width="140px" MaxLength="20" BackColor="#E0E0E0"
										ReadOnly="True"></asp:textbox>
								</TD>
								<TD><STRONG>Produk</STRONG></TD>
								<TD>:</TD>
								<TD>
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px"></asp:dropdownlisT></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"><asp:button id="btnCetak" runat="server" Width="60px" Text="Cetak"></asp:button><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnDownload" runat="server" Width="60px" Text="Download"></asp:button><asp:textbox id="txtIsShowPopup" style="VISIBILITY: hidden" runat="server" Width="0px"></asp:textbox></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>&nbsp;
						<DIV id="div1" style="HEIGHT: 320px; OVERFLOW: auto"><asp:datagrid id="dtgMain" runat="server" Width="100%" BackColor="#CDCDCD" AllowSorting="True"
								AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="50" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3"
								ShowFooter="True">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProduk" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategoryCode") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Cara Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaymentType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentType") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblCeiling" runat="server" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Plafon") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFCeiling" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblOutstanding" runat="server" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Outstanding") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFOutstanding" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Sedang Diajukan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblProposed" runat="server" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.ProposedPO") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFProposed" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblRemainCeiling" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFRemainCeiling" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Unit O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblOCQty" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFOCQty" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblOCAmount" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFOCAmount" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Unit Sisa O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblRemainOCQty" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFRemainOCQty" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Sisa O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:linkbutton id="lbtnRemainOCAmount" CommandName="DetailRemainOC" runat="server" style="text-align:right;"></asp:linkbutton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFRemainOCAmount" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Giro Cair Bulan Ini">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblGyro" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFGyro" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Max PO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblMaxPO" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFMaxPO" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Estimasi Akhir Bulan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<footerstyle HorizontalAlign="Right" VerticalAlign="Top"></footerstyle>
										<ItemTemplate>
											<asp:Label id="lblEstimation" runat="server" style="text-align:right;"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label Runat="server" ID="lblFEstimation" style="text-align:right;"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Detail">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetail" runat="server" CommandName="Detail">
												<img src="../images/Popup.gif" alt="Detil Perhitungan Max PO" border="0" style="cursor:hand"></asp:LinkButton>
											<asp:TextBox Runat="server" ID="txtUrlParams" Width="0px" style="visibility:hidden;"></asp:TextBox>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
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

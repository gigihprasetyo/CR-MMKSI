<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListContract.aspx.vb" Inherits="ListContract" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}
		function ViewDailyPKFlow()
		{}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onfocus="return checkModal()" onclick="checkModal()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN - Daftar O/C</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="Label1" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD width="25%"><asp:textbox id="txtDealerCode" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" width="20%"><asp:label id="Label9" runat="server">Nomor O/C</asp:label></TD>
								<TD width="1%"><asp:label id="Label12" runat="server">:</asp:label></TD>
								<TD width="29%"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" onblur="alphaNumericPlusBlur(txtContractNumber)"
										id="txtContractNumber" runat="server" size="22" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Jenis Pesanan</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlOrderType" runat="server" Width="140"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="lblNomorPK" runat="server">Nomor PK</asp:label></TD>
								<TD><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" onblur="alphaNumericPlusSpaceBlur(txtNomorPK)"
										id="txtNomorPK" runat="server" size="22" MaxLength="40"></asp:textbox><asp:requiredfieldvalidator id="valPeriodeMO" runat="server" ErrorMessage="*" Enabled="False" ControlToValidate="txtNomorPK"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label3" runat="server">Periode O/C</asp:label></TD>
								<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlContractPeriod" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="Label10" runat="server">Kategori</asp:label></TD>
								<TD><asp:label id="Label14" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlCategory" runat="server" Width="64px" AutoPostBack="True"></asp:dropdownlist>
									<asp:dropdownlist id="ddlSubCategory" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label11" runat="server">Kondisi Pesanan</asp:label></TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlKondisi" runat="server" Width="140"></asp:dropdownlist></TD>
								<TD class="titleField">
									<asp:label id="Label19" runat="server">Total Quantity</asp:label></TD>
								<TD><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD><table border="0" width="100%" cellpadding="0" cellspacing="0">
										<tr>
											<td width="80%"><B>
													<asp:label id="lblQuantity" runat="server" Font-Bold="True"></asp:label></B></td>
											<td></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"><asp:label id="Label4" runat="server">Total Harga Tebus</asp:label></TD>
								<TD>
									<asp:label id="Label21" runat="server">:</asp:label></TD>
								<TD>
									<asp:Label id="Label20" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
									<asp:label id="lblTotal" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"><asp:CheckBox id="cboxDikunci" runat="server" Text="Dikunci"></asp:CheckBox></TD>
								<TD></TD>
								<TD><asp:button id="btnCari" runat="server" Width="60px" Text=" Cari "></asp:button>
								</TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dgContract" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True"
											OnPageIndexChanged="dgContract_PageIndexChanged" AllowPaging="True" PageSize="25" CellPadding="3" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
											BackColor="#CDCDCD" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport',document.all.chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="ChkExport" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="Status">
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>' Visible="False">
														</asp:Label>
														<asp:Label id="lblStatusString" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ContractNumber" SortExpression="ContractNumber" HeaderText="No O/C">
													<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="RefContractNumber" SortExpression="RefContractNumber" HeaderText="O/C Reference">
													<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDealer" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox8" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PKNumber" HeaderText="No Reg PK">
													<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNoRegPK" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox9" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerPKNumber" HeaderText="Nomor PK">
													<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNomorPKs" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox10" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Kategori">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblCategory" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox11" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ContractType" HeaderText="Jenis Pesanan">
													<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblOrderType" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox12" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ProductionYear" HeaderText="Tahun Perakitan / Import">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblProductionYear" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox13" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblProjectName" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox14" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="TotalContract" HeaderText="SubTotal Harga Tebus (Rp)" DataFormatString="{0:#,###}">
													<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatusContract" runat="server">
															<img src="../images/lock.gif" border="0" alt="Tidak Dikunci"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnFileName" runat="server" CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
														<asp:Label id="lblString" runat="server" Visible="False"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDetail" runat="server" CommandName="detail">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblFlow" runat="server">
															<img src="../images/alur_flow2.gif" style="cursor:hand" border="0" alt="Alur MO"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<tr>
								<td colspan="6">
									<table width="100%" border="0" cellpadding="2" cellspacing="0">
										<tr valign="top">
											<td valign="top"><asp:Label id="Label17" runat="server" Font-Bold="True">Perhatian :</asp:Label></td>
											<td valign="top">
												<asp:Label id="Label18" runat="server">Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli</asp:Label>
												No.
												<asp:Label id="lblspaNumber" runat="server"></asp:Label>&nbsp;Tanggal
												<asp:Label id="lblspaDate" runat="server"></asp:Label>
											</td>
										</tr>
										<tr>
											<td></td>
											<td>Pembayaran harus dilakukan paling lambat tanggal 25 setiap bulannya.</td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
						<asp:button id="btnDelete" runat="server" Width="60px" Text="Hapus" Visible="False"></asp:button>
						<asp:Button id="btnLock" runat="server" Text="Dikunci" Visible="False"></asp:Button>
						<asp:Button id="btnBatalKunci" runat="server" Text="Batal Kunci" Visible="False"></asp:Button>
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

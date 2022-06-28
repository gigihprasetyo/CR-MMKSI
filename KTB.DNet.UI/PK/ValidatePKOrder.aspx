<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ValidatePKOrder.aspx.vb" Inherits="ValidatePKOrder" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ValidatePKOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		    var strDealaerBranch = '../FinishUnit/../PopUp/PopUpDealerBranchMultipleSelection.aspx';

		    function ShowPPDealerBranchSelection() {

		        var hdnTitle = document.getElementById('hdnTitle');

		        var uri = strDealaerBranch;

		        if (hdnTitle.value == "MKS" || 1==1) {

		            var txtDealerSelection = document.getElementById("txtKodeDealer");
		            if (txtDealerSelection.value != '') {
		                uri = uri + "?DealerCode=" + txtDealerSelection.value;
		            }

		        }

		        showPopUp(uri, '', 500, 760, DealerBranchSelection);
		    }

		    function DealerBranchSelection(selectedDealer) {

		        var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		        txtDealerSelection.value = selectedDealer;

		    }



		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN - Validasi Pesanan Kendaraan</td>
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
							<TBODY>
								<TR>
									<TD class="titleField" width="24%"><asp:label id="Label3" runat="server">Kode Dealer</asp:label></TD>
									<TD width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
									<TD width="25%">
										<asp:textbox id="txtKodeDealer" runat="server"></asp:textbox>
										<asp:label id="lblSearchDealer" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
									<TD class="titleField" width="20%"><asp:label id="Label5" runat="server"> Rencana Penebusan</asp:label></TD>
									<TD width="1%"><asp:label id="Label18" runat="server">:</asp:label></TD>
									<TD width="20%"><asp:dropdownlist id="ddlRencanaPenebusan" runat="server" Width="140px"></asp:dropdownlist></TD>
									<TD width="9%"></TD>
								</TR>
								<TR valign="Top">
									<TD class="titleField">Cabang Dealer</TD>
									<TD>:</TD>
									<TD>
                            <asp:textbox id="txtDealerBranchCode"   Runat="server"></asp:textbox>
									<asp:label id="lblPopUpDealerBranch" runat="server" width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
                                        <asp:HiddenField ID="hdnTitle" runat="server" Value="DEALER" />
                                    </TD>
									<TD class="titleField" style="WIDTH: 249px"><asp:label id="Label15" runat="server"> Kategori</asp:label></TD>
									<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
									<TD><asp:dropdownlist id="ddlCategory" runat="server" Width="140"></asp:dropdownlist></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 15px">Nomor PK</TD>
									<TD style="HEIGHT: 15px"><asp:label id="Label10" runat="server">:</asp:label></TD>
									<TD style="HEIGHT: 15px"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" onblur="alphaNumericPlusSpaceBlur(txtPKNumber)"
											id="txtPKNumber" runat="server" Width="140" MaxLength="40"></asp:textbox></TD>
									<TD class="titleField" style="WIDTH: 249px; HEIGHT: 15px"><asp:label id="Label16" runat="server">Kondisi Pesanan</asp:label></TD>
									<TD style="HEIGHT: 15px"><asp:label id="Label19" runat="server">:</asp:label></TD>
									<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlPurpose" runat="server" Width="140"></asp:dropdownlist></TD>
									<TD style="HEIGHT: 15px"></TD>
								</TR>
								<TR valign="top">
									<TD class="titleField"><asp:label id="Label4" runat="server" Width="96px">Jenis Pesanan</asp:label></TD>
									<TD><asp:label id="Label12" runat="server">:</asp:label></TD>
									<TD>
										<P>
											<asp:dropdownlist id="ddlOrderType" runat="server" Width="140"></asp:dropdownlist></P>
									</TD>
									<TD class="titleField" style="WIDTH: 249px">
										<P>Total Quantity</P>
										<P>
											<asp:Label id="Label7" runat="server">Total Harga</asp:Label></P>
									</TD>
									<TD>
										<P>:</P>
										<P>
											<asp:label id="Label20" runat="server">:</asp:label></P>
									</TD>
									<TD>
										<P>&nbsp;
											<asp:label id="lblQuantity" runat="server" Font-Bold="True"></asp:label></P>
										<P>
											<asp:label id="Label8" runat="server" Font-Bold="True">Rp</asp:label>&nbsp;
											<asp:label id="lblTotal" runat="server" Font-Bold="True"></asp:label><br>
											</P>
									</TD>
									<TD></TD>
								</TR>

                                <TR valign="top">
									<TD class="titleField" style="HEIGHT: 15px"><asp:label id="Label6" runat="server">Status</asp:label></TD>
									<TD style="HEIGHT: 15px">:</TD>
									<TD style="HEIGHT: 15px">
											<asp:listbox id="lboxStatus" runat="server" Width="136px" SelectionMode="Multiple" Height="48px">
												<asp:ListItem Value="0">Baru</asp:ListItem>
												<asp:ListItem Value="1">Batal</asp:ListItem>
												<asp:ListItem Value="2">Validasi</asp:ListItem>
											</asp:listbox></TD>
									<TD class="titleField" style="WIDTH: 249px; HEIGHT: 15px">&nbsp;</TD>
									<TD style="HEIGHT: 15px"></TD>
									<TD style="HEIGHT: 15px"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></TD>
									<TD style="HEIGHT: 15px"></TD>
								</TR>

								<TR>
									<TD colSpan="7">
										<div id="div1" style="HEIGHT: 220px; OVERFLOW: auto"><asp:datagrid id="dgListPK" runat="server" Width="100%" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px"
												BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False" CellSpacing="1" AllowCustomPaging="True" AllowPaging="True" PageSize="25"
												AllowSorting="True">
												<SelectedItemStyle Font-Bold="True" ForeColor="Black" BackColor="PaleGoldenrod"></SelectedItemStyle>
												<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
												<ItemStyle BackColor="White"></ItemStyle>
												<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#F28625"></HeaderStyle>
												<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
												<Columns>
													<asp:TemplateColumn>
														<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<HeaderTemplate>
															<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport',document.all.chkAllItems.checked)"
																type="checkbox">
														</HeaderTemplate>
														<ItemTemplate>
															<asp:CheckBox id="ChkExport" runat="server"></asp:CheckBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn Visible="False" DataField="PKType"></asp:BoundColumn>
													<asp:TemplateColumn Visible="False">
														<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
														<ItemTemplate>
															<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															</asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															</asp:TextBox>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="No">
														<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="PKStatus" HeaderText="Status">
														<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
														<ItemTemplate>
															<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKStatus") %>' Visible="False">
															</asp:Label>
															<asp:Label id="lblStatusString" runat="server"></asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKStatus") %>'>
															</asp:TextBox>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
														<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblDealer" runat="server"></asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox id="TextBox5" runat="server"></asp:TextBox>
														</EditItemTemplate>
													</asp:TemplateColumn>

                                                      <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang Dealer">
                                    <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerBranchCode" runat="server"  >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

													<asp:BoundColumn DataField="PKNumber" SortExpression="PKNumber" HeaderText="No Reg PK">
														<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="PKDate" SortExpression="PKDate" HeaderText="Tanggal PK" DataFormatString="{0:dd/MM/yyyy}">
														<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="DealerPKNumber" SortExpression="DealerPKNumber" HeaderText="Nomor PK">
														<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Kategori">
														<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblCategory" runat="server"></asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="OrderType" HeaderText="Jenis Pesanan">
														<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<ItemTemplate>
															<asp:Label id=lblJenisPesanan runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderType") %>'>
															</asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderType") %>'>
															</asp:TextBox>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="ProductionYear" SortExpression="ProductionYear" HeaderText="Tahun Perakitan/ Import">
														<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="ProjectName" SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
														<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn ReadOnly="True" HeaderText="Sub Total Harga (Rp)">
														<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn>
														<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
														<ItemTemplate>
															<asp:LinkButton id="lbtnEdit" runat="server" CommandName="edit">
																<img src="../images/edit.gif" alt="Ubah"></asp:LinkButton>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn Visible="False" HeaderText="DealerCode"></asp:BoundColumn>
													<asp:TemplateColumn>
														<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
														<ItemTemplate>
															<img src="../images/lock.gif" id="imgUnFreeze" runat="server">
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></div>
									</TD>
								</TR>
								<tr id="RowPerhatian" runat="server">
									<td colspan="7">
										<asp:Label id="Label2" runat="server" Font-Bold="True">Perhatian :</asp:Label>
										<asp:Label id="Label1" runat="server">Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli</asp:Label>
										<asp:Label id="lblspaNumber" runat="server"></asp:Label>&nbsp;Tanggal
										<asp:Label id="lblspaDate" runat="server"></asp:Label>
									</td>
								</tr>
								<TR>
									<TD colSpan="6" valign="bottom">
										<TABLE id="tblOperator" style="WIDTH: 192px; HEIGHT: 46px" cellSpacing="1" cellPadding="1"
											width="192" border="0" runat="server">
											<TR>
												<TD valign="top"><asp:button id="btnValidate" runat="server" Width="60px" Text="Validasi"></asp:button></TD>
												<TD valign="top"><asp:button id="btnDelete" runat="server" Width="80px" Text="Batal Validasi"></asp:button></TD>
												<TD valign="top" style="WIDTH: 50px"><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal"></asp:button></TD>
											</TR>
										</TABLE>
									</TD>
									<TD></TD>
								</TR>
							</TBODY>
						</TABLE>
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

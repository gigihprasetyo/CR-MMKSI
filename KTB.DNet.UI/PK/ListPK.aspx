<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListPK.aspx.vb" Inherits="ListPK" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListPK</title>
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
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PESANAN KENDARAAN - Persetujuan Pesanan Kendaraan Khusus</td>
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
								<TD class="titleField" width="22%"><asp:label id="Label3" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label9" runat="server">:</asp:label></TD>
								<TD width="24%">
									<asp:textbox id="txtKodeDealer" runat="server"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" width="22%"><asp:label id="Label5" runat="server">Rencana Penebusan</asp:label></TD>
								<TD width="1%"><asp:label id="Label18" runat="server">:</asp:label></TD>
								<TD width="24%"><asp:dropdownlist id="ddlRencanaPenebusan" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD width="6%"></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor PK</TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" onblur="alphaNumericPlusSpaceBlur(txtPKNumber)"
										id="txtPKNumber" runat="server" size="22" MaxLength="20"></asp:textbox></TD>
								<TD class="titleField"><asp:label id="Label15" runat="server">Kategori</asp:label></TD>
								<TD><asp:label id="Label19" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlCategory" runat="server" Width="140"></asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label4" runat="server">Jenis Pesanan</asp:label></TD>
								<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlOrderType" runat="server" Width="140"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="Label16" runat="server">Kondisi Pesanan</asp:label></TD>
								<TD><asp:label id="Label20" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlPurpose" runat="server" Width="140" Enabled="False"></asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField"><asp:label id="Label6" runat="server">Status</asp:label></TD>
								<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
								<TD>
									<asp:listbox id="lboxStatus" runat="server" Width="136px" SelectionMode="Multiple" Rows="3">
										<asp:ListItem Value="4">Rilis</asp:ListItem>
										<asp:ListItem Value="6">Setuju</asp:ListItem>
										<asp:ListItem Value="7">Tidak Setuju</asp:ListItem>
										<asp:ListItem Value="8">Blok</asp:ListItem>
									</asp:listbox></TD>
								<TD class="titleField">
									<P>
										<asp:Label id="lblTotalHarga" runat="server">Total Harga</asp:Label></P>
									<P>Total Quantity</P>
								</TD>
								<TD>
									<P>
										<asp:label id="Label1" runat="server">:</asp:label></P>
									<P>:</P>
								</TD>
								<TD>
									<P>&nbsp;
										<asp:label id="lblQuantity" runat="server" Font-Bold="True"></asp:label></P>
									<P>
										<asp:label id="Label2" runat="server" Font-Bold="True">Rp</asp:label>&nbsp;
										<asp:label id="lblTotal" runat="server" Font-Bold="True"></asp:label><br>
										<asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></P>
								</TD>
								<TD valign="bottom"></TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 270px"><asp:datagrid id="dgPKList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" BackColor="#CDCDCD" CellPadding="3" AllowPaging="True" OnPageIndexChanged="dgPKList_PageIndexChanged"
											AllowCustomPaging="True" PageSize="25" ForeColor="White">
											<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
											<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="4px" ForeColor="#FFFFFF" BackColor="#F28625"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.all.chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<HEADERSTYLE Width="5%" CssClass="titleTableSales">
															<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
														</HEADERSTYLE>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<HeaderStyle Width="2%"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PKStatus" HeaderText="Status">
													<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKStatus") %>' Visible="False">
														</asp:Label>
														<asp:Label id="lblStatusString" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKStatus") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDealer" runat="server"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox3" runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="PKNumber" SortExpression="PKNumber" HeaderText="No Reg PK">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PKDate" SortExpression="PKDate" HeaderText="Tanggal PK" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DealerPKNumber" SortExpression="DealerPKNumber" HeaderText="Nomor PK">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn SortExpression="Category.CategoryCode" HeaderText="Kategori">
													<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="OrderType" SortExpression="OrderType" HeaderText="Jenis Pesanan">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ProductionYear" SortExpression="ProductionYear" HeaderText="Tahun Perakitan/ import">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ProjectName" SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn HeaderText="SubTotal Harga (Rp)">
													<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="PKType" HeaderText="PKType"></asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" CommandName="edit">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="RejectedReason" HeaderText="Alasan Tidak Setuju">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:TextBox id="txtRejectedReason" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.RejectedReason")%>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#FFFFCC" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<tr>
								<td colSpan="7">
									<TABLE id="tblOperator" cellSpacing="1" cellPadding="3" border="0" runat="server">
										<TR>
											<TD><asp:button id="btnAgree" runat="server" Width="50px" Text="Setuju"></asp:button></TD>
											<TD><asp:button id="btnDisagree" runat="server" Width="87px" Text="Tidak Setuju"></asp:button></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2"></TD>
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

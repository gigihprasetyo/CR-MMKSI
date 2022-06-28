<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListDealerCreditAccount.aspx.vb" Inherits="FrmListDealerCreditAccount" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListDealerCreditAccount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ADMIN SISTEM&nbsp;- Daftar Credit Account</td>
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
								<TD class="titleField" width="14%">Kode Organisasi</TD>
								<td width="1%">:</td>
								<TD width="40%">
									<asp:textbox id="txtKodeDealer" Width="250px" Runat="server"></asp:textbox>
									<asp:label id="lblPopUpDealer" runat="server" width="10">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
								<TD width="45%" style="DISPLAY:none">
									<asp:checkbox id="cbSalesUnit" runat="server" Text=" Sales Unit" Font-Bold="True"></asp:checkbox>&nbsp;&nbsp; 
									&nbsp;
									<asp:checkbox id="cbService" runat="server" Text=" Service" Font-Bold="True"></asp:checkbox>&nbsp;&nbsp;&nbsp;
									<STRONG>
										<asp:checkbox id="cbSparePart" runat="server" Text=" Spare Part"></asp:checkbox>
									</STRONG>
								</TD>
							</TR>
							<tr>
								<TD class="titleField">Nama Group</TD>
								<td>:</td>
								<td>
									<asp:dropdownlist id="ddlGroup" runat="server" Width="160px"></asp:dropdownlist>
								</td>
								<td></td>
							</tr>
							<tr>
								<TD class="titleField">Credit Account</TD>
								<td>:</td>
								<td>
									<asp:TextBox Runat="server" ID="txtCreditAccount" Text="" Width="160px"></asp:TextBox>
								</td>
								<td></td>
							</tr>
							<TR>
								<TD class="titleField">Status</TD>
								<td width="1%">:</td>
								<TD>
									<asp:dropdownlist id="ddlstatus" runat="server" Width="160px" Visible="True"></asp:dropdownlist>
								</TD>
								<td>
									<asp:button id="btnSearch" runat="server" Text=" Cari " Font-Bold="True"></asp:button>
								</td>
							</TR>
						</TABLE>
						<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+"/"+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="HEIGHT: 380px; OVERFLOW: auto"><asp:datagrid id="dtgDealerList" runat="server" Width="764px" PageSize="25" AllowPaging="False"
								AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
								CellPadding="3" GridLines="Vertical" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Width="20px" Font-Size="Smaller"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" ReadOnly="True" HeaderText="Kode Org">
										<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" ReadOnly="True" HeaderText="Nama Org">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Width="68px" Text='<%# DataBinder.Eval(Container, "DataItem.DealerGroup.GroupName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SearchTerm1" HeaderText="Term Cari 1/2" Visible="False">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Width="52px" Text='<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+" / "+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblStatus runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.Status"),string) = 0, "Tidak Aktif", "Aktif") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesUnitFlag" HeaderText="Sales Unit" Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=cbSalesUnitdtg runat="server" BackColor="Transparent" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SalesUnitFlag") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceFlag" HeaderText="Service" Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbServicesdtg" runat="server" Enabled=false Checked='<%# DataBinder.Eval(Container, "DataItem.ServiceFlag") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparepartFlag" HeaderText="Spare Part" Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbSparePartdtg" runat="server" Enabled=false Checked='<%# DataBinder.Eval(Container, "DataItem.SparepartFlag") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="User Aktif" Visible="False">
										<HeaderStyle Width="6%" CssClass="titleTableGeneral"></HeaderStyle>										
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUserActive" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="LastUpdateTime" SortExpression="LastUpdateTime" ReadOnly="True" HeaderText="Diubah Tgl"
										DataFormatString="{0:dd/MM/yyyy}" Visible="False">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Credit Account" SortExpression="CreditAccount" >
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox Runat="server" ID="txtCreditAccountE" Width="90px" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnSave" runat="server" Width="16px" Text="Save" ToolTip="Simpan Credit Account"
												CommandName="Save">
												<img src="../images/download.gif" border="0">
											</asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="16px" Text="Ubah" ToolTip="Ubah Profile Organisasi"
												CommandName="Edit" style="display:none;">
												<img src="../images/edit.gif" border="0"></asp:LinkButton>
											<asp:LinkButton id="lbtnHakAkses" runat="server" ToolTip="Ubah Hak Akses Organisasi" CommandName="HakAkses"
												CausesValidation="False" style="display:none;">
												<img src="../images/lock.jpg" border="0"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center">
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

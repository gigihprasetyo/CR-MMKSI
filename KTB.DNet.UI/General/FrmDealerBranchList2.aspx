<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDealerBranchList2.aspx.vb" Inherits=".FrmDealerBranchList2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListDealerMantenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function ShowPPDealerSelection() {
		        showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {

		       
		        var txtKodeDealer = document.getElementById("txtKodeDealer");
		        txtKodeDealer.value = selectedDealer;
		    }
		  

		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Organisasi&nbsp;- Daftar Cabang Dealer</td>
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
							<tr>
								<TD class="titleField" width="14%">Kode Organisasi</TD>
								<td width="1%">:</td>
								<TD width="40%"><asp:textbox id="txtKodeDealer" Width="250px" Runat="server"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="10">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" width="14%">Status</TD>
								<td width="1%">:</td>
								<TD><asp:dropdownlist id="ddlstatus" runat="server" Width="160px"></asp:dropdownlist></TD>
							</tr>
							<TR>
								<TD class="titleField" width="14%">Nama Cabang</TD>
								<td width="1%">:</td>
								<TD width="40%"><asp:textbox id="txtDealerName" Width="250px" Runat="server"></asp:textbox></TD>
								<TD class="titleField" width="14%">Area Bisnis</TD>
								<td width="1%">:</td>
								<TD width="45%"><asp:checkbox id="cbSalesUnit" runat="server" Text=" Sales Unit" Font-Bold="True"></asp:checkbox>&nbsp;&nbsp; 
									&nbsp;<asp:checkbox id="cbService" runat="server" Text=" Service" Font-Bold="True"></asp:checkbox>&nbsp;&nbsp;&nbsp;<STRONG><asp:checkbox id="cbSparePart" runat="server" Text=" Spare Part"></asp:checkbox></STRONG></TD>
							</TR>


                            <tr>
								<TD class="titleField" width="14%">Tipe Cabang</TD>
								<td width="1%">:</td>
								<TD width="40%"> <asp:DropDownList runat="server" ID="ddlBranchType" Width="128px"></asp:DropDownList></TD>
								<TD class="titleField" width="14%">Kode Cabang/TO</TD>
                                <td width="1%">:</td>
                                <TD width="45%"> <asp:TextBox runat="server" ID="txtBranchCode"></asp:TextBox></TD>
							</tr>

							<tr>
								<TD class="titleField" width="14%"></TD>
								<td width="1%"></td>
								<TD width="40%"><asp:button id="btnSearch" runat="server" Text=" Cari " Font-Bold="True"></asp:button></TD>
								<td colspan="3"></td>
							</tr>
						</TABLE>
						<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+"/"+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>
					</TD>
				</TR>
				<TR>
					<TD width="100%">
						<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 380px">
							<asp:datagrid id="dtgDealerList" runat="server" Width="100%" PageSize="25" AllowPaging="True"
								AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
								CellSpacing="1">
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

                                    
                                    
                                  <asp:BoundColumn DataField="DealerBranchCode" SortExpression="DealerBranchCode" ReadOnly="True" HeaderText="Kode Cabang">
										<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>


                                    	<asp:BoundColumn DataField="Name" SortExpression="Name" ReadOnly="True" HeaderText="Nama Cabang">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>


									<asp:TemplateColumn SortExpression="Dealer.DealerCode"  HeaderText="Kode Org">
										<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
									  <asp:Label id="lblDealerCode" runat="server"   Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' >
											</asp:Label>

										</ItemTemplate>
									</asp:TemplateColumn>


                                    	<asp:TemplateColumn SortExpression="Dealer.DealerName"  HeaderText="Nama Org">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
									  <asp:Label id="lblDealerName" runat="server"   Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>' >
											</asp:Label>

										</ItemTemplate>
									</asp:TemplateColumn>
                                     



									<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
									  <asp:Label id="lblCity" runat="server" Width="68px" >
											</asp:Label>

										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="MainArea.AreaCode" HeaderText="Region">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
									  <asp:Label id="lblMainArea" runat="server" Width="68px" >
											</asp:Label>

										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn   HeaderText="Term 1/2">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
									  <asp:Label id="lblTerm" runat="server" Width="68px" >
											</asp:Label>

										</ItemTemplate>
									</asp:TemplateColumn>

									 
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblStatus runat="server"  >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesUnitFlag" HeaderText="Sales Unit">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbSalesUnitdtg" runat="server" BackColor="Transparent" Enabled="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SalesUnitFlag") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceFlag" HeaderText="Service">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbServicesdtg" runat="server" Enabled=false Checked='<%# DataBinder.Eval(Container, "DataItem.ServiceFlag") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparepartFlag" HeaderText="Spare Part">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbSparePartdtg" runat="server" Enabled=false Checked='<%# DataBinder.Eval(Container, "DataItem.SparepartFlag") %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								 
									<asp:BoundColumn DataField="LastUpdateTime" SortExpression="LastUpdateTime" ReadOnly="True" HeaderText="Diubah Tgl"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="16px" Text="Ubah" ToolTip="Ubah Profile Organisasi"
												CommandName="Edit">
												<img src="../images/edit.gif" border="0"></asp:LinkButton>

                                            <asp:LinkButton id="lbtnDetail" runat="server" Width="16px" Text="Ubah" ToolTip="Lihat Detail"
												CommandName="View">
												<img src="../images/detail.gif" border="0"></asp:LinkButton>

									 
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
	</body>
</HTML>

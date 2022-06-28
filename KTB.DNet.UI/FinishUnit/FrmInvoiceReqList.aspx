<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmInvoiceReqList.aspx.vb" Inherits="FrmInvoiceReqList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Register TagPrefix="domain" Namespace="KTB.DNet.Domain" Assembly="KTB.DNet.Domain" %>
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
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">


		    function ShowPPDealerBranchSelection() {
		        showPopUp('../FinishUnit/../PopUp/PopUpDealerBranchMultipleSelection.aspx', '', 500, 760, DealerBranchSelection);
		    }

		    function DealerBranchSelection(selectedDealer) {
		        
		            var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		            txtDealerSelection.value = selectedDealer;
		        
		    }


			var SomeChecked = false;
			function MakeValid()
			{
				SomeChecked = true;
			}

			function IsChecked() {
			    if (IsAnyCheckedCheckBox('chkSelect')) {
			        SomeChecked = true;
			        return true;
			    }
			    else {
			        SomeChecked = false;
			        alert("Anda belum memilih faktur");
			        return false;
			    }
			}
						
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">FAKTUR KENDARAAN&nbsp;- Daftar 
						Permohonan Faktur Kendaraan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%"><asp:label id="lblCode" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;/&nbsp;<asp:label id="lblSearch1" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="lblCity" runat="server">Kota Dealer</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="29%"><asp:label id="lblDealerCity" runat="server"></asp:label></TD>
							</TR>

							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%"><asp:label id="lblName" runat="server">Nama Dealer</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>

							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Cabang Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="25%">  <asp:textbox id="txtDealerBranchCode"   Runat="server" TextMode="MultiLine"></asp:textbox>
									<asp:label id="lblPopUpDealerBranch" runat="server" width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="29%"><asp:dropdownlist id="ddlStatus" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"><asp:label id="lblChassisNo" runat="server">Nomor Rangka</asp:label></TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox id="txtChassisNo" runat="server" size="22" MaxLength="20"></asp:textbox>&nbsp;</TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"><asp:CheckBox ID="chkIsTemporary" runat="server" Text=" Temporary Faktur" class="titleField"></asp:CheckBox></TD>
								<TD style="HEIGHT: 20px" width="1%">&nbsp;</TD>
								<TD style="HEIGHT: 20px" width="29%">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblSalesOrg" runat="server"> Kategori</asp:label></TD>
								<TD><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlSalesOrg" runat="server" Width="140"></asp:dropdownlist>&nbsp;</TD>
								<TD class="titleField">
									<asp:label id="Label4" runat="server">Tgl Pembuatan</asp:label></TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px" nowrap><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><asp:CheckBox id="cbDate" runat="server"></asp:CheckBox></td>
											<td><cc1:inticalendar id="icToday" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td align="center"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
                                        <asp:datagrid id="dgInvoiceList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
											AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<ItemTemplate>
														<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
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
												<asp:BoundColumn DataField="FakturStatusDesc" SortExpression="FakturStatus" ReadOnly="True" HeaderText="Status">
													<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="EndCustomer.SPKFaktur.SPKHeader.SPKNumber" HeaderText="No. Reg. SPK">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.SPKFaktur.SPKHeader.SPKNumber") %>' ID="Label5">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Dealer SPK">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.SPKFaktur.SPKHeader.Dealer.DealerCode") %>' ID="Label6">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

                                                <asp:TemplateColumn   HeaderText="Cabang Dealer">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server"   ID="lblDealerBranch">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

												<asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="Nomor Rangka">
													<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>' ID="Label2">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DODateText" SortExpression="DODate" ReadOnly="True" HeaderText="Tgl Cetak DO">
													<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SaveTimeText" SortExpression="EndCustomer.SaveTime" ReadOnly="True" HeaderText="Tgl Pembuatan">
													<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ValidateDateText" SortExpression="EndCustomer.ValidateTime" ReadOnly="True"
													HeaderText="Tgl Validasi">
													<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="EndCustomer.Customer.Name1" HeaderText="Nama Konsumen">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.Customer.Name1") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="EndCustomer.Customer.Alamat" HeaderText="Alamat">
													<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.Customer.Alamat") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="EndCustomer.IsTemporary" HeaderText="Temporary Faktur">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# If(Eval("EndCustomer.IsTemporary") > -1, EnumEndCustomer.TemporaryFakturDesc(Eval("EndCustomer.IsTemporary")), "")%>' ID="lblTemporary">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Detail">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkDetail" runat="server" CommandName="lnkDetail">
															<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Lihat detil">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
						<table cellspacing="0" cellpadding="0" width="100%">
							<tr>
								<td>
									<asp:button id="btnDnLoad" runat="server" Width="72px" Text="Download"></asp:button>
								</td>
								<td>
									<asp:textbox id="txtDownload" runat="server"></asp:textbox>
								</td>
								<td align="right">
									<asp:TextBox id="txtColorRed" style="VISIBILITY: visible" runat="server" Width="32px" BackColor="#ff6666"
										ReadOnly="True" BorderStyle="None"></asp:TextBox>
									Tidak Bisa Diajukan Faktur
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			document.getElementById("txtDownload").style.visibility="hidden";
			
			if (document.getElementById("txtDownload").value != "")
			{
				var downloadURL = document.getElementById("txtDownload").value
				document.getElementById("txtDownload").value = ""
				document.location.href="../DownloadContainer.aspx?"+downloadURL			
			}			
		</script>
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

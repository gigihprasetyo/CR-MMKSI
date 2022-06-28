<%@ Page Language="vb" AutoEventWireup="False" CodeBehind="frmTransactionControlPK.aspx.vb" Inherits=".frmTransactionControlPK" SmartNavigation="False"  %>

<%@ Import Namespace="KTB.DNet.Domain" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTransactionControlPK</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script src="xmlhttprequest.js" type="text/javascript"></script>
		<script language="javascript">
		    function DealerSelection(selectedCode) {
		        var txtDealer = document.getElementById("txtKodeDealer");

		        if (navigator.appName == "Microsoft Internet Explorer") {
		            txtDealer.innerText = selectedCode
		        }
		        else {
		            txtDealer.value = selectedCode
		        }

		        //txtDealer.value = selectedCode;
		        txtDealer.focus();




		    }

		    function CheckAll(aspCheckBoxID, checkVal) {
		        re = new RegExp(':' + aspCheckBoxID + '$')
		        for (i = 0; i < document.forms[0].elements.length; i++) {
		            elm = document.forms[0].elements[i]
		            if (elm.type == 'checkbox') {
		                if (re.test(elm.name)) {
		                    elm.checked = checkVal
		                }
		            }
		        }
		    }
		    function Dummy() {
		    }
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DEALER - Transaction Control</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 80px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="740" border="0">
							<TBODY>
								<TR>
									<TD class="titleField" width="24%">Dealer&nbsp;</TD>
									<td width="1%">:</td>
									<TD width="25%"><asp:textbox id="txtKodeDealer" runat="server" Width="140px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtKodeDealer','<>?\/*%$');"></asp:textbox>&nbsp;<asp:label id="lblPopUpDealer" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
									<TD width="20%"><STRONG>Produk</STRONG></TD>
									<td width="1%">:</td>
									<TD width="29%"><asp:dropdownlist id="ddlProduk" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField">Status</TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlstatus" runat="server" Width="140px"></asp:dropdownlist></TD>
									<TD class="titleField">Category</TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlKategori" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="titleField">Transaksi</TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlTransaksi" runat="server" Width="140px"></asp:dropdownlist></TD>
									<TD class="titleField">Model</TD>
									<TD>:</TD>
									<TD><asp:dropdownlist id="ddlModel" runat="server" Width="140px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD><asp:button id="btnSearch" runat="server" Width="56px" Text=" Cari " Font-Bold="True"></asp:button></TD>
									<TD></TD>
									<TD></TD>
                                    <TD></TD>
                                    <TD></TD>
									<TD></TD>
								</TR>
							</TBODY>
						</TABLE>
						<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")+"/"+DataBinder.Eval(Container, "DataItem.SearchTerm2") %>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 375px"><asp:datagrid id="dtgDealerList" runat="server" Width="100%" PageSize="25" Height="1px" AutoGenerateColumns="False"
								BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" CellPadding="3" GridLines="Horizontal" CellSpacing="1" BackColor="#CDCDCD" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="cbHeader">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" BorderColor="Transparent"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" ReadOnly="True" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" ReadOnly="True" HeaderText="Nama Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
                                    
									<asp:TemplateColumn  SortExpression="TransactionControlPK.VechileModel.Category.Description" HeaderText="Category">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCategory" runat="server" Text='<%# CType(Container.DataItem, Dealer).TransactionControlPK.VechileModel.Category.Description%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="TransactionControlPK.VechileModel.Description" HeaderText="Model">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblModel" runat="server" Text='<%# CType(Container.DataItem, Dealer).TransactionControlPK.VechileModel.Description%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="LastUpdateTime" ReadOnly="True" HeaderText="Tgl. Update"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Tgl. Update">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTglUpdate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Diubah Oleh">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUbahOleh" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Status"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="left"><asp:button id="btnActivate" runat="server" Width="80px" Text="Aktifkan" Font-Bold="True" Enabled="False"></asp:button><asp:button id="btnNoActivate" runat="server" Width="90px" Text="Non-Aktifkan" Font-Bold="True"
							Enabled="False"></asp:button></TD>
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


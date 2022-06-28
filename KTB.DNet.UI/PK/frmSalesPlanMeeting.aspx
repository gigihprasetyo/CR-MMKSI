<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSalesPlanMeeting.aspx.vb" Inherits=".frmSalesPlanMeeting"  smartNavigation="false"%>

<%@ Import Namespace="KTB.DNet.Domain" %>
<%@ Register assembly="KTB.DNet.WebCC" namespace="KTB.DNet.WebCC" tagprefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Stock Ratio</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript">

            function ShowPPDealerSelection() {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var txtDealerSelection = document.getElementById("txtDealerCode");
                txtDealerSelection.value = selectedDealer;
            }
		</script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .auto-style3 {
                width: 24%;
            }
            .auto-style5 {
                width: 12%;
            }
            .auto-style7 {
                width: 23%;
            }
            </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colspan="7">
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td class="titlePage" colSpan="6">PESANAN KENDARAAN - Sales Plan Meeting</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="20%" height="22">Meeting Date</TD>
					<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD colspan="5" width="79%">
                        <cc1:inticalendar id="ccMeetingDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
					</TD>
				</TR>

				<TR>
					<TD width="20%" class="titleField">Description</TD>
					<td width="1%"><asp:label id="Label4" runat="server">:</asp:label></td>
					<TD class="auto-style3" colspan="2"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDescription" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
										runat="server" Width="311px"></asp:textbox></TD>
					
					<td width="15%">Kategori</td>
					<td width="1%">:</td>
					<td width="34%"><asp:dropdownlist id="ddlCategory" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD class="titleField" >
						</TD>
					<TD>
						</TD>
					<TD class="auto-style5">
					<TD class="auto-style7">
						&nbsp;</TD>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</TR>
				
				<TR>
					<td>&nbsp;</td>
					<td></td>
					<td class="auto-style5"><asp:button id="btnSimpan" runat="server" Text="Simpan" Enabled="False"></asp:button></td>
					<td class="auto-style7"><asp:button id="btnBatal" runat="server" Text="Batal" Enabled="False"></asp:button></td>
					<td><asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>
						</td>
					<td></td>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="7">
						<div id="div1" style="HEIGHT: 300px; OVERFLOW: auto"><asp:datagrid id="dtgSalesPlanMeeting" runat="server" Width="100%" CellSpacing="1" OnItemDataBound="dtgSalesPlanMeeting_ItemDataBound"
								GridLines="Horizontal" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								AllowSorting="True" ShowFooter="True" OnItemCommand="dtgSalesPlanMeeting_ItemCommand"  >
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="id">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="DateTime"  HeaderText="Stock Ratio Meeting">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDateTime" runat="server"  Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SalesPlanMeeting).DateTime)%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="Category.Description" HeaderText="Category">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCategory" runat="server" Text='<%# CType(Container.DataItem, SalesPlanMeeting).Category.Description%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="40%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDescription" runat="server" Text='<%# CType(Container.DataItem, SalesPlanMeeting).Description%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEdit" runat="server" Text="" CommandName="Rubah" CausesValidation="false"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="" CommandName="Hapus" CausesValidation="false"></asp:LinkButton>
									</ItemTemplate>
									<EditItemTemplate>
										&nbsp;
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ErrorMessage" ReadOnly="True" HeaderText="Pesan">
									<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>

								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
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
	        <p>
            &nbsp;
						</p>
		</form>
		</body>
</HTML>

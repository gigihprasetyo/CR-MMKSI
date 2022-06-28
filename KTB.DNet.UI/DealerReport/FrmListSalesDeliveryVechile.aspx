<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListSalesDeliveryVechile.aspx.vb" Inherits="FrmListSalesDeliveryVechile" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pengiriman Kendaraan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPopUp()
			{
			}
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam[0];
			}
			
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
						elm.checked = checkVal
						}
					}
				}
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Transaksi&nbsp;- Daftar Pengiriman</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="18%"><asp:label id="lblDealerSearch" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" width="18%"><asp:label id="Label1" runat="server">No Reg D/O</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDORegNo" onblur="omitSomeCharacter('txtDORegNo','<>?*%$')"
										runat="server"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" height="20"><asp:label id="lblTglDeliverySearch" runat="server">Tgl Pengiriman</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr vAlign="top">
											<td><cc1:inticalendar id="icTglDeliverDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td vAlign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
											<td><cc1:inticalendar id="icTglDeliverSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblReffDO" runat="server">No Ref D/O</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDOReffNo" onblur="omitSomeCharacter('txtDOReffNo','<>?*%$')"
										runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="18%"><asp:label id="Label2" runat="server">Tujuan</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:dropdownlist id="ddlTujuan" Width="100px" Runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" width="18%"><asp:label id="lblStatusTitle" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:dropdownlist id="ddlStatus" Width="84px" Runat="server"></asp:dropdownlist>&nbsp;&nbsp;</TD>
							</TR>
							<TR>
								<td></td>
								<td></td>
								<td><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></td>
								<TD align="right" colSpan="3"><asp:label class="titleField" id="lblTotalUnit" Runat="server">Total Unit : </asp:label><asp:label id="lblTotalUnitVal" Runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgListDeliveryVechile" runat="server" Width="100%" DataKeyField="ID" BorderStyle="None"
					AllowPaging="True" PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px"
					BackColor="White" CellPadding="3" GridLines="Horizontal" ForeColor="Gray" CellSpacing="1">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="#F1F6FB"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="titleTableSales" BackColor="#000084"></HeaderStyle>
					<FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
							<HeaderTemplate>
								<INPUT id="chkAllItems" onclick="CheckAll('chkSelection',&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.forms[0].chkAllItems.checked)"
									type="checkbox">
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelection" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="lblNo" Runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblStatus" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Pengirim">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblDealer"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Tujuan">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="lblTujuan" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Nama">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="lblNama" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="RegDONumber" SortExpression="RegDONumber" HeaderText="No.Reg D/O">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="PostingDate" HeaderText="Tgl Kirim">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemTemplate>
								<asp:Label ID="lblTglKirim" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="ReffDONumber" SortExpression="ReffDONumber" HeaderText="No.Ref D/O">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Jumlah Unit">
							<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label ID="lblJmlUnit" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:label id="lblDetail" runat="server" Text="Detail">
									<img alt="Detail  Pengiriman Kendaraan" src="../images/popup.gif" style="cursor:hand"
										border="0"></asp:label>
								<asp:LinkButton id=lnkbtnEdit runat="server" Text="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit" CausesValidation="False">
									<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
								<asp:LinkButton id=lnkbtnView runat="server" Width="20px" Text="Lihat" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="View" CausesValidation="False">
									<img alt="Lihat" src="../images/detail.gif" border="0"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
			<table>
				<tr>
					<td><asp:panel id="pnlChangeStatus" Width="320px" Runat="server" Visible="True">Mengubah 
      Status: 
<asp:dropdownlist id="ddlStatus2" runat="server" Width="140"></asp:dropdownlist>
<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button></asp:panel></td>
					<td><asp:button id="btnDownload" runat="server" Width="60px" Text="Download"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

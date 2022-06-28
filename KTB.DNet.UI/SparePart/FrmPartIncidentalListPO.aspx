<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalListPO.aspx.vb" Inherits="FrmPartIncidentalListPO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPartIncidentalListPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD colSpan="4">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">
									PERMINTAAN KHUSUS &nbsp;- Daftar PO&nbsp;Permintaan Khusus</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="24%"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
					<td width="1%"><asp:label id="Label2" runat="server">:</asp:label></td>
					<td width="40%"><asp:label id="lblDealerCodeValue" runat="server"></asp:label><asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
							runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
					<TD width="35%"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 17px"><asp:label id="lblStatusEmail" runat="server">Tanggal Proses</asp:label></TD>
					<td style="HEIGHT: 17px"><asp:label id="Label5" runat="server">:</asp:label></td>
					<TD style="HEIGHT: 17px">
						<TABLE id="Table2" cellSpacing="0" cellPadding="1" border="0">
							<TR>
								<TD><asp:checkbox id="chkProcessDate" runat="server" Checked="True"></asp:checkbox></TD>
								<TD><cc1:inticalendar id="icProcDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								<TD><asp:label id="Label9" runat="server">s/d</asp:label></TD>
								<td><cc1:inticalendar id="icProcDateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 17px"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblTanggalInput" runat="server">Tanggal Pesanan</asp:label></TD>
					<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD>
						<TABLE id="Table4" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:checkbox id="chkPKDate" runat="server"></asp:checkbox></TD>
								<TD><cc1:inticalendar id="icPKDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label4" runat="server">Nomor Barang</asp:label></TD>
					<td><asp:label id="Label3" runat="server">:</asp:label></td>
					<TD><asp:textbox id="txtPartNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtPartNumber','<>?*%$;')"
							runat="server"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label10" runat="server" Visible="False">Status</asp:label></TD>
					<TD><asp:label id="Label12" runat="server" Visible="False">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlStatus" runat="server" Width="120px" Visible="False"></asp:dropdownlist></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label11" runat="server">Nomor PO</asp:label></TD>
					<TD><asp:label id="Label13" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtPONumber" runat="server"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"></TD>
					<TD></TD>
					<TD><asp:button id="btnSearch" runat="server" Width="50px" Text="Cari"></asp:button></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dgPartIncidentalDetail" runat="server" Width="100%" BackColor="#CDCDCD" CellPadding="3"
								BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD" AllowPaging="True" PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label1"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="Textbox1"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.RequestNumber" HeaderText="No Permintaan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label7"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="Textbox2"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Nomor PO">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.IncidentalDate" HeaderText="Tgl Pesanan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label8"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="Textbox3"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Tanggal Proses">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Quantity" SortExpression="Quantity" HeaderText="Qty Order">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Part">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Part">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetail" runat="server" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

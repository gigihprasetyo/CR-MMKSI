<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalPO.aspx.vb" Inherits="FrmPartIncidentalPO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListPartIncidentalKTB</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
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
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD width="24%" colSpan="6">
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">PERMINTAAN KHUSUS - Alokasi Pemesanan Khusus</TD>
							</TR>
							<TR>
								<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 95px; HEIGHT: 26px"><asp:label id="Label2" runat="server" Width="80px">Kode Dealer</asp:label></TD>
					<TD style="HEIGHT: 26px"><asp:label id="Label4" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 300px; HEIGHT: 26px" colSpan="4"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
							runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 95px">PK&nbsp;Date</TD>
					<TD>:</TD>
					<TD style="WIDTH: 404px">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td><asp:checkbox id="cbPKDate" runat="server"></asp:checkbox></td>
								<td><cc1:inticalendar id="intPKDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 95px; HEIGHT: 26px">Request Number</TD>
					<TD style="HEIGHT: 26px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 26px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtReqNumber" onblur="omitSomeCharacter('txtReqNumber','<>?*%$;')"
							runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 95px; HEIGHT: 24px">Part Number</TD>
					<TD style="HEIGHT: 24px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 24px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtPartNumber" onblur="omitSomeCharacter('txtPartNumber','<>?*%$;')"
							runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 95px; HEIGHT: 26px">Plan Date</TD>
					<TD style="HEIGHT: 26px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 26px">
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td><asp:checkbox id="cbPlanDate" runat="server" Checked="True"></asp:checkbox></td>
								<td><cc1:inticalendar id="intFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td><asp:label id="Label1" runat="server">s/d</asp:label></td>
								<td><cc1:inticalendar id="intTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<td style="WIDTH: 95px" height="24"><IMG height="1" src="../images/dot.gif" border="0"></td>
					<TD height="24"></TD>
					<TD style="WIDTH: 404px" height="24"><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button></TD>
				</tr>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dgPartList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BackColor="#CDCDCD" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pilih">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.RequestNumber" HeaderText="Req-No">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Right" Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblReqNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.DealerMailNumber" HeaderText="Nomor Surat">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerMailNumber" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.IncidentalDate" HeaderText="PK Date">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPKDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PlanDate" HeaderText="Plan Date">
										<HeaderStyle Width="6%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPlanDate" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Part No">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Part Name">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model Code">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblModelCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<%----<asp:TemplateColumn SortExpression="SparePartMaster.SupplierCode" HeaderText="Supplier Code">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSupplierCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>----%>
									<asp:TemplateColumn Visible="False" HeaderText="Part Sub No">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Part Sub Name">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StatusDetail" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatusDetail" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Quantity" HeaderText="Org Quantity">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOrgQty" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Remain Quantity">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRemainQty" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtAlokasi" runat="server" Width="40px" CssClass="textRight"></asp:TextBox>
											<asp:RangeValidator id="RangeValidator1" runat="server" Type="Integer" ErrorMessage="*" ControlToValidate="txtAlokasi"
												MaximumValue="10000000" MinimumValue="0">*</asp:RangeValidator>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtAlokasi"></asp:RequiredFieldValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2">
							<TR>
								<TD colSpan="2">
									<P>Keterangan Status</P>
								</TD>
							</TR>
							<TR>
								<TD><IMG src="../images/green.gif" border="0"></TD>
								<TD>Aktif</TD>
							</TR>
							<TR>
								<TD><IMG src="../images/yellow.gif" border="0"></TD>
								<TD>Batal Sebagian</TD>
							</TR>
							<%----<TR>
								<TD><IMG src="../images/yellow.gif" border="0"></TD>
								<TD>Sisa Quantity Dibatalkan</TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>---%>
						</TABLE>
					</TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<tr>
					<td><asp:button id="btnSave" runat="server" Text=" Simpan  "></asp:button><asp:button id="btnDownload" runat="server" Text="Download"></asp:button><asp:button id="btnCancel" runat="server" Text="Batal"></asp:button></td>
					<td></td>
					<td><asp:label id="Label5" runat="server">Warna Kuning : Belum dicetak, Warna Abu-abu : Barang Pengganti</asp:label></td>
				</tr>
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

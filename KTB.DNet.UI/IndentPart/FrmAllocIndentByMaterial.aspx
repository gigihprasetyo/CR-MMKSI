<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAllocIndentByMaterial.aspx.vb" Inherits="FrmAllocIndentByMaterial" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAllocIndentByOrder</title>
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
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealerCode");
			var txtDealerName = document.getElementById("txtDealerName");
			txtDealer.value = tempParam[0];
			txtDealerName.value = tempParam[1];				
		}
		function ShowPPSparePartSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpSparePart.aspx?bb=kk','',500,760,SparePartSelection);
		}
		function SparePartSelection(selected)
		{
			var tempParam = selected.split(';');
			var txtSparePart = document.getElementById("txtSparePartNo");
			txtSparePart.value = tempParam[0];				
		}
		function ShowPPNomorPengajuanSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpIndentPart.aspx','',500,760,NomorPOSelection);
		}
		function NomorPOSelection(selected)
		{
			//var tempParam = selected.split(';');
			var txtSparePartNo = document.getElementById("txtNoPO");
			txtSparePartNo.value = selected.substring(0,selected.length-1);				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="6">INDENT PART - Alokasi Indent Part (Per Material)</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD class="titleField" width="15%">Kode Dealer</TD>
					<TD width="1%">:</TD>
					<td width="50%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtNoBarang','<>?*%$')"
							runat="server" Width="136px" MaxLength="15"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" width="15%">Nama Dealer</TD>
					<TD width="1%">:</TD>
					<td width="50%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerName" onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"
							runat="server" Width="264px" MaxLength="15" ReadOnly="True"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 26px" vAlign="top" width="15%">Tanggal 
						Pengajuan</TD>
					<TD style="HEIGHT: 26px" vAlign="top" width="1%">:</TD>
					<td style="HEIGHT: 26px" width="50%">
						<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icPODateFrom" runat="server"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icPODateUntil" runat="server"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<TD class="titleField" width="15%">Nomor Pengajuan</TD>
					<TD width="1%">:</TD>
					<td width="50%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoPO" onblur="omitSomeCharacter('txtNoPO','<>?*%$;')"
							runat="server" Width="160px" MaxLength="15"></asp:textbox><asp:label id="lblPopUpPengajuan" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" width="15%">Nomor Barang</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="50%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSparePartNo" onblur="omitSomeCharacter('txtSparePartNo','<>?*%$;')"
							runat="server" Width="264px" MaxLength="15"></asp:textbox><asp:label id="lblSearchProduk" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" width="15%"></TD>
					<TD width="1%"></TD>
					<td width="50%"><asp:button id="btnSearch" runat="server" width="60px" Text="Cari"></asp:button></td>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px" DESIGNTIMEDRAGDROP="61"><asp:datagrid id="dtgIndentPart" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
								AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:CheckBox ID="chkPO" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="IndentPartHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.IndentPartHeader.Dealer.DealerCode") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblPartName" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>' >Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblModel" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>' >Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Remain Qty">
										<HeaderStyle Width="14%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblRemainQty" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.SisaQty") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText=" Qty">
										<HeaderStyle Width="14%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblOrderQty" Runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="AllocationQty" HeaderText="Alokasi Qty">
										<HeaderStyle Width="14%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="txtQty" Runat="server" CssClass="textRight" onkeypress="return NumericOnlyWith(event,'')" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.AllocationQty") %>' />
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="txtQtyAllocation" Runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.AllocationQty") %>' onkeypress="omitCharsOnCompsTxt(this,'<>?*%$;')">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="IndentPartHeader.RequestNo" HeaderText="Nomor Pengajuan">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.IndentPartHeader.RequestNo") %>' >Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="IndentPartHeader.RequestDate" HeaderText="Tanggal Pengajuan">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="Label4" Text='<%# format(DataBinder.Eval(Container, "DataItem.IndentPartHeader.RequestDate,"dd/MM/yyyy")") %>' >Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id=lbtnEdit runat="server" Text="Ubah" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:LinkButton id=lbtnSave tabIndex=49 CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Save" Runat="server" text="Simpan">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="Cancel" Runat="server" text="Batal">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<td colSpan="3"></td>
				</TR>
				<tr>
					<td colSpan="3"><asp:button id="btnSave" tabIndex="50" runat="server" Text="Simpan" CausesValidation="False"></asp:button><asp:button id="btnGeneratePO" runat="server" Text="Generate PO"></asp:button><asp:button id="btnCancel" tabIndex="70" runat="server" Width="48px" Text="Batal" CausesValidation="False"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

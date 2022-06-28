<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListIndentPart.aspx.vb" Inherits="FrmListIndentPart" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
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
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			//var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;				
		}
		
		
		function PONOSelection(POSelection)
		{
		    
			//alert(POSelection);
			//var tempParam = selectedDealer.split(';');
			var txtNoPO= document.getElementById("txtNoPO");
			txtNoPO.value = POSelection;				
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
			<TABLE id="Table11" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6">
						<TABLE id="Table01" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="6">INDENT PART - Daftar Status Indent Part</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6"><IMG height="10" src="../images/blank.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" vAlign="top" width="15%">Kode Dealer</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td vAlign="top" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
					<td class="titleField" vAlign="top" width="15%">Tanggal Pengajuan</td>
					<td vAlign="top" width="1%">:</td>
					<td width="50%">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<TD class="titleField" width="15%">Nomor Pengajuan</TD>
					<TD width="1%">:</TD>
					<td width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtNoPO" onblur="omitSomeCharacter('txtNoPO','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblPopUpPONo" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
					<!-- remark by Ery <td class="titleField" width="15%">Tipe Barang</td>
					<td width="1%">:</td>
					<td width="50%"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="160px"></asp:dropdownlist></td> -->
					<td class="titleField" width="15%">Tipe Barang</td>
					<td width="1%">:</td>
					<td width="50%"><asp:dropdownlist id="ddlMaterialType1" runat="server" Width="160px"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" width="15%">Status</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="25%"><asp:listbox id="lstStatus" runat="server" Width="160px" SelectionMode="Multiple"></asp:listbox></td>
					<td vAlign="top"><STRONG>Keterangan</STRONG></td>
					<td vAlign="top" rowSpan="2">:&nbsp;
					</td>
					<td rowSpan="2"><asp:dropdownlist id="DdlDesc" runat="server" Width="160px"></asp:dropdownlist><br>
						<br>
						<table cellPadding="0" width="100%" border="0">
							<tr>
								<td colSpan="2"><b>Keterangan :</b></td>
							</tr>
							<tr>
								<td vAlign="top" width="1%"><IMG alt="Merah" src="../images/red.gif" border="0">
								</td>
								<td vAlign="top" width="99%"><i>: MMKSI belum mengalokasikan pesanan </i>
								</td>
							</tr>
							<tr>
								<td vAlign="top"><IMG alt="Kuning" src="../images/yellow.gif" border="0"></td>
								<td vAlign="top"><i>: MMKSI baru memenuhi sebagian alokasi pesanan </i>
								</td>
							</tr>
							<tr>
								<td vAlign="top"><IMG alt="Hijau" src="../images/green.gif" border="0">
								</td>
								<td vAlign="top"><i>: MMKSI sudah mengalokasikan semua pesanan</i>
								</td>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<TD class="titleField" width="15%">Cara Pembayaran</TD>
					<td width="1%">:</td>
					<td width="50%">
                         <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
					</td>
					<TD class="titleField" vAlign="top" width="15%"></TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" width="15%"></TD>
					<TD vAlign="top" width="1%"></TD>
					<TD vAlign="top" width="1%"></TD>
					<TD width="25%"><asp:button id="btnSearch" runat="server" Text="Cari" width="60px"></asp:button></TD>
					<TD class="titleField" vAlign="top" width="15%"><TD><asp:Label id="lblGrandTotal" runat="server"></asp:Label></TD></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgIndentPart" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
								CellPadding="3" GridLines="None" PageSize="50"  >
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Indikator">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Image id="imgIndikator" runat="server"></asp:Image>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="14%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblStatus" text='<%# Databinder.Eval(Container, "DataItem.StatusDealerDesc")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RequestNo" SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RequestDate" SortExpression="RequestDate" HeaderText="Tanggal Pengajuan"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Total Qty">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblQty" text='<%# Databinder.Eval(Container, "DataItem.TotalQuantity")%>' CssClass="textRight">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Qty">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSisaQty" Text='<%# DataBinder.Eval(Container, "DataItem.SisaQty") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan" SortExpression="DescID">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label3" Text='<%# CType(DataBinder.Eval(Container, "DataItem.DescID"),KTB.DNet.Domain.EnumIndentDesc.IndentDesc).ToString().Replace("Silakan_Pilih","").Replace("_or_","/") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TermOfPayment.ID" HeaderText="Cara Pembayaran">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<%--<asp:Label id="Label1" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).TermOfPayment.Description%>'>Label</asp:Label>--%>
											<asp:Label id="LabelTOP" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.TermOfPayment.Description"), String) = "", "", CType(DataBinder.Eval(Container, "DataItem.TermOfPayment.Description"), String))%>'>Label</asp:Label>
                                            
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetails" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Lihat" Runat="server">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<td colSpan="6">
						<table cellPadding="0" border="0">
							<tr>
								<TD class="titleField" width="20%">Mengubah Status</TD>
								<TD width="1%">:</TD>
								<td><asp:dropdownlist id="ddlupdatestatus" runat="server" Width="152px"></asp:dropdownlist><asp:button id="btnProses" runat="server" Text="Proses" width="60px"></asp:button><asp:button id="btnDownload" runat="server" Text="Download Pesanan" width="168px" Visible="False"></asp:button><asp:button id="btnSubmit" runat="server" Width="133px" Text="Submit To MMKSI" Visible="False"></asp:button></td>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

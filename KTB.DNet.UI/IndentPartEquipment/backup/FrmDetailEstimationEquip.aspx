<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDetailEstimationEquip.aspx.vb" Inherits="FrmDetailEstimationEquip" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDetailEstimationEquip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">

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
					<td class="titlePage"><asp:label id="lblTitle" Text="INDENT PART EQUIPMENT - Detail Estimasi Indent Part Equipment"
							Runat="server"></asp:label></td>
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
								<TD class="titleField" width="30%"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD width="70%"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblNamaDealer" runat="server">Nama Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><asp:label id="lblNomorTanggalPO" runat="server">Nomor / Tanggal Pengajuan</asp:label></TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="70%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:textbox id="txtPONumber" runat="server" BackColor="#efefef" ReadOnly="True" size="22">[Dibuat oleh sistem]</asp:textbox></td>
											<td><cc1:inticalendar id="icOrderDate" runat="server" Enabled="False"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%"><STRONG>Total Amount</STRONG></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblTotalAmount" runat="server">Rp. 0</asp:label></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="30%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:label id="lblStatusValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" vAlign="top" width="30%">Nomor Barang</TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:textbox id="txtPartNumber" runat="server" Height="56px" TextMode="MultiLine" Width="280px"></asp:textbox><asp:button id="btnCari" runat="server" Text="Cari" Width="80px"></asp:button></TD>
							</TR>
							<TR>
								<TD colSpan="6">
									<div id="divGrid" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 280px" DESIGNTIMEDRAGDROP="245">
										<table width="100%">
											<tr>
												<td><asp:datagrid id="dtgIPDetail" runat="server" BackColor="#CDCDCD" Width="100%" AutoGenerateColumns="False"
														AllowSorting="True" CellSpacing="0" CellPadding="3" BorderColor="Black" BorderWidth="1px">
														<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
														<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
														<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
														<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
														<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
														<Columns>
															<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
															<asp:TemplateColumn>
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<HeaderTemplate>
																	<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
															                        document.forms[0].chkAllItems.checked)" />
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn ReadOnly="True" HeaderText="No">
																<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
																<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
																<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id=lblPartname runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.AltPartName" HeaderText="No Barang Alt">
																<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblAltPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.AltPartName") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model/Tipe/Warna">
																<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblmodel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn HeaderText="EstimationUnit" HeaderStyle-CssClass="titleTableParts" SortExpression="EstimationUnit"
																DataField="EstimationUnit" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
															<asp:BoundColumn HeaderText="Harga" HeaderStyle-CssClass="titleTableParts" SortExpression="Harga"
																DataField="Harga" DataFormatString="{0:#,##}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Jumlah">
																<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblJumlah"></asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Tanggal Konfirmasi">
																<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label Runat="server" ID="lblConfirmedDate"></asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Remark" HeaderText="Keterangan">
																<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
																<ItemTemplate>
																	<asp:TextBox id="txtRemark" runat="server" style="width:70px;" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'>
																	</asp:TextBox>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:datagrid></td>
											</tr>
										</table>
									</div>
									<div style="OVERFLOW: auto"><asp:button class="hideButtonOnPrint" id="btnConfirm" runat="server" Text="Konfirmasi" Enabled="False"
											Visible="false"></asp:button><asp:button class="hideButtonOnPrint" id="btnUpdatePrice" runat="server" Text="Update Harga"
											Visible="False"></asp:button><asp:button class="hideButtonOnPrint" id="btnSimpanRemark" runat="server" Text="Simpan Keterangan"></asp:button><asp:button class="hideButtonOnPrint" id="btnBack" runat="server" Text="Kembali"></asp:button></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

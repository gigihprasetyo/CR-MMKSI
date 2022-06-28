<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryPOAllocation.aspx.vb" Inherits="FrmEntryPOAllocation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Form Alokasi PO</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		//function Back()
		//{
		//window.history.go(-1)
		//}
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
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">PO HARIAN -&nbsp; Alokasi PO</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblKategoriValue" runat="server"></asp:label></TD>
								<TD class="titleField" width="20%"><asp:label id="lblPermintaanKirim" runat="server">Permintaan Kirim</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:label id="lblPermintaanKirimAwal" runat="server"></asp:label>&nbsp;
									<asp:label id="Label1" runat="server">s/d</asp:label>&nbsp;
									<asp:label id="lblPermintaanKirimAkhir" runat="server" DESIGNTIMEDRAGDROP="64"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTipe" runat="server">Tipe</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTipeValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTahunPerakitan" runat="server">Tahun Perakitan/Impor</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTahunPerakitanValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblTipeWarna" runat="server">Tipe / Warna</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTipeWarnaValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblJenisOrder" runat="server">Jenis Order</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblJenisOrderValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblMaterialDescription" runat="server">Model / Tipe / Warna</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblMaterialDescriptionValue" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="lblTotalUnit" runat="server">Stok ATP (unit)</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblTotalUnitValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD class="titleField" style="HEIGHT: 17px"><asp:label id="lblSisaUnit" runat="server">Sisa ATP Setelah Alokasi</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="HEIGHT: 17px"><asp:label id="lblSisaUnitValue" runat="server"></asp:label></TD>
							</TR>
							<TR style="VISIBILITY:visible">
								<TD style="HEIGHT: 20px" class="titleField">Formula A(Sisa Ceiling)</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px"><asp:label id="lblA" runat="server" Width="216px"></asp:label></TD>
								<TD style="HEIGHT: 20px" class="titleField">FormulaB(Diajukan)</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px"><asp:label id="lblB" runat="server" Width="216px"></asp:label></TD>
							</TR>
							<TR style="VISIBILITY:visible">
								<TD class="titleField">Formula C(Liquefy)</TD>
								<TD>:</TD>
								<TD><asp:label id="lblC" runat="server" Width="216px"></asp:label></TD>
								<TD class="titleField">Formula D(Acc Gyro)</TD>
								<TD>:</TD>
								<TD><asp:label id="lblD" runat="server" Width="216px"></asp:label></TD>
							</TR>
							<TR style="VISIBILITY:visible">
								<TD class="titleField">Total Available Ceiling</TD>
								<TD>:</TD>
								<TD><asp:label id="lblAvCeiling" runat="server" Width="216px"></asp:label></TD>
								<TD class="titleField">Total This PO</TD>
								<TD>:</TD>
								<TD><asp:label id="lblTotalPO" runat="server" Width="216px"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div style="OVERFLOW: auto; HEIGHT: 380px" id="div1"><asp:datagrid id="dtgEntryPOAllocation" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
								CellSpacing="1" BorderColor="#CDCDCD" BackColor="#CDCDCD" OnItemDataBound="dtgEntryPOAllocation_ItemDataBound" AutoGenerateColumns="False"
								AllowSorting="True" OnItemCommand="dtgEntryPOAllocation_ItemCommand">
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAll('chkItemChecked',&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.forms[0].chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:TextBox id="txtID" Runat="server" Width="0px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
											<asp:TextBox ID="txtBlockedStatus" Runat="server" Width="0px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.POHeader.BlockedStatus") %>'>
											</asp:TextBox>
											<asp:CheckBox id="chkItemChecked" runat="server" Height="5px"></asp:CheckBox>
											<asp:CheckBox ID="chkIsConverted" Runat="server" Height="5px" style="visibility:hidden;"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="POHeader.PONumber" HeaderText="No Reg PO">
										<HeaderStyle Width="18%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnNoRegPO" runat="server" CommandName="PODetail"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" HeaderText="Nama Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="POHeader.ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="18%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="POHeader.ReqAllocationDateTime" HeaderText="Tgl Permintaan Kirim">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="lblReqAllocationDateTime" Runat="server" text='<%# format(DataBinder.Eval(Container, "DataItem.POHeader.ReqAllocationDateTime"),"dd/MM/yyyy") %>'>
											</asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="CreatedTime" HeaderText="Tgl Pengajuan">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="ContractDetail.ContractHeader.ProjectName" HeaderText="Nama Pesanan Khusus">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Usulan Alokasi (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Order (unit)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Jawaban MKSI">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Alokasi (unit)">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtAllocation" onkeypress="return numericOnlyUniv(event)" runat="server" Width="50px"
												CssClass="textRight"></asp:TextBox>
											<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Alokasi Melebihi Permintaan" Type="Integer"
												MinimumValue="0" MaximumValue="10000" ControlToValidate="txtAllocation">*</asp:RangeValidator>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Alokasi Unit Tidak boleh kosong"
												ControlToValidate="txtAllocation">*</asp:RequiredFieldValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Sisa O/C Akhir (unit)">
										<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>

                                    <asp:TemplateColumn HeaderText="Is MDP">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsMDP" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Free Days" SortExpression="FreeDays">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreeDays" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Max TOP Day" SortExpression="MaxTOPDay">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxTOPDay" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
								</Columns>
							</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></div>
					</TD>
				</TR>
				<TR>
					<TD height="40">&nbsp;<asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnHitung" runat="server" Text="Hitung"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali"></asp:button><asp:button id="btnAlokasi" runat="server" Text="Propose Alokasi"></asp:button><asp:button id="btnKonversi" runat="server" Width="104px" Text="Konversi"></asp:button><INPUT style="WIDTH: 8px; HEIGHT: 20px" id="hdnValConfirm" value="0" size="1" type="hidden"
							name="hdnValConfirm" runat="server">
						<asp:TextBox style="VISIBILITY: hidden" id="txtIsSaving" runat="server" Width="0px" text="0"></asp:TextBox></TD>
				</TR>
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

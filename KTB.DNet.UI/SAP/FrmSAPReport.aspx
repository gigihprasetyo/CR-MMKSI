<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSAPReport.aspx.vb" Inherits="FrmSAPReport" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSAPReport</title>
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
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var splitted = selectedDealer.split(';');
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				txtDealerCodeSelection.value =splitted[0];
			}
		
			function ShowSAPSelection()
			{
				showPopUp('../PopUp/PopUpSAP.aspx','',500,760,SAPSelection);
			}
			function SAPSelection(selectedSAP)
			{
				var txtSAPNo = document.getElementById("txtSAPNo");
				//var txtPeriod = document.getElementById("txtPeriod");
				var lblStartDate = document.getElementById("lblStartPeriod");
				var lblEndDate = document.getElementById("lblEndPeriod");
				var hdnField = document.getElementById("hdnFieldTemp");
				var arrValue = selectedSAP.split(';');
				txtSAPNo.value = arrValue[0];
				lblStartDate.innerHTML = arrValue[1];
				lblEndDate.innerHTML = arrValue[2];
				hdnField.value = arrValue[1] +";" + arrValue[2];
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="6">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">SAP - Rekap Data</td>
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
					<TD class="titleField" width="10%">Kode Dealer</TD>
					<td class="titleField" width="1%">:</td>
					<TD width="20%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
							runat="server" size="22"></asp:textbox><asp:label id="lblDealerCode" runat="server" width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
					<td class="titleField" width="10%">SAP No</td>
					<td class="titleField" width="1%">:</td>
					<td width="20%"><asp:textbox id="txtSAPNo" runat="server" Width="120px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtSAPNo','<>?*%$;')"></asp:textbox><asp:label id="lblSearchSAP" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" width="10%">Kategori</TD>
					<td class="titleField" width="1%">:</td>
					<TD><asp:dropdownlist id="ddlcategori" Runat="server"></asp:dropdownlist></TD>
					<TD class="titleField" width="10%">Periode SAP</TD>
					<td class="titleField" width="1%">:</td>
					<td width="20%">
						<table>
							<tr>
								<td><asp:label id="lblStartPeriod" runat="server"></asp:label></td>
								<td>s/d</td>
								<td><asp:label id="lblEndPeriod" runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</TR>
				<TR>
					<TD class="titleField" width="10%">Pilihan Peringkat</TD>
					<td class="titleField" width="1%">:</td>
					<TD width="20%">
						<asp:dropdownlist id="ddlPeringkat" Runat="server"></asp:dropdownlist></TD>
					<td class="titleField" width="10%">Area</td>
					<td class="titleField" width="1%">:</td>
					<td width="20%">
						<asp:dropdownlist id="ddlArea" runat="server" Width="112px"></asp:dropdownlist></td>
				</TR>
				<TR>
					<TD colSpan="2"></TD>
					<TD><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button>&nbsp;
						<asp:button id="btnReCalculate" runat="server" Width="96px" Text="Hitung Ulang" Enabled="False"></asp:button>&nbsp;
						<asp:Button id="btnDownload" runat="server" Text="Download" Enabled="False"></asp:Button></TD>
					<TD colSpan="3"></TD>
				</TR>
				<TR>
				<TR>
					<TD colSpan="6">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgSAPReport" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="NO">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridSalesCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridSalesName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kategori" SortExpression="SalesmanHeader.JobPosition.Description">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridCategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="RPT Prospek" SortExpression="RptProsPek">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptProsPek") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="RPT Hot Prospek" SortExpression="RptHotProspek">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptHotProsPek") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Faktur" SortExpression="RptFaktur">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptFaktur") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PDI" SortExpression="RptPDI">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptPDI") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Test Tulis" SortExpression="WritingTestScore">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WritingTestScore") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Presentasi Produk SWAP &amp; PKT" SortExpression="GradeSWAP">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GradeSWAP") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Rata2 Salesman/SC" SortExpression="RptAvgScoreSubOrdinate">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptAvgScoreSubOrdinate") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Efektivitas Training" SortExpression="RptEffectivity">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptEffectivity") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pencapaian Penjualan Tim" SortExpression="RptAchievement">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptAchievement") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Presentasi Produk" SortExpression="GradePresentasi">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GradePresentasi") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Konsistensi Peserta" SortExpression="GradeKonsistensi">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GradeKonsistensi") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Komposisi Sales Force" SortExpression="RptKomposisi">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptKomposisi") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jumlah Pemenang SAP" SortExpression="RptWinnerAmount">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RptWinnerAmount") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="kelengkapan dan Validitas Data" SortExpression="GradeKelengkapan">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GradeKelengkapan") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Final" SortExpression="GradeFinal">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label15" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GradeFinal") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hdnFieldTemp" style="Z-INDEX: 101; LEFT: 120px; POSITION: absolute; TOP: 712px"
				type="hidden" name="Hidden1" runat="server">
		</form>
	</body>
</HTML>

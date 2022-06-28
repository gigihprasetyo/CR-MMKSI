<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSAPInputDataResult.aspx.vb" Inherits="FrmSAPInputDataResult" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSAPInputDataResult</title>
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
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,725,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var splitted = selectedDealer.split(';');
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				txtDealerCodeSelection.value =splitted[0];
			}
		
			function ShowSAPSelection()
			{
				showPopUp('../PopUp/PopUpSAP.aspx','',600,600,SAPSelection);
			}
			function SAPSelection(selectedSAP)
			{
				var txtSAPNo = document.getElementById("txtSAPNo");
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
		<DIV style="LEFT: 1px; WIDTH: 100%; POSITION: absolute; TOP: 8px; HEIGHT: 10px" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="titlePage">SAP - Input Data Result</td>
					</tr>
					<tr>
						<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
					</tr>
					<tr>
						<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
				</TABLE>
				<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
					<TR>
						<TD class="titleField" style="HEIGHT: 17px">SAP No</TD>
						<TD style="HEIGHT: 17px">:</TD>
						<TD style="HEIGHT: 17px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSAPNo" onblur="omitSomeCharacter('txtSAPNo','<>?*%$;')"
								runat="server" size="22"></asp:textbox><asp:label id="lblSAPNo" runat="server" width="16px">
								<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
						<TD class="titleField" style="HEIGHT: 17px">Periode SAP</TD>
						<TD style="HEIGHT: 17px">:</TD>
						<TD style="HEIGHT: 17px">
							<table>
								<tr>
									<td><asp:label id="lblStartPeriod" runat="server"></asp:label></td>
									<td>s/d</td>
									<td><asp:label id="lblEndPeriod" runat="server"></asp:label></td>
								</tr>
							</table>
						</TD>
					</TR>
					<tr>
						<TD class="titleField" style="HEIGHT: 17px" nowrap>Kode Dealer</TD>
						<TD style="HEIGHT: 17px">:</TD>
						<TD style="HEIGHT: 17px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerCode" onblur="omitSomeCharacter('txtSAPNo','<>?*%$;')"
								runat="server" size="22"></asp:textbox><asp:label id="lblDealerCode" runat="server" width="16px">
								<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
						<TD class="titleField" style="HEIGHT: 17px">Kategori</TD>
						<TD style="HEIGHT: 17px">:</TD>
						<TD style="HEIGHT: 17px"><asp:dropdownlist id="ddlKategori" runat="server"></asp:dropdownlist></TD>
					</tr>
					<TR>
						<TD class="titleField" style="HEIGHT: 17px"></TD>
						<TD style="HEIGHT: 17px"></TD>
						<TD style="HEIGHT: 17px"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></TD>
						<TD class="titleField" style="HEIGHT: 17px">Area</TD>
						<TD style="HEIGHT: 17px">:</TD>
						<TD style="HEIGHT: 17px"><asp:dropdownlist id="ddlArea" runat="server"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="titleField" style="HEIGHT: 17px"></TD>
						<TD style="HEIGHT: 17px"></TD>
						<TD style="HEIGHT: 17px"></TD>
						<TD class="titleField" style="HEIGHT: 17px"></TD>
						<TD style="HEIGHT: 17px"></TD>
						<TD style="HEIGHT: 17px"></TD>
					</TR>
					<TR>
						<TD class="titleField" colSpan="6">
							<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgSAPList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
									BackColor="#dedede" PageSize="500" CellPadding="3" CellSpacing="1" BorderWidth="0px">
									<AlternatingItemStyle Wrap="False" BackColor="White"></AlternatingItemStyle>
									<ItemStyle ForeColor="Black" BackColor="#F1F6FB" VerticalAlign="Top"></ItemStyle>
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn Visible="False" HeaderText="ID">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
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
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerCode") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblSalesmanCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblSalesmanName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="SalesmanHeader.JobPosition.Description" HeaderText="Kategori">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemTemplate>
												<asp:Label id="lblKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="GradeSWAP" HeaderText="Presentasi Produk dgn SWAP &amp; Simulasi PKT">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:DropDownList id="ddlSWAP" runat="server" Enabled="False">
													<asp:ListItem Value="Silahkan Pilih" Selected="True">Silahkan Pilih</asp:ListItem>
													<asp:ListItem Value="0">0</asp:ListItem>
													<asp:ListItem Value="25">25</asp:ListItem>
													<asp:ListItem Value="50">50</asp:ListItem>
													<asp:ListItem Value="75">75</asp:ListItem>
													<asp:ListItem Value="100">100</asp:ListItem>
												</asp:DropDownList>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="GradePresentasi" HeaderText="Presentasi Produk">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:DropDownList id="ddlPresentasi" runat="server" Enabled="False">
													<asp:ListItem Value="Silahkan Pilih" Selected="True">Silahkan Pilih</asp:ListItem>
													<asp:ListItem Value="0">0</asp:ListItem>
													<asp:ListItem Value="25">25</asp:ListItem>
													<asp:ListItem Value="50">50</asp:ListItem>
													<asp:ListItem Value="75">75</asp:ListItem>
													<asp:ListItem Value="100">100</asp:ListItem>
												</asp:DropDownList>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="GradeKonsistensi" HeaderText="Konsistensi Peserta SAP (Supervisor &amp; Salesman)">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:DropDownList id="ddlKonsistensi" runat="server" Enabled="False">
													<asp:ListItem Value="Silahkan Pilih" Selected="True">Silahkan Pilih</asp:ListItem>
													<asp:ListItem Value="0">0</asp:ListItem>
													<asp:ListItem Value="25">25</asp:ListItem>
													<asp:ListItem Value="50">50</asp:ListItem>
													<asp:ListItem Value="75">75</asp:ListItem>
													<asp:ListItem Value="100">100</asp:ListItem>
												</asp:DropDownList>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="GradeKelengkapan" HeaderText="Kelengkapan &amp; Validitas Data">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:DropDownList id="ddlValiditasData" runat="server" Enabled="False">
													<asp:ListItem Value="Silahkan Pilih" Selected="True">Silahkan Pilih</asp:ListItem>
													<asp:ListItem Value="0">0</asp:ListItem>
													<asp:ListItem Value="50">50</asp:ListItem>
													<asp:ListItem Value="100">100</asp:ListItem>
												</asp:DropDownList>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="GradeFrekuensi" HeaderText="Frekuensi In House Training">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtFrekInHouseTr onblur="NumOnlyBlurWithOnGridTxt(this,'');" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.GradeFrekuensi") %>' Enabled="False" MaxLength="5">
												</asp:TextBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="JumlahPeserta" HeaderText="Jumlah Peserta Training">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" onblur="NumOnlyBlurWithOnGridTxt(this,'');" id=txtJmlPeserta runat="server" Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.JumlahPeserta") %>' Enabled="False" MaxLength="5">
												</asp:TextBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn Visible="False" SortExpression="SalesmanHeader.JobPosition.Code">
											<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
											<ItemTemplate>
												<asp:Label id="lblSCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Code") %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></DIV>
						</TD>
					</TR>
					<TR>
						<TD class="titleField" style="HEIGHT: 17px" align="center" colSpan="6"><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>
							<asp:Button id="btnDownload" runat="server" Text="Download" Enabled="False" Width="60px"></asp:Button></TD>
					</TR>
				</TABLE>
				<INPUT id="hdnFieldTemp" style="Z-INDEX: 101; LEFT: 120px; POSITION: absolute; TOP: 712px"
					type="hidden" name="Hidden1" runat="server">
			</FORM>
		</DIV>
	</body>
</HTML>

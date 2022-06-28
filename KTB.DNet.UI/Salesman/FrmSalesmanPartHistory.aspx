<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanPartHistory.aspx.vb" Inherits="FrmSalesmanPartHistory" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,600,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
		
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
			function ShowSalesmanSelection()
			{	
				var lblSalesmanCode = document.getElementById("lblShowSalesman");
				showPopUp('../PopUp/PopUpSalesmanPart.aspx?IsGroupDealer=1&IsSales=1','',470,600,SalesmanSelection);
			}
				
			function SalesmanSelection(SelectedSalesman)
			{
				var tempParam = SelectedSalesman.split(';');
				var txtSalesmanCode = document.getElementById("txtID");
				var txtNama = document.getElementById("txtNama");
				txtSalesmanCode.value = tempParam[0]
				txtNama.value = tempParam[1];
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage"><asp:label id="lblPageTitle" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%">
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="191px" Visible="False"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server" Visible="False">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label>
									<asp:label id="lblDealer" runat="server" Width="220px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Employee ID</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<TD>
									<asp:textbox onkeypress="TxtKeypress();" id="txtID" onblur="TxtBlur('txtID');" runat="server"
										MaxLength="15" Visible="False"></asp:textbox>
									<asp:label id="lblShowSalesman" runat="server" width="16px" Visible="False">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
									<asp:label id="lblSalesmanCode" runat="server" Width="220px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nama</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD align="left">
									<asp:textbox onkeypress="TxtKeypress();" id="txtNama" onblur="TxtBlur('txtNama');" runat="server"
										MaxLength="15" Visible="False"></asp:textbox>&nbsp;
									<asp:label id="lblNama" runat="server" Width="220px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td><asp:button id="btnCari" runat="server" width="60px" Text="Cari" Visible="False"></asp:button></td>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dgSalesmanHeader" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
											PageSize="25" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
											AllowSorting="True" DESIGNTIMEDRAGDROP="57">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerCode" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <%--<asp:TemplateColumn SortExpression="SalesmanHeader.DealerBranch.DealerBranchCode" HeaderText="Kode Cabang">
													<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerBranchCode" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn SortExpression="SalesmanCode" HeaderText="Kode Employee">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblSalesCode" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanAdditionalInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName"
													HeaderText="Kategori">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblKategori" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanAdditionalInfo.SalesmanCategoryLevel.PositionName" HeaderText="Posisi">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPosisi" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Level">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ChangedDate" SortExpression="ChangedDate" HeaderText="Tanggal Perubahan"
													DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnApprove" CausesValidation="False" Runat="server" text="Aktif" CommandName="Approve"
															ToolTip="Disetujui">
															<img border="0" src="../images/aktif.gif" alt="Disetujui" style="cursor:hand"></asp:LinkButton>
														<asp:LinkButton id="lbtnReject" CausesValidation="False" Runat="server" text="Non-aktif" CommandName="Reject"
															ToolTip="Ditolak">
															<img border="0" src="../images/in-aktif.gif" alt="Ditolak" style="cursor:hand"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</TD>
							</TR>
							<TR>
								<td><asp:button id="btnBack" runat="server" width="60px" Text="Kembali"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

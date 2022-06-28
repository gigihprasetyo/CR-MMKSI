<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryTest.aspx.vb" Inherits="FrmEntryTest" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListParticipant</title>
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
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colspan="6">SAP - Entry Result Writing 
						Test</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1" colspan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6" colspan="6"><IMG height="6" src="../images/blank.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%" border="0">
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
								<td>
									<asp:Label id="lblStartPeriod" runat="server"></asp:Label></td>
								<td>s/d</td>
								<td>
									<asp:Label id="lblEndPeriod" runat="server"></asp:Label></td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<TD class="titleField" style="HEIGHT: 17px">Kode Dealer</TD>
					<TD style="HEIGHT: 17px">:</TD>
					<TD style="HEIGHT: 17px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerCode" onblur="omitSomeCharacter('txtSAPNo','<>?*%$;')"
							runat="server" size="22"></asp:textbox><asp:label id="lblDealerCode" runat="server" width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
					<TD class="titleField" style="HEIGHT: 17px">Kategori</TD>
					<TD style="HEIGHT: 17px">:</TD>
					<TD style="HEIGHT: 17px">
						<asp:DropDownList id="ddlKategori" runat="server" Width="112px"></asp:DropDownList></TD>
				</tr>
				<TR>
					<TD class="titleField" style="HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px">
						<asp:Button id="btnCari" runat="server" Width="72px" Text="Cari"></asp:Button></TD>
					<TD class="titleField" style="HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 17px" colSpan="6">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 320px" DESIGNTIMEDRAGDROP="245"><asp:DataGrid id="dtgSAPList" runat="server" AllowSorting="True" AutoGenerateColumns="False" PageSize="500"
								CellPadding="3" CellSpacing="1" BorderWidth="0px" Width="100%">
								<AlternatingItemStyle Wrap="False" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
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
										<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSalesmanName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
										<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSalesmanCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.JobPosition.Description" HeaderText="Kategori">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,',');" id=TxtNilai onblur="NumOnlyBlurWithOnGridTxt(this,',');" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.TestScoreDisplay") %>' CssClass="textRight" MaxLength="3">
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 17px" colSpan="6">
						<asp:Button id="btnSimpan" runat="server" Text="Simpan" Width="110px"></asp:Button>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hdnFieldTemp" style="Z-INDEX: 101; LEFT: 120px; POSITION: absolute; TOP: 712px"
				type="hidden" name="Hidden1" runat="server">
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDueDate.aspx.vb" Inherits="FrmDueDate"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Informasi Pembayaran - Tipe Obligasi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function TxtBlur(objTxt)
		{
			omitSomeCharacter(objTxt,'<>?*%$');
		}
		/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
		function TxtKeypress()
		{
			return alphaNumericExcept(event,'<>?*%$')
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
		
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{

			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtDealerCodeSelection.focus();
				txtDealerCodeSelection.blur();
			}
			else
			{
				txtDealerCodeSelection.onchange();
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 20px">INFORMASI PEMBAYARAN&nbsp;- Daftar Jatuh 
						Tempo</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="3"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="3"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="13%">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="31%">
									<asp:label id="lblMKodeDealer" Runat="server"></asp:label><asp:textbox id="txtDealerCode" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$()_-|\/#,{}=+^`~');"
										onblur="omitSomeCharacter('txtDealerCode','<>?\/*%$');"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label></TD>
								<TD class="titleField" width="2%">&nbsp;</TD>
								<TD class="titleField" width="13%">No Validasi</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD class="titleField"><asp:textbox id="txtNoValidasi" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$()_-|\/#,{}=+^`~');"
										onblur="omitSomeCharacter('txtNoValidasi','<>?\/*%$');"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="13%">Tipe Obligasi</TD>
								<TD width="1%">:</TD>
								<TD width="31%">
									<asp:DropDownList id="ddlTipeObligasi" runat="server"></asp:DropDownList></TD>
								<TD class="titleField" width="2%">&nbsp;</TD>
								<TD class="titleField" width="13%">Jenis Obligasi</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD class="titleField">
									<asp:DropDownList id="ddlTipeAssignment" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="titleField" vAlign="top" width="13%">Tgl Jatuh Tempo</TD>
								<TD width="1%">:</TD>
								<TD vAlign="middle" width="31%">
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><CC1:INTICALENDAR id="icTransDateStart" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><CC1:INTICALENDAR id="icTransDateEnd" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="titleField" width="2%">&nbsp;</TD>
								<TD class="titleField" vAlign="top" width="13%">Status</TD>
								<TD class="titleField" vAlign="top" width="1%">:</TD>
								<TD class="titleField" vAlign="top"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" vAlign="top" width="13%"></TD>
								<TD width="1%"></TD>
								<TD vAlign="middle" width="31%"><asp:button id="btnCari" runat="server" Text="Cari" Width="60px"></asp:button></TD>
								<TD class="titleField" width="2%"></TD>
								<TD class="titleField" vAlign="top" width="13%"></TD>
								<TD class="titleField" vAlign="top" width="1%"></TD>
								<TD class="titleField" vAlign="top"></TD>
							</TR>
							<TR>
								<TD width="31%" colSpan="7"><div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgDaftarJatuhTempo" runat="server" Width="100%" DataKeyField="ID" CellPadding="3"
											BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" AllowSorting="True"
											AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
													document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox ID="chkSelection" Runat="server"></asp:CheckBox>
														<asp:Label ID="lblID" Text = '<%# DataBinder.Eval(Container, "DataItem.ID")  %>' Runat="server" Visible="False">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblKodeDealer" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Assignment" HeaderText="Assignment">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblAssignment" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.Assignment")%>' >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PaymentObligationType.Description" HeaderText="Tipe">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblTipe" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.PaymentObligationType.Description")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DocDate" HeaderText="Tgl Dokumen">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="Label2" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.DocDate"),"dd/MM/yyyy")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DueDate" HeaderText="Tgl Jatuh Tempo">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblJatuhTempo" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.DueDate"),"dd/MM/yyyy")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PaymentAssignmentType.Description" HeaderText="Jenis">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblJenis" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.PaymentAssignmentType.Description")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PaymentRegDoc.ID" HeaderText="No Registrasi">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="Label3" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.PaymentRegDoc.ID")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Amount" HeaderText="Jumlah">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblJumlah" Runat="server" Text = '<%#  Format(DataBinder.Eval(Container, "DataItem.Amount"),"#,##0") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PaidAmount" HeaderText="Nilai Pembayaran">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblPaidAmount" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.PaidAmount"),"#,##0")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Pinalty" HeaderText="Pinalty">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblPinalty" Runat="server" Text = '<%# Format(DataBinder.Eval(Container, "DataItem.Pinalty"),"#,##0")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD width="31%" colSpan="7"><asp:button id="btnProses" runat="server" Text="Proses" Width="60px"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
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

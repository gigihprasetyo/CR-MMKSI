<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanTrainingMember.aspx.vb" Inherits="FrmSalesmanTrainingMember" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				txtDealerCodeSelection.value =selectedDealer;
				/*
				var arrValue = selectedDealer.split(';');
				txtDealerCodeSelection.value = arrValue[0];*/
				
			}
			function JobPosSelection(selectedJobPos)
			{
				var txtJobPos = document.getElementById("txtJobPosition");
				txtJobPos.value = selectedJobPos;
				/*
				var arrValue = selectedJobPos.split(';');
				txtJobPos.value = arrValue[0];*/
			}
			
			function AddPesertaTraining() 
			{
				if(navigator.appName != "Microsoft Internet Explorer") {
					opener.dialogWin.returnFunc('');
					window.close();
				}else {
					window.returnValue = '';
					window.close();
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">PELATIHAN TENAGA PENJUAL - Peserta 
						Pelatihan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 508px">
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="128px" ToolTip="Dealer Search 1" ReadOnly="True"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px" Enabled="False" Visible="False">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Posisi</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtJobPosition" onblur="omitSomeCharacter('txtJobPosition','<>?*%$;')"
										runat="server" MaxLength="50" Width="144px"></asp:textbox><asp:label id="lblSearchJobPos" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Tgl Mulai Bekerja</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" noWrap width="25%"><asp:dropdownlist id="ddlOperator" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><cc1:inticalendar id="icHireDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Level</TD>
								<TD style="HEIGHT: 17px" width="1%">
									<asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" noWrap width="25%">
									<asp:dropdownlist id="ddlLevel" runat="server" Width="152px"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgSalesmanTraining" runat="server" Width="100%" AllowPaging="True" PageSize="25"
											AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItem',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItem" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesId" Runat="server" Visible="False"></asp:Label>
														<asp:Label ID="lblName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="JobPosition.Code" HeaderText="Posisi">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPosition" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanLevel.Description" HeaderText="Level">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="HireDate" HeaderText="Tgl Mulai Kerja">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblHireDate" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.HireDate"),"dd/MM/yyyy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Training yang pernah diikuti">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
						<asp:button id="btnPilih" runat="server" Width="60px" Text="Pilih"></asp:button><INPUT id="btnKembali" onclick="window.close()" type="button" value="Kembali" name="btnKembali">
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

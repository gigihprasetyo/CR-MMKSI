<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanTurnOver.aspx.vb" Inherits="FrmSalesmanTurnOver" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
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
			
		</script>
		<script language="javascript">
			function ShowPPTurnOverAnalisaSelection()
			{
				var indek = GetCurrentSpanIndex();
				var _dealerCode = dgSalesmanTurnOver.rows[indek].getElementsByTagName("SPAN")[0];
				alert(indek);
				showPopUp('../Salesman/FrmSalesmanTurnOverAnalisa.aspx?DealerCode='+ _dealerCode.innerHTML,'',500,760,'')
				
			}
			/* pastikan datagrid benar */
			function GetCurrentSpanIndex()
			{
				var dgSalesmanTurnOver = document.getElementById("dgSalesmanTurnOver");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgSalesmanTurnOver.rows.length; index++)
				{
					inputs = dgSalesmanTurnOver.rows[index].getElementsByTagName("SPAN");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}				
				return -1;
			}	
			
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=true','',500,860,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				//var txtDealerNameSelection = document.getElementById("txtDealerName");
				var arrValue = selectedDealer.split(';');
				
				txtDealerCodeSelection.value = arrValue[0];
				//txtDealerNameSelection.value = arrValue[1];
				//alert('x');
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">TENAGA PENJUAL- Rekap Pengunduran Diri 
						Tenaga Penjual</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 24px" width="24%">Unit</TD>
								<TD style="HEIGHT: 24px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 24px" width="25%"><asp:dropdownlist id="ddlSalesmanUnit" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 24px" width="20%"></TD>
								<TD style="HEIGHT: 24px" width="1%"></TD>
								<TD style="HEIGHT: 24px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
										runat="server" Width="128px" MaxLength="10" ToolTip="Dealer Search 1"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%">Periode</TD>
								<TD style="HEIGHT: 20px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%" colSpan="2">
									<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" border="0">
										<TR>
											<TD><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD style="WIDTH: 29px">s/d</TD>
											<TD><cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px" width="24%"></TD>
								<TD style="HEIGHT: 24px" width="1%"></TD>
								<TD style="HEIGHT: 24px" noWrap width="25%"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 24px" width="20%"></TD>
								<TD style="HEIGHT: 24px" width="1%"></TD>
								<TD style="HEIGHT: 24px" width="29%"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgSalesmanTurnOver" runat="server" Width="100%" ShowFooter="True" AllowPaging="True"
											PageSize="25" AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1"
											BorderWidth="0px" CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Masuk">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesHire" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalSalesHire" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Keluar">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign=Right></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesResign" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<b>Total:</b>
														<asp:Label ID="lblTotalSalesResign" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit"
															Visible="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete" Visible="False">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmRegisteredDoc.aspx.vb" Inherits="FrmRegisteredDoc"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmRegisteredDoc</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function getNextSibling(startBrother){
 				endBrother=startBrother.nextSibling;
 				while(endBrother.nodeType!=1){
   					endBrother = endBrother.nextSibling;
 				} 				
 				return endBrother;
			}
			
		
		var isshown = false;
		function toggleDetail(elm){
				
				if (getNextSibling(elm.parentNode.parentNode).style.display =="none")
				{
					isshown = false;
				}
				if (getNextSibling(elm.parentNode.parentNode).style.display =="")
				{
					isshown = true;
				}
				if(!isshown){										
					getNextSibling(elm.parentNode.parentNode).style.display = "block";
					getNextSibling(elm.parentNode.parentNode).style.display = "";					
					isshown = true;
				}
				else{							
					getNextSibling(elm.parentNode.parentNode).style.display = "none";										
					getNextSibling(elm.parentNode.parentNode).style.display = "";
					getNextSibling(elm.parentNode.parentNode).style.display = "none";
					isshown = false;
				}
				
				if(elm.childNodes[2].tagName == 'IMG'){
					if(elm.childNodes[2].style.display == 'none'){
						elm.childNodes[2].style.display = 'block';										
					}
					else{
						elm.childNodes[2].style.display = 'none';					
						
					}
				}
				else{
					if(elm.childNodes[3].style.display == 'none'){
						elm.childNodes[3].style.display = 'block';										
					}
					else{
						elm.childNodes[3].style.display = 'none';					
						
					}				
				}
				
				if(elm.childNodes[0].tagName == 'IMG'){
					if(elm.childNodes[0].style.display == 'none'){
						elm.childNodes[0].style.display = 'block';
					}
					else{					
						elm.childNodes[0].style.display = 'none';
					}
				}
				else{
					if(elm.childNodes[1].style.display == 'none'){
						elm.childNodes[1].style.display = 'block';
					}
					else{					
						elm.childNodes[1].style.display = 'none';
					}				
				}
				
			}
			
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtKodeDealer");
			txtDealer.value = tempParam[0];				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" ms_positioning="text2D">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TBODY>
						<tr>
							<td class="titlePage">INFORMASI PEMBAYARAN&nbsp;-&nbsp; Daftar Register Doc</td>
						</tr>
						<tr>
							<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
						</tr>
						<tr>
							<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
						</tr>
						<TR>
							<TD>
								<TABLE id="Table2" cellSpacing="1" cellPadding="2" border="0">
									<TR>
										<td class="titleField" width="15%">Dealer</td>
										<td class="titleField" width="1%">:</td>
										<TD width="84%">
											<asp:label id="lblKodeDealer" Runat="server"></asp:label><asp:textbox id="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$()_-|\/#,{}=+^`~');"
												onblur="omitSomeCharacter('txtKodeDealer','<>?\/*%$');"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelection()"></asp:label></TD>
									</TR>
									<TR>
										<td class="titleField" width="15%">No. Registrasi</td>
										<td class="titleField" width="1%">:</td>
										<TD width="84%"><asp:textbox id="txtRegNo" runat="server" onkeypress="return numericOnlyUniv(event)" onblur="return NumericOnlyBlurWith(txtRegNo,'');"></asp:textbox></TD>
									</TR>
									<TR>
										<td class="titleField" width="15%">Tanggal Registrasi</td>
										<td class="titleField" width="1%">:</td>
										<TD width="84%">
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<TD><cc1:inticalendar id="icFromRegDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
													<TD class="titleField">&nbsp;s/d&nbsp;</TD>
													<TD><cc1:inticalendar id="icToRegDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
												</tr>
											</table>
										</TD>
									</TR>
									<tr>
										<td></td>
										<td></td>
										<td><asp:button id="btnCari" Runat="server" Text="Cari" Width="60px"></asp:button></td>
									</tr>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datalist id="dlPaymentRegDoc" Runat="server" Width="100%" CellSpacing="1" CellPadding="1"
										BorderColor="Gainsboro" BackColor="#ffffff" BorderWidth="0px">
										<HeaderTemplate>
											<tr height="22">
												<td class="titleTableSales" align="center"></td>
												<td class="titleTableSales" align="center">No</td>
												<td class="titleTableSales" align="center">Dealer</td>
												<td class="titleTableSales" align="center">No. Registrasi</td>
												<td class="titleTableSales" align="center">Tanggal Registrasi</td>
												<td class="titleTableSales" align="center">No. BOR</td>
												<td class="titleTableSales" align="center">Jumlah</td>
												<td class="titleTableSales" align="center">Diproses Oleh</td>
												<td class="titleTableSales" align="center"></td>
											</tr>
										</HeaderTemplate>
										<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
										<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
										<ItemTemplate>
											<TR>
												<TD class="bottomLine" align="center">
													<asp:label id="lblNo" onclick="toggleDetail(this)" Runat="server" Font-Bold="True">
														<img src="../images/plus.gif"> <img style="display:none" src="../images/minus.gif">
													</asp:label></TD>
												<TD class="bottomLine" align="center">&nbsp;<%# DataBinder.Eval(Container, "ItemIndex") + 1 %></TD>
												<TD class="bottomLine" align="center">&nbsp;
													<asp:label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
													</asp:label></TD>
												<TD class="bottomLine" align="right">
													<asp:label id=lblRegNo runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
													</asp:label>&nbsp;</TD>
												<TD class="bottomLine" align="center">&nbsp;
													<asp:label id=lblRegDate runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.CreateTime"),"dd/MM/yyyy") %>'>
													</asp:label></TD>
												<TD class="bottomLine" align="right">
													<asp:label id=lblBORNo runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BORNumber") %>'>
													</asp:label>&nbsp;</TD>
												<TD class="bottomLine" align="right">
													<asp:label id="lblJumlah" runat="server"></asp:label>&nbsp;</TD>
												<TD class="bottomLine" align="left">&nbsp;
													<asp:label id=lblProcessBy runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateBy") %>'>
													</asp:label></TD>
												<TD class="bottomLine" align="center">
													<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CommandName="Edit" CausesValidation="False">
														<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton></TD>
											</TR>
											<TR style="DISPLAY: none">
												<TD></TD>
												<TD colspan="7">
													<asp:DataGrid id="dtgPaymentObligation" runat="server" Width="100%" CellSpacing="1" CellPadding="3"
														BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px" ForeColor="White" PageSize="25"
														DataKeyField="ID" AutoGenerateColumns="False">
														<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
														<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
														<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#F28625"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="No">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblNoDetail" Runat="server" Text='<%# DataBinder.Eval(Container, "ItemIndex") + 1 %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Assignment" HeaderText="Assignment">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblAssignment" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Assignment") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="PaymentObligationType.Description" HeaderText="Tipe Obligasi">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblPODesc" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentObligationType.Description") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="DocDate" HeaderText="Tanggal Dokumen">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblDocDate" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DocDate"),"dd/MM/yyyy") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="TransactionDueDate" HeaderText="Tanggal Jatuh Tempo">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblTransactionDueDate" Runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.TransactionDueDate"),"dd/MM/yyyy") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Amount" HeaderText="Jumlah">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemStyle HorizontalAlign="Right"></ItemStyle>
																<ItemTemplate>
																	<asp:Label ID="lblAmount" Runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.Amount"),"#,##0") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Description" HeaderText="Keterangan">
																<HeaderStyle CssClass="titleTableSales2"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label ID="lblDesc" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:DataGrid></TD>
											</TR>
										</ItemTemplate>
										<HeaderStyle Font-Bold="True"></HeaderStyle>
										<EditItemTemplate>
											<TR>
												<TD style="COLOR: black; BACKGROUND-COLOR: #ffffff" align="center"></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="center"><%# DataBinder.Eval(Container, "ItemIndex") + 1 %></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="center">
													<asp:label id=lblEDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
													</asp:label></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="right">
													<asp:label id=lblEID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
													</asp:label></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="center">
													<asp:label id=lblECreateTime runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CreateTime"),"dd/MM/yyyy") %>'>
													</asp:label></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="right">
													<asp:TextBox id=txtNoBOR Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BORNumber") %>'>
													</asp:TextBox></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="right">
													<asp:label id="lblEJumlah" runat="server"></asp:label></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="left">
													<asp:label id=lblELastUpdate runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateBy") %>'>
													</asp:label></TD>
												<TD style="COLOR: black; BACKGROUND-COLOR: #dedede" align="center">
													<asp:LinkButton id="lbtnSave" runat="server" Text="Simpan" Width="20px" CommandName="Simpan" CausesValidation="False">
														<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
													<asp:LinkButton id="lbtnCancel" runat="server" Text="Batal" Width="20px" CommandName="Batal" CausesValidation="False">
														<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton></TD>
											</TR>
										</EditItemTemplate>
									</asp:datalist></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="titleField" vAlign="top"></TD>
						</TR>
					</TBODY>
				</TABLE>
			</DIV>
		</form>
	</body>
</HTML>

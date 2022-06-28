<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanPartTarget.aspx.vb" Inherits="FrmSalesmanPartTarget" smartNavigation="False" %>
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
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
			
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
			function GetIndex(CtlID)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
					var row = CtlID.parentElement.parentElement;
					indexRow = row.rowIndex;
					return row.rowIndex;
				}
				else
				{
					var row = CtlID.parentNode.parentNode;
					indexRow = row.rowIndex;
					return row.rowIndex;
				}
			}
			
			function ShowSalesmanPart(lblId)
			{
				var indek = GetIndex(lblId);
				//showPopUp('../General/FrmKodeWarna.aspx?type='+KodeTipe.value+'&pktype=0','',400,400,KodeWarna)
				showPopUp('../PopUp/PopUpSalesmanPart.aspx?IsGroupDealer=0&IsSales=1&IsResign=0','',470,600,SalesmanSelection);
			}
			
			function SalesmanSelection(selectedSalesman)
			{
				var tempParam = selectedSalesman.split(';');
				var indek = indexRow;
				var dgSalesmanTarget = document.getElementById("dgSalesmanTarget");
				var salesCode = dgSalesmanTarget.rows[indek].getElementsByTagName("INPUT")[0];
				
				//var hiddenField = document.getElementById("HideSalesmanID")
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				salesCode.innerText = tempParam[0];
				//hiddenField.innerText = tempParam[1];				
				}
				else
				{
				salesCode.value = tempParam[0];
				//hiddenField.value = tempParam[1];
				}
			}
			
			function ShowPopUpHistory(id)
			{
				showPopUp('../PopUp/PopUpSalesmanPartTargetHist.aspx?id=' + id, '', 340, 560);
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblPageTitle" runat="server">PART EMPLOYEE - Entry Sales Target & Realisasi</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="191px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Dealer" src="../images/popup.gif"
											border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Bulan</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<td><asp:dropdownlist id="ddlMonth" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Tahun</TD>
								<TD width="1%">:</TD>
								<TD><asp:dropdownlist id="ddlYear" tabIndex="12" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td>
									<asp:button id="btnCari" runat="server" width="60px" Text="Cari"></asp:button>
									<asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>
								</td>
							</TR>
							<tr>
								<td colspan="3" class="titleField">
									&nbsp;
								</td>
							</tr>
							<tr>
								<td colspan="3" class="titleField">
									*) Target dan Realisasi belum termasuk PPN
								</td>
							</tr>
							<TR>
								<TD class="titleField" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dgSalesmanTarget" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
											PageSize="25" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
											AllowSorting="True" >
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Part Employee ID">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblSalesmanCode runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SalesmanHeader.SalesmanCode" ) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtSalesmanCodeF" runat="server" BackColor="White"  ></asp:TextBox>
														<asp:Label id="lblSalesmanCodeF" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id=txtSalesmanCodeE runat="server" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "SalesmanHeader.SalesmanCode" ) %>'>
														</asp:TextBox>
														<asp:Label id="lblSalesmanCodeE" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nama">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblSalesmanName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SalesmanHeader.Name" ) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server" >
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Target">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblTarget runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Target")) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtTargetF" runat="server" Width="120px" BackColor="White" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtTargetE" runat="server" Width="120px" BackColor="White" onkeypress="return numericOnlyUniv(event)" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Target")) %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Realisasi">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="right" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblRealisasi" runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Realization")) %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtRealisasiF" runat="server" Width="120px" BackColor="White" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtRealisasiE" runat="server" Width="120px" BackColor="White" onkeypress="return numericOnlyUniv(event)" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Realization")) %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Persentase (%)">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPersentase" runat="server" Text='<%# String.Format("{0:0.00}",DataBinder.Eval(Container, "DataItem.Persentage")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" Runat="server" text="Hapus" CommandName="delete" CausesValidation="True">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" Runat="server" text="Ubah" CommandName="edit" CausesValidation="True">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnHistory" tabIndex="50" Runat="server" text="Histori" CommandName="history" CausesValidation="True">
															<img src="../images/popup.gif" border="0" alt="Histori"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" runat="server" CommandName="add" CausesValidation="False">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" text="Simpan" CommandName="update" CausesValidation="True">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" text="Batal" CommandName="cancel" CausesValidation="True">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
														<asp:LinkButton id="lbtnHistoryE" tabIndex="50" Runat="server" text="Histori" CommandName="history" CausesValidation="True">
															<img src="../images/popup.gif" border="0" alt="Histori"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
										<INPUT id="hideSalesmanID" type="hidden" name="HideSalesmanID" runat="server">
									</div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

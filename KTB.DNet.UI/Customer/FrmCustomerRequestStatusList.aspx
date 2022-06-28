<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCustomerRequestStatusList.aspx.vb" Inherits="FrmCustomerRequestStatusList"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmCustomerRequestStatusList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealer");
			txtDealer.value = tempParam[0];				
		}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">KONSUMEN - Daftar Registrasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 129px" width="129">No Pengajuan</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNoPengajuan" onblur="HtmlCharBlur(txtNoPengajuan)"
										runat="server"  Width="88px"></asp:textbox></td>
								<td class="titleField">Dealer
								</td>
								<td style="WIDTH: 7px">:
								</td>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDealer" onblur="HtmlCharBlur(txtDealer)"
										runat="server"  Width="124px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
							</TR>
							<TR>
                                <TD class="titleField" style="WIDTH: 129px" width="129">No. Reg. SPK</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtSPKNumber" onblur="HtmlCharBlur(txtSPKNumber)"
										runat="server"  Width="88px"></asp:textbox></td>
								<td class="titleField">Kota
								</td>
								<td style="WIDTH: 7px">:
								</td>
								<td><asp:dropdownlist id="ddlKota" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
                                <TD class="titleField" width="129">Nama</TD>
								<TD width="1%">:</TD>
								<td width="262"><asp:textbox 
								onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
								onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" id="txtNama" 
										runat="server"  Width="164px"></asp:textbox></td>
								<td class="titleField">Tipe
								</td>
								<td style="WIDTH: 7px">:
								</td>
								<td><asp:dropdownlist id="ddlTipePengajuan" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="WIDTH: 129px" width="129">Alamat</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtAlamat" onblur="HtmlCharBlur(txtCode)"
										runat="server" MaxLength="60" Width="164px"></asp:textbox>&nbsp;</td>
								<td class="titleField">Status
								</td>
								<td style="WIDTH: 7px">:
								</td>
								<td>
									<table>
										<tr>
											<td><asp:listbox id="lstStatus" runat="server" Width="128px" Rows="3" SelectionMode="Multiple"></asp:listbox></td>
											<td></td>
											<td></td>
										</tr>
									</table>
								</td>
							</TR>
                            <tr valign="top">
                                <TD class="titleField" width="129">Tgl Pengajuan</TD>
								<TD width="1%">:</TD>
								<td>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icTglPengajuan1" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;
											</td>
											<td><cc1:inticalendar id="icTglPengajuan2" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
									<br>
									<asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>
								</td>
                                <td class="titleField"></td>
								<td style="WIDTH: 7px"></td>
								<td></td>
                            </tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="HEIGHT: 320px; OVERFLOW: auto"><asp:datagrid id="dtgCustomerRequest" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
								CellPadding="3" GridLines="None" PageSize="50">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblReqStatus" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RequestType" HeaderText="Tipe">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblRequestType" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Pelanggan" SortExpression="CustomerCode">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblKodePelanggan" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SPKHeader.SPKNumber" HeaderText="No. SPK">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblSPKNumber runat="server"  >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RequestNo" SortExpression="RequestNo" HeaderText="No Pengajuan">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Nama" SortExpression="Name1">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNama" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Name3" SortExpression="Name3" HeaderText="Gedung">
										<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Alamat" HeaderText="Alamat">
										<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblAlamat" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kota">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKota" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="lbtnFileName" runat="server" CommandName="download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat SPL Detail"></asp:LinkButton>
										    <asp:LinkButton id="lbtnHistoryStatus" runat="server" Text="History" CommandName="HISTORY" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/alur_flow.gif" border="0" alt="History"></asp:LinkButton>

										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

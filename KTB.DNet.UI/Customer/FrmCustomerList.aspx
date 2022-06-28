<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCustomerList.aspx.vb" Inherits="FrmCustomerList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCustomerRequestStatusList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
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
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">KONSUMEN - Daftar Konsumen</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD align="left" width="100%">
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD style="WIDTH: 129px" class="titleField" width="129">Kode Pelanggan</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onblur="HtmlCharBlur(txtKodePelanggan)" id="txtKodePelanggan" onkeypress="return HtmlCharUniv(event)"
										runat="server" MaxLength="15"></asp:textbox></td>
								<td class="titleField">Dealer
								</td>
								<td style="WIDTH: 7px">:
								</td>
								<td><asp:textbox onblur="HtmlCharBlur(txtDealer)" id="txtDealer" onkeypress="return HtmlCharUniv(event)"
										runat="server" MaxLength="15"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" class="titleField" width="129"></TD>
								<TD width="1%"></TD>
								<TD style="WIDTH: 262px" width="262"></TD>
								<TD class="titleField">Status</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD><asp:dropdownlist id="ddlStatusCustDealer" runat="server" Width="152px" AutoPostBack="True">
										<asp:ListItem Value="-10">Silahkan pilih</asp:ListItem>
										<asp:ListItem Value="0">Aktif</asp:ListItem>
										<asp:ListItem Value="-1">Tidak Aktif</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 21px" class="titleField" width="129">Nama</TD>
								<TD style="HEIGHT: 21px" width="1%">:</TD>
								<td style="HEIGHT: 21px" width="262"><asp:textbox onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" id="txtNama"
										onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" runat="server" MaxLength="15"></asp:textbox></td>
								<td style="HEIGHT: 21px" class="titleField">Propinsi</td>
								<td style="WIDTH: 11px; HEIGHT: 21px">:</td>
								<td style="HEIGHT: 21px"><asp:dropdownlist id="ddlPropinsi" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" class="titleField" width="129">Alamat</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onblur="HtmlCharBlur(txtAlamat)" id="txtAlamat" onkeypress="return HtmlCharUniv(event)"
										runat="server" MaxLength="60"></asp:textbox>&nbsp;</td>
								<td class="titleField">Kota</td>
								<td style="WIDTH: 11px">:</td>
								<td><asp:dropdownlist id="ddlKota" runat="server" Width="152px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
								</td>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" class="titleField" width="129"><asp:checkbox id="chktglVer" Runat="server"></asp:checkbox>Tgl 
									Verifikasi</TD>
								<TD width="1%">:</TD>
								<td  style="WIDTH: 262px" width="262" >
									<cc1:inticalendar id="ICSampai" runat="server" TextBoxWidth="60"></cc1:inticalendar>&nbsp;s.d.
									<cc1:inticalendar id="ICHingga" runat="server" TextBoxWidth="60" visible="True"></cc1:inticalendar>
								</td>
								<td class="titleField">Company</td>
								<td style="WIDTH: 11px">:</td>
								<td><asp:dropdownlist id="ddlCompany" runat="server" Width="152px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
									<asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="HEIGHT: 310px; OVERFLOW: auto" id="div1"><asp:datagrid id="dtgCustomerRequest" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
								CellPadding="3" GridLines="None" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9CD"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('cbCheck',document.all.chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="cbCheck" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Plgn.">
										<HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblCustomerCode"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn SortExpression="Name1" HeaderText="Nama">
										<HeaderStyle Width="20%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNama" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Name3" SortExpression="Name3" HeaderText="Gedung">
										<HeaderStyle Width="12%" CssClass="titleTableMrk"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Alamat" HeaderText="Alamat">
										<HeaderStyle Width="20%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblAlamat" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kota">
										<HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKota" runat="server"></asp:Label><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id=lbtnEdit runat="server" Text="Pengajuan Faktur" CommandName="AJUKANFAKTUR" CausesValidation="False" CommandArgument=''>
												<img src="../images/edit.gif" border="0" alt="Pengajuan Faktur"></asp:LinkButton>
											<asp:LinkButton id=lbtnView runat="server" Text="Lihat Detail" CommandName="VIEW" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>
											<asp:LinkButton id=lbtnActivation runat="server" Text="Aktivasi" CommandName="AKTIF" CausesValidation="False" CommandArgument=''>
												<img src="../images/popup.gif" border="0" alt="Aktivasi"></asp:LinkButton>
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
					<TD><asp:button id="btnDelete" runat="server" Text="Delete" Visible="False"></asp:button><asp:button id="btnDownload" runat="server" Text="Download" Enabled="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

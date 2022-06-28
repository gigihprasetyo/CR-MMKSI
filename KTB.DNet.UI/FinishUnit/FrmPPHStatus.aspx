<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPPHStatus.aspx.vb" Inherits="FrmPPHStatus" smartNavigation="False" %>
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
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 18px">DO STATUS -&nbsp;Status Pengembalian PPh 
						Parkir</td>
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
								<TD class="titleField" style="WIDTH: 30%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 69%"><asp:textbox id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"
										Width="152px" ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 30%">No. Reg. Bukti Potong</TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 69%"><asp:textbox onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
										id="txtNoReg" onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" runat="server" Width="152px"
										MaxLength="15"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 30%">No. Bukti Potong PPH</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 69%"><asp:textbox onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
										id="txtNoBukti" onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" runat="server" Width="152px"
										MaxLength="15"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 30%">Status</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 69%"><asp:dropdownlist id="ddlStatus" runat="server" Width="152px">
										<asp:ListItem Value="-1">Silahkan pilih</asp:ListItem>
										<asp:ListItem Value="0">Baru</asp:ListItem>
										<asp:ListItem Value="1">Validasi</asp:ListItem>
										<asp:ListItem Value="2">Konfirmasi</asp:ListItem>
										<asp:ListItem Value="3">Batal</asp:ListItem>
										<asp:ListItem Value="4">Selesai</asp:ListItem>
									</asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 30%">Tgl. Generate</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 69%" align="left">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td><cc1:inticalendar id="ICStart" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="ICEnd" runat="server" TextBoxWidth="60" visible="True"></cc1:inticalendar></td>
										</tr>
									</table>
								</td>
							</TR>
							<tr>
								<TD class="titleField" style="WIDTH: 30%"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 69%"><asp:button id="btnSearch" Width="80px" Text="Cari" Runat="server"></asp:button></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgPPHStatus" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9CD"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('cbCheck',document.all.chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbCheck" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblStatusID" Visible="False"></asp:Label>
											<asp:Label Runat="server" ID="lblStatus"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeDealer" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.CreditAccount" HeaderText="Credit Account">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NoReg" SortExpression="NoReg" HeaderText="No. Reg Bukti Potong">
										<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BuktiPotongNumber" SortExpression="BuktiPotongNumber" HeaderText="No. Bukti Potong PPH">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Tgl. Generate">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTglKembali" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Form PPH">
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblSurat" runat="server">
												<img src="../images/download.gif" style="cursor:hand" border="0" alt="Surat Biaya Parkir"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Interest" Visible ="False" >
										<HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:label id="lblInterest" runat="server">
												<img src="../images/download.gif" style="cursor:hand" border="0" alt="Surat Biaya Parkir"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ReturnAssignNumber" SortExpression="ReturnAssignNumber" HeaderText="No JV Pengembalian">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/edit.gif" border="0" alt="Edit Detail"></asp:LinkButton>
											<asp:LinkButton id=lbtnView runat="server" Text="Detail" CommandName="View" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>
											<asp:LinkButton id="lbtnHistory" runat="server" Text="History" CommandName="History" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/popup.gif" border="0" alt="Lihat History"></asp:LinkButton>
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
					<TD class="titleField"><asp:label id="lblGenerate" runat="server">Mengubah Status :</asp:label><asp:dropdownlist id="ddlProcess" Width="160px" Runat="server">
							<asp:ListItem Value="-1">Silahkan pilih</asp:ListItem>
							<asp:ListItem Value="1">Validasi</asp:ListItem>
							<asp:ListItem Value="0">Batal Validasi</asp:ListItem>
							<asp:ListItem Value="3">Hapus / Batal</asp:ListItem>
						</asp:dropdownlist><asp:button id="btnProcess" runat="server" Text="Proses" Visible="False"></asp:button>
						<asp:button id="btnTransfer" runat="server" Text="Transfer ke SAP" Visible="False"></asp:button>
						<asp:button id="btnDownload" runat="server" Text="Download" Enabled="False"></asp:button>
						
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

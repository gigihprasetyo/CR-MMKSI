<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAdequacyReport.aspx.vb" Inherits="FrmAdequacyReport"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFactoring</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function ShowPPAccountSelection()
			{
				showPopUp('../PopUp/PopUpCreditAccountSelection.aspx','',500,760,AccountSelection);
			}
			
			function AccountSelection(selectedAccount)
			{
				var txtCreditAccount = document.getElementById("txtCreditAccount");
				var txtDealerName = document.getElementById("txtDealerName");
				
				var str = selectedAccount.split(";");
				txtCreditAccount.value = str[0];			
				txtDealerName.value=str[1];
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 13px">FACTORING - Ceiling Adequacy Report</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 109px"><B>Credit Account</B></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD style="WIDTH: 299px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD style="WIDTH: 211px"><B>Permintaan Kirim</B></TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><cc1:inticalendar id="calRequestDelDate" runat="server"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 109px; HEIGHT: 14px"><B>Nama Dealer</B></TD>
								<TD style="WIDTH: 7px; HEIGHT: 14px">:</TD>
								<TD style="WIDTH: 299px; HEIGHT: 14px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerName" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="288px" Enabled="False"></asp:textbox></TD>
								<TD style="WIDTH: 211px; HEIGHT: 14px"><STRONG>Tanggal Laporan</STRONG></TD>
								<TD style="WIDTH: 2px; HEIGHT: 14px">:</TD>
								<TD style="HEIGHT: 14px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtReportDate" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="144px" Enabled="False"></asp:textbox></TD>
							</TR>
							<tr>
								<td style="WIDTH: 109px"><b>Total Sisa Ceiling</b></td>
								<td style="WIDTH: 7px">:</td>
								<td style="WIDTH: 299px">
									<asp:Label id="lblTotal" runat="server" Font-Bold="True">0</asp:Label></td>
								<TD style="WIDTH: 211px"><STRONG>Factoring Status</STRONG></TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD>
									<asp:dropdownlist id="ddlFactoringStatus" runat="server" Width="120px" Height="32px"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td style="WIDTH: 109px"></td>
								<td style="WIDTH: 7px"></td>
								<td style="WIDTH: 299px" align="left"><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button>
								</td>
								<td style="WIDTH: 211px"><STRONG>Produk</STRONG></td>
								<td style="WIDTH: 2px">:</td>
								<td>
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px"></asp:dropdownlisT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<div id="divHidden" style="HEIGHT: 360px; OVERFLOW: auto"><asp:datagrid id="dtgMain" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True"
								PageSize="25" AutoGenerateColumns="False" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Horizontal"
								CellSpacing="1">
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblNo">&nbsp;</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblCreditAccount"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblProductCategory"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAccountName"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Factoring Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblOutstanding"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling Tersedia">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAvailableCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Telah Diajukan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblPODiajukan"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblRemainCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Validitas Ceiling">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<cc1:inticalendar id="calMaxTOPDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblKeterangan"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText=" ">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetail" runat="server" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Lihat Detail">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Button Runat="server" ID="btnDownload" Text="Download"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

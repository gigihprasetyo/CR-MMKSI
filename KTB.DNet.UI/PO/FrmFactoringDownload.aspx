<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFactoringDownload.aspx.vb" Inherits="FrmFactoringDownload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFactoring</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td class="titlePage">FACTORING - Standard Factoring Ceiling Report</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td style="WIDTH: 128px"><b>Credit Account</b></td>
								<td style="WIDTH: 2px">:</td>
								<td style="WIDTH: 208px">
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox>
									<asp:label id="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label>
								</td>
								<td style="WIDTH: 126px"><STRONG>Factoring Status</STRONG></td>
								<td style="WIDTH: 3px">:</td>
								<td>
									<asp:dropdownlist id="ddlFactoringStatus" runat="server" Width="130px" Height="32px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td style="WIDTH: 128px"></td>
								<td style="WIDTH: 2px"></td>
								<td align="left" style="WIDTH: 208px">
									<asp:button id="btnFind" runat="server" Text="Cari" Width="60px"></asp:button>
									&nbsp;&nbsp;<asp:button id="btnDownload" runat="server" Text="Download" Width="60px"></asp:button>
								</td>
								<td style="WIDTH: 126px"><STRONG>Produk</STRONG></td>
								<td style="WIDTH: 3px">:</td>
								<td>
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px"></asp:dropdownlisT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<div id="divHidden" style="HEIGHT: 360px; OVERFLOW: auto">
							<asp:datagrid id="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
								CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
								AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="True" AllowSorting="True">
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
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblCreditAccount"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblProductCategory"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="60%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAccountName"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total Ceiling">
										<HeaderStyle ForeColor="White" Width="60%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblTotalCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Standard Factoring Ceiling">
										<HeaderStyle ForeColor="White" Width="60%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblStandardCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Factoring Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblFactoringCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Space For TOP">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblSpaceForTop"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="TOP Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblTopCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Additional Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAdditionalCeiling"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Validitas Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblMaxTOPDate"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

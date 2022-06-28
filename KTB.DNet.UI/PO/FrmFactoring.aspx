<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFactoring.aspx.vb" Inherits="FrmFactoring"%>
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
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
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
			function confirm_save()
			{
				return confirm("Nilai Standard Ceiling < Factoring Ceiling. Yakin ingin simpan?");
			}
			function confirmSave()
			{
				var ddl = document.getElementById("ddlProductCategory");
				var produk=ddl.options[ddl.selectedIndex].text;
				return confirm("Ceiling yang akan disimpan untuk produk " + produk +". Yakin ingin simpan?");
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td class="titlePage">FACTORING - Input Ceiling Master</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="2"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td><b>Lokasi File</b></td>
								<td>:</td>
								<td>
									<table width="100" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td><INPUT id="fliInput" onkeypress="return false;" type="file" size="29" name="fliInput" runat="server"></td>
											<td>&nbsp;&nbsp;<asp:button id="btnUpload" runat="server" Text="Upload" Width="60px"></asp:button></td>
										</tr>
									</table>
								</td>
								<td><STRONG>Factoring Status</STRONG></td>
								<td>:</td>
								<td>
									<asp:dropdownlist id="ddlFactoringStatus" runat="server" Width="120px" Height="32px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td><b>Tanggal Upload</b></td>
								<td>:</td>
								<td>
									<cc1:inticalendar id="calUpload" runat="server"></cc1:inticalendar>
								</td>
								<td><STRONG>Produk</STRONG></td>
								<td>:</td>
								<td>
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlProductCategory" runat="server" Width="120px" Height="32px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td><b>Credit Account</b></td>
								<td>:</td>
								<td>
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox>
									<asp:label id="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label>
								</td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td align="left">
									<asp:button id="btnFind" runat="server" Text="Cari" Width="60px"></asp:button>
									&nbsp;&nbsp;<asp:button id="btnSave" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:button>
								</td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<TR>
								<TD><STRONG>Total PO Diajukan</STRONG></TD>
								<TD>:</TD>
								<TD align="left">
									<b>
										<asp:Label id="lblTotalPO" runat="server" Width="168px">0</asp:Label></b></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
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
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkExport',document.all.chkAllItems.checked)">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkExport" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="lblNoE"></asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblCreditAccount"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label Runat="server" ID="lblCreditAccountE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="40%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblProductCategory"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label Runat="server" ID="lblProductCategoryE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="60%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAccountName"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label Runat="server" ID="lblAccountNameE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total Ceiling">
										<HeaderStyle ForeColor="White" Width="60%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblTotalCeiling"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox Runat="server" ID="txtTotalCeiling" onkeypress="return numericOnlyUniv(event)" onkeyup="AutoThousandSeparator(this,event);"
												CssClass="textRight" Width="100"></asp:TextBox>
											<asp:Label runat="server" ID="lblTotalCeilingE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Standard Factoring Ceiling">
										<HeaderStyle ForeColor="White" Width="60%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblStandardCeiling"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox Runat="server" ID="txtStandardCeiling" onkeypress="return numericOnlyUniv(event)"
												onkeyup="AutoThousandSeparator(this,event);" CssClass="textRight" Width="100"></asp:TextBox>
											<asp:Label runat="server" ID="lblStandardCeilingE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Factoring Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblFactoringCeiling"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox Runat="server" ID="txtFactoringCeiling" onkeypress="return numericOnlyUniv(event)"
												onkeyup="AutoThousandSeparator(this,event);" CssClass="textRight" Width="100"></asp:TextBox>
											<asp:Label runat="server" ID="lblFactoringCeilingE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblOutstanding"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="txtOutstanding" onkeypress="return numericOnlyUniv(event)" CssClass="textRight"
												onkeyup="AutoThousandSeparator(this,event);" Width="100"></asp:TextBox>
											<asp:Label runat="server" ID="lblOutstandingE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding Calc">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblOutstandingCalc"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblOutstandingCalcE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling Tersedia">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAvCeiling"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="txtAvCeiling" onkeypress="return numericOnlyUniv(event)" CssClass="textRight"
												onkeyup="AutoThousandSeparator(this,event);" Width="100"></asp:TextBox>
											<asp:Label runat="server" ID="lblAvCeilingE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Diajukan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblPODiajukan"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblPODiajukanE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAvailableCeiling"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblAvailableCeilingE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblErrorMessageE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblStatus"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblStatusE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Validitas Ceiling">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblMaxTOPDate"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<cc1:inticalendar id="calMaxTOPDateE" runat="server"></cc1:inticalendar>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="CreatedBy">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblCreatedBy"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblCreatedByE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="LastUpdatedBy">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblLastUpdatedBy"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="lblLastUpdatedByE"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnActivate" runat="server" CommandName="Activate" Visible="False">
												<img src="../images/aktif.gif" border="0" alt="Aktifasi">
											</asp:LinkButton>
											<asp:LinkButton id="lbtnDeactivate" runat="server" CommandName="Deactivate" Visible="False">
												<img src="../images/in-aktif.gif" border="0" alt="Non-Aktif">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
										CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
										EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:EditCommandColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button Runat="server" ID="btnTransfer" Text="Transfer ke SAP" />
					</td>
				</tr>
			</table>
			<INPUT id="hdnValidate" type="hidden" runat="server" name="hdnValidate">
		</form>
	</body>
</HTML>

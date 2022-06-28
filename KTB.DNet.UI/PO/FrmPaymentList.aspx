<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPaymentList.aspx.vb" Inherits="FrmPaymentList"%>
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
				
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FACTORING - Daftar Pembayaran Factoring</td>
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
							<TBODY>
								<tr>
									<td style="WIDTH: 156px"><b>Lokasi File</b></td>
									<td style="WIDTH: 11px">:</td>
									<td colSpan="4">
										<table cellSpacing="0" cellPadding="0" width="100" border="0">
											<tr height="10">
												<td valign="top"><INPUT onkeypress="return false;" id="fliInput" type="file" size="29" name="fliInput" runat="server"></td>
												<td width="2"></td>
												<td valign="top"><asp:button id="btnUpload" runat="server" Width="60px" Text="Upload"></asp:button></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 156px; HEIGHT: 25px"><b></b></td>
									<td style="WIDTH: 11px; HEIGHT: 25px">:</td>
									<td style="WIDTH: 379px; HEIGHT: 25px"></td>
									<td style="WIDTH: 118px; HEIGHT: 25px"></td>
									<td style="HEIGHT: 25px"></td>
									<td style="HEIGHT: 25px"></td>
								</tr>
								<tr>
									<td style="WIDTH: 156px; HEIGHT: 21px"><b>Credit Account</b></td>
									<td style="WIDTH: 11px; HEIGHT: 21px">:</td>
									<td style="WIDTH: 379px; HEIGHT: 21px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
											runat="server"></asp:textbox><asp:label id="lblSearchAccount" onclick="ShowPPAccountSelection();" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
										</asp:label></td>
									<td style="WIDTH: 118px; HEIGHT: 21px"><STRONG>Nama Bank</STRONG></td>
									<td style="HEIGHT: 21px">:</td>
									<td style="HEIGHT: 21px"><asp:dropdownlist id="ddlBank" runat="server" Width="152px"></asp:dropdownlist></td>
								</tr>
								<TR>
									<TD style="WIDTH: 156px; HEIGHT: 21px"><STRONG>Dealer</STRONG></TD>
									<TD style="WIDTH: 11px; HEIGHT: 21px">:</TD>
									<TD style="WIDTH: 379px; HEIGHT: 21px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
											runat="server"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPDealerSelection();" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
									<TD style="WIDTH: 118px; HEIGHT: 21px"><STRONG>No. SO</STRONG></TD>
									<TD style="HEIGHT: 21px">:</TD>
									<TD style="HEIGHT: 21px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtSONumber" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
											runat="server" Width="152px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 156px; HEIGHT: 21px"><STRONG>Tanggal Jatuh Tempo</STRONG></TD>
									<TD style="WIDTH: 11px; HEIGHT: 21px">:</TD>
									<TD style="WIDTH: 380px; HEIGHT: 21px">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TBODY>
												<tr>
													<td style="WIDTH: 150px"><cc1:inticalendar id="calStart" runat="server"></cc1:inticalendar></td>
									</TD>
									<td style="WIDTH: 31px">&nbsp;s/d &nbsp;</td>
									<td><cc1:inticalendar id="calEnd" runat="server"></cc1:inticalendar></td>
					</td>
				</tr>
			</table>
			<TD><STRONG>Status Pembayaran</STRONG>
			</TD>
			<td>:</td>
			<td>
				<asp:dropdownlist id="ddlCleared" runat="server" Width="152px">
					<asp:ListItem Value="-1" Selected="True">Silahkan Pilih</asp:ListItem>
					<asp:ListItem Value="0">Not Cleared</asp:ListItem>
					<asp:ListItem Value="1">Cleared</asp:ListItem>
				</asp:dropdownlist></td>
			</TR>
			<TR>
				<TD style="WIDTH: 156px; HEIGHT: 21px"></TD>
				<TD style="WIDTH: 11px; HEIGHT: 21px"></TD>
				<TD style="WIDTH: 380px; HEIGHT: 21px"></TD>
				<TD style="VISIBILITY:hidden"><STRONG>No. Reg. Gyro</STRONG></TD>
				<TD style="VISIBILITY:hidden">:</TD>
				<TD style="VISIBILITY:hidden">
					<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtRegNumber" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
						runat="server"></asp:textbox></TD>
			</TR>
			<tr>
				<td style="WIDTH: 156px"></td>
				<td style="WIDTH: 11px"></td>
				<td style="WIDTH: 379px" align="left"><asp:button id="btnFind" runat="server" Width="96px" Text="Find in DB"></asp:button>&nbsp;&nbsp;<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button>
					<asp:button style="Z-INDEX: 0" id="btnDownload" runat="server" Text="Download" Width="96px"
						Enabled="False"></asp:button>&nbsp;
					<asp:button style="Z-INDEX: 0" id="btnTransfer" runat="server" Visible="false" Text="Transfer Ke SAP"
						Width="96px" Enabled="False"></asp:button>
				</td>
				<td style="WIDTH: 118px"><STRONG>Total Nilai</STRONG></td>
				<td>:</td>
				<td><STRONG>
						<asp:Label id="lblTotal" runat="server">0</asp:Label></STRONG></td>
			</tr>
			</TBODY></TABLE></TD></TR>
			<tr>
				<td>
					<div id="divHidden" style="HEIGHT: 340px; OVERFLOW: auto"><asp:datagrid id="dtgMain" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True"
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
										<asp:Label runat="server" ID="lblNo"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:label Runat="server" ID="lblNoE"></asp:label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status">
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblStatus"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label Runat="server" ID="lblStatusE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No. Reg. Gyro" Visible="False">
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblRegNumber"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label Runat="server" ID="lblRegNumberE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No. Reg PO">
									<HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblPONumber"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label Runat="server" ID="lblPONumberE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor SO">
									<HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblSONumber"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox Runat="server" ID="txtSONumberE" Width="100"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No Giro/Slip Transfer">
									<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblSlipNumber"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList Runat="server" ID="ddlBankE"></asp:DropDownList>
										<asp:TextBox runat="server" ID="txtGyroNumberE" Width="100"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nomor Registrasi Pembayaran">
									<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblRegNumberDPH"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtlblRegNumberDPHE" onblur="omitSomeCharacter('txtlblRegNumberDPHE','<>?*%$')"
						                                    runat="server"></asp:textbox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tanggal Jatuh Tempo">
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblBaselineDate"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label runat="server" ID="lblBaselineDateE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tanggal Efektif">
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblEffectiveDate"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label runat="server" ID="lblEffectiveDateE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nilai (Rp)">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblAmount"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox runat="server" ID="txtAmountE" onkeypress="return numericOnlyUniv(event)" CssClass="textRight"
											onkeyup="AutoThousandSeparator(this,event);" Width="100"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status Pembayaran">
									<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblIsCleared"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList Runat="server" ID="ddlIsClearedE"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Upload Terakhir Oleh">
									<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblLastUploadedBy"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label runat="server" ID="lblLastUploadedByE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Upload Terakhir">
									<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblLastUploadedTime"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label runat="server" ID="lblLastUploadedTimeE"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reversed" Visible="False">
									<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblIsReversed"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList Runat="server" ID="ddlIsReversedE"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Re-Upload">
									<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox runat="server" ID="chkReUpload"></asp:CheckBox>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:CheckBox runat="server" ID="chkReUploadE"></asp:CheckBox>
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
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
									CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
									EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;"
									Visible="False">
									<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:EditCommandColumn>
							</Columns>
						</asp:datagrid></div>
				</td>
			</tr>
			</TBODY></TABLE></form>
	</body>
</HTML>

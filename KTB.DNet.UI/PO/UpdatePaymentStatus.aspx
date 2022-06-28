<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UpdatePaymentStatus.aspx.vb" Inherits="UpdatePaymentStatus"%>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UpdatePaymentStatus</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		function ShowPPAccountSelection()
		{
			showPopUp('../General/../PopUp/PopUpCreditAccountSelection.aspx','',500,760,AccountSelection);
		}
		function AccountSelection(selectedAccount)
		{
			var txtCreditAccount = document.getElementById("txtCreditAccount");
			//var txtDealerName = document.getElementById("txtDealerName");
			
			var str = selectedAccount.split(";");
			txtCreditAccount.value = str[0];			
			//txtDealerName.value=str[1];
		}		
		//function Back()
		//{
		//var hidden = document.getElementById("HiddenField")
		//var i = hidden.value * -1
		//window.history.go(i);

		//}
		function ViewDailyPKFlow()
		{}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="2" width="100%">
				<TBODY>
					<tr>
						<td colSpan="6">
							<table id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td class="titlePage">PEMBAYARAN - Daftar Pembayaran</td>
								</tr>
								<tr>
									<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
								</tr>
								<tr>
									<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
								</tr>
							</table>
						</td>
					</tr>
					<TR>
						<TD class="titleField" width="20%"><asp:label id="Label1" runat="server"> Dealer</asp:label></TD>
						<TD>:</TD>
						<TD style="WIDTH: 257px" width="257"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
								runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
						<TD style="WIDTH: 181px" class="titleField" width="181"><asp:label id="Label5" runat="server">No. Reg. Gyro</asp:label></TD>
						<TD style="WIDTH: 9px" width="9">:</TD>
						<TD width="27%"><asp:textbox onblur="alphaNumericPlusSpaceBlur(txtSoNumber)" id="txtRegNumber" onkeypress="return alphaNumericPlusSpaceUniv(event)"
								runat="server" MaxLength="20"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 181px" class="titleField">Kategori</TD>
						<TD style="WIDTH: 3px">:</TD>
						<TD><asp:dropdownlist id="ddlCategory" runat="server" Width="128px"></asp:dropdownlist></TD>
						<TD style="WIDTH: 181px" class="titleField" width="181"><asp:label id="Label2" runat="server">Nomor SO</asp:label></TD>
						<TD style="WIDTH: 9px" width="9">:</TD>
						<TD width="27%"><asp:textbox onblur="alphaNumericPlusSpaceBlur(txtSoNumber)" id="txtSoNumber" onkeypress="return alphaNumericPlusSpaceUniv(event)"
								runat="server" MaxLength="20"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="titleField"><asp:checkbox id="cbxTanggalProses" runat="server" Checked="True" Text="Tanggal Permintaan Kirim"></asp:checkbox></TD>
						<TD>:</TD>
						<TD>
							<table id="Table3" border="0" cellSpacing="0" cellPadding="0">
								<tr>
									<td><cc1:inticalendar id="calGyroDate1" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									<TD>&nbsp;s.d&nbsp;</TD>
									<TD><cc1:inticalendar id="calGyroDate2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								</tr>
							</table>
						</TD>
						<TD style="WIDTH: 181px" class="titleField"><asp:label id="Label6" runat="server">No Giro / Slip Transfer</asp:label></TD>
						<TD style="WIDTH: 9px"><asp:label id="Label8" runat="server">:</asp:label></TD>
						<TD><asp:textbox id="txtSlipNumber" onkeypress="return alphaNumericPlusSpaceUniv(event)" runat="server"
								MaxLength="16"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="titleField"><asp:checkbox id="cbxTanggalGiro" runat="server" Text="Tanggal Giro"></asp:checkbox></TD>
						<TD>:</TD>
						<TD style="WIDTH: 257px">
							<table id="Table4" border="0" cellSpacing="0" cellPadding="0">
								<tr>
									<td><cc1:inticalendar id="calBaselineDate1" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									<TD>&nbsp;s.d&nbsp;</TD>
									<TD><cc1:inticalendar id="calBaselineDate2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								</tr>
							</table>
						</TD>
						<TD class="titleField"><asp:label id="Label9" runat="server">Pembayaran Untuk</asp:label></TD>
						<TD style="WIDTH: 9px">:</TD>
						<TD><asp:dropdownlist id="ddlPurpose" runat="server" Width="128px"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="titleField" vAlign="top">Cara Pembayaran</TD>
						<TD>:</TD>
						<TD style="WIDTH: 257px"><asp:dropdownlist id="ddlTOP" runat="server" Width="96px">
								<asp:ListItem Value="-1" Selected="True">Silahkan Pilih</asp:ListItem>
								<asp:ListItem Value="0">COD</asp:ListItem>
								<asp:ListItem Value="1">TOP</asp:ListItem>
							</asp:dropdownlist></TD>
						<TD style="WIDTH: 181px" class="titleField">User</TD>
						<TD style="WIDTH: 9px">:</TD>
						<TD><asp:textbox id="txtSAPCreator" onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="titleField" vAlign="top"><asp:label id="lblFactoring" runat="server" Visible="False">Factoring</asp:label></TD>
						<TD><asp:label id="lblFactoringColon" runat="server" Visible="False">:</asp:label></TD>
						<TD style="WIDTH: 257px"><asp:dropdownlist id="ddLFactoring" runat="server" Width="96px" Visible="False">
								<asp:ListItem Value="-1" Selected="True">Silahkan Pilih</asp:ListItem>
								<asp:ListItem Value="0">Non Factoring</asp:ListItem>
								<asp:ListItem Value="1">Factoring</asp:ListItem>
							</asp:dropdownlist></TD>
						</TD>
						<TD style="WIDTH: 181px" class="titleField">Jenis Entry</TD>
						<TD style="WIDTH: 3px">:</TD>
						<TD><asp:dropdownlist id="ddlGyroType" runat="server"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="titleField">Status</TD>
						<TD>:</TD>
						<TD style="WIDTH: 257px"><asp:dropdownlist id="ddlStatusDP" runat="server"></asp:dropdownlist></TD>
						<TD style="WIDTH: 181px" class="titleField">Status Transfer</TD>
						<TD style="WIDTH: 3px">:</TD>
						<TD><asp:dropdownlist id="ddlIsTransfered" runat="server" Width="128px"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="titleField">Status Pembayaran</TD>
						<TD>:</TD>
						<TD style="WIDTH: 257px">
							<asp:dropdownlist id="ddlRemarkStatus" runat="server" Width="152px">
								<asp:ListItem Value="-1" Selected="True">Silahkan Pilih</asp:ListItem>
								<asp:ListItem Value="0">Not Cleared</asp:ListItem>
								<asp:ListItem Value="1">Cleared</asp:ListItem>
							</asp:dropdownlist></TD>
						<TD style="WIDTH: 181px" class="titleField">Total Nilai</TD>
						<TD style="WIDTH: 3px">:</TD>
						<TD><STRONG>Rp</STRONG>
							<asp:label id="lblTotalNilaiValue" runat="server" Font-Bold="True"></asp:label></TD>
					</TR>
					<tr>
						<td></td>
						<td></td>
						<td><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button></td>
						<TD style="WIDTH: 181px" class="titleField"></TD>
						<TD style="WIDTH: 3px"></TD>
						<TD></TD>
					</tr>
					<TR>
						<TD colSpan="6">
							<div style="WIDTH: 100%; HEIGHT: 250px; OVERFLOW: auto" id="div1"><asp:datagrid id="dgDailyPayment" runat="server" Width="100%" BackColor="#CDCDCD" CellPadding="3"
									BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="100" AllowPaging="True" AllowSorting="True">
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle BackColor="White"></ItemStyle>
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
										<asp:TemplateColumn Visible="False" HeaderText="ID">
											<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
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
											<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:TemplateColumn>
										<asp:BoundColumn SortExpression="Status" HeaderText="Status">
											<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="DailyPaymentHeader.RegNumber" HeaderText="No. Reg. Gyro">
											<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn SortExpression="POHeader.DealerPONumber" HeaderText="Dealer PO Number">
											<HeaderStyle ForeColor="White" Width="12%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="lbnNoRegPO" runat="server" CommandName="PoDetail"></asp:LinkButton>
											</ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn SortExpression="POHeader.SONumber" HeaderText="Nomor SO">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="PaymentPurpose.Description" HeaderText="Pembayaran Untuk">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
									<%--	<asp:BoundColumn DataField="SlipNumber" SortExpression="SlipNumber" HeaderText="No Giro / Slip Transfer">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>--%>
                                        <asp:TemplateColumn SortExpression="SlipNumber" HeaderText="No Giro / Slip Transfer">
	<HeaderStyle ForeColor="White" Width="12%" CssClass="titleTableSales"></HeaderStyle>
	<ItemStyle VerticalAlign="Top"></ItemStyle>
	<ItemTemplate>
		<asp:label id="lblSlipNumber" runat="server"  ></asp:label>
	</ItemTemplate>

</asp:TemplateColumn>

										<asp:BoundColumn DataField="DocDate" SortExpression="DocDate" HeaderText="Tanggal Proses" DataFormatString="{0:dd/MM/yyyy}"
											Visible="False">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="BaselineDate" SortExpression="BaselineDate" HeaderText="Tanggal Giro"
											DataFormatString="{0:dd/MM/yyyy}">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="Amount" HeaderText="Nilai (Rp)">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="Selisih">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="PPh">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn HeaderText="Nilai SO (Rp)">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Tanggal Jatuh Tempo">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label Runat="server" ID="lblTglJatuhTempo" Width="60px"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn Visible="False" HeaderText="TOP (hari)">
											<HeaderStyle ForeColor="White" Width="9%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="SAPCreator" SortExpression="SAPCreator" HeaderText="Dibuat Oleh">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn Visible="False" SortExpression="RejectStatus" HeaderText="Status Tolakan">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn SortExpression="EffectiveDate" HeaderText="Tanggal Efektif">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label Runat="server" ID="lblEffectiveDate" Width="60px" Text='<%# iif(DataBinder.Eval(Container, "DataItem.EffectiveDate")<cdate("1990.01.01"), "", format(DataBinder.Eval(Container, "DataItem.EffectiveDate"),"dd/MM/yyyy")) %>' >
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="AcceleratedDate" HeaderText="Tanggal Percepatan">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<cc1:inticalendar id="icAcceleratedDate" runat="server" TextBoxWidth="70" Value='<%# DataBinder.Eval(Container, "DataItem.AcceleratedDate") %>' >
												</cc1:inticalendar>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn SortExpression="Remark" HeaderText="Keterangan">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:textbox ID="txtRemark" Runat="server" MaxLength="500" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>'>
												</asp:textbox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn SortExpression="IsCleared" HeaderText="Cleared">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn SortExpression="IsReversed" HeaderText="Reversed">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle VerticalAlign="Top"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn SortExpression="RemarkStatus" HeaderText="Status Pembayaran">
											<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label Runat="server" ID="lblRemarkStatus"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText=" ">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton Runat="server" ID="lbtnEdit" CommandName="Edit">
													<img src="../images/edit.gif" border="0" alt="Edit">
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText=" ">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton Runat="server" ID="lbtnDownload" CommandName="download">
													<img src="../images/download.gif" border="0" alt="Download">
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText=" ">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton Runat="server" ID="lbtnDelete" CommandName="del">
													<img src="../images/trash.gif" border="0" alt="Delete" onclick="return confirm('Yakin ingin menghapus data ini?');">
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText=" ">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblHistoryStatus" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Status">
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText=" ">
											<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblFlow" runat="server">
													<img src="../images/alur_flow2.gif" style="cursor:hand" border="0" alt="Lihat Alur PO">
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></div>
						</TD>
					</TR>
					<TR>
						<TD colSpan="6"><asp:label id="Label11" runat="server" Font-Bold="True" Font-Italic="True">Mengubah Status Tolakan:</asp:label><asp:dropdownlist id="ddlAction" runat="server"></asp:dropdownlist><asp:button id="btnProses" runat="server" Text="Proses"></asp:button><asp:button id="btnDownload" runat="server" Text="Download"></asp:button>&nbsp;
							<asp:button id="btnDownloadDealer" runat="server" Text="Download Report"></asp:button><asp:button id="btnSimpan" runat="server" Width="112px" Text="Simpan Percepatan"></asp:button>&nbsp;
							<asp:button id="btnCancel" runat="server" Width="112px" Text="Batal Percepatan"></asp:button>&nbsp;
							<asp:button id="btnValidate" runat="server" Width="80px" Text="Validasi"></asp:button>&nbsp;
							<asp:button id="btnCancelValidate" runat="server" Width="90px" Text="Batal Validasi"></asp:button>&nbsp;
							<asp:button id="btnTranfer" runat="server" Width="112px" Text="Transfer Ke SAP"></asp:button><asp:button id="btnTransferUlang" runat="server" Width="112px" Text="Transfer Ulang"></asp:button><asp:button id="btnTransferAcc" runat="server" Width="168px" Text="Transfer Percepatan Ke SAP"></asp:button><asp:button id="btnTransferAccUlang" runat="server" Width="192px" Text="Transfer Ulang Percepatan Ke SAP"></asp:button></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCeiling2.aspx.vb" Inherits="FrmCeiling2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCeiling</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPAccountSelection()
		{
			showPopUp('../General/../PopUp/PopUpCreditAccountSelection2.aspx','',500,760,AccountSelection2);
		}
		
		function AccountSelection2(selectedAccount)
			{
				var tempParam= selectedAccount;
				var txtAccountSelection = document.getElementById("txtCreditAccount");
				txtAccountSelection.value = selectedAccount;	
			}

		function Spanning()
		{
			var dtgMain	= document.getElementById("dtgMain");
			var txtIsSpanned=document.getElementById("txtIsSpanned");
			var i=0;
			if(txtIsSpanned.value!="1") return false;
			if(dtgMain.rows.length<=1) return false;
			for(i=1;i<dtgMain.rows.length;i++)
			{
				if((i-1)%3==0)
				{
					dtgMain.rows[i].cells[0].rowSpan="3";
					dtgMain.rows[i].cells[0].vAlign="middle";
					dtgMain.rows[i].cells[1].rowSpan="3";
					dtgMain.rows[i].cells[1].vAlign="middle";
					dtgMain.rows[i+1].deleteCell(0);
					dtgMain.rows[i+1].deleteCell(0);
					dtgMain.rows[i+2].deleteCell(0);
					dtgMain.rows[i+2].deleteCell(0);
				}
			}
			Spanning2();
		}
		
		function Spanning2()
		{
			var dtgMain	= document.getElementById("dtgMain2");
			var txtIsSpanned=document.getElementById("txtIsSpanned");
			var i=0;
			if(txtIsSpanned.value!="1") return false;
			if(dtgMain.rows.length<=1) return false;
			for(i=1;i<dtgMain.rows.length;i++)
			{
				if((i-1)%3==0)
				{
					dtgMain.rows[i].cells[0].rowSpan="3";
					dtgMain.rows[i].cells[0].vAlign="middle";
					dtgMain.rows[i].cells[1].rowSpan="3";
					dtgMain.rows[i].cells[1].vAlign="middle";
					dtgMain.rows[i+1].deleteCell(0);
					dtgMain.rows[i+1].deleteCell(0);
					dtgMain.rows[i+2].deleteCell(0);
					dtgMain.rows[i+2].deleteCell(0);
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<!--td class="titlePage">PO HARIAN &nbsp;- Daftar PO</td-->
					<td class="titlePage">CREDIT CONTROL&nbsp;- Ceiling Position Master Data * &nbsp;<asp:label id="lblProductCategory" Visible="False" Runat="server">MMC</asp:label>
					</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD style="HEIGHT: 17px" class="titleField" width="22%"><asp:label id="lblDealer" runat="server">Credit Account</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 17px" width="328"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD style="HEIGHT: 17px" class="titleField" width="20%"><asp:label id="lblSalesOrg" runat="server" Font-Bold="True"> Permintaan Kirim</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px" width="20%"><cc1:inticalendar id="icReqDelivery" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label6" runat="server" Font-Bold="True">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtDealerName" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server" readonly="true" BorderStyle="None" Width="296px"></asp:textbox></TD>
								<TD class="titleField"><asp:label id="Label7" runat="server"> Tanggal Laporan</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox onblur="return alphaNumericPlusBlur(txtDealerPO)" id="txtReportDate" onkeypress="return alphaNumericPlusUniv(event)"
										runat="server" Width="140px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="White" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px"><asp:label id="Label1" runat="server" Font-Bold="True"> Total Sisa Ceiling</asp:label></TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 24px"><b><B><asp:label id="lblTotalCredit" runat="server" Width="176px"></asp:label></B></b></TD>
								<TD style="HEIGHT: 24px"><b>Produk</b>
								</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px"><asp:dropdownlist id="ddlProductCategory" runat="server" width="130px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button><asp:textbox style="VISIBILITY: hidden" id="txtIsSpanned" runat="server" Width="16px" ReadOnly="True">1</asp:textbox></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnSaveMaxTOPDate" runat="server" Width="136px" Text="Simpan Tanggal Validitas"></asp:button></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div style="HEIGHT: 340px; OVERFLOW: auto" id="divHidden" DESIGNTIMEDRAGDROP="203"><asp:datagrid id="dtgMain" runat="server" Width="100%" BackColor="#CDCDCD" BorderColor="#CDCDCD"
								AllowSorting="True" CellPadding="3" BorderWidth="0px" CellSpacing="1" PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CreditAccount" HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProductCategory" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PaymentType" HeaderText="Tipe Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaymentType" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPlafon" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOutStanding" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding SAP">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOutstandingSAP" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling Tersedia">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAvailablePlafon" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Telah Diajukan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPOInPropose" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Yg Akan Cair">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLiquefiedPO" runat="server" Text='0'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn visible="False" HeaderText="Giro Percepatan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAcceleratedGyro" runat="server" Text='0'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRemainPlafon" runat="server" Text="0"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Validitas Ceiling">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<cc1:inticalendar id="calMaxTOPDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="True" HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKeterangan" runat="server" Text=""></asp:Label>
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
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><br>
							<div style="DISPLAY: none"><STRONG>* Nilai sisa ceiling TOP tidak termasuk giro 
									percepatan</STRONG>
							</div>
							<br>
							<asp:label id="lblTotal" Visible="False" Runat="server" Font-Bold="True" text="TOTAL"></asp:label><asp:datagrid id="dtgMain2" runat="server" Width="100%" BackColor="#CDCDCD" BorderColor="#CDCDCD"
								AllowSorting="True" CellPadding="3" BorderWidth="0px" CellSpacing="1" PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo2" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CreditAccount" HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProductCategory2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PaymentType" HeaderText="Tipe Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaymentType2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPlafon2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOutStanding2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding SAP">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOutstandingSAP2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling Tersedia">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAvailablePlafon2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Telah Diajukan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPOInPropose2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Yg Akan Cair">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLiquefiedPO2" runat="server" Text='0'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn visible="False" HeaderText="Giro Percepatan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAcceleratedGyro2" runat="server" Text='0'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRemainPlafon2" runat="server" Text="0"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="" Visible="true">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<cc1:inticalendar Visible="False" id="calMaxTOPDate2" runat="server" TextBoxWidth="70"></cc1:inticalendar>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="" Visible="true">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label Visible="False" id="lblKeterangan2" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText=" " visible="true">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton Visible="False" id="lbtnDetail2" runat="server" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Lihat Detail">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
				
			}	
		</script>
	</body>
</HTML>

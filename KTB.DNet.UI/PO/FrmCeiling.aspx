<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCeiling.aspx.vb" Inherits="FrmCeiling"%>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCeiling</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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

		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		
		function AccountSelection(selectedAccount)
		{
			var txtCreditAccount = document.getElementById("txtCreditAccount");
			var txtDealerName = document.getElementById("txtDealerName");
			
			var str = selectedAccount.split(";");
			txtCreditAccount.value = str[0];			
			txtDealerName.value=str[1];
		}
		
		function ViewDailyPKFlow()
		{}
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
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input style="VISIBILITY: hidden" onclick="Spanning();" type="button">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<!--td class="titlePage">PO HARIAN &nbsp;- Daftar PO</td-->
					<td class="titlePage">CREDIT CONTROL&nbsp;- Ceiling Position Master Data</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="22%"><asp:label id="lblDealer" runat="server">Credit Account</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 17px" width="328"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="lblSalesOrg" runat="server" Font-Bold="True"> Permintaan Kirim</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px" width="20%"><cc1:inticalendar id="icReqDelivery" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label6" runat="server" Font-Bold="True">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerName" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="296px" BorderStyle="None" readonly="true"></asp:textbox></TD>
								<TD class="titleField"><asp:label id="Label7" runat="server"> Tanggal Laporan</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtReportDate" onblur="return alphaNumericPlusBlur(txtDealerPO)"
										runat="server" Width="140px" MaxLength="20" BorderColor="White" BackColor="#E0E0E0" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 24px"><asp:label id="Label1" runat="server" Font-Bold="True"> Total Ceiling</asp:label></TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 24px"><b><B><asp:label id="lblTotalCredit" runat="server" Width="176px"></asp:label></B></b></TD>
								<TD style="HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 24px"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 328px"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button><asp:textbox id="txtIsSpanned" style="VISIBILITY: hidden" runat="server" Width="16px" ReadOnly="True">1</asp:textbox></TD>
								<TD></TD>
								<TD></TD>
								<TD>
									<asp:button id="btnSaveMaxTOPDate" runat="server" Width="136px" Text="Simpan Tanggal Validitas"></asp:button></TD>
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
						<div id="divHidden" style="OVERFLOW: auto; HEIGHT: 340px" DESIGNTIMEDRAGDROP="203"><asp:datagrid id="dtgMain" runat="server" Width="100%" BorderColor="#CDCDCD" BackColor="#CDCDCD"
								AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
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
											<asp:Label id="lblCreditAccount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PaymentType" HeaderText="Tipe Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaymentType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentType") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPlafon" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Plafon") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Outstanding">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOutStanding" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OutStanding") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ceiling Tersedia">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAvailablePlafon" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvailablePlafon") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Dalam Proses">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblInProcess" runat="server" Text="0"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Sisa Plafon">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRemainPlafon1" runat="server" Text="0"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Telah Diajukan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPOInPropose" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProposedPO") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PO Yg Akan Cair">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLiquefiedPO" runat="server" Text='0'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Giro Percepatan">
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
									<asp:TemplateColumn Visible="False" HeaderText="Plafon PO Baru">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNewPOPlafon" runat="server" Text="0"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="True" HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKeterangan" runat="server" Text="0"></asp:Label>
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
							</asp:datagrid>
							<br>
							<STRONG>* Nilai sisa ceiling TOP tidak termasuk giro percepatan</STRONG>
						</div>
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

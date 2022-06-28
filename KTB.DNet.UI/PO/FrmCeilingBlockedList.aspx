<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCeilingBlockedList.aspx.vb" Inherits="FrmCeilingBlockedList"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCeilingBlockedList</title>
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
			showPopUp('../General/../PopUp/PopUpCreditAccountSelection.aspx','',500,760,AccountSelection);
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
		function ToggleRemark(chkID, txtID){
			var chk=document.getElementById(chkID);
			var txt=document.getElementById(txtID);
			if(chk.checked){
				txt.value="Tahan DO";
				txt.disabled=true;
			}
			else{
				txt.value="";
				txt.disabled=false;
			}
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
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			&nbsp;
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="95%" border="0">
				<tr>
					<!--td class="titlePage">PO HARIAN &nbsp;- Daftar PO</td-->
					<td class="titlePage">CREDIT CONTROL&nbsp;- Daftar Blok PO</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" style="WIDTH: 776px; HEIGHT: 128px" cellSpacing="1" cellPadding="2"
							width="776" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 132px; HEIGHT: 27px" width="132">Kode Dealer</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="WIDTH: 351px; HEIGHT: 27px" width="328"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 27px" width="20%">Nomor PO</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="HEIGHT: 27px" width="20%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtPONumber" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="168px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 132px">Akun Kredit</TD>
								<TD>:</TD>
								<TD style="WIDTH: 351px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAccount" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchCredit" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField">Nomor Reg. PO</TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtRegPONumber" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server" Width="168px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 132px; HEIGHT: 24px"><STRONG>Permintaan Kirim</STRONG></TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="WIDTH: 351px; HEIGHT: 24px">
									<table>
										<tr>
											<td>
												<cc1:inticalendar id="icReqDeliveryStart" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<td>
												s.d</td>
											<td>
												<cc1:inticalendar id="icReqDeliveryEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
										</tr>
									</table>
								</TD>
								<TD style="HEIGHT: 24px"><STRONG>Cara Pembayaran</STRONG></TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px"><asp:dropdownlist id="ddlPaymentType" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 132px; HEIGHT: 21px"><STRONG>Total Harga Tebus</STRONG></TD>
								<TD style="HEIGHT: 21px">:</TD>
								<TD style="WIDTH: 351px; HEIGHT: 21px">
									<asp:Label id="lblTotalHargaTebus" runat="server" Width="184px" Font-Bold="True" Height="15px">0</asp:Label></TD>
								<TD style="HEIGHT: 21px"><STRONG>Keterangan</STRONG></TD>
								<TD style="HEIGHT: 21px">:</TD>
								<TD style="HEIGHT: 21px">
									<table cellpadding="0" cellspacing="0">
										<tr>
											<td><asp:Image id="Image1" runat="server" Width="17px" Height="17px" ImageUrl="../Images/Red.gif"></asp:Image></td>
											<td>Blok</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 132px"><STRONG>Status</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 351px">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="140px"></asp:dropdownlist>
								</TD>
								<TD>
									<asp:TextBox style="VISIBILITY: visible" id="txtColorGreen" runat="server" Width="32px" BackColor="PaleGreen"
										Visible="False"></asp:TextBox></TD>
								<TD></TD>
								<TD>
									<table cellpadding="0" cellspacing="0">
										<tr>
											<td><asp:Image id="Image2" runat="server" Width="17px" Height="17px" ImageUrl="../Images/Yellow.gif"></asp:Image></td>
											<td>Pass Ceiling</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 132px"><STRONG>
										<asp:label id="lblFactoring" runat="server">Factoring</asp:label></STRONG></TD>
								<TD>
									<asp:label id="lblFactoringColon" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 351px">
									<asp:dropdownlist id="ddlFactoring" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
								<TD>
									<table cellpadding="0" cellspacing="0">
										<tr>
											<td><asp:Image id="Image3" runat="server" Width="17px" Height="17px" ImageUrl="../Images/Green.gif"></asp:Image></td>
											<td>Pass dan Konfirmasi</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 132px"><STRONG>Total Quantity</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 351px">
									<asp:label style="Z-INDEX: 0" id="lblQuantity" runat="server" Font-Bold="True"></asp:label></TD>
								<TD><STRONG>Produk</STRONG></TD>
								<TD>:</TD>
								<TD>
									<asp:dropdownlisT style="Z-INDEX: 0" id="ddlProductCategory" runat="server" width="130px"></asp:dropdownlisT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 132px"></TD>
								<TD></TD>
								<TD style="WIDTH: 351px"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 315px">
						<div id="divData" style="WIDTH: 776px; HEIGHT: 300px; OVERFLOW: auto"><asp:datagrid id="dtgMain" runat="server" Width="1400px" AllowSorting="True" CellPadding="3" BorderWidth="0px"
								CellSpacing="1" PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="#CDCDCD" AllowPaging="True">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" type="checkbox" onclick="CheckAll('chkItem',&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.forms[0].chkAllItems.checked)">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItem" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Image id="imgStatus" runat="server" Width="17px" Height="17px" ImageUrl="../Images/Green.gif"></asp:Image>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContractHeader.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ContractHeader.Dealer.CreditAccount" HeaderText="Credit Account">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCreditAccount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContractHeader.Dealer.CreditAccount") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="No Reg. PO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRegPONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerPONumber" HeaderText="No PO">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerPONumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Permintaan Kirim">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblReqDeliveryDate" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TermOfPayment.PaymentType" HeaderText="Cara Pembayaran">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPaymentType" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi Permintaan Kirim">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAllocationDate" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChangedTime" HeaderText="Waktu Perubahan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblChangedDate" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChangedBy" HeaderText="Oleh">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblChangedBy" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TotalHarga" HeaderText="Total Harga (VH)">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTotal" runat="server" Text="0"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:DropDownList id="ddlRemarkStatus" Width="128px" Runat="server"></asp:DropDownList>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Factoring">
										<HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblFactoring"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<TABLE style="WIDTH: 136px; HEIGHT: 28px">
												<TR>
													<TD>
														<asp:CheckBox id="chkRemark" Checked="False" runat="server" Text=" " style="visibility:hidden;"></asp:CheckBox>
														<asp:TextBox id="txtRemark" Runat="server" width="104px"></asp:TextBox></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetail" runat="server" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Lihat Detail">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnPopup" runat="server" CommandName="Popup">
												<img src="../images/Popup.gif" border="0" alt="Lihat Detail">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnFlow" runat="server" CommandName="Flow">
												<img src="../images/alur_flow.gif" border="0" alt="Lihat Detail">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 18px">
						<asp:button id="btnPass" runat="server" Width="72px" Text="Pass Ceiling"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnKonfirmasi" runat="server" Width="72px" Text="Konfirmasi"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnRelease" runat="server" Width="80px" Text="Release DO"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnSimpanGrid" runat="server" Width="80px" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Width="80px" Text="Batal" Visible="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD></TD>
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

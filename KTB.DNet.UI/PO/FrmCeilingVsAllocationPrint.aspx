<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCeilingVsAllocationPrint.aspx.vb" Inherits="FrmCeilingVsAllocationPrint"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCeilingVsAllocation</title>
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
			
			var str = selectedAccount.split(";");
			txtCreditAccount.value = str[0];	
		}
		
		function ShowPopupMaxPO()
		{
			var txtIsShowPopup = document.getElementById("txtIsShowPopup");
			if(txtIsShowPopup.value==1)
			{
				showPopUp('../PopUp/PopUpMaxPO.aspx','',500,760);
			}
		}
		
		
		function ViewDailyPKFlow()
		{}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">CREDIT CONTROL&nbsp;&nbsp;- Ceiling vs Alokasi</td>
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
								<TD style="HEIGHT: 6px" colspan="3">
									<asp:Label style="Z-INDEX: 0" id="lblCreditAccount" runat="server" Width="288px" Font-Bold="True"
										Font-Size="Larger" Height="10px"></asp:Label></TD>
								<TD style="HEIGHT: 6px"></TD>
								<TD style="WIDTH: 28px; HEIGHT: 6px"></TD>
								<TD style="HEIGHT: 6px"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 15px;width:100%" colspan="6">
									<asp:Label style="Z-INDEX: 0" id="lblPeriode" runat="server" Font-Bold="True" Font-Size="Larger"
										Width="400px" Height="10px"></asp:Label>
									<hr></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 15px"><STRONG>Tanggal Laporan</STRONG></TD>
								<TD style="WIDTH: 11px; HEIGHT: 15px">:</TD>
								<TD style="WIDTH: 328px; HEIGHT: 15px"><asp:label id="lblReportDate" runat="server" Width="128px"></asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="lblPaymentTypeTitle" runat="server" Width="128px" Font-Bold="True">PaymentType</asp:label></TD>
								<TD style="WIDTH: 28px; HEIGHT: 15px"><asp:label id="lblPaymetTypeColon" runat="server" Width="8px">:</asp:label></TD>
								<TD style="HEIGHT: 15px"><asp:label id="lblPaymentType" runat="server" Width="128px"></asp:label></TD>
							</TR>
							<TR>
								<TD><STRONG>Permintaan Kirim</STRONG></TD>
								<TD style="WIDTH: 11px">:</TD>
								<TD style="WIDTH: 328px"><asp:label id="lblReqDeliveryDate" runat="server" Width="128px"></asp:label></TD>
								<TD><STRONG>Sisa Hari Kerja</STRONG></TD>
								<TD style="WIDTH: 28px">:</TD>
								<TD><asp:label id="lblRemainDay" runat="server" Width="128px"></asp:label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD style="WIDTH: 11px"></TD>
								<TD style="WIDTH: 328px"></TD>
								<TD></TD>
								<TD style="WIDTH: 28px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="6">
									<asp:Button style="Z-INDEX: 0" id="btnKembali" runat="server" Text="Kembali"></asp:Button>
									<input id="btnCetak" style="WIDTH: 62px; HEIGHT: 21px" onclick="document.getElementById('btnKembali').style.visibility='hidden';document.getElementById('btnCetak').style.visibility='hidden';window.print();document.getElementById('btnKembali').style.visibility='visible';document.getElementById('btnCetak').style.visibility='visible';"
										type="button" value="Cetak">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<table height="100%">
				<tr vAlign="top">
					<td align="left"><asp:datagrid id="dtgMain" runat="server" Width="100%" ShowFooter="True" CellPadding="1" BorderWidth="1px"
							BorderColor="Black" PageSize="50" AllowCustomPaging="True" AutoGenerateColumns="False" AllowSorting="True"
							BackColor="#CDCDCD" Font-Size="XX-Small">
							<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
							<EditItemStyle VerticalAlign="Top"></EditItemStyle>
							<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="Smaller" Font-Bold="True" BackColor="Teal"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Font-Size="Smaller" Text='<%# DataBinder.Eval(Container, "DataItem.No") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Credit Account">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblCreditAccount runat="server" Font-Size="Smaller" Text='<%# DataBinder.Eval(Container, "DataItem.CreditAccount") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:Label ID="lblTotal" Font-Bold="True" Runat="server" Text="Total"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Produk">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblProduk" runat="server" Font-Size="Smaller" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategoryCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="TOP">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPaymentType" runat="server" Font-Size="Smaller" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentType") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ceiling">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCeiling" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Ceiling") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFCeiling" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Outstanding">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblOutstanding" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Outstanding") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFOutstanding" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PO Sedang Diajukan">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblProposed" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Proposed") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFProposed" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sisa Ceiling">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblRemainCeiling" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.RemainCeiling") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFRemainCeiling" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit O/C">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblOCQty" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.OCQty") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFOCQty" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nilai O/C">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblOCAmount" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.OCAmount") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFOCAmount" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Sisa O/C">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblRemainOCQty" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.RemainOCQty") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFRemainOCQty" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nilai Sisa O/C">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:label id="lblRemainOCAmount" Font-Size="Smaller" CommandName="DetailRemainOC" runat="server" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.RemainOCAmount") %>'>
										</asp:label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFRemainOCAmount" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Giro Cair Bulan Ini">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblGyro" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Gyro") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFGyro" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Max PO">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblMaxPO" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.MaxPO") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFMaxPO" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Estimasi Akhir Bulan">
									<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblEstimation" runat="server" Font-Size="Smaller" style="text-align:right;" Text='<%# DataBinder.Eval(Container, "DataItem.Estimation") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label Runat="server" ID="lblFEstimation" Font-Size="Smaller" style="text-align:right;"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
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

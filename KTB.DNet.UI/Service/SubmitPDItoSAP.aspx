<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SubmitPDItoSAP.aspx.vb" Inherits="SubmitPDItoSAP" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SubmitPDItoSAP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
			function userConfirm()
			{
				var a= confirm('Anda yakin mendownload data ini ?');
				
				return a;
			}
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PRE DELIVERY INSPECTION&nbsp;- Transfer&nbsp;PDI ke SAP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td width="35%" class="titleField">Dealer
								</td>
								<td width="1%">:</td>
								<td width="64%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeDealer" runat="server" Width="256px"
										ToolTip="Dealer Search 1"></asp:textbox>
									<asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField">PDI dirilis sampai dengan tanggal</TD>
								<td>:</td>
								<td><cc1:IntiCalendar id="ICSampai" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
							</TR>
							<TR>
								<TD><STRONG>Kategori</STRONG></TD>
								<TD>:</TD>
								<TD>
									<asp:dropdownlist style="Z-INDEX: 0" id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td><asp:Button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD><DIV id="div1" style="HEIGHT: 350px; OVERFLOW: auto"><asp:datagrid id="dtgSubmPDISAP" runat="server" PageSize="50" ShowFooter="True" Width="100%" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BackColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<FooterStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" ForeColor="Aqua" BackColor="Blue"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle BackColor="#CCCCFF"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")& " - " & DataBinder.Eval(Container, "DataItem.SearchTerm1")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle BackColor="#CCCCFF"></FooterStyle>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblName" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle BackColor="#CCCCFF"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCity" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle BackColor="#CCCCFF"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="Label1" runat="server">Total : </asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id="Label2" runat="server">Label</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total PDI">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTotalPDI" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" BackColor="#CCCCFF"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblTotal" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD height="40">
						<asp:button id="btnDownload" runat="server" Width="96px" Text="Download Ulang"></asp:button></TD>
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

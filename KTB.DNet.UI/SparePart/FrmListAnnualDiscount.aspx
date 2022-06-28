<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListAnnualDiscount.aspx.vb" Inherits="FrmListAnnualDiscount" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListAnnualDiscount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
	   	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">ANNUAL DISCOUNT&nbsp;- Daftar&nbsp;Item Annual Discount</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="7"><STRONG>Periode :&nbsp;
							<asp:label id="lblPeriodeFrom" runat="server"></asp:label>&nbsp;&nbsp;
							<asp:label id="lblsd" runat="server"> s/d</asp:label>&nbsp; </STRONG>
						<asp:label id="lblPeriodeTo" runat="server" Font-Bold="True"></asp:label>&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="7" style="HEIGHT: 17px"></TD>
				</TR>
				<TR>
					<TD colSpan="7" style="HEIGHT: 23px">
						<asp:Button id="Button1" runat="server" Text="Kembali" Width="86px"></asp:Button>&nbsp;
						<asp:button id="btnDownloadXls" runat="server" Width="96px" Text="Download"></asp:button></TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgAnnualDiscount" runat="server" CellPadding="3" BorderWidth="0px" CellSpacing="1"
							BorderColor="#CDCDCD" BackColor="#CDCDCD" AllowSorting="True" AllowPaging="True" PageSize="25"
							AllowCustomPaging="True" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Silver"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="Id" HeaderText="Id"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="PartNo" SortExpression="PartNo" HeaderText="Nomor Barang">
									<HeaderStyle Width="17%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PartName" SortExpression="PartName" HeaderText="Nama Barang">
									<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Model" SortExpression="Model" HeaderText="Model">
									<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MinimumQty" SortExpression="MinimumQty" HeaderText="Minimum Qty" DataFormatString="{0:#,###}">
									<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Point" SortExpression="Point" HeaderText="Point" DataFormatString="{0:#,###}">
									<HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div></TD>
				</TR>
				<TR>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
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

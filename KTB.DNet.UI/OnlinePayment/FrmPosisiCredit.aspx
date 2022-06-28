<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPosisiCredit.aspx.vb" Inherits="FrmPosisiCredit" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Credit Control - Laporan Posisi Credit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPCreditAcctSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpCreditAccount.aspx','',500,760,CreditAcctSelection);
			}
			
			function CreditAcctSelection(SelectedAcct)
			{
				var txtCreditAcct = document.getElementById("txtCreditAcct");
				txtCreditAcct.value = SelectedAcct;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Credit Control - Laporan Posisi Credit</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblCreditAcct" runat="server">Credit Acct</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtCreditAcct" onblur="omitSomeCharacter('txtCreditAcct','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchCreditAcct" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" height="20"><asp:label id="lblTglDueDate" runat="server">Tgl Jatuh Tempo</asp:label></TD>
								<TD>:</TD>
								<TD><cc1:inticalendar id="icTglDueDate" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<asp:panel Visible="False"></asp:panel>
							<tr>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colSpan="4"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="60px"></asp:button></TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgCreditOutstanding" runat="server" Width="100%" AllowPaging="True" PageSize="50"
					AllowSorting="True" AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" CellSpacing="1">
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblNo" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Credit Account">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>' ID="lblCreditAccount" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="DealerName" HeaderText="Dealer">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName") %>' ID="lblDealerName" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="CeilingAmount" HeaderText="Credit Ceiling">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CeilingAmount"),"#,##0") %>' ID="lblCreditCeiling" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="AllOutStanding" HeaderText="All OS">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.AllOutStanding"),"#,##0")%>' ID="lblAllOS" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Regular OS">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblRegOS"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Total Temp OS">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblTempOS"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Temp Project OS">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblProjectTempOS"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Temp Project Special">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblSpecialOS"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="RejectedOS" HeaderText="Rejected OS">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.RejectedOS"),"#,##0") %>' ID="Label3" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BlokedAmount" HeaderText="Blocked">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.BlokedAmount"),"#,##0") %>' ID="Label4" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Avalaible Reg TOP">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign=Right></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblAvailbleRegTOP"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
			<br>
			<asp:button id="btnDownload" runat="server" Text="Download" Width="72px"></asp:button></form>
	</body>
</HTML>

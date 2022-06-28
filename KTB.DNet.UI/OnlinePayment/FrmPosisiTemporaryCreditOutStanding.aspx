<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPosisiTemporaryCreditOutStanding.aspx.vb" Inherits="FrmPosisiTemporaryCreditOutStanding"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemporaryCreditOutstandingPosition</title>
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
				showPopUp('../SparePart/../PopUp/PopUpCreditAccount.aspx','',500,380,CreditAcctSelection);
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
					<td class="titlePage">Credit Control - Posisi Temporary Credit Outstanding</td>
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
							<TR>
								<TD class="titleField"><asp:label id="lblTempKind" runat="server">Temporary Kind</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtTempKind" onblur="omitSomeCharacter('txtTempKind','<>?*%$;')"
										runat="server"></asp:textbox></TD>
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
							<tr>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colSpan="4"><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgCreditOutstanding" runat="server" Width="100%" CellSpacing="1" CellPadding="3"
					BackColor="#CDCDCD" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" AllowSorting="True" PageSize="50" AllowPaging="True">
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
						<asp:TemplateColumn SortExpression="TemporaryKind" HeaderText="Temporary Kind">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TemporaryKind")%>' ID="lblTemporaryKind" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ProjectName" HeaderText="Project Name">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>' ID="Label2" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="SPLNumber" HeaderText="No. SPL">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SPLNumber") %>' ID="lblSPLNumber" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="CeilingAmount" HeaderText="Credit Ceiling">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CeilingAmount"),"#,###") %>' ID="lblCreditCeiling" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="TemporaryCreditExposure" HeaderText="Amount Outstanding">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.TemporaryCreditExposure"),"#,###") %>' ID="Label1" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BlokedName" HeaderText="Status">
							<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BlokedName") %>' ID="Label3" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
			<br>
			<asp:button id="btnDownload" runat="server" Width="72px" Text="Download"></asp:button></form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPartIncidentalKTB.aspx.vb" Inherits="FrmListPartIncidentalKTB" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListPartIncidentalKTB</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
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
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD width="24%" colSpan="6">
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">PERMINTAAN KHUSUS - Daftar&nbsp;Permintaan&nbsp;Khusus dari 
									Dealer</TD>
							</TR>
							<TR>
								<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="24%">Status &nbsp;MKS</TD>
					<TD width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 229px" width="229"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
					<TD style="WIDTH: 147px" width="147"></TD>
					<TD style="WIDTH: 215px" width="215"></TD>
					<TD width="50%"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label2" runat="server" Width="80px">Kode Dealer</asp:label></TD>
					<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 300px" colSpan="4"><asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
							runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField">Tanggal Pesanan</TD>
					<TD>:</TD>
					<TD style="WIDTH: 229px"><cc1:inticalendar id="intFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
					<TD style="WIDTH: 147px">s/d</TD>
					<TD style="WIDTH: 215px"><cc1:inticalendar id="intTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
					<TD><asp:button id="btnFind" runat="server" Width="80px" Text="Cari"></asp:button></TD>
				</TR>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
					<TD height="10"></TD>
					<TD style="WIDTH: 229px" height="10"></TD>
					<TD style="WIDTH: 147px" height="10"></TD>
				</tr>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dgPartList" runat="server" Width="100%" CellPadding="1" BorderWidth="0px" CellSpacing="1"
								BackColor="#CDCDCD" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="Dealer.SearchTerm2" HeaderText="Kode Dealer 2">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RequestNumber" SortExpression="RequestNumber" HeaderText="Nomor Permintaan">
										<HeaderStyle ForeColor="White" Width="12%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IncidentalDate" SortExpression="IncidentalDate" HeaderText="Tanggal Pesanan"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PoliceNumber" SortExpression="PoliceNumber" HeaderText="No Polisi">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WorkOrder" SortExpression="WorkOrder" HeaderText="WO">
										<HeaderStyle ForeColor="White" Width="11%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Status" SortExpression="Status" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PIC" SortExpression="PIC" HeaderText="PIC">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="KTBStatus" HeaderText="Status MMKSI">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKtbStatus" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Prioritas">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPriority" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDetail" runat="server" CommandName="Detail">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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

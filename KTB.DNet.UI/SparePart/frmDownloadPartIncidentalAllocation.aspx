<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmDownloadPartIncidentalAllocation.aspx.vb" Inherits="frmDownloadPartIncidentalAllocation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmDownloadPartIncidentalAllocation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
								<TD class="titlePage">PERMINTAAN KHUSUS - Alokasi Pemesanan Khusus</TD>
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
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dgPartList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BackColor="#CDCDCD" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.RequestNumber" HeaderText="Req-No">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblReqNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.DealerMailNumber" HeaderText="Nomor Surat">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerMailNumber" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.IncidentalDate" HeaderText="PK Date">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPKDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PlanDate" HeaderText="Plan Date">
										<HeaderStyle Width="6%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPlanDate" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Part No">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Part Name">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model Code">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblModelCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.SupplierCode" HeaderText="Supplier Code">
										<HeaderStyle Font-Bold="True" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSupplierCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Part Sub No">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Part Sub Name">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Quantity" HeaderText="Org Quantity">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOrgQty" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Remain Quantity">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRemainQty" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="txtAlokasi" runat="server" Width="40px"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Type Code">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTypeCode" runat="server" Width="40px"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Prod Year">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProdYear" runat="server" Width="40px"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Priority">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPriority" runat="server" Width="40px"></asp:Label>
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

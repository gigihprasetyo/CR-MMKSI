<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalPOProcess.aspx.vb" Inherits="FrmPartIncidentalPOProcess"  smartNavigation="False"%>
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
		
		function Process(){
			document.getElementById("btnProses").disabled=true;
			document.getElementById("btnSave").click();
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
								<TD class="titlePage">PERMINTAAN KHUSUS - Upload Pesanan Khusus</TD>
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
					<TD class="titleField" style="WIDTH: 147px; HEIGHT: 26px"><asp:label id="Label2" runat="server" Width="80px">Kode Dealer</asp:label></TD>
					<TD style="HEIGHT: 26px"><asp:label id="Label4" runat="server">:</asp:label></TD>
					<TD style="WIDTH: 300px; HEIGHT: 26px" colSpan="4"><asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
							runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 147px">PK&nbsp;Date</TD>
					<TD>:</TD>
					<TD style="WIDTH: 404px" noWrap>
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td><asp:checkbox id="cbPKDate" runat="server"></asp:checkbox></td>
								<td><cc1:inticalendar id="intPKDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 147px; HEIGHT: 26px">Request Number</TD>
					<TD style="HEIGHT: 26px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 26px"><asp:textbox id="txtReqNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtReqNumber','<>?*%$;')"
							runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 147px; HEIGHT: 24px">Part Number</TD>
					<TD style="HEIGHT: 24px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 24px"><asp:textbox id="txtPartNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtPartNumber','<>?*%$;')"
							runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 147px; HEIGHT: 24px">PO Number</TD>
					<TD style="HEIGHT: 24px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 24px"><asp:textbox id="txtPoNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtPoNumber','<>?*%$;')"
							runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 147px; HEIGHT: 24px">Sudah ada Nomor PO</TD>
					<TD style="HEIGHT: 24px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 24px">
						<asp:RadioButtonList id="rbPO" runat="server" Font-Bold="True" RepeatDirection="Horizontal">
							<asp:ListItem Value="Ya">Ya</asp:ListItem>
							<asp:ListItem Value="Tidak" Selected="True">Tidak</asp:ListItem>
							<asp:ListItem Value="Semua">Semua</asp:ListItem>
						</asp:RadioButtonList></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 147px; HEIGHT: 26px">Plan Date</TD>
					<TD style="HEIGHT: 26px">:</TD>
					<TD style="WIDTH: 404px; HEIGHT: 26px" noWrap>
						<table cellSpacing="0" cellPadding="1" border="0">
							<tr>
								<td><asp:checkbox id="cbPlanDate" runat="server"></asp:checkbox></td>
								<td><cc1:inticalendar id="intFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td><asp:label id="Label1" runat="server">s/d</asp:label></td>
								<td><cc1:inticalendar id="intTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 230px"><asp:datagrid id="dgPartList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BackColor="#CDCDCD" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.PartIncidentalHeader.RequestNumber" HeaderText="Req-No">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblReqNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.PartIncidentalHeader.IncidentalDate" HeaderText="PK Date">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPKDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.PlanDate" HeaderText="Plan Date">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPlanDate" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="PO Number">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=Label5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.SparePartMaster.PartNumber" HeaderText="Part No">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.SparePartMaster.PartName" HeaderText="Part Name">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Part Sub No">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPartSubNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StatusDetail" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatusDetail" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalDetail.Quantity" HeaderText="Org Quantity">
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
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtAlokasi" runat="server" Width="40px" CssClass="textRight"></asp:TextBox>
											<asp:RangeValidator id="RangeValidator1" runat="server" MinimumValue="0" MaximumValue="10000000" ControlToValidate="txtAlokasi"
												ErrorMessage="*" Type="Integer">*</asp:RangeValidator>
											<asp:Label id="lblAlokasi" runat="server"></asp:Label>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtAlokasi" ErrorMessage="*"></asp:RequiredFieldValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Proses">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="lbtnSave" runat="server" CommandName="Save">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2">
							<TR>
								<TD colSpan="2">Keterangan Status</TD>
							</TR>
							<TR>
								<TD><IMG src="../images/green.gif" border="0"></TD>
								<TD>Aktif</TD>
							</TR>
							<TR>
								<TD><IMG src="../images/yellow.gif" border="0"></TD>
								<TD>Batal Sebagian</TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<input type="button" id="btnProses" value="Proses" onclick="Process();" />					
						<asp:button id="btnSave" runat="server" Text="Proses" style="display:none;"></asp:button>
						<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button>
						<asp:button id="btnCancel" runat="server" Text="Batal"></asp:button></td>
				</tr>
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
